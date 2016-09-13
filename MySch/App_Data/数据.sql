

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
alter table TEdu add constraint FK_TEdu_AccID foreign key (AccIDS) references TAcc (IDS)
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
	Fixed	bit not null,	--是否不用
	AccIDS	nvarchar(20) not null
)
go
alter table TPart add constraint PK_TPart primary key clustered (ID)
alter table TPart add constraint FK_TPart_AccID foreign key (AccIDS) references TAcc (IDS)
create unique nonclustered index UN_TPart_IDS on TPart (IDS)
create unique nonclustered index UN_TPart_Name on TPart (Name)


insert TPart values (Lower(REPLACE(NEWID(), '-','')), '3212840201', '实验初中', '01', 0, '32128402')
insert TPart values (Lower(REPLACE(NEWID(), '-','')), '3212840202', '二附初中', '02', 1, '32128402')
insert TPart values (Lower(REPLACE(NEWID(), '-','')), '3212840203', '二附三水', '03', 1, '32128402')
insert TPart values (Lower(REPLACE(NEWID(), '-','')), '3212840204', '天目学校', '04', 1, '32128402')

--分级设置
create table TStep
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,	--32128402XXXXYY
	Name	nvarchar(20) not null,	--级
	Value	nvarchar(10) not null,	--级编号
	Graduated	bit not null,	--是否毕业
	AccIDS	nvarchar(20) not null
)
go
alter table TStep add constraint PK_TStep primary key clustered (ID)
alter table TStep add constraint FK_TStep_AccIDS foreign key (AccIDS) references TAcc (IDS)
create unique nonclustered index UN_TStep_IDS on TStep (IDS)
go

insert TStep values (Lower(REPLACE(NEWID(), '-','')), '32128402201601', '2016级', '201601', 0, '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '32128402201501', '2015级', '201501', 0, '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '32128402201401', '2014级', '201401', 0, '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '32128402201301', '2013级', '201301', 1, '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '32128402201201', '2012级', '201201', 1, '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '32128402201101', '2011级', '201101', 1, '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '32128402201001', '2010级', '201001', 1, '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '32128402200901', '2009级', '200901', 1, '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '32128402200801', '2008级', '200801', 1, '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '32128402200701', '2007级', '200701', 1, '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '32128402200601', '2006级', '200601', 1, '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '32128402200501', '2005级', '200501', 1, '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '32128402200401', '2004级', '200401', 1, '32128402')

--校区分级
create table TPartStep
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,	--32128402012016XX
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
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '3212840201201601', '3212840201', '32128402201601', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '3212840201201501', '3212840201', '32128402201501', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '3212840201201401', '3212840201', '32128402201401', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '3212840201201301', '3212840201', '32128402201301', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '3212840201201201', '3212840201', '32128402201201', '32128402')
--二附		         
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '3212840202201101', '3212840202', '32128402201101', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '3212840202201001', '3212840202', '32128402201001', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '3212840202200901', '3212840202', '32128402200901', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '3212840202200801', '3212840202', '32128402200801', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '3212840202200701', '3212840202', '32128402200701', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '3212840202200601', '3212840202', '32128402200601', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '3212840202200501', '3212840202', '32128402200501', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '3212840202200401', '3212840202', '32128402200401', '32128402')
--三水		         
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '3212840203201101', '3212840203', '32128402201101', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '3212840203201001', '3212840203', '32128402201001', '32128402')
--天目		         
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '3212840204200901', '3212840204', '32128402200901', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '3212840204200801', '3212840204', '32128402200801', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '3212840204200701', '3212840204', '32128402200701', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '3212840204200601', '3212840204', '32128402200601', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '3212840204200501', '3212840204', '32128402200501', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '3212840204200401', '3212840204', '32128402200401', '32128402')


--校区分组查询
drop view QPartStep
go
create view QPartStep
as
select a.*
,Name = b.Name + ' - ' + c.Name
,b.Name as PartName
,c.Name as StepName
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
drop view QTerm
go
create view QTerm
as
select 
a.*
, Name = b.Name + '年度 - ' + c.Name
from TTerm a left join TYear b
on a.YearIDS = b.IDS
left join TSemester c
on a.SemesterIDS = c.IDS
go

