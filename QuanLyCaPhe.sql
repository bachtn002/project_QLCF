CREATE DATABASE qlcaphe
use qlcaphe

GO
CREATE TABLE TableCoffee(
id INT IDENTITY PRIMARY KEY,
nameTable NVARCHAR(100) NOT NULL,
statusTable NVARCHAR(100) NOT NULL DEFAULT N'Trống' -- Trống || Có người
)
INSERT INTO TableCoffee(nameTable)
VALUES(N'Bàn 1')
INSERT INTO TableCoffee(nameTable)
VALUES(N'Bàn 2')
INSERT INTO TableCoffee(nameTable)
VALUES(N'Bàn 3')
INSERT INTO TableCoffee(nameTable)
VALUES(N'Bàn 4')
INSERT INTO TableCoffee(nameTable)
VALUES(N'Bàn 5')
INSERT INTO TableCoffee(nameTable)
VALUES(N'Bàn 6')
GO
CREATE TABLE Acount(
displayName NVARCHAR(100) NOT NULL,
userName NVARCHAR(100) PRIMARY KEY NOT NULL,
passWord NVARCHAR(100) NOT NULL,
typeAccount INT NOT NULL  DEFAULT 0-- 1 : admin; 0: staff
)
INSERT INTO Acount(displayName, userName, passWord, typeAccount)
VALUES(N'NgocLam',N'admin',N'1234',N'0')

INSERT INTO Acount(displayName, userName, passWord, typeAccount)
VALUES(N'NgocLam',N'admin1',N'12345',N'0')
GO
CREATE TABLE FoodCategory(
id INT IDENTITY PRIMARY KEY,
nameCategory NVARCHAR(100) NOT NULL,
)
INSERT INTO FoodCategory(nameCategory)
VALUES(N'Hải sản')
INSERT INTO FoodCategory(nameCategory)
VALUES(N'Cafe truyền thống')
GO
CREATE TABLE Food(
nameFood NVARCHAR(100) NOT NULL,
priceFood FLOAT NOT NULL DEFAULT 0,
id INT IDENTITY PRIMARY KEY,
idCategory INT NOT NULL,
FOREIGN KEY (idCategory) REFERENCES FoodCategory(id)
)
INSERT INTO Food(nameFood,idCategory, priceFood)
VALUES(N'Tôm càng xanh',1,5500000)
INSERT INTO Food(nameFood,idCategory, priceFood)
VALUES(N'Cua sốt me',1,6500000)
INSERT INTO Food(nameFood,idCategory, priceFood)
VALUES(N'Tu hài',1,4500000)
INSERT INTO Food(nameFood,idCategory, priceFood)
VALUES(N'Lẩu hải sản',1,7500000)

