GO
DROP VIEW BAOCAOTHUCHI
GO
CREATE VIEW BAOCAOTHUCHI
AS
SELECT ID, TenNhanVien, CAST(ThoiGian AS DATE) as ThoiGian, ThoiGian As ThoiGianFull , TenLoaiThuChi, TongTien, GhiChu, Thu, Chi FROM
(
	SELECT ID, TenNhanVien, ThoiGian, TenLoaiThuChi, TongTien, GhiChu,
		CASE
			WHEN L.LoaiThuChiID = 1 THEN TongTien
			ELSE 0
		END AS Thu,
		CASE
			WHEN L.LoaiThuChiID = 2 THEN TongTien
			ELSE 0
		END AS Chi
		FROM THUCHI T
		INNER JOIN LOAITHUCHI L ON  L.LoaiThuChiID = T.LoaiThuChiID
		INNER JOIN NHANVIEN N ON N.NhanVienID = T.NhanVienID
	UNION
	SELECT 
		B.BanHangID As ID,
		N.TenNhanVien,
		NgayBan AS  ThoiGian,
		N'Phiếu thu' as TenLoaiThuChi,
		B.TongTien,
		N'Bán hàng' As GhiChu,
		B.TongTien AS Thu,
		0 As Chi
		FROM BANHANG B
		INNER JOIN NHANVIEN N ON N.NhanVienID = B.NhanVienID
) AS TC

