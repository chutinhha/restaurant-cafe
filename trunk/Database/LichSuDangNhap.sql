DROP VIEW [BAOCAOLICHDANGNHAP]
GO
CREATE VIEW [BAOCAOLICHDANGNHAP]
As
SELECT        l.ID, l.ThoiGian, nv.TenNhanVien, lnv.TenLoaiNhanVien
FROM            dbo.LICHSUDANGNHAP AS l INNER JOIN
                         dbo.NHANVIEN AS nv ON l.NhanVienID = nv.NhanVienID INNER JOIN
                         dbo.LOAINHANVIEN AS lnv ON lnv.LoaiNhanVienID = nv.LoaiNhanVienID