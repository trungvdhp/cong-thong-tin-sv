/****** Object:  ForeignKey [FK_DangKy_Nhom]    Script Date: 11/08/2012 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DangKy_Nhom]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_DanhSachLopTinChi]'))
ALTER TABLE [dbo].[MOD_DanhSachLopTinChi] DROP CONSTRAINT [FK_DangKy_Nhom]
GO
/****** Object:  ForeignKey [FK_MOD_HocKy_ChuyenNganh_MOD_HocKy]    Script Date: 11/08/2012 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_HocKy_ChuyenNganh_MOD_HocKy]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_HocKy_ChuyenNganh]'))
ALTER TABLE [dbo].[MOD_HocKy_ChuyenNganh] DROP CONSTRAINT [FK_MOD_HocKy_ChuyenNganh_MOD_HocKy]
GO
/****** Object:  ForeignKey [FK_MOD_NguoiDung_MOD_NhomNguoiDung]    Script Date: 11/08/2012 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NguoiDung_MOD_NhomNguoiDung]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung]'))
ALTER TABLE [dbo].[MOD_NguoiDung] DROP CONSTRAINT [FK_MOD_NguoiDung_MOD_NhomNguoiDung]
GO
/****** Object:  ForeignKey [FK_MOD_NguoiDung_VaiTro_HeThong_MOD_NguoiDung]    Script Date: 11/08/2012 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NguoiDung_VaiTro_HeThong_MOD_NguoiDung]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_HeThong]'))
ALTER TABLE [dbo].[MOD_NguoiDung_VaiTro_HeThong] DROP CONSTRAINT [FK_MOD_NguoiDung_VaiTro_HeThong_MOD_NguoiDung]
GO
/****** Object:  ForeignKey [FK_MOD_NguoiDung_VaiTro_HeThong_MOD_VaiTro]    Script Date: 11/08/2012 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NguoiDung_VaiTro_HeThong_MOD_VaiTro]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_HeThong]'))
ALTER TABLE [dbo].[MOD_NguoiDung_VaiTro_HeThong] DROP CONSTRAINT [FK_MOD_NguoiDung_VaiTro_HeThong_MOD_VaiTro]
GO
/****** Object:  ForeignKey [FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_LopTinChi_TC]    Script Date: 11/08/2012 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_LopTinChi_TC]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_LopTinChi]'))
ALTER TABLE [dbo].[MOD_NguoiDung_VaiTro_LopTinChi] DROP CONSTRAINT [FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_LopTinChi_TC]
GO
/****** Object:  ForeignKey [FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_NguoiDung]    Script Date: 11/08/2012 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_NguoiDung]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_LopTinChi]'))
ALTER TABLE [dbo].[MOD_NguoiDung_VaiTro_LopTinChi] DROP CONSTRAINT [FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_NguoiDung]
GO
/****** Object:  ForeignKey [FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_VaiTro]    Script Date: 11/08/2012 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_VaiTro]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_LopTinChi]'))
ALTER TABLE [dbo].[MOD_NguoiDung_VaiTro_LopTinChi] DROP CONSTRAINT [FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_VaiTro]
GO
/****** Object:  ForeignKey [FK_MOD_NhomHocVien_MOD_LopTinChi_TC]    Script Date: 11/08/2012 22:50:14 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NhomHocVien_MOD_LopTinChi_TC]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NhomHocVien]'))
ALTER TABLE [dbo].[MOD_NhomHocVien] DROP CONSTRAINT [FK_MOD_NhomHocVien_MOD_LopTinChi_TC]
GO
/****** Object:  ForeignKey [FK_Nhom_To]    Script Date: 11/08/2012 22:50:14 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Nhom_To]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NhomHocVien]'))
ALTER TABLE [dbo].[MOD_NhomHocVien] DROP CONSTRAINT [FK_Nhom_To]
GO
/****** Object:  ForeignKey [FK_MOD_ToNhom_MOD_LopTinChi_TC]    Script Date: 11/08/2012 22:50:14 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_ToNhom_MOD_LopTinChi_TC]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_ToNhom]'))
ALTER TABLE [dbo].[MOD_ToNhom] DROP CONSTRAINT [FK_MOD_ToNhom_MOD_LopTinChi_TC]
GO
/****** Object:  Table [dbo].[MOD_DanhSachLopTinChi]    Script Date: 11/08/2012 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_DanhSachLopTinChi]') AND type in (N'U'))
DROP TABLE [dbo].[MOD_DanhSachLopTinChi]
GO
/****** Object:  Table [dbo].[MOD_NguoiDung_VaiTro_HeThong]    Script Date: 11/08/2012 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_HeThong]') AND type in (N'U'))
DROP TABLE [dbo].[MOD_NguoiDung_VaiTro_HeThong]
GO
/****** Object:  Table [dbo].[MOD_NguoiDung_VaiTro_LopTinChi]    Script Date: 11/08/2012 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_LopTinChi]') AND type in (N'U'))
DROP TABLE [dbo].[MOD_NguoiDung_VaiTro_LopTinChi]
GO
/****** Object:  Table [dbo].[MOD_NhomHocVien]    Script Date: 11/08/2012 22:50:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_NhomHocVien]') AND type in (N'U'))
DROP TABLE [dbo].[MOD_NhomHocVien]
GO
/****** Object:  Table [dbo].[MOD_ToNhom]    Script Date: 11/08/2012 22:50:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_ToNhom]') AND type in (N'U'))
DROP TABLE [dbo].[MOD_ToNhom]
GO
/****** Object:  Table [dbo].[MOD_HocKy_ChuyenNganh]    Script Date: 11/08/2012 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_HocKy_ChuyenNganh]') AND type in (N'U'))
DROP TABLE [dbo].[MOD_HocKy_ChuyenNganh]
GO
/****** Object:  Table [dbo].[MOD_NguoiDung]    Script Date: 11/08/2012 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung]') AND type in (N'U'))
DROP TABLE [dbo].[MOD_NguoiDung]
GO
/****** Object:  Table [dbo].[MOD_LopTinChi_TC]    Script Date: 11/08/2012 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_LopTinChi_TC]') AND type in (N'U'))
DROP TABLE [dbo].[MOD_LopTinChi_TC]
GO
/****** Object:  Table [dbo].[MOD_DichVu]    Script Date: 11/08/2012 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_DichVu]') AND type in (N'U'))
DROP TABLE [dbo].[MOD_DichVu]
GO
/****** Object:  Table [dbo].[MOD_HocKy]    Script Date: 11/08/2012 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_HocKy]') AND type in (N'U'))
DROP TABLE [dbo].[MOD_HocKy]
GO
/****** Object:  Table [dbo].[MOD_VaiTro]    Script Date: 11/08/2012 22:50:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_VaiTro]') AND type in (N'U'))
DROP TABLE [dbo].[MOD_VaiTro]
GO
/****** Object:  Table [dbo].[MOD_NhomNguoiDung]    Script Date: 11/08/2012 22:50:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_NhomNguoiDung]') AND type in (N'U'))
DROP TABLE [dbo].[MOD_NhomNguoiDung]
GO
/****** Object:  Default [DF_MOD_ChuyenNganh_ID_moodle]    Script Date: 11/08/2012 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_MOD_ChuyenNganh_ID_moodle]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_HocKy]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MOD_ChuyenNganh_ID_moodle]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MOD_HocKy] DROP CONSTRAINT [DF_MOD_ChuyenNganh_ID_moodle]
END


End
GO
/****** Object:  Default [DF_ThoiKhoaBieu_Id]    Script Date: 11/08/2012 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ThoiKhoaBieu_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_LopTinChi_TC]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ThoiKhoaBieu_Id]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MOD_LopTinChi_TC] DROP CONSTRAINT [DF_ThoiKhoaBieu_Id]
END


End
GO
/****** Object:  Default [DF_TaiKhoan_Quyen_ID_quyen]    Script Date: 11/08/2012 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_TaiKhoan_Quyen_ID_quyen]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_HeThong]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TaiKhoan_Quyen_ID_quyen]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MOD_NguoiDung_VaiTro_HeThong] DROP CONSTRAINT [DF_TaiKhoan_Quyen_ID_quyen]
END


End
GO
/****** Object:  Default [DF_Quyen_He_thong]    Script Date: 11/08/2012 22:50:14 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Quyen_He_thong]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_VaiTro]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Quyen_He_thong]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MOD_VaiTro] DROP CONSTRAINT [DF_Quyen_He_thong]
END


End
GO
/****** Object:  Table [dbo].[MOD_NhomNguoiDung]    Script Date: 11/08/2012 22:50:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_NhomNguoiDung]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MOD_NhomNguoiDung](
	[ID_nhom_nd] [int] IDENTITY(1,1) NOT NULL,
	[Ten_nhom] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Mo_ta] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_MOD_NhomNguoiDung] PRIMARY KEY CLUSTERED 
(
	[ID_nhom_nd] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[MOD_NhomNguoiDung] ON
INSERT [dbo].[MOD_NhomNguoiDung] ([ID_nhom_nd], [Ten_nhom], [Mo_ta]) VALUES (1, N'Quản trị', N'Người dùng là quản trị')
INSERT [dbo].[MOD_NhomNguoiDung] ([ID_nhom_nd], [Ten_nhom], [Mo_ta]) VALUES (2, N'Giảng viên', N'Người dùng là giáo viên')
INSERT [dbo].[MOD_NhomNguoiDung] ([ID_nhom_nd], [Ten_nhom], [Mo_ta]) VALUES (3, N'Sinh viên', N'Người dùng là sinh viên')
SET IDENTITY_INSERT [dbo].[MOD_NhomNguoiDung] OFF
/****** Object:  Table [dbo].[MOD_VaiTro]    Script Date: 11/08/2012 22:50:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_VaiTro]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MOD_VaiTro](
	[ID_vai_tro] [int] NOT NULL,
	[Ten_vai_tro] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Mo_ta] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[He_thong] [bit] NOT NULL,
 CONSTRAINT [PK_Quyen] PRIMARY KEY CLUSTERED 
(
	[ID_vai_tro] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[MOD_HocKy]    Script Date: 11/08/2012 22:50:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_HocKy]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MOD_HocKy](
	[ID_moodle] [int] NOT NULL,
	[Ky_dang_ky] [int] NOT NULL,
 CONSTRAINT [PK_MOD_HocKy] PRIMARY KEY CLUSTERED 
(
	[ID_moodle] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[MOD_DichVu]    Script Date: 11/08/2012 22:50:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_DichVu]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MOD_DichVu](
	[ID_dv] [int] NOT NULL,
	[Ten_dv] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Ten_rut_gon] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_DichVu] PRIMARY KEY CLUSTERED 
(
	[ID_dv] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[MOD_DichVu] ([ID_dv], [Ten_dv], [Ten_rut_gon]) VALUES (1, N'Quản trị', N'admin')
INSERT [dbo].[MOD_DichVu] ([ID_dv], [Ten_dv], [Ten_rut_gon]) VALUES (2, N'Quản trị khóa học', N'course')
INSERT [dbo].[MOD_DichVu] ([ID_dv], [Ten_dv], [Ten_rut_gon]) VALUES (3, N'Người dùng', N'user')
/****** Object:  Table [dbo].[MOD_LopTinChi_TC]    Script Date: 11/08/2012 22:50:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_LopTinChi_TC]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MOD_LopTinChi_TC](
	[ID_moodle] [int] NOT NULL,
	[ID_lop_tc] [int] NOT NULL,
 CONSTRAINT [PK_MOD_LopTinChi_TC] PRIMARY KEY CLUSTERED 
(
	[ID_moodle] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[MOD_NguoiDung]    Script Date: 11/08/2012 22:50:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MOD_NguoiDung](
	[ID_moodle] [int] NOT NULL,
	[ID_nd] [int] NOT NULL,
	[ID_nhom_nd] [int] NOT NULL,
 CONSTRAINT [PK_MOD_NguoiDung] PRIMARY KEY CLUSTERED 
(
	[ID_moodle] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[MOD_HocKy_ChuyenNganh]    Script Date: 11/08/2012 22:50:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_HocKy_ChuyenNganh]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MOD_HocKy_ChuyenNganh](
	[ID_moodle] [bigint] NOT NULL,
	[Ky_dang_ky] [int] NOT NULL,
	[ID_chuyen_nganh] [int] NOT NULL,
 CONSTRAINT [PK_MOD_HocKy_ChuyenNganh] PRIMARY KEY CLUSTERED 
(
	[ID_moodle] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[MOD_ToNhom]    Script Date: 11/08/2012 22:50:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_ToNhom]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MOD_ToNhom](
	[ID_to] [int] NOT NULL,
	[Ten_to] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Mo_ta] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ID_lop_tc] [int] NOT NULL,
 CONSTRAINT [PK_To] PRIMARY KEY CLUSTERED 
(
	[ID_to] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[MOD_NhomHocVien]    Script Date: 11/08/2012 22:50:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_NhomHocVien]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MOD_NhomHocVien](
	[ID_nhom] [int] NOT NULL,
	[Ten_nhom] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Mo_ta] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ID_to] [int] NULL,
	[ID_lop_tc] [int] NOT NULL,
 CONSTRAINT [PK_Nhom] PRIMARY KEY CLUSTERED 
(
	[ID_nhom] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[MOD_NguoiDung_VaiTro_LopTinChi]    Script Date: 11/08/2012 22:50:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_LopTinChi]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MOD_NguoiDung_VaiTro_LopTinChi](
	[UserID] [int] NOT NULL,
	[ID_lop_tc] [int] NOT NULL,
	[ID_vai_tro] [int] NOT NULL,
 CONSTRAINT [PK_MOD_NguoiDung_VaiTro_LopTinChi_1] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[ID_lop_tc] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[MOD_NguoiDung_VaiTro_HeThong]    Script Date: 11/08/2012 22:50:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_HeThong]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MOD_NguoiDung_VaiTro_HeThong](
	[STT] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[ID_vai_tro] [int] NOT NULL,
 CONSTRAINT [PK_MOD_NguoiDung_VaiTro_HeThong_1] PRIMARY KEY CLUSTERED 
(
	[STT] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[MOD_DanhSachLopTinChi]    Script Date: 11/08/2012 22:50:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_DanhSachLopTinChi]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MOD_DanhSachLopTinChi](
	[ID] [int] NOT NULL,
	[ID_nhom] [int] NULL,
 CONSTRAINT [PK_DangKy] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Default [DF_MOD_ChuyenNganh_ID_moodle]    Script Date: 11/08/2012 22:50:13 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_MOD_ChuyenNganh_ID_moodle]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_HocKy]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MOD_ChuyenNganh_ID_moodle]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MOD_HocKy] ADD  CONSTRAINT [DF_MOD_ChuyenNganh_ID_moodle]  DEFAULT ((0)) FOR [ID_moodle]
END


End
GO
/****** Object:  Default [DF_ThoiKhoaBieu_Id]    Script Date: 11/08/2012 22:50:13 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ThoiKhoaBieu_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_LopTinChi_TC]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ThoiKhoaBieu_Id]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MOD_LopTinChi_TC] ADD  CONSTRAINT [DF_ThoiKhoaBieu_Id]  DEFAULT ((0)) FOR [ID_moodle]
END


End
GO
/****** Object:  Default [DF_TaiKhoan_Quyen_ID_quyen]    Script Date: 11/08/2012 22:50:13 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_TaiKhoan_Quyen_ID_quyen]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_HeThong]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TaiKhoan_Quyen_ID_quyen]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MOD_NguoiDung_VaiTro_HeThong] ADD  CONSTRAINT [DF_TaiKhoan_Quyen_ID_quyen]  DEFAULT ((0)) FOR [ID_vai_tro]
END


End
GO
/****** Object:  Default [DF_Quyen_He_thong]    Script Date: 11/08/2012 22:50:14 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Quyen_He_thong]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_VaiTro]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Quyen_He_thong]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MOD_VaiTro] ADD  CONSTRAINT [DF_Quyen_He_thong]  DEFAULT ((0)) FOR [He_thong]
END


End
GO
/****** Object:  ForeignKey [FK_DangKy_Nhom]    Script Date: 11/08/2012 22:50:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DangKy_Nhom]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_DanhSachLopTinChi]'))
ALTER TABLE [dbo].[MOD_DanhSachLopTinChi]  WITH CHECK ADD  CONSTRAINT [FK_DangKy_Nhom] FOREIGN KEY([ID_nhom])
REFERENCES [dbo].[MOD_NhomHocVien] ([ID_nhom])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DangKy_Nhom]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_DanhSachLopTinChi]'))
ALTER TABLE [dbo].[MOD_DanhSachLopTinChi] CHECK CONSTRAINT [FK_DangKy_Nhom]
GO
/****** Object:  ForeignKey [FK_MOD_HocKy_ChuyenNganh_MOD_HocKy]    Script Date: 11/08/2012 22:50:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_HocKy_ChuyenNganh_MOD_HocKy]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_HocKy_ChuyenNganh]'))
ALTER TABLE [dbo].[MOD_HocKy_ChuyenNganh]  WITH CHECK ADD  CONSTRAINT [FK_MOD_HocKy_ChuyenNganh_MOD_HocKy] FOREIGN KEY([Ky_dang_ky])
REFERENCES [dbo].[MOD_HocKy] ([ID_moodle])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_HocKy_ChuyenNganh_MOD_HocKy]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_HocKy_ChuyenNganh]'))
ALTER TABLE [dbo].[MOD_HocKy_ChuyenNganh] CHECK CONSTRAINT [FK_MOD_HocKy_ChuyenNganh_MOD_HocKy]
GO
/****** Object:  ForeignKey [FK_MOD_NguoiDung_MOD_NhomNguoiDung]    Script Date: 11/08/2012 22:50:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NguoiDung_MOD_NhomNguoiDung]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung]'))
ALTER TABLE [dbo].[MOD_NguoiDung]  WITH CHECK ADD  CONSTRAINT [FK_MOD_NguoiDung_MOD_NhomNguoiDung] FOREIGN KEY([ID_nhom_nd])
REFERENCES [dbo].[MOD_NhomNguoiDung] ([ID_nhom_nd])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NguoiDung_MOD_NhomNguoiDung]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung]'))
ALTER TABLE [dbo].[MOD_NguoiDung] CHECK CONSTRAINT [FK_MOD_NguoiDung_MOD_NhomNguoiDung]
GO
/****** Object:  ForeignKey [FK_MOD_NguoiDung_VaiTro_HeThong_MOD_NguoiDung]    Script Date: 11/08/2012 22:50:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NguoiDung_VaiTro_HeThong_MOD_NguoiDung]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_HeThong]'))
ALTER TABLE [dbo].[MOD_NguoiDung_VaiTro_HeThong]  WITH CHECK ADD  CONSTRAINT [FK_MOD_NguoiDung_VaiTro_HeThong_MOD_NguoiDung] FOREIGN KEY([UserID])
REFERENCES [dbo].[MOD_NguoiDung] ([ID_moodle])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NguoiDung_VaiTro_HeThong_MOD_NguoiDung]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_HeThong]'))
ALTER TABLE [dbo].[MOD_NguoiDung_VaiTro_HeThong] CHECK CONSTRAINT [FK_MOD_NguoiDung_VaiTro_HeThong_MOD_NguoiDung]
GO
/****** Object:  ForeignKey [FK_MOD_NguoiDung_VaiTro_HeThong_MOD_VaiTro]    Script Date: 11/08/2012 22:50:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NguoiDung_VaiTro_HeThong_MOD_VaiTro]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_HeThong]'))
ALTER TABLE [dbo].[MOD_NguoiDung_VaiTro_HeThong]  WITH CHECK ADD  CONSTRAINT [FK_MOD_NguoiDung_VaiTro_HeThong_MOD_VaiTro] FOREIGN KEY([ID_vai_tro])
REFERENCES [dbo].[MOD_VaiTro] ([ID_vai_tro])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NguoiDung_VaiTro_HeThong_MOD_VaiTro]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_HeThong]'))
ALTER TABLE [dbo].[MOD_NguoiDung_VaiTro_HeThong] CHECK CONSTRAINT [FK_MOD_NguoiDung_VaiTro_HeThong_MOD_VaiTro]
GO
/****** Object:  ForeignKey [FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_LopTinChi_TC]    Script Date: 11/08/2012 22:50:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_LopTinChi_TC]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_LopTinChi]'))
ALTER TABLE [dbo].[MOD_NguoiDung_VaiTro_LopTinChi]  WITH CHECK ADD  CONSTRAINT [FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_LopTinChi_TC] FOREIGN KEY([ID_lop_tc])
REFERENCES [dbo].[MOD_LopTinChi_TC] ([ID_moodle])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_LopTinChi_TC]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_LopTinChi]'))
ALTER TABLE [dbo].[MOD_NguoiDung_VaiTro_LopTinChi] CHECK CONSTRAINT [FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_LopTinChi_TC]
GO
/****** Object:  ForeignKey [FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_NguoiDung]    Script Date: 11/08/2012 22:50:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_NguoiDung]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_LopTinChi]'))
ALTER TABLE [dbo].[MOD_NguoiDung_VaiTro_LopTinChi]  WITH CHECK ADD  CONSTRAINT [FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_NguoiDung] FOREIGN KEY([UserID])
REFERENCES [dbo].[MOD_NguoiDung] ([ID_moodle])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_NguoiDung]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_LopTinChi]'))
ALTER TABLE [dbo].[MOD_NguoiDung_VaiTro_LopTinChi] CHECK CONSTRAINT [FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_NguoiDung]
GO
/****** Object:  ForeignKey [FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_VaiTro]    Script Date: 11/08/2012 22:50:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_VaiTro]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_LopTinChi]'))
ALTER TABLE [dbo].[MOD_NguoiDung_VaiTro_LopTinChi]  WITH CHECK ADD  CONSTRAINT [FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_VaiTro] FOREIGN KEY([ID_vai_tro])
REFERENCES [dbo].[MOD_VaiTro] ([ID_vai_tro])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_VaiTro]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_LopTinChi]'))
ALTER TABLE [dbo].[MOD_NguoiDung_VaiTro_LopTinChi] CHECK CONSTRAINT [FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_VaiTro]
GO
/****** Object:  ForeignKey [FK_MOD_NhomHocVien_MOD_LopTinChi_TC]    Script Date: 11/08/2012 22:50:14 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NhomHocVien_MOD_LopTinChi_TC]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NhomHocVien]'))
ALTER TABLE [dbo].[MOD_NhomHocVien]  WITH CHECK ADD  CONSTRAINT [FK_MOD_NhomHocVien_MOD_LopTinChi_TC] FOREIGN KEY([ID_lop_tc])
REFERENCES [dbo].[MOD_LopTinChi_TC] ([ID_moodle])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NhomHocVien_MOD_LopTinChi_TC]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NhomHocVien]'))
ALTER TABLE [dbo].[MOD_NhomHocVien] CHECK CONSTRAINT [FK_MOD_NhomHocVien_MOD_LopTinChi_TC]
GO
/****** Object:  ForeignKey [FK_Nhom_To]    Script Date: 11/08/2012 22:50:14 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Nhom_To]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NhomHocVien]'))
ALTER TABLE [dbo].[MOD_NhomHocVien]  WITH CHECK ADD  CONSTRAINT [FK_Nhom_To] FOREIGN KEY([ID_to])
REFERENCES [dbo].[MOD_ToNhom] ([ID_to])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Nhom_To]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NhomHocVien]'))
ALTER TABLE [dbo].[MOD_NhomHocVien] CHECK CONSTRAINT [FK_Nhom_To]
GO
/****** Object:  ForeignKey [FK_MOD_ToNhom_MOD_LopTinChi_TC]    Script Date: 11/08/2012 22:50:14 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_ToNhom_MOD_LopTinChi_TC]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_ToNhom]'))
ALTER TABLE [dbo].[MOD_ToNhom]  WITH CHECK ADD  CONSTRAINT [FK_MOD_ToNhom_MOD_LopTinChi_TC] FOREIGN KEY([ID_lop_tc])
REFERENCES [dbo].[MOD_LopTinChi_TC] ([ID_moodle])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_ToNhom_MOD_LopTinChi_TC]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_ToNhom]'))
ALTER TABLE [dbo].[MOD_ToNhom] CHECK CONSTRAINT [FK_MOD_ToNhom_MOD_LopTinChi_TC]
GO
