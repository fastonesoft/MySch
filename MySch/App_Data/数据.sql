use master

------------------------------------
--
--          创建数据库文件
--
------------------------------------

--数据库存在，则删除。
if exists
(
	select *
	from sysdatabases
	where name = 'MySch'
)
drop database MySch
go

--创建：数据库
create database MySch
on primary
(
	name = 'MySch',
	filename = 'D:\SQLData\MySch.mdf',
	size = 5mb,
	filegrowth = 5mb
)
go

use MySch


--用户表
if exists
(
	select * from sysobjects
	where ID = object_ID(N'TAcc') and objectproperty(ID, N'istable') = 1
)
drop table TAcc
go
create table TAcc
(
	--注册资料
	ID	nvarchar(32) not null,	
	IDS	nvarchar(20) not null,	--帐号代码x321284xxx
	Name	nvarchar(20) not null,	--帐号全称、姓名
	Pwd	nvarchar(32) not null,
	RegTime	datetime not null default getdate(),
	Fixed	bit not null,
	Parent	nvarchar(32),
)
go
alter table TAcc add constraint PK_TAcc primary key clustered (ID)
create unique nonclustered index UN_TAcc_IDS on TAcc (IDS)
create unique nonclustered index UN_TAcc_Name on TAcc (Name)
--插入管理员
insert TAcc values ('51e66f66919ee73bc252590bdf3b339c','admin','系统管理员','538e1387be95027c7c4edf399c4e0149','2015-09-10 12:00:00',  0, null)
go

--新生报名
create table TStudReg
(
	ID	nvarchar(32) not null,	--唯一编号
	IDS	nvarchar(20) not null,	--身份证号
	Name	nvarchar(20) not null,	--姓名
	FromSch	nvarchar(20),	--学校
	FromGrade	nvarchar(10),	--年级
	NationID	nvarchar(20),	--全国学籍号
	ReadState	nvarchar(20),	--就读状态
	IsProblem	bit not null,	--是否问题学籍
	--以上：自动注册时填充
	StudNo	nvarchar(32),	--学籍号-考试编号
	SchChoose	bit not null,	--是否择校
	Memo	nvarchar(50),	--备注
	--以上：归档时填充
	Mobil1	nvarchar(20),	--联系电话一
	Mobil2	nvarchar(20),	--联系电话二
	Name1	nvarchar(20),	--第一监护人
	Name2	nvarchar(20),	--第二监护人
	Home	nvarchar(50),	--家庭地址
	Permanent	nvarchar(50),	--户籍地址
	Reged	bit not null,	--是否注册
	OpenID	nvarchar(32),	--用户ID	
)
alter table TStudReg add constraint PK_TStudReg primary key clustered (ID)
create unique nonclustered index UN_TStudReg_IDS on TStudReg (IDS)
create index IN_TStudReg_Name on TStudReg (Name)
create index IN_TStudReg_StudNo on TStudReg (StudNo)
go

create table TPrint
(
	Name	nvarchar(20) not null,	--
	X	nvarchar(10) not null,
	Y	nvarchar(10) not null,
)
go
alter table TPrint add constraint PK_TPrint primary key clustered (Name)

insert TPrint values ('No', '1190px', '90px')
insert TPrint values ('Name', '850px', '200px')
insert TPrint values ('School', '940px', '270px')
go



--学制编排
create table TEducation
(
	ID	nvarchar(32) not null,
	IDS	int not null,
	Name	nvarchar(20) not null,
	Fixed	bit not null	--是否使用
)
go

insert TEducation values (Lower(REPLACE(NEWID(), '-','')), 1, '一年级', 0)
insert TEducation values (Lower(REPLACE(NEWID(), '-','')), 2, '二年级', 0)
insert TEducation values (Lower(REPLACE(NEWID(), '-','')), 3, '三年级', 0)
insert TEducation values (Lower(REPLACE(NEWID(), '-','')), 4, '四年级', 0)
insert TEducation values (Lower(REPLACE(NEWID(), '-','')), 5, '五年级', 0)
insert TEducation values (Lower(REPLACE(NEWID(), '-','')), 6, '六年级', 0)
insert TEducation values (Lower(REPLACE(NEWID(), '-','')), 7, '七年级', 1)
insert TEducation values (Lower(REPLACE(NEWID(), '-','')), 8, '八年级', 1)
insert TEducation values (Lower(REPLACE(NEWID(), '-','')), 9, '九年级', 1)

