
using NNPEFWEB.Models;
using NNPEFWEB.ViewModel;
using System.Threading.Tasks;

namespace NNPEFWEB.Services
{
    public interface IAuthenticationService
    {
        Task<ResponseModel<User>> SignInUserAsync(string email, string password, string client);
        Task<User> FindUser(string Username);
        Task<User> FindUserByEmail(string Email);
        Task<bool> IsUserByEmailConfirmed(User user);
        Task<string> GeneratePasswordTokenAsync(User user);
        void updateUserlogins(User values);
        Task SignOutUserAsync();
    }
}
