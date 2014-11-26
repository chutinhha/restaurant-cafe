USE [Karaoke]
GO
/****** Object:  Table [dbo].[CAIDATMAYINHOADON]    Script Date: 11/25/2014 03:21:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINHOADON]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CAIDATMAYINHOADON](
	[HeaderTextString1] [nvarchar](50) NULL,
	[HeaderTextString2] [nvarchar](50) NULL,
	[HeaderTextString3] [nvarchar](50) NULL,
	[HeaderTextString4] [nvarchar](50) NULL,
	[HeaderTextFontSize1] [float] NOT NULL,
	[HeaderTextFontSize2] [float] NOT NULL,
	[HeaderTextFontSize3] [float] NOT NULL,
	[HeaderTextFontSize4] [float] NOT NULL,
	[HeaderTextFontStyle1] [int] NOT NULL,
	[HeaderTextFontStyle2] [int] NOT NULL,
	[HeaderTextFontStyle3] [int] NOT NULL,
	[HeaderTextFontStyle4] [int] NOT NULL,
	[TitleTextFontSize] [float] NOT NULL,
	[TitleTextFontStyle] [int] NOT NULL,
	[InfoTextFontSize] [float] NOT NULL,
	[InfoTextFontStyle] [int] NOT NULL,
	[ItemFontSize] [float] NOT NULL,
	[SumanyFontSize] [float] NOT NULL,
	[SumanyFontStyle] [int] NOT NULL,
	[SumanyFontSizeBig] [float] NOT NULL,
	[SumanyFontStyleBig] [int] NOT NULL,
	[FooterTextFontSize1] [float] NOT NULL,
	[FooterTextFontSize2] [float] NOT NULL,
	[FooterTextFontSize3] [float] NOT NULL,
	[FooterTextFontSize4] [float] NOT NULL,
	[FooterTextFontStyle1] [int] NOT NULL,
	[FooterTextFontStyle2] [int] NOT NULL,
	[FooterTextFontStyle3] [int] NOT NULL,
	[FooterTextFontStyle4] [int] NOT NULL,
	[FooterTextString1] [nvarchar](50) NULL,
	[FooterTextString2] [nvarchar](50) NULL,
	[FooterTextString3] [nvarchar](50) NULL,
	[FooterTextString4] [nvarchar](50) NULL
) ON [PRIMARY]
END
GO
INSERT [dbo].[CAIDATMAYINHOADON] ([HeaderTextString1], [HeaderTextString2], [HeaderTextString3], [HeaderTextString4], [HeaderTextFontSize1], [HeaderTextFontSize2], [HeaderTextFontSize3], [HeaderTextFontSize4], [HeaderTextFontStyle1], [HeaderTextFontStyle2], [HeaderTextFontStyle3], [HeaderTextFontStyle4], [TitleTextFontSize], [TitleTextFontStyle], [InfoTextFontSize], [InfoTextFontStyle], [ItemFontSize], [SumanyFontSize], [SumanyFontStyle], [SumanyFontSizeBig], [SumanyFontStyleBig], [FooterTextFontSize1], [FooterTextFontSize2], [FooterTextFontSize3], [FooterTextFontSize4], [FooterTextFontStyle1], [FooterTextFontStyle2], [FooterTextFontStyle3], [FooterTextFontStyle4], [FooterTextString1], [FooterTextString2], [FooterTextString3], [FooterTextString4]) VALUES (N'D? Khúc Cafe', N'69 Ðý?ng 59 , P.14 , Qu?n G? V?p', N'ÐT : 0984942390', NULL, 14, 12, 12, 12, 1, 1, 1, 0, 12, 1, 12, 0, 12, 12, 1, 14, 1, 12, 12, 12, 12, 2, 0, 0, 0, N'Xin cám õn - H?n g?p l?i', NULL, NULL, NULL)
/****** Object:  Table [dbo].[CAIDATMAYINBEP]    Script Date: 11/25/2014 03:21:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINBEP]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CAIDATMAYINBEP](
	[TitleTextFontSize] [float] NOT NULL,
	[TitleTextFontStyle] [int] NOT NULL,
	[InfoTextFontSize] [float] NOT NULL,
	[InfoTextFontStyle] [int] NOT NULL,
	[ItemTextFontSize] [float] NOT NULL,
	[ItemTextFontStyle] [int] NOT NULL,
	[SumTextFontSize] [float] NOT NULL,
	[SumTextFontStyle] [int] NOT NULL
) ON [PRIMARY]
END
GO
INSERT [dbo].[CAIDATMAYINBEP] ([TitleTextFontSize], [TitleTextFontStyle], [InfoTextFontSize], [InfoTextFontStyle], [ItemTextFontSize], [ItemTextFontStyle], [SumTextFontSize], [SumTextFontStyle]) VALUES (12, 1, 12, 1, 12, 1, 12, 1)
/****** Object:  Default [DF_CAIDATMAYINHOADON_HeaderTextFontSize1]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINHOADON_HeaderTextFontSize1]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINHOADON]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINHOADON_HeaderTextFontSize1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINHOADON] ADD  CONSTRAINT [DF_CAIDATMAYINHOADON_HeaderTextFontSize1]  DEFAULT ((12)) FOR [HeaderTextFontSize1]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINHOADON_HeaderTextFontSize2]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINHOADON_HeaderTextFontSize2]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINHOADON]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINHOADON_HeaderTextFontSize2]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINHOADON] ADD  CONSTRAINT [DF_CAIDATMAYINHOADON_HeaderTextFontSize2]  DEFAULT ((12)) FOR [HeaderTextFontSize2]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINHOADON_HeaderTextFontSize3]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINHOADON_HeaderTextFontSize3]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINHOADON]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINHOADON_HeaderTextFontSize3]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINHOADON] ADD  CONSTRAINT [DF_CAIDATMAYINHOADON_HeaderTextFontSize3]  DEFAULT ((12)) FOR [HeaderTextFontSize3]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINHOADON_HeaderTextFontSize4]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINHOADON_HeaderTextFontSize4]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINHOADON]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINHOADON_HeaderTextFontSize4]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINHOADON] ADD  CONSTRAINT [DF_CAIDATMAYINHOADON_HeaderTextFontSize4]  DEFAULT ((12)) FOR [HeaderTextFontSize4]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINHOADON_HeaderTextFontStyle1]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINHOADON_HeaderTextFontStyle1]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINHOADON]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINHOADON_HeaderTextFontStyle1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINHOADON] ADD  CONSTRAINT [DF_CAIDATMAYINHOADON_HeaderTextFontStyle1]  DEFAULT ((0)) FOR [HeaderTextFontStyle1]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINHOADON_HeaderTextFontStyle2]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINHOADON_HeaderTextFontStyle2]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINHOADON]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINHOADON_HeaderTextFontStyle2]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINHOADON] ADD  CONSTRAINT [DF_CAIDATMAYINHOADON_HeaderTextFontStyle2]  DEFAULT ((0)) FOR [HeaderTextFontStyle2]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINHOADON_HeaderTextFontStyle3]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINHOADON_HeaderTextFontStyle3]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINHOADON]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINHOADON_HeaderTextFontStyle3]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINHOADON] ADD  CONSTRAINT [DF_CAIDATMAYINHOADON_HeaderTextFontStyle3]  DEFAULT ((0)) FOR [HeaderTextFontStyle3]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINHOADON_HeaderTextFontStyle4]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINHOADON_HeaderTextFontStyle4]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINHOADON]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINHOADON_HeaderTextFontStyle4]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINHOADON] ADD  CONSTRAINT [DF_CAIDATMAYINHOADON_HeaderTextFontStyle4]  DEFAULT ((0)) FOR [HeaderTextFontStyle4]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINHOADON_TitleTextFontSize]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINHOADON_TitleTextFontSize]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINHOADON]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINHOADON_TitleTextFontSize]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINHOADON] ADD  CONSTRAINT [DF_CAIDATMAYINHOADON_TitleTextFontSize]  DEFAULT ((12)) FOR [TitleTextFontSize]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINHOADON_TitleTextFontStyle]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINHOADON_TitleTextFontStyle]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINHOADON]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINHOADON_TitleTextFontStyle]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINHOADON] ADD  CONSTRAINT [DF_CAIDATMAYINHOADON_TitleTextFontStyle]  DEFAULT ((0)) FOR [TitleTextFontStyle]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINHOADON_InfoTextFontSize]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINHOADON_InfoTextFontSize]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINHOADON]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINHOADON_InfoTextFontSize]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINHOADON] ADD  CONSTRAINT [DF_CAIDATMAYINHOADON_InfoTextFontSize]  DEFAULT ((12)) FOR [InfoTextFontSize]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINHOADON_InfoTextFontStyle]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINHOADON_InfoTextFontStyle]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINHOADON]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINHOADON_InfoTextFontStyle]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINHOADON] ADD  CONSTRAINT [DF_CAIDATMAYINHOADON_InfoTextFontStyle]  DEFAULT ((0)) FOR [InfoTextFontStyle]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINHOADON_ItemFontSize]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINHOADON_ItemFontSize]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINHOADON]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINHOADON_ItemFontSize]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINHOADON] ADD  CONSTRAINT [DF_CAIDATMAYINHOADON_ItemFontSize]  DEFAULT ((12)) FOR [ItemFontSize]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINHOADON_SumanyFontSize]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINHOADON_SumanyFontSize]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINHOADON]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINHOADON_SumanyFontSize]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINHOADON] ADD  CONSTRAINT [DF_CAIDATMAYINHOADON_SumanyFontSize]  DEFAULT ((12)) FOR [SumanyFontSize]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINHOADON_SumanyFontStyle]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINHOADON_SumanyFontStyle]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINHOADON]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINHOADON_SumanyFontStyle]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINHOADON] ADD  CONSTRAINT [DF_CAIDATMAYINHOADON_SumanyFontStyle]  DEFAULT ((0)) FOR [SumanyFontStyle]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINHOADON_SumanyFontSizeBig]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINHOADON_SumanyFontSizeBig]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINHOADON]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINHOADON_SumanyFontSizeBig]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINHOADON] ADD  CONSTRAINT [DF_CAIDATMAYINHOADON_SumanyFontSizeBig]  DEFAULT ((12)) FOR [SumanyFontSizeBig]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINHOADON_SumanyFontStyleBig]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINHOADON_SumanyFontStyleBig]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINHOADON]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINHOADON_SumanyFontStyleBig]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINHOADON] ADD  CONSTRAINT [DF_CAIDATMAYINHOADON_SumanyFontStyleBig]  DEFAULT ((0)) FOR [SumanyFontStyleBig]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINHOADON_FooterTextFontSize1]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINHOADON_FooterTextFontSize1]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINHOADON]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINHOADON_FooterTextFontSize1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINHOADON] ADD  CONSTRAINT [DF_CAIDATMAYINHOADON_FooterTextFontSize1]  DEFAULT ((12)) FOR [FooterTextFontSize1]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINHOADON_FooterTextFontSize2]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINHOADON_FooterTextFontSize2]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINHOADON]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINHOADON_FooterTextFontSize2]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINHOADON] ADD  CONSTRAINT [DF_CAIDATMAYINHOADON_FooterTextFontSize2]  DEFAULT ((12)) FOR [FooterTextFontSize2]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINHOADON_FooterTextFontSize3]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINHOADON_FooterTextFontSize3]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINHOADON]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINHOADON_FooterTextFontSize3]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINHOADON] ADD  CONSTRAINT [DF_CAIDATMAYINHOADON_FooterTextFontSize3]  DEFAULT ((12)) FOR [FooterTextFontSize3]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINHOADON_FooterTextFontSize4]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINHOADON_FooterTextFontSize4]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINHOADON]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINHOADON_FooterTextFontSize4]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINHOADON] ADD  CONSTRAINT [DF_CAIDATMAYINHOADON_FooterTextFontSize4]  DEFAULT ((12)) FOR [FooterTextFontSize4]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINHOADON_FooterTextFontStyle1]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINHOADON_FooterTextFontStyle1]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINHOADON]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINHOADON_FooterTextFontStyle1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINHOADON] ADD  CONSTRAINT [DF_CAIDATMAYINHOADON_FooterTextFontStyle1]  DEFAULT ((0)) FOR [FooterTextFontStyle1]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINHOADON_FooterTextFontStyle2]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINHOADON_FooterTextFontStyle2]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINHOADON]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINHOADON_FooterTextFontStyle2]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINHOADON] ADD  CONSTRAINT [DF_CAIDATMAYINHOADON_FooterTextFontStyle2]  DEFAULT ((0)) FOR [FooterTextFontStyle2]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINHOADON_FooterTextFontStyle3]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINHOADON_FooterTextFontStyle3]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINHOADON]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINHOADON_FooterTextFontStyle3]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINHOADON] ADD  CONSTRAINT [DF_CAIDATMAYINHOADON_FooterTextFontStyle3]  DEFAULT ((0)) FOR [FooterTextFontStyle3]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINHOADON_FooterTextFontStyle4]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINHOADON_FooterTextFontStyle4]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINHOADON]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINHOADON_FooterTextFontStyle4]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINHOADON] ADD  CONSTRAINT [DF_CAIDATMAYINHOADON_FooterTextFontStyle4]  DEFAULT ((0)) FOR [FooterTextFontStyle4]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINBEP_TitleTextFontSize]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINBEP_TitleTextFontSize]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINBEP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINBEP_TitleTextFontSize]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINBEP] ADD  CONSTRAINT [DF_CAIDATMAYINBEP_TitleTextFontSize]  DEFAULT ((12)) FOR [TitleTextFontSize]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINBEP_TitleTextFontStyle]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINBEP_TitleTextFontStyle]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINBEP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINBEP_TitleTextFontStyle]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINBEP] ADD  CONSTRAINT [DF_CAIDATMAYINBEP_TitleTextFontStyle]  DEFAULT ((0)) FOR [TitleTextFontStyle]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINBEP_InfoTextFontSize]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINBEP_InfoTextFontSize]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINBEP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINBEP_InfoTextFontSize]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINBEP] ADD  CONSTRAINT [DF_CAIDATMAYINBEP_InfoTextFontSize]  DEFAULT ((12)) FOR [InfoTextFontSize]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINBEP_InfoTextFontStyle]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINBEP_InfoTextFontStyle]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINBEP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINBEP_InfoTextFontStyle]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINBEP] ADD  CONSTRAINT [DF_CAIDATMAYINBEP_InfoTextFontStyle]  DEFAULT ((0)) FOR [InfoTextFontStyle]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINBEP_ItemTextFontSize]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINBEP_ItemTextFontSize]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINBEP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINBEP_ItemTextFontSize]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINBEP] ADD  CONSTRAINT [DF_CAIDATMAYINBEP_ItemTextFontSize]  DEFAULT ((12)) FOR [ItemTextFontSize]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINBEP_ItemTextFontStyle]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINBEP_ItemTextFontStyle]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINBEP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINBEP_ItemTextFontStyle]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINBEP] ADD  CONSTRAINT [DF_CAIDATMAYINBEP_ItemTextFontStyle]  DEFAULT ((0)) FOR [ItemTextFontStyle]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINBEP_SumTextFontSize]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINBEP_SumTextFontSize]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINBEP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINBEP_SumTextFontSize]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINBEP] ADD  CONSTRAINT [DF_CAIDATMAYINBEP_SumTextFontSize]  DEFAULT ((12)) FOR [SumTextFontSize]
END


End
GO
/****** Object:  Default [DF_CAIDATMAYINBEP_SumTextFontStyle]    Script Date: 11/25/2014 03:21:02 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_CAIDATMAYINBEP_SumTextFontStyle]') AND parent_object_id = OBJECT_ID(N'[dbo].[CAIDATMAYINBEP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CAIDATMAYINBEP_SumTextFontStyle]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CAIDATMAYINBEP] ADD  CONSTRAINT [DF_CAIDATMAYINBEP_SumTextFontStyle]  DEFAULT ((0)) FOR [SumTextFontStyle]
END


End
GO
