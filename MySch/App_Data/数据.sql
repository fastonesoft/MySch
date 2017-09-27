

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

ALTER DATABASE MySch COLLATE Chinese_PRC_CI_AS
--Chinese_PRC_CI_AS  Chinese_PRC_BIN

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





--权限设置--
--权限分类
--  相当于对进行标题分类
create table ARoleType
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,
	Name	nvarchar(20) not null,
)
go
alter table ARoleType add constraint PK_ARoleType primary key clustered (ID)
create unique nonclustered index UN_ARoleType_IDS on ARoleType (IDS)
create unique nonclustered index UN_ARoleType_Name on ARoleType (Name)

insert ARoleType values (Lower(REPLACE(NEWID(), '-','')), '01', '系统设置')
insert ARoleType values (Lower(REPLACE(NEWID(), '-','')), '02', '模板定制')
insert ARoleType values (Lower(REPLACE(NEWID(), '-','')), '03', '权限分配')
insert ARoleType values (Lower(REPLACE(NEWID(), '-','')), '04', '用户管理')
insert ARoleType values (Lower(REPLACE(NEWID(), '-','')), '05', '有客来访')
insert ARoleType values (Lower(REPLACE(NEWID(), '-','')), '06', '教师相关')
insert ARoleType values (Lower(REPLACE(NEWID(), '-','')), '07', '主任工作')
insert ARoleType values (Lower(REPLACE(NEWID(), '-','')), '08', '备课计划')
insert ARoleType values (Lower(REPLACE(NEWID(), '-','')), '09', '年级分工')
insert ARoleType values (Lower(REPLACE(NEWID(), '-','')), '10', '学校安排')
insert ARoleType values (Lower(REPLACE(NEWID(), '-','')), '11', '集团设置')
insert ARoleType values (Lower(REPLACE(NEWID(), '-','')), '12', '局里有约')


--权限内容
--  用Action自动添加
create table ARoleAction
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(100) not null,	--url
	Name	nvarchar(20) not null,	--动作名称
	IsMenu	bit not null,	--是否作菜单项
	RoleTypeIDS	nvarchar(20) not null,	--权限分类编号
)
go
alter table ARoleAction add constraint PK_ARoleAction primary key clustered (ID)
alter table ARoleAction add constraint FK_ARoleAction_RoleTypeIDS foreign key (RoleTypeIDS) references ARoleType (IDS)
create unique nonclustered index UN_ARoleAction_IDS on ARoleAction (IDS)
create unique nonclustered index UN_ARoleAction_Name on ARoleAction (Name)


--权限分组
create table ARoleGroup
(
	ID	nvarchar(32) not null,
	IDS	int not null,
	Name	nvarchar(20) not null,	--教师、班主任、年管会、学校、教育局、管理员……
	GroupRole	nvarchar(max),	--RoleActionIDS列表，以,分隔
)
go

alter table ARoleGroup add constraint PK_ARoleGroup primary key clustered (ID)
create unique nonclustered index UN_ARoleGroup_IDS on ARoleGroup (IDS)


insert ARoleGroup values (Lower(REPLACE(NEWID(), '-','')), 0, '游客', null)
insert ARoleGroup values (Lower(REPLACE(NEWID(), '-','')), 1, '教师', null)
insert ARoleGroup values (Lower(REPLACE(NEWID(), '-','')), 2, '班主任', null)
insert ARoleGroup values (Lower(REPLACE(NEWID(), '-','')), 3, '备课组长', null)
insert ARoleGroup values (Lower(REPLACE(NEWID(), '-','')), 4, '年级组长', null)
insert ARoleGroup values (Lower(REPLACE(NEWID(), '-','')), 5, '学校管理员', null)
insert ARoleGroup values (Lower(REPLACE(NEWID(), '-','')), 6, '教育集团总长', null)
insert ARoleGroup values (Lower(REPLACE(NEWID(), '-','')), 7, '教育局负责人', null)
insert ARoleGroup values (Lower(REPLACE(NEWID(), '-','')), 99, '系统管理员', null)





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
	ID	nvarchar(32) not null,	--unionid
	IDS	nvarchar(32) not null,	--编号
	Name	nvarchar(20) not null,	--帐号全称、姓名
	RoleGroupIDS	int not null,	--帐号类型
	RegTime	datetime not null,	--注册时间
	Passed	bit not null,	--是否通过审核
	Fixed	bit not null,	--是否冻结
	Valided	nvarchar(32) not null,	--信息验证
	ParentID	nvarchar(32),
)
go
alter table TAcc add constraint PK_TAcc primary key clustered (ID)
alter table TAcc add constraint FK_TAcc_RoleGroupIDS foreign key (RoleGroupIDS) references ARoleGroup (IDS)
create unique nonclustered index UN_TAcc_IDS on TAcc (IDS)

--插入管理员
insert TAcc values ('o47ZhvzWPWSNS26vG_45Fuz5JMZk','admin','系统管理员', 99,'2017-05-10 12:00:00',  1, 0, '471cc448fe732d7b61994e3615f0b1de',  null)
insert TAcc values ('o47ZhvxoQA9QOOgDSZ5hGaea4xdI','32128402','实验初中', 1,'2017-05-10 12:00:00', 1, 0, '02eb64efa485fad6640c8456fa67e267', 'o47ZhvzWPWSNS26vG_45Fuz5JMZk')
go



--登录日志
create table TLogin
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(32) not null,
	IP	nvarchar(32) not null,
	Name	nvarchar(20) not null,
	Pwd	nvarchar(32) not null,
	Brower	nvarchar(32) not null,
	LoginMsg	nvarchar(32) not null,
	LoginTime	datetime not null,	--登录时间
)
go
alter table TLogin add constraint PK_TLogin primary key clustered (ID)
create index IN_TLogin_Name on TLogin (Name)

--打印位置记录
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
	Value	int not null,
	Fixed	bit not null,	--是否使用
	AccIDS	nvarchar(32) not null
)
go
alter table TEdu add constraint PK_TEdu primary key clustered (ID)
alter table TEdu add constraint FK_TEdu_AccID foreign key (AccIDS) references TAcc (IDS)
create unique nonclustered index UN_TEdu_IDS on TEdu (IDS)


