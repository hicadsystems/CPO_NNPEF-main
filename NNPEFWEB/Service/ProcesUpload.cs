using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using NNPEFWEB.Models;
using NNPEFWEB.Repository;
using NNPEFWEB.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{

    public class ProcesUpload
    {
        private readonly List<personLoginVM> personLoginVMs;
         //private readonly List<UsersUpload> userVMs;
        //private readonly IPersonService personService;
        //private readonly ILoanTypeService loanTypeService;
        private readonly string connectionstring;
        private readonly IUnitOfWorks unitOfWork;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private string user;
        public ProcesUpload(
            string _connectionstring, List<personLoginVM> personLoginVMs, 
             IUnitOfWorks unitOfWork, string user,
              UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            this.personLoginVMs = personLoginVMs;
            //this.userVMs = userVMs;
            this.user = user;
            this.unitOfWork = unitOfWork;
            connectionstring = _connectionstring;
            _userManager = userManager;
            _signInManager = signInManager;

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
        public async Task processUploadInThread2()
        {
            try
            {
                foreach (var s in personLoginVMs)
                {

                    using (SqlConnection sqlcon = new SqlConnection(connectionstring))
                    {
                        using (SqlCommand cmd = new SqlCommand("UploadShipUsers", sqlcon))
                        {
                            cmd.CommandTimeout = 1200;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@rank", s.rank));
                            cmd.Parameters.Add(new SqlParameter("@userName", s.svcNo));
                            cmd.Parameters.Add(new SqlParameter("@password", s.svcNo));
                            cmd.Parameters.Add(new SqlParameter("@surName", s.surName));
                            cmd.Parameters.Add(new SqlParameter("@othername", s.otheName));
                            cmd.Parameters.Add(new SqlParameter("@phoneNumber", s.phoneNumber));
                            cmd.Parameters.Add(new SqlParameter("@email", s.email));
                            cmd.Parameters.Add(new SqlParameter("@appointment", s.appointment));
                            cmd.Parameters.Add(new SqlParameter("@department", s.department));
                            cmd.Parameters.Add(new SqlParameter("@ship", s.ship));


                            sqlcon.Open();
                            cmd.ExecuteNonQuery();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }
}
}
