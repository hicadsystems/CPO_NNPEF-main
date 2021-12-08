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
        Task<List<PersonalInfoModel>> getPersonList(int iDisplayStart, int iDisplayLength, string payclass, string ship);
        Task<int> getPersonListCount(string payclass, string ship);
        Task<List<PersonalInfoModel>> FilterBySearch(string svcno);
        IEnumerable<ef_personalInfo> GetUpdatedPersonnelBySVCNO(string appointm, string payclass, string ship, string svcno);
        ef_personalInfo GetPersonBySVC_No(Expression<Func<ef_personalInfo, bool>> predicate);
        Task<ef_personalInfo> GetPersonalInfo(string svcno);
        IEnumerable<PersonalInfoModel> GetPersonalReport(string svcno);
        ef_personalInfo GetPersonalInfoByRank(string svcno,string rank);
        ef_personalInfo GetPersonalInfoByClass(string payclass);
        ef_personalInfo GetPersonalInfoByShip(string ship);
        IEnumerable<ef_personalInfo> GetPersonnelByCommand(string command, string appointm,string payclass);
        IEnumerable<ef_personalInfo> GetUpdatedPersonnel(string appointm, string payclass, string ship);
        List<ef_personalInfo> GetPEFReport(ApiSearchModel apiSearchModel);
        IEnumerable<ef_personalInfo> downloadPersonalReport(string svcno);
        List<ef_personalInfo> GetPEFReport2();
        Task<List<PersonalInfoModel>> getPersonList(int iDisplayStart, int iDisplayLength);
        Task<int> getPersonListCount();
        IEnumerable<ef_personalInfo> GetUpdatedPersonnelByCPO(string payclass);
        ef_personalInfo GetOnePersonnel(string svcno);

    }
}
