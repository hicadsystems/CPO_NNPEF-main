using NNPEFWEB.Models;
using NNPEFWEB.Repository;
using NNPEFWEB.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{
    public class PersonInfoService:IPersonInfoService
    {
        private readonly IUnitOfWorks unitOfWork;
        public PersonInfoService(IUnitOfWorks unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<List<PersonalInfoModel>> getPersonList(int iDisplayStart, int iDisplayLength, string payclass, string ship)
        {
            return await unitOfWork.Personinfo.getPersonList(iDisplayStart, iDisplayLength, payclass, ship);
        }
        public async Task<int> getPersonListCount(string payclass, string ship)
        {
            return await unitOfWork.Personinfo.getPersonListCount(payclass, ship);
        }
        public async Task<List<PersonalInfoModel>> FilterBySearch(string svcno)
        {
            return await unitOfWork.Personinfo.FilterBySearch(svcno);
        }
        public async Task<bool> AddPersonalInfo(ef_personalInfo value)
        {
            unitOfWork.Personinfo.Update(value);
            return await unitOfWork.Done();
        }

        public IEnumerable<ef_personalInfo> downloadPersonalReport(string svcno)
        {
            return unitOfWork.Personinfo.downloadPersonalReport(svcno);
        }
        public ef_personalInfo GetPersonBySVC_No(string svcno)
        {
            return unitOfWork.Personinfo.GetPersonBySVC_No(x=>x.serviceNumber==svcno);
        }
        public ef_personalInfo GetOnePersonnel(string svcno)
        {
            return unitOfWork.Personinfo.GetOnePersonnel(svcno);
        }
        public Task<ef_personalInfo> GetPersonalInfo(string username)
        {
            return unitOfWork.Personinfo.GetPersonalInfo(username);
        }
        
        public ef_personalInfo GetPersonalInfoByClass(string payclass)
        {
            throw new NotImplementedException();
        }

        public ef_personalInfo GetPersonalInfoByRank(string svcno, string rank)
        {
            throw new NotImplementedException();
        }

        public ef_personalInfo GetPersonalInfoByShip(string ship)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PersonalInfoModel> GetPersonalReport(string username)
        {
            return unitOfWork.Personinfo.GetPersonalReport(username);
        }

        public IEnumerable<ef_personalInfo> GetPersonnelByCommand(string payclass)
        {
            return unitOfWork.Personinfo.GetPersonnelByCommand(payclass);
        }
   
        public List<ef_personalInfo> PEFReport(ApiSearchModel apiSearchModel)
        {
            return unitOfWork.Personinfo.GetPEFReport(apiSearchModel);
        }
        public List<ef_personalInfo> GetPEFReport()
        {
            return unitOfWork.Personinfo.GetPEFReport2();
        }

        public IEnumerable<ef_personalInfo> GetUpdatedPersonnel(string payclass, string ship)
        {
            return unitOfWork.Personinfo.GetUpdatedPersonnel(payclass,ship);
        }
        public IEnumerable<ef_personalInfo> GetUpdatedPersonnel3(string payclass, string ship)
        {
            return unitOfWork.Personinfo.GetUpdatedPersonnel3(payclass, ship);
        }
        public IEnumerable<ef_personalInfo> GetUpdatedPersonnel2()
        {
            return unitOfWork.Personinfo.GetUpdatedPersonnel2();
        }

        public IEnumerable<ef_personalInfo> GetPersonnelStatusReport(string status)
        {
            return unitOfWork.Personinfo.GetPersonnelStatusRepo(status);
        }
        public IEnumerable<ef_personalInfo> GetUpdatedPersonnelBySVCNO(string payclass, string ship,string svcno)
        {
            return unitOfWork.Personinfo.GetUpdatedPersonnelBySVCNO(payclass, ship,svcno);
        }
        public IEnumerable<ef_personalInfo> GetUpdatedPersonnelBySVCNO2(string payclass,string svcno)
        {
            return unitOfWork.Personinfo.GetUpdatedPersonnelBySVCNO2(payclass, svcno);
        }
        public IEnumerable<ef_personalInfo> GetUpdatedPersonnelByCpo(string payclass,string ship)
        {
            return unitOfWork.Personinfo.GetUpdatedPersonnelByCPO(payclass,ship);
        }
        public Task<List<PersonalInfoModel>> getPersonList(int iDisplayStart, int iDisplayLength)
        {
            return unitOfWork.Personinfo.getPersonList(iDisplayStart, iDisplayLength);
        }
        public Task<int> getPersonListCount()
        {
            return unitOfWork.Personinfo.getPersonListCount();
        }
    }
}
