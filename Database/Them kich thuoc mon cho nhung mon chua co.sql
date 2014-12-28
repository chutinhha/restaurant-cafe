DECLARE @monID int, @donViID int, @gia decimal(18, 0);
DECLARE vendor_cursor CURSOR FOR 
SELECT M.MonID, M.DonViID, M.Gia FROM MENUMON M Where M.MonID Not in (Select KTM.MonID From MENUKICHTHUOCMON KTM)
OPEN vendor_cursor
FETCH NEXT FROM vendor_cursor INTO @monID, @donViID, @gia
WHILE @@FETCH_STATUS = 0
BEGIN
    IF @donViID = 1	
		INSERT INTO	MENUKICHTHUOCMON(MonID, TenLoaiBan, LoaiBanID, DonViID, ChoPhepTonKho, GiaBanMacDinh, ThoiGia, KichThuocLoaiBan, SoLuongBanBan, Visual, Deleted, Edit, SapXep) Values
		(@monID, N'', 1, @donViID, 1, @gia,0,1,1,1,0,0,1);
	IF @donViID = 2
		INSERT INTO	MENUKICHTHUOCMON(MonID, TenLoaiBan, LoaiBanID, DonViID, ChoPhepTonKho, GiaBanMacDinh, ThoiGia, KichThuocLoaiBan, SoLuongBanBan, Visual, Deleted, Edit, SapXep) Values
		(@monID, N'', 3, @donViID, 1, @gia,0,1000,1,1,0,0,1);
		IF @donViID = 3
		INSERT INTO	MENUKICHTHUOCMON(MonID, TenLoaiBan, LoaiBanID, DonViID, ChoPhepTonKho, GiaBanMacDinh, ThoiGia, KichThuocLoaiBan, SoLuongBanBan, Visual, Deleted, Edit, SapXep) Values
		(@monID, N'', 5, @donViID, 1, @gia,0,1000,1,1,0,0,1);
    FETCH NEXT FROM vendor_cursor INTO @monID, @donViID, @gia
END 
CLOSE vendor_cursor;
DEALLOCATE vendor_cursor;
