using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Models
{
    public class ef_specialisationarea
    {
        [Key]
        public int Id { get; set; }
        public string specName { get; set; }
      }
    }