go
alter table TEducation add constraint PK_TEducation primary key clustered (ID)
create unique nonclustered index UN_TEducation_IDS on TEducation (IDS)


--校区设置
create table TPart
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,
	Name	nvarchar(20) not null,
	Fixed	bit not null,
	AccIDS	nvarchar(32) not null
)
go
alter table TPart add constraint PK_TPart primary key clustered (ID)
alter table TPart add constraint FK_TPart_AccID foreign key (AccIDS) references TAcc (IDS)
create unique nonclustered index UN_TPart_IDS on TPart (IDS)
create unique nonclustered index UN_TPart_Name on TPart (Name)

--生源分组
create table TStep
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,
	Name	int not null,	--级
	Fixed	bit not null,	--是否毕业
)
go
alter table TStep add constraint PK_TStep primary key clustered (ID)
create unique nonclustered index UN_TStep_IDS on TStep (IDS)

--校区生源
create table TPartSt
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,
	PartIDS	nvarchar(20) not null,
	StepIDS	nvarchar(20) not null,
)
go
alter table TPartSt add constraint PK_TPartSt primary key clustered (ID)
alter table TPartSt add constraint FK_TPartSt_PartIDS foreign key (PartIDS) references TPart (IDS)
alter table TPartSt add constraint FK_TPartSt_StepIDS foreign key (StepIDS) references TStep (IDS)
create unique nonclustered index UN_TPartSt_IDS on TPart (IDS)
go

--校区生源 查询
create view QPartSt
as
select a.*, PartName = b.Name, StepName = c.Name
from TPartSt a left join TPart b
on a.PartIDS = b.IDS
left join TStep c
on a.StepIDS = c.IDS
go


-------------------------------------------------------------------
---以下不算

--样式表
create table Theme
(
	ID	nvarchar(32) not null,
	GD	nvarchar(32) not null,
	Name	nvarchar(32) not null,
	Defaulted	bit not null,
)
go

--页面表
create table TPage
(
	ID	nvarchar(32) not null,
	GD	nvarchar(32) not null,
	Name	nvarchar(32) not null,
	ThemeID	nvarchar(32) not null,
)
go

alter table TPage add constraint PK_TPage primary key clustered (ID)
create unique nonclustered index UN_TPage_Name on TPage (Name)

create table TColumn
(
	ID	nvarchar(32) not null,
	GD	nvarchar(32) not null,
	Name	nvarchar(32) not null,
	Html	nvarchar(max) not null,
	Txt	nvarchar(max) not null,
	Fixed	bit not null,
	PageID	nvarchar(32) not null,
)
go


--文件记录
create table TFileInfor
(
	Name	nvarchar(40) not null,	--文件名称 GUID + .xxx
	fileClass	nvarchar(20) not null,	--文件分类
	fileAuthor	nvarchar(20) not null,	--文件作者
	updateTime	datetime not null default getdate(),
)
alter table TFileInfor add constraint PK_TFileInfor primary key clustered (Name)
create unique nonclustered index UN_TFileInfor_fileAuthor on TFileInfor (fileAuthor)
go


--登录日志
create table TLogin
(
	ID	nvarchar(32) not null,
	IDS	int identity(1,1) not null,
	Brower	nvarchar(36) not null,
	IP	nvarchar(36) not null,
	loginTime	datetime not null,	--登录时间
	Name	nvarchar(20) not null,
	Pwd	nvarchar(36) not null,
	loginMsg	nvarchar(36) not null,
)
go
alter table TLogin add constraint PK_TLogin primary key clustered (ID)
create unique nonclustered index UN_TLogin_IDS on TLogin (IDS)


--年度
create table TYear
(
	ID	nvarchar(32) not null,
	IDS	int not null,	--年度开始
	Ends	int not null,	--年度结束
	IsCurrent	bit not null,	--是否当前年度
)
go
alter table TStep add constraint PK_TStep primary key clustered (ID)
create unique nonclustered index UN_TStep_IDS on TStep (IDS)


--级设置
create table TStep
(
	ID	nvarchar(32) not null,
	IDS	int not null,	--级
	Fixed	bit not null
)
go
alter table TStep add constraint PK_TStep primary key clustered (ID)
create unique nonclustered index UN_TStep_IDS on TStep (IDS)

