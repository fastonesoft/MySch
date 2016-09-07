

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


--微日志
create table TLog
(
	GD	nvarchar(32) not null,
	Value	nvarchar(2000) not null,
	CreateTime	datetime not null default getdate(), 
)
go
alter table TLog add constraint PK_TLog primary key clustered (GD)




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
	ID	nvarchar(32) not null,	
	IDS	nvarchar(20) not null,	--帐号321284xx
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
insert TAcc values ('02b7f4a7710ac87488ab1f13b8e22a65','32128402','姜堰区实验初中','fe9cad83ab25c5474cc26be3f010f281','2015-09-10 12:00:00',  0, '51e66f66919ee73bc252590bdf3b339c')
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
create table TEdu
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,
	Name	nvarchar(10) not null,
	Value	nvarchar(2) not null,
	Fixed	bit not null,	--是否使用
	AccIDS	nvarchar(20) not null
)
go
alter table TEdu add constraint PK_TEdu primary key clustered (ID)
create unique nonclustered index UN_TEdu_IDS on TEdu (IDS)


insert TEdu values (Lower(REPLACE(NEWID(), '-','')), '3212840201', '一年级', '01', 0, '32128402')
insert TEdu values (Lower(REPLACE(NEWID(), '-','')), '3212840202', '二年级', '02', 0, '32128402')
insert TEdu values (Lower(REPLACE(NEWID(), '-','')), '3212840203', '三年级', '03', 0, '32128402')
insert TEdu values (Lower(REPLACE(NEWID(), '-','')), '3212840204', '四年级', '04', 0, '32128402')
insert TEdu values (Lower(REPLACE(NEWID(), '-','')), '3212840205', '五年级', '05', 0, '32128402')
insert TEdu values (Lower(REPLACE(NEWID(), '-','')), '3212840206', '六年级', '06', 0, '32128402')
insert TEdu values (Lower(REPLACE(NEWID(), '-','')), '3212840207', '七年级', '07', 1, '32128402')
insert TEdu values (Lower(REPLACE(NEWID(), '-','')), '3212840208', '八年级', '08', 1, '32128402')
insert TEdu values (Lower(REPLACE(NEWID(), '-','')), '3212840209', '九年级', '09', 1, '32128402')


--校区设置
create table TPart
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,	--32128402XX
	Name	nvarchar(10) not null,
	Value	nvarchar(2) not null,
	Fixed	bit not null,
	AccIDS	nvarchar(20) not null
)
go
alter table TPart add constraint PK_TPart primary key clustered (ID)
alter table TPart add constraint FK_TPart_AccID foreign key (AccIDS) references TAcc (IDS)
create unique nonclustered index UN_TPart_IDS on TPart (IDS)
create unique nonclustered index UN_TPart_Name on TPart (Name)


insert TPart values (Lower(REPLACE(NEWID(), '-','')), '3212840201', '实验初中', '01', 1, '32128402')
insert TPart values (Lower(REPLACE(NEWID(), '-','')), '3212840202', '二附初中', '02', 0, '32128402')
insert TPart values (Lower(REPLACE(NEWID(), '-','')), '3212840203', '二附三水', '03', 0, '32128402')
insert TPart values (Lower(REPLACE(NEWID(), '-','')), '3212840204', '天目学校', '04', 0, '32128402')

--分级设置
create table TStep
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,	--32128402XXXX
	Name	nvarchar(10) not null,	--级
	Graduated	bit not null,	--是否毕业
	AccIDS	nvarchar(20) not null
)
go
alter table TStep add constraint PK_TStep primary key clustered (ID)
alter table TStep add constraint FK_TStep_AccIDS foreign key (AccIDS) references TAcc (IDS)
create unique nonclustered index UN_TStep_IDS on TStep (IDS)
go

insert TStep values (Lower(REPLACE(NEWID(), '-','')), '321284022016', '2016', 0, '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '321284022015', '2015', 0, '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '321284022014', '2014', 0, '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '321284022013', '2013', 1, '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '321284022012', '2012', 1, '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '321284022011', '2011', 1, '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '321284022010', '2010', 1, '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '321284022009', '2009', 1, '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '321284022008', '2008', 1, '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '321284022007', '2007', 1, '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '321284022006', '2006', 1, '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '321284022005', '2005', 1, '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '321284022004', '2004', 1, '32128402')

