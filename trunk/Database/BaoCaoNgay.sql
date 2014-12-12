USE Karaoke
GO
DROP VIEW BAOCAONGAYTONG
GO
CREATE VIEW BAOCAONGAYTONG
AS
Select 
	ISNULL(MAX(BanHangID), 0) As ID,
	CAST(NgayBan AS DATE) as NgayBan,
	SUM(TienMat) as TienMat,
	SUM(TienThe) as TienThe,
	SUM(TienTraLai) as TienTraLai,
	SUM(GiamGia) as GiamGia,
	SUM(ChietKhau) as ChietKhau,
	SUM(TienBo) as TienBo,
	SUM(PhiDichVu) as PhiDichVu,
	SUM(TienKhacHang) as TienKhacHang,
	SUM(TongTien) as TongTien,
	COUNT_BIG(*) AS SoHoaDon
	from dbo.BANHANG GROUP BY CAST(NgayBan AS DATE)
GO
DROP VIEW BAOCAONGAYKHACHHANG
GO
CREATE VIEW BAOCAONGAYKHACHHANG
AS
Select 
	ISNULL(MAX(BanHangID), 0) As ID,
	CAST(NgayBan AS DATE) as NgayBan, 
	KH.TenKhachHang,
	SUM(TongTien) as TongTien
	from BANHANG BH Inner Join KHACHHANG KH ON KH.KhachHangID = BH.KhachHangID
	GROUP BY KH.TenKhachHang, CAST(NgayBan AS DATE)	
GO
DROP VIEW BAOCAONGAYTHE
GO
CREATE VIEW BAOCAONGAYTHE
AS
Select 
	ISNULL(MAX(BanHangID), 0) As ID,
	CAST(NgayBan AS DATE) as NgayBan, 
	T.TenThe,
	SUM(TongTien) as TongTien
	from BANHANG BH Inner Join THE T ON T.TheID = BH.TheID
	GROUP BY T.TenThe, CAST(NgayBan AS DATE)	
GO
DROP VIEW BAOCAONGAYMON
GO
CREATE VIEW BAOCAONGAYMON
AS
SELECT 
	ISNULL(MAX(BH.BanHangID), 0) As ID,
	CAST(NgayBan AS DATE) as NgayBan, M.NhomID,
	CASE
		WHEN KTM.TenLoaiBan = '' THEN M.TenDai
		ELSE M.TenDai + ' ('+KTM.TenLoaiBan+')'
	END AS Ten,
	KTM.KichThuocMonID,
	SUM(CTBH.SoLuongBan) As SoLuongBan, SUM(ThanhTien) AS ThanhTien
	FROM BANHANG BH Inner Join CHITIETBANHANG CTBH ON BH.BanHangID = CTBH.BanHangID
	Inner Join MENUKICHTHUOCMON KTM ON CTBH.KichThuocMonID = KTM.KichThuocMonID
	Inner Join MENUMON M ON KTM.MonID = M.MonID
	GROUP BY CAST(NgayBan AS DATE), M.TenDai, KTM.TenLoaiBan, M.NhomID, KTM.KichThuocMonID	
GO
DROP VIEW BAOCAONGAYNHOM
GO
CREATE VIEW BAOCAONGAYNHOM
AS
SELECT 
	ISNULL(MAX(BCNM.ID), 0) As ID,
	CAST(NgayBan AS DATE) as NgayBan, 
	N.TenDai,  N.NhomID,
	SUM(BCNM.SoLuongBan) As SoLuongBan, 
	SUM(BCNM.ThanhTien) AS ThanhTien
	From BAOCAONGAYMON BCNM Inner Join MENUNHOM N ON N.NhomID = BCNM.NhomID
	GROUP BY CAST(NgayBan AS DATE), N.TenDai, N.NhomID

GO

UPDATE BANHANG SET TheID = 1 Where BanHangID % 5 = 0
UPDATE BANHANG SET TheID = 2 Where BanHangID % 5 = 1
UPDATE BANHANG SET TheID = 3 Where BanHangID % 5 = 2
UPDATE BANHANG SET TheID = 4 Where BanHangID % 5 = 3
UPDATE BANHANG SET TheID = 5 Where BanHangID % 5 = 4

UPDATE BANHANG SET KhachHangID  = 1 Where BanHangID % 9 = 1
UPDATE BANHANG SET KhachHangID  = 2 Where BanHangID % 9 = 2
UPDATE BANHANG SET KhachHangID  = 3 Where BanHangID % 9 = 3
UPDATE BANHANG SET KhachHangID  = 4 Where BanHangID % 9 = 4
UPDATE BANHANG SET KhachHangID  = 5 Where BanHangID % 9 = 5
UPDATE BANHANG SET KhachHangID  = 6 Where BanHangID % 9 = 6
UPDATE BANHANG SET KhachHangID  = 7 Where BanHangID % 9 = 7
UPDATE BANHANG SET KhachHangID  = 8 Where BanHangID % 9 = 8
UPDATE BANHANG SET KhachHangID  = 9 Where BanHangID % 9 = 0