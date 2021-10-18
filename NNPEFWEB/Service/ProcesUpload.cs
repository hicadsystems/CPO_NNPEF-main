using NNPEFWEB.Models;
using NNPEFWEB.Repository;
using NNPEFWEB.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{
    public class ProcesUpload
    {
        private readonly List<personLoginVM> personLoginVMs;
        //private readonly IPersonService personService;
        //private readonly ILoanTypeService loanTypeService;
        //private readonly ILoanRegisterService loanRegisterService;
        private readonly IUnitOfWorks unitOfWork;
        private string user;
        public ProcesUpload(List<personLoginVM> personLoginVMs, IUnitOfWorks unitOfWork, string user)
        {
            this.personLoginVMs = personLoginVMs;
            this.user = user;
            this.unitOfWork = unitOfWork;
        }

        public async Task processUploadInThread()
        {

            foreach (var s in personLoginVMs)
            {
                var getPersonId = unitOfWork.Personinfo.GetPersonBySVC_No(x => x.serviceNumber == s.svcNo);
                if (getPersonId == null)
                {
                    
                    unitOfWork.PersonLogin.Create(new ef_personnelLogin()
                    {
                        rank = s.rank,
                        svcNo = s.svcNo,
                        userName=s.svcNo,
                        password=s.svcNo,
                        surName = s.surName,
                        otheName = s.otheName,
                        payClass=s.payClass,
                        phoneNumber=s.phoneNumber,
                        email=s.email,
                        dateCreated = System.DateTime.Now,
                    });
                    await unitOfWork.Done();
                }
                
                if (getPersonId == null)
                {

                    unitOfWork.Personinfo.Create(new ef_personalInfo()
                    {
                        Rank = s.rank,
                        serviceNumber = s.svcNo,
                        Surname = s.surName,
                        OtherName = s.otheName,
                        gsm_number = s.phoneNumber,
                        email = s.email,
                        datecreated = System.DateTime.Now,
                    });
                    await unitOfWork.Done();
                }
            }
            
        }
   
    }
}
