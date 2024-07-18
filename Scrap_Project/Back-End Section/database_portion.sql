use db1;
go

create table person_data(
	id int primary key identity(1,1) not null,
	u_name varchar(50) not null,
	email_add varchar(1000) not null,
	b_date date not null,
	contact int not null,
	img_path varchar(max) not null
);