using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NNPEFWEB.Controllers;
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

        private string user;
        private readonly ILogger<HomeController> _logger;
        public ProcesUpload(
            ILogger<HomeController> logger,
            string configuration, List<personLoginVM> personLoginVMs, 
             IUnitOfWorks unitOfWork, string user
              
            )
        {
            this.personLoginVMs = personLoginVMs;
            //this.userVMs = userVMs;
            this.user = user;
            _logger = logger;
            this.unitOfWork = unitOfWork;
            connectionstring = configuration;

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
                        dateCreated = DateTime.Now,
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
                        datecreated = DateTime.Now,
                        bankbranch = s.bank,
                        BankACNumber = s.account_number,
                        Birthdate = Convert.ToDateTime(s.date_of_birth),
                        DateEmpl = Convert.ToDateTime(s.date_of_joining),
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
        public async Task  processUploadShipUser()
        {
            try
            {
                foreach (var s in personLoginVMs)
                {
                    
                    var getPersonId = unitOfWork.shiplogin.GetShips(x => x.userName == s.svcNo);
                    if (getPersonId == null)
                    {

                        unitOfWork.shiplogin.Create(new ef_shiplogin()
                        {
                            rank = s.rank,
                            userName = s.svcNo,
                            password = s.password,
                            surName = s.surName,
                            otheName=s.otheName,
                            phoneNumber = s.phoneNumber,
                            command = s.department,
                            ship = s.ship,
                            confirmPassword = false,
                            dateCreated = System.DateTime.Now,
                        }) ;
                        await unitOfWork.Done();
                    }
                }
                //foreach (var s in personLoginVMs)
                //{

                //    using (SqlConnection sqlcon = new SqlConnection(connectionstring))
                //    {
                //        using (SqlCommand cmd = new SqlCommand("UploadShipUsers2", sqlcon))
                //        {
                //            cmd.CommandTimeout = 1200;
                //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //            cmd.Parameters.Add(new SqlParameter("@rank", s.rank));
                //            cmd.Parameters.Add(new SqlParameter("@userName", s.svcNo));
                //            cmd.Parameters.Add(new SqlParameter("@password", s.password));
                //            cmd.Parameters.Add(new SqlParameter("@surName", s.surName));
                //            cmd.Parameters.Add(new SqlParameter("@phoneNumber", s.phoneNumber));
                //             cmd.Parameters.Add(new SqlParameter("@department", s.department));
                //            cmd.Parameters.Add(new SqlParameter("@ship", s.ship));


                //             sqlcon.Open();
                //            cmd.ExecuteNonQuery();
                //        }

                //    }
                //}
            }
            catch (Exception ex)
            {
                throw;
                //_logger.LogInformation(ex.Message);
            }
        }

        public async Task processUpdatepersonnel()
        {
            try
            {
                foreach (var s in personLoginVMs)
                {

                    using (SqlConnection sqlcon = new SqlConnection(connectionstring))
                    {
                        using (SqlCommand cmd = new SqlCommand("CommisionedPersonnelUpload", sqlcon))
                        {
                            cmd.CommandTimeout = 1200;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@svcNo", s.svcNo));
                            cmd.Parameters.Add(new SqlParameter("@oldSvcNo", s.oldsvcno));
                            

                            sqlcon.Open();
                            await cmd.ExecuteNonQueryAsync();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }
        public async Task uploadUpdatepersonnel()
        {
            try
            {
                foreach (var s in personLoginVMs)
                {

                    using (SqlConnection sqlcon = new SqlConnection(connectionstring))
                    {
                        using (SqlCommand cmd = new SqlCommand("UploadUploadPerson", sqlcon))
                        {
                            cmd.CommandTimeout = 1200;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                             cmd.Parameters.Add(new SqlParameter("@svcno", s.svcNo));
	                         cmd.Parameters.Add(new SqlParameter("@rank", s.rank));
                             cmd.Parameters.Add(new SqlParameter("@surName", s.surName));
                             cmd.Parameters.Add(new SqlParameter("@othername", s.otheName));
	                         cmd.Parameters.Add(new SqlParameter("@DateofBirth", s.DateofBirth));
                             cmd.Parameters.Add(new SqlParameter("@DateofJoiningSvc", s.DateofJoiningSvc));
                            cmd.Parameters.Add(new SqlParameter("@Bank", s.Bank));
                            cmd.Parameters.Add(new SqlParameter("@AccountNo", s.AccountNo));
                            cmd.Parameters.Add(new SqlParameter("@AccountName", s.AccountNo));
                            cmd.Parameters.Add(new SqlParameter("@email", s.email));
                            cmd.Parameters.Add(new SqlParameter("@ship", s.ship));
                            cmd.Parameters.Add(new SqlParameter("@payclass", s.payClass));
                            cmd.Parameters.Add(new SqlParameter("@phoneNumber", s.phoneNumber));

                            sqlcon.Open();
                            await cmd.ExecuteNonQueryAsync();
                        }

                    }
                   

                }
                var dd = personLoginVMs.FirstOrDefault();
                using (SqlConnection sqlcon = new SqlConnection(connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("RemoveExitPersonnel", sqlcon))
                    {
                        cmd.CommandTimeout = 1200;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@payclass", dd.payClass));
                        

                        sqlcon.Open();
                        await cmd.ExecuteNonQueryAsync();
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
