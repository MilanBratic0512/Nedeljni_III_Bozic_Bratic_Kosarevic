CREATE OR ALTER PROCEDURE Get_AllReceptsSortedByNameDESC
AS
	select ReceptName, tblType.TypeName, PersonNumber from tblRecept
	left join tblType on tblRecept.TypeID=tblType.TypeID
	order by ReceptName DESC
GO