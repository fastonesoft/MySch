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
	ID	nvarchar(32) not null,	--帐号代码x321284xxx
	GD	nvarchar(32) not null,
	Name	nvarchar(32) not null,	--帐号全称、姓名
	Pwd	nvarchar(32) not null,
	RegTime	datetime not null default getdate(),
	Fixed	bit not null,
	Parent	nvarchar(32),
)
go
alter table TAcc add constraint PK_TAcc primary key clustered (ID)
create unique nonclustered index UN_TAcc_GD on TAcc (GD)
create unique nonclustered index UN_TAcc_Name on TAcc (Name)
--插入管理员
insert TAcc values ('admin','51e66f66919ee73bc252590bdf3b339c', '系统管理员','538e1387be95027c7c4edf399c4e0149','2015-09-10 12:00:00',  0, null)
go

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


--新生报名
create table TStudReg
(
	ID	nvarchar(20) not null,	--身份证号
	GD	nvarchar(32) not null,	--编号
	Name	nvarchar(20) not null,	--姓名
	fromSch	nvarchar(20),	--学校
	fromGrade	nvarchar(10),	--年级
	nationID	nvarchar(20),	--全国学籍号
	readState	nvarchar(20),	--就读状态
	isProblem	bit not null,	--是否问题学籍
	--以上：自动注册时填充
	schChoose	bit not null,	--是否择校
	studNo	nvarchar(20),	--报名编号 - 学籍号
	Memo	nvarchar(50),	--备注
	--以上：归档时填充
	Mobil1	nvarchar(20),	--联系电话一
	Mobil2	nvarchar(20),	--联系电话二
	Name1	nvarchar(20),	--第一监护人
	Name2	nvarchar(20),	--第二监护人
	Home	nvarchar(50),	--家庭地址
	Permanent	nvarchar(50),	--户籍地址
	Reged	bit not null,	--是否注册
)
alter table TStudReg add constraint PK_TStudReg primary key clustered (ID)
create unique nonclustered index UN_TStudReg_GD on TStudReg (GD)
create index IN_TStudReg_Name on TStudReg (Name)
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
	ID	bigint identity(1,1) not null,
	Brower	nvarchar(36) not null,
	IP	nvarchar(36) not null,
	loginTime	datetime not null,	--登录时间
	Name	nvarchar(20) not null,
	Pwd	nvarchar(36) not null,
	loginMsg	nvarchar(36) not null,
)
go
alter table TLogin add constraint PK_TLogin primary key clustered (ID)


create table TLog
(
	GD	nvarchar(32) not null,
	Value	nvarchar(2000) not null,
	CreateTime	datetime not null default getdate(), 
)
go
alter table TLog add constraint PK_TLog primary key clustered (GD)

