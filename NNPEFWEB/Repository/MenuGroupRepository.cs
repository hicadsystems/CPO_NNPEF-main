


using NNPEFWEB.Data;
using NNPEFWEB.Models;

namespace NNPEFWEB.Repository
{
    public class MenuGroupRepository : GenericRepository<MenuGroup>, IMenuGroupRepository
    {
        private readonly ApplicationDbContext context;

        public MenuGroupRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
