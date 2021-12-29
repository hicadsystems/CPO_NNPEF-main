using NNPEFWEB.Data;
using NNPEFWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NNPEFWEB.Repository
{
    public class PersonLoginRepository:GenericRepository<ef_personnelLogin>,IPersonLoginRepository
    {
        private readonly ApplicationDbContext _context;
        public PersonLoginRepository(ApplicationDbContext context):base(context)
        {
            this._context = context;
        }
        public ef_personnelLogin GetPersonBySVC_No(Expression<Func<ef_personnelLogin, bool>> predicate)
        {
            return _context.ef_PersonnelLogins.FirstOrDefault(predicate);
        }
        public async Task<ef_personnelLogin> GetPersonnel(string svcno)
        {
           var dd= _context.ef_PersonnelLogins.FirstOrDefault(x => x.svcNo == svcno);
            return dd;

        }
        public async Task<ef_shiplogin> GetUserByShip(string svcno)
        {
            var dd = _context.ef_shiplogins.FirstOrDefault(x => x.userName == svcno);
            return dd;

        }
        public async Task<ef_personnelLogin> GetPersonnelByMail(string email)
        {
            return _context.ef_PersonnelLogins.FirstOrDefault(x => x.email == email);

        }
        public async Task<List<ef_personnelLogin>> GetAllPersonnel()
        {
            return _context.ef_PersonnelLogins.ToList();

        }
        public async Task<ef_personnelLogin> GetPersonnelBypassword(string password,string per)
        {
        
            var pp=    _context.ef_PersonnelLogins.FirstOrDefault(x => x.password == password && x.userName==per);
            return pp;

        }

        public ef_personnelLogin GetPersonnelLogin(string username,string password)
        {
            return _context.ef_PersonnelLogins.FirstOrDefault(x => x.userName.Equals(username.Trim()) && x.password.Equals(password));
        }
       
    }
}
