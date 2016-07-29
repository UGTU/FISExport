--Заполнение конкурсных групп
DECLARE @year INT
SET @year=2016
insert into [FIS].[Abit_CompetitiveGroup]([UID_Campaign]
           ,[Name]
           ,[id_educLevel]
           ,[id_form]
           ,[id_eduSource]
           ,[id_specFIS]
           ,[IsForKrim]
           ,[IsAdditional])

SELECT DISTINCT Abit_Campaign.UID UID_Campaign, [FIS].[SpecDetailDictionary].[Name]+' '+Form_ed.Cname_form_ed+' '+TypeDirection.NameTypeDirection+' c оплатой обучения' Name, 
	FISeducLevel.id id_educLevel, FISFormEd.id id_form,  
	57158 id_eduSource --контрактники
	, [FIS].[SpecDetailDictionary].id id_specFIS
	,0 IsForKrim,0 IsAdditional    
FROM            
    dbo.ABIT_Diapazon_spec_fac INNER JOIN
    dbo.Relation_spec_fac ON dbo.ABIT_Diapazon_spec_fac.ik_spec_fac = dbo.Relation_spec_fac.ik_spec_fac INNER JOIN
    dbo.EducationBranch ON dbo.Relation_spec_fac.ik_spec = dbo.EducationBranch.ik_spec INNER JOIN
    dbo.Direction ON dbo.EducationBranch.ik_direction = dbo.Direction.ik_direction INNER JOIN
    dbo.TypeDirection ON dbo.Direction.id_type_direction = dbo.TypeDirection.id_type_direction  INNER JOIN
    dbo.Form_ed ON dbo.Relation_spec_fac.Ik_form_ed = dbo.Form_ed.Ik_form_ed

	INNER JOIN fis.DictionaryContent FISTypePK ON FISTypePK.IDItem=TypeDirection.ik_FIS_TypePK AND FISTypePK.DictionaryCode=38
	INNER JOIN fis.Abit_Campaign ON FISTypePK.id=Abit_Campaign.ik_FIS_TypePK AND Abit_Campaign.YearFrom=@year
	INNER JOIN fis.DictionaryContent FISeducLevel ON FISeducLevel.IDItem=Direction.ik_FB AND FISeducLevel.DictionaryCode=2
	INNER JOIN fis.DictionaryContent FISFormEd ON FISFormEd.IDItem=Form_ed.ik_FB AND FISFormEd.DictionaryCode=14
	inner join [FIS].[SpecDetailDictionary] on EducationBranch.ik_FB = [FIS].[SpecDetailDictionary].ID
WHERE ABIT_Diapazon_spec_fac.NNyear=@year

 
UNION 
SELECT DISTINCT Abit_Campaign.UID UID_Campaign, [FIS].[SpecDetailDictionary].[Name]+' '+Form_ed.Cname_form_ed+' '+TypeDirection.NameTypeDirection+' Бюджетные места' Name, 
	FISeducLevel.id id_educLevel, FISFormEd.id id_form,  
	57156 id_eduSource --Бюджетные места
	, [FIS].[SpecDetailDictionary].id id_specFIS
	,0 IsForKrim,0 IsAdditional    
FROM            
    dbo.ABIT_Diapazon_spec_fac INNER JOIN
    dbo.Relation_spec_fac ON dbo.ABIT_Diapazon_spec_fac.ik_spec_fac = dbo.Relation_spec_fac.ik_spec_fac INNER JOIN
    dbo.EducationBranch ON dbo.Relation_spec_fac.ik_spec = dbo.EducationBranch.ik_spec INNER JOIN
    dbo.Direction ON dbo.EducationBranch.ik_direction = dbo.Direction.ik_direction INNER JOIN
    dbo.TypeDirection ON dbo.Direction.id_type_direction = dbo.TypeDirection.id_type_direction  INNER JOIN
    dbo.Form_ed ON dbo.Relation_spec_fac.Ik_form_ed = dbo.Form_ed.Ik_form_ed

	INNER JOIN fis.DictionaryContent FISTypePK ON FISTypePK.IDItem=TypeDirection.ik_FIS_TypePK AND FISTypePK.DictionaryCode=38
	INNER JOIN fis.Abit_Campaign ON FISTypePK.id=Abit_Campaign.ik_FIS_TypePK AND Abit_Campaign.YearFrom=@year
	INNER JOIN fis.DictionaryContent FISeducLevel ON FISeducLevel.IDItem=Direction.ik_FB AND FISeducLevel.DictionaryCode=2
	INNER JOIN fis.DictionaryContent FISFormEd ON FISFormEd.IDItem=Form_ed.ik_FB AND FISFormEd.DictionaryCode=14
	inner join [FIS].[SpecDetailDictionary] on EducationBranch.ik_FB = [FIS].[SpecDetailDictionary].ID
