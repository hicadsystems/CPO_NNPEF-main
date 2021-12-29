using NNPEFWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NNPEFWEB.Repository
{
    public interface IPersonLoginRepository:IGenericRepository<ef_personnelLogin>
    {
        ef_personnelLogin GetPersonBySVC_No(Expression<Func<ef_personnelLogin, bool>> predicate);
        ef_personnelLogin GetPersonnelLogin(string username, string password);
        Task<ef_personnelLogin> GetPersonnel(string svcno);
        Task<ef_personnelLogin> GetPersonnelBypassword(string password, string per);
        Task<List<ef_personnelLogin>> GetAllPersonnel();
        Task<ef_personnelLogin> GetPersonnelByMail(string email);
        Task<ef_shiplogin> GetUserByShip(string svcno);
    }
}
