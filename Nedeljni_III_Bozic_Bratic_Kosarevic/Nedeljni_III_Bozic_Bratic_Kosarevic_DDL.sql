--Creating database only if database is not created yet
IF DB_ID('Nedeljni_III_Bozic_Bratic_Kosarevic_DDL') IS NULL
CREATE DATABASE Nedeljni_III_Bozic_Bratic_Kosarevic_DDL
GO
USE Nedeljni_III_Bozic_Bratic_Kosarevic_DDL;



if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblComponents')
drop table tblComponents;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblRecept')
drop table tblRecept;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblUser')
drop table tblUser;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblType')
drop table tblType;

if OBJECT_ID('vwRecept','v') IS NOT NULL DROP VIEW vwRecept;

create table tblUser (
UserID int identity(1,1) primary key,
FullName nvarchar (50) not null,
Username nvarchar (50) unique not null,
Password nvarchar (100)  not null 
)
create table tblType(
TypeID int identity(1,1) primary key,
TypeName nvarchar(100) not null
)

create table tblRecept (
ReceptID int identity(1,1) primary key,
UserID int not null,
TypeID int not null,
ReceptName nvarchar(100) not null,
PersonNumber int not null,
Author nvarchar(100) not null,
ReceptText nvarchar(400) not null,
CreationDate date not null
)

create table tblComponents(
ComponentID int identity (1,1) primary key,
ReceptID int not null,
ComponentName nvarchar(50) not null,
ComponentAmount int not null
)

Insert into tblUser values ('Admin Name','Admin',';a,u???C_????/?-myZ?Z?jc?zS?');
Insert into tblType values ('Appetizer')
Insert into tblType values ('Main course');
Insert into tblType values ('Dessert');

Alter Table tblRecept
Add foreign key (UserID) references tblUser(UserID);
Alter Table tblRecept
Add foreign key (TypeID) references tblType(TypeID);

Alter Table tblComponents
Add foreign key (ReceptID) references tblRecept(ReceptID);

insert into tblUser values('Aca', 'aca', 'A?????s?e8?B0F?]???\f?l%H?%'); --Password acasaca

insert into tblRecept values(2, 1, 'Recepie 1', 3, 'Author 1', 'Recepie Text 1', '1-1-2020');
insert into tblRecept values(2, 2, 'Recepie 2', 4, 'Author 1', 'Recepie Text 1', '1-1-2019');
insert into tblRecept values(2, 3, 'Recepie 3', 5, 'Author 1', 'Recepie Text 1', '1-1-2018');

insert into tblComponents values (1, 'pavlaka', 3);
insert into tblComponents values (1, 'mleko', 3);
insert into tblComponents values (1, 'jaja', 3);
insert into tblComponents values (2, 'pavlaka', 3);
insert into tblComponents values (2, 'djumbir', 3);
insert into tblComponents values (2, 'cevapi', 3);
insert into tblComponents values (3, 'lignja', 3);
insert into tblComponents values (3, 'skusa', 3);
insert into tblComponents values (3, 'becka', 3);

select * from tblRecept
select * from tblUser
select * from tblComponents
select * from tblType

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
