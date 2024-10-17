Create table BrandMedia(
	Id uniqueidentifier not null CONSTRAINT [pk_BrandMedia_Id] PRIMARY KEY CLUSTERED,
	BrandId uniqueidentifier,
	Name nvarchar(100) not null,  
	DisplayOrder bit not null,
	Description nvarchar(max) null,
	Url nvarchar(100) not null,
	OrgId uniqueidentifier NULL,
	DomainId uniqueidentifier NULL,
	Created datetime NOT NULL default getutcdate(),
	CreatedBy nvarchar(100) NULL,
	LastUpdated datetime NOT NULL default getutcdate(),
	LastUpdatedBy nvarchar(100) NULL,
)