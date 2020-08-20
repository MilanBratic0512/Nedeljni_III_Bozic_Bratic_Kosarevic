CREATE OR ALTER PROCEDURE Get_AllReceptsSortedByComponentsASC
AS
select ReceptName, tblType.TypeName, PersonNumber from 
(SELECT COUNT(ComponentName)NumberOfComponents, ReceptID
FROM tblComponents group by ReceptID)CountComp
left join tblRecept on tblRecept.ReceptID=CountComp.ReceptID
left join tblType on tblRecept.TypeID=tblType.TypeID
	order by NumberOfComponents asc
GO