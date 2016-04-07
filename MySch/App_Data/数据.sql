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
create index IN_TKey_Name on TKey (Name)
create index IN_TKey_Parent on TKey (Parent)
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
create unique nonclustered index IN_TAcc_Name on TAcc (Name)
--插入管理员
insert TAcc values ('admin','083c1a7e3e8f6adcb09566055de33853', '系统管理员','c0e5fa241935e64e19c39dc38109c13a','2015-09-10 12:00:00',  0, null)
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
create unique nonclustered index IN_TPage_Name on TPage (Name)

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


