CREATE OR ALTER PROCEDURE Insert_Recept
	@UserID int, @TypeID int, @ReceptName nvarchar(100), @PersonNumber int,
	@Author nvarchar(100),  @ReceptText nvarchar(400), @CreationDate date
AS
	insert into tblRecept(UserID, TypeID, ReceptName, PersonNumber, Author, ReceptText, CreationDate) 
	Values(@UserID, @TypeID, @ReceptName, @PersonNumber, @Author, @ReceptText, @CreationDate)
	select SCOPE_IDENTITY()

GO

