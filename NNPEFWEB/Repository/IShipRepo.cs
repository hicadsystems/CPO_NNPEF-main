using NNPEFWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NNPEFWEB.Repository
{
    public interface IShipRepo : IGenericRepository<ef_shiplogin>
    {
        ef_shiplogin GetShips(Expression<Func<ef_shiplogin, bool>> predicate);
        ef_shiplogin GetPersonnelLogin(string username, string password);
        Task<ef_shiplogin> GetPersonnel(string svcno);
        Task<ef_shiplogin> GetPersonnelBypassword(string password, string per);
        Task<List<ef_shiplogin>> GetAllPersonnel();
        Task<ef_shiplogin> GetPersonnelByMail(string email);
        Task<ef_shiplogin> GetUserByShip(string svcno);
    }
}
