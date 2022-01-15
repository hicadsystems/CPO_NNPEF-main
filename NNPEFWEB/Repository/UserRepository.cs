

using Microsoft.EntityFrameworkCore;
using NNPEFWEB.Data;
using NNPEFWEB.Models;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NNPEFWEB.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<User> User(Expression<Func<User, bool>> predicate)
        {
           var reu = await context.Users
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .FirstOrDefaultAsync(predicate);
            return reu;
        }

        public async Task<User> UserWithRolesandMenus(Expression<Func<User, bool>> predicate)
        {
            try
            {

            return await context.Users
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .ThenInclude(x => x.RoleMenus)
                .ThenInclude(x => x.Menu)
                .ThenInclude(x => x.MenuGroup)
                .FirstOrDefaultAsync(predicate);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<User> UserWithRoles(Expression<Func<User, bool>> predicate)
        {

            try
            {
                var pp = await context.Users
                            .Include(x => x.UserRoles)
                            .ThenInclude(x => x.Role).FirstOrDefaultAsync(predicate);
                return pp;
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

      
    }
}
