--USE SoftUni --P01_P11
--GO
--USE Geography --P12_P18
--GO

--1
SELECT TOP 5 e.EmployeeID, e.JobTitle, e.AddressID, a.AddressText FROM Employees AS e
JOIN Addresses AS a ON e.AddressID = a.AddressID
ORDER BY e.AddressID

--2
SELECT TOP 50 e.[FirstName], e.LAStName, t.Name, a.AddressText FROM Employees AS e
JOIN Addresses AS a ON e.AddressID = a.AddressID
JOIN Towns AS t ON t.TownID = a.TownID
ORDER BY e.FirstName, e.LAStName

--3
SELECT TOP 50 e.EmployeeID, e.[FirstName], e.LAStName, d.Name FROM Employees AS e
JOIN Departments AS d ON d.DepartmentID = e.DepartmentID
WHERE d.Name = 'Sales'
ORDER BY e.EmployeeID

--4
SELECT TOP 5 e.EmployeeID, e.[FirstName], e.Salary, d.Name FROM Employees AS e
JOIN Departments AS d ON d.DepartmentID = e.DepartmentID
WHERE e.Salary > 15000
ORDER BY d.DepartmentID

--5
SELECT TOP 3 EmployeeID, FirstName from Employees
WHERE EmployeeID NOT IN
	(SELECT e.EmployeeID FROM Employees AS e
JOIN EmployeesProjects AS ep ON ep.EmployeeID = e.EmployeeID)
Order by EmployeeID

--6
SELECT e.FirstName, e.LAStName, e.HireDate, d.Name FROM Employees AS e
JOIN Departments AS d ON d.DepartmentID = e.DepartmentID
WHERE e.HireDate > '19990101'
AND d.Name = 'Sales' OR d.Name = 'Finance'
ORDER BY e.HireDate

--7
SELECT TOP 5 e.EmployeeID, e.FirstName, p.Name FROM Employees AS e
JOIN EmployeesProjects AS ep ON e.EmployeeID = ep.EmployeeID
JOIN Projects AS p ON p.ProjectID = ep.ProjectID
WHERE p.StartDate > '20020813' AND p.EndDate IS NULL
ORDER BY e.EmployeeID

--8
SELECT top 5 e.EmployeeID, e.FirstName, 
CASE
	WHEN p.StartDate >= '20050101' THEN NULL
	ELSE p.Name
 END AS ProjectName
FROM Employees AS e
JOIN EmployeesProjects AS ep ON e.EmployeeID = ep.EmployeeID
JOIN Projects AS p ON p.ProjectID = ep.ProjectID
WHERE e.EmployeeID = 24

--9
SELECT e.EmployeeID, e.FirstName, m.EmployeeID, m.FirstName FROM Employees AS e
JOIN Employees AS m ON m.EmployeeID = e.ManagerID
WHERE m.EmployeeID IN (3, 7)
ORDER BY e.EmployeeID

--10
SELECT top 50 e.EmployeeID, CONCAT(e.FirstName, ' ', e.LAStName) AS EmployeeName, CONCAT(m.FirstName, ' ', m.LAStName) AS ManagerName, d.Name AS DepartmentName
FROM Employees AS e
JOIN Employees AS m ON m.EmployeeID = e.ManagerID
JOIN Departments AS d ON d.DepartmentID = e.DepartmentID
ORDER BY e.EmployeeID

--11
SELECT MIN(avgSalaries.avg) FROM 
	(SELECT AVG(Salary) AS avg FROM Employees
GROUP BY DepartmentID) AS avgSalaries

--12
SELECT mc.CountryCode, m.MountainRange, p.PeakName, p.ElevatiON FROM MountainsCountries AS mc
JOIN Mountains AS m ON m.Id = mc.MountainId
JOIN Peaks AS p ON p.MountainId = mc.MountainId
WHERE mc.CountryCode = 'BG' and p.ElevatiON > 2835
ORDER BY p.ElevatiON DESC

--13
SELECT CountryCode, COUNT(MountainId) AS MountainRanges FROM MountainsCountries
WHERE CountryCode IN ('US', 'RU', 'BG')
GROUP BY CountryCode

--14
SELECT TOP 5 c.CountryName, r.RiverName FROM Countries AS c
LEFT JOIN CountriesRivers AS cr ON cr.CountryCode = c.CountryCode
LEFT JOIN Rivers AS r ON r.Id = cr.RiverId
WHERE c.CONtinentCode = 'AF'
ORDER BY c.CountryName

--15
WITH CCYCONtUsage_CTE (CONtinentCode, CurrencyCode, CurrencyUsage) AS (
  SELECT CONtinentCode, CurrencyCode, COUNT(CountryCode) AS CurrencyUsage
  FROM Countries 
  GROUP BY CONtinentCode, CurrencyCode
  HAVING COUNT(CountryCode) > 1  
)
SELECT CONtMax.CONtinentCode, ccy.CurrencyCode, CONtMax.CurrencyUsage 
  FROM
  (SELECT CONtinentCode, MAX(CurrencyUsage) AS CurrencyUsage
   FROM CCYCONtUsage_CTE 
   GROUP BY CONtinentCode) AS CONtMax
JOIN CCYCONtUsage_CTE AS ccy 
ON (CONtMax.CONtinentCode = ccy.CONtinentCode AND CONtMax.CurrencyUsage = ccy.CurrencyUsage)
ORDER BY CONtMax.CONtinentCode

--16
SELECT COUNT(c.CountryCode) FROM Countries AS c
LEFT JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode
WHERE mc.MountainId IS NULL

--17
SELECT TOP 5 c.CountryName, MAX(p.ElevatiON) AS HighestPeakElevatiON, MAX(r.Length) AS LONgestRiverLength
FROM Countries AS c
LEFT JOIN MountainsCountries AS mc ON c.CountryCode = mc.CountryCode
LEFT JOIN Peaks AS p ON p.MountainId = mc.MountainId
LEFT JOIN CountriesRivers AS cr ON c.CountryCode = cr.CountryCode
LEFT JOIN Rivers AS r ON r.Id = cr.RiverId
GROUP BY c.CountryName
ORDER BY HighestPeakElevatiON DESC, LONgestRiverLength DESC, c.CountryName

--18
SELECT TOP 5 c.CountryName AS [Country],
CASE
	WHEN p.PeakName IS NULL THEN '(no highest peak)'
	ELSE p.PeakName
	END AS [HighestPeakName],
CASE
	WHEN p.ElevatiON IS NULL THEN 0
	ELSE MAX(p.ElevatiON)
END AS [HighestPeakElevatiON],
CASE
	WHEN m.MountainRange IS NULL THEN '(no mountain)'
	ELSE m.MountainRange
END AS [Mountain] 
FROM Countries AS c
LEFT JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode
LEFT JOIN Mountains AS m ON m.Id = mc.MountainId
LEFT JOIN Peaks AS p ON m.Id = p.MountainId
       GROUP BY c.CountryName, p.PeakName, p.ElevatiON, m.MountainRange
ORDER BY c.CountryName, p.PeakName