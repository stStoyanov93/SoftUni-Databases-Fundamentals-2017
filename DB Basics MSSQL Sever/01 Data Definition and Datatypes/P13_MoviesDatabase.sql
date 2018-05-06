CREATE DATABASE Movies
USE Movies
GO

CREATE TABLE Directors (
	Id INT IDENTITY,
	DirectorName NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX),
	CONSTRAINT PK_Id_Directors PRIMARY KEY (Id) 
)

CREATE TABLE Genres (
	Id INT IDENTITY,
	GenreName NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX),
	CONSTRAINT PK_Id_Genres PRIMARY KEY (Id) 
)

CREATE TABLE Categories (
	Id INT IDENTITY,
	CategoryName NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX),
	CONSTRAINT PK_Id_Categories PRIMARY KEY (Id)
)

CREATE TABLE Movies (
	Id INT IDENTITY,
	Title NVARCHAR(50) NOT NULL,
	DirectorId INT NOT NULL,
	CopyrightYear DATE DEFAULT GETDATE(),
	[Length] INT,
	GenreId INT NOT NULL,
	CategoryId INT NOT NULL,
	Rating INT,
	Notes NVARCHAR(MAX),
	CONSTRAINT PK_Id_Movies PRIMARY KEY (Id),
	CONSTRAINT CH_Rating CHECK (Rating >= 1 AND Rating <= 5)
)

INSERT INTO Directors (DirectorName, Notes) VALUES
	('Stephen Spielberg', 'All is fine...'),
	('Stephen Mielberg', 'Check check check...'),
	('Stephen Pielberg', NULL),
	('Stephen Drielberg', 'huh'),
	('Stephen Frielberg', NULL)

INSERT INTO Genres (GenreName) VALUES
	('Horror'),
	('Action'),
	('Comedy'),
	('Fantasy'),
	('Dramedy')

INSERT INTO Categories (CategoryName) VALUES
	('Oscars'),
	('Emmies'),
	('BAFTA'),
	('MAFTA'),
	('AFTA')

INSERT INTO Movies(Title, DirectorID, GenreID, CategoryID,Rating) Values
	('Scary Movie', 11233412, 643675, 3, 2),
	('Action Movie', 535123, 123453, 2, 4),
	('Erotic Movie', 7657457, 51532, 1, 3),
	('Love movie', 123547568, 4343, 4, 2),
	('Dramatic Movie', 97876543, 123, 1, 5)