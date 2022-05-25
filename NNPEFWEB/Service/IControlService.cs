using NNPEFWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{
   public interface IControlService
    {
        IEnumerable<ef_control> getcontrolbyid(int id);
        IEnumerable<ef_control> getcontrol();
        ef_control getcontrolbyship(string ship);
        void Addcontrol(ef_control model);
        void Updatecontrol(ef_control model);
        void Deletcontrol(int id);
    }
}
