TRUNCATE TABLE dbo.[Address];

SET IDENTITY_INSERT [dbo].[Address] ON
INSERT INTO [dbo].[Address] ([AddressKey], [FullName], [Street], [City], [StateCode], [Zip], [Country]) VALUES (1, N'Joe Bloggs', N'1 New St.', N'Birmingham', N'England', N'B01 3TN', N'UK')
INSERT INTO [dbo].[Address] ([AddressKey], [FullName], [Street], [City], [StateCode], [Zip], [Country]) VALUES (2, N'John Doe', N'16 S 31st St.', N'Boulder', N'CO', N'80304', N'USA')
INSERT INTO [dbo].[Address] ([AddressKey], [FullName], [Street], [City], [StateCode], [Zip], [Country]) VALUES (3, N'Brent Leroy', N'Corner Gas', N'Dog River', N'SK', N'S0G 4H0', N'CANADA')
SET IDENTITY_INSERT [dbo].[Address] OFF
