using NNPEFWEB.Models;
using NNPEFWEB.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{
    public interface IPersonInfoService
    {
        IEnumerable<ef_personalInfoHist> GetPersonHist(string year, string svcno);
        IEnumerable<ef_personalInfo> GetPersonnelShipReportrepo(string status, string payclass, string ship);
        Task<PaginatedList<ef_personalInfo>> GetPersonnelShipReport(string status, string payclass, string ship, int? pageNumber);
        Task<List<PersonalInfoModel>> getPersonList(int iDisplayStart, int iDisplayLength, string payclass, string ship);
        Task<int> getPersonListCount(string payclass, string ship);
        Task<List<PersonalInfoModel>> FilterBySearch(string svcno);
        ef_personalInfo GetPersonBySVC_No(string svcno);
        Task<ef_personalInfo> GetPersonalInfo(string username);
        ef_personalInfo GetOnePersonnel(string svcno);
        IEnumerable<PersonalInfoModel> GetPersonalReport(string username);
        ef_personalInfo GetPersonalInfoByRank(string svcno, string rank);
        ef_personalInfo GetPersonalInfoByClass(string payclass);
        ef_personalInfo GetPersonalInfoByShip(string ship);
        IEnumerable<ef_personalInfo> GetUpdatedPersonnel2();
        Task<PaginatedList<ef_personalInfo>> GetPersonnelStatusReport(string status, string shipToSearch,string payclass, int? pageNumber);
        Task<PaginatedList<ef_personalInfo>> GetUpdatedPersonnelRepo2(int? pageNumber);
        Task<PaginatedList<ef_personalInfo>> GetUpdatedPersonnelBySHip(string payclass, string ship, int? pageNumber);
        IEnumerable<ef_personalInfo> GetPersonnelStatusReportrepo(string status, string shipToSearch);
        
       IEnumerable<ef_personalInfo> GetPersonnelStatusRepoReport(string status, string shipToSearch,string payclass);
        IEnumerable<ef_personalInfo> GetPersonnelByCommand(string payclass);
        IEnumerable<ef_personalInfo> GetUpdatedPersonnel(string payclass, string ship);
        IEnumerable<ef_personalInfo> GetUpdatedPersonnel3(string payclass, string ship);
        IEnumerable<ef_personalInfo> GetUpdatedPersonnelBySVCNO(string payclass, string ship,string svcno);
        IEnumerable<ef_personalInfo> GetUpdatedPersonnelBySVCNO2(string payclass, string svcno);
        Task<bool> AddPersonalInfo(ef_personalInfo value);
        List<ef_personalInfo> PEFReport(ApiSearchModel apiSearchModel);
        IEnumerable<ef_personalInfo> downloadPersonalReport(string svcno);
        List<ef_personalInfo> GetPEFReport();
        Task<List<PersonalInfoModel>> getPersonList(int iDisplayStart, int iDisplayLength);
        Task<int> getPersonListCount();
        IEnumerable<ef_personalInfo> GetUpdatedPersonnelByCpo(string payclass, string ship);
        Task<PaginatedList<ef_personalInfo>> GetUpdatedPersonnelByHOD(string payclass, string status, string ship, int? pageNumber);
        Task<List<ef_personalInfo>> GetUpdatedPersonnelByHOD2(string payclass, string status, string ship);
    }
}
