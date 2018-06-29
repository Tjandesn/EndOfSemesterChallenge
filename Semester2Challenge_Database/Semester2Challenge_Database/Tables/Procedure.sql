CREATE TABLE [dbo].[Procedure]
(
	[ProcedureID] int not null,
	[Description] nvarchar (256) not null,
	[Price] money not null,
	Constraint PK_Procedure primary key (ProcedureID)
)
