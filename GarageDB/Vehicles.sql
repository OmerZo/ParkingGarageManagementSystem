CREATE TABLE [dbo].[Vehicles]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NCHAR(10) NOT NULL, 
    [LicenseId] NCHAR(10) NOT NULL, 
    [Phone] NCHAR(10) NOT NULL, 
    [LotNumber] INT NOT NULL
)
