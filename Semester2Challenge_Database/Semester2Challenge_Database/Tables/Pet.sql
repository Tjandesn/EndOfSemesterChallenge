CREATE TABLE [dbo].[Pet]
(
	[PetName] nvarchar (20) not null,
	[Type] nvarchar (20) not null,
	[OwnerID] int not null,
	Constraint PK_Pet primary key (PetName, OwnerID),
	Constraint FK_OwnerID foreign key (OwnerID) references [Owner] (OwnerID)
)
