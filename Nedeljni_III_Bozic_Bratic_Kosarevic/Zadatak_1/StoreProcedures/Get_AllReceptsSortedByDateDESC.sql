CREATE OR ALTER PROCEDURE Get_AllReceptsSortedByDateDESC 
AS
	select ReceptName, tblType.TypeName, PersonNumber from tblRecept
	left join tblType on tblRecept.TypeID=tblType.TypeID
	order by CreationDate DESC
GO