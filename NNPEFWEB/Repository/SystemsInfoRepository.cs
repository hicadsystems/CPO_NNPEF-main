using NNPEFWEB.Data;
using NNPEFWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Repository
{
    public class SystemsInfoRepository: GenericRepository<ef_systeminfo>, ISystemsInfoRepository
    {
        private readonly ApplicationDbContext _context;

        public SystemsInfoRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

        public ef_systeminfo GetSysteminfo()
        {
            ef_systeminfo ef_Systeminfo = _context.ef_systeminfos.FirstOrDefault();

            return ef_Systeminfo;
        }
    }
}
