USE Nedeljni_III_Bozic_Bratic_Kosarevic_DDL;

CREATE PROCEDURE Get_AllRecepts 
AS
	select * from tblRecept
	left join tblType on tblRecept.TypeID=tblType.TypeID
GO

CREATE PROCEDURE Get_AllReceptsComponentsNumber @ReceptID int
AS
SELECT COUNT(*) NumberOfComponents FROM tblComponents c
left join tblRecept r on r.ReceptID=c.ReceptID
where c.ReceptID = @ReceptID
GO

 CREATE PROCEDURE  Get_AllReceptsByName
  @ReceptName nvarchar(50)
  as 
	select * from tblRecept
	left join tblType on tblRecept.TypeID=tblType.TypeID
    where  ReceptName LIKE '%'+ @ReceptName +'%'
go

 CREATE PROCEDURE  Get_AllReceptsByType
  @TypeName nvarchar(50)
  as 
	select * from tblRecept
	left join tblType on tblRecept.TypeID=tblType.TypeID
    where  TypeName LIKE '%'+ @TypeName +'%'
go

CREATE PROCEDURE Get_AllReceptsByComponents
 @searchComponent nvarchar(MAX)
 as
	select * from tblRecept r
	left join tblType on r.TypeID=tblType.TypeID 
	left join tblComponents on r.ReceptID=tblComponents.ReceptID
    WHERE ComponentName LIKE '%'+ @searchComponent +'%'
go

CREATE PROCEDURE Get_AllComponentsByReceptId
@ReceptID int
AS
	select ComponentID, ReceptID, ComponentName, ComponentAmount from tblComponents
	where ReceptID=@ReceptID
GO

CREATE PROCEDURE Get_AllTypes
AS
	select TypeID, TypeName  from tblType
GO

CREATE PROCEDURE Update_Component
@ComponentID int, @ReceptID int, @ComponentName nvarchar(50), @ComponentAmount int
AS
	update tblComponents set  
	ReceptID=@ReceptID,
	ComponentName=@ComponentName,
	ComponentAmount=@ComponentAmount
	where ComponentID=@ComponentID

GO

CREATE PROCEDURE Update_Recept
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

CREATE PROCEDURE Delete_Component
@ComponentID int
as
Delete from tblComponents where ComponentID=@ComponentID
go

CREATE PROCEDURE Insert_Recept
	@UserID int, @TypeID int, @ReceptName nvarchar(100), @PersonNumber int, @Author nvarchar(100),  @ReceptText nvarchar(400), @CreationDate date
AS
	insert into tblRecept(UserID, TypeID, ReceptName, PersonNumber, Author, ReceptText, CreationDate) 
	Values(@UserID, @TypeID, @ReceptName, @PersonNumber, @Author, @ReceptText, @CreationDate)
	select SCOPE_IDENTITY()
GO

CREATE  PROCEDURE Insert_Components
	@ReceptID int, @ComponentName nvarchar(50), @ComponentAmount int
AS
	insert into tblComponents(ReceptID, ComponentName, ComponentAmount) 
	Values(@ReceptID, @ComponentName, @ComponentAmount)
	select SCOPE_IDENTITY()
GO

CREATE PROCEDURE Get_AllComponents
AS
	select ComponentID, ReceptID, ComponentName, ComponentAmount from tblComponents
GO

CREATE PROCEDURE Get_AllComponentsByInput
@searchComponent nvarchar(MAX)
AS
	select * from tblComponents
	where  ComponentName LIKE '%'+ @searchComponent +'%'
GO