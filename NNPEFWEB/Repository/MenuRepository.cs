

using NNPEFWEB.Data;
using NNPEFWEB.Models;

namespace NNPEFWEB.Repository
{
    public class MenuRepository : GenericRepository<Menu>, IMenuRepository
    {
        private readonly ApplicationDbContext context;
        public MenuRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
