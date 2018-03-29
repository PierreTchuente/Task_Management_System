USE [TaskManagementDB]
GO

/****** Object:  StoredProcedure [dbo].[GetOrderShoppingCartSP]    Script Date: 11/3/2017 2:11:13 PM ******/
DROP PROCEDURE [dbo].[OrdersNotReturnedSP]
GO

/****** Object:  StoredProcedure [dbo].[GetOrderShoppingCartSP]    Script Date: 11/3/2017 2:11:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[OrdersNotReturnedSP]
	--@ID VARCHAR,
	--@TodayDate DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT
		OrderEquipment.*,
		StatusT.STATUST_Desc,
		UserT.USERT_Name AS Username,
		Equipment.EQ_Name
	FROM OrderEquipment INNER JOIN UserT ON  OrderEquipment.USERT_ID = UserT.USERT_ID,
	StatusT,
	Equipment
	WHERE( OrderEquipment.OE_IsDeleted = 0
		AND OrderEquipment.STATUST_ID = StatusT.STATUST_ID
		AND OrderEquipment.EQ_ID = Equipment.EQ_ID)
		AND OrderEquipment.STATUST_ID = 1004
	ORDER BY (OrderEquipment.OE_DateOrdered) DESC;
	
	RETURN
END


GO


