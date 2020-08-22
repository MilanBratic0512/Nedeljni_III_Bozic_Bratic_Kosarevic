CREATE or Alter PROCEDURE Get_AllComponentsByReceptId
@ReceptID int
AS
	select ComponentID, ReceptID, ComponentName, ComponentAmount from tblComponents
	where ReceptID=@ReceptID
GO
