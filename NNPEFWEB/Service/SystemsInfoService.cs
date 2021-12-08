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
    public class SystemsInfoService : ISystemsInfoService
    {
        private readonly IUnitOfWorks _unitOfWorks;
        private readonly string connectionString;
        public SystemsInfoService(IConfiguration configuration,IUnitOfWorks unitOfWorks)
        {
            this._unitOfWorks = unitOfWorks;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<ef_systeminfo> GetAllSystemsInfo()
        {
            var response = _unitOfWorks.SystemsInfo.All();

            return response;
        }

        public ef_systeminfo GetSysteminfo()
        {
            var respose = _unitOfWorks.SystemsInfo.GetSysteminfo();

            return respose;
        }

        public ef_systeminfo GetSystemsInfo(int id)
        {
            var response = _unitOfWorks.SystemsInfo.Find(id).Result;

            return response;
        }

        public string CreateSystemsInfo(systeminfoVM ef_SysteminfoVM)
        {
            string message = string.Empty;

            ef_systeminfo ef_Systeminfo = new ef_systeminfo();

            try
            {
                ef_Systeminfo.comp_code = ef_SysteminfoVM.comp_code;
                ef_Systeminfo.comp_name = ef_SysteminfoVM.comp_name;
                ef_Systeminfo.Address = ef_SysteminfoVM.Address;
                ef_Systeminfo.SiteStatus = ef_SysteminfoVM.SiteStatus;
                ef_Systeminfo.opendate = ef_SysteminfoVM.opendate;
                ef_Systeminfo.hrlink = ef_SysteminfoVM.hrlink;
                ef_Systeminfo.town = ef_SysteminfoVM.town;
                ef_Systeminfo.lg = ef_SysteminfoVM.lg;
                ef_Systeminfo.state = ef_SysteminfoVM.state;
                ef_Systeminfo.email = ef_SysteminfoVM.email;
                ef_Systeminfo.box = ef_SysteminfoVM.box;
                ef_Systeminfo.tel = ef_SysteminfoVM.tel;
                ef_Systeminfo.serveraddr = ef_SysteminfoVM.serveraddr;
                ef_Systeminfo.serverport = ef_SysteminfoVM.serverport;
                ef_Systeminfo.email_pword = ef_SysteminfoVM.email_pword;
                ef_Systeminfo.company_image = ef_SysteminfoVM.company_image;
                ef_Systeminfo.datecreated = ef_SysteminfoVM.datecreated;
                ef_Systeminfo.createdby = ef_SysteminfoVM.createdby;
                ef_Systeminfo.closedate = ef_SysteminfoVM.closedate;

                bool response = _unitOfWorks.SystemsInfo.Create(ef_Systeminfo);

                if (response == true)
                {
                    _unitOfWorks.Done();

                    message = "Systems Info Created Successfully";
                }
                else
                {
                    message = "An Error occured while trying to create the systems info";
                }
            }
            catch (Exception ex)
            {

                message = ex.Message;
            }

            return message;

        }

        public string UpdateSystemsInfo(ef_systeminfo ef_Systeminfo)
        {
            string message = string.Empty;

            try
            {
                var systemInfoInDB = _unitOfWorks.SystemsInfo.Find(ef_Systeminfo.Id).Result;

                if (systemInfoInDB == null)
                {
                    message = "System info with name " + ef_Systeminfo.comp_name + "does not exist!";
                }
                else
                {
                    systemInfoInDB.comp_code = ef_Systeminfo.comp_code;
                    systemInfoInDB.comp_name = ef_Systeminfo.comp_name;
                    systemInfoInDB.Address = ef_Systeminfo.Address;
                    systemInfoInDB.SiteStatus = ef_Systeminfo.SiteStatus;
                    systemInfoInDB.opendate = ef_Systeminfo.opendate;
                    systemInfoInDB.hrlink = ef_Systeminfo.hrlink;
                    systemInfoInDB.town = ef_Systeminfo.town;
                    systemInfoInDB.lg = ef_Systeminfo.lg;
                    systemInfoInDB.state = ef_Systeminfo.state;
                    systemInfoInDB.email = ef_Systeminfo.email;
                    systemInfoInDB.box = ef_Systeminfo.box;
                    systemInfoInDB.tel = ef_Systeminfo.tel;
                    systemInfoInDB.serveraddr = ef_Systeminfo.serveraddr;
                    systemInfoInDB.serverport = ef_Systeminfo.serverport;
                    systemInfoInDB.email_pword = ef_Systeminfo.email_pword;
                    systemInfoInDB.company_image = ef_Systeminfo.company_image;
                    systemInfoInDB.datecreated = ef_Systeminfo.datecreated;
                    systemInfoInDB.createdby = ef_Systeminfo.createdby;
                    systemInfoInDB.closedate = ef_Systeminfo.closedate;

                    var response = _unitOfWorks.SystemsInfo.Update(systemInfoInDB);

                    if (response == true)
                    {
                        _unitOfWorks.Done();

                        message = "System info with name " + ef_Systeminfo.comp_name + "Updated Successfully!";
                    }
                    else
                    {
                        message = "An Error occured while trying to Update systems info";
                    }
                }
            }
            catch (Exception ex)
            {

                message = ex.Message;
            }

            return message;
        }
        public void UpdateFormNumber(int formnumber,int payclass)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {

                    using (SqlCommand cmd = new SqlCommand("UpdateFormNumber", sqlcon))
                    {
                        cmd.CommandTimeout = 1200;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@formnumber", formnumber));
                        cmd.Parameters.Add(new SqlParameter("@payclass", payclass));
                       
                        sqlcon.Open();
                        cmd.ExecuteNonQuery();
                      }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public string DeleteSystemsInfo(int id)
        {
            string message = string.Empty;

            try
            {
                var systemInfoInDB = _unitOfWorks.SystemsInfo.Find(id).Result;

                if (systemInfoInDB == null)
                {
                    message = "System info with name does not exist!";
                }
                else
                {
                    _unitOfWorks.SystemsInfo.Remove(systemInfoInDB);

                    _unitOfWorks.Done();

                    message = "System info with name " + systemInfoInDB.comp_name + "Deleted Successfully!";
                }
            }
            catch (Exception ex)
            {

                message = ex.Message;
            }

            return message;
        }
    }
}