--校区分级
create table TPartStep
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,	--321284022016XX
	PartIDS	nvarchar(20) not null,
	StepIDS	nvarchar(20) not null,
	AccIDS	nvarchar(20) not null,
)
go
alter table TPartStep add constraint PK_TPartStep primary key clustered (ID)
alter table TPartStep add constraint FK_TPartStep_PartIDS foreign key (PartIDS) references TPart (IDS)
alter table TPartStep add constraint FK_TPartStep_StepIDS foreign key (StepIDS) references TStep (IDS)
alter table TPartStep add constraint FK_TPartStep_AccIDS foreign key (AccIDS) references TAcc (IDS)
create unique nonclustered index UN_TPartStep_IDS on TPartStep (IDS)
go
--实验
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402012016', '3212840201', '321284022016', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402012015', '3212840201', '321284022015', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402012014', '3212840201', '321284022014', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402012013', '3212840201', '321284022013', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402012012', '3212840201', '321284022012', '32128402')
--二附	                           
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402022011', '3212840202', '321284022011', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402022010', '3212840202', '321284022010', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402022009', '3212840202', '321284022009', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402022008', '3212840202', '321284022008', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402022007', '3212840202', '321284022007', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402022006', '3212840202', '321284022006', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402022005', '3212840202', '321284022005', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402022004', '3212840202', '321284022004', '32128402')
--三水	                           
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402032011', '3212840203', '321284022011', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402032010', '3212840203', '321284022010', '32128402')
--天目	                           
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402042009', '3212840204', '321284022009', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402042008', '3212840204', '321284022008', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402042007', '3212840204', '321284022007', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402042006', '3212840204', '321284022006', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402042005', '3212840204', '321284022005', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402042004', '3212840204', '321284022004', '32128402')


--校区分组查询
go
create view QPartStep
as
select a.*
,Name = b.Name + ' - ' + c.Name + '级'
,b.Name as PartName, c.Name as StepName
,Graduated = ISNULL(c.Graduated, 1)
from TPartStep a left join TPart b
on a.PartIDS = b.IDS
left join TStep c
on a.StepIDS = c.IDS
go

--年度设置
create table TYear
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,	--年度编号32128402XXXX
	Name	nvarchar(10) not null,	--年度2016
	IsCurrent	bit not null,	--当前年度
	AccIDS	nvarchar(20) not null	--所属学校
)
go
alter table TYear add constraint PK_TYear primary key clustered (ID)
alter table TYear add constraint FK_TYear_AccIDS foreign key (AccIDS) references TAcc (IDS)
create unique nonclustered index UN_TYear_IDS on TYear (IDS)
go
insert TYear values (Lower(REPLACE(NEWID(), '-','')), '321284022016', '2016', 1, '32128402')
insert TYear values (Lower(REPLACE(NEWID(), '-','')), '321284022015', '2015', 0, '32128402')
insert TYear values (Lower(REPLACE(NEWID(), '-','')), '321284022014', '2014', 0, '32128402')
insert TYear values (Lower(REPLACE(NEWID(), '-','')), '321284022013', '2013', 0, '32128402')
insert TYear values (Lower(REPLACE(NEWID(), '-','')), '321284022012', '2012', 0, '32128402')
insert TYear values (Lower(REPLACE(NEWID(), '-','')), '321284022011', '2011', 0, '32128402')
insert TYear values (Lower(REPLACE(NEWID(), '-','')), '321284022010', '2010', 0, '32128402')
insert TYear values (Lower(REPLACE(NEWID(), '-','')), '321284022009', '2009', 0, '32128402')
insert TYear values (Lower(REPLACE(NEWID(), '-','')), '321284022008', '2008', 0, '32128402')
insert TYear values (Lower(REPLACE(NEWID(), '-','')), '321284022007', '2007', 0, '32128402')
insert TYear values (Lower(REPLACE(NEWID(), '-','')), '321284022006', '2006', 0, '32128402')
insert TYear values (Lower(REPLACE(NEWID(), '-','')), '321284022005', '2005', 0, '32128402')
insert TYear values (Lower(REPLACE(NEWID(), '-','')), '321284022004', '2004', 0, '32128402')


