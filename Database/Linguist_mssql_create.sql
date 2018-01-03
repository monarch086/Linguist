CREATE TABLE [Words] (
	WordId int IDENTITY(1,1) NOT NULL,
	UserId int NOT NULL,
	OriginalWord nvarchar(512) NOT NULL,
	Translation nvarchar(512) NOT NULL,
	DateAdded datetime,
  CONSTRAINT [PK_WORDS] PRIMARY KEY CLUSTERED
  (
  [WordId] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Users] (
	UserId int IDENTITY(1,1) NOT NULL,
	Login nvarchar(255) NOT NULL UNIQUE,
	Password nvarchar(512) NOT NULL,
	Salt int NOT NULL,
	Name nvarchar(512) NOT NULL,
	DateAdded datetime,
	IsAdmin bit NOT NULL DEFAULT '0',
  CONSTRAINT [PK_USERS] PRIMARY KEY CLUSTERED
  (
  [UserId] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Categories] (
	CategoryId int IDENTITY(1,1) NOT NULL,
	ParentCategoryId int DEFAULT '0',
	UserId int NOT NULL,
	CategoryName nvarchar(1023) NOT NULL,
	DateAdded datetime,
  CONSTRAINT [PK_CATEGORIES] PRIMARY KEY CLUSTERED
  (
  [CategoryId] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [CatWordRelations] (
	CatWordRelationId int IDENTITY(1,1) NOT NULL,
	CategoryId int NOT NULL,
	WordId int NOT NULL,
  CONSTRAINT [PK_CATWORDRELATIONS] PRIMARY KEY CLUSTERED
  (
  [CatWordRelationId] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO

ALTER TABLE [Words] WITH CHECK ADD CONSTRAINT [Words_fk0] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
ON UPDATE CASCADE
GO
ALTER TABLE [Words] CHECK CONSTRAINT [Words_fk0]
GO






