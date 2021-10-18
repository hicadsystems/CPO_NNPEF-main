using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.ViewModel
{
    public class MailClass
    {
        public string fromMailId { get; set; }
        public string fromMailPassword { get; set; }
        public List<string> toMailIds { get; set; }

    }
}
