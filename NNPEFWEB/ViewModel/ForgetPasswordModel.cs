using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.ViewModel
{
    public class ForgetPasswordModel
    {
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class PasswordCode
    {
        public string Code { get; set; }
      
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        public string ResetCode { get; set; }

    }
}
