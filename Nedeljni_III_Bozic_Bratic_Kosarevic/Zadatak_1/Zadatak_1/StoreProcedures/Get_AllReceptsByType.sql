 CREATE OR ALTER PROCEDURE  Get_AllReceptsByType
  @TypeID int
  as 
	select ReceptName, tblType.TypeName, PersonNumber from tblRecept
	left join tblType on tblRecept.TypeID=tblType.TypeID
    where  tblRecept.TypeID=@TypeID
go