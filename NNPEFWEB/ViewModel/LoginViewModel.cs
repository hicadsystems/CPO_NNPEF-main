
using System.ComponentModel.DataAnnotations;

namespace NNPEFWEB.ViewModel
{
    public class LoginViewModel
    {
        
        public string EmailAddress { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string Client { get; set; }

        public string MacAddress { get; set; }
        [Required]
        public int fundtype { get; set; }
    }
}
