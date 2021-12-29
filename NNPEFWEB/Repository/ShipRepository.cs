using NNPEFWEB.Data;
using NNPEFWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NNPEFWEB.Repository
{
    public class ShipRepository : GenericRepository<ef_shiplogin>, IShipRepo
    {
        private readonly ApplicationDbContext _context;
        public ShipRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }
        public ef_shiplogin GetShips(Expression<Func<ef_shiplogin, bool>> predicate)
        {
            return _context.ef_shiplogins.FirstOrDefault(predicate);
        }
        public async Task<ef_shiplogin> GetPersonnel(string svcno)
        {
            var dd = _context.ef_shiplogins.FirstOrDefault(x => x.userName == svcno);
            return dd;

        }
        public async Task<ef_shiplogin> GetUserByShip(string svcno)
        {
            var dd = _context.ef_shiplogins.FirstOrDefault(x => x.userName == svcno);
            return dd;

        }
        public async Task<ef_shiplogin> GetPersonnelByMail(string email)
        {
            return _context.ef_shiplogins.FirstOrDefault(x => x.email == email);

        }
        public async Task<List<ef_shiplogin>> GetAllPersonnel()
        {
            return _context.ef_shiplogins.ToList();

        }
        public async Task<ef_shiplogin> GetPersonnelBypassword(string password, string per)
        {

            var pp = _context.ef_shiplogins.FirstOrDefault(x => x.password == password && x.userName == per);
            return pp;

        }

        public ef_shiplogin GetPersonnelLogin(string username, string password)
        {
            return _context.ef_shiplogins.FirstOrDefault(x => x.userName.Equals(username.Trim()) && x.password.Equals(password));
        }

    }
}

