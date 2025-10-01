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
USE QuanLyPhongVe;
GO

DROP TABLE IF EXISTS Users;
GO

CREATE TABLE Users (
    UserID INT IDENTITY PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(MAX) NOT NULL, -- lưu chuỗi đã mã hoá AES
    Role NVARCHAR(20) NOT NULL
);

-- Ví dụ thêm 1 admin (mật khẩu "123" đã được mã hoá AES bằng AESHelper)
-- Bạn có thể tự Insert bằng form Đăng ký
UPDATE Users
SET Role = 'Admin'
WHERE Username = 'admin';
-- Bảng Khách hàng
CREATE TABLE KhachHang (
    MaKH INT IDENTITY(1,1) PRIMARY KEY,
    TenKH NVARCHAR(100) NOT NULL,
    SDT NVARCHAR(20),
    Email NVARCHAR(100)
);

-- Bảng Hóa đơn
CREATE TABLE HoaDon (
    MaHD INT IDENTITY(1,1) PRIMARY KEY,
    MaKH INT NOT NULL,
    NgayLap DATETIME DEFAULT GETDATE(),
    TongTien DECIMAL(18,2),
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH)
);

-- Bảng Chi tiết hóa đơn
CREATE TABLE ChiTietHoaDon (
    MaCTHD INT IDENTITY(1,1) PRIMARY KEY,
    MaHD INT NOT NULL,
    TenPhim NVARCHAR(100),
    SoLuong INT,
    DonGia DECIMAL(18,2),
    ThanhTien AS (SoLuong * DonGia) PERSISTED,
    FOREIGN KEY (MaHD) REFERENCES HoaDon(MaHD)
);
