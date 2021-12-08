using NNPEFWEB.Models;
using NNPEFWEB.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{
    public interface ISystemsInfoService
    {
        IEnumerable<ef_systeminfo> GetAllSystemsInfo();

        ef_systeminfo GetSysteminfo();
        ef_systeminfo GetSystemsInfo(int id);

        string CreateSystemsInfo(systeminfoVM ef_Systeminfo);

        string UpdateSystemsInfo(ef_systeminfo ef_Systeminfo);
        string DeleteSystemsInfo(int id);
        void UpdateFormNumber(int formnumber, int payclass);
    }
}
