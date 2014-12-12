DROP VIEW BAOCAODINHLUONG
GO
CREATE VIEW BAOCAODINHLUONG
AS	
	SELECT  
	CAST(NgayBan AS DATE) As NgayBan,
	ISNULL(MAX(DL.MonID), 0) As ID,	
	M.TenDai As TenMon,
	CASE
		WHEN M.DonViID = 1 THEN  'Cái, Lon, ...'
		WHEN M.DonViID = 2 THEN  'Kg' 
		WHEN M.DonViID = 3 THEN  'Lít'
		WHEN M.DonViID = 4 THEN  'Giờ'
	END As DonViTinh,
	CASE
		WHEN M.DonViID = 1 THEN ISNULL(SUM(DL.SoLuong * DL.KichThuocBan), 0)
		WHEN M.DonViID = 2 THEN CAST(ISNULL(SUM(DL.SoLuong * DL.KichThuocBan), 0) / 1000.000 AS DECIMAL(10,3)) 
		WHEN M.DonViID = 3 THEN CAST(ISNULL(SUM(DL.SoLuong * DL.KichThuocBan), 0) / 1000.000 AS DECIMAL(10,3)) 
		WHEN M.DonViID = 4 THEN CAST(ISNULL(SUM(DL.SoLuong * DL.KichThuocBan), 0) / 3600.000 AS DECIMAL(10,3)) 					
		ELSE CAST(ISNULL(SUM(DL.SoLuong * DL.KichThuocBan), 0) AS DECIMAL(10,0)) 
	END
	AS SoLuong
	FROM BANHANG BH Inner Join CHITIETBANHANG CTBH ON BH.BanHangID = CTBH.BanHangID
	Inner Join MENUKICHTHUOCMON KTM ON CTBH.KichThuocMonID = KTM.KichThuocMonID	
	Inner join DINHLUONG DL ON DL.KichThuocMonChinhID = CTBH.KichThuocMonID
	Inner Join MENUMON M ON DL.MonID = M.MonID		
	Where KTM.ChoPhepTonKho = 0
	GROUP BY CAST(NgayBan AS DATE), DL.MonID, M.TenDai, M.DonViID


	