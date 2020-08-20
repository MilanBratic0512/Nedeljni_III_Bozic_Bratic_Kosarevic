CREATE PROCEDURE Get_AllReceptsByComponents
 @searchComponent nvarchar(MAX)
 as

	select  distinct(tblRecept.ReceptID), ReceptName, tblType.TypeName, PersonNumber from  tblRecept
	left join tblComponents on tblRecept.ReceptID=tblComponents.ReceptID
	left join tblType on tblRecept.TypeID=tblType.TypeID
	WHERE EXISTS (SELECT value  
    FROM STRING_SPLIT(@searchComponent, ' ')  
    WHERE 
        RTRIM(value)<>''
        and(    
        ComponentName like '%'+ value+'%' )

        )
go