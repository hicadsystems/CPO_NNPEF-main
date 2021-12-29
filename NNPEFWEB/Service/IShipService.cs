using NNPEFWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{
    public interface IShipService
    {
        ef_shiplogin GetPersonBySvcno(string person);
        ef_shiplogin personnelLogin(string username, string password);
        Task<ef_shiplogin> GetPersonel(string svcno);
        Task<List<ef_shiplogin>> GetAllPersonel();
       // Task<bool> updatepersonlogin(ef_shiplogin values);
        Task<ef_shiplogin> GetPersonelByPassword(string password, string per);
        Task<ef_shiplogin> GetPersonelByMail(string email);
        void updateshiplogin2(ef_shiplogin values);
        Task<ef_shiplogin> GetUserByShip(string person);
        void updateshiploginss(ef_shiplogin values);
    }
}
