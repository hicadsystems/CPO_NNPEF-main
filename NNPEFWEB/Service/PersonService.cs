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
    public class PersonService:IPersonService
    {
        private readonly IUnitOfWorks unitOfWork;
        private readonly string connectionString;
        public PersonService(IConfiguration configuration ,IUnitOfWorks unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public ef_personnelLogin GetPersonBySvc_NO(string person)
        {
            return unitOfWork.PersonLogin.GetPersonBySVC_No(x => x.svcNo == person);
        }
        public Task<ef_personnelLogin> GetPersonel(string svcno)
        {
            return unitOfWork.PersonLogin.GetPersonnel(svcno);
        }
        public Task<ef_personnelLogin> GetPersonelByPassword(string password)
        {
            return unitOfWork.PersonLogin.GetPersonnelBypassword(password);
        }
        public async Task<bool> updatepersonlogin(ef_personnelLogin values)
        {
            unitOfWork.PersonLogin.Update(values);
            return await unitOfWork.Done();
        }

        public ef_personnelLogin personnelLogin(string username,string password)
        {
            return unitOfWork.PersonLogin.GetPersonnelLogin(username, password);
        }

        public async Task<List<ef_personnelLogin>> GetAllPersonel()
        {
            return await unitOfWork.PersonLogin.GetAllPersonnel();
        }
        public Task<ef_personnelLogin> GetPersonelByMail(string email)
        {
            return unitOfWork.PersonLogin.GetPersonnelByMail(email);
        }
        public void updatepersonlogin2(ef_personnelLogin values)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {

                    using (SqlCommand cmd = new SqlCommand("ResetPassword", sqlcon))
                    {
                        cmd.CommandTimeout = 1200;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@username", values.userName));
                        cmd.Parameters.Add(new SqlParameter("@password", values.password));
                        cmd.Parameters.Add(new SqlParameter("@expiredate", values.expireDate));

                        sqlcon.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)

            {

            }
        }
    }
}
