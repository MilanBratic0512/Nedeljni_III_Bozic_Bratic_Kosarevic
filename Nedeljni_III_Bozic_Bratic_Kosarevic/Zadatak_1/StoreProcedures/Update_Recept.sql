CREATE OR ALTER PROCEDURE Update_Recept
	@ReceptID int, @UserID int, @TypeID int, @ReceptName nvarchar(100), @PersonNumber int, 
	@Author nvarchar(100),  @ReceptText nvarchar(400),  @CreationDate date
AS
	update tblRecept set  
	UserID=@UserID,
	TypeID=@TypeID,
	ReceptName=@ReceptName,
	PersonNumber=@PersonNumber,
	Author=@Author,
	ReceptText=@ReceptText,
	CreationDate=@CreationDate
	where ReceptID=@ReceptID

GO