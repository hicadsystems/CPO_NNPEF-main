

using System.ComponentModel.DataAnnotations;

namespace NNPEFWEB.ViewModels.Roles
{
    public class RoleViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public int[] Menus { get; set; }

        public bool Active { get; set; }
    }
}
