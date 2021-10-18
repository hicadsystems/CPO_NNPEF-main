﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NNPEFWEB.Data;

namespace NNPEFWEB.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210927211908_createsystemstable")]
    partial class createsystemstable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("NNPEFWEB.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Appointment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Command")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<byte[]>("ProfilePicture")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Rank")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<int>("UsernameChangeLimit")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("User");
                });

            modelBuilder.Entity("NNPEFWEB.Models.ef_bank", b =>
                {
                    b.Property<string>("bankcode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("bankname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("branchname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("cbn_branch")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("cbn_code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("bankcode");

                    b.ToTable("ef_banks");
                });

            modelBuilder.Entity("NNPEFWEB.Models.ef_branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("branchName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ef_branches");
                });

            modelBuilder.Entity("NNPEFWEB.Models.ef_command", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("commandName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ef_commands");
                });

            modelBuilder.Entity("NNPEFWEB.Models.ef_localgovt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.Property<string>("code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lgaName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ef_localgovts");
                });

            modelBuilder.Entity("NNPEFWEB.Models.ef_personalInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("AltNokPassport")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Anyother_Loan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Anyother_LoanYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BankACNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bankcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateEmpl")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateLeft")
                        .HasColumnType("datetime2");

                    b.Property<string>("FGSHLS_loan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FGSHLS_loanYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GBC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GBC_Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocalGovt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaritalStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NHFcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NHFcodeYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NNMFBL_loan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NNMFBL_loanYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NNNCS_loan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NNNCS_loanYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NSITFcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NSITFcodeYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("NokPassport")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("OtherName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PPCFS_loan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PPCFS_loanYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Passport")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Rank")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SBC_allow")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sex")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StateofOrigin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("accomm_type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("aircrew_allow")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("bankbranch")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("branch")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("car_loan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("car_loanYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("cdr_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("cdr_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("cdr_rank")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("cdr_svcno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("chid_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("chid_name2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("chid_name3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("chid_name4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("command")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("confirmedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("createdby")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("dateModify")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dateVerify")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dateconfirmed")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("datecreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("div_off_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("div_off_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("div_off_rank")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("div_off_svcno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("division")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("emolumentform")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("entitlement")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("entry_mode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("exittype")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("expirationOfEngagementDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("formNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gradelevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gradetype")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gsm_number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gsm_number2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("hazard_allow")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("hod_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("hod_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("hod_rank")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("hod_svcno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("home_address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nok_address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nok_address2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nok_email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nok_email2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nok_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nok_name2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nok_nationalId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nok_nationalId2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nok_phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nok_phone2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nok_relation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nok_relation2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("other_allow")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("payrollclass")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pfacode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("qualification")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("religion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("rent_subsidy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("runoutDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("seniorityDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("serviceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("shift_duty_allow")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ship")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sp_email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sp_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sp_phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sp_phone2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("special_forces_allow")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("specialisation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("taxed")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("town")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("verifyBy")
                        .HasColumnType("datetime2");

                    b.Property<string>("welfare_loan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("welfare_loanYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("yearOfPromotion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ef_personalInfos");
                });

            modelBuilder.Entity("NNPEFWEB.Models.ef_personnelLogin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("confirmPassword")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("dateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("expireDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("loginDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("otheName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("payClass")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("rank")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("surName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("svcNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ef_PersonnelLogins");
                });

            modelBuilder.Entity("NNPEFWEB.Models.ef_rank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("rankName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ef_ranks");
                });

            modelBuilder.Entity("NNPEFWEB.Models.ef_relationship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ef_relationships");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            description = "Mother"
                        },
                        new
                        {
                            Id = 2,
                            description = "Father"
                        },
                        new
                        {
                            Id = 3,
                            description = "son"
                        },
                        new
                        {
                            Id = 4,
                            description = "Daughter"
                        },
                        new
                        {
                            Id = 5,
                            description = "Brother"
                        },
                        new
                        {
                            Id = 6,
                            description = "Sister"
                        },
                        new
                        {
                            Id = 7,
                            description = "Wife"
                        });
                });

            modelBuilder.Entity("NNPEFWEB.Models.ef_ship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LandSea")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("commandid")
                        .HasColumnType("int");

                    b.Property<string>("shipName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ef_ships");
                });

            modelBuilder.Entity("NNPEFWEB.Models.ef_specialisationarea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("specName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ef_specialisationareas");
                });

            modelBuilder.Entity("NNPEFWEB.Models.ef_state", b =>
                {
                    b.Property<int>("StateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("geozoneid")
                        .HasColumnType("int");

                    b.HasKey("StateId");

                    b.ToTable("ef_states");
                });

            modelBuilder.Entity("NNPEFWEB.Models.ef_systeminfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SiteStatus")
                        .HasColumnType("int");

                    b.Property<string>("box")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("cloasedate")
                        .HasColumnType("datetime2");

                    b.Property<string>("comp_code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("comp_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("company_image")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("createdby")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("datecreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email_pword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("hrlink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("opendate")
                        .HasColumnType("datetime2");

                    b.Property<string>("serveraddr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("serverport")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("state")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("town")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ef_systeminfos");
                });

            modelBuilder.Entity("NNPEFWEB.ViewModel.UserViewModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Command")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConfirmPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rank")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserViewModel");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("NNPEFWEB.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("NNPEFWEB.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NNPEFWEB.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("NNPEFWEB.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
