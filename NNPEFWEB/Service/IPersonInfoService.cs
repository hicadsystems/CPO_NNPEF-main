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
        ef_personalInfo GetPersonBySVC_No(string svcno);
        Task<ef_personalInfo> GetPersonalInfo(string username);
        ef_personalInfo GetOnePersonnel(string svcno);
        IEnumerable<PersonalInfoModel> GetPersonalReport(string username);
        ef_personalInfo GetPersonalInfoByRank(string svcno, string rank);
        ef_personalInfo GetPersonalInfoByClass(string payclass);
        ef_personalInfo GetPersonalInfoByShip(string ship);
        IEnumerable<ef_personalInfo> GetPersonnelByCommand(string command, string appointm, string payclass);
        IEnumerable<ef_personalInfo> GetUpdatedPersonnel(string command, string appointm, string payclass, string ship, string dept);
        Task<bool> AddPersonalInfo(ef_personalInfo value);
        List<ef_personalInfo> PEFReport(ApiSearchModel apiSearchModel);
        IEnumerable<ef_personalInfo> downloadPersonalReport(string svcno);
        List<ef_personalInfo> GetPEFReport();
        Task<List<PersonalInfoModel>> getPersonList(int iDisplayStart, int iDisplayLength);
        Task<int> getPersonListCount();
        IEnumerable<ef_personalInfo> GetUpdatedPersonnelByCpo(string payclass);
    }
}
