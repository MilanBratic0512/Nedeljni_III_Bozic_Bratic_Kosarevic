CREATE OR ALTER PROCEDURE Get_AllRecepts 
AS
	select ReceptName, tblType.TypeName, PersonNumber from tblRecept
	left join tblType on tblRecept.TypeID=tblType.TypeID
GO

