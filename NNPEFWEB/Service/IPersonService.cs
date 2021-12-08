using NNPEFWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{
    public interface IPersonService
    {
        ef_personnelLogin GetPersonBySvc_NO(string person);
        ef_personnelLogin personnelLogin(string username, string password);
        Task<ef_personnelLogin> GetPersonel(string svcno);
        Task<List<ef_personnelLogin>> GetAllPersonel();
        Task<bool> updatepersonlogin(ef_personnelLogin values);
        Task<ef_personnelLogin> GetPersonelByPassword(string password, string per);
        Task<ef_personnelLogin> GetPersonelByMail(string email);
        void updatepersonlogin2(ef_personnelLogin values);
    }
}
