-- Step 1: Drop the existing default constraint (if any) on the 'Id' column
ALTER TABLE [dbo].[ProductCustomFieldOption]
DROP CONSTRAINT IF EXISTS [DF_ProductCustomFieldOption_Id]; -- Modify this if there is a specific constraint name

-- Step 2: Alter the table to set NewSequentialId() as the default value for 'Id'
ALTER TABLE [dbo].[ProductCustomFieldOption]
ADD CONSTRAINT DF_ProductCustomFieldOption_Id DEFAULT (NewSequentialId()) FOR [Id];



---- Step 1: Drop the current primary key constraint on the 'Id' column
--ALTER TABLE [dbo].[ProductCustomFieldOption]
--DROP CONSTRAINT [PK_ProductCustomFieldOption_Id];

---- Step 2: Set NewSequentialId() as the default value for 'Id' and re-add the primary key
--ALTER TABLE [dbo].[ProductCustomFieldOption]
--ADD CONSTRAINT DF_ProductCustomFieldOption_Id DEFAULT (NewSequentialId()) FOR [Id];

---- Step 3: Re-add the primary key constraint on the 'Id' column
--ALTER TABLE [dbo].[ProductCustomFieldOption]
--ADD CONSTRAINT [PK_ProductCustomFieldOption_Id] PRIMARY KEY CLUSTERED ([Id] ASC);
