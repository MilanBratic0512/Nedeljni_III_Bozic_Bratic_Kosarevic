CREATE OR ALTER PROCEDURE Get_AllReceptsSortedByNameASC
AS
	select ReceptName, tblType.TypeName, PersonNumber from tblRecept
	left join tblType on tblRecept.TypeID=tblType.TypeID
	order by ReceptName ASC
GO