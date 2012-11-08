USE [DHHH]
GO

/****** Object:  StoredProcedure [dbo].[SP_SinhVien]    Script Date: 11/08/2012 08:41:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Vu Dinh Trung
-- Create date: 5/11/2012
-- Description:	Lay danh sach sinh vien
-- =============================================
CREATE PROCEDURE [dbo].[SP_SinhVien] 
@Id_lop varchar(10) = '',
@Ma_sv varchar(10) = '',
@Ho_dem nvarchar(50) = '',
@Ten nvarchar(50) = '',
@Ngay_sinh varchar(20) = '',
@Gioi_tinh nvarchar(3) = ''
AS
BEGIN
	SELECT ds.ID_sv, Ma_sv
		, LEFT(Ho_ten, LEN(Ho_ten) - CHARINDEX(' ', REVERSE(Ho_ten))) Ho_dem
		, RIGHT(Ho_ten, CHARINDEX(' ', REVERSE(Ho_ten))-1) Ten
		, Ngay_sinh, gt.Gioi_tinh, lop.Ten_lop, ID_moodle
	FROM DHHH.dbo.STU_DanhSach ds
		JOIN DHHH.dbo.STU_Lop lop on ds.ID_lop = lop.ID_lop
		JOIN DHHH.dbo.STU_HoSoSinhVien hs ON ds.ID_sv = hs.ID_sv
		JOIN DHHH.dbo.STU_GioiTinh gt ON hs.ID_gioi_tinh = gt.ID_gioi_tinh
		LEFT JOIN DHHH.dbo.MOD_NguoiDung nd ON ds.ID_sv = nd.ID_nd AND nd.ID_nhom_nd = 3
	WHERE ds.ID_lop = @Id_lop
		AND Ma_sv LIKE '%' + @Ma_sv + '%'
		AND LEFT(Ho_ten, LEN(Ho_ten) - CHARINDEX(' ', REVERSE(Ho_ten))) LIKE '%' + @Ho_dem + '%'
		AND RIGHT(Ho_ten, CHARINDEX(' ', REVERSE(Ho_ten))-1) LIKE '%' + @Ten + '%'
		AND Ngay_sinh LIKE '%' + @Ngay_sinh + '%'
		AND Gioi_tinh LIKE '%' + @Gioi_tinh + '%'
END

GO


