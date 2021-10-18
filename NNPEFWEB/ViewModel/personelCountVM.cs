using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.ViewModel
{
    public class personelCountVM
    {
        public int AllStaff { get; set; } = 0;
        public int CompletedStaff { get; set; } = 0;
        public int AwaitingApprovalStaff { get; set; } = 0;
        public int ApproveStaff { get; set; } = 0;
        public int YettoFillStaff { get; set; } = 0;
        public int CommandYetToFill { get; set; } = 0;

        public int AllStaffOfficers { get; set; } = 0;
        public int CompletedStaffOfficers { get; set; } = 0;
        public int AwaitingApprovalStaffOfficers { get; set; } = 0;
        public int ApproveStaffOfficers { get; set; } = 0;
        public int YettoFillStaffOfficers { get; set; } = 0;
        public int CommandYetToFillOfficers { get; set; } = 0;

        public int AllStaffTrainings { get; set; } = 0;
        public int CompletedStaffTrainings { get; set; } = 0;
        public int AwaitingApprovalStaffTrainings { get; set; } = 0;
        public int ApproveStaffTrainings { get; set; } = 0;
        public int YettoFillStaffTrainings { get; set; } = 0;
        public int CommandYetToFillTrainings { get; set; } = 0;

        public int AllStaffRatings { get; set; } = 0;
        public int CompletedStaffRatings { get; set; } = 0;
        public int AwaitingApprovalStaffRatings { get; set; } = 0;
        public int ApproveStaffRatings { get; set; } = 0;
        public int YettoFillStaffRatings { get; set; } = 0;
        public int CommandYetToFillRatings { get; set; } = 0;

    }
}
