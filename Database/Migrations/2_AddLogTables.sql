CREATE TABLE [TestResults] (
	TestResultId int IDENTITY(1,1) NOT NULL,
	UserId int NOT NULL,
	Date datetime NOT NULL,
	RightWords nvarchar(1023) NOT NULL,
	WrongWords nvarchar(1023) NOT NULL,
  CONSTRAINT [PK_TESTRESULTS] PRIMARY KEY CLUSTERED
  (
  [TestResultId] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [TrainingResults] (
	TrainingResultId int IDENTITY(1,1) NOT NULL,
	UserId int NOT NULL,
	Date datetime NOT NULL,
	Words nvarchar(1023) NOT NULL,
  CONSTRAINT [PK_TRAININGRESULTS] PRIMARY KEY CLUSTERED
  (
  [TrainingResultId] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Visitors] (
	VisitorId int IDENTITY(1,1) NOT NULL,
	Login nvarchar(255) NOT NULL,
	Ip nvarchar(64) NOT NULL,
	Url nvarchar(512) NOT NULL,
	Date datetime NOT NULL,
  CONSTRAINT [PK_VISITORS] PRIMARY KEY CLUSTERED
  (
  [VisitorId] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO