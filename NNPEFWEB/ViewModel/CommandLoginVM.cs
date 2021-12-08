using Microsoft.AspNetCore.Mvc.Rendering;
using NNPEFWEB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.ViewModel
{
    public class CommandLoginVM
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public string Class { get; set; }
        public IEnumerable<SelectListItem> commandList { get; set; }
        public IEnumerable<SelectListItem> shiplist { get; set; }
        public string appointment { get; set; }
        public int Command { get; set; }
        public int ship { get; set; }
        public int Id { get; set; }
    }
    public class RegisterVM
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Command")]
        public int Command { get; set; }
        [Required]
        [Display(Name = "Rank")]
        public string Rank { get; set; }

        [Required]
        [Display(Name = "Appointment")]
        public string Appointment { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public List<SelectListItem> RankList { get; set; }
        public List<SelectListItem> CommandList { get; set; }
    }
}
