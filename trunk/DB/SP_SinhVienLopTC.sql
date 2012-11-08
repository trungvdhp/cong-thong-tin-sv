USE [DHHH]
GO

/****** Object:  StoredProcedure [dbo].[SP_SinhVienLopTC]    Script Date: 11/08/2012 08:42:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[SP_SinhVienLopTC] @Id_lop_tc int
as
begin
select hs.ID_sv,Ho_ten,l.Ten_lop,Ma_sv from STU_HoSoSinhVien hs inner join STU_DanhSachLopTinChi ds on hs.ID_sv=ds.ID_sv inner join STU_Danhsach  dssv on dssv.ID_sv=hs.ID_sv inner join STU_Lop l on l.ID_lop = dssv.ID_lop 

where ds.ID_lop_tc = @Id_lop_tc
end
GO


