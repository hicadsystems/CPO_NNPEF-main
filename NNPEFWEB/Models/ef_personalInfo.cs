using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Models
{
    public class ef_personalInfo
    {
        [Key]
        public int Id { get; set; }
        public int rankId { get; set; }
        public string formNumber { get; set; }
        public string serviceNumber { get; set; }
        public string Surname { get; set; }
        public string OtherName { get; set; }
        public string Title { get; set; }
        public string Rank { get; set; }
        public string Sex { get; set; }
        public string MaritalStatus { get; set; }
        public byte[] Passport { get; set; }
        public byte[] NokPassport { get; set; }
        public byte[] AltNokPassport { get; set; }
        public DateTime? Birthdate { get; set; }
        public string religion { get; set; }
        public string gsm_number { get; set; }
        public string gsm_number2 { get; set; }
        public string email { get; set; }
        public string home_address { get; set; }
        public string AccountName { get; set; }
        public string Bankcode { get; set; }
        public string bankbranch { get; set; }
        public string BankACNumber { get; set; }
        public string pfacode { get; set; }
        public string specialisation { get; set; }
        public string command { get; set; }
        public string branch { get; set; }
        public string ship { get; set; }
        public string exittype { get; set; }
        public DateTime? DateEmpl { get; set; }
        public DateTime? advanceDate { get; set; }
        
        public DateTime? DateLeft { get; set; }
        public DateTime? seniorityDate { get; set; }
        public DateTime? runoutDate { get; set; }
        public string nok_phone12 { get; set; }
        public string nok_phone22 { get; set; }
        public string AddressofAcommodation { get; set; }
        public string AcommodationStatus { get; set; }
        public string yearOfPromotion { get; set; }
        public DateTime? expirationOfEngagementDate { get; set; }
        public string StateofOrigin { get; set; }
        public string LocalGovt { get; set; }
        public string TaxCode { get; set; }
        public string nok_address { get; set; }
        public string nok_relation { get; set; }
        public string nok_phone { get; set; }
        public string nok_email { get; set; }
        public string nok_nationalId { get; set; }
        public string nok_name { get; set; }

        public string nok_address2 { get; set; }
        public string nok_relation2 { get; set; }
        public string nok_phone2 { get; set; }
        public string nok_email2 { get; set; }
        public string nok_nationalId2 { get; set; }
        public string nok_name2 { get; set; }

        public string sp_name { get; set; }
        public string sp_phone { get; set; }
        public string sp_phone2 { get; set; }
        public string sp_email { get; set; }
        public string chid_name { get; set; }
        public string chid_name2 { get; set; }
        public string chid_name3 { get; set; }
        public string chid_name4 { get; set; }

        public string entry_mode { get; set; }
        public string Status { get; set; }
        public string taxed { get; set; }
        public string gradelevel { get; set; }
        public string gradetype { get; set; }
        public string entitlement { get; set; }
        public string town { get; set; }
        public string accomm_type { get; set; }
        public string GBC { get; set; }
        public string GBC_Number { get; set; }
        public string aircrew_allow { get; set; }
        public string pilot_allow { get; set; }
        public string shift_duty_allow { get; set; }
        public string hazard_allow { get; set; }
        public string rent_subsidy { get; set; }
        public string SBC_allow { get; set; }
        public string special_forces_allow { get; set; }
        public string other_allow { get; set; }
        public string other_allowspecify { get; set; }
        
        public string NSITFcode { get; set; }
        public string NHFcode { get; set; }
        public string FGSHLS_loan { get; set; }
        public string car_loan { get; set; }
        public string welfare_loan { get; set; }
        public string NNNCS_loan { get; set; }
        public string NNMFBL_loan { get; set; }
        public string PPCFS_loan { get; set; }
        public string Anyother_Loan { get; set; }

        public string NSITFcodeYear { get; set; }
        public string NHFcodeYear { get; set; }
        public string FGSHLS_loanYear { get; set; }
        public string car_loanYear { get; set; }
        public string welfare_loanYear { get; set; }
        public string NNNCS_loanYear { get; set; }
        public string NNMFBL_loanYear { get; set; }
        public string PPCFS_loanYear { get; set; }
        public string Anyother_LoanYear { get; set; }
        public int classes { get;set;}
        public string div_off_name { get; set; }
            public string div_off_rank { get; set; }
            public string div_off_svcno { get; set; }
            public DateTime? div_off_date { get; set; }
            public string hod_name { get; set; }
            public string hod_rank { get; set; }
            public string hod_svcno { get; set; }
            public DateTime? hod_date { get; set; }
            public string cdr_name { get; set; }
            public string cdr_rank { get; set; }
            public string cdr_svcno { get; set; }
            public DateTime? cdr_date { get; set; }
        public string qualification { get; set; }
        public string division { get; set; }
        public string payrollclass { get; set; }
            public string emolumentform { get; set; }
            public string createdby { get; set; }
            public DateTime? datecreated { get; set; }
            public DateTime? dateModify { get; set; }
            public DateTime? dateVerify { get; set; }
            public DateTime? verifyBy { get; set; }
            public DateTime? dateconfirmed { get; set; }
            public string confirmedBy { get; set; }
        public string appointment { get; set; }

    }
}
