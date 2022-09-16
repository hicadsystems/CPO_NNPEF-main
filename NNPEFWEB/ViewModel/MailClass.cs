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
        public string to { get; set; }
        public string from { get; set; }
        public string fromName { get; set; }
        public string subject { get; set; }
        public string bodyText { get; set; }
        public string message { get; set; }


    }
}
