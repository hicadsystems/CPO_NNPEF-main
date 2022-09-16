using NNPEFWEB.Data;
using NNPEFWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Repository
{
    public class ContactUsRepository : GenericRepository<ef_ContactUs>, IContactUs
    {
        private readonly ApplicationDbContext _context;
        public ContactUsRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }
        public IEnumerable<ef_ContactUs> GetContactUs()
        {
            var con = new List<ef_ContactUs>();
            con = (from c in _context.ef_contactUs
                   where (c.Response == null)
                   select new ef_ContactUs
                   {
                       Id=c.Id,
                       Email=c.Email,
                       Message=c.Message,
                       PersonName=c.PersonName,
                       PhoneNumber=c.PhoneNumber,
                       Response=c.Response,
                       Ship=c.Ship
                   }).ToList();
            return con;
        }
      
    }
}
