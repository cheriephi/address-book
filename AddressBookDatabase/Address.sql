CREATE TABLE [dbo].[Address] (
    [FullName]   NVARCHAR (255) NOT NULL,
    [Street]     NVARCHAR (255) NULL,
    [City]       NVARCHAR (255) NULL,
    [StateCode]  NVARCHAR (255) NULL,
    [Zip]        NVARCHAR (255) NULL,
    [Country]    NVARCHAR (255) NULL,
    CONSTRAINT [IX_Address_FullName] PRIMARY KEY CLUSTERED ([FullName] ASC)
);

