using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using NNPEFWEB.Data;
using NNPEFWEB.Models;

namespace NNPEFWEB.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext context;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext _context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            context = _context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }
            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
            [Required]
            [Display(Name = "User Name")]
            public string UserName { get; set; }

            //[Required]
            //[Display(Name = "Command")]
            //public int Command { get; set; }
            //[Required]
            //[Display(Name = "Ship")]
            //public int ship { get; set; }
            //public int spec { get; set; }
            [Required]
            [Display(Name = "Rank")]
            public string Rank { get; set; }
           
            [Required]
            [Display(Name = "Appointment")]
            public string Appointment { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }
      
        public List<ef_rank> RankList { get; set; }
        //public List<ef_command> CommandList { get; set; }
        //public List<ef_ship> ShipList { get; set; }
        //public List<ef_specialisationarea> SpecList { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            
            RankList = (from rk in context.ef_ranks
                           select new ef_rank()
                           {
                               rankName = rk.rankName,
                               Id = rk.Id,
                           }).ToList();

           

            //CommandList = (from rk in context.ef_commands
            //               select new ef_command()
            //               {
            //                   commandName = rk.commandName,
            //                   code = rk.code,
            //               }).ToList();

            //ShipList = (from sh in context.ef_ships
            //               select new ef_ship()
            //               {
            //                   shipName = sh.shipName,
            //                   Id = sh.Id,
            //               }).ToList();

            //SpecList = (from sp in context.ef_specialisationareas
            //            select new ef_specialisationarea()
            //            {
            //                specName = sp.specName,
            //                Id = sp.Id,
            //            }).ToList();



            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                MailAddress address = new MailAddress(Input.Email);
                string userName = address.User;
               
                var user = new User {
                    UserName = Input.UserName,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    Rank=Input.Rank,
                    Appointment=Input.Appointment

                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    if(Input.Appointment=="CDR")
                    await _userManager.AddToRoleAsync(user, Enum.Roles.CDR.ToString());
                    if (Input.Appointment == "HOD")
                        await _userManager.AddToRoleAsync(user, Enum.Roles.HOD.ToString());
                    if (Input.Appointment == "DO")
                        await _userManager.AddToRoleAsync(user, Enum.Roles.DO.ToString());
                    if (Input.Appointment == "Admin")
                        await _userManager.AddToRoleAsync(user, Enum.Roles.Admin.ToString());
                    if (Input.Appointment == "SEC")
                        await _userManager.AddToRoleAsync(user, Enum.Roles.Operator.ToString());
                    if (Input.Appointment == "Section A")
                        await _userManager.AddToRoleAsync(user, Enum.Roles.SecionA.ToString());
                    if (Input.Appointment == "Section B")
                        await _userManager.AddToRoleAsync(user, Enum.Roles.SecionB.ToString());
                    if (Input.Appointment == "Section C")
                        await _userManager.AddToRoleAsync(user, Enum.Roles.SecionC.ToString());
                    if (Input.Appointment == "Section D")
                        await _userManager.AddToRoleAsync(user, Enum.Roles.SecionD.ToString());
                    if (Input.Appointment == "Section E")
                        await _userManager.AddToRoleAsync(user, Enum.Roles.SecionE.ToString());
                    if(Input.Appointment == "Officers Section")
                        await _userManager.AddToRoleAsync(user, Enum.Roles.OfficersSection.ToString());

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "UserRole");
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    TempData["regError"] = error.Description;
                    return RedirectToPage();
                }
            }

            // If we got this far, something failed, redisplay form
            return RedirectToPage();
        }
    }
}