insert TEdu values (Lower(REPLACE(NEWID(), '-','')), '3212840201', '一年级', 1, 0, '32128402')
insert TEdu values (Lower(REPLACE(NEWID(), '-','')), '3212840202', '二年级', 2, 0, '32128402')
insert TEdu values (Lower(REPLACE(NEWID(), '-','')), '3212840203', '三年级', 3, 0, '32128402')
insert TEdu values (Lower(REPLACE(NEWID(), '-','')), '3212840204', '四年级', 4, 0, '32128402')
insert TEdu values (Lower(REPLACE(NEWID(), '-','')), '3212840205', '五年级', 5, 0, '32128402')
insert TEdu values (Lower(REPLACE(NEWID(), '-','')), '3212840206', '六年级', 6, 0, '32128402')
insert TEdu values (Lower(REPLACE(NEWID(), '-','')), '3212840207', '七年级', 7, 1, '32128402')
insert TEdu values (Lower(REPLACE(NEWID(), '-','')), '3212840208', '八年级', 8, 1, '32128402')
insert TEdu values (Lower(REPLACE(NEWID(), '-','')), '3212840209', '九年级', 9, 1, '32128402')


--校区设置
create table TPart
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,	--32128402XX
	Name	nvarchar(10) not null,
	Value	nvarchar(2) not null,
	Fixed	bit not null,	--是否不用
	AccIDS	nvarchar(32) not null
)
go
alter table TPart add constraint PK_TPart primary key clustered (ID)
alter table TPart add constraint FK_TPart_AccID foreign key (AccIDS) references TAcc (IDS)
create unique nonclustered index UN_TPart_IDS on TPart (IDS)
create unique nonclustered index UN_TPart_Name on TPart (Name)


insert TPart values (Lower(REPLACE(NEWID(), '-','')), '3212840201', '实验初中', '02', 0, '32128402')
insert TPart values (Lower(REPLACE(NEWID(), '-','')), '3212840202', '二附初中', '02', 1, '32128402')
insert TPart values (Lower(REPLACE(NEWID(), '-','')), '3212840203', '二附三水', '03', 1, '32128402')
insert TPart values (Lower(REPLACE(NEWID(), '-','')), '3212840204', '天目学校', '04', 1, '32128402')
insert TPart values (Lower(REPLACE(NEWID(), '-','')), '3212840211', '桥头初中', '11', 0, '32128402')
insert TPart values (Lower(REPLACE(NEWID(), '-','')), '3212840213', '张沐初中', '13', 0, '32128402')
insert TPart values (Lower(REPLACE(NEWID(), '-','')), '3212840214', '白米初中', '14', 0, '32128402')
insert TPart values (Lower(REPLACE(NEWID(), '-','')), '3212840216', '娄庄中学', '16', 0, '32128402')





--校区分级
create table TStep
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,	--32128402012016XX
	Name	nvarchar(20) not null,	--级
	Value	nvarchar(20) not null,	--级编号
	Graduated	bit not null,	--是否毕业
	CanRecruit	bit not null,	--能否招生（要放到Grade中）
	PartIDS	nvarchar(20) not null,
	AccIDS	nvarchar(32) not null,
)
go
alter table TStep add constraint PK_TStep primary key clustered (ID)
alter table TStep add constraint FK_TStep_PartIDS foreign key (PartIDS) references TPart (IDS)
alter table TStep add constraint FK_TStep_AccIDS foreign key (AccIDS) references TAcc (IDS)
create unique nonclustered index UN_TStep_IDS on TStep (IDS)
go

--实验
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '3212840201201701', '2017级', '201701', 0, 1, '3212840201', '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '3212840201201601', '2016级', '201601', 0, 0, '3212840201', '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '3212840201201501', '2015级', '201501', 0, 0, '3212840201', '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '3212840201201401', '2014级', '201401', 0, 0, '3212840201', '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '3212840201201301', '2013级', '201301', 1, 0, '3212840201', '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '3212840201201201', '2012级', '201201', 1, 0, '3212840201', '32128402')
--二附			      
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '3212840202201101', '2011级', '201101', 1, 0, '3212840202', '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '3212840202201001', '2010级', '201001', 1, 0, '3212840202', '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '3212840202200901', '2009级', '200901', 1, 0, '3212840202', '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '3212840202200801', '2008级', '200801', 1, 0, '3212840202', '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '3212840202200701', '2007级', '200701', 1, 0, '3212840202', '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '3212840202200601', '2006级', '200601', 1, 0, '3212840202', '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '3212840202200501', '2005级', '200501', 1, 0, '3212840202', '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '3212840202200401', '2004级', '200401', 1, 0, '3212840202', '32128402')
--三水			      
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '3212840203201101', '2011级', '201101', 1, 0, '3212840203', '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '3212840203201001', '2010级', '201001', 1, 0, '3212840203', '32128402')
--天目			      
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '3212840204200901', '2009级', '200901', 1, 0, '3212840204', '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '3212840204200801', '2008级', '200801', 1, 0, '3212840204', '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '3212840204200701', '2007级', '200701', 1, 0, '3212840204', '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '3212840204200601', '2006级', '200601', 1, 0, '3212840204', '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '3212840204200501', '2005级', '200501', 1, 0, '3212840204', '32128402')
insert TStep values (Lower(REPLACE(NEWID(), '-','')), '3212840204200401', '2004级', '200401', 1, 0, '3212840204', '32128402')


--年度设置
create table TYear
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,	--年度编号32128402XXXX
	Name	nvarchar(10) not null,	--年度2016
	IsCurrent	bit not null,	--当前年度
	AccIDS	nvarchar(32) not null	--所属学校
)
go
alter table TYear add constraint PK_TYear primary key clustered (ID)
alter table TYear add constraint FK_TYear_AccIDS foreign key (AccIDS) references TAcc (IDS)
create unique nonclustered index UN_TYear_IDS on TYear (IDS)
go
insert TYear values (Lower(REPLACE(NEWID(), '-','')), '321284022017', '2017', 1, '32128402')
insert TYear values (Lower(REPLACE(NEWID(), '-','')), '321284022016', '2016', 0, '32128402')
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
	AccIDS	nvarchar(32) not null,
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
	AccIDS	nvarchar(32) not null,
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

insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402201602', 1, '321284022016', '3212840202', '32128402')
insert TTerm values (Lower(REPLACE(NEWID(), '-','')), '32128402201701', 1, '321284022017', '3212840201', '32128402')

--年级设置
create table TGrade
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(32) not null,	--年级编号3212840201201601XX
	StepIDS	nvarchar(20) not null,	--分级
	YearIDS	nvarchar(20) not null,	--年度
	EduIDS	nvarchar(20) not null,	--学制
	AccIDS	nvarchar(32) not null,	--用户
	CanFeng	bit not null,	
	TakeNum	int not null,
	GoneModel	bit not null,
	GoneList	nvarchar(max),
)
go
alter table TGrade add constraint PK_TGrade primary key clustered (ID)
alter table TGrade add constraint FK_TGrade_StepIDS foreign key (StepIDS) references TStep (IDS)
alter table TGrade add constraint FK_TGrade_YearIDS foreign key (YearIDS) references TYear (IDS)
alter table TGrade add constraint FK_TGrade_EduIDS foreign key (EduIDS) references TEdu (IDS)
alter table TGrade add constraint FK_TGrade_AccIDS foreign key (AccIDS) references TAcc (IDS)
create unique nonclustered index UN_TGrade_IDS on TGrade (IDS)
--实验
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020120170107', '3212840201201701', '321284022017', '3212840207', '32128402', 1, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020120160108', '3212840201201601', '321284022017', '3212840208', '32128402', 1, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020120150109', '3212840201201501', '321284022017', '3212840209', '32128402', 1, 2, 1, NULL)

insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020120160107', '3212840201201601', '321284022016', '3212840207', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020120150107', '3212840201201501', '321284022015', '3212840207', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020120150108', '3212840201201501', '321284022016', '3212840208', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020120140107', '3212840201201401', '321284022014', '3212840207', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020120140108', '3212840201201401', '321284022015', '3212840208', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020120140109', '3212840201201401', '321284022016', '3212840209', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020120130107', '3212840201201301', '321284022013', '3212840207', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020120130108', '3212840201201301', '321284022014', '3212840208', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020120130109', '3212840201201301', '321284022015', '3212840209', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020120120107', '3212840201201201', '321284022012', '3212840207', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020120120108', '3212840201201201', '321284022013', '3212840208', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020120120109', '3212840201201201', '321284022014', '3212840209', '32128402', 0, 2, 1, NULL)
--三水		                              
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020320110108', '3212840203201101', '321284022012', '3212840208', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020320110109', '3212840203201101', '321284022013', '3212840209', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020320100109', '3212840203201001', '321284022012', '3212840209', '32128402', 0, 2, 1, NULL)
--天目		                              
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420090107', '3212840204200901', '321284022009', '3212840207', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420080107', '3212840204200801', '321284022008', '3212840207', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420070107', '3212840204200701', '321284022007', '3212840207', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420060107', '3212840204200601', '321284022006', '3212840207', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420090108', '3212840204200901', '321284022010', '3212840208', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420080108', '3212840204200801', '321284022009', '3212840208', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420070108', '3212840204200701', '321284022008', '3212840208', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420060108', '3212840204200601', '321284022007', '3212840208', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420050108', '3212840204200501', '321284022006', '3212840208', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420090109', '3212840204200901', '321284022011', '3212840209', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420080109', '3212840204200801', '321284022010', '3212840209', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420070109', '3212840204200701', '321284022009', '3212840209', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420060109', '3212840204200601', '321284022008', '3212840209', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420050109', '3212840204200501', '321284022007', '3212840209', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020420040109', '3212840204200401', '321284022006', '3212840209', '32128402', 0, 2, 1, NULL)
--二附		                              
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220110107', '3212840202201101', '321284022011', '3212840207', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220100107', '3212840202201001', '321284022010', '3212840207', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220090107', '3212840202200901', '321284022009', '3212840207', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220080107', '3212840202200801', '321284022008', '3212840207', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220070107', '3212840202200701', '321284022007', '3212840207', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220060107', '3212840202200601', '321284022006', '3212840207', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220110108', '3212840202201101', '321284022012', '3212840208', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220100108', '3212840202201001', '321284022011', '3212840208', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220090108', '3212840202200901', '321284022010', '3212840208', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220080108', '3212840202200801', '321284022009', '3212840208', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220070108', '3212840202200701', '321284022008', '3212840208', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220060108', '3212840202200601', '321284022007', '3212840208', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220050108', '3212840202200501', '321284022006', '3212840208', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220110109', '3212840202201101', '321284022013', '3212840209', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220100109', '3212840202201001', '321284022012', '3212840209', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220090109', '3212840202200901', '321284022011', '3212840209', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220080109', '3212840202200801', '321284022010', '3212840209', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220070109', '3212840202200701', '321284022009', '3212840209', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220060109', '3212840202200601', '321284022008', '3212840209', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220050109', '3212840202200501', '321284022007', '3212840209', '32128402', 0, 2, 1, NULL)
insert TGrade values (Lower(REPLACE(NEWID(), '-','')), '321284020220040109', '3212840202200401', '321284022006', '3212840209', '32128402', 0, 2, 1, NULL)



create table TBan
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,	--3212840220160107XX
	Num	nvarchar(10) not null,
	GradeIDS	nvarchar(32) not null,
	MasterIDS	nvarchar(32),
	AccIDS	nvarchar(32) not null,
	NotFeng	bit not null,
	OnlyFixed	bit not null,
	ChangeNum	int not null,
	Differ	int not null,
	IsAbs	bit not null,
	SameSex	bit not null,
)
go
alter table TBan add constraint PK_TBan primary key clustered (ID)
alter table TBan add constraint FK_TBan_GradeIDS foreign key (GradeIDS) references TGrade (IDS)
alter table TBan add constraint FK_TBan_MasterIDS foreign key (MasterIDS) references TAcc (ID)
alter table TBan add constraint FK_TBan_AccIDS foreign key (AccIDS) references TAcc (IDS)
create unique nonclustered index UN_TBan_IDS on TBan (IDS)

