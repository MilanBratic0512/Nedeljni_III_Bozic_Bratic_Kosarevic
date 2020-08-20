CREATE PROCEDURE Get_AllComponents
AS
	select ComponentID, ReceptID, ComponentName, ComponentAmount from tblComponents
GO