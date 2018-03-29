USE [TaskManagementDB]
GO

/****** Object:  StoredProcedure [dbo].[UpdateEquipmentStatusSP]    Script Date: 10/30/2017 1:24:53 PM ******/
DROP PROCEDURE [dbo].[UpdateEquipmentStatusSP]
GO

/****** Object:  StoredProcedure [dbo].[UpdateEquipmentStatusSP]    Script Date: 10/30/2017 1:24:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[UpdateEquipmentStatusSP]
	--@ID VARCHAR,
	@EqID INT,
	@StatusID INT,
	@OEID UNIQUEIDENTIFIER,
	@Time DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @CurrentStatus INT;
	SET @CurrentStatus = (SELECT STATUST_ID FROM OrderEquipment WHERE (OE_ID = @OEID));
	IF (((@CurrentStatus = 1004) AND (@StatusID = 1003)) OR ((@CurrentStatus = 1002) AND (@StatusID = 1004)))
	BEGIN
		IF (@StatusID = 1003) -- i.e. OrderReturned
		BEGIN
			DECLARE @Qty INT;
			SET @Qty = (SELECT OE_QuantityOrdered FROM OrderEquipment WHERE (OE_ID = @OEID));
			-- Update Equipment; add back what was out
			

			UPDATE Equipment SET
			EQ_QuantityOrdered = EQ_QuantityOrdered - @Qty
			WHERE (EQ_ID = @EqID);
			-- Update OrderEquipment
			UPDATE OrderEquipment SET
			OE_IsReturned = 1,
			OE_Return_Time = @Time,
			STATUST_ID = @StatusID
			WHERE (OE_ID = @OEID);
		END
		IF (@StatusID = 1004) -- i.e. OrderOut
		BEGIN
			-- Update OrderEquipment
			UPDATE OrderEquipment SET
			OE_Out_Time = @Time,
			STATUST_ID = @StatusID
			WHERE (OE_ID = @OEID);

			----Update the quantity in Equipement table
			DECLARE @Qty2 INT;
			SET @Qty2 = (SELECT OE_QuantityOrdered FROM OrderEquipment WHERE (OE_ID = @OEID));

			UPDATE Equipment SET
				EQ_QuantityOrdered = EQ_QuantityOrdered + @Qty2
			WHERE (EQ_ID = @EqID);
		END
	END	
	
END



GO


