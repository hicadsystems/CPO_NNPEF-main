using NNPEFWEB.Models;
using NNPEFWEB.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{
    public interface IContactUsService
    {
        IEnumerable<ef_ContactUs> GetContacts();
        Task<bool> CreateContact(ef_ContactUs contact);
        Task<string> UpdateContactInfo(ef_ContactUs contact);
        ef_ContactUs GetContactById(int id);
        Task<bool> DeleteContactById(int id);
    }
}
