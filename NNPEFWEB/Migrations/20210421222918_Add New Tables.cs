using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NNPEFWEB.Migrations
{
    public partial class AddNewTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ef_banks",
                columns: table => new
                {
                    bankcode = table.Column<string>(nullable: false),
                    bankname = table.Column<string>(nullable: true),
                    branchname = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    cbn_code = table.Column<string>(nullable: true),
                    cbn_branch = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_banks", x => x.bankcode);
                });

            migrationBuilder.CreateTable(
                name: "ef_branches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(nullable: true),
                    branchName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ef_commands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(nullable: true),
                    commandName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_commands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ef_localgovts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(nullable: true),
                    lgaName = table.Column<string>(nullable: true),
                    StateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_localgovts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ef_personalInfos",
                columns: table => new
                {
                    serviceNumber = table.Column<string>(nullable: false),
                    Surname = table.Column<string>(nullable: true),
                    OtherName = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Rank = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    Birthdate = table.Column<DateTime>(nullable: false),
                    religion = table.Column<string>(nullable: true),
                    gsm_number = table.Column<string>(nullable: true),
                    gsm_number2 = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    home_address = table.Column<string>(nullable: true),
                    Bankcode = table.Column<string>(nullable: true),
                    bankbranch = table.Column<string>(nullable: true),
                    BankACNumber = table.Column<string>(nullable: true),
                    pfacode = table.Column<string>(nullable: true),
                    specialisation = table.Column<string>(nullable: true),
                    command = table.Column<string>(nullable: true),
                    branch = table.Column<string>(nullable: true),
                    ship = table.Column<string>(nullable: true),
                    exittype = table.Column<string>(nullable: true),
                    DateEmpl = table.Column<DateTime>(nullable: false),
                    DateLeft = table.Column<DateTime>(nullable: false),
                    seniorityDate = table.Column<DateTime>(nullable: false),
                    yearOfPromotion = table.Column<string>(nullable: true),
                    expirationOfEngagementDate = table.Column<DateTime>(nullable: false),
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
                    shift_duty_allow = table.Column<string>(nullable: true),
                    hazard_allow = table.Column<string>(nullable: true),
                    rent_subsidy = table.Column<string>(nullable: true),
                    SBC_allow = table.Column<string>(nullable: true),
                    special_forces_allow = table.Column<string>(nullable: true),
                    other_allow = table.Column<string>(nullable: true),
                    NSITFcode = table.Column<string>(nullable: true),
                    NHFcode = table.Column<string>(nullable: true),
                    FGSHLS_loan = table.Column<string>(nullable: true),
                    car_loan = table.Column<string>(nullable: true),
                    welfare_loan = table.Column<string>(nullable: true),
                    NNNCS_loan = table.Column<string>(nullable: true),
                    NNMFBL_loan = table.Column<string>(nullable: true),
                    div_off_name = table.Column<string>(nullable: true),
                    div_off_rank = table.Column<string>(nullable: true),
                    div_off_svcno = table.Column<string>(nullable: true),
                    div_off_date = table.Column<DateTime>(nullable: false),
                    hod_name = table.Column<string>(nullable: true),
                    hod_rank = table.Column<string>(nullable: true),
                    hod_svcno = table.Column<string>(nullable: true),
                    hod_date = table.Column<DateTime>(nullable: false),
                    cdr_name = table.Column<string>(nullable: true),
                    cdr_rank = table.Column<string>(nullable: true),
                    cdr_svcno = table.Column<string>(nullable: true),
                    cdr_date = table.Column<DateTime>(nullable: false),
                    payrollclass = table.Column<string>(nullable: true),
                    emolumentform = table.Column<string>(nullable: true),
                    createdby = table.Column<string>(nullable: true),
                    datecreated = table.Column<DateTime>(nullable: false),
                    dateModify = table.Column<DateTime>(nullable: false),
                    dateVerify = table.Column<DateTime>(nullable: false),
                    verifyBy = table.Column<DateTime>(nullable: false),
                    dateconfirmed = table.Column<DateTime>(nullable: false),
                    confirmedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_personalInfos", x => x.serviceNumber);
                });

            migrationBuilder.CreateTable(
                name: "ef_ranks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rankName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_ranks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ef_ships",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(nullable: true),
                    shipName = table.Column<string>(nullable: true),
                    LandSea = table.Column<string>(nullable: true),
                    commandid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_ships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ef_specialisationareas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    specName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_specialisationareas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ef_states",
                columns: table => new
                {
                    StateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    geozoneid = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ef_states", x => x.StateId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ef_banks");

            migrationBuilder.DropTable(
                name: "ef_branches");

            migrationBuilder.DropTable(
                name: "ef_commands");

            migrationBuilder.DropTable(
                name: "ef_localgovts");

            migrationBuilder.DropTable(
                name: "ef_personalInfos");

            migrationBuilder.DropTable(
                name: "ef_ranks");

            migrationBuilder.DropTable(
                name: "ef_ships");

            migrationBuilder.DropTable(
                name: "ef_specialisationareas");

            migrationBuilder.DropTable(
                name: "ef_states");
        }
    }
}
