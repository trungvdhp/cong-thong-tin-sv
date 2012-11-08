USE [DHHH]
GO

/****** Object:  View [dbo].[ViewLopTC]    Script Date: 11/08/2012 08:39:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[ViewLopTC]
AS
SELECT     ltc.ID_lop_tc, mtc.ID_mon, mh.Ten_mon, mtc.Ky_hieu_lop_tc, mtc.Ky_dang_ky, hk.Dot, hk.Hoc_ky, hk.Nam_hoc
, mh.Ten_mon + RIGHT(mtc.Ky_hieu_lop_tc, 5) + ' (N' + RIGHT('0' + CAST(ltc.STT_lop AS VARCHAR(2)), 2) + ')' AS Ten_lop_tc, ltc.Tu_ngay,
                      ltc.Den_ngay
FROM         DHHH.dbo.PLAN_LopTinChi_TC ltc JOIN
                      DHHH.dbo.PLAN_MonTinChi_TC mtc ON mtc.ID_mon_tc = ltc.ID_mon_Tc JOIN
                      DHHH.dbo.MARK_MonHoc mh ON mh.ID_mon = mtc.ID_mon JOIN
                      DHHH.dbo.PLAN_HocKyDangKy_TC hk ON hk.Ky_dang_ky = mtc.Ky_dang_ky
WHERE     ltc.ID_lop_lt = 0
UNION
SELECT     l2.ID_lop_tc, mtc.ID_mon, mh.Ten_mon, mtc.Ky_hieu_lop_tc, mtc.Ky_dang_ky, hk.Dot, hk.Hoc_ky, hk.Nam_hoc
, mh.Ten_mon + RIGHT(mtc.Ky_hieu_lop_tc, 5) + ' (N' + RIGHT('0' + CAST(l1.STT_lop AS VARCHAR(2)), 2) 
                      + '.TH' + CAST(l2.STT_Lop AS VARCHAR) + ')' AS Ma_nhom, l2.Tu_ngay, l2.Den_ngay
FROM         DHHH.dbo.PLAN_LopTinChi_TC l1 JOIN
                      DHHH.dbo.PLAN_LopTinChi_TC l2 ON l1.ID_lop_tc = l2.ID_lop_lt JOIN
                      DHHH.dbo.PLAN_MonTinChi_TC mtc ON mtc.ID_mon_tc = l1.ID_mon_Tc JOIN
                      DHHH.dbo.MARK_MonHoc mh ON mh.ID_mon = mtc.ID_mon JOIN
                      DHHH.dbo.PLAN_HocKyDangKy_TC hk ON hk.Ky_dang_ky = mtc.Ky_dang_ky
WHERE     l2.ID_lop_lt <> 0

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 10
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewLopTC'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewLopTC'
GO