--2017级

--2016级

--学科设置
create table TSub
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,
	AccIDS	nvarchar(32) not null,
	Value	nvarchar(10) not null,
	Name	nvarchar(10) not null,
	SName	nvarchar(1) not null,
	Fixed	bit not null,
)
go

alter table TSub add constraint PK_TSub primary key clustered (ID)
alter table TSub add constraint FK_TSub_AccIDS foreign key (AccIDS) references TAcc (IDS)
create unique nonclustered index UN_TSub_IDS on TSub (IDS)

insert TSub values (Lower(REPLACE(NEWID(), '-','')), '3212840201', '32128402', '01', '语文', '语', 0)
insert TSub values (Lower(REPLACE(NEWID(), '-','')), '3212840202', '32128402', '02', '数字', '数', 0)
insert TSub values (Lower(REPLACE(NEWID(), '-','')), '3212840203', '32128402', '03', '英语', '英', 0)
insert TSub values (Lower(REPLACE(NEWID(), '-','')), '3212840204', '32128402', '04', '物理', '物', 0)
insert TSub values (Lower(REPLACE(NEWID(), '-','')), '3212840205', '32128402', '05', '化学', '化', 0)
insert TSub values (Lower(REPLACE(NEWID(), '-','')), '3212840206', '32128402', '06', '政治', '政', 0)
insert TSub values (Lower(REPLACE(NEWID(), '-','')), '3212840207', '32128402', '07', '历史', '历', 0)
insert TSub values (Lower(REPLACE(NEWID(), '-','')), '3212840208', '32128402', '08', '地理', '地', 0)
insert TSub values (Lower(REPLACE(NEWID(), '-','')), '3212840209', '32128402', '09', '生物', '生', 0)
insert TSub values (Lower(REPLACE(NEWID(), '-','')), '3212840210', '32128402', '10', '体育', '体', 0)
insert TSub values (Lower(REPLACE(NEWID(), '-','')), '3212840211', '32128402', '11', '音乐', '音', 0)
insert TSub values (Lower(REPLACE(NEWID(), '-','')), '3212840212', '32128402', '12', '美术', '美', 0)
insert TSub values (Lower(REPLACE(NEWID(), '-','')), '3212840213', '32128402', '13', '信息', '信', 0)
insert TSub values (Lower(REPLACE(NEWID(), '-','')), '3212840214', '32128402', '14', '口语', '口', 0)
insert TSub values (Lower(REPLACE(NEWID(), '-','')), '3212840215', '32128402', '15', '听力', '听', 0)



--学生去向
create table StudOut
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,
	Name	nvarchar(10) not null,
	Value	nvarchar(10) not null,
	CanReturn	bit not null,	--能否回校（同届的学校）
	AccIDS	nvarchar(32) not null,
)

go

alter table StudOut add constraint PK_StudOut primary key clustered (ID)
create unique nonclustered index UN_StudOut_IDS on StudOut (IDS)

insert StudOut values (Lower(REPLACE(NEWID(), '-','')), '3212840201', '毕业', '01', 0, '32128402')
insert StudOut values (Lower(REPLACE(NEWID(), '-','')), '3212840202', '升学', '02', 0, '32128402')
insert StudOut values (Lower(REPLACE(NEWID(), '-','')), '3212840203', '休学', '03', 0, '32128402')
insert StudOut values (Lower(REPLACE(NEWID(), '-','')), '3212840204', '转出', '04', 0, '32128402')
insert StudOut values (Lower(REPLACE(NEWID(), '-','')), '3212840205', '外借', '05', 1, '32128402')
insert StudOut values (Lower(REPLACE(NEWID(), '-','')), '3212840206', '辍学', '06', 1, '32128402')
insert StudOut values (Lower(REPLACE(NEWID(), '-','')), '3212840207', '流生', '07', 1, '32128402')
insert StudOut values (Lower(REPLACE(NEWID(), '-','')), '3212840208', '其他', '08', 1, '32128402')
insert StudOut values (Lower(REPLACE(NEWID(), '-','')), '3212840299', '临时', '99', 1, '32128402')







--学生来源
create table StudCome
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,
	Name	nvarchar(10) not null,
	Value	nvarchar(20) not null,
	AccIDS	nvarchar(32) not null,
)

go

alter table StudCome add constraint PK_StudCome primary key clustered (ID)
create unique nonclustered index UN_StudCome_IDS on StudCome (IDS)

insert StudCome values (Lower(REPLACE(NEWID(), '-','')), '3212840201', '应届生', '01', '32128402')
insert StudCome values (Lower(REPLACE(NEWID(), '-','')), '3212840202', '休复生', '02', '32128402')
insert StudCome values (Lower(REPLACE(NEWID(), '-','')), '3212840203', '借读生', '03', '32128402')
insert StudCome values (Lower(REPLACE(NEWID(), '-','')), '3212840204', '借考生', '04', '32128402')
insert StudCome values (Lower(REPLACE(NEWID(), '-','')), '3212840205', '转入生', '05', '32128402')
insert StudCome values (Lower(REPLACE(NEWID(), '-','')), '3212840206', '重读生', '06', '32128402')


