 CREATE OR ALTER PROCEDURE  Get_AllReceptsByName
  @ReceptName nvarchar(3)
  as 
	select ReceptName, tblType.TypeName, PersonNumber from tblRecept
	left join tblType on tblRecept.TypeID=tblType.TypeID
    where  ReceptName LIKE '%'+ @ReceptName +'%'
go