--学期设置
create table TSemester
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,	--学期编号32128402XX
	Name	nvarchar(20) not null,	--学期名称：第一学期
	Value	nvarchar(2) not null,
	AccIDS	nvarchar(20) not null,
)
alter table TSemester add constraint PK_TSemester primary key clustered (ID)
create unique nonclustered index UN_TSemester_IDS on TSemester (IDS)
go
insert TSemester values (Lower(REPLACE(NEWID(), '-','')), '3212840201', '第一学期', '01', '32128402')
insert TSemester values (Lower(REPLACE(NEWID(), '-','')), '3212840202', '第二学期', '02', '32128402')


--学期列表
create table TTerm
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,	--学期编号32128402YYYYXX
	IsCurrent	bit not null,
	YearIDS	nvarchar(20) not null,
	SemesterIDS	nvarchar(20) not null,
	AccIDS	nvarchar(20) not null,
)
go
alter table TTerm add constraint PK_TTerm primary key clustered (ID)
alter table TTerm add constraint FK_TTerm_AccIDS foreign key (AccIDS) references TAcc (IDS)
alter table TTerm add constraint FK_TTerm_YearIDS foreign key (YearIDS) references TYear (IDS)
alter table TTerm add constraint FK_TTerm_SemesterIDS foreign key (SemesterIDS) references TSemester (IDS)
create unique nonclustered index UN_TTerm_IDS on TTerm (IDS)

insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402200401', 0, '321284022004', '3212840201', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402200402', 0, '321284022004', '3212840202', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402200501', 0, '321284022005', '3212840201', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402200502', 0, '321284022005', '3212840202', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402200601', 0, '321284022006', '3212840201', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402200602', 0, '321284022006', '3212840202', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402200701', 0, '321284022007', '3212840201', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402200702', 0, '321284022007', '3212840202', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402200801', 0, '321284022008', '3212840201', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402200802', 0, '321284022008', '3212840202', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402200901', 0, '321284022009', '3212840201', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402200902', 0, '321284022009', '3212840202', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402201001', 0, '321284022010', '3212840201', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402201002', 0, '321284022010', '3212840202', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402201101', 0, '321284022011', '3212840201', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402201102', 0, '321284022011', '3212840202', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402201201', 0, '321284022012', '3212840201', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402201202', 0, '321284022012', '3212840202', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402201301', 0, '321284022013', '3212840201', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402201302', 0, '321284022013', '3212840202', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402201401', 0, '321284022014', '3212840201', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402201402', 0, '321284022014', '3212840202', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402201501', 0, '321284022015', '3212840201', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402201502', 0, '321284022015', '3212840202', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402201601', 1, '321284022016', '3212840201', '32128402')

--学期查询
go
create view QTerm
as
select a.*, b.Name as YearName, c.Name as TermName
from TTerm a left join TYear b
on a.YearIDS = b.IDS
left join TSemester c
on a.SemesterIDS = c.IDS
go

