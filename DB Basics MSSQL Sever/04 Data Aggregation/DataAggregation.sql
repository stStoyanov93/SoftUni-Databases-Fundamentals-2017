USE Gringotts

--1
SELECT COUNT(*) AS [Count]
 FROM WizzardDeposits

--2
SELECT MAX(MagicWandSize) AS LongestMagicWand
FROM WizzardDeposits

--3
SELECT DepositGroup, MAX(MagicWandSize) AS LongestMagicWand
FROM WizzardDeposits
GROUP BY DepositGroup

--4
SELECT DepositGroup
FROM WizzardDeposits
GROUP BY DepositGroup
HAVING AVG(MagicWandSize) = (
							SELECT MIN(tempAVGSizes.avgMagicWandsSize) FROM
							(
								SELECT DepositGroup, 
								AVG(MagicWandSize) AS avgMagicWandsSize
								FROM WizzardDeposits
								GROUP BY DepositGroup
							) AS tempAVGSizes)

--4 - Second way to solve it
SELECT TOP 1 WITH TIES DepositGroup
FROM WizzardDeposits
GROUP BY DepositGroup
ORDER BY AVG(MagicWandSize)

--5
SELECT DepositGroup,
SUM(DepositAmount)
FROM WizzardDeposits
GROUP BY DepositGroup

 --6
SELECT DepositGroup,
SUM(DepositAmount)
FROM WizzardDeposits
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY DepositGroup

 --7
SELECT DepositGroup,
SUM(DepositAmount) AS TotalSum
FROM WizzardDeposits
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY DepositGroup
HAVING SUM(DepositAmount) < 150000
ORDER BY SUM(DepositAmount) DESC

 --8
SELECT DepositGroup,
MagicWandCreator,
MIN(DepositCharge) AS MinDepositCharge
FROM WizzardDeposits
GROUP BY DepositGroup, MagicWandCreator
ORDER BY MagicWandCreator, DepositGroup

--9
SELECT Ages.AgeGroup, COUNT(*) AS WizzardCount FROM 
(
SELECT 
CASE
	WHEN Age BETWEEN 0 AND 10 THEN '[0-10]'
	WHEN Age BETWEEN 11 AND 20 THEN '[11-20]'
	WHEN Age BETWEEN 21 AND 30 THEN '[21-30]'
	WHEN Age BETWEEN 31 AND 40 THEN '[31-40]'
	WHEN Age BETWEEN 41 AND 50 THEN '[41-50]'
	WHEN Age BETWEEN 51 AND 60 THEN '[51-60]'
	WHEN Age > 60 THEN '[61+]'
END AS AgeGroup
FROM WizzardDeposits ) AS Ages
GROUP BY Ages.AgeGroup

--10
SELECT DISTINCT LEFT(FirstName, 1) AS FirstLetter
FROM WizzardDeposits
WHERE DepositGroup = 'Troll Chest'
ORDER BY FirstLetter

--11
SELECT DepositGroup, IsDepositExpired, AVG(DepositInterest)
FROM WizzardDeposits
WHERE DepositStartDate > CAST('01/01/1985' AS datetime)
GROUP BY DepositGroup, IsDepositExpired
ORDER BY DepositGroup DESC, IsDepositExpired

--12
SELECT SUM(tempData.Difference) AS TotalSum FROM
	(
	SELECT FirstName AS [Host Wizzard], DepositAmount AS [Host Wizard Deposit],
	LEAD (FirstName) OVER (ORDER BY Id) AS [Guest Wizard],
	LEAD (DepositAmount) OVER (ORDER BY Id) AS [Guest Wizard Deposit],
	DepositAmount - LEAD (DepositAmount) OVER (ORDER BY Id) AS Difference
	FROM WizzardDeposits
	) AS tempData 

USE SoftUni
GO

--13
SELECT DepartmentID, SUM(Salary) 
FROM Employees
GROUP BY DepartmentID
ORDER BY DepartmentID

--14
SELECT DepartmentID, MIN(Salary) AS MinSalary 
FROM Employees
WHERE DepartmentID IN (2, 5, 7) AND HireDate > '2000/01/01'
GROUP BY DepartmentID

--15
SELECT * INTO EmployeesNewTable FROM Employees
WHERE Salary > 30000

DELETE FROM EmployeesNewTable
WHERE ManagerID = 42

UPDATE EmployeesNewTable
SET Salary += 5000
WHERE DepartmentID = 1

SELECT DepartmentID, AVG(Salary) 
FROM EmployeesNewTable
GROUP BY DepartmentID

--16
SELECT DepartmentID, MAX(Salary) AS MaxSalary 
FROM Employees
GROUP BY DepartmentID
HAVING MAX(Salary) NOT BETWEEN 30000 AND 70000

--17
SELECT COUNT(EmployeeID) AS Count
FROM Employees
GROUP BY ManagerID
HAVING ManagerID IS NULL

--18
SELECT RankedSalaries.DepartmentID,
RankedSalaries.ThirdHighestSalary FROM
	(
	SELECT DepartmentID,
	MAX(Salary) AS [ThirdHighestSalary],
	DENSE_RANK() OVER (PARTITION BY DepartmentID ORDER BY SALARY DESC) AS Rank
	FROM Employees
	GROUP BY DepartmentID, Salary
	) AS RankedSalaries
WHERE RankedSalaries.Rank = 3

--19
SELECT TOP 10 FirstName, LastName, DepartmentID FROM Employees
WHERE Salary > (
				SELECT AVG(Salary) FROM Employees AS avgSalary
				WHERE Employees.DepartmentID = avgSalary.DepartmentID
				GROUP BY DepartmentID
				)
ORDER BY DepartmentID