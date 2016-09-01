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
	Name	nvarchar(20) not null,
	Fixed	bit not null,	--是否使用
	AccIDS	nvarchar(20) not null
)
go
alter table TEdu add constraint PK_TEdu primary key clustered (ID)
create unique nonclustered index UN_TEdu_IDS on TEdu (IDS)


insert TEdu values (Lower(REPLACE(NEWID(), '-','')), '3212840201', '一年级', 0, '32128402')
insert TEdu values (Lower(REPLACE(NEWID(), '-','')), '3212840202', '二年级', 0, '32128402')
insert TEdu values (Lower(REPLACE(NEWID(), '-','')), '3212840203', '三年级', 0, '32128402')
insert TEdu values (Lower(REPLACE(NEWID(), '-','')), '3212840204', '四年级', 0, '32128402')
insert TEdu values (Lower(REPLACE(NEWID(), '-','')), '3212840205', '五年级', 0, '32128402')
insert TEdu values (Lower(REPLACE(NEWID(), '-','')), '3212840206', '六年级', 0, '32128402')
insert TEdu values (Lower(REPLACE(NEWID(), '-','')), '3212840207', '七年级', 1, '32128402')
insert TEdu values (Lower(REPLACE(NEWID(), '-','')), '3212840208', '八年级', 1, '32128402')
insert TEdu values (Lower(REPLACE(NEWID(), '-','')), '3212840209', '九年级', 1, '32128402')


--校区设置
create table TPart
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,	--32128402XX
	Name	nvarchar(20) not null,
	Fixed	bit not null,
	AccIDS	nvarchar(20) not null
)
go
alter table TPart add constraint PK_TPart primary key clustered (ID)
alter table TPart add constraint FK_TPart_AccID foreign key (AccIDS) references TAcc (IDS)
create unique nonclustered index UN_TPart_IDS on TPart (IDS)
create unique nonclustered index UN_TPart_Name on TPart (Name)


insert TPart values (Lower(REPLACE(NEWID(), '-','')), '3212840201', '实验初中', 1, '32128402')
insert TPart values (Lower(REPLACE(NEWID(), '-','')), '3212840202', '二附初中', 0, '32128402')
insert TPart values (Lower(REPLACE(NEWID(), '-','')), '3212840203', '二附三水', 0, '32128402')
insert TPart values (Lower(REPLACE(NEWID(), '-','')), '3212840204', '天目学校', 0, '32128402')

--分级设置
create table TStep
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,	--32128402XXXX
	Name	nvarchar(10) not null,	--级
	Fixed	bit not null,	--是否毕业
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
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402201601', '3212840201', '321284022016', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402201501', '3212840201', '321284022015', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402201401', '3212840201', '321284022014', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402201301', '3212840201', '321284022013', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402201201', '3212840201', '321284022012', '32128402')
--二附
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402201101', '3212840202', '321284022011', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402201001', '3212840202', '321284022010', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402200901', '3212840202', '321284022009', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402200801', '3212840202', '321284022008', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402200701', '3212840202', '321284022007', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402200601', '3212840202', '321284022006', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402200501', '3212840202', '321284022005', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402200401', '3212840202', '321284022004', '32128402')
--天目
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402201102', '3212840203', '321284022011', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402201002', '3212840203', '321284022010', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402200902', '3212840204', '321284022009', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402200802', '3212840204', '321284022008', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402200702', '3212840204', '321284022007', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402200602', '3212840204', '321284022006', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402200502', '3212840204', '321284022005', '32128402')
insert TPartStep values (Lower(REPLACE(NEWID(), '-','')), '32128402200402', '3212840204', '321284022004', '32128402')

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
create table TTerm
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,	--学期编号32128402YYYYXX
	Name	nvarchar(20) not null,	--学期名称：第一学期
	IsCurrent	bit not null,
	YearIDS	nvarchar(20) not null,
	AccIDS	nvarchar(20) not null,
)
go
alter table TTerm add constraint PK_TTerm primary key clustered (ID)
alter table TTerm add constraint FK_TTerm_AccIDS foreign key (AccIDS) references TAcc (IDS)
alter table TTerm add constraint FK_TTerm_YearIDS foreign key (YearIDS) references TYear (IDS)
create unique nonclustered index UN_TTerm_IDS on TTerm (IDS)

insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402200401', '第一学期', 0, '321284022004', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402200402', '第二学期', 0, '321284022004', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402200501', '第一学期', 0, '321284022005', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402200502', '第二学期', 0, '321284022005', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402200601', '第一学期', 0, '321284022006', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402200602', '第二学期', 0, '321284022006', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402200701', '第一学期', 0, '321284022007', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402200702', '第二学期', 0, '321284022007', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402200801', '第一学期', 0, '321284022008', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402200802', '第二学期', 0, '321284022008', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402200901', '第一学期', 0, '321284022009', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402200902', '第二学期', 0, '321284022009', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402201001', '第一学期', 0, '321284022010', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402201002', '第二学期', 0, '321284022010', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402201101', '第一学期', 0, '321284022011', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402201102', '第二学期', 0, '321284022011', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402201201', '第一学期', 0, '321284022012', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402201202', '第二学期', 0, '321284022012', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402201301', '第一学期', 0, '321284022013', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402201302', '第二学期', 0, '321284022013', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402201401', '第一学期', 0, '321284022014', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402201402', '第二学期', 0, '321284022014', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402201501', '第一学期', 0, '321284022015', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402201502', '第二学期', 0, '321284022015', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402201601', '第一学期', 1, '321284022016', '32128402')

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
--
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220160107', '32128402201601', '321284022016', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220150107', '32128402201501', '321284022015', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220140107', '32128402201401', '321284022014', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220130107', '32128402201301', '321284022013', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220120107', '32128402201201', '321284022012', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220110107', '32128402201101', '321284022011', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220100107', '32128402201001', '321284022010', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220090107', '32128402200901', '321284022009', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220080107', '32128402200801', '321284022008', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220070107', '32128402200701', '321284022007', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220060107', '32128402200601', '321284022006', '3212840207', '32128402')
--			        
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220150108', '32128402201501', '321284022016', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220140108', '32128402201401', '321284022015', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220130108', '32128402201301', '321284022014', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220120108', '32128402201201', '321284022013', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220110108', '32128402201101', '321284022012', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220100108', '32128402201001', '321284022011', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220090108', '32128402200901', '321284022010', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220080108', '32128402200801', '321284022009', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220070108', '32128402200701', '321284022008', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220060108', '32128402200601', '321284022007', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220050108', '32128402200501', '321284022006', '3212840208', '32128402')
--			        
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220140109', '32128402201401', '321284022016', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220130109', '32128402201301', '321284022015', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220120109', '32128402201201', '321284022014', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220110109', '32128402201101', '321284022013', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220100109', '32128402201001', '321284022012', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220090109', '32128402200901', '321284022011', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220080109', '32128402200801', '321284022010', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220070109', '32128402200701', '321284022009', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220060109', '32128402200601', '321284022008', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220050109', '32128402200501', '321284022007', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220040109', '32128402200401', '321284022006', '3212840209', '32128402')
--
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220110207', '32128402201102', '321284022011', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220100207', '32128402201002', '321284022010', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220090207', '32128402200902', '321284022009', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220080207', '32128402200802', '321284022008', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220070207', '32128402200702', '321284022007', '3212840207', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220060207', '32128402200602', '321284022006', '3212840207', '32128402')
--			        
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220110208', '32128402201102', '321284022012', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220100208', '32128402201002', '321284022011', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220090208', '32128402200902', '321284022010', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220080208', '32128402200802', '321284022009', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220070208', '32128402200702', '321284022008', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220060208', '32128402200602', '321284022007', '3212840208', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220050208', '32128402200502', '321284022006', '3212840208', '32128402')
--			        
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220110209', '32128402201102', '321284022013', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220100209', '32128402201002', '321284022012', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220090209', '32128402200902', '321284022011', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220080209', '32128402200802', '321284022010', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220070209', '32128402200702', '321284022009', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220060209', '32128402200602', '321284022008', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220050209', '32128402200502', '321284022007', '3212840209', '32128402')
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '3212840220040209', '32128402200402', '321284022006', '3212840209', '32128402')
--       

create table TBan
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,	--3212840220160107XX
	Name	int not null,
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
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022016010701', 1, '3212840220160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022016010702', 2, '3212840220160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022016010703', 3, '3212840220160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022016010704', 4, '3212840220160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022016010705', 5, '3212840220160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022016010706', 6, '3212840220160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022016010707', 7, '3212840220160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022016010708', 8, '3212840220160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022016010709', 9, '3212840220160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022016010710', 10, '3212840220160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022016010711', 11, '3212840220160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022016010712', 12, '3212840220160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022016010713', 13, '3212840220160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022016010714', 14, '3212840220160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022016010715', 15, '3212840220160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022016010716', 16, '3212840220160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022016010717', 17, '3212840220160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022016010718', 18, '3212840220160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022016010719', 19, '3212840220160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022016010720', 20, '3212840220160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022016010721', 21, '3212840220160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022016010722', 22, '3212840220160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022016010723', 23, '3212840220160107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022016010724', 24, '3212840220160107', null, null, '32128402')
--2015
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010801', 1, '3212840220150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010802', 2, '3212840220150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010803', 3, '3212840220150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010804', 4, '3212840220150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010805', 5, '3212840220150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010806', 6, '3212840220150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010807', 7, '3212840220150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010808', 8, '3212840220150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010809', 9, '3212840220150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010810', 10, '3212840220150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010811', 11, '3212840220150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010812', 12, '3212840220150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010813', 13, '3212840220150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010814', 14, '3212840220150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010815', 15, '3212840220150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010816', 16, '3212840220150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010817', 17, '3212840220150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010818', 18, '3212840220150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010819', 19, '3212840220150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010820', 20, '3212840220150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010821', 21, '3212840220150108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010822', 22, '3212840220150108', null, null, '32128402')
--
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010701', 1, '3212840220150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010702', 2, '3212840220150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010703', 3, '3212840220150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010704', 4, '3212840220150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010705', 5, '3212840220150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010706', 6, '3212840220150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010707', 7, '3212840220150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010708', 8, '3212840220150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010709', 9, '3212840220150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010710', 10, '3212840220150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010711', 11, '3212840220150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010712', 12, '3212840220150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010713', 13, '3212840220150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010714', 14, '3212840220150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010715', 15, '3212840220150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010716', 16, '3212840220150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010717', 17, '3212840220150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010718', 18, '3212840220150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010719', 19, '3212840220150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010720', 20, '3212840220150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010721', 21, '3212840220150107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022015010722', 22, '3212840220150107', null, null, '32128402')

