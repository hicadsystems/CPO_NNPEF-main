using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Models
{
    public class ef_localgovt
    {
        [Key]
        public int Id { get; set; }
        public string code { get; set; }
        public string lgaName { get; set; }
        public int StateId { get; set; }
    }
}
