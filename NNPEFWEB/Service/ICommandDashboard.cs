using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{
    public interface ICommandDashboard
    {
       
        int AllStaffOfficers(string ship);
        int ApprovedStaffOfficers(string ship);
        int AwaiteApprovalStaffOfficers(string ship);
        int YetToFillStaffOfficers(string ship);
        int AllCommandStaffOfficers(string ship);
        int AllStaffRatings(string ship);
        int ApprovedStaffRatings(string ship);
        int AwaiteApprovalStaffRatings(string ship);
        int YetToFillStaffRatings(string ship);
        int AllCommandStaffRatings(string ship);
        int AllStaffTrainings(string ship);
        int ApprovedStaffTrainings(string ship);
        int AwaiteApprovalStaffTrainings(string ship);
        int YetToFillStaffTrainings(string ship);
        int AllCommandStaffTrainings(string ship);




    }
}
