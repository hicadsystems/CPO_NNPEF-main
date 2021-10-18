using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.ViewModel
{
    public class ApiSearchModel: BasicPagination
    {
        public string sortby { get; set; }
        public string command { get; set; }

    }
}
