--USE SoftUni
--GO
--USE Bank
--GO
--USE Diablo
--GO

--1
CREATE PROC usp_GetEmployeesSalaryAbove35000
AS
BEGIN
	SELECT FirstName, LastName FROM Employees
	WHERE Salary > 35000
END

--2
CREATE PROC usp_GetEmployeesSalaryAboveNumber(@number DECIMAL(18,4))
AS
BEGIN
	SELECT FirstName, LastName FROM Employees
	WHERE Salary >= @number
END

--3
CREATE PROC usp_GetTownsStartingWith(@name VARCHAR(MAX))
AS
BEGIN
	SELECT Name FROM Towns
	WHERE Name LIKE @name + '%'
END

--4
CREATE PROC usp_GetEmployeesFromTown (@townName VARCHAR(MAX))
AS
BEGIN
	SELECT e.FirstName, e.LastName FROM Employees AS e
	JOIN Addresses AS a ON a.AddressID = e.AddressID
	JOIN Towns AS t ON t.TownID = a.TownID
	WHERE t.Name = @townName
END

--5
CREATE FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18,4))
RETURNS VARCHAR(7)
AS
BEGIN
	DECLARE @level VARCHAR(7);

	IF(@salary < 30000)
		BEGIN
		SET @level = 'Low'
		END
	ELSE IF(@salary <= 50000)
		BEGIN
		SET @level = 'Average'
		END
	ELSE
		BEGIN
		SET @level = 'High'
		END

	RETURN @level;
END

--6
CREATE PROC usp_EmployeesBySalaryLevel(@SalaryLevel VARCHAR(10))
AS
SELECT FirstName, LastName FROM Employees
WHERE dbo.ufn_GetSalaryLevel(Salary) = @SalaryLevel

--7
CREATE FUNCTION ufn_IsWordComprised(@setOfLetters VARCHAR(max), @word VARCHAR(max))
RETURNS BIT
AS
BEGIN
  DECLARE @isComprised BIT = 0;
  DECLARE @currentIndex INT = 1;
  DECLARE @currentChar CHAR;

  WHILE(@currentIndex <= LEN(@word))
  BEGIN

    SET @currentChar = SUBSTRING(@word, @currentIndex, 1);
    IF(CHARINDEX(@currentChar, @setOfLetters) = 0)
      RETURN @isComprised;
    SET @currentIndex += 1;

  END

  RETURN @isComprised + 1;

END

--8
CREATE PROC usp_DeleteEmployeesFromDepartment (@departmentId INT)
AS

ALTER TABLE Departments
ALTER COLUMN ManagerID INT NULL

DELETE FROM EmployeesProjects
WHERE EmployeeID IN (
	SELECT EmployeeID FROM Employees
	WHERE DepartmentID = @departmentId)

UPDATE Employees
SET ManagerID = NULL
WHERE ManagerID IN (
	SELECT EmployeeID FROM Employees
	WHERE DepartmentID = @departmentId)


UPDATE Departments
SET ManagerID = NULL
WHERE ManagerID IN (
	SELECT EmployeeID FROM Employees
	WHERE DepartmentID = @departmentId)

DELETE FROM Employees
WHERE EmployeeID IN (
	SELECT EmployeeID FROM Employees
	WHERE DepartmentID = @departmentId)

DELETE FROM Departments
WHERE DepartmentID = @departmentId
SELECT COUNT(*) AS [Employees Count] FROM Employees AS e
JOIN Departments AS d
ON d.DepartmentID = e.DepartmentID
WHERE e.DepartmentID = @departmentId

-9
CREATE PROC usp_GetHoldersFullName
AS
BEGIN
	SELECT CONCAT(FirstName, ' ', LastName) AS FullName
	FROM AccountHolders
END

--10
CREATE PROC usp_GetHoldersWithBalanceHigherThan (@minBalance money)
AS
BEGIN
	
	SELECT ah.FirstName AS [First Name], ah.LastName AS [Last Name]
	FROM (SELECT AccountHolderId FROM Accounts
    GROUP BY AccountHolderId
	HAVING SUM(Balance) > @minBalance) AS sq
	JOIN AccountHolders AS ah ON ah.Id = sq.AccountHolderId
	ORDER BY ah.LastName, ah.FirstName 
	
END

--11
CREATE FUNCTION ufn_CalculateFutureValue (@sum MONEY, @annualInterestRate FLOAT, @years INT)
RETURNS money
AS
BEGIN
	RETURN @sum * POWER(@annualInterestRate + 1, @years);
END

--12
CREATE PROC usp_CalculateFutureValueForAccount (@accountId int, @interestRate float)
AS
BEGIN
  DECLARE @years int = 5;

  SELECT
    a.Id AS [Account Id],
    ah.FirstName AS [First Name],
    ah.LastName AS [Last Name], 
    a.Balance AS [Current Balance],
    dbo.ufn_CalculateFutureValue(a.Balance, @interestRate, @years) AS [Balance in 5 years]
  FROM AccountHolders AS ah
  JOIN Accounts AS a ON ah.Id = a.AccountHolderId
  WHERE a.Id = @accountId

END

--13
CREATE FUNCTION ufn_CashInUsersGames(@gameName NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN

	SELECT SUM(Cash) AS SumCash FROM 
		(
		       SELECT ug.Cash, 
			          ROW_NUMBER() OVER(ORDER BY Cash DESC) AS RowNum 
		         FROM UsersGames AS ug
		   INNER JOIN Games AS g
		           ON g.Id = ug.GameId
		        WHERE g.Name = @gameName
		) AS CashList
WHERE RowNum % 2 = 1