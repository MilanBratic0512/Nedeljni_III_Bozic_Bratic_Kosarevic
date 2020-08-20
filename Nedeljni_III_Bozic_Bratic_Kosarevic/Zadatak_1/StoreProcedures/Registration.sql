CREATE OR ALTER PROCEDURE Registration
	@FullName nvarchar(50), @Username nvarchar(50), @Password nvarchar(max) 
AS
	insert into tblUser(FullName, Username, Password) 
	Values(@FullName, @Username, @Password)
	select SCOPE_IDENTITY()
GO