--学生表
create table Student
(
	ID	nvarchar(32) not null,	--唯一编号
	IDS	nvarchar(32) not null,	--学生编号
	--报名信息记录
	IDC	nvarchar(20) not null,	--身份证号
	Name	nvarchar(10) not null,	--姓名
	StepIDS	nvarchar(20) not null,	--校区分级编号
	FromSch	nvarchar(64),	--毕业小学
	SchChoose	bit not null,	--是否择校
	--
	RegNo	nvarchar(10),	--注册编号
	RegUID	nvarchar(32),	--注册人
	--
	Examed	bit not null,	--是否审核通过
	ExamUID	nvarchar(32),	--审核人
	ExamUIDe	nvarchar(32),	--复核人
	--
	Mobil1	nvarchar(20),	--联系电话一
	Mobil2	nvarchar(20),	--联系电话二
	Name1	nvarchar(20),	--第一监护人
	Name2	nvarchar(20),	--第二监护人
	Home	nvarchar(50),	--家庭地址
	Birth	nvarchar(50),	--户籍地址
	Fixed	bit not null,	--是否确认
	--
	Memo	nvarchar(50),	--备注
	--
	AccIDS	nvarchar(32) not null,	--学校编号
	--
)
go
alter table Student add constraint PK_Student primary key clustered (ID)
alter table Student add constraint FK_Student_StepIDS foreign key (StepIDS) references TStep (IDS)
create unique nonclustered index UN_Student_IDS on Student (IDS)
create unique nonclustered index UN_Student_IDC on Student (IDC)
create index IN_Student_Name on Student (Name)
create index IN_Student_RegUID on Student (RegUID)



--年度学生
create table StudGrade
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(32) not null,	--GradeIDS + 流水号
	GradeIDS	nvarchar(32) not null,
	BanIDS	nvarchar(20) not null,
	OldBan	nvarchar(10) not null,	--原班级编号、考场号XXYY
	StudIDS	nvarchar(32) not null,
	StudCode	nvarchar(20),	--学籍号
	Choose	bit not null,	--学籍性质：是否择校生
	ComeIDS	nvarchar(20) not null,	--学生来源
	ComeTime	date,	--进校时间
	GroupID	nvarchar(32),	--同分组编号，调动要一起调动
	Fixed	bit not null,	--固定的
	Score	int,	--总分
	OutIDS	nvarchar(20),	--离校说明
	OutTime	date,	--离校时间
	InSch	bit not null,	--是否在校
)
go

alter table StudGrade add constraint PK_StudGrade primary key clustered (ID)
alter table StudGrade add constraint FK_StudGrade_BanIDS foreign key (BanIDS) references TBan (IDS)
alter table StudGrade add constraint FK_StudGrade_StudIDS foreign key (StudIDS) references Student (IDS)
alter table StudGrade add constraint FK_StudGrade_GradeIDS foreign key (GradeIDS) references TGrade (IDS)
alter table StudGrade add constraint FK_StudGrade_ComeIDS foreign key (ComeIDS) references StudCome (IDS)
create unique nonclustered index UN_StudGrade_IDS on StudGrade (IDS)


--分班中转表
create table StudGradeMove
(
	ID	nvarchar(32) not null,	--StudGrade编号
	IDS	nvarchar(32) not null,	--StudGrade编号
	BanIDS	nvarchar(20) not null,	--学生班级编号
	OwnerIDS	nvarchar(32) not null,	--请求的班主任  
			--两个学生一组，完成检测  
)
go
alter table StudGradeMove add constraint PK_StudGradeMove primary key clustered (ID)
create unique nonclustered index UN_StudGradeMove_IDS on StudGradeMove (IDS)
alter table StudGradeMove add constraint FK_StudGradeMove_BanIDS foreign key (BanIDS) references TBan (IDS)
--alter table StudGradeMove add constraint FK_StudGradeMove_OwnerAccIDS foreign key (OwnerAccIDS) references TAcc (ID)


--年度学生列表类型
create table StudGradeType
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(32) not null,
	Name	nvarchar(20) not null,	--年度学生列表类型名称
	AccIDS	nvarchar(32) not null,	--学校编号
)
go

--年度学生列表
create table StudGradeTable
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(32) not null,
	GradeIDS	nvarchar(32) not null,	--年度编号
	TableName	nvarchar(20) not null,	--年度学生列表名称
	TypeIDS	nvarchar(20) not null,	--年度学生列表类型
	Memo	nvarchar(100),
)
go

--年度学生明细
create table StudGradeField
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(32) not null,
	TableIDS	nvarchar(32) not null,
	FieldName	nvarchar(20) not null,
)
go


----考试相关

--考场设置
create table KaoPlace
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,	--AccIDS + PlaceNo
	AccIDS	nvarchar(32) not null,
	PlaceNo	nvarchar(2) not null,
	Fixed	bit not null,
)
go
alter table KaoPlace add constraint PK_KaoPlace primary key clustered (ID)
alter table KaoPlace add constraint FK_KaoPlace_AccIDS foreign key (AccIDS) references TAcc (AccIDS)
create unique nonclustered index UN_KaoPlace_IDS on KaoPlace (IDS)


--考试类型
create table KaoType
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,	--AccIDS + Value
	AccIDS	nvarchar(32) not null,
	Name	nvarchar(20) not null,	--类型名称
	Value	nvarchar(2) not null,
	Fixed	bit not null,
)
go
alter table KaoType add constraint PK_KaoType primary key clustered (ID)
alter table KaoType add constraint FK_KaoType_AccIDS foreign key (AccIDS) references TAcc (AccIDS)
create unique nonclustered index UN_KaoType_IDS on KaoType (IDS)

insert KaoType values (Lower(REPLACE(NEWID(), '-','')), '3212840201', '32128402', '周练', '01', 0)
insert KaoType values (Lower(REPLACE(NEWID(), '-','')), '3212840202', '32128402', '月考', '02', 0)
insert KaoType values (Lower(REPLACE(NEWID(), '-','')), '3212840203', '32128402', '统考', '03', 0)
insert KaoType values (Lower(REPLACE(NEWID(), '-','')), '3212840204', '32128402', '调研', '04', 0)
insert KaoType values (Lower(REPLACE(NEWID(), '-','')), '3212840205', '32128402', '中考', '05', 0)


--一、考试
create table Kao
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(32) not null,	--TermIDS + Value
	TermIDS	nvarchar(32) not null,
	OwnerIDS	nvarchar(32) not null,
	KaoTypeIDS	nvarchar(32) not null,
	CreateTime	datetime not null,
	Name	nvarchar(32) not null,
	Value	nvarchar(10) not null,
	KaoCoded	bit not null,
)
go
alter table Kao add constraint PK_Kao primary key clustered (ID)
alter table Kao add constraint FK_Kao_TermIDS foreign key (TermIDS) references TTerm (IDS)
alter table Kao add constraint FK_Kao_OwnerIDS foreign key (OwnerIDS) references TAcc (IDS)
alter table Kao add constraint FK_Kao_KaoTypeIDS foreign key (KaoTypeIDS) references KaoType (IDS)
create unique nonclustered index UN_Kao_IDS on Kao (IDS)

