USE [CRUD]
GO
/****** Object:  StoredProcedure [dbo].[procUpsertProduct_20240420]    Script Date: 4/20/2024 7:13:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Manish
-- Create date: 11-Apr-2024
-- Description:	Insert User and update
-- =============================================
/*
-------------------------------------History----------------------
Date			Ticket			by					Comment
14-Apr-2024			01			Manish				Created
14-Apr-2024			02			Manish Gupta		Created Update Query
*/	
ALTER PROCEDURE [dbo].[procUpsertProduct_20240420] 
	(
	@id int,
	@Title nvarchar(50),
	@StockCode nvarchar(100),
	@Price decimal(10,2),
	@Category nvarchar(100),
	@Gender nvarchar(50),
	@IsActive bit
	)
AS
BEGIN
	declare @isValid bit = 0,@Message nvarchar(100)
	if (@id IS NULL)
	Begin 
	 -- Check if the stock code already exists
		IF EXISTS (SELECT 1 FROM Product WHERE StockCode = @StockCode)
			BEGIN
			  Set @Message='StockCode is Already Exist'
			END
		else 
			Begin
				insert into Product([Title], [StockCode], [Price], [Category], [Gender],[IsActive])
				Select @title,@StockCode,@Price,@Category,@Gender,@IsActive

				if @@ROWCOUNT > 0
				set @isValid =1
				Set @Message='insert success'
			End
	End
	else
	Begin
		UPDATE [Product] 
		SET Title = @Title,StockCode = @StockCode,Price = @Price,Category = @Category,Gender = @Gender,IsActive=@IsActive
		WHERE id = @Id

		if @@ROWCOUNT > 0
		set @isValid =1

		Set @Message='update success'
	End

	select @isValid isValid,@Message [Message]
END
