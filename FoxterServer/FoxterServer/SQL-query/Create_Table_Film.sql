use Films

create table Films
(
	Id int null,
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
insert into Films values
(1,'','','','','','','','','','','')
select * from Films
drop table Films