--考试学科
create table KaoSub
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(32) not null,	--AccIDS + Value
	AccIDS	nvarchar(32) not null,
	KaoIDS	nvarchar(32) not null,
	SubIDS	nvarchar(20) not null,

	Value	nvarchar(20) not null,
	Fixed	bit not null,
)
go

insert KaoSub values (Lower(REPLACE(NEWID(), '-','')), '3212840201', '32128402', '语文', '语', '01', 0)
insert KaoSub values (Lower(REPLACE(NEWID(), '-','')), '3212840202', '32128402', '数学', '数', '02', 0)
insert KaoSub values (Lower(REPLACE(NEWID(), '-','')), '3212840203', '32128402', '英语', '英', '03', 0)
insert KaoSub values (Lower(REPLACE(NEWID(), '-','')), '3212840204', '32128402', '物理', '物', '04', 0)
insert KaoSub values (Lower(REPLACE(NEWID(), '-','')), '3212840205', '32128402', '化学', '化', '05', 0)
insert KaoSub values (Lower(REPLACE(NEWID(), '-','')), '3212840206', '32128402', '政治', '政', '06', 0)
insert KaoSub values (Lower(REPLACE(NEWID(), '-','')), '3212840207', '32128402', '历史', '历', '07', 0)
insert KaoSub values (Lower(REPLACE(NEWID(), '-','')), '3212840208', '32128402', '地理', '地', '08', 0)
insert KaoSub values (Lower(REPLACE(NEWID(), '-','')), '3212840209', '32128402', '生物', '生', '09', 0)
insert KaoSub values (Lower(REPLACE(NEWID(), '-','')), '3212840210', '32128402', '体育', '体', '10', 0)
insert KaoSub values (Lower(REPLACE(NEWID(), '-','')), '3212840211', '32128402', '音乐', '音', '11', 0)
insert KaoSub values (Lower(REPLACE(NEWID(), '-','')), '3212840212', '32128402', '美术', '美', '12', 0)
insert KaoSub values (Lower(REPLACE(NEWID(), '-','')), '3212840213', '32128402', '信息', '信', '13', 0)
insert KaoSub values (Lower(REPLACE(NEWID(), '-','')), '3212840214', '32128402', '听力', '听', '14', 0)
insert KaoSub values (Lower(REPLACE(NEWID(), '-','')), '3212840215', '32128402', '口语', '口', '15', 0)


--

----年级学科（这里的Value、Scoring是统一设置，默认值）
--create table KSubGrade
--(
--	ID	nvarchar(32) not null,
--	IDS	nvarchar(20) not null,
--	GradeIDS	nvarchar(20) not null,
--	SubIDS	nvarchar(20) not null,
--	DefaultValue	int not null,	--学科分值
--	DefaultScoring	bit not null,	--是否记分
--)
--go

--alter table KSubGrade add constraint PK_KSubGrade primary key clustered (ID)
--alter table KSubGrade add constraint FK_KSubGrade_SubIDS foreign key (SubIDS) references TSub (IDS)
--create unique nonclustered index UN_KSubGrade_IDS on KSubGrade (IDS)

--insert KSubGrade values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010701', '321284020120160107', '3212840201', 150, 1)
--insert KSubGrade values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010702', '321284020120160107', '3212840202', 150, 1)
--insert KSubGrade values (Lower(REPLACE(NEWID(), '-','')), '32128402012016010703', '321284020120160107', '3212840203', 150, 1)




----班级课任教师
--create table KSubBan
--(
--	ID	nvarchar(32) not null,
--	IDS	nvarchar(32) not null,
--	BanIDS	nvarchar(20) not null,
--	SubGradeIDS	nvarchar(20) not null,
--	AccIDS	nvarchar(32) not null,
--	IsMaster	bit not null,	--是否班主任
--	--是否要增加时间？
--)
--go

--alter table KSubBan add constraint PK_KSubBan primary key clustered (ID)
--alter table KSubBan add constraint FK_KSubBan_BanIDS foreign key (BanIDS) references TBan (IDS)
--alter table KSubBan add constraint FK_KSubBan_SubGradeIDS foreign key (SubGradeIDS) references KSubGrade (IDS)
--alter table KSubBan add constraint FK_KSubBan_AccIDS foreign key (AccIDS) references TAcc (IDS)
--create unique nonclustered index UN_KSubBan_IDS on KSubBan (IDS)



----参加考试学科（这里的Value、Scoring是实际值，Value过滤非法输入）
--create table KSubTest
--(
--	ID	nvarchar(32) not null,
--	IDS	nvarchar(20) not null,
--	KaoIDS	nvarchar(20) not null,
--	SubIDS	nvarchar(20) not null,
--	Value	int not null,
--	Scoring	bit not null,
--)
--go
--alter table KSubTest add constraint PK_KSubTest primary key clustered (ID)
--alter table KSubTest add constraint FK_KSubTest_KaoIDS foreign key (KaoIDS) references Kao (IDS)
--alter table KSubTest add constraint FK_KSubTest_SubIDS foreign key (SubIDS) references TSub (IDS)
--create unique nonclustered index UN_KSubTest_IDS on KSubTest (IDS)

----考场设置分类
--create table KRoomType
--(
--	ID	nvarchar(32) not null,
--	IDS	nvarchar(20) not null,
--	Name	nvarchar(20) not null,	--考场分类名称（初一、初二、初三）
--	Fixed	bit not null,	--是否启用
--)
--go
--alter table KRoomType add constraint PK_KRoomType primary key clustered (ID)
--create unique nonclustered index UN_KRoomType_IDS on KRoomType (IDS)


