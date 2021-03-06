USE [NNPEFDATA]
GO
/****** Object:  StoredProcedure [dbo].[DownloadForm]    Script Date: 10/4/2021 12:51:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DownloadForm]
	@svcno nvarchar(30)
AS
--BEGIN
--SELECT  * from   dbo.ef_personalInfos where Id=33333333333333333
--	SELECT        dbo.ef_personalInfos.serviceNumber, dbo.ef_personalInfos.Surname, dbo.ef_personalInfos.OtherName, dbo.ef_personalInfos.Sex, dbo.ef_personalInfos.MaritalStatus, dbo.ef_personalInfos.Birthdate, 
--                         dbo.ef_personalInfos.religion, dbo.ef_personalInfos.gsm_number, dbo.ef_personalInfos.gsm_number2, dbo.ef_personalInfos.email, dbo.ef_personalInfos.home_address, dbo.ef_personalInfos.BankACNumber, 
--                         dbo.ef_personalInfos.seniorityDate, dbo.ef_personalInfos.yearOfPromotion, dbo.ef_personalInfos.expirationOfEngagementDate, dbo.ef_personalInfos.TaxCode, dbo.ef_personalInfos.nok_address, 
--                         dbo.ef_personalInfos.nok_relation, dbo.ef_personalInfos.nok_phone, dbo.ef_personalInfos.nok_email, dbo.ef_personalInfos.nok_name, dbo.ef_personalInfos.nok_address2, dbo.ef_personalInfos.nok_relation2, 
--                         dbo.ef_personalInfos.nok_phone2, dbo.ef_personalInfos.nok_email2, dbo.ef_personalInfos.nok_nationalId2, dbo.ef_personalInfos.nok_name2, dbo.ef_personalInfos.sp_name, dbo.ef_personalInfos.sp_phone, 
--                         dbo.ef_personalInfos.sp_phone2, dbo.ef_personalInfos.sp_email, dbo.ef_personalInfos.chid_name, dbo.ef_personalInfos.chid_name2, dbo.ef_personalInfos.chid_name3, dbo.ef_personalInfos.chid_name4, 
--                         dbo.ef_personalInfos.entry_mode, dbo.ef_personalInfos.gradelevel, dbo.ef_personalInfos.taxed, dbo.ef_personalInfos.accomm_type, dbo.ef_personalInfos.GBC, dbo.ef_personalInfos.GBC_Number, 
--                         dbo.ef_personalInfos.aircrew_allow, dbo.ef_personalInfos.shift_duty_allow, dbo.ef_personalInfos.hazard_allow, dbo.ef_personalInfos.rent_subsidy, dbo.ef_personalInfos.SBC_allow, dbo.ef_personalInfos.special_forces_allow, 
--                         dbo.ef_personalInfos.other_allow, dbo.ef_personalInfos.NSITFcode, dbo.ef_personalInfos.NHFcode, dbo.ef_personalInfos.FGSHLS_loan, dbo.ef_personalInfos.car_loan, dbo.ef_personalInfos.welfare_loan, 
--                         dbo.ef_personalInfos.NNNCS_loan, dbo.ef_personalInfos.NNMFBL_loan, dbo.ef_personalInfos.div_off_rank, dbo.ef_personalInfos.div_off_name, dbo.ef_personalInfos.div_off_svcno, dbo.ef_personalInfos.div_off_date, 
--                         dbo.ef_personalInfos.hod_name, dbo.ef_personalInfos.hod_rank, dbo.ef_personalInfos.hod_svcno, dbo.ef_personalInfos.hod_date, dbo.ef_personalInfos.cdr_name, dbo.ef_personalInfos.cdr_rank, 
--                         dbo.ef_personalInfos.cdr_svcno, dbo.ef_personalInfos.cdr_date, dbo.ef_personalInfos.emolumentform, dbo.ef_personalInfos.qualification, dbo.ef_personalInfos.division, dbo.ef_personalInfos.PPCFS_loan, 
--                         dbo.ef_personalInfos.Anyother_LoanYear, dbo.ef_personalInfos.FGSHLS_loanYear, dbo.ef_personalInfos.NHFcodeYear, dbo.ef_personalInfos.NNMFBL_loanYear, dbo.ef_personalInfos.NNNCS_loanYear, 
--                         dbo.ef_personalInfos.NSITFcodeYear, dbo.ef_personalInfos.PPCFS_loanYear, dbo.ef_personalInfos.car_loanYear, dbo.ef_personalInfos.welfare_loanYear, dbo.ef_personalInfos.formNumber, dbo.ef_personalInfos.runoutDate, 
--                         dbo.ef_personalInfos.Anyother_Loan, dbo.ef_personalInfos.Passport, dbo.ef_personalInfos.NokPassport, dbo.ef_personalInfos.AltNokPassport, dbo.ef_personalInfos.confirmedBy, dbo.ef_personalInfos.dateconfirmed, 
--                         dbo.ef_personalInfos.pfacode, dbo.ef_personalInfos.DateEmpl, dbo.ef_states.Name, dbo.ef_specialisationareas.specName, dbo.ef_relationships.description, dbo.ef_ships.shipName, dbo.ef_commands.commandName, 
--                         dbo.ef_branches.branchName, dbo.ef_localgovts.lgaName, dbo.ef_banks.bankname, dbo.ef_ranks.rankName
--FROM            dbo.ef_personalInfos Left Outer JOIN
--                         dbo.ef_specialisationareas ON dbo.ef_personalInfos.Id = dbo.ef_specialisationareas.Id Left Outer JOIN
--                         dbo.ef_ships ON dbo.ef_personalInfos.Id = dbo.ef_ships.Id Left Outer JOIN
--                         dbo.ef_relationships ON dbo.ef_personalInfos.Id = dbo.ef_relationships.Id Left Outer JOIN
--                         dbo.ef_ranks ON dbo.ef_personalInfos.Id = dbo.ef_ranks.Id Left Outer JOIN
--                         dbo.ef_localgovts ON dbo.ef_personalInfos.Id = dbo.ef_localgovts.Id Left Outer JOIN
--                         dbo.ef_commands ON dbo.ef_personalInfos.Id = dbo.ef_commands.Id Left Outer JOIN
--                         dbo.ef_branches ON dbo.ef_personalInfos.Id = dbo.ef_branches.Id Left Outer JOIN
--                         dbo.ef_banks ON dbo.ef_personalInfos.Bankcode = dbo.ef_banks.bankcode Left Outer JOIN
--						  dbo.ef_states ON dbo.ef_personalInfos.StateofOrigin = dbo.ef_states.StateId 

--						  where dbo.ef_personalInfos.serviceNumber=@svcno
                     
--END
SELECT        dbo.ef_personalInfos.serviceNumber, dbo.ef_personalInfos.Surname, dbo.ef_personalInfos.OtherName, dbo.ef_personalInfos.Sex, dbo.ef_personalInfos.MaritalStatus, dbo.ef_personalInfos.Birthdate, 
                         dbo.ef_personalInfos.religion, dbo.ef_personalInfos.gsm_number, dbo.ef_personalInfos.gsm_number2, dbo.ef_personalInfos.email, dbo.ef_personalInfos.home_address, dbo.ef_personalInfos.BankACNumber, 
                         dbo.ef_personalInfos.seniorityDate, dbo.ef_personalInfos.yearOfPromotion, dbo.ef_personalInfos.expirationOfEngagementDate, dbo.ef_personalInfos.TaxCode, dbo.ef_personalInfos.nok_address, 
                         dbo.ef_personalInfos.nok_relation, dbo.ef_personalInfos.nok_phone, dbo.ef_personalInfos.nok_email, dbo.ef_personalInfos.nok_name, dbo.ef_personalInfos.nok_address2, dbo.ef_personalInfos.nok_relation2, 
                         dbo.ef_personalInfos.nok_phone2, dbo.ef_personalInfos.nok_email2, dbo.ef_personalInfos.nok_nationalId2, dbo.ef_personalInfos.nok_name2, dbo.ef_personalInfos.sp_name, dbo.ef_personalInfos.sp_phone, 
                         dbo.ef_personalInfos.sp_phone2, dbo.ef_personalInfos.sp_email, dbo.ef_personalInfos.chid_name, dbo.ef_personalInfos.chid_name2, dbo.ef_personalInfos.chid_name3, dbo.ef_personalInfos.chid_name4, 
                         dbo.ef_personalInfos.entry_mode, dbo.ef_personalInfos.gradelevel, dbo.ef_personalInfos.taxed, dbo.ef_personalInfos.accomm_type, dbo.ef_personalInfos.GBC, dbo.ef_personalInfos.GBC_Number, 
                         dbo.ef_personalInfos.aircrew_allow, dbo.ef_personalInfos.shift_duty_allow, dbo.ef_personalInfos.hazard_allow, dbo.ef_personalInfos.rent_subsidy, dbo.ef_personalInfos.SBC_allow, dbo.ef_personalInfos.special_forces_allow, 
                         dbo.ef_personalInfos.other_allow, dbo.ef_personalInfos.NSITFcode, dbo.ef_personalInfos.NHFcode, dbo.ef_personalInfos.FGSHLS_loan, dbo.ef_personalInfos.car_loan, dbo.ef_personalInfos.welfare_loan, 
                         dbo.ef_personalInfos.NNNCS_loan, dbo.ef_personalInfos.NNMFBL_loan, dbo.ef_personalInfos.div_off_rank, dbo.ef_personalInfos.div_off_name, dbo.ef_personalInfos.div_off_svcno, dbo.ef_personalInfos.div_off_date, 
                         dbo.ef_personalInfos.hod_name, dbo.ef_personalInfos.hod_rank, dbo.ef_personalInfos.hod_svcno, dbo.ef_personalInfos.hod_date, dbo.ef_personalInfos.cdr_name, dbo.ef_personalInfos.cdr_rank, 
                         dbo.ef_personalInfos.cdr_svcno, dbo.ef_personalInfos.cdr_date, dbo.ef_personalInfos.emolumentform, dbo.ef_personalInfos.qualification, dbo.ef_personalInfos.division, dbo.ef_personalInfos.PPCFS_loan, 
                         dbo.ef_personalInfos.Anyother_LoanYear, dbo.ef_personalInfos.FGSHLS_loanYear, dbo.ef_personalInfos.NHFcodeYear, dbo.ef_personalInfos.NNMFBL_loanYear, dbo.ef_personalInfos.NNNCS_loanYear, 
                         dbo.ef_personalInfos.NSITFcodeYear, dbo.ef_personalInfos.PPCFS_loanYear, dbo.ef_personalInfos.car_loanYear, dbo.ef_personalInfos.welfare_loanYear, dbo.ef_personalInfos.formNumber, dbo.ef_personalInfos.runoutDate, 
                         dbo.ef_personalInfos.Anyother_Loan, dbo.ef_personalInfos.Passport, dbo.ef_personalInfos.NokPassport, dbo.ef_personalInfos.AltNokPassport, dbo.ef_personalInfos.confirmedBy, dbo.ef_personalInfos.dateconfirmed, 
                         dbo.ef_personalInfos.pfacode, dbo.ef_personalInfos.DateEmpl, dbo.ef_states.Name, dbo.ef_specialisationareas.specName, dbo.ef_relationships.description, dbo.ef_ships.shipName, dbo.ef_commands.commandName, 
                         dbo.ef_branches.branchName, dbo.ef_localgovts.lgaName, dbo.ef_banks.bankname, dbo.ef_ranks.rankName
FROM            dbo.ef_personalInfos Left Outer JOIN
                         dbo.ef_specialisationareas ON dbo.ef_personalInfos.Id = dbo.ef_specialisationareas.Id Left Outer JOIN
                         dbo.ef_ships ON dbo.ef_personalInfos.Id = dbo.ef_ships.Id Left Outer JOIN
                         dbo.ef_relationships ON dbo.ef_personalInfos.Id = dbo.ef_relationships.Id Left Outer JOIN
                         dbo.ef_ranks ON dbo.ef_personalInfos.Id = dbo.ef_ranks.Id Left Outer JOIN
                         dbo.ef_localgovts ON dbo.ef_personalInfos.Id = dbo.ef_localgovts.Id Left Outer JOIN
                         dbo.ef_commands ON dbo.ef_personalInfos.Id = dbo.ef_commands.Id Left Outer JOIN
                         dbo.ef_branches ON dbo.ef_personalInfos.Id = dbo.ef_branches.Id Left Outer JOIN
                         dbo.ef_banks ON dbo.ef_personalInfos.Bankcode = dbo.ef_banks.bankcode Left Outer JOIN
						  dbo.ef_states ON dbo.ef_personalInfos.StateofOrigin = dbo.ef_states.StateId 

						  where dbo.ef_personalInfos.serviceNumber=@svcno
                     
GO
/****** Object:  StoredProcedure [dbo].[OpenCloseForm]    Script Date: 10/4/2021 12:51:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[OpenCloseForm]
@opendate date,
@closedate date,
@status int,
@user nvarchar(max)

AS
declare @userrole nvarchar(50),@count int
BEGIN
select * from [User]
set @userrole=(select appointment from [User] where username=@user)
	if(@userrole='Admin')
	begin
	set @count=(select count(*) from ef_systeminfos)
	if(@count>0)
	begin
	   update ef_systeminfos set opendate=@opendate,closedate=@closedate,Sitestatus=@status
	 end
	 else
	 begin
	   insert into ef_systeminfos(opendate,closedate,Sitestatus)
	         values(@opendate,@closedate,@status)
	   
	   end
	end
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_LoginInfo]    Script Date: 10/4/2021 12:51:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Sp_LoginInfo]
	 @Id nvarchar(50), 
     @Email nvarchar(50),
	 @Firstame nvarchar(50), 
	 @OtherName nvarchar(50), 
     @UserName nvarchar(50), 
     @PasswordHash nvarchar(50),
     @EmailConfirmed nvarchar(50),
	 @Phone nvarchar(12),
	 @UserRole int, 
     @OperationType nvarchar(50)
AS
BEGIN
	if(@OperationType='2')
	begin
	update [User] set EmailConfirmed=1 where UserName=@UserName
	end
	else if(@OperationType='1')
	begin
	   insert into [User](Id,Email,UserName,PasswordHash,EmailConfirmed,FirstName,LastName,UserRole,PhoneNumber)
	   values(@Id,@Email,@UserName,@PasswordHash,@EmailConfirmed,@Firstame,@OtherName,@UserRole,@Phone)
	end

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateByCommand]    Script Date: 10/4/2021 12:51:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateByCommand]
	@command nvarchar(50),
	@payclass nvarchar(50),
	@username nvarchar(50)
AS
declare @userAppoinment nvarchar(50)
BEGIN
	set @userAppoinment=(select appointment from [User] where UserName=@username)

	update ef_personalInfos set Status=@userAppoinment where Status='DO' and payrollclass=@payclass and command=@command
END
GO
/****** Object:  StoredProcedure [dbo].[UpdatePayrollEF]    Script Date: 10/4/2021 12:51:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdatePayrollEF]
	@command nvarchar(30)
AS
declare @svcno nvarchar(20)
BEGIN
  
   declare rs cursor for
   select serviceNumber from ef_personalInfos where Status='CDR' and command=@command
   open rs
   fetch next from rs into @svcno
   while @@FETCH_STATUS=0
   begin

      update ef_personalInfos set Status='CPO' where Status='CDR' and command=@command and serviceNumber=@svcno
      update HICADDATA..hr_employees set emolumentform='Yes' where Empl_ID=@svcno and command=@command

	  fetch next from rs into @svcno
   end
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateUserEmail]    Script Date: 10/4/2021 12:51:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateUserEmail]
	@email nvarchar(200)
AS
BEGIN
	Update [User] set EmailConfirmed=1 where Email=@email
END
GO
