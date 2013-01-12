/****** Script for SelectTopNRows command from SSMS  ******/
declare @Ma_sv varchar(5), @Hoc_ky int, @Nam_hoc varchar(9), @Ky_hieu varchar(5), @ID_sv int
select @Ma_sv = '40165', @Hoc_ky = 2, @Nam_hoc = '2011-2012', @Ky_hieu = '17201'
set @ID_sv = (SELECT ID_sv FROM [DHHH].[dbo].[STU_HoSoSinhVien] where Ma_sv = @Ma_sv)

  select dtc.ID_sv, dtc.ID_mon, mon.Ten_mon, mon.Ky_hieu, dtp.Diem
  from DHHH.dbo.MARK_Diem_TC dtc join DHHH.dbo.MARK_DiemThanhPhan_TC dtp
  on dtc.ID_diem = dtp.ID_diem
  join DHHH.dbo.MARK_MonHoc mon
  on dtc.ID_mon = mon.ID_mon
  where mon.Ky_hieu = @Ky_hieu and dtp.Hoc_ky_TP = @Hoc_ky
  and dtp.Nam_hoc_TP = @Nam_hoc and dtp.ID_thanh_phan = 1 and ID_sv = @ID_sv
  
  select ID_diem, DHHH.dbo.MARK_MonHoc.Ten_mon
  from DHHH.dbo.MARK_Diem_TC
  join DHHH.dbo.MARK_MonHoc on DHHH.dbo.MARK_Diem_TC.ID_mon = DHHH.dbo.MARK_MonHoc.ID_mon
  where ID_sv = @ID_sv 
  and Hoc_ky = @Hoc_ky and Nam_hoc = @Nam_hoc
  
  select * from DHHH.dbo.MARK_DiemThi_TC
  where ID_diem in ( select ID_diem
  from DHHH.dbo.MARK_Diem_TC
  join DHHH.dbo.MARK_MonHoc on DHHH.dbo.MARK_Diem_TC.ID_mon = DHHH.dbo.MARK_MonHoc.ID_mon
  where ID_sv = @ID_sv 
  and Hoc_ky = @Hoc_ky and Nam_hoc = @Nam_hoc)
  
  