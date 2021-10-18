using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.ViewModel
{
    public class CommonProperties
    {
        public DateTime CreatedDate { get; set; } = new DateTime(1900, 1, 1);
        public DateTime UpdatedDate { get; set; } = new DateTime(1900, 1, 1);
        public string Message { get; set; } 
    }
}
