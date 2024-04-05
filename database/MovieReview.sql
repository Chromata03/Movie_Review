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

-- Create the 'users' table
CREATE TABLE [dbo].[users] (
    [id]       INT             IDENTITY (1, 1) NOT NULL,
    [username] VARCHAR (255)   NULL,
    [password] NVARCHAR (255)  NULL,
    [keys]     VARBINARY (MAX) NULL,
    [created]  DATETIME        NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

-- Create the 'userInfo' table
CREATE TABLE [dbo].[userInfo] (
    [userId]     INT           NULL,
    [name]       VARCHAR (255) NULL,
    [email]      VARCHAR (255) NULL,
    [last_login] DATETIME      NULL,
    FOREIGN KEY ([userId]) REFERENCES [dbo].[users] ([id])
);

-- Reset the identity seed for the 'users' table to 1
DBCC CHECKIDENT ('dbo.users', RESEED, 1);
GO