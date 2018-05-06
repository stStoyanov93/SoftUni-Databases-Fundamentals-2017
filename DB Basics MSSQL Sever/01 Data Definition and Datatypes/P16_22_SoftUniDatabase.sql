CREATE DATABASE SoftUni
Use SoftUni
GO

CREATE TABLE Towns (
	Id INT IDENTITY PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL
)


CREATE TABLE Addresses (
	Id INT IDENTITY PRIMARY KEY,
	AddressText VARCHAR(50) NOT NULL,
	TownId INT FOREIGN KEY REFERENCES Towns(Id) NOT NULL
)

CREATE TABLE Departments (
	Id INT IDENTITY PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL
)
Drop table Employees
CREATE TABLE Employees (
	Id INT IDENTITY PRIMARY KEY,
	FirstName VARCHAR(50) NOT NULL,
	MiddleName VARCHAR(50),
	LastName VARCHAR(50) NOT NULL,
	JobTitle VARCHAR(50) NOT NULL,
	DepartmentId INT FOREIGN KEY REFERENCES Departments(Id),
	HireDate VARCHAR(50),
	Salary NUMERIC(12,2),
	AddressId INT FOREIGN KEY REFERENCES Addresses(Id)
)

INSERT INTO Towns(Name) VALUES
	('Sofia'),
	('Plovdiv'),
	('Varna'),
	('Burgas')
	
INSERT INTO Departments(Name) VALUES
	('Engineering'),
	('Sales'),
	('Marketing'),
	('Software Development'),
	('Quality Assurance')

INSERT INTO Employees (FirstName, MiddleName, LastName, JobTitle, DepartmentId, HireDate, Salary, AddressId)VALUES
	('Ivan', 'Ivanov', 'Ivanov', '.NET Developer', 4, '01/02/2013', 3500.00, NULL),
	('Peter', 'Petrov', 'Petrov', 'Senior Engineer', 1, '02/03/2004', 4000.00, NULL),
	('Maria', 'Petrova', 'Ivanova', 'Intern', 5, '28/08/2016', 525.25, NULL),
	('Georgi', 'Teziev', 'Ivanov', 'CEO', 2, '09/12/2007', 3000.00, NULL),
	('Peter', 'Pan', 'Pan', 'Intern', 3, '28/08/2016', 599.88, NULL)

SELECT * FROM Towns
ORDER BY [Name]

SELECT * FROM Departments
ORDER BY [Name]

SELECT * FROM Employees
ORDER BY Salary DESC

SELECT [Name] FROM Towns
ORDER BY [Name]

SELECT [Name] FROM Departments
ORDER BY [Name]

SELECT FirstName, LastName, JobTitle, Salary FROM Employees
ORDER BY Salary DESC

UPDATE Employees
SET Salary *= 1.1

SELECT Salary FROM Employees