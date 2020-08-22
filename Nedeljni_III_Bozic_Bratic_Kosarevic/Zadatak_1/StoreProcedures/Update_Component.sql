CREATE OR ALTER PROCEDURE Update_Component
@ComponentID int, @ReceptID int, @ComponentName nvarchar(50), @ComponentAmount int
AS
	update tblComponents set  
	ReceptID=@ReceptID,
	ComponentName=@ComponentName,
	ComponentAmount=@ComponentAmount
	where ComponentID=@ComponentID

GO