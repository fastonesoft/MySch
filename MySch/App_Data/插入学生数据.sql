
use MySch

/****** Script for SelectTopNRows command from SSMS  ******/
insert [MySch].[dbo].TStudent
SELECT 
[ID] = Lower(REPLACE(NEWID(), '-',''))
      ,[IDS]
      ,[Name]
      ,[IDC]
      ,[StepIDS] = StepIDS
      ,[IsProblem] = 0
      ,[Mobil1]
      ,[Mobil2]
      ,[Name1]
      ,[Name2]
      ,[Home]
      ,[Birth]
      ,[Checked] = 0
      ,[Fixed] = 0
      ,[Memo] = null
      ,[AccIDS] = '32128402'
      ,[OpenID]  = null
FROM [MvcSch].[dbo].gall

--年度学生插入
insert [MySch].[dbo].TGradeStud
SELECT [ID] = Lower(REPLACE(NEWID(), '-',''))
      ,[IDS]
      ,[GradeIDS]
      ,[BanIDS]
      ,[OldBan]
      ,[StudIDS]
      ,[StudCode] = Code
      ,[Choose]
      ,[ComeIDS]
      ,[ComeTime]
      ,[GroupID] = null
      ,[Fixed] = 0
      ,[Score] = null
      ,[OutIDS] = null
      ,[OutTime] = null
      ,[InSch] = 1
  FROM [MvcSch].[dbo].g2016
  
  --年度九、八年级（择校生状态要重核）
insert [MySch].[dbo].TGradeStud
SELECT [ID] = Lower(REPLACE(NEWID(), '-',''))
      ,[IDS]
      ,[GradeIDS]
      ,[BanIDS]
      ,[OldBan]
      ,[StudIDS]
      ,[StudCode] = null
      ,[Choose] = 1
      ,[ComeIDS]
      ,[ComeTime]
      ,[GroupID] = null
      ,[Fixed] = 0
      ,[Score] = null
      ,[OutIDS] = null
      ,[OutTime] = null
      ,[InSch] = 1
  FROM [MvcSch].[dbo].g201415
  


---新的，直接导入数据
insert MySch.dbo.TStudent
SELECT [ID]
      ,[IDS]
      ,[Name]
      ,[IDC]
      ,[StepIDS] = [PartStepIDS]
      ,[IsProblem]
      ,[Mobil1]
      ,[Mobil2]
      ,[Name1]
      ,[Name2]
      ,[Home]
      ,[Birth]
      ,[Checked]
      ,[CanModify] = 1
      ,[Memo]
      ,[AccIDS]
      ,[OpenID]
  FROM [MvcSch].[dbo].[TStudent]
  

