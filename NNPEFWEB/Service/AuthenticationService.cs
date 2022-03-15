
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using NNPEFWEB.Extention;
using NNPEFWEB.Models;
using NNPEFWEB.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace NNPEFWEB.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly string connectionString;

        public AuthenticationService(IConfiguration configuration,SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<ResponseModel<User>> SignInUserAsync(string UserName, string password, string client)
        {
            var user = await userManager.FindByNameAsync(UserName);
            //var rol = await UserRoles.FindByNameAsync(UserName);

            if (user == null)
            {
                return user.ToResponse("Account Not Found");
            }

            SignInResult result = null;
            if(client == "true")
            {
                result = await signInManager.CheckPasswordSignInAsync(user, password, false);
            }
            else
            {
                result = await signInManager.PasswordSignInAsync(user, password, true, false);
            }
                   
            if (result.Succeeded)
            {
                return user.ToResponse(success : true);
            }
            return user.ToResponse("Password incorrect or account inactive");
        }
        public async Task<User> FindUser(string Username)
        {
            return await userManager.FindByNameAsync(Username);
        }

        public async Task<User> FindUserByEmail(string Email)
        {
            return await userManager.FindByEmailAsync(Email);
        }

        public async Task<bool> IsUserByEmailConfirmed(User user)
        {
            return await userManager.IsEmailConfirmedAsync(user);
        }

        public async Task<string> GeneratePasswordTokenAsync(User user)
        {
            return await userManager.GeneratePasswordResetTokenAsync(user);
        }
        public void updateUserlogins(User values)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {

                    using (SqlCommand cmd = new SqlCommand("ResetUserPassword", sqlcon))
                    {
                        cmd.CommandTimeout = 1200;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@username", values.UserName));
                        cmd.Parameters.Add(new SqlParameter("@password", values.PasswordHash));
                        cmd.Parameters.Add(new SqlParameter("@expiredate", values.UpdatedOn));

                        sqlcon.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)

            {

            }
        }
        public async Task SignOutUserAsync()
        {
            await signInManager.SignOutAsync();
        }
    }

}
