-- Drop the 'userInfo' table if it exists
IF OBJECT_ID('dbo.userInfo', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.userInfo;
END
GO
-- Drop the 'users' table if it exists
IF OBJECT_ID('dbo.users', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.users;
END
GO

-- Create the 'users' table if it does not exist
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'users')
BEGIN
    CREATE TABLE [dbo].[users] (
        [id]       INT             IDENTITY (1, 1) NOT NULL,
        [username] VARCHAR (255)   NOT NULL UNIQUE,
        [password] NVARCHAR (255)  NOT NULL,
        [keys]     VARBINARY (MAX) NOT NULL,
        [created]  DATETIME        DEFAULT GETDATE(),
        PRIMARY KEY CLUSTERED ([id] ASC)
    );
END
GO

-- Create the 'userInfo' table if it does not exist
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'userInfo')
BEGIN
    CREATE TABLE [dbo].[userInfo] (
        [id]     INT           NOT NULL,
        [name]       VARCHAR (255) NOT NULL,
        [email]      VARCHAR (255) NOT NULL UNIQUE,
        [last_login] DATETIME      DEFAULT GETDATE(),
        FOREIGN KEY ([id]) REFERENCES [dbo].[users] ([id])
    );
END
GO

-- Reset the identity seed for the 'users' table to 1
DBCC CHECKIDENT ('dbo.users', RESEED, 1);
GO