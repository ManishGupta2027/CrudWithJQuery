USE [CRUD]
GO

/****** Object:  Table [dbo].[ProductCustomFieldOption]    Script Date: 10/2/2024 10:27:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProductCustomFieldOption](
    Id [uniqueidentifier]  CONSTRAINT [PK_ProductCustomFieldOption_Id] PRIMARY KEY CLUSTERED,
	[CustomFieldId] [uniqueidentifier] NOT NULL,
	[FieldCode] [nvarchar](50)  NOT NULL,
	[OptionText] [nvarchar](500) NOT NULL,
	[OptionValue] [nvarchar](500) NOT NULL,
	[IsDefault] [bit]  not NULL Default 0,
	[DisplayOrder] [int] NOT NULL,
	[Orgs] [uniqueidentifier] NULL,
	[Domain] [uniqueidentifier] NULL,
	[Created] [datetime] NOT NULL DEFAULT (getutcdate()),
	[CreatedBy] [nvarchar](50) NULL,
	[LastUpdated] [datetime] NOT NULL DEFAULT (getutcdate()),
	[LastUpdatedBy] [nvarchar](50) NULL
)