WHERE ABIT_Diapazon_spec_fac.NNyear=@year 
AND [MestBudjet]>0

 
UNION 
SELECT DISTINCT Abit_Campaign.UID UID_Campaign, [FIS].[SpecDetailDictionary].[Name]+' '+Form_ed.Cname_form_ed+' '+TypeDirection.NameTypeDirection+' Целевой прием' Name, 
	FISeducLevel.id id_educLevel, FISFormEd.id id_form,  
	57159 id_eduSource --Целевой прием
	, [FIS].[SpecDetailDictionary].id id_specFIS
	,0 IsForKrim,0 IsAdditional    
FROM            
    dbo.ABIT_Diapazon_spec_fac INNER JOIN
    dbo.Relation_spec_fac ON dbo.ABIT_Diapazon_spec_fac.ik_spec_fac = dbo.Relation_spec_fac.ik_spec_fac INNER JOIN
    dbo.EducationBranch ON dbo.Relation_spec_fac.ik_spec = dbo.EducationBranch.ik_spec INNER JOIN
    dbo.Direction ON dbo.EducationBranch.ik_direction = dbo.Direction.ik_direction INNER JOIN
    dbo.TypeDirection ON dbo.Direction.id_type_direction = dbo.TypeDirection.id_type_direction  INNER JOIN
    dbo.Form_ed ON dbo.Relation_spec_fac.Ik_form_ed = dbo.Form_ed.Ik_form_ed

	INNER JOIN fis.DictionaryContent FISTypePK ON FISTypePK.IDItem=TypeDirection.ik_FIS_TypePK AND FISTypePK.DictionaryCode=38
	INNER JOIN fis.Abit_Campaign ON FISTypePK.id=Abit_Campaign.ik_FIS_TypePK AND Abit_Campaign.YearFrom=@year
	INNER JOIN fis.DictionaryContent FISeducLevel ON FISeducLevel.IDItem=Direction.ik_FB AND FISeducLevel.DictionaryCode=2
	INNER JOIN fis.DictionaryContent FISFormEd ON FISFormEd.IDItem=Form_ed.ik_FB AND FISFormEd.DictionaryCode=14
	inner join [FIS].[SpecDetailDictionary] on EducationBranch.ik_FB = [FIS].[SpecDetailDictionary].ID
WHERE ABIT_Diapazon_spec_fac.NNyear=@year 
AND [MestCKP]>0

 
UNION 
SELECT DISTINCT Abit_Campaign.UID UID_Campaign, [FIS].[SpecDetailDictionary].[Name]+' '+Form_ed.Cname_form_ed+' '+TypeDirection.NameTypeDirection+' Квота приема лиц, имеющих особое право' Name, 
	FISeducLevel.id id_educLevel, FISFormEd.id id_form,  
	57157 id_eduSource --Квота приема лиц, имеющих особое право
	, [FIS].[SpecDetailDictionary].id id_specFIS
	,0 IsForKrim,0 IsAdditional    
FROM            
    dbo.ABIT_Diapazon_spec_fac INNER JOIN
    dbo.Relation_spec_fac ON dbo.ABIT_Diapazon_spec_fac.ik_spec_fac = dbo.Relation_spec_fac.ik_spec_fac INNER JOIN
    dbo.EducationBranch ON dbo.Relation_spec_fac.ik_spec = dbo.EducationBranch.ik_spec INNER JOIN
    dbo.Direction ON dbo.EducationBranch.ik_direction = dbo.Direction.ik_direction INNER JOIN
    dbo.TypeDirection ON dbo.Direction.id_type_direction = dbo.TypeDirection.id_type_direction  INNER JOIN
    dbo.Form_ed ON dbo.Relation_spec_fac.Ik_form_ed = dbo.Form_ed.Ik_form_ed

	INNER JOIN fis.DictionaryContent FISTypePK ON FISTypePK.IDItem=TypeDirection.ik_FIS_TypePK AND FISTypePK.DictionaryCode=38
	INNER JOIN fis.Abit_Campaign ON FISTypePK.id=Abit_Campaign.ik_FIS_TypePK AND Abit_Campaign.YearFrom=@year
	INNER JOIN fis.DictionaryContent FISeducLevel ON FISeducLevel.IDItem=Direction.ik_FB AND FISeducLevel.DictionaryCode=2
	INNER JOIN fis.DictionaryContent FISFormEd ON FISFormEd.IDItem=Form_ed.ik_FB AND FISFormEd.DictionaryCode=14
	inner join [FIS].[SpecDetailDictionary] on EducationBranch.ik_FB = [FIS].[SpecDetailDictionary].ID
	WHERE ABIT_Diapazon_spec_fac.NNyear=@year 
AND [MestLgot]>0