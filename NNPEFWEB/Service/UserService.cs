using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using NNPEFWEB.Models;
using NNPEFWEB.Repository;
using NNPEFWEB.ViewModel;
using NNPEFWEB.Extention;
using NNPEFWEB.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace NNPEFWEB.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;
        private readonly IUnitOfWorks unitOfWork;
        private readonly ApplicationDbContext context;
        private readonly string connectionString;
        public UserService(IConfiguration configuration, UserManager<User> userManager, IUnitOfWorks unitOfWork, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.context = context;
            connectionString = configuration.GetConnectionString("DefaultConnection");
             
        
        }

        public async Task<ResponseModel<User>> CreateUser(User user, IEnumerable<int> roles, string password, int? device)
        {
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {

                try
                {
                    if (roles != null && roles.Count() > 0)
                    {
                        AddRoleAndDevice(user.Id, roles, device);
                        await unitOfWork.Done();
                    }
                    
                }
                catch (Exception)
                {

                    throw;
                }
                return user.ToResponse("successfully created user", true);
            }

            return user.ToResponse(result.Errors.ToString(), false);
            
        }

        private void AddRoleAndDevice(int userId, IEnumerable<int> roles, int? device)
        {
            var userRoles = new List<UserRole>();
            foreach (var role in roles)
            {
                userRoles.Add(new UserRole
                {
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    UserId = userId,
                    RoleId = role,
                });
            }
            unitOfWork.UserRoles.CreateRange(userRoles);
          
        }

        public async Task<IdentityResult> UpdateUser(User user)
        {
            return await userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> UpdateUserInfo(User user, IEnumerable<int> roles, int? device)
        {
            var userRoles = unitOfWork.UserRoles.GetByExpression(x => x.UserId == user.Id);
            try
            {
                
                unitOfWork.UserRoles.RemoveRange(userRoles);
                AddRoleAndDevice(user.Id, roles, device);
                await unitOfWork.Done();
            }
            catch (Exception)
            {

                throw;
            }

           
            return await userManager.UpdateAsync(user);
        }

        public async Task<User> GetUserById(int id)
        {
            return await userManager.FindByIdAsync(id.ToString());
        }

        public async Task<User> GetUserWithRoles(int id)
        {
            return await unitOfWork.Users.UserWithRoles(x => x.Id == id);
        }

        public async Task<User> GetUserWithRolesAndMenus(int id)
        {
            try
            {
                return await unitOfWork.Users.UserWithRolesandMenus(x => x.Id == id);
            }
            catch (Exception ex)
            {

                string message=ex.Message;
            }
            return null;
        }

        public IEnumerable<User> GetUsers()
        {
            return unitOfWork.Users.All();
        }

        
        public async Task<User> GetUserRolesDevices(int id)
        {
            return await unitOfWork.Users.User(x => x.Id == id);
        }

        public async Task<User> GetUserByResetCode(string resetcode)
        {
            return await unitOfWork.Users.User(x => x.ResetPasswordCode == resetcode);
        }

        public async Task<User> GetUserByUserName(string username)
        {
            return await userManager.FindByNameAsync(username);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }
        public void DeleteUser(int id)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {

                    using (SqlCommand cmd = new SqlCommand("DeleteUser", sqlcon))
                    {
                        cmd.CommandTimeout = 1200;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        sqlcon.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)

            {

            }
        }
    }
}
