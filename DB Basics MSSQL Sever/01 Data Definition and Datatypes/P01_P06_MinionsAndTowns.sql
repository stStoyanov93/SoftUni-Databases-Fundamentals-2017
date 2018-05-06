CREATE DATABASE Minions

USE Minions

CREATE TABLE Minions (
	Id INT PRIMARY KEY,
	Name NVARCHAR(50),
	Age INT
)

CREATE TABLE Towns (
	Id INT PRIMARY KEY,
	Name NVARCHAR(50)
)

ALTER TABLE Minions
	ADD CONSTRAINT FK_Town FOREIGN KEY (TownId) REFERENCES Towns(Id)


INSERT INTO Towns (Id, Name) VALUES
	(1, 'Sofia'),
	(2, 'Plovdiv'),
	(3, 'Varna')	

INSERT INTO Minions (Id, Name, Age, TownId) Values
	(1, 'Kevin', 22, 1),
	(2, 'Bob', 15, 3),
	(3, 'Steward', NULL, 2)

TRUNCATE TABLE Minions
DROP TABLE Towns
DROP TABLE Minions