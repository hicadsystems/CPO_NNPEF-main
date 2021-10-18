using NNPEFWEB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Repository
{
    public interface IUnitOfWorks
    {
        IPersonLoginRepository PersonLogin { get; }
        IPersonInfoRepository Personinfo { get; }
        ISystemsInfoRepository SystemsInfo { get; }
        Task<bool> Done();
    }
}
