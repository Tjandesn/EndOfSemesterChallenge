CREATE TABLE [dbo].[Owner]
(
	[OwnerId] int not null,
	[Surname] nvarchar (50) not null,
	[GivenName] nvarchar (50) not null,
	[Phone] nvarchar (10) not null,
	Constraint PK_Owner primary key (OwnerID)
)
