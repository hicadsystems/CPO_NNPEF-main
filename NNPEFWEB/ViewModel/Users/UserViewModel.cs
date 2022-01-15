using NNPEFWEB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace NNPEFWEB.ViewModels.Users
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Lastname { get; set; }

        public bool IsActive { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public string Appointment { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Passwords must be the same")]
        public string ConfirmPassword { get; set; }

        [Required]
        public int[] Rolesid { get; set; }

        public int? Device { get; set; }
        public string Rank { get; set; }

        public DateTime UpdatedOn { get; set; }
        public List<User> Users { get; set; }

        public IEnumerable<Role> Roles { get; set; }


    }
}
