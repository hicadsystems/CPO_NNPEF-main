using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{
    public interface IDashboard
    {
        int AllStaff();
        int ApprovedStaff();
        int AwaiteApprovalStaff();
        int YetToFillStaff();
        int AllCommandStaff();

        int AllStaffOfficers();
        int ApprovedStaffOfficers();
        int AwaiteApprovalStaffOfficers();
        int YetToFillStaffOfficers();
        int AllCommandStaffOfficers();

        int AllStaffRatings();
         int ApprovedStaffRatings();
        int AwaiteApprovalStaffRatings();
        int YetToFillStaffRatings();
        int AllCommandStaffRatings();

        int AllStaffTrainings();
        int ApprovedStaffTrainings();
        int AwaiteApprovalStaffTrainings();
        int YetToFillStaffTrainings();
        int AllCommandStaffTrainings();




    }
}
