CREATE TABLE [dbo].[Address] (
    [AddressKey] INT            IDENTITY (1, 1) NOT NULL,
    [FullName]   NVARCHAR (255) NOT NULL,
    [Street]     NVARCHAR (255) NULL,
    [City]       NVARCHAR (255) NULL,
    [StateCode]  NVARCHAR (255) NULL,
    [Zip]        NVARCHAR (255) NULL,
    [Country]    NVARCHAR (255) NULL,
    CONSTRAINT [IX_Address_AddressKey] PRIMARY KEY CLUSTERED ([AddressKey] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Address_FullName]
    ON [dbo].[Address]([FullName] ASC);
