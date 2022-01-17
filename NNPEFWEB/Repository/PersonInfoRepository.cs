using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NNPEFWEB.Data;
using NNPEFWEB.Models;
using NNPEFWEB.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NNPEFWEB.Repository
{
    public class PersonInfoRepository:GenericRepository<ef_personalInfo>,IPersonInfoRepository
    {
        private readonly ApplicationDbContext _context;
   
        //private readonly ;
        public PersonInfoRepository(ApplicationDbContext context) :base(context)
        {
            this._context = context;
          }

        public async Task<ef_personalInfo> GetPersonalInfo(string svcno)
        {
            return _context.ef_personalInfos.Where(x => x.serviceNumber == svcno).FirstOrDefault();
        }
        public async Task<List<PersonalInfoModel>> FilterBySearch(string svcno)
        {
            return await (from pp in _context.ef_personalInfos
                    where (pp.serviceNumber.Replace(@"/", "").Contains(svcno))
                    select new PersonalInfoModel
                    {
                        serviceNumber = pp.serviceNumber,
                        Surname = pp.Surname,
                        OtherName = pp.OtherName,
                    }).ToListAsync();
                    
        }
        public async Task<List<PersonalInfoModel>> getPersonList(int iDisplayStart, int iDisplayLength, string payclass, string ship)
        {
            return await (from pers in _context.ef_personalInfos
                          where (pers.payrollclass == payclass && pers.ship == ship)
                          select new PersonalInfoModel
                          {

                              BankACNumber = pers.BankACNumber,
                              DateEmpl = pers.DateEmpl,
                              email = pers.email,
                              Surname = pers.Surname,
                              home_address = pers.home_address,
                              OtherName = pers.OtherName,
                              Rank = pers.Rank,
                              serviceNumber = pers.serviceNumber,
                              Sex = pers.Sex,


                          }).Skip(iDisplayStart).Take(iDisplayLength).ToListAsync();
        }
        //public async Task<List<PersonalInfoModel>> getPersonListold(int iDisplayStart, int iDisplayLength, string payclass, string ship)
        //{
        //    return await (from pers in _context.ef_personalInfos
        //                  where (pers.payrollclass==payclass&& pers.ship==ship)
        //                  select new PersonalInfoModel
        //                  {

        //                      BankACNumber = pers.BankACNumber,
        //                      DateEmpl = pers.DateEmpl,
        //                      email = pers.email,
        //                      Surname = pers.Surname,
        //                      home_address = pers.home_address,
        //                      OtherName = pers.OtherName,
        //                      Rank = pers.Rank,
        //                      serviceNumber = pers.serviceNumber,
        //                      Sex = pers.Sex,


        //                  }).Skip(iDisplayStart).Take(iDisplayLength).ToListAsync();
        //}

        public async Task<int> getPersonListCount(string payclass, string ship)
        {
            return await (from pers in _context.ef_personalInfos
                          where (pers.payrollclass == payclass && pers.ship == ship)
                          select new PersonalInfoModel
                          {

                              BankACNumber = pers.BankACNumber,
                              DateEmpl = pers.DateEmpl,
                              email = pers.email,
                              Surname = pers.Surname,
                              home_address = pers.home_address,
                              OtherName = pers.OtherName,
                              Rank = pers.Rank,
                              serviceNumber = pers.serviceNumber,
                              Sex = pers.Sex,


                          }).CountAsync();
        }


        public ef_personalInfo GetOnePersonnel(string svcno)
        {
           return (from ppl in _context.ef_personalInfos
                      where (ppl.serviceNumber==svcno)
                      select new ef_personalInfo
                      {
                          
                          serviceNumber = ppl.serviceNumber,
                          Surname = ppl.Surname,
                          OtherName = ppl.OtherName,
                          Rank = ppl.Rank,
                          email = ppl.email,
                          gsm_number = ppl.gsm_number,
                          gsm_number2 = ppl.gsm_number2,
                          Birthdate = ppl.Birthdate,
                          DateEmpl = ppl.DateEmpl,
                          seniorityDate = ppl.seniorityDate,
                          home_address = ppl.home_address,
                          branch = ppl.branch,
                          command = ppl.command,
                          ship = ppl.ship,
                          specialisation = ppl.specialisation,
                          StateofOrigin = ppl.StateofOrigin,
                          LocalGovt = ppl.LocalGovt,
                          religion = ppl.religion,
                          MaritalStatus = ppl.MaritalStatus,

                          chid_name = ppl.chid_name,
                          chid_name2 = ppl.chid_name2,
                          chid_name3 = ppl.chid_name3,
                          chid_name4 = ppl.chid_name4,

                          sp_name = ppl.sp_name,
                          sp_phone = ppl.sp_phone,
                          sp_phone2 = ppl.sp_phone2,
                          sp_email = ppl.sp_email,

                          nok_name = ppl.nok_name,
                          nok_phone = ppl.nok_phone,
                          nok_address = ppl.nok_address,
                          nok_email = ppl.nok_email,
                          nok_nationalId = ppl.nok_nationalId,
                          nok_relation = ppl.nok_relation,
                          nok_name2 = ppl.nok_name2,
                          nok_phone2 = ppl.nok_phone2,
                          nok_address2 = ppl.nok_address2,
                          nok_email2 = ppl.nok_email2,
                          nok_nationalId2 = ppl.nok_nationalId2,
                          nok_relation2 = ppl.nok_relation2,

                          Bankcode = ppl.Bankcode,
                          BankACNumber = ppl.BankACNumber,
                          bankbranch = ppl.bankbranch,

                          rent_subsidy = ppl.rent_subsidy,
                          shift_duty_allow = ppl.shift_duty_allow,
                          aircrew_allow = ppl.aircrew_allow,
                          SBC_allow = ppl.SBC_allow,
                          hazard_allow = ppl.hazard_allow,
                          special_forces_allow = ppl.special_forces_allow,
                          other_allow = ppl.other_allow,

                          FGSHLS_loan = ppl.FGSHLS_loan,
                          welfare_loan = ppl.welfare_loan,
                          car_loan = ppl.car_loan,
                          NNMFBL_loan = ppl.NNMFBL_loan,
                          NNNCS_loan = ppl.NNNCS_loan,
                          PPCFS_loan = ppl.PPCFS_loan,
                          Anyother_Loan = ppl.Anyother_Loan,

                          FGSHLS_loanYear = ppl.FGSHLS_loanYear,
                          welfare_loanYear = ppl.welfare_loanYear,
                          car_loanYear = ppl.car_loanYear,
                          NNMFBL_loanYear = ppl.NNMFBL_loanYear,
                          NNNCS_loanYear = ppl.NNNCS_loanYear,
                          PPCFS_loanYear = ppl.PPCFS_loanYear,
                          Anyother_LoanYear = ppl.Anyother_LoanYear,


                          div_off_name = ppl.div_off_name,
                          div_off_rank = ppl.div_off_rank,
                          div_off_svcno = ppl.div_off_svcno,

                          hod_name = ppl.hod_name,
                          hod_rank = ppl.hod_rank,
                          hod_svcno = ppl.hod_svcno,

                          cdr_name = ppl.cdr_name,
                          cdr_rank = ppl.cdr_rank,
                          cdr_svcno = ppl.cdr_svcno,

                          // Passport=ppl.Passport,
                          // NokPassport= ppl.NokPassport,
                          //AltNokPassport = ppl.AltNokPassport,
                      }).FirstOrDefault();

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
       
public IEnumerable<ef_personalInfo> downloadPersonalReport(string svcno)
{
    var pp = new List<ef_personalInfo>();
            //using (SqlConnection sqlcon = new SqlConnection(connectionString))
            //{
            //    using (SqlCommand cmd = new SqlCommand("DownloadForm", sqlcon))
            //    {
            //        cmd.CommandTimeout = 1200;
            //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //        cmd.Parameters.Add(new SqlParameter("@svcno", svcno));
            //        sqlcon.Open();

            //        using (SqlDataReader sdr = cmd.ExecuteReader())
            //        {
            //            while (sdr.Read())
            //            {
            //                pp.Add(new ef_personalInfo
            //                {
            //                      serviceNumber = sdr["serviceNumber"].ToString(),
            //                      Surname = sdr["Surname"].ToString(),
            //                      OtherName = sdr["OtherName"].ToString(),
            //                      Rank = sdr["Rank"].ToString(),
            //                      email = sdr["email"].ToString(),
            //                      gsm_number = sdr["gsm_number"].ToString(),
            //                      gsm_number2 = sdr["gsm_number2"].ToString(),
            //                      Birthdate =Convert.ToDateTime(sdr["Birthdate"]),
            //                      DateEmpl = Convert.ToDateTime(sdr["DateEmpl"]),
            //                      seniorityDate = Convert.ToDateTime(sdr["seniorityDate"]),
            //                      home_address = sdr["home_address"].ToString(),
            //                      branch = sdr["branchName"].ToString(),
            //                      command = sdr["commandName"].ToString(),
            //                      ship = sdr["ship"].ToString(),
            //                      specialisation = sdr["specialisation"].ToString(),
            //                      StateofOrigin = sdr["Name"].ToString(),
            //                      LocalGovt = sdr["lgaName"].ToString(),
            //                      religion = sdr["religion"].ToString(),
            //                      MaritalStatus = sdr["MaritalStatus"].ToString(),
                        
            //                      chid_name = sdr["chid_name"].ToString(),
            //                      chid_name2 = sdr["chid_name2"].ToString(),
            //                      chid_name3 = sdr["chid_name3"].ToString(),
            //                      chid_name4 = sdr["chid_name4"].ToString(),

            //                      sp_name = sdr["sp_name"].ToString(),
            //                      sp_phone = sdr["sp_phone"].ToString(),
            //                      sp_phone2 = sdr["sp_phone2"].ToString(),
            //                      sp_email = sdr["sp_email"].ToString(),

            //                      nok_name = sdr["nok_name"].ToString(),
            //                      nok_phone = sdr["nok_phone"].ToString(),
            //                      nok_address = sdr["nok_address"].ToString(),
            //                      nok_email = sdr["nok_email"].ToString(),
            //                      nok_nationalId = sdr["nok_nationalId"].ToString(),
            //                      nok_relation = sdr["nokrelation"].ToString(),
            //                      nok_name2 = sdr["nok_name2"].ToString(),
            //                      nok_phone2 = sdr["nok_phone2"].ToString(),
            //                      nok_address2 = sdr["nok_address2"].ToString(),
            //                      nok_email2 = sdr["nok_email2"].ToString(),
            //                      nok_nationalId2 = sdr["nok_nationalId2"].ToString(),
            //                      nok_relation2 = sdr["nokrelation2"].ToString(),

            //                      Bankcode = sdr["bankname"].ToString(),
            //                      BankACNumber = sdr["BankACNumber"].ToString(),
            //                      bankbranch = sdr["bankbranch"].ToString(),

            //                      rent_subsidy = sdr["rent_subsidy"].ToString(),
            //                      shift_duty_allow = sdr["shift_duty_allow"].ToString(),
            //                      aircrew_allow = sdr["aircrew_allow"].ToString(),
            //                      SBC_allow = sdr["SBC_allow"].ToString(),
            //                      hazard_allow = sdr["hazard_allow"].ToString(),
            //                      special_forces_allow = sdr["special_forces_allow"].ToString(),
            //                      other_allow = sdr["other_allow"].ToString(),

            //                      FGSHLS_loan = sdr["FGSHLS_loan"].ToString(),
            //                      welfare_loan = sdr["welfare_loan"].ToString(),
            //                      car_loan = sdr["car_loan"].ToString(),
            //                      NNMFBL_loan = sdr["NNMFBL_loan"].ToString(),
            //                      NNNCS_loan = sdr["NNNCS_loan"].ToString(),
            //                      PPCFS_loan = sdr["PPCFS_loan"].ToString(),
            //                      Anyother_Loan = sdr["Anyother_Loan"].ToString(),

            //                      FGSHLS_loanYear = sdr["FGSHLS_loanYear"].ToString(),
            //                      welfare_loanYear = sdr["welfare_loanYear"].ToString(),
            //                      car_loanYear = sdr["car_loanYear"].ToString(),
            //                      NNMFBL_loanYear = sdr["NNMFBL_loanYear"].ToString(),
            //                      NNNCS_loanYear = sdr["NNNCS_loanYear"].ToString(),
            //                      PPCFS_loanYear = sdr["PPCFS_loanYear"].ToString(),
            //                      Anyother_LoanYear = sdr["Anyother_LoanYear"].ToString(),


            //                      div_off_name = sdr["div_off_name"].ToString(),
            //                      div_off_rank = sdr["div_off_rank"].ToString(),
            //                      div_off_svcno = sdr["div_off_svcno"].ToString(),

            //                      hod_name = sdr["hod_name"].ToString(),
            //                      hod_rank = sdr["hod_rank"].ToString(),
            //                      hod_svcno = sdr["hod_svcno"].ToString(),

            //                      cdr_name = sdr["cdr_name"].ToString(),
            //                      cdr_rank = sdr["cdr_rank"].ToString(),
            //                      cdr_svcno = sdr["cdr_svcno"].ToString(),

            //            //  Passport=Convert.ToByte(sdr["Passport"]),
            //             // NokPassport= Convert.ToByte(sdr["NokPassport"]),

            //                });
            //            }
            //        }
            //    }
            //}
            return pp;
        }
    public IEnumerable<PersonalInfoModel> GetPersonalReport(string svcno)
        {
            var pp = new List<PersonalInfoModel>();
                pp = (from per in _context.ef_personalInfos
                      //join rk in _context.ef_ranks on per.Rank equals rk.rankName
                      join cm in _context.ef_commands on per.command equals cm.code
                      join state in _context.ef_states on per.StateofOrigin equals Convert.ToString(state.StateId)
                      join lga in _context.ef_localgovts on per.LocalGovt equals Convert.ToString(lga.Id)
                      //join spec in _context.ef_specialisationareas on per.specialisation equals spec.specName.ToString()
                      join br in _context.ef_branches on per.branch equals br.code
                      join rel1 in _context.ef_relationships on per.nok_relation equals Convert.ToString(rel1.Id)
                      join rel2 in _context.ef_relationships on per.nok_relation2 equals Convert.ToString(rel2.Id)
                      join bank in _context.ef_banks on per.Bankcode equals bank.bankcode
                      where (per.serviceNumber==svcno)
                      select new PersonalInfoModel
                      {
                          serviceNumber = per.serviceNumber,
                          Surname = per.Surname,
                          OtherName = per.OtherName,
                          Rank = per.Rank,
                          email = per.email,
                          gsm_number = per.gsm_number,
                          gsm_number2 = per.gsm_number2,
                          Birthdate = per.Birthdate,
                          DateEmpl = per.DateEmpl,
                          seniorityDate = per.seniorityDate,
                          home_address = per.home_address,
                          branch = br.branchName,
                          command = cm.commandName,
                          ship = per.ship,
                          specialisation = per.specialisation,
                          StateofOrigin = state.Name,
                          LocalGovt = lga.lgaName,
                          religion = per.religion,
                          MaritalStatus = per.MaritalStatus,
                        
                          chid_name = per.chid_name,
                          chid_name2 = per.chid_name2,
                          chid_name3 = per.chid_name3,
                          chid_name4 = per.chid_name4,

                          sp_name = per.sp_name,
                          sp_phone = per.sp_phone,
                          sp_phone2 = per.sp_phone2,
                          sp_email = per.sp_email,

                          nok_name = per.nok_name,
                          nok_phone = per.nok_phone,
                          nok_address = per.nok_address,
                          nok_email = per.nok_email,
                          nok_nationalId = per.nok_nationalId,
                          nok_relation = rel1.description,
                          nok_name2 = per.nok_name2,
                          nok_phone2 = per.nok_phone2,
                          nok_address2 = per.nok_address2,
                          nok_email2 = per.nok_email2,
                          nok_nationalId2 = per.nok_nationalId2,
                          nok_relation2 = rel2.description,

                          Bankcode = bank.bankname,
                          BankACNumber = per.BankACNumber,
                          bankbranch = per.bankbranch,

                          rent_subsidy = per.rent_subsidy,
                          shift_duty_allow = per.shift_duty_allow,
                          aircrew_allow = per.aircrew_allow,
                          SBC_allow = per.SBC_allow,
                          hazard_allow = per.hazard_allow,
                          special_forces_allow = per.special_forces_allow,
                          other_allow = per.other_allow,

                          FGSHLS_loan = per.FGSHLS_loan,
                          welfare_loan = per.welfare_loan,
                          car_loan = per.car_loan,
                          NNMFBL_loan = per.NNMFBL_loan,
                          NNNCS_loan = per.NNNCS_loan,
                          PPCFS_loan = per.PPCFS_loan,
                          Anyother_Loan = per.Anyother_Loan,

                          FGSHLS_loanYear = per.FGSHLS_loanYear,
                          welfare_loanYear = per.welfare_loanYear,
                          car_loanYear = per.car_loanYear,
                          NNMFBL_loanYear = per.NNMFBL_loanYear,
                          NNNCS_loanYear = per.NNNCS_loanYear,
                          PPCFS_loanYear = per.PPCFS_loanYear,
                          Anyother_LoanYear = per.Anyother_LoanYear,


                          div_off_name = per.div_off_name,
                          div_off_rank = per.div_off_rank,
                          div_off_svcno = per.div_off_svcno,

                          hod_name = per.hod_name,
                          hod_rank = per.hod_rank,
                          hod_svcno = per.hod_svcno,

                          cdr_name = per.cdr_name,
                          cdr_rank = per.cdr_rank,
                          cdr_svcno = per.cdr_svcno,

                          Passport=per.Passport,
                          NokPassport=per.NokPassport,

                      }).ToList();

            return pp;
        }

        public ef_personalInfo GetPersonBySVC_No(Expression<Func<ef_personalInfo, bool>> predicate)
        {
            return _context.ef_personalInfos.FirstOrDefault(predicate);
        }

        public IEnumerable<ef_personalInfo> GetPersonnelByCommand(string payclass)
        {
            var pp = new List<ef_personalInfo>();
            
                pp = (from ppl in _context.ef_personalInfos
                      //join rk in _context.ef_ranks on ppl.Rank equals rk.rankName
                      join cm in _context.ef_commands on ppl.command equals cm.code
                      where (ppl.payrollclass == payclass && (ppl.Status == "HOD"|| ppl.Status == "CDR"))
                      select new ef_personalInfo
                      {
                          serviceNumber = ppl.serviceNumber,
                          Surname = ppl.Surname,
                          OtherName = ppl.OtherName,
                          Rank = ppl.Rank,
                          seniorityDate = ppl.seniorityDate,
                          command = cm.commandName,
                          payrollclass=ppl.payrollclass,
                          ship=ppl.ship,
                          Status=ppl.Status

                      }).OrderByDescending(x => x.seniorityDate).ThenByDescending(x => x.serviceNumber).ToList();

            
            return pp;
        }

        public IEnumerable<ef_personalInfo> GetUpdatedPersonnelBySVCNO2(string payclass,string svcno)
        {
            var pp = new List<ef_personalInfo>();
            
                pp = (from ppl in _context.ef_personalInfos
                      where (ppl.payrollclass == payclass && ppl.serviceNumber == svcno)
                      select new ef_personalInfo
                      {
                          serviceNumber = ppl.serviceNumber,
                          Surname = ppl.Surname,
                          OtherName = ppl.OtherName,
                          Rank = ppl.Rank,
                          seniorityDate = ppl.seniorityDate,
                          command = ppl.command,
                          payrollclass = ppl.payrollclass,
                          classes = ppl.classes,
                          ship = ppl.ship
                      }).OrderByDescending(x => x.seniorityDate).ThenByDescending(x => x.serviceNumber).ToList();

           
            return pp;
        }

        public IEnumerable<ef_personalInfo> GetUpdatedPersonnelBySVCNO(string payclass, string ship, string svcno)
        {
            var pp = new List<ef_personalInfo>();
            if (ship == null)
            {
                pp = (from ppl in _context.ef_personalInfos
                      where (ppl.payrollclass == payclass && ppl.serviceNumber == svcno)
                      select new ef_personalInfo
                      {
                          serviceNumber = ppl.serviceNumber,
                          Surname = ppl.Surname,
                          OtherName = ppl.OtherName,
                          Rank = ppl.Rank,
                          seniorityDate = ppl.seniorityDate,
                          command = ppl.command,
                          payrollclass = ppl.payrollclass,
                          classes = ppl.classes,
                          ship = ppl.ship
                      }).OrderByDescending(x => x.seniorityDate).ThenByDescending(x => x.serviceNumber).ToList();

            }
            else
            {
                pp = (from ppl in _context.ef_personalInfos
                      where (ppl.ship == ship && ppl.classes == Convert.ToInt32(payclass) && ppl.serviceNumber == svcno)
                      select new ef_personalInfo
                      {
                          serviceNumber = ppl.serviceNumber,
                          Surname = ppl.Surname,
                          OtherName = ppl.OtherName,
                          Rank = ppl.Rank,
                          seniorityDate = ppl.seniorityDate,
                          command = ppl.command,
                          payrollclass = ppl.payrollclass,
                          classes = ppl.classes,
                          ship = ppl.ship
                      }).OrderByDescending(x => x.seniorityDate).ThenByDescending(x => x.serviceNumber).ToList();
            }
                return pp;
        }
        public IEnumerable<ef_personalInfo> GetUpdatedPersonnel(string payclass,string ship)
        {
            var pp = new List<ef_personalInfo>();

            if (ship == null)
            {
                pp = (from ppl in _context.ef_personalInfos
                      where (ppl.payrollclass == payclass && ppl.Status == "SHIP")
                      select new ef_personalInfo
                      {
                          Id = ppl.Id,
                          serviceNumber = ppl.serviceNumber,
                          Surname = ppl.Surname,
                          OtherName = ppl.OtherName,
                          Rank = ppl.Rank,
                          seniorityDate = ppl.seniorityDate,
                          command = ppl.command,
                          payrollclass = ppl.payrollclass,
                          classes = ppl.classes,
                          ship = ppl.ship
                      }).OrderByDescending(x => x.seniorityDate).ThenByDescending(x => x.serviceNumber).ToList();

            }
            else
            {
                pp = (from ppl in _context.ef_personalInfos
                      where (ppl.ship == ship && ppl.classes == Convert.ToInt32(payclass) && ppl.Status == "SHIP")
                      select new ef_personalInfo
                      {
                          Id = ppl.Id,
                          serviceNumber = ppl.serviceNumber,
                          Surname = ppl.Surname,
                          OtherName = ppl.OtherName,
                          Rank = ppl.Rank,
                          seniorityDate = ppl.seniorityDate,
                          command = ppl.command,
                          payrollclass = ppl.payrollclass,
                          classes = ppl.classes,
                          ship = ppl.ship

                      }).OrderByDescending(x => x.seniorityDate).ThenByDescending(x => x.serviceNumber).ToList();
            }

            return pp;
        }

        public IEnumerable<ef_personalInfo> GetPersonnelStatusRepo(string statusToSearch)
        {
            // statusToSearch = statusCriteria.Trim();

            //var sortedpersonelInfo = new List<ef_personalInfo>();

            var pp = new List<ef_personalInfo>();

            if (statusToSearch != " " && statusToSearch != null)
            {
                if (statusToSearch == "AllStaff")
                {
                    pp = (from ppl in _context.ef_personalInfos.Where(x => x.Rank != null)
                          join rak in _context.ef_ranks on ppl.Rank equals rak.rankName
                          select new ef_personalInfo
                          {
                              Id = ppl.Id,
                              rankId = rak.Id,
                              serviceNumber = ppl.serviceNumber,
                              Surname = ppl.Surname,
                              OtherName = ppl.OtherName,
                              Rank = ppl.Rank,
                              seniorityDate = ppl.seniorityDate,
                              command = ppl.command,
                              payrollclass = ppl.payrollclass,
                              classes = ppl.classes,
                              ship = ppl.ship,
                              Status = ppl.Status,

                          }).OrderByDescending(x => x.ship).ThenByDescending(x => x.rankId).ToList();
                }
                else if (statusToSearch == "Processed")
                {
                    pp = (from ppl in _context.ef_personalInfos.Where(x => x.Rank != null)
                          where ppl.emolumentform == "Yes"
                          join rak in _context.ef_ranks on ppl.Rank equals rak.rankName
                          select new ef_personalInfo
                          {
                              Id = ppl.Id,
                              rankId = rak.Id,
                              serviceNumber = ppl.serviceNumber,
                              Surname = ppl.Surname,
                              OtherName = ppl.OtherName,
                              Rank = ppl.Rank,
                              seniorityDate = ppl.seniorityDate,
                              command = ppl.command,
                              payrollclass = ppl.payrollclass,
                              classes = ppl.classes,
                              ship = ppl.ship,
                              Status = ppl.Status,

                          }).OrderByDescending(x => x.ship).ThenByDescending(x => x.rankId).ToList();
                }
                else
                {
                    pp = (from ppl in _context.ef_personalInfos
                          where ppl.Status == statusToSearch
                          join rak in _context.ef_ranks on ppl.Rank equals rak.rankName
                          select new ef_personalInfo
                          {
                              Id = ppl.Id,
                              rankId = rak.Id,
                              serviceNumber = ppl.serviceNumber,
                              Surname = ppl.Surname,
                              OtherName = ppl.OtherName,
                              Rank = ppl.Rank,
                              seniorityDate = ppl.seniorityDate,
                              command = ppl.command,
                              payrollclass = ppl.payrollclass,
                              classes = ppl.classes,
                              ship = ppl.ship,
                              Status = ppl.Status,

                          }).OrderByDescending(x => x.ship).ThenByDescending(x => x.rankId).ToList();
                }
            }
            else
            {
                pp = (from ppl in _context.ef_personalInfos.Where(x=>x.Rank!=null)
                      join rak in _context.ef_ranks on ppl.Rank equals rak.rankName
                      select new ef_personalInfo
                      {
                          Id = ppl.Id,
                          rankId = rak.Id,
                          serviceNumber = ppl.serviceNumber,
                          Surname = ppl.Surname,
                          OtherName = ppl.OtherName,
                          Rank = ppl.Rank,
                          seniorityDate = ppl.seniorityDate,
                          command = ppl.command,
                          payrollclass = ppl.payrollclass,
                          classes = ppl.classes,
                          ship = ppl.ship,
                          Status = ppl.Status,

                      }).OrderByDescending(x => x.ship).ThenByDescending(x => x.rankId).ToList();
            }

            //var rankinDb = _context.ef_ranks.ToList();
            //rankinDb.Reverse();

            //foreach (var rank in rankinDb)
            //{
            //    foreach (var personInfo in pp)
            //    {
            //        if (rank.rankName == personInfo.Rank)
            //        {
            //            sortedpersonelInfo.Add(personInfo);
            //        }
            //    }
            //}


            return pp;
        }

        public IEnumerable<ef_personalInfo> GetUpdatedPersonnel2()
        {
            var pp = new List<ef_personalInfo>();

            pp = (from ppl in _context.ef_personalInfos
                  select new ef_personalInfo
                  {
                      Id = ppl.Id,
                      serviceNumber = ppl.serviceNumber,
                      Surname = ppl.Surname,
                      OtherName = ppl.OtherName,
                      Rank = ppl.Rank,
                      seniorityDate = ppl.seniorityDate,
                      command = ppl.command,
                      payrollclass = ppl.payrollclass,
                      classes = ppl.classes,
                      ship = ppl.ship

                  }).OrderByDescending(x => x.ship).ThenByDescending(x => x.Id).ToList();


            return pp;
        }
        public IEnumerable<ef_personalInfo> GetUpdatedPersonnel3(string payclass, string ship)
        {
            var pp = new List<ef_personalInfo>();
            if (ship == null)
            {
                pp = (from ppl in _context.ef_personalInfos
                      where (ppl.payrollclass == payclass && ppl.Status == "CPO")
                      select new ef_personalInfo
                      {
                          Id = ppl.Id,
                          serviceNumber = ppl.serviceNumber,
                          Surname = ppl.Surname,
                          OtherName = ppl.OtherName,
                          Rank = ppl.Rank,
                          seniorityDate = ppl.seniorityDate,
                          command = ppl.command,
                          payrollclass = ppl.payrollclass,
                          classes = ppl.classes,
                          ship = ppl.ship
                      }).OrderByDescending(x => x.seniorityDate).ThenByDescending(x => x.serviceNumber).ToList();

            }
            else
            {
                pp = (from ppl in _context.ef_personalInfos
                      where (ppl.ship == ship && ppl.classes == Convert.ToInt32(payclass) && ppl.Status == "CPO")
                      select new ef_personalInfo
                      {
                          Id = ppl.Id,
                          serviceNumber = ppl.serviceNumber,
                          Surname = ppl.Surname,
                          OtherName = ppl.OtherName,
                          Rank = ppl.Rank,
                          seniorityDate = ppl.seniorityDate,
                          command = ppl.command,
                          payrollclass = ppl.payrollclass,
                          classes = ppl.classes,
                          ship = ppl.ship
                      }).OrderByDescending(x => x.seniorityDate).ThenByDescending(x => x.serviceNumber).ToList();
            }
            return pp;
        }

        public List<ef_personalInfo> GetPEFReport2()
        {
            var pp = new List<ef_personalInfo>();
                     pp = (from ppl in _context.ef_personalInfos
                          //join rk in _context.ef_ranks on ppl.Rank equals rk.rankName
                      join cm in _context.ef_commands on ppl.command equals cm.code
                      select new ef_personalInfo
                      {
                          serviceNumber = ppl.serviceNumber,
                          Surname = ppl.Surname,
                          OtherName = ppl.OtherName,
                          Rank = ppl.Rank,
                          seniorityDate = ppl.seniorityDate,
                          command = cm.commandName,
                          payrollclass = ppl.payrollclass,
                          ship = ppl.ship
                      }).OrderByDescending(x => x.seniorityDate).ThenByDescending(x => x.serviceNumber).ToList();
            return pp;
        }
         public List<ef_personalInfo> GetPEFReport(ApiSearchModel apiSearchModel)
        {
            var pp = new List<ef_personalInfo>();
            if (apiSearchModel.sortby == "ALL")
            {
                pp = (from ppl in _context.ef_personalInfos
                      //join rk in _context.ef_ranks on ppl.Rank equals rk.rankName
                      join cm in _context.ef_commands on ppl.command equals cm.code
                      where(apiSearchModel.command== ""||ppl.command== apiSearchModel.command)
                      select new ef_personalInfo
                      {
                          serviceNumber = ppl.serviceNumber,
                          Surname = ppl.Surname,
                          OtherName = ppl.OtherName,
                          Rank = ppl.Rank,
                          seniorityDate = ppl.seniorityDate,
                          command = cm.commandName,
                          payrollclass = ppl.payrollclass,
                          ship = ppl.ship
                      }).OrderByDescending(x => x.seniorityDate).ThenByDescending(x => x.serviceNumber).ToList();

            }
            else if (apiSearchModel.sortby == "CPO")
            {
                pp = (from ppl in _context.ef_personalInfos
                      //join rk in _context.ef_ranks on ppl.Rank equals rk.rankName
                      join cm in _context.ef_commands on ppl.command equals cm.code
                      where (ppl.Status=="CPO" && (apiSearchModel.command == ""||ppl.command== apiSearchModel.command))
                      select new ef_personalInfo
                      {
                          serviceNumber = ppl.serviceNumber,
                          Surname = ppl.Surname,
                          OtherName = ppl.OtherName,
                          Rank = ppl.Rank,
                          seniorityDate = ppl.seniorityDate,
                          command = cm.commandName,
                          payrollclass = ppl.payrollclass,
                          ship = ppl.ship
                      }).OrderByDescending(x => x.seniorityDate).ThenByDescending(x => x.serviceNumber).ToList();

            }
            if (apiSearchModel.sortby == "Command")
            {
                pp = (from ppl in _context.ef_personalInfos
                      //join rk in _context.ef_ranks on ppl.Rank equals rk.rankName
                      join cm in _context.ef_commands on ppl.command equals cm.code
                      where (ppl.Status == "CDR" && (apiSearchModel.command == "" || ppl.command == apiSearchModel.command))
                      select new ef_personalInfo
                      {
                          serviceNumber = ppl.serviceNumber,
                          Surname = ppl.Surname,
                          OtherName = ppl.OtherName,
                          Rank = ppl.Rank,
                          seniorityDate = ppl.seniorityDate,
                          command = cm.commandName,
                          payrollclass = ppl.payrollclass,
                          ship = ppl.ship
                      }).OrderByDescending(x => x.seniorityDate).ThenByDescending(x => x.serviceNumber).ToList();

            }
            if (apiSearchModel.sortby == "AwaitngApproval")
            {
                pp = (from ppl in _context.ef_personalInfos
                      //join rk in _context.ef_ranks on ppl.Rank equals rk.rankName
                      join cm in _context.ef_commands on ppl.command equals cm.code
                      where ((ppl.Status == "HOD"||ppl.Status=="DO") && (apiSearchModel.command == "" || ppl.command == apiSearchModel.command))
                      select new ef_personalInfo
                      {
                          serviceNumber = ppl.serviceNumber,
                          Surname = ppl.Surname,
                          OtherName = ppl.OtherName,
                          Rank = ppl.Rank,
                          seniorityDate = ppl.seniorityDate,
                          command = cm.commandName,
                          payrollclass = ppl.payrollclass,
                          ship = ppl.ship
                      }).OrderByDescending(x => x.seniorityDate).ThenByDescending(x => x.serviceNumber).ToList();

            }
            if (apiSearchModel.sortby == "Yet To Fill Form")
            {
                pp = (from ppl in _context.ef_personalInfos
                      //join rk in _context.ef_ranks on ppl.Rank equals rk.rankName
                     // join cm in _context.ef_commands on ppl.command equals cm.code
                      where (ppl.Status == null) //&& (apiSearchModel.command == "All" || ppl.command == apiSearchModel.command))
                      select new ef_personalInfo
                      {
                          serviceNumber = ppl.serviceNumber,
                          Surname = ppl.Surname,
                          OtherName = ppl.OtherName,
                          Rank = ppl.Rank,
                          seniorityDate = ppl.seniorityDate,
                          command = ppl.command,
                          payrollclass = ppl.payrollclass,
                          ship = ppl.ship
                      }).OrderByDescending(x => x.seniorityDate).ThenByDescending(x => x.serviceNumber).ToList();

            }
            return pp;
        }

        public Task<List<PersonalInfoModel>> getPersonList(int iDisplayStart, int iDisplayLength)
        {

           return (from per in _context.ef_personalInfos
                  join cm in _context.ef_commands on per.command equals cm.code
                  join bank in _context.ef_banks on per.Bankcode equals bank.bankcode
                  select new PersonalInfoModel
                  {
                      serviceNumber = per.serviceNumber,
                      Surname = per.Surname,
                      OtherName = per.OtherName,
                      Rank = per.Rank,
                      email = per.email,
                      gsm_number = per.gsm_number,
                      gsm_number2 = per.gsm_number2,
                      Birthdate = per.Birthdate,
                      DateEmpl = per.DateEmpl,
                      seniorityDate = per.seniorityDate,
                      home_address = per.home_address,
                      command = cm.commandName,
                      ship = per.ship,
                      specialisation = per.specialisation,
                      religion = per.religion,
                      MaritalStatus = per.MaritalStatus,
                      Bankcode = bank.bankname,
                      BankACNumber = per.BankACNumber,
                      bankbranch = per.bankbranch,
                          div_off_name = per.div_off_name,
                          div_off_rank = per.div_off_rank,
                          div_off_svcno = per.div_off_svcno,

                          hod_name = per.hod_name,
                          hod_rank = per.hod_rank,
                          hod_svcno = per.hod_svcno,

                          cdr_name = per.cdr_name,
                          cdr_rank = per.cdr_rank,
                          cdr_svcno = per.cdr_svcno,

                          Passport = per.Passport,
                          NokPassport = per.NokPassport,

                      }).Skip(iDisplayStart).Take(iDisplayLength).ToListAsync();
    }
        public async Task<int> getPersonListCount()
        {
            return await (from pers in _context.ef_personalInfos
                          select new PersonalInfoModel
                          {                           
                              email = pers.email,
                              Surname = pers.Surname,
                              serviceNumber = pers.serviceNumber,
                              Sex = pers.Sex,
                              Title = pers.Title,

                          }).CountAsync();
        }
        public IEnumerable<ef_personalInfo> GetUpdatedPersonnelByCPO(string payclass, string ship)
        {
                var pp = new List<ef_personalInfo>();
            if (ship == null)
            {
                pp = (from ppl in _context.ef_personalInfos
                      where (ppl.payrollclass == payclass && ppl.Status == "SHIP" && ppl.emolumentform != "Yes")
                      select new ef_personalInfo
                      {
                          serviceNumber = ppl.serviceNumber,
                          Surname = ppl.Surname,
                          OtherName = ppl.OtherName,
                          Rank = ppl.Rank,
                          seniorityDate = ppl.seniorityDate,
                          payrollclass = ppl.payrollclass,
                          ship = ppl.ship

                      }).OrderByDescending(x => x.seniorityDate).ThenByDescending(x => x.serviceNumber).ToList();


            }
            else
            {
                pp = (from ppl in _context.ef_personalInfos
                      where (ppl.payrollclass == payclass && ppl.Status == "CPO" && ppl.emolumentform != "Yes" && ppl.ship == ship)
                      select new ef_personalInfo
                      {
                          serviceNumber = ppl.serviceNumber,
                          Surname = ppl.Surname,
                          OtherName = ppl.OtherName,
                          Rank = ppl.Rank,
                          seniorityDate = ppl.seniorityDate,
                          payrollclass = ppl.payrollclass,
                          ship = ppl.ship

                      }).OrderByDescending(x => x.seniorityDate).ThenByDescending(x => x.serviceNumber).ToList();
            }
            return pp;
        }
        }
    }