insert [MySch].[dbo].[TGradeStud]
SELECT  [ID]
      ,[IDS]
      ,[GradeIDS]
      ,[BanIDS]
      ,[OldBan]
      ,[StudIDS]
      ,[StudCode]
      ,[Choose]
      ,[ComeIDS]
      ,[ComeTime]
      ,[GroupID]
      ,[Fixed]
      ,[Score]
      ,[OutIDS]
      ,[OutTime]
      ,[InSch]
  FROM [MvcSch].[dbo].[TGradeStud]


  --班级
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010701', '01', '321284020120160107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010702', '02', '321284020120160107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010703', '03', '321284020120160107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010704', '04', '321284020120160107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010705', '05', '321284020120160107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010706', '06', '321284020120160107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010707', '07', '321284020120160107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010708', '08', '321284020120160107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010709', '09', '321284020120160107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010710', '10', '321284020120160107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010711', '11', '321284020120160107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010712', '12', '321284020120160107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010713', '13', '321284020120160107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010714', '14', '321284020120160107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010715', '15', '321284020120160107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010716', '16', '321284020120160107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010717', '17', '321284020120160107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010718', '18', '321284020120160107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010719', '19', '321284020120160107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010720', '20', '321284020120160107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010721', '21', '321284020120160107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010722', '22', '321284020120160107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010723', '23', '321284020120160107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010724', '24', '321284020120160107', null, 0, 1, 10, 5, 0, 1)
--2015		    
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010801', '01', '321284020120150108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010802', '02', '321284020120150108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010803', '03', '321284020120150108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010804', '04', '321284020120150108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010805', '05', '321284020120150108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010806', '06', '321284020120150108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010807', '07', '321284020120150108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010808', '08', '321284020120150108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010809', '09', '321284020120150108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010810', '10', '321284020120150108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010811', '11', '321284020120150108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010812', '12', '321284020120150108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010813', '13', '321284020120150108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010814', '14', '321284020120150108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010815', '15', '321284020120150108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010816', '16', '321284020120150108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010817', '17', '321284020120150108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010818', '18', '321284020120150108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010819', '19', '321284020120150108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010820', '20', '321284020120150108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010821', '21', '321284020120150108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010822', '22', '321284020120150108', null, 0, 1, 10, 5, 0, 1)
--		    
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010701', '01', '321284020120150107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010702', '02', '321284020120150107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010703', '03', '321284020120150107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010704', '04', '321284020120150107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010705', '05', '321284020120150107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010706', '06', '321284020120150107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010707', '07', '321284020120150107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010708', '08', '321284020120150107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010709', '09', '321284020120150107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010710', '10', '321284020120150107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010711', '11', '321284020120150107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010712', '12', '321284020120150107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010713', '13', '321284020120150107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010714', '14', '321284020120150107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010715', '15', '321284020120150107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010716', '16', '321284020120150107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010717', '17', '321284020120150107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010718', '18', '321284020120150107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010719', '19', '321284020120150107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010720', '20', '321284020120150107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010721', '21', '321284020120150107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010722', '22', '321284020120150107', null, 0, 1, 10, 5, 0, 1)
--2014		    
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010901', '01', '321284020120140109', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010902', '02', '321284020120140109', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010903', '03', '321284020120140109', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010904', '04', '321284020120140109', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010905', '05', '321284020120140109', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010906', '06', '321284020120140109', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010907', '07', '321284020120140109', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010908', '08', '321284020120140109', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010909', '09', '321284020120140109', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010910', '10', '321284020120140109', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010911', '11', '321284020120140109', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010912', '12', '321284020120140109', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010913', '13', '321284020120140109', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010914', '14', '321284020120140109', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010915', '15', '321284020120140109', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010916', '16', '321284020120140109', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010917', '17', '321284020120140109', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010918', '18', '321284020120140109', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010919', '19', '321284020120140109', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010920', '20', '321284020120140109', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010921', '21', '321284020120140109', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010922', '22', '321284020120140109', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010923', '23', '321284020120140109', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010924', '24', '321284020120140109', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010925', '25', '321284020120140109', null, 0, 1, 10, 5, 0, 1)
--		    
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010801', '01', '321284020120140108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010802', '02', '321284020120140108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010803', '03', '321284020120140108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010804', '04', '321284020120140108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010805', '05', '321284020120140108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010806', '06', '321284020120140108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010807', '07', '321284020120140108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010808', '08', '321284020120140108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010809', '09', '321284020120140108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010810', '10', '321284020120140108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010811', '11', '321284020120140108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010812', '12', '321284020120140108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010813', '13', '321284020120140108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010814', '14', '321284020120140108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010815', '15', '321284020120140108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010816', '16', '321284020120140108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010817', '17', '321284020120140108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010818', '18', '321284020120140108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010819', '19', '321284020120140108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010820', '20', '321284020120140108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010821', '21', '321284020120140108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010822', '22', '321284020120140108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010823', '23', '321284020120140108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010824', '24', '321284020120140108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010825', '25', '321284020120140108', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010826', '26', '321284020120140108', null, 0, 1, 10, 5, 0, 1)
--		    
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010701', '01', '321284020120140107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010702', '02', '321284020120140107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010703', '03', '321284020120140107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010704', '04', '321284020120140107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010705', '05', '321284020120140107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010706', '06', '321284020120140107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010707', '07', '321284020120140107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010708', '08', '321284020120140107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010709', '09', '321284020120140107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010710', '10', '321284020120140107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010711', '11', '321284020120140107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010712', '12', '321284020120140107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010713', '13', '321284020120140107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010714', '14', '321284020120140107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010715', '15', '321284020120140107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010716', '16', '321284020120140107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010717', '17', '321284020120140107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010718', '18', '321284020120140107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010719', '19', '321284020120140107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010720', '20', '321284020120140107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010721', '21', '321284020120140107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010722', '22', '321284020120140107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010723', '23', '321284020120140107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010724', '24', '321284020120140107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010725', '25', '321284020120140107', null, 0, 1, 10, 5, 0, 1)
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010726', '26', '321284020120140107', null, 0, 1, 10, 5, 0, 1)
go

select * from TBan 
where ids = '32128402012015010901'

select * from StudGrade

update studgrade
set banids = gradeids + substring(oldban,1,2)
where gradeids = '321284020120150109'or gradeids = '321284020120160108'



delete from studgrade



select banids, avg(score)
from studgrade
where gradeids = '321284020120150109'
group by banids


select * from studgrade where id = '000f4a47ed56448b87d61ccc66d5db66'
select * from student where ids = '32128402012015010185'

select * from student where name like  '%王海%'

insert StudGradeMove
select ID,ids,banids, OwnerIDS ='o47ZhvxoQA9QOOgDSZ5hGaea4xdI' from studgrade where studids = '32128402012015010104'


