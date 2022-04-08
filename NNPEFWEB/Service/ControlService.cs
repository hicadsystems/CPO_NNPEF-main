using NNPEFWEB.Data;
using NNPEFWEB.Models;
using NNPEFWEB.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{
    
    public class ControlService: IControlService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWorks _unitofworks;
        public ControlService(ApplicationDbContext context, IUnitOfWorks unitofworks)
        {
            _context = context;
            _unitofworks = unitofworks;
        }

        public IEnumerable<ef_control> getcontrol()
        {
            return _context.ef_control.ToList();
        }
        public IEnumerable<ef_control> getcontrolbyid(int id)
        {
            return _context.ef_control.Where(x=>x.Id==id);
        }
        public ef_control getcontrolbyship(string ship)
        {
            return _context.ef_control.Where(x=>x.ship==ship).FirstOrDefault();
        }
        public void Addcontrol(ef_control model)
        {
            _context.Add(model);
            _unitofworks.Done();
                
        }
        public void Updatecontrol(ef_control model)
        {
            _context.Update(model);
            _unitofworks.Done();

        }
        public void Deletcontrol(int id)
        {
           var con= _context.ef_control.Find(id);
            _context.Remove(con);
            _unitofworks.Done();

        }

    }
}
