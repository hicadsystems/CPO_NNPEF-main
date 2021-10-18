using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Models
{
    public class ef_state
    {
        [Key]
        public int StateId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int geozoneid { get; set; }
        public int CountryId { get; set; }
   
    }
}
