CREATE VIEW TotalWords AS

SELECT Count, u.UserId, Login FROM 
(SELECT COUNT(WordId) AS Count, w.UserId
  FROM dbo.Words w 
 GROUP BY w.UserId) wo

 FULL JOIN dbo.Users u
	ON u.UserId = wo.UserId