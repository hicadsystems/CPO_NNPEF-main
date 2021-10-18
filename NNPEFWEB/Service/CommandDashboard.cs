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

        
      
        public int AllCommandStaffOfficers(string ship, string command)
        {
            return _context.ef_personalInfos.Where(x => x.payrollclass == "1" && x.ship == ship && x.command == command).Count();
        }

        public int AllStaffOfficers(string ship, string command)
        {
           var pp= _context.ef_personalInfos.Where(x => x.payrollclass == "1" && x.ship == ship && x.command == command).Count();
            return pp;    
       }

        
        public int ApprovedStaffOfficers(string ship, string command)
        {
            return _context.ef_personalInfos.Where(x => x.Status == "CDR" && x.payrollclass == "1" && x.ship == ship && x.command == command).Count();
        }

       
        public int AwaiteApprovalStaffOfficers(string ship, string command)
        {
            return _context.ef_personalInfos.Where(x => x.Status == "HOD" || x.Status == "DO" && x.payrollclass == "1" && x.ship == ship && x.command == command).Count();
        }

        
        public int YetToFillStaffOfficers(string ship, string command)
        {
            return _context.ef_personalInfos.Where(x => x.Status == null && x.payrollclass == "1" && x.ship == ship && x.command == command).Count();
        }
        
        public int AllCommandStaffRatings(string ship, string command)
        {
            return _context.ef_personalInfos.Where(x => x.payrollclass == "2" && x.ship == ship && x.command == command).Count();
        }

       

        public int AllStaffRatings(string ship, string command)
        {
            return _context.ef_personalInfos.Where(x => x.payrollclass == "2" && x.ship == ship && x.command == command).Count();
        }
       
        public int ApprovedStaffRatings(string ship, string command)
        {
            return _context.ef_personalInfos.Where(x => x.Status == "CDR" && x.payrollclass == "2" && x.ship == ship && x.command == command).Count();
        }

        

        public int AwaiteApprovalStaffRatings(string ship, string command)
        {
            return _context.ef_personalInfos.Where(x => x.Status == "HOD" || x.Status == "DO" && x.payrollclass == "2" && x.ship == ship && x.command == command).Count();
        }

        

        public int YetToFillStaffRatings(string ship, string command)
        {
            return _context.ef_personalInfos.Where(x => x.Status == null && x.payrollclass == "2" && x.ship == ship && x.command == command).Count();
        }
        

        public int AllCommandStaffTrainings(string ship, string command)
        {
            return _context.ef_personalInfos.Where(x => x.payrollclass == "3" && x.ship == ship && x.command == command).Count();
        }

        
        public int AllStaffTrainings(string ship, string command)
        {
            return _context.ef_personalInfos.Where(x => x.payrollclass == "3" && x.ship == ship && x.command == command).Count();
        }

        public int ApprovedStaffTrainings(string ship, string command)
        {
            return _context.ef_personalInfos.Where(x => x.Status == "CDR" && x.payrollclass == "3" && x.ship == ship && x.command == command).Count();
        }

        
        public int AwaiteApprovalStaffTrainings(string ship, string command)
        {
            return _context.ef_personalInfos.Where(x => x.Status == "HOD" || x.Status == "DO" && x.payrollclass == "2" && x.ship == ship && x.command == command).Count();
        }

        
        public int YetToFillStaffTrainings(string ship, string command)
        {
            return _context.ef_personalInfos.Where(x => x.Status == null && x.payrollclass == "3" && x.ship == ship && x.command == command).Count();
        }
    }
}
