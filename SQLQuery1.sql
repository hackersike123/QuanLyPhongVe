CREATE DATABASE QuanLyPhongVe;
GO
USE QuanLyPhongVe;
GO
CREATE TABLE Users (
    UserID INT IDENTITY PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(50) NOT NULL,
    Role NVARCHAR(20) NOT NULL
);
INSERT INTO Users (Username, Password, Role)
VALUES 
('admin', '123', 'Admin'),
('nhanvien', '123', 'Staff');
SELECT * FROM Users;
