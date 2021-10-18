using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.ViewModel
{
    public class systeminfoVM
    {
        public int Id { get; set; }
        public string comp_code { get; set; }
        public string comp_name { get; set; }
        public string Address { get; set; }
        public int SiteStatus { get; set; }
        public DateTime opendate { get; set; }
        public DateTime closedate { get; set; }
        public string hrlink { get; set; }
        public string town { get; set; }
        public string lg { get; set; }
        public string state { get; set; }
        public string email { get; set; }
        public string box { get; set; }
        public string tel { get; set; }
        public string OfficersFormNo { get; set; }
        public string RatingsFormNo { get; set; }
        public string TrainingFormNo { get; set; }
        public string serveraddr { get; set; }
        public string serverport { get; set; }
        public string email_pword { get; set; }
        public byte[] company_image { get; set; }
        public DateTime datecreated { get; set; }
        public string createdby { get; set; }
    }
}
