using NNPEFWEB.Models;
using NNPEFWEB.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NNPEFWEB.Repository
{
    public interface IPersonInfoRepository:IGenericRepository<ef_personalInfo>
    {
        Task<PaginatedList<ef_personalInfo>> GetPersonnelDashBoardOfficer(string payclass, string ship, int? pageNumber);
        Task<List<ef_personalInfo>> GetUpdatedPersonnelByHOD2(string payclass, string status, string ship);
        Task<PaginatedList<ef_personalInfo>> GetUpdatedPersonnelByHOD(string payclass, string status, string ship, int? pageNumber);
        IEnumerable<ef_personalInfo> GetPersonnelShipReportrepo(string statusToSearch, string payclass, string ship);
        Task<PaginatedList<ef_personalInfo>> GetPersonnelShipReport(string statusToSearch, string payclass, string ship, int? pageNumber);
        Task<List<PersonalInfoModel>> getPersonList(int iDisplayStart, int iDisplayLength, string payclass, string ship);
        Task<int> getPersonListCount(string payclass, string ship);
        Task<List<PersonalInfoModel>> FilterBySearch(string svcno);
        IEnumerable<ef_personalInfo> GetUpdatedPersonnelBySVCNO(string payclass, string ship, string svcno);
        IEnumerable<ef_personalInfo> GetUpdatedPersonnelBySVCNO2(string payclass, string svcno);
        IEnumerable<ef_personalInfo> GetUpdatedPersonnel2();
        Task<PaginatedList<ef_personalInfo>> GetUpdatedPersonnelBySHip(string payclass, string ship, int? pageNumber);
        ef_personalInfo GetPersonBySVC_No(Expression<Func<ef_personalInfo, bool>> predicate);
        Task<ef_personalInfo> GetPersonalInfo(string svcno);
        IEnumerable<PersonalInfoModel> GetPersonalReport(string svcno);
        ef_personalInfo GetPersonalInfoByRank(string svcno,string rank);
        ef_personalInfo GetPersonalInfoByClass(string payclass);
        ef_personalInfo GetPersonalInfoByShip(string ship);
        IEnumerable<ef_personalInfo> GetPersonnelByCommand(string payclass);
        IEnumerable<ef_personalInfo> GetUpdatedPersonnel(string payclass, string ship);
        IEnumerable<ef_personalInfo> GetPersonnelStatusReportrepo(string statusToSearch, string shipToSearch);
        Task<PaginatedList<ef_personalInfo>> GetPersonnelStatusRepo(string searchCriteria, string shipToSearch, int? pageNumber);
        Task<PaginatedList<ef_personalInfo>> GetUpdatedPersonnelRepo2(int? pageNumber);
        IEnumerable<ef_personalInfo> GetUpdatedPersonnel3(string payclass, string ship);
        List<ef_personalInfo> GetPEFReport(ApiSearchModel apiSearchModel);
        IEnumerable<ef_personalInfo> downloadPersonalReport(string svcno);
        List<ef_personalInfo> GetPEFReport2();
        Task<List<PersonalInfoModel>> getPersonList(int iDisplayStart, int iDisplayLength);
        Task<int> getPersonListCount();
        IEnumerable<ef_personalInfo> GetUpdatedPersonnelByCPO(string payclass,string ship);
        ef_personalInfo GetOnePersonnel(string svcno);

    }
}