--年级设置
create table TGrade
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,	--年级编号32128402201601XX
	PartStepIDS	nvarchar(20) not null,	--分级
	YearIDS	nvarchar(20) not null,	--年度
	EduIDS	nvarchar(20) not null,	--学制
	AccIDS	nvarchar(20) not null	--用户	
)
go
alter table TGrade add constraint PK_TGrade primary key clustered (ID)
alter table TGrade add constraint FK_TGrade_PartStepIDS foreign key (PartStepIDS) references TPartStep (IDS)
alter table TGrade add constraint FK_TGrade_YearIDS foreign key (YearIDS) references TYear (IDS)
alter table TGrade add constraint FK_TGrade_EduIDS foreign key (EduIDS) references TEdu (IDS)
alter table TGrade add constraint FK_TGrade_AccIDS foreign key (AccIDS) references TAcc (IDS)
create unique nonclustered index UN_TGrade_IDS on TGrade (IDS)
--实验
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840201201607', '32128402012016', '321284022016', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840201201507', '32128402012015', '321284022015', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840201201508', '32128402012015', '321284022016', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840201201407', '32128402012014', '321284022014', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840201201408', '32128402012014', '321284022015', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840201201409', '32128402012014', '321284022016', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840201201307', '32128402012013', '321284022013', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840201201308', '32128402012013', '321284022014', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840201201309', '32128402012013', '321284022015', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840201201207', '32128402012012', '321284022012', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840201201208', '32128402012012', '321284022013', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840201201209', '32128402012012', '321284022014', '3212840209', '32128402')
--三水
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840203201108', '32128402032011', '321284022012', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840203201109', '32128402032011', '321284022013', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840203201009', '32128402032010', '321284022012', '3212840209', '32128402')
--天目
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840204200907', '32128402042009', '321284022009', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840204200807', '32128402042008', '321284022008', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840204200707', '32128402042007', '321284022007', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840204200607', '32128402042006', '321284022006', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840204200908', '32128402042009', '321284022010', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840204200808', '32128402042008', '321284022009', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840204200708', '32128402042007', '321284022008', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840204200608', '32128402042006', '321284022007', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840204200508', '32128402042005', '321284022006', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840204200909', '32128402042009', '321284022011', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840204200809', '32128402042008', '321284022010', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840204200709', '32128402042007', '321284022009', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840204200609', '32128402042006', '321284022008', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840204200509', '32128402042005', '321284022007', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840204200409', '32128402042004', '321284022006', '3212840209', '32128402')
--二附
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840202201107', '32128402022011', '321284022011', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840202201007', '32128402022010', '321284022010', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840202200907', '32128402022009', '321284022009', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840202200807', '32128402022008', '321284022008', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840202200707', '32128402022007', '321284022007', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840202200607', '32128402022006', '321284022006', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840202201108', '32128402022011', '321284022012', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840202201008', '32128402022010', '321284022011', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840202200908', '32128402022009', '321284022010', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840202200808', '32128402022008', '321284022009', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840202200708', '32128402022007', '321284022008', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840202200608', '32128402022006', '321284022007', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840202200508', '32128402022005', '321284022006', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840202201109', '32128402022011', '321284022013', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840202201009', '32128402022010', '321284022012', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840202200909', '32128402022009', '321284022011', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840202200809', '32128402022008', '321284022010', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840202200709', '32128402022007', '321284022009', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840202200609', '32128402022006', '321284022008', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840202200509', '32128402022005', '321284022007', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840202200409', '32128402022004', '321284022006', '3212840209', '32128402')


--年级查询
go
create view QGrade
as
select a.*
,Name = b.Name + ' - ' + e.Name
,PartStepName = b.Name
,PartName = b.PartName
,StepName = b.StepName
,YearName = y.Name
,EduName = e.Name
,Graduated = ISNULL( b.Graduated, 1)
from TGrade a left join QPartStep b
on a.PartStepIDS = b.IDS
left join TYear y
on a.YearIDS = y.IDS
left join TEdu e
on a.EduIDS = e.IDS
go

create table TBan
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,	--3212840220160107XX
	Num	int not null,
	GradeIDS	nvarchar(20) not null,
	MasterIDS	nvarchar(20),
	GroupIDS	nvarchar(20),
	AccIDS	nvarchar(20) not null,
)
go
alter table TBan add constraint PK_TBan primary key clustered (ID)
alter table TBan add constraint FK_TBan_GradeIDS foreign key (GradeIDS) references TGrade (IDS)
alter table TBan add constraint FK_TBan_MasterIDS foreign key (MasterIDS) references TAcc (IDS)
alter table TBan add constraint FK_TBan_GroupIDS foreign key (GroupIDS) references TAcc (IDS)
alter table TBan add constraint FK_TBan_AccIDS foreign key (AccIDS) references TAcc (IDS)
create unique nonclustered index UN_TBan_IDS on TBan (IDS)

