ALTER TABLE [dbo].[Users]
ADD RestoreCode NVARCHAR(255)
GO

ALTER TABLE [dbo].[Users]
ADD DateRestoreCodeGenerated datetime 
GO