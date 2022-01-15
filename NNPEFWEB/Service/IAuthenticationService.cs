﻿
using NNPEFWEB.Models;
using NNPEFWEB.ViewModel;
using System.Threading.Tasks;

namespace NNPEFWEB.Services
{
    public interface IAuthenticationService
    {
        Task<ResponseModel<User>> SignInUserAsync(string email, string password, string client);

        Task SignOutUserAsync();
    }
}