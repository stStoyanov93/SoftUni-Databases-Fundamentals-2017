--USING SOFTUNI DATABASE
--1
SELECT FirstName, LastName FROM Employees
WHERE FirstName LIKE 'SA%'

--2
SELECT FirstName, LastName FROM Employees
WHERE LastName LIKE '%ei%'

--3
SELECT FirstName FROM Employees
WHERE DepartmentID IN (3, 10) AND DATEPART(YEAR, HireDate) BETWEEN 1995 AND 2005

--4
SELECT FirstName, LastName FROM Employees
WHERE JobTitle NOT LIKE '%engineer%'

--5
SELECT [Name] FROM Towns
WHERE LEN([Name]) BETWEEN 5 AND 6
ORDER BY [Name]

--6
SELECT TownID,[Name] FROM Towns
WHERE [Name] LIKE '[MKBE]%'
ORDER BY [Name]

--7
SELECT TownID,[Name] FROM Towns
WHERE [Name] LIKE '[^RBD]%'
ORDER BY [Name]

--8
GO
CREATE VIEW V_EmployeesHiredAfter2000 AS
SELECT FirstName, LastName FROM Employees
WHERE DATEPART(YEAR, HireDate) > 2000
GO

--9
SELECT FirstName, LastName FROM Employees
WHERE LEN(LastName) = 5

--USING GEOGRAPHY DATABASE
--10
SELECT CountryName AS [Country Name], IsoCode AS [ISO Code] FROM Countries
WHERE CountryName LIKE '%A%A%A%'
ORDER BY [ISO Code]

--11
SELECT 
	PeakName,
	RiverName,
	LOWER(PeakName + RIGHT(RiverName, LEN(RiverName) - 1)) AS Mix
FROM Peaks, Rivers
WHERE RIGHT(PeakName, 1) = LEFT(RiverName, 1)
ORDER BY Mix

--USING DIABLO DATABASE
--12
SELECT TOP(50) [Name], FORMAT([Start], 'yyyy-MM-dd') AS [Start] FROM Games
WHERE DATEPART(YEAR, [START]) BETWEEN 2011 AND 2012
ORDER BY [Start], [Name]

--13
SELECT 
	Username, 
	RIGHT(Email, LEN(Email) - CHARINDEX('@', Email, 1)) AS [Email Provider]
FROM Users
ORDER BY [Email Provider], Username

--14
SELECT Username, IpAddress AS [IP Address] FROM Users
WHERE IpAddress LIKE '___.1%.%.___'
ORDER BY Username

--15
SELECT
[Name] AS Game,
--[Start] AS [Part of the Day]
CASE
	WHEN DATEPART(HOUR, GAMES.Start) BETWEEN 0 AND 11 THEN 'Morning'
	WHEN DATEPART(HOUR, GAMES.Start) BETWEEN 12 AND 17 THEN 'Afternoon'
	WHEN DATEPART(HOUR, GAMES.Start) BETWEEN 18 AND 23 THEN 'Evening'
END AS [Part of the Day],
CASE
	WHEN Duration BETWEEN 0 AND 3 THEN 'Extra Short'
	WHEN Duration BETWEEN 4 AND 6 THEN 'Short'
	WHEN Duration > 6 THEN 'Long'
	ELSE 'Extra Long'
END AS [Duration]
FROM GAMES
ORDER BY [Name], [Duration]

--CREATE ORDERS DATABASE
--USE ORDERS DATABASE

--16
SELECT ProductName,
	   OrderDate,
	   DATEADD(DAY, 3, OrderDate) AS [Pay Due],
	   DATEADD(MONTH, 1, OrderDate) AS [Deliver Due]
FROM Orders