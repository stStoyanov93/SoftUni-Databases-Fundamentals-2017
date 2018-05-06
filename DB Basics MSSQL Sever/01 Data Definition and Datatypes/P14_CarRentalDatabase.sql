CREATE DATABASE CarRental
USE CarRental
GO

CREATE TABLE Categories (
	Id INT IDENTITY,
	CONSTRAINT PK_Id_Cathegories PRIMARY KEY(Id),
	CategoryName NVARCHAR(50) NOT NULL,
	DailyRate INT,
	WeeklyRate INT,
	MonthlyRate INT,
	WeekendRate INT
)

CREATE TABLE Cars (
	Id INT IDENTITY,
	CONSTRAINT PK_Id_Cars PRIMARY KEY(Id),
	PlateNumber VARCHAR(50) UNIQUE NOT NULL,
	Model NVARCHAR(50) NOT NULL,
	CarYear INT NOT NULL,
	CategoryId INT NOT NULL,
	Doors INT,
	Picture VARBINARY(MAX),
	Condition NVARCHAR(50),
	Available BIT NOT NULL
)

ALTER TABLE Cars
ADD CONSTRAINT CNST_Default_Doors_Number DEFAULT 4 FOR Doors 


CREATE TABLE Employees (
	Id INT IDENTITY,
	CONSTRAINT PK_Id_Employees PRIMARY KEY (Id),
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	Title NVARCHAR(50),
	Notes NVARCHAR(MAX)
)

CREATE TABLE Customers (
	Id INT IDENTITY,
	CONSTRAINT PK_Id_Customers PRIMARY KEY (Id),
	DriverLicenceNumber INT UNIQUE NOT NULL,
	FullName NVARCHAR(100) NOT NULL,
	[Address] NVARCHAR(50),
	City NVARCHAR(50) NOT NULL,
	ZIPCode INT NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE RentalOrders (
	Id INT IDENTITY,
	CONSTRAINT PK_Id_RentalOrders PRIMARY KEY(Id),
	EmployeeId INT NOT NULL,
	CustomerId INT NOT NULL,
	CarId INT NOT NULL,
	TankLevel NUMERIC (10, 2),
	KilometrageStart BIGINT,
	KilometrageEnd BIGINT,
	TotalKilometrage BIGINT,
	StartDate DATE DEFAULT GETDATE(),
	EndDate DATE,
	TotalDays INT,
	RateApplied NVARCHAR(50),
	TaxRate NVARCHAR(50),
	OrderStatus NVARCHAR(50),
	Notes NVARCHAR(255)
)

INSERT INTO Categories(CategoryName, MonthlyRate) VALUES
	('Some cathegory', 22),
	('Some other cathegory', 22),
	('Another cathegory', 22)

INSERT INTO Cars(PlateNumber, Model, CarYear, CategoryId, Doors, Available) VALUES
	('11a223', 'BMW Black', 1993, 1, 5, 1),
	('11b223', 'Truck', 1993, 3, 2, 0)

INSERT INTO Cars(PlateNumber, Model, CarYear, CategoryId, Available) VALUES
	('11c223', 'BMW Black', 1993, 1, 1)

INSERT INTO Employees(FirstName,LastName,Title,Notes) VALUES
	('Gosho','Peshov','Software Developer',NULL),
	('Pesho','Goshov', NULL, NULL),
	('Mariika','Petrova','Doctor',NULL)

INSERT INTO Customers (DriverLicenceNumber, FullName, City, ZIPCode) VALUES
	(10101010, 'Pesho', 'Sofia', 2600), 
	(1010101, 'Gosho', 'Plovdiv', 2700), 
	(10101012, 'Mordred', 'London', 2800) 

INSERT INTO RentalOrders (EmployeeId, CustomerId, CarId) VALUES
	(1, 1, 3),
	(1, 2, 1),
	(2, 3, 1)