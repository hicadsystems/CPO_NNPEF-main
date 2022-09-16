using NNPEFWEB.Models;
using NNPEFWEB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{
    public class ContactUsService : IContactUsService
    {
        private readonly IUnitOfWorks unitOfWork;
        public ContactUsService(IUnitOfWorks unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateContact(ef_ContactUs contact)
        {
            var contactEntity = unitOfWork.ContactUs.Create(contact);
            await unitOfWork.Done();

            return contactEntity;
        }

        public async Task<bool> DeleteContactById(int id)
        {
            bool response = false;

            var entityInDB = unitOfWork.ContactUs.Find(id).Result;

            if (entityInDB != null)
            {
                response = unitOfWork.ContactUs.Remove(entityInDB);
                await unitOfWork.Done();

                return response;
            }

            return response;
            
        }

        public ef_ContactUs GetContactById(int id)
        {
            return unitOfWork.ContactUs.Find(id).Result;
        }

        public IEnumerable<ef_ContactUs> GetContacts()
        {
            return unitOfWork.ContactUs.All();
        }
        public IEnumerable<ef_ContactUs> GetListContacts()
        {
            return unitOfWork.ContactUs.GetContactUs();
        }

        public async Task<string> UpdateContactInfo(ef_ContactUs contact)
        {
            var contactInDB = unitOfWork.ContactUs.Find(contact.Id).Result;

            if (contactInDB ==  null)
            {
                return "Contact Doesn't Exist!!!";
            }
            else
            {
                contactInDB.Id = contact.Id;
                contactInDB.PersonName = contact.PersonName;
                contactInDB.Ship = contact.Ship;
                contactInDB.Email = contact.Email;
                contactInDB.PhoneNumber = contact.PhoneNumber;
                contactInDB.Message = contact.Message;
                contactInDB.Response = contact.Response;

                unitOfWork.ContactUs.Update(contactInDB);
                await unitOfWork.Done();

                return "Contact Updated Successfully!!!";
            }
        }
    }
}
