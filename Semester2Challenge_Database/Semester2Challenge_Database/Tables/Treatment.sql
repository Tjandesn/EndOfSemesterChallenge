CREATE TABLE [dbo].[Treatment]
(
	[PetName] nvarchar (20) not null,
	[OwnerID] int not null,
	[ProcedureID] int not null,
	[Date] date not null,
	[Notes] nvarchar (256) not null,
	[Price] money not null,
	Constraint PK_Treatment primary key (PetName, OwnerID, ProcedureID, Date),
	Constraint FK_Pet foreign key (PetName, OwnerID) references Pet (PetName, OwnerID),
	Constraint FK_Procedure foreign key (ProcedureID) references [Procedure] (ProcedureID)
)
