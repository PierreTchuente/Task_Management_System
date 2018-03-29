USE [TaskManagementDB]
GO

/****** Object:  StoredProcedure [dbo].[CreateDepartmentSP]    Script Date: 01-Nov-17 4:03:28 PM ******/
DROP PROCEDURE [dbo].[CreateDepartmentSP]
GO

/****** Object:  StoredProcedure [dbo].[CreateDepartmentSP]    Script Date: 01-Nov-17 4:03:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Manuella Nouko
-- Create date: July 03rd 2017
-- Description:	Create A New Department.
-- =============================================
CREATE PROCEDURE [dbo].[CreateDepartmentSP]
	--@ID VARCHAR,
	@Desc VARCHAR(40),
	@HOD UNIQUEIDENTIFIER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @CurrentHODLevel INTEGER 
	DECLARE @ID INTEGER 

	IF OBJECT_ID('tempdb..#OutputTbl') IS NOT NULL
	DROP TABLE dbo.#OutputTbl
	
	CREATE TABLE dbo.#OutputTbl
	(
	Id int
	)


	SET @CurrentHODLevel = (SELECT UserT.LEVELT_ID From UserT Where UserT.USERT_ID = @HOD)

	IF (@CurrentHODLevel > 3)
		BEGIN
			UPDATE UserT SET
			USERT.LEVELT_ID = 3
			WHERE (UserT.USERT_ID = @HOD)
		END
	
	
    -- Insert statements for procedure here
	INSERT INTO Department(DEPT_Desc,Dept_HOD) OUTPUT inserted.DEPT_ID INTO dbo.#OutputTbl
	VALUES(
		@Desc,
		@HOD
	)
		
    SET @ID = (SELECT * From dbo.#OutputTbl)

	UPDATE UserT SET
	USERT.DEPT_ID = @ID
	WHERE (UserT.USERT_ID = @HOD)

	RETURN
END



GO


