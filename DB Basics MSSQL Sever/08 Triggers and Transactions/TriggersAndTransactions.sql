--USE Bank
--GO
--USE Diablo
--GO
--USE SoftUni
--GO

--1
CREATE TRIGGER tr_InsertNewLogEntryAfterBalanceChange ON Accounts
AFTER UPDATE
AS
BEGIN

	DECLARE @accountId INT = (SELECT Id FROM inserted)
	DECLARE @oldBalance MONEY = (SELECT Balance FROM deleted)
	DECLARE @newBalance MONEY = (SELECT Balance FROM inserted)
	
	IF(@NewBalance <> @OldBalance)
	BEGIN
		INSERT INTO Logs VALUES (@AccountId, @OldBalance, @NewBalance)
	END
	
END

--2
CREATE TRIGGER tr_LogsNotificationEmails ON Logs 
AFTER INSERT
AS
BEGIN

  DECLARE @recipient INT = (SELECT AccountId FROM inserted);
  DECLARE @oldBalance MONEY = (SELECT OldSum FROM inserted);
  DECLARE @newBalance MONEY = (SELECT NewSum FROM inserted);
  DECLARE @subject VARCHAR(200) = CONCAT('Balance change for account: ', @recipient);
  DECLARE @body VARCHAR(200) = CONCAT('On ', GETDATE(), ' your balance was changed from ', @oldBalance, ' to ', @newBalance, '.');  

  INSERT INTO NotificationEmails (Recipient, Subject, Body) 
  VALUES (@recipient, @subject, @body)
  
END

--3
CREATE PROCEDURE usp_DepositMoney (@accountId INT, @moneyAmount MONEY) 
AS
IF (@moneyAmount >= 0)

BEGIN
	UPDATE Accounts
    SET Balance += @moneyAmount
	WHERE Id = @accountId
END

--4
CREATE PROCEDURE usp_WithdrawMoney (@accountId INT, @moneyAmount DECIMAL(15, 4))
AS
IF (@moneyAmount >= 0)

BEGIN
	UPDATE Accounts
	SET Balance -= @moneyAmount
	WHERE Id = @accountId
END

--5
CREATE PROCEDURE usp_TransferMoney(@senderId INT, @receiverId INT, @amount DECIMAL(15,4))
AS
BEGIN

	BEGIN TRANSACTION
	EXEC dbo.usp_WithdrawMoney @senderId, @amount
	EXEC dbo.usp_DepositMoney @receiverId, @amount

	DECLARE @senderBalance DECIMAL;
	SET @senderBalance = (SELECT Balance FROM Accounts WHERE Id = @senderId)

	IF (@senderBalance < 0)
	BEGIN
		ROLLBACK
	END
	ELSE
	BEGIN
		COMMIT
	END
	
END

--9
CREATE TRIGGER tr_SavedDeletedEmployees ON Employees
AFTER DELETE 
AS
BEGIN

  INSERT INTO Deleted_Employees
  SELECT FirstName, 
         LastName, 
		 MiddleName, 
  	     JobTitle, 
  	     DepartmentID, 
  	     Salary 
  FROM deleted
  
END