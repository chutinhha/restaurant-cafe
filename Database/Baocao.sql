Select * from MENUNHOM
Select * from Menumon

Update MENUMON Set Deleted = 0 Where MonID = 9;
Select * from DONVI

Select * from MENUKICHTHUOCMON

Select * from LICHSUBANHANG
Select * from BANHANG
Select * from NHACUNGCAP
Update NHACUNGCAP set Edit = 0;


Delete From KHACHHANG

Create View BaoCaoLichSuBanHang
As
Select NV.TenNhanVien, B.TenBan, BH.NgayBan, 
	from BANHANG BH
	Left join NHANVIEN NV On BH.NhanVienID = BH.NhanVienID
	Left join THE T On BH.TheID = T.TheID
	Left join KHACHHANG KH On BH.KhachHangID = Kh.KhachHangID
	Left join BAN B On BH.BanID = B.BanID	