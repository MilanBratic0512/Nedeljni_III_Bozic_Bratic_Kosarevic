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

insert into tblRecept values(1, 1, 'Recepie 1', 3, 'Author 1', 'Recepie Text 1', '1-1-2020');
insert into tblRecept values(1, 2, 'Recepie 2', 4, 'Author 1', 'Recepie Text 1', '1-1-2019');
insert into tblRecept values(1, 3, 'Recepie 3', 5, 'Author 1', 'Recepie Text 1', '1-1-2018');

insert into tblComponents values (1, 'pavlaka', 3);
insert into tblComponents values (1, 'mleko', 3);
insert into tblComponents values (1, 'jaja', 3);
insert into tblComponents values (2, 'pavlaka', 3);
insert into tblComponents values (2, 'biber', 3);
insert into tblComponents values (2, 'sol', 3);
insert into tblComponents values (3, 'majonez', 3);
insert into tblComponents values (3, 'kajmak', 3);
insert into tblComponents values (3, 'kecap', 3);
