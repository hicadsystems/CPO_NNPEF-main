using NNPEFWEB.Models;
using NNPEFWEB.ViewModel;
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
        IEnumerable<ef_personalInfo> ApprovedStaffRatingsList();
        IEnumerable<ef_personalInfo> AwaiteApprovalStaffRatingsList();
        IEnumerable<ef_personalInfo> YetToFillStaffRatingsList();
        int ApprovedStaffRatings();
        int AwaiteApprovalStaffRatings();
        int YetToFillStaffRatings();
        int AllCommandStaffRatings();
        IEnumerable<ef_personalInfo> AllStaffTrainingsList();
        IEnumerable<ef_personalInfo> ApprovedStaffTrainingsList();
        IEnumerable<ef_personalInfo> AwaiteApprovalTrainingsList();
        IEnumerable<ef_personalInfo> YetToFillStaffTrainingsList();
        int AllStaffTrainings();
        int ApprovedStaffTrainings();
        int AwaiteApprovalStaffTrainings();
        int YetToFillStaffTrainings();
        int AllCommandStaffTrainings();
        Task<PaginatedList<ef_personalInfo>> AllStaffList(string payclass, string ship, int? pageNumber);
        Task<PaginatedList<ef_personalInfo>> ApprovedStaffList(string payclass, string ship, int? pageNumber);
        Task<PaginatedList<ef_personalInfo>> AwaiteApprovalStaffList(string payclass, string ship, int? pageNumber);
        Task<PaginatedList<ef_personalInfo>> YetToFillStaffList(string payclass, string ship, int? pageNumber);
        
        Task<PaginatedList<ef_personalInfo>> AllStaffRatingsList(string payclass, string ship, int? pageNumber);
        Task<PaginatedList<ef_personalInfo>> ApprovedStaffRatingsList(string payclass, string ship, int? pageNumber);
        Task<PaginatedList<ef_personalInfo>> AwaiteApprovalStaffRatingsList(string payclass, string ship, int? pageNumber);
        Task<PaginatedList<ef_personalInfo>> YetToFillStaffRatingsList(string payclass, string ship, int? pageNumber);
      
        Task<PaginatedList<ef_personalInfo>> AllStaffTrainingsList(string payclass, string ship, int? pageNumber);
        Task<PaginatedList<ef_personalInfo>> ApprovedStaffTrainingsList(string payclass, string ship, int? pageNumber);
        Task<PaginatedList<ef_personalInfo>> AwaiteApprovalTrainingsList(string payclass, string ship, int? pageNumber);
        Task<PaginatedList<ef_personalInfo>> YetToFillStaffTrainingsList(string payclass, string ship, int? pageNumber);

    }
}
