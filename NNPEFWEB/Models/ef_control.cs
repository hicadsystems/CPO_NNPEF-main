using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Models
{
    public class ef_control
    {
        [Key]
        public int Id { get; set; }
        public string ship { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public  string processingyear { get; set; }
        public string status { get; set; }
        public string createdby { get; set; }
        public DateTime datecreated { get; set; }
    }
}
