
use MySch

/****** Script for SelectTopNRows command from SSMS  ******/
insert [MySch].[dbo].TStudent
SELECT 
[ID] = Lower(REPLACE(NEWID(), '-',''))
      ,[IDS]
      ,[Name]
      ,[CID]
      ,[PartStepIDS]
      ,[IsProblem] = 0
      ,[Mobil1]
      ,[Mobil2]
      ,[Name1]
      ,[Name2]
      ,[Home]
      ,[Birth]
      ,[Checked] = 0
      ,[CanModify] = 0
      ,[Memo] = null
      ,[AccIDS] = '32128402'
      ,[OpenID]  = null
FROM [MvcSch].[dbo].gall

--年度学生插入
insert [MySch].[dbo].TGradeStud
SELECT [ID] = Lower(REPLACE(NEWID(), '-',''))
      ,[IDS]
      ,[GradeIDS]
      ,[StudIDS]
      ,[StudCode] = null
      ,[BanIDS]
      ,[OldBan]
      ,[Choose]
      ,[ComeIDS]
      ,ComeTime
      ,[GroupID] = null
      ,[Fixed] = 0
      ,[Score] = null
      ,OutIDS = null
      ,OutTime = null
      ,InSch = 1
  FROM [MvcSch].[dbo].g2016
  
  --年度九、八年级
insert [MySch].[dbo].TGradeStud
SELECT [ID] = Lower(REPLACE(NEWID(), '-',''))
      ,[IDS]
      ,[GradeIDS]
      ,[StudIDS]
      ,[StudCode] = null
      ,[BanIDS]
      ,[OldBan]
      ,[Choose] = 0
      ,[ComeIDS]
      ,ComeTime
      ,[GroupID] = null
      ,[Fixed] = 0
      ,[Score] = null
      ,OutIDS = null
      ,OutTime = null
      ,InSch = 1
  FROM [MvcSch].[dbo].g201415
  
  
