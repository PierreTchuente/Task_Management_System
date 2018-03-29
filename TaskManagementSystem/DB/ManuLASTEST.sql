USE [TaskManagementDB]
GO

/****** Object:  StoredProcedure [dbo].[UpdatDepartmentSP]    Script Date: 1/24/2018 1:45:30 PM ******/
DROP PROCEDURE [dbo].[UpdateRoleSP]
GO

/****** Object:  StoredProcedure [dbo].[UpdatDepartmentSP]    Script Date: 1/24/2018 1:45:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Manuella Nouko
-- Create date: January 24th 2018
-- Description:	Update Role.
-- =============================================
CREATE PROCEDURE [dbo].[UpdateRoleSP]
	@DeptID INT,
	@UserID UNIQUEIDENTIFIER,
	@SuperUserID UNIQUEIDENTIFIER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- First Update the HOD of the current department to SuperUser.
	UPDATE Department SET
		Dept_HOD = @SuperUserID
	WHERE (DEPT_ID = @DeptID);
	-- Then check if user is HOD in another department
	DECLARE @isAnotherHOD AS BIT;
	SET @isAnotherHOD = 1;
	DECLARE @NumberOfOtherDept AS INT;
	SET @NumberOfOtherDept = (SELECT COUNT(Dept_ID) FROM Department WHERE Dept_HOD = @UserID);
	-- If not another HOD, Set the return value to 0 to mean no and update user role.
	IF (@NumberOfOtherDept = 0)
	BEGIN
		SET @isAnotherHOD = 0;
		UPDATE UserT SET LEVELT_ID = 1004 WHERE (USERT_ID = @UserID);
	END
	
    
	RETURN @isAnotherHOD;
END




GO


