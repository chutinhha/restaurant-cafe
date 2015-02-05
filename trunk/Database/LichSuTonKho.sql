CREATE TABLE [dbo].[LICHSUTONKHO](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NgayGhiNhan] [datetime] NOT NULL,
	[KhoID] [int] NULL,
	[MonID] [int] NULL,
	[DonViID] [int] NULL,
	[DauKySoLuong] [int] NULL,
	[DauKyThanhTien] [decimal](18, 2) NULL,
	[NhapSoLuong] [int] NOT NULL,
	[NhapThanhTien] [decimal](18, 2) NOT NULL,
	[XuatSoLuong] [int] NOT NULL,
	[XuatThanhTien] [decimal](18, 2) NOT NULL,
	[CuoiKySoLuong] [int] NOT NULL,
	[CuoiKyDonGia] [decimal](18, 2) NOT NULL,
	[CuoiKyThanhTien] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_LICHSUTONKHO] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[BAOCAOLICHSUTONKHO]    Script Date: 2/5/2015 5:34:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[BAOCAOLICHSUTONKHO]
AS
SELECT  CASE WHEN M.DonViID = 1 THEN 'Cái' WHEN M.DonViID = 2 THEN 'Kg' WHEN M.DonViID = 3 THEN 'Lít' WHEN M.DonViID = 4 THEN 'Giờ' END AS DonViTinh, 
	M.TenDai AS TenBaoCao, T.*                    
FROM dbo.MENUMON AS M RIGHT OUTER JOIN
	 dbo.LICHSUTONKHO AS T ON M.MonID = T.MonID
GO
ALTER TABLE [dbo].[LICHSUTONKHO] ADD  CONSTRAINT [DF_LICHSUTONKHO_NgayGhiNhan]  DEFAULT (getdate()) FOR [NgayGhiNhan]
GO
ALTER TABLE [dbo].[LICHSUTONKHO] ADD  CONSTRAINT [DF_LICHSUTONKHO_DauKySoLuong]  DEFAULT ((0)) FOR [DauKySoLuong]
GO
ALTER TABLE [dbo].[LICHSUTONKHO] ADD  CONSTRAINT [DF_LICHSUTONKHO_DauKyThanhTien]  DEFAULT ((0)) FOR [DauKyThanhTien]
GO
ALTER TABLE [dbo].[LICHSUTONKHO] ADD  CONSTRAINT [DF_LICHSUTONKHO_NhapSoLuong]  DEFAULT ((0)) FOR [NhapSoLuong]
GO
ALTER TABLE [dbo].[LICHSUTONKHO] ADD  CONSTRAINT [DF_LICHSUTONKHO_NhapThanhTien]  DEFAULT ((0)) FOR [NhapThanhTien]
GO
ALTER TABLE [dbo].[LICHSUTONKHO] ADD  CONSTRAINT [DF_LICHSUTONKHO_XuatSoLuong]  DEFAULT ((0)) FOR [XuatSoLuong]
GO
ALTER TABLE [dbo].[LICHSUTONKHO] ADD  CONSTRAINT [DF_LICHSUTONKHO_XuatThanhTien]  DEFAULT ((0)) FOR [XuatThanhTien]
GO
ALTER TABLE [dbo].[LICHSUTONKHO] ADD  CONSTRAINT [DF_LICHSUTONKHO_CuoiKySoLuong]  DEFAULT ((0)) FOR [CuoiKySoLuong]
GO
ALTER TABLE [dbo].[LICHSUTONKHO] ADD  CONSTRAINT [DF_LICHSUTONKHO_CuoiKyDonGia]  DEFAULT ((0)) FOR [CuoiKyDonGia]
GO
ALTER TABLE [dbo].[LICHSUTONKHO] ADD  CONSTRAINT [DF_LICHSUTONKHO_CuoiKyThanhTien]  DEFAULT ((0)) FOR [CuoiKyThanhTien]
GO

