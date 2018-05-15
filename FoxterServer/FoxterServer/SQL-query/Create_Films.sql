use Films

create table Films
(
	Id int primary key identity,
	[Name] nvarchar(max) not null,
	Image_Main nvarchar(max) not null,
	Age nvarchar(max) null,
	Genre nvarchar(max) null,
	[Year] nvarchar(max) null,
	Country nvarchar(max) null,
	Duration nvarchar(max) null,
	Rating nvarchar(max) null,
	Info nvarchar(max) null,
	Video nvarchar(max) null,
	Images nvarchar(max) null
)

create table Cinemas
(
	Id int primary key identity,
	[Name] nvarchar(max) not null,
	[Address] nvarchar(max) null,
	Info_Little nvarchar(max) null,
	Schedule nvarchar (max) null,
	Image_Main nvarchar(max) null,
	Info nvarchar(max) null,
	Telephone nvarchar(max) null,
	Images nvarchar(max) null
)
create table Users
(
	Id int primary key identity,
	[Name] nvarchar(max) not null,
	UserName nvarchar(max) not null,
	[Password] nvarchar(max) not null
)

select * from Users
select * from Cinemas
select * from Films
drop table Films
drop table Cinemas
drop table Users