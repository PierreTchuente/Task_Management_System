USE [TaskManagementDB]
GO

/****** Object:  StoredProcedure [dbo].[GetUserTSP]    Script Date: 01-Nov-17 7:38:25 PM ******/
DROP PROCEDURE [dbo].[GetUserDetails]
GO

/****** Object:  StoredProcedure [dbo].[GetUserTSP]    Script Date: 01-Nov-17 7:38:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetUserDetails]
	@ID UNIQUEIDENTIFIER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
    -- Insert statements for procedure here
SELECT        Gender.GENDER_Desc, UserT.USERT_ID, UserT.USERT_Name, UserT.USERT_Username, UserT.LEVELT_ID, LevelT.LEVELT_Desc, UserT.USERT_Email, UserT.USERT_PhotoLink, UserT.USERT_Registered_Time, 
                         UserT.USERT_OnlineStatus, UserT.USERT_PhoneNumber, UserT.GENDER_ID, UserT.USERT_DOB, UserT.USERT_Address, UserT.DEPT_ID, UserT.USERT_MemberStatus, UserT.USERT_IsActive, Department.DEPT_Desc
FROM            UserT INNER JOIN
                         LevelT ON UserT.LEVELT_ID = LevelT.LEVELT_ID INNER JOIN
                         Department ON UserT.DEPT_ID = Department.DEPT_ID INNER JOIN
                         Gender ON UserT.GENDER_ID = Gender.GENDER_ID
	WHERE( USERT_ID = @ID)
	
	RETURN
END

GO


