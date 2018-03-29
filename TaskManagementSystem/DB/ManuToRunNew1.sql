USE [TaskManagementDB]
GO

/****** Object:  StoredProcedure [dbo].[GetOrderShoppingCartSP]    Script Date: 11/3/2017 2:11:13 PM ******/
DROP PROCEDURE [dbo].[GetTodaysOrdersSP]
GO

/****** Object:  StoredProcedure [dbo].[GetOrderShoppingCartSP]    Script Date: 11/3/2017 2:11:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[GetTodaysOrdersSP]
	--@ID VARCHAR,
	@TodayDate DATETIME
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
		AND CAST(OrderEquipment.OE_DateOrdered AS DATE) = CAST(@TodayDate AS DATE)
		AND OrderEquipment.STATUST_ID = StatusT.STATUST_ID
		AND OrderEquipment.EQ_ID = Equipment.EQ_ID)
	ORDER BY (OrderEquipment.OE_DateOrdered) DESC;
	
	RETURN
END


GO


