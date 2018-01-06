ALTER TABLE Linguist.dbo.Words
ADD RememberIndex INT

UPDATE Linguist.dbo.Words
SET RememberIndex = 0