--年级设置
create table TGrade
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,	--年级编号3212840201201601XX
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
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020120160107', '3212840201201601', '321284022016', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020120150107', '3212840201201501', '321284022015', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020120150108', '3212840201201501', '321284022016', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020120140107', '3212840201201401', '321284022014', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020120140108', '3212840201201401', '321284022015', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020120140109', '3212840201201401', '321284022016', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020120130107', '3212840201201301', '321284022013', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020120130108', '3212840201201301', '321284022014', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020120130109', '3212840201201301', '321284022015', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020120120107', '3212840201201201', '321284022012', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020120120108', '3212840201201201', '321284022013', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020120120109', '3212840201201201', '321284022014', '3212840209', '32128402')
--三水		                              
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020320110108', '3212840203201101', '321284022012', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020320110109', '3212840203201101', '321284022013', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020320100109', '3212840203201001', '321284022012', '3212840209', '32128402')
--天目		                              
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420090107', '3212840204200901', '321284022009', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420080107', '3212840204200801', '321284022008', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420070107', '3212840204200701', '321284022007', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420060107', '3212840204200601', '321284022006', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420090108', '3212840204200901', '321284022010', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420080108', '3212840204200801', '321284022009', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420070108', '3212840204200701', '321284022008', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420060108', '3212840204200601', '321284022007', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420050108', '3212840204200501', '321284022006', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420090109', '3212840204200901', '321284022011', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420080109', '3212840204200801', '321284022010', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420070109', '3212840204200701', '321284022009', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420060109', '3212840204200601', '321284022008', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420050109', '3212840204200501', '321284022007', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420040109', '3212840204200401', '321284022006', '3212840209', '32128402')
--二附		                              
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220110107', '3212840202201101', '321284022011', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220100107', '3212840202201001', '321284022010', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220090107', '3212840202200901', '321284022009', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220080107', '3212840202200801', '321284022008', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220070107', '3212840202200701', '321284022007', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220060107', '3212840202200601', '321284022006', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220110108', '3212840202201101', '321284022012', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220100108', '3212840202201001', '321284022011', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220090108', '3212840202200901', '321284022010', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220080108', '3212840202200801', '321284022009', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220070108', '3212840202200701', '321284022008', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220060108', '3212840202200601', '321284022007', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220050108', '3212840202200501', '321284022006', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220110109', '3212840202201101', '321284022013', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220100109', '3212840202201001', '321284022012', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220090109', '3212840202200901', '321284022011', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220080109', '3212840202200801', '321284022010', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220070109', '3212840202200701', '321284022009', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220060109', '3212840202200601', '321284022008', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220050109', '3212840202200501', '321284022007', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220040109', '3212840202200401', '321284022006', '3212840209', '32128402')