insert TStep values (Lower(REPLACE(NEWID(), '-','')), 2004, 1)
insert TStep values (Lower(REPLACE(NEWID(), '-','')), 2005, 1)
insert TStep values (Lower(REPLACE(NEWID(), '-','')), 2006, 1)
insert TStep values (Lower(REPLACE(NEWID(), '-','')), 2007, 1)
insert TStep values (Lower(REPLACE(NEWID(), '-','')), 2008, 1)
insert TStep values (Lower(REPLACE(NEWID(), '-','')), 2009, 1)
insert TStep values (Lower(REPLACE(NEWID(), '-','')), 2010, 1)
insert TStep values (Lower(REPLACE(NEWID(), '-','')), 2011, 1)
insert TStep values (Lower(REPLACE(NEWID(), '-','')), 2012, 1)
insert TStep values (Lower(REPLACE(NEWID(), '-','')), 2013, 1)
insert TStep values (Lower(REPLACE(NEWID(), '-','')), 2014, 0)
insert TStep values (Lower(REPLACE(NEWID(), '-','')), 2015, 0)
insert TStep values (Lower(REPLACE(NEWID(), '-','')), 2016, 0)



--学校
create table TSchool
(
	ID	nvarchar(20) not null,	--32128441402
	GD	nvarchar(32) not null,
	Name	nvarchar(32) not null,	--学校名称
	Fixed	bit not null	--是否撤销
)
go
alter table TSchool add constraint PK_TSchool primary key clustered (ID)
create unique nonclustered index UN_TSchool_GD on TSchool (GD)
create index IN_TSchool_Name on TSchool (Name)




--年级
create table TGrade
(
	ID	nvarchar(20) not null,	--g200402
	GD	nvarchar(32) not null,
	Name	nvarchar(20) not null,	--
	YearID	int not null,
	SchoolID	nvarchar(20) not null		
)
go
alter table TGrade add constraint PK_TGrade primary key clustered (ID)
create unique nonclustered index UN_TGrade_GD on TGrade (GD)
create index IN_TGrade_Name on TGrade (Name)



--用户班级：班主任设置
create table TClassMaster
(
	ID	nvarchar(20) not null,	--班级编号：20150701
	GD	nvarchar(32) not null,	--编码
	StepID	int not null,	--级
	GradeID	int not null,	--年级
	AccID	nvarchar(20)
)
go


--条码生成
create table TCode
(
	ID	nvarchar(32) not null
)
go
alter table TCode add constraint PK_TCode primary key clustered (ID)



--以上先做
--用户学科：课任老师





--分班测试
create table TClassNew
(
	GD	nvarchar(32) not null,
	ID	nvarchar(20) not null,
	OClassID	nvarchar(20) not null,
	NClassID	nvarchar(20) not null,
	SameGroup	nvarchar(32),
	Needed	bit,
	Score1	float,
	Score2	float,
	Score3	float,
	Score4	float,
	Total	float,
)
go

--教室设置
create table TRoom
(
	ID	nvarchar(10) not null,	--教室编号：101、201、205
	GD	nvarchar(32) not null,
	Num	int
)
go



--键值分类
create table TKeyClass
(
	ID	nvarchar(32) not null,
	GD	nvarchar(32) not null,
	Name	nvarchar(32) not null,
	Fixed	bit not null,
	Parent	nvarchar(32),
)
alter table TKeyClass add constraint PK_TKeyClass primary key clustered (ID)
create index UN_TKeyClass_Parent on TKeyClass (Parent)
go
--键值分类可以无限自我扩展

--键值对
create table TKey
(
	ID	nvarchar(32) not null,	--Value
	GD	nvarchar(32) not null,	--Valid
	Name	nvarchar(32) not null,	--Key
	Fixed	bit not null,	
	KeyClassID	nvarchar(32) not null,
)
go
alter table TKey add constraint PK_TKey primary key clustered (ID)
create index UN_TKey_Name on TKey (Name)
go




--微日志
create table TLog
(
	GD	nvarchar(32) not null,
	Value	nvarchar(2000) not null,
	CreateTime	datetime not null default getdate(), 
)
go
alter table TLog add constraint PK_TLog primary key clustered (GD)




delete from tstudreg where ids = '321284200508150254'