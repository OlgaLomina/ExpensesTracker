-- SQL Create Database Example
IF EXISTS 
   (
     SELECT name FROM master.dbo.sysdatabases 
     WHERE name = N'Expensesdb'
    )
BEGIN
	SELECT 'Database Name already Exist' AS Message
END
ELSE
BEGIN
	CREATE DATABASE Expensesdb
	SELECT 'New Database is Created'
END

use Expensesdb;

CREATE TABLE Roles(
   Role_id INT NOT NULL,
   Name VARCHAR (25) NOT NULL,
   PRIMARY KEY (Role_id)
);

CREATE TABLE Users (
Userid INT IDENTITY (1, 1) PRIMARY KEY,
 Role_id INT NOT NULL,
 Manager_id INT,
 First_name VARCHAR (25) NOT NULL,
 Last_name VARCHAR (25) NOT NULL,
 Email VARCHAR (25) NOT NULL UNIQUE,
 User_password VARCHAR (25) NOT NULL,
 Date_time DATETIME,
 Comment VARCHAR (255),
 FOREIGN KEY (role_id) 
        REFERENCES roles (Role_id) 
);

CREATE TABLE Expenses (
 Expense_id INT IDENTITY (1, 1) PRIMARY KEY,
 Userid INT NOT NULL,
 Name VARCHAR (25) NOT NULL,
 Date_time DATETIME,
 Amount DECIMAL (18,2), 
 Comment VARCHAR (255),
 FOREIGN KEY (userid) 
        REFERENCES users (Userid)
		ON DELETE CASCADE ON UPDATE CASCADE,
);

INSERT INTO Roles (Role_id,Name)
VALUES (0, 'admin');

INSERT INTO roles (Role_id, Name)
VALUES (1, 'manager');

INSERT INTO roles (role_id, name)
VALUES (2, 'user');

------------------------------

INSERT INTO users (role_id, first_name, last_name, email, user_password, date_time)
VALUES (2, 'Tom', 'First', 'tom.first@gmail.com', 'Tom12345', '19-jun-2019' );


--------------------
INSERT INTO expenses (userid, name, amount, date_time)
VALUES (1, 'Costco gas', 34.23, '12-mar-2013' );
INSERT INTO expenses (userid, name, amount, date_time, comment)
VALUES (1, 'Costco', 156.00, '26-mar-2013', 'Food');



ALTER TABLE users 
ALTER COLUMN  User_password VARCHAR (25) NOT NULL;

ALTER TABLE Expenses 
ALTER COLUMN   Amount DECIMAL (18,2);