--年级查询
drop view QGrade
go
create view QGrade
as
select a.*
,b.PartIDS
,b.StepIDS
,Name = b.Name + ' - ' + e.Name
,TreeName = b.StepName + ' - ' + e.Name
,EduName = e.Name
,PartName = b.PartName
,Graduated = ISNULL(b.Graduated, 1)
,IsCurrent = ISNULL(y.IsCurrent, 0)
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
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010701', 1, '321284020120160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010702', 2, '321284020120160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010703', 3, '321284020120160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010704', 4, '321284020120160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010705', 5, '321284020120160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010706', 6, '321284020120160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010707', 7, '321284020120160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010708', 8, '321284020120160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010709', 9, '321284020120160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010710', 10, '321284020120160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010711', 11, '321284020120160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010712', 12, '321284020120160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010713', 13, '321284020120160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010714', 14, '321284020120160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010715', 15, '321284020120160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010716', 16, '321284020120160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010717', 17, '321284020120160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010718', 18, '321284020120160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010719', 19, '321284020120160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010720', 20, '321284020120160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010721', 21, '321284020120160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010722', 22, '321284020120160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010723', 23, '321284020120160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010724', 24, '321284020120160107', null, null, '32128402')
--2015		    
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010801', 1, '321284020120150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010802', 2, '321284020120150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010803', 3, '321284020120150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010804', 4, '321284020120150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010805', 5, '321284020120150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010806', 6, '321284020120150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010807', 7, '321284020120150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010808', 8, '321284020120150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010809', 9, '321284020120150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010810', 10, '321284020120150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010811', 11, '321284020120150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010812', 12, '321284020120150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010813', 13, '321284020120150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010814', 14, '321284020120150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010815', 15, '321284020120150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010816', 16, '321284020120150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010817', 17, '321284020120150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010818', 18, '321284020120150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010819', 19, '321284020120150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010820', 20, '321284020120150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010821', 21, '321284020120150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010822', 22, '321284020120150108', null, null, '32128402')
--		    
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010701', 1, '321284020120150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010702', 2, '321284020120150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010703', 3, '321284020120150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010704', 4, '321284020120150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010705', 5, '321284020120150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010706', 6, '321284020120150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010707', 7, '321284020120150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010708', 8, '321284020120150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010709', 9, '321284020120150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010710', 10, '321284020120150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010711', 11, '321284020120150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010712', 12, '321284020120150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010713', 13, '321284020120150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010714', 14, '321284020120150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010715', 15, '321284020120150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010716', 16, '321284020120150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010717', 17, '321284020120150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010718', 18, '321284020120150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010719', 19, '321284020120150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010720', 20, '321284020120150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010721', 21, '321284020120150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012015010722', 22, '321284020120150107', null, null, '32128402')
--2014		    
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010901', 1, '321284020120140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010902', 2, '321284020120140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010903', 3, '321284020120140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010904', 4, '321284020120140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010905', 5, '321284020120140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010906', 6, '321284020120140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010907', 7, '321284020120140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010908', 8, '321284020120140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010909', 9, '321284020120140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010910', 10, '321284020120140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010911', 11, '321284020120140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010912', 12, '321284020120140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010913', 13, '321284020120140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010914', 14, '321284020120140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010915', 15, '321284020120140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010916', 16, '321284020120140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010917', 17, '321284020120140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010918', 18, '321284020120140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010919', 19, '321284020120140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010920', 20, '321284020120140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010921', 21, '321284020120140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010922', 22, '321284020120140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010923', 23, '321284020120140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010924', 24, '321284020120140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010925', 25, '321284020120140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010926', 26, '321284020120140109', null, null, '32128402')
--		    
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010801', 1, '321284020120140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010802', 2, '321284020120140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010803', 3, '321284020120140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010804', 4, '321284020120140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010805', 5, '321284020120140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010806', 6, '321284020120140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010807', 7, '321284020120140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010808', 8, '321284020120140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010809', 9, '321284020120140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010810', 10, '321284020120140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010811', 11, '321284020120140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010812', 12, '321284020120140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010813', 13, '321284020120140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010814', 14, '321284020120140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010815', 15, '321284020120140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010816', 16, '321284020120140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010817', 17, '321284020120140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010818', 18, '321284020120140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010819', 19, '321284020120140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010820', 20, '321284020120140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010821', 21, '321284020120140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010822', 22, '321284020120140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010823', 23, '321284020120140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010824', 24, '321284020120140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010825', 25, '321284020120140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010826', 26, '321284020120140108', null, null, '32128402')
--		    
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010701', 1, '321284020120140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010702', 2, '321284020120140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010703', 3, '321284020120140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010704', 4, '321284020120140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010705', 5, '321284020120140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010706', 6, '321284020120140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010707', 7, '321284020120140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010708', 8, '321284020120140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010709', 9, '321284020120140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010710', 10, '321284020120140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010711', 11, '321284020120140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010712', 12, '321284020120140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010713', 13, '321284020120140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010714', 14, '321284020120140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010715', 15, '321284020120140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010716', 16, '321284020120140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010717', 17, '321284020120140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010718', 18, '321284020120140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010719', 19, '321284020120140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010720', 20, '321284020120140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010721', 21, '321284020120140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010722', 22, '321284020120140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010723', 23, '321284020120140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010724', 24, '321284020120140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010725', 25, '321284020120140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '32128402012014010726', 26, '321284020120140107', null, null, '32128402')


--班级查询
drop view QBan
go
create view QBan
as
select b.*
,PartStepIDS = g.PartStepIDS
,g.YearIDS
,g.EduIDS
,Name = g.Name + '（' + RIGHT('00' + CAST(b.Num as nvarchar(5)), 2) + '）班'
,TreeName = g.EduName + '（' + RIGHT('00' + CAST(b.Num as nvarchar(5)), 2) + '）班'
,DataGridName = g.PartName + ' - ' + g.EduName + '（' + RIGHT('00' + CAST(b.Num as nvarchar(5)), 2) + '）班'
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



go
--学生去向
create table TOut
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,
	Name	nvarchar(10) not null,
	Value	nvarchar(10) not null,
	AccIDS	nvarchar(20) not null,
)

go

alter table TOut add constraint PK_TOut primary key clustered (ID)
create unique nonclustered index UN_TOut_IDS on TOut (IDS)

insert TOut values (Lower(REPLACE(NEWID(), '-','')), '3212840201', '毕业', '01', '32128402')
insert TOut values (Lower(REPLACE(NEWID(), '-','')), '3212840202', '休学', '02', '32128402')
insert TOut values (Lower(REPLACE(NEWID(), '-','')), '3212840203', '转出', '03', '32128402')
insert TOut values (Lower(REPLACE(NEWID(), '-','')), '3212840204', '外借', '04', '32128402')
insert TOut values (Lower(REPLACE(NEWID(), '-','')), '3212840205', '辍学', '05', '32128402')
insert TOut values (Lower(REPLACE(NEWID(), '-','')), '3212840206', '流生', '06', '32128402')


--学生来源
create table TCome
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,
	Name	nvarchar(10) not null,
	Value	nvarchar(20) not null,
	AccIDS	nvarchar(20) not null,
)