INSERT INTO Food(nameFood,idCategory, priceFood)
VALUES(N'Cafe đen đường',2,550000)
INSERT INTO Food(nameFood,idCategory, priceFood)
VALUES(N'Cafe đen đá',2,550000)
INSERT INTO Food(nameFood,idCategory, priceFood)
VALUES(N'Cafe đen sữa',2,550000)
INSERT INTO Food(nameFood,idCategory, priceFood)
VALUES(N'Cafe nâu',2,550000)
INSERT INTO Food(nameFood,idCategory, priceFood)
VALUES(N'Cafe nâu chuối',2,550000)
GO
CREATE TABLE Bill(
id INT IDENTITY PRIMARY KEY,
dateCheckIn DATETIME NOT NULL DEFAULT GETDATE(),
dateCheckOut DATETIME ,
idTable INT NOT NULL,
statusBill INT NOT NULL, 
discountCoffee INT DEFAULT 0,
FOREIGN KEY (idTable) REFERENCES TableCoffee(id)
)
GO
CREATE TABLE BillInfor(
id INT IDENTITY PRIMARY KEY,
idBill INT NOT NULL,
idFood INT NOT NULL,
countFood INT NOT NULL DEFAULT 0,

FOREIGN KEY (idFood) REFERENCES Food(id),
FOREIGN KEY (idBill) REFERENCES Bill(id)
)
GO
CREATE PROCEDURE SP_checkLogin4
@userName NVARCHAR(100), @passWord NVARCHAR(100)
AS
SELECT * FROM Acount WHERE userName = @userName AND passWord = @passWord
GO
CREATE PROCEDURE SP_loadNameTable
@id INT
AS
SELECT * FROM TableCoffee WHERE id=@id
GO
CREATE PROC SP_ListTableCoffee
AS
SELECT * FROM TableCoffee 
GO
CREATE PROC SP_GetIdBillByIdTable
@IdTable INT
AS
SELECT * FROM Bill WHERE idTable=@IdTable AND statusBill=0
GO
CREATE PROC SP_GetListHoaDon1
@idTable1 INT
AS
SELECT * FROM Bill AS b, BillInfor AS bi, Food AS f, TableCoffee AS tc
WHERE b.idTable=tc.id AND bi.idBill=b.id AND bi.idFood=f.id AND tc.id=@idTable1
GO
CREATE PROC SP_GetDanhSachMon
@idTable INT
AS
SELECT F.nameFood, BI.countFood, F.priceFood, F.priceFood*BI.countFood AS priceTotal 
FROM Bill AS B, BillInfor AS BI, Food AS F , TableCoffee AS T 
WHERE BI.idBill=B.id AND BI.idFood=F.id AND F.id=BI.idFood AND B.idTable= T.id AND B.statusBill=0 AND T.id=1

GO
CREATE PROC SP_UpdateBillInforByCountFoodDropped
@idBill INT, @idFood INT, @countFood INT

AS
	DECLARE @isExitBillInfor INT;
	DECLARE @foodCount INT;
	SELECT @isExitBillInfor=id, @foodCount=countFood FROM BillInfor 
	WHERE idBill=@idBill AND idFood=@idFood;

	IF(@isExitBillInfor>0)
	BEGIN
		DECLARE @newCount INT=@foodCount-@countFood
		IF(@newCount>0)
			UPDATE BillInfor SET countFood=@foodCount-@countFood 
			WHERE @idBill=idBill AND @idFood=idFood
		ELSE
			DELETE BillInfor WHERE @idBill=idBill AND @idFood=idFood;
	END
	ELSE
	BEGIN
		DELETE BillInfor WHERE @idBill=idBill AND @idFood=idFood;
	END
GO
CREATE PROC SP_InsertBill
@idTable INT
AS
INSERT INTO Bill(dateCheckIn, dateCheckOut, idTable, statusBill, discountCoffee)
VALUES(GETDATE() , NULL , @idTable , 0 , 0);
GO
CREATE PROC SP_InsertBillInfor
@idBill INT, @idFood INT, @countFood INT
 
AS
	DECLARE @isExitBillInfor INT;
	DECLARE @foodCount INT=1;

	SELECT @isExitBillInfor= id, @foodCount=countFood FROM BillInfor 
	WHERE idBill=@idBill AND idFood=@idFood;

	IF(@isExitBillInfor>0)
	BEGIN
		DECLARE @newCount INT =@foodCount+@countFood 
		IF(@newCount>0)
			UPDATE BillInfor SET countFood=@foodCount+@countFood WHERE idBill=@idBill AND idFood=@idFood
		ELSE
			DELETE BillInfor  WHERE idBill=@idBill AND idFood=@idFood
	END
	ELSE
	BEGIN
		INSERT INTO BillInfor(idBill, idFood, countFood)
		VALUES(@idBill, @idFood, @countFood)
	END
GO
CREATE TRIGGER TG_UpdateBillInfor
ON BillInfor FOR INSERT, UPDATE
AS
	DECLARE @idBill INT
	SELECT @idBill= idBill FROM inserted
	DECLARE @idTable INT
	SELECT @idTable= idTable FROM Bill WHERE id=@idBill AND statusBill=0
	DECLARE @count INT
	SELECT @count=COUNT(*) FROM BillInfor WHERE idBill=@idBill
	IF(@count>0)
	BEGIN
	UPDATE TableCoffee SET statusTable =N'Có người' WHERE id=@idTable
	END
	ELSE
	BEGIN
	UPDATE TableCoffee SET statusTable =N'Trong' WHERE id=@idTable
	END
