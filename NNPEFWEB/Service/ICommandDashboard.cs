using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{
    public interface ICommandDashboard
    {
       
        int AllStaffOfficers(string ship, string command);
        int ApprovedStaffOfficers(string ship, string command);
        int AwaiteApprovalStaffOfficers(string ship, string command);
        int YetToFillStaffOfficers(string ship, string command);
        int AllCommandStaffOfficers(string ship, string command);
        int AllStaffRatings(string ship, string command);
        int ApprovedStaffRatings(string ship, string command);
        int AwaiteApprovalStaffRatings(string ship, string command);
        int YetToFillStaffRatings(string ship, string command);
        int AllCommandStaffRatings(string ship, string coomand);
        int AllStaffTrainings(string ship, string command);
        int ApprovedStaffTrainings(string ship, string command);
        int AwaiteApprovalStaffTrainings(string ship, string command);
        int YetToFillStaffTrainings(string ship, string command);
        int AllCommandStaffTrainings(string ship, string command);




    }
}
