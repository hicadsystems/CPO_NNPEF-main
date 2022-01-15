
using NNPEFWEB.Models;
using System.Collections.Generic;

namespace NNPEFWEB.ViewModels.Roles
{
    public class RolesViewModel
    {
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<Menu> Menus { get; set; }
    }
}
