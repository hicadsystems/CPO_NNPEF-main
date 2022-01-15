

using NNPEFWEB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{
    public interface IMenuService
    {
        IEnumerable<Menu> GetActiveMenus();
        IEnumerable<Menu> GetMenus();
        Task<Menu> GetMenuById(int id);
        Task<bool> AddMenu(Menu menu);
        Task<bool> UpdateMenu(Menu menu);
        Menu GetMenuByCode(string code);
    }
}
