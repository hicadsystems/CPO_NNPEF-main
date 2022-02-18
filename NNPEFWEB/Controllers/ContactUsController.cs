using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NNPEFWEB.Models;
using NNPEFWEB.Service;
using NNPEFWEB.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly IContactUsService _contactUsService;
        public ContactUsController(IContactUsService contactUsService)
        {
            this._contactUsService = contactUsService;
        }

        // GET: ContactUsController
        [HttpGet]
        public IActionResult Index()
        {
            var contactList = _contactUsService.GetContacts();

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
        [ValidateAntiForgeryToken]
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
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View();
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
