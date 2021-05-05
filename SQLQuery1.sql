create database ElectronicsShopDb
go

use ElectronicsShopDb
go

create table Members
(
	IdMember uniqueidentifier not null primary key,
	FirstName varchar(250) not null,
	LastName varchar(250) not null,
	Address varchar(500) not null,
	City varchar(100) not null,
	Country varchar(100) not null,
	PostalCode varchar(50) null,
	Phone varchar(20) null,
	Email varchar(100) not null,
)

create table Categories
(
	IdCategory uniqueidentifier not null primary key,
	CategoryName varchar(150) not null,
)

create table Products
(
	IdProduct uniqueidentifier not null primary key,
	IdCategory uniqueidentifier not null,
	ProductName varchar(150) not null,
	Manufacturer varchar(150) not null,
	Description nvarchar(max) null,
	Price money not null,
	Warranty int not null,
	Rating int null default 0,

	constraint [FK_Products_Categories] foreign key (IdCategory) references Categories(IdCategory),
)

create table Reviews
(
	IdReview uniqueidentifier not null primary key,
	IdMember uniqueidentifier not null,
	IdProduct uniqueidentifier not null,
	Review nvarchar(max) not null,
	ReviewDate datetime not null,

	constraint [FK_Members_Reviews] foreign key (IdMember) references Members(IdMember),
	constraint [FK_Products_Reviews] foreign key (IdProduct) references Products(IdProduct),
)

create table Orders
(
	IdOrder uniqueidentifier not null primary key,
	IdProduct uniqueidentifier not null,
	DateOrder datetime not null,
	Quantity int not null,
	Price money not null,

	constraint [FK_Products_Orders] foreign key (IdProduct) references Products(IdProduct),
)

create table ShoppingCarts
(
	IdShoppingCart uniqueidentifier not null primary key,
	IdProduct uniqueidentifier not null,
	Quantity int not null,
	Price money not null,

	constraint [FK_Products_ShoppingCarts] foreign key (IdProduct) references Products(IdProduct),
)

create table Cart
(
	IdCart uniqueidentifier not null primary key,
	IdShoppingCart uniqueidentifier not null,
	IdMember uniqueidentifier not null,
	Date datetime not null,

	constraint [FK_ShoppingCarts_Cart] foreign key (IdShoppingCart) references ShoppingCarts(IdShoppingCart),
	constraint [FK_Members_Cart] foreign key (IdMember) references Members(IdMember),
)