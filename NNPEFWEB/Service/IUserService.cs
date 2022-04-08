
using Microsoft.AspNetCore.Identity;
using NNPEFWEB.Models;
using NNPEFWEB.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{
    public interface IUserService
    {
        Task<ResponseModel<User>> CreateUser(User user, IEnumerable<int> roles, string password, int? device);
        Task<User> GetUserWithRoles(int id);
        Task<User> GetUserByResetCode(string resetcode);
        IEnumerable<User> GetUsers();
        Task<User> GetUserByUserName(string username);
        Task<User> GetUserWithRolesAndMenus(int id);
        Task<User> GetUserRolesDevices(int id);
        Task<IdentityResult> UpdateUserInfo(User user, IEnumerable<int> roles,  int? device);
        Task<IdentityResult> UpdateUser(User user);
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
        void DeleteUser(int id);
    }
}
