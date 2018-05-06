CREATE TABLE People (
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(200) NOT NULL,
	Picture VARBINARY(MAX),
	Height NUMERIC (4, 2),
	Weight NUMERIC (6, 2),
	Gender CHAR NOT NULL CHECK (Gender = 'm' or Gender = 'f'),
	Birthdate DATE NOT NULL,
	Biography NVARCHAR(MAX) 
)

INSERT INTO PEOPLE (Name, Gender, Birthdate) VALUES
	('Anne', 'f', '1990-11-10'),
	('Tom', 'm', '1990-11-10'),
	('Rick', 'm', '1990-11-10'),
	('Clown', 'm', '1990-11-10'),
	('Dwarf', 'f', '1990-11-10')

CREATE TABLE Users (
	Id BIGINT PRIMARY KEY IDENTITY,
	Username VARCHAR(30) NOT NULL,
	Password VARCHAR(26) NOT NULL,
	ProfilePicture VARBINARY(MAX),
	LastLoginTime DATE,
	IsDeleted BIT CHECK (IsDeleted = 0 OR IsDeleted = 1)
) 

INSERT INTO Users (Username, Password) VALUES
	('GRRRsd', '123456'),
	('Anne', 'SDAS3'),
	('Tom', '4SDFSS'),
	('Rick', 'FGDFG'),
	('Clown', '4T5E'),
	('Dwarf', 'GFDG')

ALTER TABLE Users
	DROP CONSTRAINT [PK__Users__3214EC07295F531D]

ALTER TABLE Users
	ADD CONSTRAINT	UQ_Users UNIQUE (Username)

ALTER TABLE Users
	ADD CONSTRAINT PK_Users PRIMARY KEY(Id, Username)

ALTER TABLE Users
	ADD CONSTRAINT CHK_ProfilePictureSize CHECK (DATALENGTH(ProfilePicture) <= 900 * 1024)

ALTER TABLE Users
	ADD CONSTRAINT CHK_PasswordSize CHECK (DATALENGTH(Password) >= 5)

ALTER TABLE Users
	ADD DEFAULT GETDATE() FOR LastLoginTime

ALTER TABLE Users
	DROP CONSTRAINT [PK_Users]

ALTER TABLE Users
	ADD CONSTRAINT PK_User_ID	PRIMARY KEY (Id)

ALTER TABLE Users
	ADD CONSTRAINT Username_Lenght CHECK (LEN(Username) >= 3)