--2016级
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120160701', 1, '3212840201201607', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120160702', 2, '3212840201201607', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120160703', 3, '3212840201201607', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120160704', 4, '3212840201201607', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120160705', 5, '3212840201201607', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120160706', 6, '3212840201201607', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120160707', 7, '3212840201201607', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120160708', 8, '3212840201201607', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120160709', 9, '3212840201201607', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120160710', 10, '3212840201201607', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120160711', 11, '3212840201201607', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120160712', 12, '3212840201201607', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120160713', 13, '3212840201201607', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120160714', 14, '3212840201201607', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120160715', 15, '3212840201201607', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120160716', 16, '3212840201201607', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120160717', 17, '3212840201201607', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120160718', 18, '3212840201201607', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120160719', 19, '3212840201201607', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120160720', 20, '3212840201201607', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120160721', 21, '3212840201201607', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120160722', 22, '3212840201201607', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120160723', 23, '3212840201201607', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120160724', 24, '3212840201201607', null, null, '32128402')
--2015
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150801', 1, '3212840201201508', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150802', 2, '3212840201201508', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150803', 3, '3212840201201508', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150804', 4, '3212840201201508', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150805', 5, '3212840201201508', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150806', 6, '3212840201201508', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150807', 7, '3212840201201508', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150808', 8, '3212840201201508', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150809', 9, '3212840201201508', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150810', 10, '3212840201201508', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150811', 11, '3212840201201508', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150812', 12, '3212840201201508', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150813', 13, '3212840201201508', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150814', 14, '3212840201201508', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150815', 15, '3212840201201508', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150816', 16, '3212840201201508', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150817', 17, '3212840201201508', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150818', 18, '3212840201201508', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150819', 19, '3212840201201508', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150820', 20, '3212840201201508', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150821', 21, '3212840201201508', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150822', 22, '3212840201201508', null, null, '32128402')
--
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150701', 1, '3212840201201507', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150702', 2, '3212840201201507', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150703', 3, '3212840201201507', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150704', 4, '3212840201201507', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150705', 5, '3212840201201507', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150706', 6, '3212840201201507', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150707', 7, '3212840201201507', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150708', 8, '3212840201201507', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150709', 9, '3212840201201507', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150710', 10, '3212840201201507', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150711', 11, '3212840201201507', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150712', 12, '3212840201201507', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150713', 13, '3212840201201507', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150714', 14, '3212840201201507', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150715', 15, '3212840201201507', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150716', 16, '3212840201201507', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150717', 17, '3212840201201507', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150718', 18, '3212840201201507', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150719', 19, '3212840201201507', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150720', 20, '3212840201201507', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150721', 21, '3212840201201507', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120150722', 22, '3212840201201507', null, null, '32128402')
--2014
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140901', 1, '3212840201201409', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140902', 2, '3212840201201409', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140903', 3, '3212840201201409', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140904', 4, '3212840201201409', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140905', 5, '3212840201201409', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140906', 6, '3212840201201409', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140907', 7, '3212840201201409', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140908', 8, '3212840201201409', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140909', 9, '3212840201201409', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140910', 10, '3212840201201409', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140911', 11, '3212840201201409', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140912', 12, '3212840201201409', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140913', 13, '3212840201201409', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140914', 14, '3212840201201409', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140915', 15, '3212840201201409', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140916', 16, '3212840201201409', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140917', 17, '3212840201201409', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140918', 18, '3212840201201409', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140919', 19, '3212840201201409', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140920', 20, '3212840201201409', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140921', 21, '3212840201201409', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140922', 22, '3212840201201409', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140923', 23, '3212840201201409', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140924', 24, '3212840201201409', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140925', 25, '3212840201201409', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140926', 26, '3212840201201409', null, null, '32128402')
--
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140801', 1, '3212840201201408', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140802', 2, '3212840201201408', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140803', 3, '3212840201201408', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140804', 4, '3212840201201408', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140805', 5, '3212840201201408', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140806', 6, '3212840201201408', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140807', 7, '3212840201201408', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140808', 8, '3212840201201408', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140809', 9, '3212840201201408', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140810', 10, '3212840201201408', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140811', 11, '3212840201201408', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140812', 12, '3212840201201408', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140813', 13, '3212840201201408', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140814', 14, '3212840201201408', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140815', 15, '3212840201201408', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140816', 16, '3212840201201408', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140817', 17, '3212840201201408', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140818', 18, '3212840201201408', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140819', 19, '3212840201201408', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140820', 20, '3212840201201408', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140821', 21, '3212840201201408', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140822', 22, '3212840201201408', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140823', 23, '3212840201201408', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140824', 24, '3212840201201408', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140825', 25, '3212840201201408', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140826', 26, '3212840201201408', null, null, '32128402')
--
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140701', 1, '3212840201201407', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140702', 2, '3212840201201407', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140703', 3, '3212840201201407', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140704', 4, '3212840201201407', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140705', 5, '3212840201201407', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140706', 6, '3212840201201407', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140707', 7, '3212840201201407', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140708', 8, '3212840201201407', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140709', 9, '3212840201201407', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140710', 10, '3212840201201407', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140711', 11, '3212840201201407', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140712', 12, '3212840201201407', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140713', 13, '3212840201201407', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140714', 14, '3212840201201407', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140715', 15, '3212840201201407', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140716', 16, '3212840201201407', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140717', 17, '3212840201201407', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140718', 18, '3212840201201407', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140719', 19, '3212840201201407', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140720', 20, '3212840201201407', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140721', 21, '3212840201201407', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140722', 22, '3212840201201407', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140723', 23, '3212840201201407', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140724', 24, '3212840201201407', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140725', 25, '3212840201201407', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284020120140726', 26, '3212840201201407', null, null, '32128402')


