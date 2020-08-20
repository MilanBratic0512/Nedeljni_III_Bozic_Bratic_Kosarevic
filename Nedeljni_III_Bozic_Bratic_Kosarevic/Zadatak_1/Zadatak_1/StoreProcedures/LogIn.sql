CREATE OR ALTER PROCEDURE LogIn
	@Username nvarchar(50), @Password nvarchar(max) 
AS
	select UserID, FullName, Username from tblUser 
	where Username=@Username and Password=@Password

GO