--2014
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010901', 1, '3212840220140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010902', 2, '3212840220140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010903', 3, '3212840220140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010904', 4, '3212840220140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010905', 5, '3212840220140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010906', 6, '3212840220140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010907', 7, '3212840220140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010908', 8, '3212840220140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010909', 9, '3212840220140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010910', 10, '3212840220140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010911', 11, '3212840220140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010912', 12, '3212840220140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010913', 13, '3212840220140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010914', 14, '3212840220140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010915', 15, '3212840220140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010916', 16, '3212840220140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010917', 17, '3212840220140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010918', 18, '3212840220140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010919', 19, '3212840220140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010920', 20, '3212840220140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010921', 21, '3212840220140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010922', 22, '3212840220140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010923', 23, '3212840220140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010924', 24, '3212840220140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010925', 25, '3212840220140109', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010926', 26, '3212840220140109', null, null, '32128402')
--
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010801', 1, '3212840220140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010802', 2, '3212840220140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010803', 3, '3212840220140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010804', 4, '3212840220140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010805', 5, '3212840220140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010806', 6, '3212840220140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010807', 7, '3212840220140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010808', 8, '3212840220140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010809', 9, '3212840220140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010810', 10, '3212840220140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010811', 11, '3212840220140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010812', 12, '3212840220140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010813', 13, '3212840220140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010814', 14, '3212840220140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010815', 15, '3212840220140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010816', 16, '3212840220140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010817', 17, '3212840220140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010818', 18, '3212840220140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010819', 19, '3212840220140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010820', 20, '3212840220140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010821', 21, '3212840220140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010822', 22, '3212840220140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010823', 23, '3212840220140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010824', 24, '3212840220140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010825', 25, '3212840220140108', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010826', 26, '3212840220140108', null, null, '32128402')
--
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010701', 1, '3212840220140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010702', 2, '3212840220140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010703', 3, '3212840220140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010704', 4, '3212840220140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010705', 5, '3212840220140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010706', 6, '3212840220140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010707', 7, '3212840220140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010708', 8, '3212840220140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010709', 9, '3212840220140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010710', 10, '3212840220140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010711', 11, '3212840220140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010712', 12, '3212840220140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010713', 13, '3212840220140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010714', 14, '3212840220140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010715', 15, '3212840220140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010716', 16, '3212840220140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010717', 17, '3212840220140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010718', 18, '3212840220140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010719', 19, '3212840220140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010720', 20, '3212840220140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010721', 21, '3212840220140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010722', 22, '3212840220140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010723', 23, '3212840220140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010724', 24, '3212840220140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010725', 25, '3212840220140107', null, null, '32128402')
insert TBan values (Lower(REPLACE(NEWID(), '-','')), '321284022014010726', 26, '3212840220140107', null, null, '32128402')


--班级查询
go
create view QBan
as
select b.*
,PartStepIDS = g.PartStepIDS
,PartIDS = p.IDS
,StepIDS = s.IDS
,YearIDS = y.IDS
,EduIDS = e.IDS
,BanName = p.Name + ' - ' + s.Name + '级 - ' + e.Name + '（' +cast(b.Name as nvarchar(20)) + '）班'
,GradeName = p.Name + ' - ' + s.Name + '级 - ' + e.Name
,PartStepName = p.Name + ' - ' + s.Name + '级'
,PartName = p.Name
,StepName = s.Name
,EduName = e.Name
,MasterName = m.Name
,GroupName = gp.Name
from TBan b left join TGrade g
on b.GradeIDS = g.IDS
left join TPartStep ps
on g.PartStepIDS = ps.IDS
left join TPart p
on ps.PartIDS = p.IDS
left join TStep s
on ps.StepIDS = s.IDS
left join TYear y
on g.YearIDS = y.IDS
left join TEdu e
on g.EduIDS = e.IDS
left join TAcc m
on b.MasterIDS = m.IDS
left join TAcc gp
on b.GroupIDS = gp.IDS
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
create view QBanStud
as
select a.*, b.
from TBanStud a left join QBan b
on a.BanIDS = b.IDS
left join TStudent c
on a.StudIDS = c.IDS


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