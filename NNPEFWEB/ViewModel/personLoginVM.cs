using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.ViewModel
{
    public class personLoginVM
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string password { get; set; }
        public string svcNo { get; set; }
        public string oldsvcno { get; set; }
        public string rank { get; set; }
        public string payClass { get; set; }
        public string surName { get; set; }
        public string otheName { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string ship { get; set; }
        public string appointment { get; set; }
        public string department { get; set; }
        public string AccountName { get; set; }
        public string AccountNo  { get; set; }
         public string Bank { get; set; }
         public DateTime? DateofBirth  { get; set; }
        public DateTime? DateofJoiningSvc { get; set; }
        public string account_number { get; set; }
        public string date_of_birth { get; set; }
        public string date_of_joining { get; set; }
    }
    
}
