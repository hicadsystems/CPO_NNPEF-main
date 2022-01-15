using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using NNPEFWEB.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace NNPEFWEB.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _config;
        public ForgotPasswordModel(UserManager<User> userManager, IEmailSender emailSender, IConfiguration config)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _config = config;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ResetPassword",
                        pageHandler: null,
                        values: new { area = "Identity", code },
                        protocol: Request.Scheme);

                    string messageToSend = $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";

                    var UserName = _config.GetValue<string>("mailconfig:SenderEmail");
                    var Password = _config.GetValue<string>("mailconfig:Password");
                    string sender = _config.GetValue<string>("mailconfig:SenderEmail");
                    string emailFrom = "NN-CPO";
                    var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                    var message = new MailMessage();
                    message.To.Add(new MailAddress(Input.Email));

                    message.From = new MailAddress(sender, emailFrom);
                    message.Subject = "Password Verification Code";
                    message.Body = string.Format(body, emailFrom, sender, messageToSend);
                    message.IsBodyHtml = true;

                    string host = _config.GetValue<string>("mailconfig:Server");
                    int port = _config.GetValue<int>("mailconfig:Port");
                    var enableSSL = _config.GetValue<bool>("mailconfig:enableSSL");

                    SmtpClient SmtpServer = new SmtpClient(host);

                    var smtp = new SmtpClient
                    {
                        Host = host,
                        Port = port,
                        EnableSsl = enableSSL,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = true,
                        Credentials = new NetworkCredential(UserName, Password)
                    };
                    SmtpServer.Port = port; // Also Add the port number to send it, its default for Gmail
                    SmtpServer.Credentials = new System.Net.NetworkCredential(UserName, Password);
                    SmtpServer.EnableSsl = enableSSL;
                    SmtpServer.Timeout = 200000; // Add Timeout property
                    SmtpServer.Send(message);

                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                //var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                //var callbackUrl = Url.Page(
                //    "/Account/ResetPassword",
                //    pageHandler: null,
                //    values: new { area = "Identity", code },
                //    protocol: Request.Scheme);

                //await _emailSender.SendEmailAsync(
                //    Input.Email,
                //    "Reset Password",
                //    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                
                
                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
