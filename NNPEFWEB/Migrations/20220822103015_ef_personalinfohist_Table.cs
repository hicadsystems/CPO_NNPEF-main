using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NNPEFWEB.Migrations
{
    public partial class ef_personalinfohist_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FormYear",
                table: "ef_personalInfos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "upload",
                table: "ef_personalInfos",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ef_personalInfosHist",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rankId = table.Column<int>(nullable: false),
                    formNumber = table.Column<string>(nullable: true),
                    serviceNumber = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    OtherName = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Rank = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    Passport = table.Column<byte[]>(nullable: true),
                    NokPassport = table.Column<byte[]>(nullable: true),
                    AltNokPassport = table.Column<byte[]>(nullable: true),
                    Birthdate = table.Column<DateTime>(nullable: true),
                    religion = table.Column<string>(nullable: true),
                    gsm_number = table.Column<string>(nullable: true),
                    gsm_number2 = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    home_address = table.Column<string>(nullable: true),
                    AccountName = table.Column<string>(nullable: true),
                    Bankcode = table.Column<string>(nullable: true),
                    bankbranch = table.Column<string>(nullable: true),
                    BankACNumber = table.Column<string>(nullable: true),
                    pfacode = table.Column<string>(nullable: true),
                    specialisation = table.Column<string>(nullable: true),
                    command = table.Column<string>(nullable: true),
                    branch = table.Column<string>(nullable: true),
                    ship = table.Column<string>(nullable: true),
                    exittype = table.Column<string>(nullable: true),
                    DateEmpl = table.Column<DateTime>(nullable: true),
                    advanceDate = table.Column<DateTime>(nullable: true),
                    DateLeft = table.Column<DateTime>(nullable: true),
                    seniorityDate = table.Column<DateTime>(nullable: true),
                    runoutDate = table.Column<DateTime>(nullable: true),
                    nok_phone12 = table.Column<string>(nullable: true),
                    nok_phone22 = table.Column<string>(nullable: true),
                    AddressofAcommodation = table.Column<string>(nullable: true),
                    AcommodationStatus = table.Column<string>(nullable: true),
                    yearOfPromotion = table.Column<string>(nullable: true),
                    expirationOfEngagementDate = table.Column<DateTime>(nullable: true),
                    StateofOrigin = table.Column<string>(nullable: true),
                    LocalGovt = table.Column<string>(nullable: true),
                    TaxCode = table.Column<string>(nullable: true),
                    nok_address = table.Column<string>(nullable: true),
                    nok_relation = table.Column<string>(nullable: true),
                    nok_phone = table.Column<string>(nullable: true),
                    nok_email = table.Column<string>(nullable: true),
                    nok_nationalId = table.Column<string>(nullable: true),
                    nok_name = table.Column<string>(nullable: true),
                    nok_address2 = table.Column<string>(nullable: true),
                    nok_relation2 = table.Column<string>(nullable: true),
                    nok_phone2 = table.Column<string>(nullable: true),
                    nok_email2 = table.Column<string>(nullable: true),
                    nok_nationalId2 = table.Column<string>(nullable: true),
                    nok_name2 = table.Column<string>(nullable: true),
                    sp_name = table.Column<string>(nullable: true),
                    sp_phone = table.Column<string>(nullable: true),
                    sp_phone2 = table.Column<string>(nullable: true),
                    sp_email = table.Column<string>(nullable: true),
                    chid_name = table.Column<string>(nullable: true),
                    chid_name2 = table.Column<string>(nullable: true),
                    chid_name3 = table.Column<string>(nullable: true),
                    chid_name4 = table.Column<string>(nullable: true),
                    entry_mode = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    taxed = table.Column<string>(nullable: true),
                    gradelevel = table.Column<string>(nullable: true),
                    gradetype = table.Column<string>(nullable: true),
                    entitlement = table.Column<string>(nullable: true),
                    town = table.Column<string>(nullable: true),
                    accomm_type = table.Column<string>(nullable: true),
                    GBC = table.Column<string>(nullable: true),
                    GBC_Number = table.Column<string>(nullable: true),
                    aircrew_allow = table.Column<string>(nullable: true),
                    pilot_allow = table.Column<string>(nullable: true),
                    shift_duty_allow = table.Column<string>(nullable: true),
                    hazard_allow = table.Column<string>(nullable: true),
                    rent_subsidy = table.Column<string>(nullable: true),
                    SBC_allow = table.Column<string>(nullable: true),
                    special_forces_allow = table.Column<string>(nullable: true),
                    other_allow = table.Column<string>(nullable: true),
                    other_allowspecify = table.Column<string>(nullable: true),
                    NSITFcode = table.Column<string>(nullable: true),
                    NHFcode = table.Column<string>(nullable: true),
                    FGSHLS_loan = table.Column<string>(nullable: true),
                    car_loan = table.Column<string>(nullable: true),
                    welfare_loan = table.Column<string>(nullable: true),
                    NNNCS_loan = table.Column<string>(nullable: true),
                    NNMFBL_loan = table.Column<string>(nullable: true),
                    PPCFS_loan = table.Column<string>(nullable: true),
                    Anyother_Loan = table.Column<string>(nullable: true),
                    NSITFcodeYear = table.Column<string>(nullable: true),
                    NHFcodeYear = table.Column<string>(nullable: true),
                    FGSHLS_loanYear = table.Column<string>(nullable: true),
                    car_loanYear = table.Column<string>(nullable: true),
                    welfare_loanYear = table.Column<string>(nullable: true),
                    NNNCS_loanYear = table.Column<string>(nullable: true),
                    NNMFBL_loanYear = table.Column<string>(nullable: true),
                    PPCFS_loanYear = table.Column<string>(nullable: true),
                    Anyother_LoanYear = table.Column<string>(nullable: true),
                    classes = table.Column<int>(nullable: false),
                    div_off_name = table.Column<string>(nullable: true),
                    div_off_rank = table.Column<string>(nullable: true),
                    div_off_svcno = table.Column<string>(nullable: true),
                    div_off_date = table.Column<DateTime>(nullable: true),
                    hod_name = table.Column<string>(nullable: true),
                    hod_rank = table.Column<string>(nullable: true),
                    hod_svcno = table.Column<string>(nullable: true),
                    hod_date = table.Column<DateTime>(nullable: true),
                    cdr_name = table.Column<string>(nullable: true),
                    cdr_rank = table.Column<string>(nullable: true),
                    cdr_svcno = table.Column<string>(nullable: true),
                    cdr_date = table.Column<DateTime>(nullable: true),
                    qualification = table.Column<string>(nullable: true),
                    division = table.Column<string>(nullable: true),
                    payrollclass = table.Column<string>(nullable: true),
                    emolumentform = table.Column<string>(nullable: true),
                    createdby = table.Column<string>(nullable: true),
                    datecreated = table.Column<DateTime>(nullable: true),
                    dateModify = table.Column<DateTime>(nullable: true),
                    dateVerify = table.Column<DateTime>(nullable: true),
                    verifyBy = table.Column<DateTime>(nullable: true),
                    dateconfirmed = table.Column<DateTime>(nullable: true),
                    confirmedBy = table.Column<string>(nullable: true),
                    appointment = table.Column<string>(nullable: true),
                    FormYear = table.Column<string>(nullable: true),
                    upload = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_personalInfosHist", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ef_personalInfosHist");

            migrationBuilder.DropColumn(
                name: "FormYear",
                table: "ef_personalInfos");

            migrationBuilder.DropColumn(
                name: "upload",
                table: "ef_personalInfos");
        }
    }
}
