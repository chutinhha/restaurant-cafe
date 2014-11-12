USE [Karaoke]
GO
DELETE FROM THE
DBCC CHECKIDENT (THE, RESEED, 0)
GO
DELETE FROM MENUKHUYENMAI
DELETE FROM MENUITEMMAYIN
GO
DELETE FROM MENUGIA
DBCC CHECKIDENT (MENUGIA, RESEED, 0)
GO
DELETE FROM DINHLUONG
DBCC CHECKIDENT (DINHLUONG, RESEED, 0)
GO
DELETE FROM MENUKICHTHUOCMON
DBCC CHECKIDENT (MENUKICHTHUOCMON, RESEED, 0)
GO
DELETE FROM MENUMON
DBCC CHECKIDENT (MENUMON, RESEED, 0)
GO
DELETE FROM MENUNHOM
DBCC CHECKIDENT (MENUNHOM, RESEED, 0)
GO
DELETE FROM LOAIBAN
DBCC CHECKIDENT (LOAIBAN, RESEED, 0)
GO
DELETE FROM DONVI
DBCC CHECKIDENT (DONVI, RESEED, 0)
GO
DELETE FROM CHITIETNHAPKHO
DBCC CHECKIDENT (CHITIETNHAPKHO, RESEED, 0)
GO
DELETE FROM NHAPKHO
DBCC CHECKIDENT (NHAPKHO, RESEED, 0)
GO
DELETE FROM TONKHO
DBCC CHECKIDENT (TONKHO, RESEED, 0)
GO
DELETE FROM LOAIPHATSINH
DBCC CHECKIDENT (LOAIPHATSINH, RESEED, 0)
GO
INSERT INTO LOAIPHATSINH(Ten) Values(N'Nhập kho'), (N'Xuất kho'), (N'Chuyển kho'),(N'Mất kho'),(N'Chỉnh kho');
INSERT INTO DONVI(TenDonVi) Values(N'Số lượng'), (N'Trọng lượng'), (N'Thể tích'),(N'Thời gian');
INSERT INTO LOAIBAN(TenLoaiBan,KichThuocBan,DonViID) Values
(N'Cái',1,1),
(N'Gram',1,2),
(N'Kg',1000,2),
(N'Millilit ',1,3),
(N'Lít',1000,3),
(N'Giờ',3600,4),
(N'Phút',60,4),
(N'Giây',1,4);
GO
INSERT INTO [dbo].[MENUNHOM]([TenNgan],[TenDai],[LoaiNhomID],[MayIn],[SapXep],[GiamGia],[Hinh],[Font],[MauChu],[MauNen],[SoLuongMon],[Visual],[Deleted]) VALUES
(N'Nước',N'Nước',1,0,1,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Rượi',N'Rượi',1,0,2,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Bia',N'Bia',1,0,3,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Đồ Khô',N'Đồ Khô',2,0,1,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Đồ chế biến',N'Đồ chế biến',2,0,2,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Nguyên liệu',N'Nguyên liệu',3,0,1,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Dịch vụ',N'Dịch vụ',5,0,1,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Karaoke',N'Karaoke',4,0,1,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Bánh',N'Bánh',2,0,3,0,NULL,NULL,NULL,NULL,0,1,0);

