CREATE OR ALTER PROCEDURE Insert_Components
	@ReceptID int, @ComponentName nvarchar(50), @ComponentAmount int
AS
	insert into tblComponents(ReceptID, ComponentName, ComponentAmount) 
	Values(@ReceptID, @ComponentName, @ComponentAmount)
	select SCOPE_IDENTITY()
GO