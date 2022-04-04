CREATE DATABASE TOURDL
GO

USE TOURDL
GO

/*******TAI KHOAN QUAN TRI***************/
Create Table Admins 
(
	UserAdmin varchar(30) primary key,
	PassAdmin varchar(30) not null,
	Hoten nVarchar(50)
)
Go


/*************LOAI TOUR**************/
CREATE TABLE LOAITOUR
(
	IDLOAI CHAR(3) PRIMARY KEY NOT NULL,
	TENLOAI NVARCHAR(200) NOT NULL,
)
GO

/*************MIEN**************/
CREATE TABLE MIEN
(
	IDMIEN CHAR(3) PRIMARY KEY,
	TENMIEN NVARCHAR(5)
)
GO


/*************TOUR DU LICH**************/
CREATE TABLE TOURDULICH
(
	IDTOUR CHAR(3) PRIMARY KEY NOT NULL,
	TENTOUR NVARCHAR(200) NOT NULL,
	ANH NVARCHAR(50),
	GIA FLOAT,
	SONGAY INT DEFAULT 1,
	MOTA NVARCHAR(MAX),
	NGAYXUATPHAT DATE,
	DIEMDL NVARCHAR(50),
	IDLOAI CHAR(3) REFERENCES LOAITOUR(IDLOAI),
	IDMIEN CHAR(3) REFERENCES MIEN(IDMIEN)
)
GO

/*************KHACH HANG**************/
CREATE TABLE KHACHHANG
(
	IDKH INT PRIMARY KEY NOT NULL IDENTITY(1,1),	
	TENKH NVARCHAR(40) NOT NULL,
	Taikhoan Varchar(50) UNIQUE,
	Matkhau Varchar(50) NOT NULL,
	CMND VARCHAR(15),
	SDT VARCHAR(22),
	EMAIL VARCHAR(30)
)
GO

/*************KHACH SAN**************/
CREATE TABLE KHACHSAN
(
	IDKS CHAR(3) PRIMARY KEY NOT NULL,
	TENKS NVARCHAR(40) NOT NULL,
	DIACHI NVARCHAR(50),
	SDTKS VARCHAR(22),
	GIA FLOAT,
	ANH NVARCHAR(50),
	IDMIEN CHAR(3) REFERENCES MIEN(IDMIEN),
	TINHTRANG NVARCHAR(5),
	MOTA NVARCHAR(MAX),
)
GO

/*************DON DAT PHONG KHACH SAN**************/
CREATE TABLE VEPHONG
(
	IDDON INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	IDKH INT REFERENCES KHACHHANG(IDKH),
	IDKS CHAR(3) REFERENCES KHACHSAN(IDKS)
)
GO



/*************VE TOUR DU LICH**************/
CREATE TABLE VETOUR
(
	IDVE INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	IDTOUR CHAR(3) REFERENCES TOURDULICH(IDTOUR),
	IDKH INT REFERENCES KHACHHANG(IDKH)
)
GO


/*************XE DU LICH**************/
CREATE TABLE PHUONGTIEN
(
	IDPT CHAR(3) PRIMARY KEY NOT NULL,
	TENPT NVARCHAR(40) NOT NULL,
	LOAIPT NVARCHAR(20),
	SUCCHUA VARCHAR(2),
	ANH NVARCHAR(50),
	MOTA NVARCHAR(MAX),
)
GO

/*************DON DAT XE**************/
CREATE TABLE VEXE
(
	IDVEXE INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	NGAYXP DATE,
	IDKH INT REFERENCES KHACHHANG(IDKH),
	IDPT CHAR(3) REFERENCES PHUONGTIEN(IDPT)
)
GO


/*********************************INSERT******************************/
SET DATEFORMAT dmy;

INSERT INTO Admins VALUES('LONG1','123',' Hoang Long');


INSERT INTO LOAITOUR VALUES('LO1',N'Cá nhân');
INSERT INTO LOAITOUR VALUES('LO2',N'Gia đình');
INSERT INTO LOAITOUR VALUES('LO3',N'Tổ chức');

INSERT INTO MIEN VALUES('MIT',N'Trung');
INSERT INTO MIEN VALUES('MIN',N'Nam');
INSERT INTO MIEN VALUES('MIB',N'Bắc');

