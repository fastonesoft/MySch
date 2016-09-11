/****** Script for SelectTopNRows command from SSMS  ******/
insert [MySch].[dbo].TStudent
SELECT 
[ID] = Lower(REPLACE(NEWID(), '-',''))
      ,[IDS]
      ,[Name]
      ,[CID]
      ,[FromSch] = null
      ,[FromGrade] = null
      ,[NationID] = null
      ,[ReadState] = null
      ,[IsProblem] = 0
      ,[PartStepIDS]
      ,[Code]
      ,[ComeDate]
      ,[OutIDS]
      ,[OutDate]
      ,[Mobil1]
      ,[Mobil2]
      ,[Name1]
      ,[Name2]
      ,[Home]
      ,[Birth]
      ,[Memo] = null
      ,[Checked] = 0
      ,[AccIDS] = '32128402'
      ,[OpenID]  = null
  FROM [MvcSch].[dbo].gall


--年度学生插入
insert [MySch].[dbo].TGradeStud
SELECT [ID] = Lower(REPLACE(NEWID(), '-',''))
      ,[IDS]
      ,[GradeIDS]
      ,[StudIDS]
      ,[BanIDS]
      ,[OldBan]
      ,[Choose]
      ,[ComeIDS] = ReadIDS
      ,[GroupID] = null
      ,[Fixed] = 0
      ,[Score] = null
  FROM [MvcSch].[dbo].g2016
  