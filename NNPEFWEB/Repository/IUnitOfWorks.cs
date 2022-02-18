using NNPEFWEB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Repository
{
    public interface IUnitOfWorks
    {
        IUserRepository Users { get; }
        IUserRoleRepository UserRoles { get; }
        IMenuRepository Menus { get; }
        IRoleMenuRepository RoleMenus { get; }
        IMenuGroupRepository MenuGroups { get; }
        IPersonLoginRepository PersonLogin { get; }
        IPersonInfoRepository Personinfo { get; }
        ISystemsInfoRepository SystemsInfo { get; }
        IShipRepo shiplogin { get; }
        IContactUs ContactUs { get; }
        Task<bool> Done();
    }
}
