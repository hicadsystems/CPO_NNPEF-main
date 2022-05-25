using NNPEFWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{
    public interface IDashboard
    {
        int AllStaff();
        IEnumerable<ef_personalInfo> AllStaffList();
        int ApprovedStaff();
        IEnumerable<ef_personalInfo> ApprovedStaffList();
        int AwaiteApprovalStaff();
        IEnumerable<ef_personalInfo> AwaiteApprovalStaffList();
        int YetToFillStaff();
        IEnumerable<ef_personalInfo> YetToFillStaffList();
        int AllCommandStaff();

        int AllStaffOfficers();
        int ApprovedStaffOfficers();
        int AwaiteApprovalStaffOfficers();
        int YetToFillStaffOfficers();
        int AllCommandStaffOfficers();

        int AllStaffRatings();
        IEnumerable<ef_personalInfo> AllStaffRatingsList();
        int ApprovedStaffRatings();
        IEnumerable<ef_personalInfo> ApprovedStaffRatingsList();
        int AwaiteApprovalStaffRatings();
        IEnumerable<ef_personalInfo> AwaiteApprovalStaffRatingsList();
        int YetToFillStaffRatings();
        IEnumerable<ef_personalInfo> YetToFillStaffRatingsList();
        int AllCommandStaffRatings();

        int AllStaffTrainings();
        IEnumerable<ef_personalInfo> AllStaffTrainingsList();
        int ApprovedStaffTrainings();
        IEnumerable<ef_personalInfo> ApprovedStaffTrainingsList();
        int AwaiteApprovalStaffTrainings();
        IEnumerable<ef_personalInfo> AwaiteApprovalStaffTrainingsList();
        int YetToFillStaffTrainings();
        IEnumerable<ef_personalInfo> YetToFillStaffTrainingsList();
        int AllCommandStaffTrainings();




    }
}
