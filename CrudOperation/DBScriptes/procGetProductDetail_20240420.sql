USE [CRUD]
GO
Drop Proc procGetProductDetail_20042024
/****** Object:  StoredProcedure [dbo].[procGetProductDetail_19042024]    Script Date: 4/20/2024 11:31:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Manish Gupta
-- Create date: 14-Apr-2024
-- Description:	GetProductDetail
-- =============================================
--Exec procGetProductDetail_20240420 8
CREATE PROCEDURE [dbo].[procGetProductDetail_20240420]
@id int
AS
BEGIN
	select [Title], [StockCode],[Price],[Category],[id],[Gender],[IsActive]
	from [Product]
	where id=@id
END

