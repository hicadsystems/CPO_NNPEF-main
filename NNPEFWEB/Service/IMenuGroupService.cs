using NNPEFWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{
    public interface IMenuGroupService
    {
        IEnumerable<MenuGroup> GetActiveMenuGroups();
        IEnumerable<MenuGroup> GetMenuGroupss();
    }
}
