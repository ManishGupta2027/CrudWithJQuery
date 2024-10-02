Use CRUd
GO
-- =============================================
-- Author:        Avinash
-- Create date:   02-Oct-2024
-- Description:   Check Empty Guid 
-- =============================================
/*
Date		Ticket			By					Comments
0000		0000			Avinash				Created

*/
CREATE FUNCTION dbo.funCheckGUID (
    @Id UNIQUEIDENTIFIER
)
RETURNS UNIQUEIDENTIFIER
AS
BEGIN
    -- Check if @Id is NULL or Empty GUID and return NULL
    IF @Id IS NULL OR @Id = '00000000-0000-0000-0000-000000000000'
    BEGIN
        RETURN NULL
    END
    -- Return the original @Id if it's not NULL or Empty GUID
    RETURN @Id
END
GO
