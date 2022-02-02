using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Models
{
    public class ef_ContactUs
    {
        public int Id { get; set; }
        public string PersonName { get; set; }
        public string  Ship { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public string Response { get; set; }
    }
}
