using Microsoft.EntityFrameworkCore.Internal;
using NNPEFWEB.Data;
using NNPEFWEB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{
    public class CommandDashboard : ICommandDashboard
    {
        private readonly IUnitOfWorks _unitOfWork;
        private readonly ApplicationDbContext _context;
        public CommandDashboard(ApplicationDbContext context, IUnitOfWorks unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        
      
        public int AllCommandStaffOfficers(string ship)
        {
            return _context.ef_personalInfos.Where(x => x.payrollclass == "1" && x.ship == ship ).Count();
        }

        public int AllStaffOfficers(string ship)
        {
           var pp= _context.ef_personalInfos.Where(x => x.payrollclass == "1" && x.ship == ship ).Count();
            return pp;    
       }

        
        public int ApprovedStaffOfficers(string ship)
        {
            return _context.ef_personalInfos.Where(x => x.Status == "CDR" && x.payrollclass == "1" && x.ship == ship ).Count();
        }

       
        public int AwaiteApprovalStaffOfficers(string ship)
        {
            return _context.ef_personalInfos.Where(x => x.Status == "HOD" || x.Status == "DO" && x.payrollclass == "1" && x.ship == ship ).Count();
        }

        
        public int YetToFillStaffOfficers(string ship)
        {
            return _context.ef_personalInfos.Where(x => x.Status == null && x.payrollclass == "1" && x.ship == ship ).Count();
        }
        
        public int AllCommandStaffRatings(string ship)
        {
            return _context.ef_personalInfos.Where(x => x.serviceNumber.Substring(2, 0) != "NN" && x.serviceNumber.Substring(2, 0) != "BT" && x.ship == ship ).Count();
        }

       

        public int AllStaffRatings(string ship)
        {
            return _context.ef_personalInfos.Where(x => x.serviceNumber.Substring(2, 0) != "NN" && x.serviceNumber.Substring(2, 0) != "BT" && x.ship == ship ).Count();
        }
       
        public int ApprovedStaffRatings(string ship)
        {
            return _context.ef_personalInfos.Where(x => x.Status == "CDR" && x.serviceNumber.Substring(2, 0) != "NN" && x.serviceNumber.Substring(2, 0) != "BT" && x.ship == ship ).Count();
        }

        

        public int AwaiteApprovalStaffRatings(string ship)
        {
            return _context.ef_personalInfos.Where(x => x.Status == "HOD" || x.Status == "DO" && x.serviceNumber.Substring(2,0)!="NN" && x.serviceNumber.Substring(2, 0) != "BT" && x.ship == ship ).Count();
        }

        

        public int YetToFillStaffRatings(string ship)
        {
            return _context.ef_personalInfos.Where(x => x.Status == null && x.serviceNumber.Substring(2, 0) != "NN" && x.serviceNumber.Substring(2, 0) != "BT" && x.ship == ship ).Count();
        }
        

        public int AllCommandStaffTrainings(string ship)
        {
            return _context.ef_personalInfos.Where(x => x.payrollclass == "6" && x.ship == ship ).Count();
        }

        
        public int AllStaffTrainings(string ship)
        {
            return _context.ef_personalInfos.Where(x => x.payrollclass == "6" && x.ship == ship ).Count();
        }

        public int ApprovedStaffTrainings(string ship)
        {
            return _context.ef_personalInfos.Where(x => x.Status == "CDR" && x.payrollclass == "6" && x.ship == ship ).Count();
        }

        
        public int AwaiteApprovalStaffTrainings(string ship)
        {
            return _context.ef_personalInfos.Where(x => x.Status == "HOD" || x.Status == "DO" && x.payrollclass == "6" && x.ship == ship ).Count();
        }

        
        public int YetToFillStaffTrainings(string ship)
        {
            return _context.ef_personalInfos.Where(x => x.Status == null && x.payrollclass == "6" && x.ship == ship ).Count();
        }
    }
}
