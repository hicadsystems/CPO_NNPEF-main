using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.ViewModels.Menus
{
    public class MenuViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Action { get; set; }

        [Required]
        public string Controller { get; set; }

        public int? MenuGroup { get; set; }
    }
}
