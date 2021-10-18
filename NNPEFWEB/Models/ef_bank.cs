using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Models
{
    public class ef_bank
    {
            [Key]
            public string bankcode { get; set; }
            public string bankname { get; set; }
            public string branchname { get; set; }
            public string address { get; set; }
            public string email { get; set; }
            public string cbn_code { get; set; }
            public string cbn_branch { get; set; }
        }
    }
