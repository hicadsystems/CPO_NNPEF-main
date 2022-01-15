

using NNPEFWEB.Models;
using NNPEFWEB.Repository;
using System.Collections.Generic;

namespace NNPEFWEB.Service
{
    public class MenuGroupService : IMenuGroupService
    {
        private readonly IUnitOfWorks unitOfWork;
        public MenuGroupService(IUnitOfWorks unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<MenuGroup> GetMenuGroupss()
        {
            return unitOfWork.MenuGroups.All();
        }

        public IEnumerable<MenuGroup> GetActiveMenuGroups()
        {
            return null;
        }
    }
}
