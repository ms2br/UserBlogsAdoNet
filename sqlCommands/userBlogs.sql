--CREATE DATABASE UsersBlogs
--use UsersBlogs

--CREATE TABLE Users
--(
--	ID int primary key identity,
--	FirstName nvarchar(25) not null,
--	LastName nvarchar(25) not null default('XXX'),
--	UserName nvarchar(30) not null unique,
--	UserEmailAddress nvarchar(260) not null unique,
--	Password nvarchar(max) not null,
--)

--alter TABLE Users
--add IsDeleted bit not null default(0)

--CREATE TABLE Blogs
--(
--	ID int primary key identity,
--	Title nvarchar(25) not null,
--	Description nvarchar(max) not null,
--	UserId int references Users(ID) 
--)

--alter TABLE Blogs
--add IsDeleted bit not null default(0)

--insert into Users(FirstName,LastName,UserName,UserEmailAddress,Password)
--values('asas','asas','aasas','aasasasa','aaasasasa')

--insert into Blogs(Title,Description,UserId)  
--values('asas','asas',2)

--select * from Users
--select * from Blogs