--班级查询
go
create view QBan
as
select b.*
,PartStepIDS = g.PartStepIDS
,g.YearIDS
,g.EduIDS
,Name = g.Name + '（' + CAST(b.Num as nvarchar(5)) + '）班'
,GradeName = g.Name
,MasterName = m.Name
,GroupName = p.Name
,Graduated = ISNULL(g.Graduated, 1)
from TBan b left join QGrade g
on b.GradeIDS = g.IDS
left join TAcc m
on b.MasterIDS = m.IDS
left join TAcc p
on b.GroupIDS = p.IDS
go


--学生表
create table TStudent
(
	ID	nvarchar(32) not null,	--唯一编号
	IDS	nvarchar(20) not null,	--身份证号
	Name	nvarchar(20) not null,	--姓名
	FromSch	nvarchar(20),	--学校
	FromGrade	nvarchar(10),	--年级
	NationID	nvarchar(20),	--全国学籍号
	ReadState	nvarchar(20),	--就读状态
	IsProblem	bit not null,	--是否问题学籍
	--
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
go
alter table TStudent add constraint PK_TStudent primary key clustered (ID)
create unique nonclustered index UN_TStudent_IDS on TStudent (IDS)
create index IN_TStudent_Name on TStudent (Name)
create index IN_TStudent_StudNo on TStudent (StudNo)
go

--班级学生
create table TBanStud
(
	ID	nvarchar(32) not null,
	IDS	as BanIDS + StudIDS,
	BanIDS	nvarchar(20) not null,
	StudIDS	nvarchar(20) not null,
)
go
alter table TBanStud add constraint PK_TBanStud primary key clustered (ID)
alter table TBanStud add constraint FK_TBanStud_BanIDS foreign key (BanIDS) references TBan (IDS)
alter table TBanStud add constraint FK_TBanStud_StudIDS foreign key (StudIDS) references TStudent (IDS)
create unique nonclustered index UN_TBanStud_IDS on TBanStud (IDS)


--班级学生查询
go
create view QBanStud
as
select a.*
, b.GradeIDS, b.GradeName
, Graduated = ISNULL(b.Graduated, 1)
, StudID = c.ID, StudName = c.Name, StudNo = c.StudNo
, StudSex = case CAST(SUBSTRING(c.IDS , 17, 1) as int) % 2 when 1 then '男' when 0 then '女' end
from TBanStud a left join QBan b
on a.BanIDS = b.IDS
left join TStudent c
on a.StudIDS = c.IDS
go


--分班测试
create table TBanFen
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,
	GradeOldIDS	nvarchar(20) not null,
	GradeNewIDS	nvarchar(20) not null,
	BanOldIDS	nvarchar(20) not null,
	BanNewIDS	nvarchar(20),
	Total	float,
)
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





delete from tstudreg where ids = '321284200508150254'