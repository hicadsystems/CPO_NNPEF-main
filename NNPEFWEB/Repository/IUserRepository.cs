using NNPEFWEB.Models;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NNPEFWEB.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> UserWithRoles(Expression<Func<User, bool>> predicate);
        Task<User> UserWithRolesandMenus(Expression<Func<User, bool>> predicate);
        Task<User> User(Expression<Func<User, bool>> predicate);
    }
}
