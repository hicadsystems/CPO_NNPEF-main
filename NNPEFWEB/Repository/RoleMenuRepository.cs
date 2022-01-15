

using NNPEFWEB.Data;
using NNPEFWEB.Models;

namespace NNPEFWEB.Repository
{
    public class RoleMenuRepository : GenericRepository<RoleMenu>, IRoleMenuRepository
    {
        private readonly ApplicationDbContext context;

        public RoleMenuRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
