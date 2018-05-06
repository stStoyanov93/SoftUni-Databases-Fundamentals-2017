--1
CREATE TABLE Passports
(
PassportID INT PRIMARY KEY IDENTITY(101,1),
PassportNumber CHAR(8)
)

CREATE TABLE Persons
(
PersonID INT PRIMARY KEY IDENTITY,
FirstName VARCHAR(30),
Salary DECIMAL(10,2),
PassportID INT NOT NULL FOREIGN KEY REFERENCES Passports(PassportID)
)

INSERT INTO Passports VALUES
	('N34FG21B'),
	('K65LO4R7'),
	('ZE657QP2')

INSERT INTO Persons VALUES
	('Roberto', 43300.00, 102),
	('Tom', 56100.00, 103),
	('Roberto', 60200.00, 101)

--2
CREATE TABLE Manufacturers
(
ManufacturerID INT PRIMARY KEY IDENTITY,
[Name] VARCHAR(50) NOT NULL,
EstablishedOn DATE
)

CREATE TABLE Models
(
ModelID INT PRIMARY KEY IDENTITY(101, 1),
[Name] VARCHAR(50) NOT NULL,
ManufacturerID INT FOREIGN KEY REFERENCES Manufacturers(ManufacturerID)
)

INSERT INTO Manufacturers VALUES
	('BMW', '07/03/1916'),
	('Tesla', '01/01/2003'),
	('Lada', '01/05/1966')

INSERT INTO Models VALUES
	('X1', 1),
	('i6', 1),
	('Model S', 2),
	('Model X', 2),
	('Model 3', 2),
	('Nova', 3)

--3
CREATE TABLE Students
(
StudentID INT PRIMARY KEY IDENTITY,
[Name] VARCHAR(50)
)

CREATE TABLE Exams
(
ExamID INT PRIMARY KEY IDENTITY(101, 1),
[Name] VARCHAR(50)
)

CREATE TABLE StudentsExams
(
StudentID INT FOREIGN KEY REFERENCES Students(StudentID),
ExamID INT FOREIGN KEY REFERENCES Exams(ExamID),
CONSTRAINT PK_ExamsStudents PRIMARY KEY(StudentID, ExamID)
)

INSERT INTO Students VALUES
	('Mila'),
	('Toni'),
	('Ron')

INSERT INTO Exams VALUES
	('SpringMVC'),
	('Neo4j'),
	('Oracle 11g')

INSERT INTO StudentsExams VALUES
	(1, 101),
	(1, 102),
	(2, 101),
	(3, 103),
	(2, 102),
	(2, 103)

--4
CREATE TABLE Teachers
(
TeacherID INT PRIMARY KEY IDENTITY(101,1),
Name VARCHAR(50) NOT NULL,
ManagerID INT
)

INSERT INTO Teachers VALUES
	('John', NULL),
	('Maya', 106),
	('Silvia', 106),
	('Ted', 105),
	('Mark', 101),
	('Greta', 101)

ALTER TABLE Teachers
ADD CONSTRAINT FK_ManagerIDTeacher FOREIGN KEY (ManagerID) REFERENCES Teachers(TeacherID)

--5
REATE TABLE Cities(
  CityID INT PRIMARY KEY,
  Name VARCHAR(50) NOT NULL,
)

CREATE TABLE Customers(
  CustomerID INT PRIMARY KEY,
  Name VARCHAR(50) NOT NULL,
  Birthday DATE,
  CityID INT,
  CONSTRAINT FK_Customers_Cities FOREIGN KEY (CityID) REFERENCES Cities(CityID)
)

CREATE TABLE Orders(
  OrderID INT PRIMARY KEY,
  CustomerID INT NOT NULL,
  CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
)

CREATE TABLE ItemTypes(
  ItemTypeID INT PRIMARY KEY,
  Name VARCHAR(50) NOT NULL,
)

CREATE TABLE Items(
  ItemID INT PRIMARY KEY,
  Name VARCHAR(50) NOT NULL,
  ItemTypeID INT NOT NULL,
  CONSTRAINT FK_Items_ItemTypes FOREIGN KEY (ItemTypeID) REFERENCES ItemTypes(ItemTypeID)
)

CREATE TABLE OrderItems(
  OrderID INT NOT NULL,
  ItemID INT NOT NULL,
  CONSTRAINT PK_OrderItems PRIMARY KEY (OrderID, ItemID),
  CONSTRAINT FK_OrderItems_Orders FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
  CONSTRAINT FK_OrderItems_Items FOREIGN KEY (ItemID) REFERENCES Items(ItemID)
)

--7, 8 - Generate E/R diagrams

--9
USE Geography
GO

SELECT m.MountainRange,p.PeakName, p.Elevation FROM Peaks AS p
JOIN Mountains AS m ON p.MountainId = m.Id
WHERE m.MountainRange = 'Rila'
ORDER BY Elevation DESC