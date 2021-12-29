using NNPEFWEB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Repository
{
    public class UnitOfWorks:IUnitOfWorks
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWorks(ApplicationDbContext context)
        {
            this._context = context;

            PersonLogin = new PersonLoginRepository(_context);
            Personinfo = new PersonInfoRepository(_context);
            SystemsInfo = new SystemsInfoRepository(_context);
            shiplogin = new ShipRepository(_context);
        }
        public IPersonLoginRepository PersonLogin { get; }

        public IPersonInfoRepository Personinfo { get; }
        public IShipRepo shiplogin { get; }

        public ISystemsInfoRepository SystemsInfo { get; }


        public async Task<bool> Done()
        {
            return await _context.Instance.SaveChangesAsync() > 0;
        }
    }
}
