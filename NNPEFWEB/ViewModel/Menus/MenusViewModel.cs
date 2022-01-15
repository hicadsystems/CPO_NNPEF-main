
using NNPEFWEB.Models;
using System.Collections.Generic;

namespace NNPEFWEB.ViewModels.Menus
{
    public class MenusViewModel
    {
        public IEnumerable<Menu> Menus { get; set; }
        public IEnumerable<MenuGroup> MenuGroups { get; set; }
    }
}
