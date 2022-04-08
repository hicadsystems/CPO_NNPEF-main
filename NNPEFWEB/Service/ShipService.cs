using Microsoft.Extensions.Configuration;
using NNPEFWEB.Models;
using NNPEFWEB.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{
    public class ShipService : IShipService
    {
        private readonly IUnitOfWorks unitOfWork;
        private readonly string connectionString;
        public ShipService(IConfiguration configuration, IUnitOfWorks unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            connectionString = configuration.GetConnectionString("DefaultConnection");

        }
        public Task<ef_shiplogin> GetUserByShip(string person)
        {
            return unitOfWork.shiplogin.GetUserByShip(person);
        }
        public ef_shiplogin GetPersonBySvcno(string person)
        {
            return unitOfWork.shiplogin.GetShips(x => x.userName == person);
        }
        public async Task<bool> addShipLogin(ef_shiplogin value)
        {
            unitOfWork.shiplogin.Create(value);
            return await unitOfWork.Done();
        }
        public async Task<bool> UpdateShipLogin(ef_shiplogin value)
        {
            unitOfWork.shiplogin.Update(value);
            return await unitOfWork.Done();
        }
         public Task<ef_shiplogin> GetshiploginById(int id)
        {
            return unitOfWork.shiplogin.Find(id);
        }
        public Task<ef_shiplogin> GetPersonel(string svcno)
        {
            return unitOfWork.shiplogin.GetPersonnel(svcno);
        }
        public Task<ef_shiplogin> GetPersonelByPassword(string password, string per)
        {
            return unitOfWork.shiplogin.GetPersonnelBypassword(password, per);
        }
        //public async Task<bool> updateshiplogin(ef_shiplogin values)
        //{
        //    unitOfWork.shiplogin.Update(values);
        //    return await unitOfWork.Done();
        //}

        public ef_shiplogin personnelLogin(string username, string password)
        {
            return unitOfWork.shiplogin.GetPersonnelLogin(username, password);
        }
        public void DeleteUser(int id)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {

                    using (SqlCommand cmd = new SqlCommand("DeleteShipUser", sqlcon))
                    {
                        cmd.CommandTimeout = 1200;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        sqlcon.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)

            {

            }
        }
        public async Task<IEnumerable<ef_shiplogin>> GetAllPersonel()
        { 
            return await unitOfWork.shiplogin.GetAllPersonnel();
        }
        public Task<ef_shiplogin> GetPersonelByMail(string email)
        {
            return unitOfWork.shiplogin.GetPersonnelByMail(email);
        }
        public void updateshiplogin2(ef_shiplogin values)
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
        public void updateshiploginss(ef_shiplogin values)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {

                    using (SqlCommand cmd = new SqlCommand("ResetShipPassword", sqlcon))
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
