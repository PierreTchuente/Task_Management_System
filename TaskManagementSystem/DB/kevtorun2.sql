USE [TaskManagementDB]
GO

/****** Object:  StoredProcedure [dbo].[UpdatDepartmentSP]    Script Date: 01-Nov-17 4:54:45 PM ******/
DROP PROCEDURE [dbo].[UpdatDepartmentSP]
GO

/****** Object:  StoredProcedure [dbo].[UpdatDepartmentSP]    Script Date: 01-Nov-17 4:54:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Manuella Nouko
-- Create date: July 03rd 2017
-- Description:	Update Department.
-- =============================================
CREATE PROCEDURE [dbo].[UpdatDepartmentSP]
	@ID INT,
	@Desc VARCHAR(40),
	@HOD UNIQUEIDENTIFIER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	Declare @PreviousHOD UNIQUEIDENTIFIER 
	Declare @PreviousHODLevel INTEGER 
	Declare @CurrentHODLevel INTEGER 
	Declare @NumberOfDepartmentOfPreviousHOD INTEGER

	SET @PreviousHOD = (SELECT UserT.USERT_ID From UserT,Department Where (UserT.USERT_ID = Department.Dept_HOD AND Department.DEPT_ID = @ID))
	SET @PreviousHODLevel = (SELECT UserT.LEVELT_ID From UserT,Department Where (UserT.USERT_ID = Department.Dept_HOD AND Department.DEPT_ID = @ID))
	SET @CurrentHODLevel = (SELECT UserT.LEVELT_ID From UserT Where UserT.USERT_ID = @HOD)
	SET @NumberOfDepartmentOfPreviousHOD = (SELECT COUNT(Department.Dept_HOD) From Department Where Department.Dept_HOD = @PreviousHOD)
	IF (@NumberOfDepartmentOfPreviousHOD = 1)
	BEGIN
		IF (@PreviousHODLevel >= 3)
			BEGIN
				UPDATE UserT SET
				USERT.LEVELT_ID = 1004
				WHERE (UserT.USERT_ID = @PreviousHOD)
			END
	END

	IF (@CurrentHODLevel > 3)
		BEGIN
			UPDATE UserT SET
			USERT.LEVELT_ID = 3
			WHERE (UserT.USERT_ID = @HOD)
		END

    -- Insert statements for procedure here
	UPDATE Department SET
		DEPT_Desc = @DESC,
		Dept_HOD = @HOD
	WHERE( DEPT_ID = @ID)
	
	UPDATE UserT SET
	USERT.DEPT_ID = @ID
	WHERE (UserT.USERT_ID = @HOD)

	RETURN
END




GO


