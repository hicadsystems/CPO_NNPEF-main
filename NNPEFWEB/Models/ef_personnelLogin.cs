using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Models
{
    public class ef_personnelLogin
    {
        public int Id { get; set; }
        public string svcNo { get; set; }
        public string rank { get; set; }
        public string payClass { get; set; }
        public string surName { get; set; }
        public string otheName { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public bool confirmPassword { get; set; }
        public Nullable<System.DateTime> dateCreated { get; set; }
        public Nullable<System.DateTime> loginDate { get; set; }
        public Nullable<System.DateTime> expireDate { get; set; }
        public string Appointment { get; set; }
        public string ship { get; set; }
        public int openship { get; set; }
        public string spec { get; set; }
    }
}
