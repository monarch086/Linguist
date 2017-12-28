CREATE TABLE [Words] (
	WordId int NOT NULL,
	CategoryId int NOT NULL,
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
	UserId int NOT NULL,
	Login nvarchar(255) NOT NULL UNIQUE,
	Password nvarchar(512) NOT NULL,
	Name nvarchar(512) NOT NULL,
	DateAdded datetime,
  CONSTRAINT [PK_USERS] PRIMARY KEY CLUSTERED
  (
  [UserId] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Categories] (
	CategoryId int NOT NULL,
	CategoryName nvarchar(1023) NOT NULL,
	DateAdded datetime,
  CONSTRAINT [PK_CATEGORIES] PRIMARY KEY CLUSTERED
  (
  [CategoryId] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
ALTER TABLE [Words] WITH CHECK ADD CONSTRAINT [Words_fk0] FOREIGN KEY ([CategoryId]) REFERENCES [Categories]([CategoryId])
ON UPDATE CASCADE
GO
ALTER TABLE [Words] CHECK CONSTRAINT [Words_fk0]
GO
ALTER TABLE [Words] WITH CHECK ADD CONSTRAINT [Words_fk1] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
ON UPDATE CASCADE
GO
ALTER TABLE [Words] CHECK CONSTRAINT [Words_fk1]
GO