GO
CREATE TRIGGER TG_UpdateBill
ON Bill FOR  UPDATE
AS
	DECLARE @idBill INT
	SELECT @idBill=id FROM inserted
	DECLARE @idTable INT
	SELECT @idTable=idTable FROM Bill WHERE id=@idBill
	DECLARE @count INT =0;
	SELECT @count=COUNT(*) FROM Bill WHERE idTable=@idTable AND statusBill=0
	IF(@count=0)
	BEGIN
	UPDATE TableCoffee SET statusTable=N'Trống' WHERE id=@idTable
	END
GO
ALTER PROC SP_SwitchTable

@idTable1 INT, @idTable2 INT
AS
DECLARE @idBill1 INT
DECLARE @idBill2 INT
DECLARE @isTableBill1 INT=1
DECLARE @isTableBill2 INT=1


SELECT @idBill1=id FROM Bill WHERE idTable=@idTable1 AND statusBill=0
SELECT @idBill2=id FROM Bill WHERE idTable=@idTable2 AND statusBill=0
DECLARE @countBillIF INT
DECLARE @countBillIF1 INT
SELECT @countBillIF = COUNT(*) FROM BillInfor WHERE idBill=@idBill1
SELECT @countBillIF1 = COUNT(*) FROM BillInfor WHERE idBill=@idBill2

IF(@idBill1 IS NULL )
BEGIN
	INSERT Bill(dateCheckIn, dateCheckOut, idTable, statusBill)
	VALUES(GETDATE(),NULL, @idTable1, 0)

	SELECT @idBill1=MAX(id) FROM Bill WHERE idTable=@idTable1 AND statusBill=0

END
SELECT @isTableBill1=COUNT(*) FROM BillInfor WHERE idBill=@idBill1
 IF(@idBill2 IS NULL  )
BEGIN
	INSERT Bill(dateCheckIn, dateCheckOut, idTable, statusBill)
	VALUES(GETDATE(),NULL, @idTable2, 0)

	SELECT @idBill2=MAX(id) FROM Bill WHERE idTable=@idTable2 AND statusBill=0
END

SELECT @isTableBill2=COUNT(*) FROM BillInfor WHERE idBill=@idBill2

SELECT id INTO IdBillInforTable FROM BillInfor WHERE idBill=@idBill2
UPDATE BillInfor SET idBill=@idBill2 WHERE idBill=@idBill1
UPDATE BillInfor SET idBill=@idBill1 WHERE id IN(SELECT * FROM IdBillInforTable)


DROP TABLE IdBillInforTable
IF(@isTableBill1=0)
	UPDATE TableCoffee SET statusTable=N'Trống' WHERE id=@idTable2
IF(@isTableBill2=0)
	UPDATE TableCoffee SET statusTable=N'Trống' WHERE id=@idTable1
GO
SELECT * FROM Acount
SELECT * FROM TableCoffee
SELECT * FROM Bill
SELECT * FROM BillInfor
SELECT * FROM Food
SELECT * FROM FoodCategory

DELETE BillInfor
DELETE Bill
UPDATE TableCoffee SET statusTable=N'Trống'

GO
USE qlcaphe
ALTER TABLE Bill ADD totalPrice FLOAT
GO
ALTER PROC SP_GetListBillByDate
@dateCheckIn DATETIME, @dateCheckOut DATETIME
AS
BEGIN
SELECT t.nameTable AS [Tên bàn], b.dateCheckIn AS [Ngày CheckIn], b.dateCheckOut AS [Ngày CheckOut], b.totalPrice AS [Tổng tiền] 
FROM Bill as b, TableCoffee as t
WHERE dateCheckIn>=@dateCheckIn AND dateCheckOut<=@dateCheckOut AND statusBill=1 AND t.id=b.idTable
END

GO

