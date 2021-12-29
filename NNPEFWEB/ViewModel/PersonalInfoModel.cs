using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.ViewModel
{
    public class PersonalInfoModel
    {
        public string id { get; set; }
        public string formNumber { get; set; }
        public string serviceNumber { get; set; }
        [Required(ErrorMessage = "The Surname is required")]
        public string Surname { get; set; }
        public string OtherName { get; set; }
        public string Title { get; set; }
        [Required(ErrorMessage = "The Rank is required")]
        public string Rank { get; set; }
        public List<SelectListItem> rankList { get; set; }

        public byte[] Passport { get; set; }
        public string passports { get; set; }
        public byte[] NokPassport { get; set; }
        public string nokpassports { get; set; }
        public byte[] AltNokPassport { get; set; }
        public string altnokpassports { get; set; }
        public string Sex { get; set; }
        public string MaritalStatus { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name= "Birthdate")]
        public  DateTime? Birthdate { get; set; }
        public string religion { get; set; }
        [Required(ErrorMessage = "The Phone Number is required")]
        public string gsm_number { get; set; }
        public string gsm_number2 { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }
        public string home_address { get; set; }
        public string AccountName { get; set; }
        [Required(ErrorMessage = "The bank is required")]
        public string Bankcode { get; set; }
        public List<SelectListItem> bankList { get; set; }
        public string bankbranch { get; set; }
        [Required(ErrorMessage = "The Bank account number is required")]
        public string BankACNumber { get; set; }
        public string pfacode { get; set; }
        public string specialisation { get; set; }
        public List<SelectListItem> specialisationList { get; set; }
        public string command { get; set; }
        public List<SelectListItem> commandList { get; set; }
        public string branch { get; set; }
        public List<SelectListItem> branchList { get; set; }
        public string appointment { get; set; }
        public string yearOfAdvancement { get; set; }
        public string ship { get; set; }
        
        public List<SelectListItem> shipList { get; set; }
        public string qualification { get; set; }
        public string division { get; set; }
        public string category { get; set; }

        public List<SelectListItem> categoryList { get; set; }
        public string exittype { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "DateEmpl")]
        public DateTime? DateEmpl { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "runOutDate")]
        public DateTime? runOutDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "DateLeft")]
        public DateTime DateLeft { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "seniorityDate")]
        public DateTime? seniorityDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "advanceDate")]
        public DateTime? advanceDate { get; set; }
        public string yearOfPromotion { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "expirationOfEngagementDate")]
        public DateTime? expirationOfEngagementDate { get; set; }
        public string StateofOrigin { get; set; }
        public List<SelectListItem> StateofOriginList { get; set; }
        public string LocalGovt { get; set; }
        public List<SelectListItem> LocalGovtList { get; set; }
        public string TaxCode { get; set; }
        public string nok_address { get; set; }
        public string nok_relation { get; set; }
        public List<SelectListItem> nok_relationList { get; set; }
        public string nok_phone { get; set; }
        public string nok_phone12 { get; set; }
        public string nok_phone22 { get; set; }
        public string other_allowspecify { get; set; }
        public string nok_email { get; set; }
        public string nok_nationalId { get; set; }
        public string nok_name { get; set; }

        public string nok_address2 { get; set; }
        public string nok_relation2 { get; set; }
        public List<SelectListItem> nok_relation2List { get; set; }
        public string nok_phone2 { get; set; }
        public string nok_email2 { get; set; }
        public string nok_nationalId2 { get; set; }
        public string AcommodationStatus { get; set; }
        public string AddressofAcommodation { get; set; }
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
        public List<SelectListItem> entry_modeList { get; set; }
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

        public string div_off_name { get; set; }
        public string div_off_rank { get; set; }
        public string div_off_svcno { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "div_off_date")]
        public DateTime div_off_date { get; set; }
        public string hod_name { get; set; }
        public string hod_rank { get; set; }
        public string hod_svcno { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "hod_date")]
        public DateTime hod_date { get; set; }
        public string cdr_name { get; set; }
        public string cdr_rank { get; set; }
        public string cdr_svcno { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "cdr_date")]
        public DateTime cdr_date { get; set; }

        public string payrollclass { get; set; }
        public string emolumentform { get; set; }
        public string createdby { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime datecreated { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime dateModify { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime dateVerify { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime verifyBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime dateconfirmed { get; set; }
        public string confirmedBy { get; set; }
        public byte[] logo { get; set; }

    }
    public class PersonListID_Name
    {
        public string Id { get; set; }
        public string name { get; set; }
    }
}
