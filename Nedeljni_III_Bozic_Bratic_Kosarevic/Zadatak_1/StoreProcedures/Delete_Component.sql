CREATE OR ALTER PROCEDURE Delete_Component
@ComponentID int
as
Delete from tblComponents where ComponentID=@ComponentID
go