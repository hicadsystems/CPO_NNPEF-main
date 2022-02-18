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
    }
}
