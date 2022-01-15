

using NNPEFWEB.Data;
using NNPEFWEB.Models;

namespace NNPEFWEB.Repository
{
    public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
    {
        private readonly ApplicationDbContext context;

        public UserRoleRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