INSERT INTO TOURDULICH VALUES('DL1',N'Du lịch Phan Thiết ','Phanthiet.jpg',3500000,2,N'Sáng: Xvà tạo màu bằng hàng ngàn đèn LED 256 màu.','30/01/2021',N'Phan Thiết-Bình Thuận','LO3','MIB');
INSERT INTO TOURDULICH VALUES('DL2',N'Nha Trang Vinpearland','Nhatrang.jpg',5000000,3,N'Du lịch Nha Trang Tết Tân Sửu 2021, du khách cùng Du Lịch Việt khám phá các điểm du lịch nổi tiếng như Vinpearl trên đảo Hòn Tre,... Để tới khu vui chơi này thì bạn nên đi cáp treo để có thể trải nghiệm cảm giác ở giữa không trung và phía dưới là biển cả bao la. Tại khu vui chơi này bạn sẽ được trải nghiệm các trò cảm giác mạnh, vui nhộn, thú vị mà gia đình bạn già trẻ ai cũng thích hết. Nha Trang không chỉ đẹp về cảnh sắc thiên nhiên lẫn con người mà đồ ăn cũng cực kỳ phong phú, ngon miệng.
 
tag: Du lịch Nha Trang Tết Nguyên Đán, Du lịch Nha Trang Tết nguyên đán 2021, Du lịch Nha Trang Tết Tân Sửu, Tour Nha Trang Tet Nguyen Đan, Tìm Tour Nha Trang Tet Nguyen Đan, Du lich Nha Trang Tet Am lich 2021.','2/2/2021','Nha Trang','LO2','MIT');
INSERT INTO TOURDULICH VALUES('DL3',N'Tour Đảo Bình Ba','Binhba.jpg',2890000,2,N'Du lịch Nha Trang Tết Nguyên Đán 2021 đi Đảo Bình Ba - Còn gì tuyệt vời hơn, khi đón những ngày đầu năm mới tại hòn đảo hoang sơ Đảo Bình Ba - Một bức tranh thiên nhiên tuyệt đẹp được vẽ nên bởi biển trời xanh như ngọc, quanh năm nắng trải dài trên những triền cát vàng lấp lánh!','5/3/2021','Nha Trang','LO1','MIN');

INSERT INTO KHACHHANG VALUES(N'Nguyễn Ngọc Hiền',N'hien@',N'123',225717939,0374334682,'hien@gmail.com');
INSERT INTO KHACHHANG VALUES(N'Nguyễn Hoàng Long',N'long@',N'123',225717939,0374334682,'long@gmail.com');

INSERT INTO KHACHSAN VALUES('KS1',N'Bạch Mai',N'Thủ Đức',0123456789,2000,'a1.jpg','MIT',N'Còn',N'Số lượng 11 phòng. Mỗi phòng có diện tích 45m2. Nội thất - tiện nghi nổi bật gồm 2 giường đơn/1 giường đôi, phòng tắm. Phòng có sức chứa tối đa 02 người.');
INSERT INTO KHACHSAN VALUES('KS2',N'Tân Xuân',N'Bình Thạnh',0123456789,5000,'a2.jpg','MIB',N'Còn',N'Số lượng 14 phòng. Mỗi phòng có diện tích 45m2. Nội thất - tiện nghi nổi bật gồm giường 2 giường đơn/1 giường đôi, phòng tắm (Vì số lượng phòng có bồn tắm nằm ít, khách hàng vui lòng liên hệ resort hoặc Chăm sóc khách hàng Chudu24 để được sắp xếp phòng). Phòng có sức chứa tối đa 02 người.');
INSERT INTO KHACHSAN VALUES('KS3',N'Tương Lai',N'Linh Trung',0123456789,3000,'a3.jpg','MIB',N'Còn',N'Số lượng 46 phòng. Mỗi phòng có diện tích 45m2. Nội thất - tiện nghi nổi bật gồm giường 2 giường đơn/1 giường đôi, phòng tắm (Vì số lượng phòng có bồn tắm nằm ít, khách hàng vui lòng liên hệ resort hoặc Chăm sóc khách hàng Chudu24 để được sắp xếp phòng). Phòng có sức chứa tối đa 02 người.');

INSERT INTO PHUONGTIEN VALUES('PT1','Mercedes S500',N'4 chổ',4,'anh1.jpg',N'Xe được trang bị màn hình tivi với hàng chục kênh truyền hình, âm thanh nổi 3D từ hệ thống loa Burmester có công suất tối đa 1540W, sẽ mang lại những trải nghiệm thư giãn êm dịu chưa từng có.');
INSERT INTO PHUONGTIEN VALUES('PT2','Limousine dcar',N'6 chổ',6,'anh2.jpg',N'Ấn tượng đầu tiên khi bước vào khoang hành khách là hệ thống đèn LED hình giọt nước rực rỡ, với hàng trăm bóng đèn nhỏ được cài đặt tinh tế trên trần và hai bên cạnh xe. Thiết kế đặc biệt này, khiến cho khoang hành khách trở nên bừng sáng hơn. Dưới ánh đèn LED rực rỡ, những chi tiết ốp gỗ trang trí nâu bóng kết hợp cùng tông màu trắng kem của nội thất, đã tạo nên những vẻ đẹp nổi bật và quyến rũ.');
INSERT INTO PHUONGTIEN VALUES('PT3',N'xe 39 chổ',N'39 chổ',35,'anh3.jpg',N'Từ Tp. Hồ Chí Minh hay Hà Nội, không khó để thuê xe 39 chỗ, thuê xe dịch vụ di chuyển đến các tỉnh thành khắp cả nước. Tuy nhiên, để có một chiếc xe êm ái và thuận tiện thì vẫn còn đó những phân vân về giá cả cũng như chất lượng dịch vụ.');

INSERT INTO VETOUR VALUES('');
INSERT INTO VETOUR VALUES('');

INSERT INTO VEXE VALUES('');
INSERT INTO VEXE VALUES('');

INSERT INTO VEPHONG VALUES('');
INSERT INTO VEPHONG VALUES('');


SELECT *FROM TOURDULICH
SELECT *FROM LOAITOUR
SELECT *FROM KHACHHANG
SELECT *FROM KHACHSAN
SELECT *FROM MIEN
SELECT *FROM PHUONGTIEN


SELECT *FROM VEPHONG
SELECT *FROM VEXE
SELECT *FROM VETOUR