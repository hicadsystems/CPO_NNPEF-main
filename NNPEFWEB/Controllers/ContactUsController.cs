using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NNPEFWEB.Models;
using NNPEFWEB.Service;
using NNPEFWEB.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NNPEFWEB.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly IContactUsService _contactUsService;
        private readonly IConfiguration config;
        public ContactUsController(IContactUsService contactUsService, IConfiguration configuration)
        {
            this._contactUsService = contactUsService;
            config = configuration;
        }

        // GET: ContactUsController
        [HttpGet]
        public IActionResult Index()
        {
            var contactList = _contactUsService.GetContacts().Where(x=>x.Response==null);

            List<ContactUsViewModel> contactUsViewModel = new List<ContactUsViewModel>();

            foreach (var contact in contactList)
            {
                var contactUsVM = new ContactUsViewModel
                {
                    Id = contact.Id,
                    PersonName = contact.PersonName,
                    Ship = contact.Ship,
                    Email = contact.Email,
                    PhoneNumber = contact.PhoneNumber,
                    Message = contact.Message,
                    Response = contact.Response
                };

                contactUsViewModel.Add(contactUsVM);
            }

            return View(contactUsViewModel);
        }

        // GET: ContactUsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ContactUsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactUsController/Create
        [HttpPost]
        public IActionResult Create(ContactUsViewModel contactsVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newContact = new ef_ContactUs
                    {
                        PersonName = contactsVM.PersonName,
                        Ship = contactsVM.Ship,
                        Email = contactsVM.Email,
                        PhoneNumber = contactsVM.PhoneNumber,
                        Message = contactsVM.Message
                    };

                    var createdContact = _contactUsService.CreateContact(newContact).Result;

                    if (createdContact == true)
                    {
                        TempData["Message"] = "Ticket Successfully Summitted!!!";
                    }
                    else
                    {
                        TempData["Message"] = "Error occured while trying to Create Contact!!!";
                    }
                }
                return RedirectToAction("homepage","Home");
            }
            catch(Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("homepage", "Home");
            }
        }

        // GET: ContactUsController/Edit/5
        public ActionResult Edit(int id)
        {
            ContactUsViewModel contactUsViewModel = new ContactUsViewModel();
            try
            {
                var contactInDB = _contactUsService.GetContactById(id);

                if (contactInDB != null)
                {
                    contactUsViewModel.Id = contactInDB.Id;
                    contactUsViewModel.PersonName = contactInDB.PersonName;
                    contactUsViewModel.Ship = contactInDB.Ship;
                    contactUsViewModel.Email = contactInDB.Email;
                    contactUsViewModel.PhoneNumber = contactInDB.PhoneNumber;
                    contactUsViewModel.Message = contactInDB.Message;
                    contactUsViewModel.Response = contactInDB.Response;

                    return View(contactUsViewModel);
                }
                else
                {
                    TempData["Message"] = "Contact doesn't exist!!";
                }
            }
            catch (Exception ex)
            {

                TempData["Message"] = ex.Message;
            }
            return View(contactUsViewModel);
        }

        // POST: ContactUsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContactUsViewModel contactsVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newContact = new ef_ContactUs
                    {
                        Id = contactsVM.Id,
                        PersonName = contactsVM.PersonName,
                        Ship = contactsVM.Ship,
                        Email = contactsVM.Email,
                        PhoneNumber = contactsVM.PhoneNumber,
                        Message = contactsVM.Message,
                        Response = contactsVM.Response
                    };
                    string emailFrom = "NN-CPO";
                    string emailSender = config.GetValue<string>("mailconfig:SenderEmail");
                    if (!string.IsNullOrEmpty(contactsVM.Email))
                        SendEmailNotification(emailFrom, emailSender, contactsVM.Response+" To Your "+contactsVM.Message , contactsVM.Email);

                    var response = _contactUsService.UpdateContactInfo(newContact).Result;

                    TempData["Message"] = response;
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View(contactsVM);
            }
        }
        public void SendEmailNotification(string emailFrom, string sender, string messageToSend, string receipient)
        {

            var UserName = config.GetValue<string>("mailconfig:SenderEmail");
            var Password = config.GetValue<string>("mailconfig:Password");
            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(receipient));
            message.From = new MailAddress(sender, emailFrom);
            message.Subject = "Password Verification Code";
            message.Body = string.Format(body, emailFrom, sender, messageToSend);
            message.IsBodyHtml = true;



            string host = config.GetValue<string>("mailconfig:Server");
            int port = config.GetValue<int>("mailconfig:Port");
            var enableSSL = config.GetValue<bool>("mailconfig:enableSSL");

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


        }
        // GET: ContactUsController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var contactInDb = _contactUsService.GetContactById(id);

                if (contactInDb != null)
                {
                    var response = _contactUsService.DeleteContactById(contactInDb.Id).Result;

                    if (response == true)
                        TempData["Message"] = "Contact Deleted Successfully!!! ";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;

                return RedirectToAction(nameof(Index));
            }
        }

        // POST: ContactUsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
