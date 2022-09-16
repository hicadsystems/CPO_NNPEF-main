using NNPEFWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Repository
{
    public interface IContactUs : IGenericRepository<ef_ContactUs>
    {
        IEnumerable<ef_ContactUs> GetContactUs();
    }
}