Alter PROC SP_BAOCAOLICHSUTONKHO(@KhoID int, @DateFrom datetime, @DateTo datetime)
AS
BEGIN
	Select M.MonID,
	CASE WHEN M.DonViID = 1 THEN 'Cái' WHEN M.DonViID = 2 THEN 'Kg' WHEN M.DonViID = 3 THEN 'Lít' WHEN M.DonViID = 4 THEN 'Giờ' END AS DonViTinh, 
	M.TenDai AS TenBaoCao,
	IIF(ISNULL(MIN(BC.ID),0)>0,(Select DauKySoLuong from BAOCAOLICHSUTONKHO Where ID = MIN(BC.ID)),0) As DauKySoLuong,
	IIF(ISNULL(MIN(BC.ID),0)>0,(Select DauKyThanhTien from BAOCAOLICHSUTONKHO Where ID = MIN(BC.ID)),0) As DauKyThanhTien,	
	ISNULL(SUM(BC.NhapSoLuong),0) As NhapSoLuong,
	ISNULL(SUM(BC.NhapThanhTien),0) As NhapThanhTien,
	ISNULL(SUM(BC.XuatSoLuong),0) As XuatSoLuong,
	ISNULL(SUM(BC.XuatThanhTien),0) As XuatThanhTien,
	IIF(ISNULL(MAX(BC.ID),0)>0,(Select CuoiKySoLuong from BAOCAOLICHSUTONKHO Where ID = MAX(BC.ID)),0) As CuoiKySoLuong,
	IIF(ISNULL(MAX(BC.ID),0)>0,(Select CuoiKyDonGia from BAOCAOLICHSUTONKHO Where ID = MAX(BC.ID)),0) As CuoiKyDonGia,
	IIF(ISNULL(MAX(BC.ID),0)>0,(Select CuoiKyThanhTien from BAOCAOLICHSUTONKHO Where ID = MAX(BC.ID)),0) As CuoiKyThanhTien
	from MENUMON AS M
	LEFT OUTER JOIN BAOCAOLICHSUTONKHO AS BC ON BC.MonID = M.MonID
	Where Deleted = 0 And BC.KhoID = @KhoID And CAST(BC.NgayGhiNhan As DATE) >= @DateFrom And CAST(BC.NgayGhiNhan As DATE) <= @DateTo
	GROUP BY M.MonID, M.DonViID, M.TenDai
END

Alter TRIGGER TR_INSERT_LICHSUTONKHO
ON LICHSUTONKHO AFTER INSERT
As
BEGIN
	Declare @ID int
	Declare @MonID int
	Declare @KhoID int
	SELECT Top 1 @ID = ID, @MonID = MonID, @KhoID = KhoID FROM INSERTED
	Declare @DauKySoLuong int
	Declare @DauKyThanhTien Decimal(18,2)
	Declare @Gia Decimal(18,2)
	SELECT Top 1 @Gia = Gia From MENUMON Where MonID = @MonID
	SET @DauKySoLuong = 0 
	Set @DauKyThanhTien = 0
	IF EXISTS(SELECT 1 From LICHSUTONKHO Where ID < @ID And MonID = @MonID And KhoID =@KhoID)
		SELECT Top 1 @DauKySoLuong = ISNULL(CuoiKySoLuong,0), @DauKyThanhTien = ISNULL(CuoiKyThanhTien,0) From LICHSUTONKHO Where ID < @ID And MonID = @MonID And KhoID =@KhoID Order By ID DESC	
	Update LICHSUTONKHO Set DauKySoLuong = @DauKySoLuong, DauKyThanhTien = @DauKyThanhTien, CuoiKySoLuong = @DauKySoLuong + NhapSoLuong - XuatSoLuong, CuoiKyDonGia = @Gia, CuoiKyThanhTien = @Gia * (@DauKySoLuong + NhapSoLuong - XuatSoLuong) Where ID = @ID And MonID = @MonID And KhoID =@KhoID
END