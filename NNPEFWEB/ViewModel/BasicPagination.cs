using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.ViewModel
{
    public class BasicPagination
    {

            public string sEcho { get; set; }

            public int iDisplayStart { get; set; }

            public int iDisplayLength { get; set; }

            public int aaData { get; set; }

            public int iTotalRecords { get; set; }

            public int iTotalDisplayRecords { get; set; }

            public string status { get; set; }

            public string errormessage { get; set; }
        }

        public class BasePaginationSearch
        {
            public string sEcho { get; set; }

            public int iDisplayStart { get; set; }

            public int iDisplayLength { get; set; }


        }
    }

