using NNPEFWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Repository
{
    public interface ISystemsInfoRepository: IGenericRepository<ef_systeminfo>
    {
        ef_systeminfo GetSysteminfo();
    }
}