INSERT INTO [dbo].[MENUMON]([TenNgan],[TenDai],[NhomID],[Gia],[GST],[MayIn],[SapXep],[GiamGia],[Font],[MauChu],[MauNen],[Hinh],[SoLuongKichThuocMon],[Visual],[Deleted]) VALUES
(N'Lavie',N'Lavie',1,59000,10,1,1,0,NULL,NULL,NULL,NULL,0,1,0),
(N'C2',N'C2',1,11000,10,1,2,0,NULL,NULL,NULL,NULL,0,1,0),
(N'0 độ',N'0 độ',1,9000,10,1,3,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Trà đá',N'Trà đá',1,9000,10,1,4,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Tiger',N'Tiger',3,45000,10,1,1,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Sài gòn đỏ',N'Sài gòn đỏ',3,30000,10,1,2,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Sài gòn xanh',N'Sài gòn xanh',3,17000,10,1,3,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Heineken',N'Heineken',3,9000,10,1,4,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Đậu phộng',N'Đậu phộng',4,49000,10,1,1,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Khô mực',N'Khô mực',4,30000,10,1,2,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Tôm khô',N'Tôm khô',4,63000,10,1,3,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Khô đuối',N'Khô đuối',4,20000,10,1,4,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Thèo lèo',N'Thèo lèo',9,57000,10,1,1,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Cơm cháy',N'Cơm cháy',9,67000,10,1,2,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Cá viên chiên',N'Cá viên chiên',5,21000,10,1,1,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Cơn chiên dương châu',N'Cơn chiên dương châu',5,71000,10,1,2,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Ếch xào',N'Ếch xào',5,23000,10,1,3,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Chân gà hấp',N'Chân gà hấp',5,39000,10,1,4,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Cơm cuộn trứng',N'Cơm cuộn trứng',5,91000,10,1,5,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Khoai tây chiên',N'Khoai tây chiên',5,96000,10,1,6,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Đường',N'Đường',6,64000,10,1,1,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Muối',N'Muối',6,70000,10,1,2,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Bột ngọt',N'Bột ngọt',6,97000,10,1,3,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Cà phê',N'Cà phê',6,16000,10,1,4,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Thịt gà',N'Thịt gà',6,54000,10,1,5,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Thịt heo',N'Thịt heo',6,78000,10,1,6,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Thịt bò',N'Thịt bò',6,51000,10,1,7,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Thịt vịt',N'Thịt vịt',6,42000,10,1,8,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Cá diêu hồng',N'Cá diêu hồng',6,81000,10,1,9,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Trứng gà',N'Trứng gà',6,87000,10,1,10,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Trứng vịt',N'Trứng vịt',6,79000,10,1,11,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Đá',N'Đá',6,69000,10,1,12,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Trà',N'Trà',6,10000,10,1,13,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Soda',N'Soda',1,23000,10,1,5,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Cháo vịt',N'Cháo vịt',5,78000,10,1,7,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Cháo gà',N'Cháo gà',5,75000,10,1,8,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Gạo',N'Gạo',6,51000,10,1,14,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Vịt quay',N'Vịt quay',5,97000,10,1,9,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Heo quay',N'Heo quay',5,85000,10,1,10,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Gà nướng',N'Gà nướng',5,7000,10,1,11,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Cá lóc hấp bầu',N'Cá lóc hấp bầu',5,13000,10,1,12,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Cá diêu hồng chiên',N'Cá diêu hồng chiên',5,88000,10,1,13,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Dầu ăn',N'Dầu ăn',6,61000,10,1,15,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Dầu hào',N'Dầu hào',6,43000,10,1,16,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Socola',N'Socola',6,13000,10,1,17,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Nước tương',N'Nước tương',6,43000,10,1,18,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Sting',N'Sting',1,36000,10,1,6,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Cocacola',N'Cocacola',1,22000,10,1,7,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Pepsi',N'Pepsi',1,71000,10,1,8,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Hành lá',N'Hành lá',6,34000,10,1,19,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Tỏi',N'Tỏi',6,47000,10,1,20,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Ớt',N'Ớt',6,12000,10,1,21,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Tiêu',N'Tiêu',6,50000,10,1,22,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Xà bông',N'Xà bông',6,61000,10,1,23,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Thùng bia ngoài',N'Thùng bia ngoài',7,18000,10,1,1,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Bánh sinh nhật',N'Bánh sinh nhật',7,69000,10,1,2,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Đồ ăn ngoài',N'Đồ ăn ngoài',7,99000,10,1,3,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Thùng nước ngọt ngoài',N'Thùng nước ngọt ngoài',7,5000,10,1,4,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Giờ hát',N'Giờ hát',8,25000,10,1,1,0,NULL,NULL,NULL,NULL,0,1,0),
(N'Tẩy đá',N'Tẩy đá',1,66000,10,1,9,0,NULL,NULL,NULL,NULL,0,1,0);

INSERT INTO THE(TenThe,ChietKhau) Values
(N'Đông Á',0),
(N'Vietinbank',0),
(N'Techcombank',0),
(N'HDBank',0),
(N'Agribank',0);