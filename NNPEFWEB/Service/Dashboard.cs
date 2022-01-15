using Microsoft.EntityFrameworkCore.Internal;
using NNPEFWEB.Data;
using NNPEFWEB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{
    public class Dashboard : IDashboard
    {
        private readonly IUnitOfWorks _unitOfWork;
        private readonly ApplicationDbContext _context;
        public Dashboard(ApplicationDbContext context, IUnitOfWorks unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public int AllCommandStaff()
        {
            return _context.ef_personalInfos.Count();
        }

        public int AllStaff()
        {
            return _context.ef_personalInfos.Count();
        }

        public int ApprovedStaff()
        {
           return _context.ef_personalInfos.Where(x=>x.Status=="CDR").Count();
        }

        public int AwaiteApprovalStaff()
        {
            return _context.ef_personalInfos.Where(x => x.Status == "HOD" || x.Status=="DO").Count();
        }

        public int YetToFillStaff()
        {
            return _context.ef_personalInfos.Where(x => x.Status == null).Count();
        }

        public int AllCommandStaffOfficers()
        {
            return _context.ef_personalInfos.Where(x=>x.payrollclass=="1").Count();
        }

        public int AllStaffOfficers()
        {
            return _context.ef_personalInfos.Where(x => x.classes == 1).Count();
        }

        public int ApprovedStaffOfficers()
        {
            return _context.ef_personalInfos.Where(x => x.Status == "CPO" && x.classes==1).Count();
        }

 
        public int AwaiteApprovalStaffOfficers()
        {
            return _context.ef_personalInfos.Where(x => x.Status == "SHIP"  && x.classes==1).Count();
        }


        public int YetToFillStaffOfficers()
        {
            return _context.ef_personalInfos.Where(x => x.Status == null && x.classes==1).Count();
        }

        public int AllCommandStaffRatings()
        {
            return _context.ef_personalInfos.Where(x => x.payrollclass == "2").Count();
        }

        public int AllStaffRatings()
        {
            return _context.ef_personalInfos.Where(x => x.classes == 2).Count();
        }

        public int ApprovedStaffRatings()
        {
            return _context.ef_personalInfos.Where(x => x.Status == "CPO" && x.classes == 2).Count();
        }


        public int AwaiteApprovalStaffRatings()
        {
            return _context.ef_personalInfos.Where(x => x.Status == "SHIP" && x.classes == 2).Count();
        }

        public int YetToFillStaffRatings()
        {
            return _context.ef_personalInfos.Where(x => x.Status == null && x.classes == 2).Count();
        }

        public int AllCommandStaffTrainings()
        {
            return _context.ef_personalInfos.Where(x => x.classes == 3).Count();
        }

        public int AllStaffTrainings()
        {
            return _context.ef_personalInfos.Where(x => x.classes == 3).Count();
        }

        public int ApprovedStaffTrainings()
        {
            return _context.ef_personalInfos.Where(x => x.Status == "CPO" && x.classes == 3).Count();
        }


        public int AwaiteApprovalStaffTrainings()
        {
            return _context.ef_personalInfos.Where(x => x.Status == "SHIP"  && x.classes == 3).Count();
        }

        public int YetToFillStaffTrainings()
        {
            return _context.ef_personalInfos.Where(x => x.Status == null && x.classes == 3).Count();
        }

    }
}