go

alter table TCome add constraint PK_TCome primary key clustered (ID)
create unique nonclustered index UN_TCome_IDS on TCome (IDS)

insert TCome values (Lower(REPLACE(NEWID(), '-','')), '3212840201', '应届生', '01', '32128402')
insert TCome values (Lower(REPLACE(NEWID(), '-','')), '3212840202', '休复生', '02', '32128402')
insert TCome values (Lower(REPLACE(NEWID(), '-','')), '3212840203', '借读生', '03', '32128402')
insert TCome values (Lower(REPLACE(NEWID(), '-','')), '3212840204', '借考生', '04', '32128402')
insert TCome values (Lower(REPLACE(NEWID(), '-','')), '3212840205', '转入生', '05', '32128402')
insert TCome values (Lower(REPLACE(NEWID(), '-','')), '3212840206', '重读生', '06', '32128402')




--学生表
create table TStudent
(
	ID	nvarchar(32) not null,	--唯一编号
	IDS	nvarchar(20) not null,	--学生编号
	Name	nvarchar(10) not null,	--姓名
	CID	nvarchar(20) ,	--身份证号
	FromSch	nvarchar(20),	--学校
	FromGrade	nvarchar(10),	--年级
	NationID	nvarchar(20),	--全国学籍号
	ReadState	nvarchar(20),	--就读状态
	IsProblem	bit not null,	--是否问题学籍
	--
	PartStepIDS	nvarchar(20) not null,	--校区分级编号
	--
	Code	nvarchar(20),	--学籍号
	--
	ComeDate	date,
	OutIDS	nvarchar(20),	--去向
	OutDate	date,
	--
	Mobil1	nvarchar(20),	--联系电话一
	Mobil2	nvarchar(20),	--联系电话二
	Name1	nvarchar(20),	--第一监护人
	Name2	nvarchar(20),	--第二监护人
	Home	nvarchar(50),	--家庭地址
	Birth	nvarchar(50),	--户籍地址
	Memo	nvarchar(50),	--备注
	Checked	bit not null,	--是否完成信息核对
	--
	AccIDS	nvarchar(20) not null,	--学校编号
	--
	OpenID	nvarchar(32),	--用户ID
)
go
alter table TStudent add constraint PK_TStudent primary key clustered (ID)
alter table TStudent add constraint FK_TStudent_OutIDS foreign key (OutIDS) references TOut (IDS)
create unique nonclustered index UN_TStudent_IDS on TStudent (IDS)
create index IN_TStudent_CID on TStudent (CID)
create index IN_TStudent_Name on TStudent (Name)
create index IN_TStudent_Code on TStudent (Code)

--年度学生
create table TGradeStud
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(32) not null,	--GradeIDS + StudIDS流水号
	GradeIDS	nvarchar(20) not null,
	StudIDS	nvarchar(20) not null,
	BanIDS	nvarchar(20) not null,
	OldBan	nvarchar(10) not null,	--原班级编号、考场号XXYY
	Choose	bit not null,	--学籍性质：是否择校生
	ComeIDS	nvarchar(20) not null,	--学生来源，应届、往届……
	GroupID	nvarchar(32),	--同分组编号，调动要一起调动
	Fixed	bit not null,	--固定的
	Score	int,	--总分
)
go

alter table TGradeStud add constraint PK_TGradeStud primary key clustered (ID)
alter table TGradeStud add constraint FK_TGradeStud_BanIDS foreign key (BanIDS) references TBan (IDS)
alter table TGradeStud add constraint FK_TGradeStud_StudIDS foreign key (StudIDS) references TStudent (IDS)
alter table TGradeStud add constraint FK_TGradeStud_GradeIDS foreign key (GradeIDS) references TGrade (IDS)
alter table TGradeStud add constraint FK_TGradeStud_ComeIDS foreign key (ComeIDS) references TCome (IDS)
create unique nonclustered index UN_TGradeStud_IDS on TGradeStud (IDS)



--年级学生查询
drop view QGradeStud
go
create view QGradeStud
as
select a.*
, BanName = b.Name
, b.DataGridName
, Graduated = ISNULL(b.Graduated, 1)
, StudName = c.Name
, StudSex = case CAST(SUBSTRING(c.CID , 17, 1) as int) % 2 when 1 then '男' when 0 then '女' end
, CID = c.CID
, ComeName = d.Name
, Checked = ISNULL(c.Checked, 0)
from TGradeStud a left join QBan b
on a.BanIDS = b.IDS
left join TStudent c
on a.StudIDS = c.IDS
left join TCome d
on a.ComeIDS = d.IDS
go




-------------------------------------------------------------------
---以下不算

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