----考场设置
--create table KRoom
--(
--	ID	nvarchar(32) not null,
--	IDS	nvarchar(20) not null,
--	Name	nvarchar(20) not null,	--考场名称
--	Value	nvarchar(5) not null,	--考场编号
--	Hold	int not null,	--考场容纳人数
--	BeginNum	int not null,	--起始号码（默认从1开始编号）
--	TypeIDS	nvarchar(20) not null,	--分类编号
--)
--go
--alter table KRoom add constraint PK_KRoom primary key clustered (ID)
--alter table KRoom add constraint FK_KRoom_TypeIDS foreign key (TypeIDS) references KRoomType (IDS)
--create unique nonclustered index UN_KRoom_IDS on KRoom (IDS)


----参加考试的人员设置
--create table KStud
--(
--	ID	nvarchar(32) not null,
--	IDS	nvarchar(20) not null,
--	KaoIDS	nvarchar(20) not null,
--	StudIDS	nvarchar(32) not null,
--	Room	nvarchar(10),
--	Seat	nvarchar(10),
--	Kao	nvarchar(20),
--)
--go
--alter table KStud add constraint PK_KStud primary key clustered (ID)
--alter table KStud add constraint FK_KStud_KaoIDS foreign key (KaoIDS) references Kao (IDS)
--alter table KStud add constraint FK_KStud_StudIDS foreign key (StudIDS) references Student (IDS)
--create unique nonclustered index UN_KStud_IDS on KStud (IDS)


----考试成绩
--create table KScore
--(
--	ID	nvarchar(32) not null,
--	IDS	nvarchar(20) not null,
--	KStudIDS	nvarchar(20) not null,
--	KaoIDS	nvarchar(20) not null,
--	SubIDS	nvarchar(20) not null,
--	Value	float,
--	BanIndex	int,
--	GradeIndex	int,
--	GroupIndex	int,
--	TotalIndex	int,
--)
--go
--alter table KScore add constraint PK_KScore primary key clustered (ID)
--alter table KScore add constraint FK_KScore_KStudIDS foreign key (KStudIDS) references KStud (IDS)
--alter table KScore add constraint FK_KScore_KaoIDS foreign key (KaoIDS) references Kao (IDS)
--alter table KScore add constraint FK_KScore_SubIDS foreign key (SubIDS) references TSub (IDS)
--create unique nonclustered index UN_KScore_IDS on KScore (IDS)

----考试成绩明细
--create table KScoreDetail
--(
--	ID	nvarchar(32) not null,
--	IDS	nvarchar(20) not null,
--	ScoreIDS	nvarchar(20) not null,	--成绩编号
--	Name	nvarchar(20) not null,
--	Value	nvarchar(20),
--)
--go
--alter table KScoreDetail add constraint PK_KScoreDetail primary key clustered (ID)
--alter table KScoreDetail add constraint FK_KScoreDetail_ScoreIDS foreign key (ScoreIDS) references KScore (IDS)
--create unique nonclustered index UN_KScoreDetail_IDS on KScoreDetail (IDS)



--数据访问
create table ADatum
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,	--名称，英文
	Name	nvarchar(20) not null,	--标题，中文
	Command	nvarchar(max) not null,
)
go
alter table ADatum add constraint PK_ADatum primary key clustered (ID)
create unique nonclustered index UN_ADatum_IDS on ADatum (IDS)
create unique nonclustered index UN_ADatum_Name on ADatum (Name)

--select * from dbo.XXX where ID = '{0}'
--select * from dbo.XXX where ID = '{0}' and IDS = '{1}'
--select * from dbo.XXX where AccID = '{2}'
--select * from dbo.XXX where IDS like '%{3}%' or ID like '%{3}%'
--参数怎么传递
--string.Format(S, AccID, ID, IDS, Query)
--AccID参数自动获得

--样式列表
create table ATheme
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,
	Name	nvarchar(20) not null,
	IsCurrent	bit not null,
)
go
alter table ATheme add constraint PK_ATheme primary key clustered (ID)
create unique nonclustered index UN_ATheme_IDS on ATheme (IDS)
create unique nonclustered index UN_ATheme_Name on ATheme (Name)



--页面表
create table APage
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,
	Name	nvarchar(20) not null,
	Bootup	bit not null,
	Html	nvarchar(max) not null,
	Script	nvarchar(max),
 	Fixed	bit not null,
	ParentID	nvarchar(32) not null,
)
go
alter table APage add constraint PK_APage primary key clustered (ID)
create unique nonclustered index UN_APage_IDS on APage (IDS)




--WX
create table WxUploadFile
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(20) not null,
	FileType	nvarchar(10) not null,
	UploadType	nvarchar(10) not null,
	CreateTime	datetime not null,
)
alter table WxUploadFile add constraint PK_WxUploadFile primary key clustered (ID)
create index IN_WxUploadFile_IDS on WxUploadFile (IDS)

go



--视图
create view QrAccRoleGroup
as
SELECT     dbo.TAcc.ID, dbo.TAcc.IDS, dbo.TAcc.Name, dbo.TAcc.RoleGroupIDS, dbo.TAcc.Passed, dbo.TAcc.Fixed, dbo.ARoleGroup.Name AS RoleGroupName, 
                      dbo.TAcc.ParentID
FROM         dbo.TAcc LEFT OUTER JOIN
                      dbo.ARoleGroup ON dbo.TAcc.RoleGroupIDS = dbo.ARoleGroup.IDS
go



--过滤表的作用
--拦截未登记的用户
--实际上就是教师列表
--以“姓名”的方式，查找到相应的教师
--添加到用户表中
--用户可以“修改”过滤表中复制过来的数据，不反馈给过滤表

--上墙 - 用户过滤表
create table WxAccFilter
(
	ID	nvarchar(32) not null,	--编号
	IDS	nvarchar(32) not null,	--手机号码
	Name	nvarchar(10) not null,	--姓名
	Mobils	nvarchar(10),
)
go
alter table WxAccFilter add constraint PK_WxAccFilter primary key clustered (ID)
create unique nonclustered index UN_WxAccFilter_IDS on WxAccFilter (IDS)
create unique nonclustered index UN_WxAccFilter_Name on WxAccFilter (Name)


