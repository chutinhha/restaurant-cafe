DROP VIEW BAOCAODINHLUONG
GO
CREATE VIEW BAOCAODINHLUONG
AS
Select DL.ID As ID ,M.TenDai As TenMonChinh,
	CASE
		WHEN M1.DonViID = 1 THEN ISNULL(DL.KichThuocBan * DL.SoLuong, 0)
		WHEN M1.DonViID = 2 THEN CAST(ISNULL(DL.KichThuocBan * DL.SoLuong, 0) / 1000.000 AS DECIMAL(10,3)) 
		WHEN M1.DonViID = 3 THEN CAST(ISNULL(DL.KichThuocBan * DL.SoLuong, 0) / 1000.000 AS DECIMAL(10,3)) 
		WHEN M1.DonViID = 4 THEN CAST(ISNULL(DL.KichThuocBan * DL.SoLuong, 0) / 3600.000 AS DECIMAL(10,3)) 			
		ELSE CAST(ISNULL(DL.KichThuocBan * DL.SoLuong, 0) AS DECIMAL(10,0)) 
	END AS DinhLuong,
	CASE
		WHEN M1.DonViID = 1 THEN  M1.TenDai + ' (Cái, Lon, ...)' 
		WHEN M1.DonViID = 2 THEN  M1.TenDai + ' (Kg)' 
		WHEN M1.DonViID = 3 THEN  M1.TenDai + ' (Lít)' 
		WHEN M1.DonViID = 4 THEN  M1.TenDai + ' (giờ)'
		ELSE M1.TenDai
	END As TenMonPhu
	from DINHLUONG DL
	Inner join MENUKICHTHUOCMON KTM On DL.KichThuocMonChinhID = KTM.KichThuocMonID
	Inner join MENUMON M ON M.MonID = KTM.MonID
	Inner join MENUMON M1 ON DL.MonID = M1.MonID


	