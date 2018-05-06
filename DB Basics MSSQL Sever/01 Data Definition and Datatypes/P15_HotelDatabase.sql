CREATE DATABASE Hotel
USE Hotel
GO

CREATE TABLE Employees (
	Id INT IDENTITY,
	CONSTRAINT PK_Id_Employees PRIMARY KEY(Id),
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	Title NVARCHAR(50),
	Notes NVARCHAR(MAX)
)

CREATE TABLE Customers (
	AccountNumber INT IDENTITY,
	CONSTRAINT PK_Id_Customers PRIMARY KEY(AccountNumber),
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	PhoneNumber INT NOT NULL,
	EmergencyName NVARCHAR(50) NOT NULL,
	EmergencyNumber INT NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE RoomStatus (
	Available BIT DEFAULT 1 NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE RoomTypes (
	RoomType NVARCHAR(50) NOT NULL,
	CONSTRAINT CH_RoomType CHECK (RoomType = 'internal' OR 
	RoomType = 'external'),
	Notes NVARCHAR(MAX)
)

CREATE TABLE BedTypes (
	BedType NVARCHAR(50) NOT NULL,
	CONSTRAINT CH_BedTypes CHECK (BedType = 'single' OR 
	BedType = 'double'),
	Notes NVARCHAR(MAX)
)

CREATE TABLE Rooms (
	RoomNumber INT IDENTITY,
	CONSTRAINT PK_Id_Rooms PRIMARY KEY(RoomNumber),
	RoomType INT NOT NULL,
	BedType INT NOT NULL,
	Rate INT,
	RoomStatus BIT NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Payments
(
	Id INT PRIMARY KEY IDENTITY,
	EmployeeId INT UNIQUE NOT NULL,
	PaymentDate DATE,
	AccountNumber INT NOT NULL,
	FirstDateOccupied DATE,
	LastDateOccupied DATE,
	TotalDays INT NOT NULL,
	AmountCharged INT NOT NULL,
	TaxRate INT,
	TaxAmount INT,
	PaymentTotal INT NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Occupancies
(
	Id INT PRIMARY KEY IDENTITY,
	EmployeeId INT UNIQUE NOT NULL,
	DateOccupied DATE,
	AccountNumber INT NOT NULL,
	RoomNumber INT NOT NULL,
	RateApplied INT,
	PhoneCharge INT,
	Notes NVARCHAR(MAX)
)

INSERT INTO Employees (FirstName, LastName) VALUES
	('Gosho', 'Peshov'),
	('Pesho', 'Peshov'),
	('Meri', 'Peshova')

INSERT INTO Customers (FirstName, LastName, PhoneNumber, EmergencyName, EmergencyNumber) VALUES
	('Gosho', 'Peshov', 09992424, 'Pesho Peshov', 09992425),
	('Gosho', 'Peshov', 09992424, 'Pesho Peshov', 09992425),
	('Gosho', 'Peshov', 09992424, 'Pesho Peshov', 09992425)

INSERT INTO RoomStatus(Available) VALUES
	(1),
	(1),
	(0)

INSERT INTO RoomTypes(RoomType) VALUES
	('internal'),
	('internal'),
	('external')

INSERT INTO BedTypes(BedType) VALUES
	('single'),
	('double'),
	('double')

INSERT INTO Rooms(RoomType, BedType, RoomStatus) VALUES
	(1, 1, 1),
	(1, 2, 1),
	(2, 2, 0)

INSERT INTO Payments(EmployeeId,PaymentDate,AccountNumber,FirstDateOccupied,LastDateOccupied,TotalDays,AmountCharged,TaxRate,TaxAmount,PaymentTotal,Notes) VALUES
	(231, NULL, 2311, NULL,NULL, 7, 5000, 0,0,5000,NULL),
	(321, NULL, 3211, NULL,NULL, 7, 5000, 0,2000,7000,NULL),
	(999, NULL, 9989, NULL,NULL, 7, 5000, 0,50,5500,NULL)

INSERT INTO Occupancies(EmployeeId,DateOccupied,AccountNumber,RoomNumber,RateApplied,PhoneCharge,Notes) VALUES
	(991, NULL, 534, 8, NULL,NULL,NULL),
	(561, NULL, 75, 9, NULL,NULL,NULL),
	(135, NULL, 8, 10, NULL,NULL,NULL)

UPDATE Payments
SET TaxRate -= TaxRate * 0.03

SELECT TaxRate FROM Payments

DELETE Occupancies