--上墙 - 用户表
create table WxAcc
(
	ID	nvarchar(32) not null,	--编号
	IDS	nvarchar(32) not null,	--unionid
	Name	nvarchar(10) not null,	--姓名
	Mobil	nvarchar(32) not null,	--手机号
	Mobils	nvarchar(10),	--短号
	openid	nvarchar(32) not null,	--
	nickname	nvarchar(32) not null,	--昵称
	headimgurl	nvarchar(200) not null,	--
)
go
alter table WxAcc add constraint PK_WxAcc primary key clustered (ID)
create unique nonclustered index UN_WxAcc_IDS on WxAcc (IDS)
create unique nonclustered index UN_WxAcc_Name on WxAcc (Name)

--上墙 - 活动列表
create table WxAction
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(32) not null,	--活动编号 - 20170001
	Name	nvarchar(20) not null,	--活动名称
	IsCurrent	bit not null,	--当前活动
	NeedCheck	bit not null,	--需要检测是否当前活动
)
go
alter table WxAction add constraint PK_WxAction primary key clustered (ID)
create unique nonclustered index UN_WxAction_IDS on WxAction (IDS)
create unique nonclustered index UN_WxAction_Name on WxAction (Name)
insert WxAction values (Lower(REPLACE(NEWID(), '-','')), '20170001', '庆祝2017年教师节活动', 1,1)

--上墙 - 奖项
create table WxPrize
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(32) not null,	--奖别：01、02
	Name	nvarchar(20) not null,
	Num	int not null,
)
go
alter table WxPrize add constraint PK_WxPrize primary key clustered (ID)
create unique nonclustered index UN_WxPrize_IDS on WxPrize (IDS)
create unique nonclustered index UN_WxPrize_Name on WxPrize (Name)
insert WxPrize values (Lower(REPLACE(NEWID(), '-','')), '01', '特等奖', 1)
insert WxPrize values (Lower(REPLACE(NEWID(), '-','')), '02', '一等奖', 1)
insert WxPrize values (Lower(REPLACE(NEWID(), '-','')), '03', '二等奖', 2)
insert WxPrize values (Lower(REPLACE(NEWID(), '-','')), '04', '三等奖', 3)
insert WxPrize values (Lower(REPLACE(NEWID(), '-','')), '05', '鼓励奖', 6)

--上墙 - 用户留言
create table WxAccSend
(
	ID	nvarchar(32) not null,
	IDS	nvarchar(32) not null,	--guid
	WxAccIDS	nvarchar(32) not null,	--用户帐号
	WxActionIDS	nvarchar(32) not null,	--活动编号
	CreateTime	datetime not null,	--创建时间
	SendMsg	nvarchar(max) not null,	--发送的信息
	MsgType	nvarchar(20) not null,	--消息类型
	Showed	bit not null,	--是否已显示
)
go
alter table WxAccSend add constraint PK_WxAccSend primary key clustered (ID)
create unique nonclustered index UN_WxAccSend_IDS on WxAccSend (IDS)
alter table WxAccSend add constraint FK_WxAccSend_WxAccIDS foreign key (WxAccIDS) references WxAcc (IDS)
alter table WxAccSend add constraint FK_WxAccSend_WxActionIDS foreign key (WxActionIDS) references WxAction (IDS)

go
create view QrWxAccSend
as
select 
  a.*
  ,AccName = b.Name
  ,AccImage = b.headimgurl
  ,ActionName = c.Name
from WxAccSend a left join WxAcc b
on a.WxAccIDS = b.IDS
left join WxAction c
on a.WxActionIDS = c.IDS
where c.IsCurrent = 1
go

--上墙 - 中奖用户表
create table WxAccPrize
(
	ID	nvarchar(32) not null,	--GUID
	IDS	nvarchar(32) not null,	--用户帐号 - 活动编号 => MD5
	WxAccIDS	nvarchar(32) not null,	--用户帐号
	WxActionIDS	nvarchar(32) not null,	--活动编号
	WxPrizeIDS	nvarchar(32) not null,	--几等奖
)
go
alter table WxAccPrize add constraint PK_WxAccPrize primary key clustered (ID)
create unique nonclustered index UN_WxAccPrize on WxAccPrize (IDS)
alter table WxAccPrize add constraint FK_WxAccPrize_WxAccIDS foreign key (WxAccIDS) references WxAcc (IDS)
alter table WxAccPrize add constraint FK_WxAccPrize_WxActionIDS foreign key (WxActionIDS) references WxAction (IDS)
alter table WxAccPrize add constraint FK_WxAccPrize_WxPrizeIDS foreign key (WxPrizeIDS) references WxPrize (IDS)


go
--
create view QrWxAccPrize
as
select
  a.*
  ,AccName = b.Name
  ,AccImage = b.headimgurl
  ,ActionName = c.Name
  ,PrizeName = d.Name
from WxAccPrize a left join WxAcc b
on a.WxAccIDS = b.IDS
left join WxAction c
on a.WxActionIDS = c.IDS
left join WxPrize d
on a.WxPrizeIDS = d.IDS
where c.IsCurrent = 1
go


--学生上传图片查询
--excel
--substring(start = 1 开始计数)
--C#是从0开始计数
create view QrWxStudentUpload
as 
SELECT     dbo.WxUploadFile.ID, dbo.WxUploadFile.IDS, dbo.WxUploadFile.FileType, dbo.WxUploadFile.UploadType, dbo.WxUploadFile.CreateTime, dbo.Student.IDC, 
                      dbo.Student.Name, CASE substring(dbo.student.idc, 17, 1) % 2 WHEN 0 THEN '女' WHEN 1 THEN '男' END AS StudSex, dbo.StudGrade.GradeIDS, 
                      dbo.StudGrade.BanIDS, dbo.TBan.Num, dbo.StudGrade.InSch, dbo.StudGrade.Score, dbo.StudGrade.ID AS StudGradeID
FROM         dbo.WxUploadFile INNER JOIN
                      dbo.Student ON dbo.WxUploadFile.IDS = dbo.Student.IDS INNER JOIN
                      dbo.StudGrade ON dbo.Student.IDS = dbo.StudGrade.StudIDS INNER JOIN
                      dbo.TBan ON dbo.StudGrade.BanIDS = dbo.TBan.IDS

