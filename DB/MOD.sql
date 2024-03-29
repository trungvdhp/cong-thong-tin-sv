/****** Object:  ForeignKey [FK_MOD_DanhSachLopTinChi_MOD_LopTinChi_TC]    Script Date: 12/29/2012 09:55:00 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_DanhSachLopTinChi_MOD_LopTinChi_TC]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_DanhSachLopTinChi]'))
ALTER TABLE [dbo].[MOD_DanhSachLopTinChi] DROP CONSTRAINT [FK_MOD_DanhSachLopTinChi_MOD_LopTinChi_TC]
GO
/****** Object:  ForeignKey [FK_MOD_DanhSachLopTinChi_MOD_NguoiDung]    Script Date: 12/29/2012 09:55:00 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_DanhSachLopTinChi_MOD_NguoiDung]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_DanhSachLopTinChi]'))
ALTER TABLE [dbo].[MOD_DanhSachLopTinChi] DROP CONSTRAINT [FK_MOD_DanhSachLopTinChi_MOD_NguoiDung]
GO
/****** Object:  ForeignKey [FK_MOD_DanhSachLopTinChi_MOD_NhomHocVien]    Script Date: 12/29/2012 09:55:00 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_DanhSachLopTinChi_MOD_NhomHocVien]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_DanhSachLopTinChi]'))
ALTER TABLE [dbo].[MOD_DanhSachLopTinChi] DROP CONSTRAINT [FK_MOD_DanhSachLopTinChi_MOD_NhomHocVien]
GO
/****** Object:  ForeignKey [FK_MOD_DichVu_Quyen_MOD_DichVu]    Script Date: 12/29/2012 09:55:00 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_DichVu_Quyen_MOD_DichVu]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_DichVu_Quyen]'))
ALTER TABLE [dbo].[MOD_DichVu_Quyen] DROP CONSTRAINT [FK_MOD_DichVu_Quyen_MOD_DichVu]
GO
/****** Object:  ForeignKey [FK_MOD_DichVu_Quyen_MOD_Quyen]    Script Date: 12/29/2012 09:55:00 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_DichVu_Quyen_MOD_Quyen]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_DichVu_Quyen]'))
ALTER TABLE [dbo].[MOD_DichVu_Quyen] DROP CONSTRAINT [FK_MOD_DichVu_Quyen_MOD_Quyen]
GO
/****** Object:  ForeignKey [FK_MOD_LopTinChi_TC_MOD_HocKy]    Script Date: 12/29/2012 09:55:00 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_LopTinChi_TC_MOD_HocKy]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_LopTinChi_TC]'))
ALTER TABLE [dbo].[MOD_LopTinChi_TC] DROP CONSTRAINT [FK_MOD_LopTinChi_TC_MOD_HocKy]
GO
/****** Object:  ForeignKey [FK_MOD_NguoiDung_MOD_NhomNguoiDung]    Script Date: 12/29/2012 09:55:00 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NguoiDung_MOD_NhomNguoiDung]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung]'))
ALTER TABLE [dbo].[MOD_NguoiDung] DROP CONSTRAINT [FK_MOD_NguoiDung_MOD_NhomNguoiDung]
GO
/****** Object:  ForeignKey [FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_LopTinChi_TC]    Script Date: 12/29/2012 09:55:00 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_LopTinChi_TC]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_LopTinChi]'))
ALTER TABLE [dbo].[MOD_NguoiDung_VaiTro_LopTinChi] DROP CONSTRAINT [FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_LopTinChi_TC]
GO
/****** Object:  ForeignKey [FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_NguoiDung]    Script Date: 12/29/2012 09:55:00 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_NguoiDung]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_LopTinChi]'))
ALTER TABLE [dbo].[MOD_NguoiDung_VaiTro_LopTinChi] DROP CONSTRAINT [FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_NguoiDung]
GO
/****** Object:  ForeignKey [FK_MOD_NhomHocVien_MOD_LopTinChi_TC]    Script Date: 12/29/2012 09:55:00 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NhomHocVien_MOD_LopTinChi_TC]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NhomHocVien]'))
ALTER TABLE [dbo].[MOD_NhomHocVien] DROP CONSTRAINT [FK_MOD_NhomHocVien_MOD_LopTinChi_TC]
GO
/****** Object:  ForeignKey [FK_Nhom_To]    Script Date: 12/29/2012 09:55:00 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Nhom_To]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NhomHocVien]'))
ALTER TABLE [dbo].[MOD_NhomHocVien] DROP CONSTRAINT [FK_Nhom_To]
GO
/****** Object:  ForeignKey [FK_MOD_ToNhom_MOD_LopTinChi_TC]    Script Date: 12/29/2012 09:55:00 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_ToNhom_MOD_LopTinChi_TC]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_ToNhom]'))
ALTER TABLE [dbo].[MOD_ToNhom] DROP CONSTRAINT [FK_MOD_ToNhom_MOD_LopTinChi_TC]
GO
/****** Object:  Table [dbo].[MOD_DanhSachLopTinChi]    Script Date: 12/29/2012 09:55:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_DanhSachLopTinChi]') AND type in (N'U'))
DROP TABLE [dbo].[MOD_DanhSachLopTinChi]
GO
/****** Object:  Table [dbo].[MOD_NhomHocVien]    Script Date: 12/29/2012 09:55:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_NhomHocVien]') AND type in (N'U'))
DROP TABLE [dbo].[MOD_NhomHocVien]
GO
/****** Object:  Table [dbo].[MOD_ToNhom]    Script Date: 12/29/2012 09:55:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_ToNhom]') AND type in (N'U'))
DROP TABLE [dbo].[MOD_ToNhom]
GO
/****** Object:  Table [dbo].[MOD_NguoiDung_VaiTro_LopTinChi]    Script Date: 12/29/2012 09:55:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_LopTinChi]') AND type in (N'U'))
DROP TABLE [dbo].[MOD_NguoiDung_VaiTro_LopTinChi]
GO
/****** Object:  Table [dbo].[MOD_DichVu_Quyen]    Script Date: 12/29/2012 09:55:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_DichVu_Quyen]') AND type in (N'U'))
DROP TABLE [dbo].[MOD_DichVu_Quyen]
GO
/****** Object:  Table [dbo].[MOD_LopTinChi_TC]    Script Date: 12/29/2012 09:55:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_LopTinChi_TC]') AND type in (N'U'))
DROP TABLE [dbo].[MOD_LopTinChi_TC]
GO
/****** Object:  Table [dbo].[MOD_NguoiDung]    Script Date: 12/29/2012 09:55:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung]') AND type in (N'U'))
DROP TABLE [dbo].[MOD_NguoiDung]
GO
/****** Object:  Table [dbo].[MOD_HocKy]    Script Date: 12/29/2012 09:55:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_HocKy]') AND type in (N'U'))
DROP TABLE [dbo].[MOD_HocKy]
GO
/****** Object:  Table [dbo].[MOD_DichVu]    Script Date: 12/29/2012 09:55:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_DichVu]') AND type in (N'U'))
DROP TABLE [dbo].[MOD_DichVu]
GO
/****** Object:  Table [dbo].[MOD_NhomNguoiDung]    Script Date: 12/29/2012 09:55:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_NhomNguoiDung]') AND type in (N'U'))
DROP TABLE [dbo].[MOD_NhomNguoiDung]
GO
/****** Object:  Table [dbo].[MOD_Quyen]    Script Date: 12/29/2012 09:55:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_Quyen]') AND type in (N'U'))
DROP TABLE [dbo].[MOD_Quyen]
GO
/****** Object:  Default [DF_MOD_DichVu_Root]    Script Date: 12/29/2012 09:55:00 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_MOD_DichVu_Root]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_DichVu]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MOD_DichVu_Root]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MOD_DichVu] DROP CONSTRAINT [DF_MOD_DichVu_Root]
END


End
GO
/****** Object:  Default [DF_MOD_NguoiDung_VaiTro_LopTinChi_Dinh_chi]    Script Date: 12/29/2012 09:55:00 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_MOD_NguoiDung_VaiTro_LopTinChi_Dinh_chi]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_LopTinChi]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MOD_NguoiDung_VaiTro_LopTinChi_Dinh_chi]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MOD_NguoiDung_VaiTro_LopTinChi] DROP CONSTRAINT [DF_MOD_NguoiDung_VaiTro_LopTinChi_Dinh_chi]
END


End
GO
/****** Object:  Table [dbo].[MOD_Quyen]    Script Date: 12/29/2012 09:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_Quyen]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MOD_Quyen](
	[ID_quyen] [int] IDENTITY(1,1) NOT NULL,
	[Ten_quyen] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Action_name] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_MOD_Quyen] PRIMARY KEY CLUSTERED 
(
	[ID_quyen] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[MOD_Quyen] ON
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (341, N'Quản lý danh mục học kỳ', N'MoodleCategory.ManageSemester')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (342, N'Tạo các học kỳ', N'MoodleCategory.CreateSemesters')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (343, N'Xóa các học kỳ', N'MoodleCategory.DeleteSemesters')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (344, N'MoodleCourse.Manage', N'MoodleCourse.Manage')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (345, N'MoodleCourse.CreateCourses', N'MoodleCourse.CreateCourses')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (346, N'MoodleCourse.DeleteCourses', N'MoodleCourse.DeleteCourses')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (347, N'MoodleCourse.CourseStudentGrade', N'MoodleCourse.CourseStudentGrade')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (349, N'MoodleCourse.MyCourseGrade', N'MoodleCourse.MyCourseGrade')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (353, N'MoodleEnrol.EnrolStudent', N'MoodleEnrol.EnrolStudent')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (354, N'MoodleEnrol.ManualEnrolStudents', N'MoodleEnrol.ManualEnrolStudents')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (355, N'MoodleEnrol.SuspendEnrolStudents', N'MoodleEnrol.SuspendEnrolStudents')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (356, N'MoodleEnrol.EnrolTeacher', N'MoodleEnrol.EnrolTeacher')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (357, N'MoodleEnrol.ManualEnrolTeachers', N'MoodleEnrol.ManualEnrolTeachers')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (359, N'MoodleQuiz.UpdateYGrades', N'MoodleQuiz.UpdateYGrades')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (360, N'MoodleQuiz.CourseQuizList', N'MoodleQuiz.CourseQuizList')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (361, N'MoodleQuiz.QuizStudentGrade', N'MoodleQuiz.QuizStudentGrade')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (363, N'MoodleQuiz.MyQuizGrade', N'MoodleQuiz.MyQuizGrade')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (365, N'MoodleQuiz.StudentQuizGrade', N'MoodleQuiz.StudentQuizGrade')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (367, N'MoodleQuiz.MyQuizResult', N'MoodleQuiz.MyQuizResult')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (368, N'MoodleQuiz.StudentQuizResult', N'MoodleQuiz.StudentQuizResult')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (369, N'MoodleGroup.StudentGroup', N'MoodleGroup.StudentGroup')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (370, N'MoodleGroup.CreateGroup', N'MoodleGroup.CreateGroup')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (371, N'MoodleGroup.DeleteGroups', N'MoodleGroup.DeleteGroups')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (372, N'MoodleGroup.AddGroupMembers', N'MoodleGroup.AddGroupMembers')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (373, N'MoodleGroup.DeleteGroupMembers', N'MoodleGroup.DeleteGroupMembers')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (374, N'MoodleGroup.CreateGrouping', N'MoodleGroup.CreateGrouping')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (375, N'MoodleGroup.DeleteGroupings', N'MoodleGroup.DeleteGroupings')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (376, N'MoodleGroup.UpdateGrouping', N'MoodleGroup.UpdateGrouping')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (377, N'MoodleGroup.AssignGrouping', N'MoodleGroup.AssignGrouping')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (378, N'MoodleGroup.UnassignGrouping', N'MoodleGroup.UnassignGrouping')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (379, N'MoodleUser.ManageStudent', N'MoodleUser.ManageStudent')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (380, N'MoodleUser.CreateStudents', N'MoodleUser.CreateStudents')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (381, N'MoodleUser.DeleteStudents', N'MoodleUser.DeleteStudents')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (382, N'MoodleUser.UpdateStudents', N'MoodleUser.UpdateStudents')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (383, N'MoodleUser.SyncStudents', N'MoodleUser.SyncStudents')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (384, N'MoodleUser.ManageTeacher', N'MoodleUser.ManageTeacher')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (385, N'MoodleUser.CreateTeachers', N'MoodleUser.CreateTeachers')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (386, N'MoodleUser.DeleteTeachers', N'MoodleUser.DeleteTeachers')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (387, N'MoodleUser.UpdateTeachers', N'MoodleUser.UpdateTeachers')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (388, N'MoodleUser.SyncTeachers', N'MoodleUser.SyncTeachers')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (389, N'MoodleUser.ManageAdminUser', N'MoodleUser.ManageAdminUser')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (390, N'MoodleUser.CreateAdminUsers', N'MoodleUser.CreateAdminUsers')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (391, N'MoodleUser.DeleteAdminUsers', N'MoodleUser.DeleteAdminUsers')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (392, N'MoodleUser.UpdateAdminUsers', N'MoodleUser.UpdateAdminUsers')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (393, N'MoodleUser.SyncAdminUsers', N'MoodleUser.SyncAdminUsers')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (394, N'MoodleUser.ChangePassword', N'MoodleUser.ChangePassword')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (395, N'MoodleUser.MyProfile', N'MoodleUser.MyProfile')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (399, N'MoodleUser.MyCourseProfile', N'MoodleUser.MyCourseProfile')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (404, N'MoodleCourse.StudentCourseGrade', N'MoodleCourse.StudentCourseGrade')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (406, N'MoodleUser.UserProfile', N'MoodleUser.UserProfile')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (407, N'MoodleUser.UserCourseProfile', N'MoodleUser.UserCourseProfile')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (408, N'MoodleEnrol.UnassignTeacherRole', N'MoodleEnrol.UnassignTeacherRole')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (409, N'MoodleEnrol.UnassignTeacherAllRoles', N'MoodleEnrol.UnassignTeacherAllRoles')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (410, N'MoodleEnrol.EnrolAdminUser', N'MoodleEnrol.EnrolAdminUser')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (411, N'MoodleEnrol.ManualEnrolAdminUsers', N'MoodleEnrol.ManualEnrolAdminUsers')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (412, N'MoodleEnrol.UnassignAdminUserRole', N'MoodleEnrol.UnassignAdminUserRole')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (413, N'MoodleUser.SearchStudent', N'MoodleUser.SearchStudent')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (417, N'Tìm kiếm tài khoản moodle giáo viên', N'MoodleUser.SearchTeacher')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (418, N'Xem bảng điểm tổng kết khóa học tôi được ghi danh', N'MoodleCourse.MyCourseStudentGrade')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (419, N'MoodleQuiz.MyQuizStudentGrade', N'MoodleQuiz.MyQuizStudentGrade')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (420, N'MoodleQuiz.UpdateYGrade', N'MoodleQuiz.UpdateYGrade')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (421, N'MoodleUser.AssignTeacherSystemRole', N'MoodleUser.AssignTeacherSystemRole')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (422, N'MoodleUser.UnassignTeacherSystemRole', N'MoodleUser.UnassignTeacherSystemRole')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (423, N'MoodleUser.UnassignTeacherAllSystemRoles', N'MoodleUser.UnassignTeacherAllSystemRoles')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (424, N'MoodleUser.AssignAdminUserSystemRole', N'MoodleUser.AssignAdminUserSystemRole')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (425, N'MoodleUser.UnassignAdminUserSystemRole', N'MoodleUser.UnassignAdminUserSystemRole')
INSERT [dbo].[MOD_Quyen] ([ID_quyen], [Ten_quyen], [Action_name]) VALUES (426, N'MoodleUser.UnassignAdminUserAllSystemRoles', N'MoodleUser.UnassignAdminUserAllSystemRoles')
SET IDENTITY_INSERT [dbo].[MOD_Quyen] OFF
/****** Object:  Table [dbo].[MOD_NhomNguoiDung]    Script Date: 12/29/2012 09:55:00 ******/
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
/****** Object:  Table [dbo].[MOD_DichVu]    Script Date: 12/29/2012 09:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_DichVu]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MOD_DichVu](
	[ID_dv] [int] IDENTITY(1,1) NOT NULL,
	[Ten_dv] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Ten_rut_gon] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Root] [bit] NULL,
 CONSTRAINT [PK_DichVu] PRIMARY KEY CLUSTERED 
(
	[ID_dv] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[MOD_DichVu] ON
INSERT [dbo].[MOD_DichVu] ([ID_dv], [Ten_dv], [Ten_rut_gon], [Root]) VALUES (1, N'Quản trị hệ thống', N'admin', 1)
INSERT [dbo].[MOD_DichVu] ([ID_dv], [Ten_dv], [Ten_rut_gon], [Root]) VALUES (2, N'Quản trị khóa học', N'course', NULL)
INSERT [dbo].[MOD_DichVu] ([ID_dv], [Ten_dv], [Ten_rut_gon], [Root]) VALUES (3, N'Người dùng', N'user', NULL)
SET IDENTITY_INSERT [dbo].[MOD_DichVu] OFF
/****** Object:  Table [dbo].[MOD_HocKy]    Script Date: 12/29/2012 09:55:00 ******/
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
INSERT [dbo].[MOD_HocKy] ([ID_moodle], [Ky_dang_ky]) VALUES (10, 4)
INSERT [dbo].[MOD_HocKy] ([ID_moodle], [Ky_dang_ky]) VALUES (13, 1)
/****** Object:  Table [dbo].[MOD_NguoiDung]    Script Date: 12/29/2012 09:55:00 ******/
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
	[ID_vai_tro] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_MOD_NguoiDung] PRIMARY KEY CLUSTERED 
(
	[ID_moodle] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (2, 1, 1, N'1')
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (32, 10, 1, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (35, 11, 1, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (298, 18115, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (299, 18116, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (301, 18117, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (302, 18118, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (303, 18119, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (304, 18120, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (305, 18121, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (306, 18122, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (309, 18123, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (310, 18124, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (311, 18125, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (312, 18126, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (313, 18127, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (314, 18128, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (316, 18129, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (317, 18130, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (319, 18131, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (320, 18132, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (322, 18133, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (323, 18134, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (324, 18135, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (325, 18136, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (327, 18137, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (328, 18138, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (329, 18139, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (330, 18140, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (331, 18141, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (332, 18142, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (333, 18143, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (334, 18144, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (336, 18145, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (339, 18146, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (340, 18147, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (341, 18148, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (342, 18149, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (343, 18150, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (344, 18151, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (345, 18152, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (346, 18153, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (347, 18154, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (348, 18155, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (349, 18156, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (350, 18157, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (351, 18158, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (352, 18159, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (353, 18160, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (354, 18161, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (355, 18162, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (356, 18163, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (357, 18164, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (358, 18165, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (360, 18166, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (361, 18167, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (363, 18168, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (364, 18169, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (367, 18170, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (368, 18171, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (369, 18172, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (371, 18173, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (372, 18174, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (373, 18175, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (374, 18176, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (375, 18177, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (376, 18178, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (377, 18179, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (379, 18180, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (380, 18181, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (381, 18182, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (382, 18183, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (383, 18184, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (384, 18185, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (385, 18186, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (386, 18187, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (387, 18188, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (388, 18189, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (391, 18190, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (392, 18191, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (394, 18192, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (397, 18193, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (398, 18194, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (399, 18195, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (400, 18196, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (401, 18197, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (402, 18198, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (403, 18199, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (404, 18200, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (405, 18201, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (406, 18202, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (407, 18203, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (408, 18204, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (409, 18205, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (410, 18206, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (411, 18207, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (412, 18208, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (414, 18209, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (415, 18210, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (416, 18211, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (417, 18212, 3, NULL)
GO
print 'Processed 100 total records'
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (418, 18213, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (419, 18214, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (420, 18215, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (421, 18216, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (422, 18217, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (423, 18218, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (424, 18219, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (425, 18220, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (428, 15682, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (429, 16611, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (430, 16613, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (431, 16791, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (432, 16799, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (433, 16946, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (435, 16974, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (437, 17377, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (438, 17379, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (439, 17380, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (440, 17381, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (441, 17382, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (442, 17383, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (443, 17384, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (444, 17385, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (445, 17386, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (446, 17387, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (448, 17388, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (449, 17389, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (450, 17390, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (451, 17391, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (452, 17392, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (453, 17393, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (454, 17394, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (455, 17395, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (456, 17396, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (457, 17398, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (458, 17399, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (459, 17400, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (460, 17401, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (461, 17402, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (462, 17403, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (464, 17404, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (465, 17405, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (466, 17406, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (467, 17407, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (468, 17408, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (469, 17409, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (470, 17410, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (471, 17411, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (472, 17412, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (473, 17413, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (474, 17414, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (475, 17415, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (476, 17416, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (477, 17417, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (478, 17418, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (480, 17419, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (481, 17420, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (482, 17421, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (483, 17422, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (484, 17423, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (486, 17424, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (487, 17425, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (488, 17426, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (489, 17428, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (490, 17429, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (491, 17430, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (492, 17432, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (493, 17433, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (494, 17434, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (495, 17436, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (496, 15868, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (497, 16610, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (498, 16622, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (499, 16765, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (500, 16792, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (501, 16944, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (502, 16971, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (503, 16996, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (504, 17437, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (505, 17438, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (507, 17439, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (508, 17441, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (509, 17442, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (510, 17443, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (511, 17444, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (512, 17445, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (513, 17446, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (514, 17448, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (515, 17449, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (516, 17450, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (517, 17451, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (518, 17453, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (519, 17454, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (520, 17455, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (521, 17457, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (522, 17458, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (523, 17459, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (524, 17460, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (525, 17461, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (526, 17462, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (527, 17463, 3, NULL)
GO
print 'Processed 200 total records'
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (528, 17464, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (529, 17466, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (530, 17467, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (531, 17468, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (532, 17469, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (533, 17470, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (534, 17471, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (535, 17472, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (536, 17473, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (537, 17474, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (538, 17475, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (539, 17476, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (540, 17477, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (541, 17478, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (542, 17479, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (543, 17480, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (544, 17481, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (545, 17482, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (546, 17483, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (547, 17484, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (548, 17485, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (549, 17486, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (550, 17487, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (551, 17488, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (552, 17489, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (553, 17490, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (554, 17491, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (555, 17492, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (556, 17494, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (557, 17495, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (558, 17496, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (723, 9259, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (724, 11092, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (725, 11093, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (726, 11095, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (727, 11096, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (728, 11097, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (729, 11098, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (730, 11099, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (731, 11100, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (732, 11101, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (733, 11102, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (734, 11103, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (735, 11105, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (736, 11106, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (737, 11107, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (738, 11108, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (739, 11109, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (740, 11110, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (741, 11111, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (742, 11112, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (743, 11113, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (744, 11114, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (745, 11115, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (746, 11117, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (747, 11118, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (748, 11119, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (750, 11120, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (751, 11121, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (752, 11122, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (753, 11123, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (754, 11124, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (755, 11125, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (756, 11126, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (758, 11128, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (759, 11129, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (760, 11130, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (761, 11131, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (762, 11132, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (763, 11133, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (766, 11135, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (767, 11136, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (768, 11137, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (770, 11138, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (771, 11139, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (772, 11140, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (773, 11141, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (774, 11142, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (775, 11143, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (776, 11145, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (777, 11146, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (779, 11148, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (780, 11150, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (781, 11151, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (782, 11152, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (783, 11153, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (784, 11154, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (785, 11156, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (786, 11157, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (787, 11158, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (789, 11159, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (790, 11160, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (791, 11161, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (792, 11162, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (793, 11163, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (794, 11164, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (795, 11165, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (796, 11166, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (797, 11167, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (798, 11168, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (799, 11169, 3, NULL)
GO
print 'Processed 300 total records'
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (800, 11170, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (801, 11171, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (802, 11172, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (803, 11173, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (804, 11174, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (805, 11175, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (806, 11176, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (807, 11177, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (808, 11178, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (809, 11179, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (810, 11180, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (811, 11181, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (812, 11182, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (813, 11183, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (814, 11184, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (815, 11185, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (816, 11186, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (817, 11187, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (818, 11188, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (819, 11189, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (820, 11190, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (821, 11191, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (822, 11192, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (823, 11194, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (824, 11195, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (825, 11196, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (826, 11197, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (827, 11198, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (828, 11199, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (829, 11200, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (830, 11202, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (831, 11203, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (832, 11204, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (833, 11205, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (834, 11206, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (835, 11207, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (836, 11208, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (837, 11209, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (838, 11210, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (839, 11211, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (840, 11212, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (841, 11213, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (844, 11214, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (845, 11215, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (846, 11216, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (847, 11218, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (848, 11219, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (849, 11220, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (850, 11221, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (851, 11224, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (852, 11225, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (853, 11226, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (854, 11227, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (855, 11228, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (856, 11229, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (857, 11230, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (858, 11231, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (859, 11232, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (860, 11233, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (862, 1267, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (863, 1279, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (864, 1312, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (865, 1337, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (866, 1338, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (867, 1483, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (868, 1568, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (869, 1569, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (870, 1570, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (871, 1571, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (872, 1572, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (873, 1573, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (874, 1574, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (875, 1575, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (876, 1576, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (877, 1577, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (878, 1578, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (879, 1579, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (880, 1580, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (881, 1581, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (882, 1582, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (883, 1583, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (884, 1584, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (885, 1585, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (886, 1586, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (887, 1587, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (888, 1588, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (889, 1589, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (890, 1590, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (891, 1591, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (892, 1592, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (893, 1593, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (894, 1594, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (895, 1595, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (896, 1596, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (897, 1597, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (898, 1598, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (899, 1599, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (900, 1600, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (901, 1601, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (902, 1602, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (903, 1603, 3, NULL)
GO
print 'Processed 400 total records'
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (904, 1604, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (905, 1605, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (906, 1606, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (907, 1607, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (908, 1608, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (909, 1609, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (910, 1610, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (911, 1611, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (912, 1612, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (913, 1613, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (914, 1614, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (915, 1615, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (916, 1616, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (917, 1617, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (918, 2933, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (919, 2934, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (920, 2935, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (921, 2936, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (922, 2937, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (923, 2938, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (924, 2939, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (925, 2940, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (926, 2941, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (927, 2942, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (928, 2943, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (929, 2944, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (930, 2945, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (931, 2946, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (932, 2947, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (933, 2948, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (934, 2949, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (935, 2950, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (936, 2951, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (937, 2952, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (938, 2953, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (939, 2954, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (940, 2955, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (941, 2956, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (942, 2957, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (943, 2958, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (944, 2959, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (945, 2960, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (946, 2961, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (947, 12483, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (948, 12484, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (949, 12485, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (950, 12486, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (951, 12487, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (952, 12488, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (953, 12489, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (954, 12490, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (955, 12491, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (956, 12492, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (957, 12493, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (959, 12494, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (960, 12495, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (961, 12496, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (962, 12497, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (963, 12498, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (964, 12499, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (965, 12500, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (966, 12501, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (967, 12502, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (968, 12503, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (969, 12504, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (970, 12505, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (971, 12506, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (972, 12507, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (973, 12508, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (974, 12509, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (975, 12510, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (976, 12511, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (977, 12512, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (978, 12513, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (979, 12514, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (980, 12515, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (981, 12516, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (982, 12517, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (983, 12518, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (984, 12519, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (985, 12520, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (986, 12521, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (987, 12522, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (988, 12523, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (989, 12524, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (990, 12525, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (991, 12526, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (992, 12527, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (993, 12528, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (994, 12529, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (995, 12530, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (996, 12531, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (997, 12532, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (998, 12533, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (999, 12534, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1000, 12535, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1001, 12536, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1002, 12537, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1003, 12538, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1004, 12539, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1005, 12540, 3, NULL)
GO
print 'Processed 500 total records'
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1006, 12541, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1007, 12542, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1009, 12543, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1010, 12544, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1011, 12545, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1012, 12546, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1013, 12547, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1014, 12548, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1015, 12549, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1016, 12550, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1017, 12551, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1018, 12552, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1019, 12553, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1020, 12554, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1021, 12555, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1022, 12556, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1023, 12557, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1024, 12558, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1025, 12559, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1026, 12560, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1028, 12561, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1029, 12562, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1030, 12563, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1031, 12564, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1032, 12565, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1033, 12566, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1034, 12567, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1035, 12568, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1037, 12569, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1038, 12570, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1039, 12571, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1040, 12572, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1041, 12573, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1042, 12574, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1043, 12575, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1044, 12576, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1045, 12577, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1046, 12578, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1047, 12579, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1048, 12580, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1049, 12581, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1050, 12582, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1051, 12583, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1052, 12584, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1053, 12585, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1054, 12586, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1055, 12587, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1056, 12588, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1057, 12589, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1058, 12590, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1059, 12591, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1060, 12592, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1061, 12593, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1062, 12594, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1063, 12595, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1064, 12596, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1065, 12597, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1066, 12598, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1067, 12599, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1068, 12600, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1069, 12601, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1070, 12602, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1071, 12603, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1072, 12604, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1073, 12605, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1074, 12606, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1075, 12607, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1076, 12608, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1077, 12609, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1078, 12610, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1079, 12611, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1080, 12612, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1081, 12613, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1082, 12614, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1083, 12615, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1084, 12616, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1085, 12617, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1086, 12618, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1087, 12619, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1088, 12620, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1089, 12621, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1090, 12622, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1091, 12623, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1092, 12624, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1093, 12625, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1094, 12626, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1095, 12627, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1096, 12628, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1097, 12629, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1098, 12630, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1099, 12631, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1100, 12632, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1101, 12633, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1102, 12634, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1103, 12635, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1104, 12636, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1105, 12637, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1106, 12638, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1107, 12639, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1108, 12640, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1117, 5, 2, NULL)
GO
print 'Processed 600 total records'
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1118, 9255, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1119, 9403, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1124, 2, 1, N'1,2')
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1126, 10080, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1127, 10096, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1128, 945, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1129, 954, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1134, 956, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1135, 960, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1136, 965, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1137, 978, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1138, 984, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1139, 987, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1140, 10252, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1141, 10289, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1142, 12364, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1143, 13402, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1144, 13406, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1145, 13434, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1146, 13459, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1147, 13473, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1148, 13481, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1149, 13486, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1150, 13489, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1151, 13503, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1152, 13512, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1153, 13513, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1154, 13549, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1155, 13557, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1156, 13562, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1157, 14078, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1158, 14341, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1159, 14364, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1160, 14570, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1161, 14580, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1162, 14598, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1163, 14603, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1164, 14608, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1165, 14614, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1166, 14630, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1167, 14631, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1168, 14634, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1169, 14638, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1170, 14651, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1171, 14656, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1172, 14684, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1173, 14716, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1174, 14719, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1175, 14721, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1176, 14724, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1177, 14742, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1178, 14759, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1179, 14761, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1180, 14767, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1181, 14769, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1184, 1, 2, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1186, 10121, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1187, 3, 2, N'')
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1188, 10146, 3, NULL)
INSERT [dbo].[MOD_NguoiDung] ([ID_moodle], [ID_nd], [ID_nhom_nd], [ID_vai_tro]) VALUES (1190, 3, 1, NULL)
/****** Object:  Table [dbo].[MOD_LopTinChi_TC]    Script Date: 12/29/2012 09:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_LopTinChi_TC]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MOD_LopTinChi_TC](
	[ID_moodle] [int] NOT NULL,
	[ID_lop_tc] [int] NOT NULL,
	[ID_danhmuc] [int] NOT NULL,
 CONSTRAINT [PK_MOD_LopTinChi_TC] PRIMARY KEY CLUSTERED 
(
	[ID_moodle] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[MOD_LopTinChi_TC] ([ID_moodle], [ID_lop_tc], [ID_danhmuc]) VALUES (46, 1380, 10)
INSERT [dbo].[MOD_LopTinChi_TC] ([ID_moodle], [ID_lop_tc], [ID_danhmuc]) VALUES (47, 1381, 10)
/****** Object:  Table [dbo].[MOD_DichVu_Quyen]    Script Date: 12/29/2012 09:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_DichVu_Quyen]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MOD_DichVu_Quyen](
	[ID_dv] [int] NOT NULL,
	[ID_quyen] [int] NOT NULL,
 CONSTRAINT [PK_MOD_DichVu_Quyen] PRIMARY KEY CLUSTERED 
(
	[ID_dv] ASC,
	[ID_quyen] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 344)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 345)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 346)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 347)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 349)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 354)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 355)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 356)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 357)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 359)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 360)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 361)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 363)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 365)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 367)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 368)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 369)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 370)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 371)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 372)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 373)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 374)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 375)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 376)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 377)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 378)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 379)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 385)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 386)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (2, 387)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (3, 349)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (3, 363)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (3, 367)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (3, 395)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (3, 399)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (3, 406)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (3, 407)
INSERT [dbo].[MOD_DichVu_Quyen] ([ID_dv], [ID_quyen]) VALUES (3, 413)
/****** Object:  Table [dbo].[MOD_NguoiDung_VaiTro_LopTinChi]    Script Date: 12/29/2012 09:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_LopTinChi]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MOD_NguoiDung_VaiTro_LopTinChi](
	[UserID] [int] NOT NULL,
	[ID_lop_tc] [int] NOT NULL,
	[ID_vai_tro] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Dinh_chi] [bit] NOT NULL,
 CONSTRAINT [PK_MOD_NguoiDung_VaiTro_LopTinChi_1] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[ID_lop_tc] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[MOD_NguoiDung_VaiTro_LopTinChi] ([UserID], [ID_lop_tc], [ID_vai_tro], [Dinh_chi]) VALUES (2, 46, N'3', 0)
INSERT [dbo].[MOD_NguoiDung_VaiTro_LopTinChi] ([UserID], [ID_lop_tc], [ID_vai_tro], [Dinh_chi]) VALUES (1184, 46, N'1', 1)
INSERT [dbo].[MOD_NguoiDung_VaiTro_LopTinChi] ([UserID], [ID_lop_tc], [ID_vai_tro], [Dinh_chi]) VALUES (1187, 46, N'1,3,4', 0)
INSERT [dbo].[MOD_NguoiDung_VaiTro_LopTinChi] ([UserID], [ID_lop_tc], [ID_vai_tro], [Dinh_chi]) VALUES (1190, 46, N'', 0)
/****** Object:  Table [dbo].[MOD_ToNhom]    Script Date: 12/29/2012 09:55:00 ******/
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
INSERT [dbo].[MOD_ToNhom] ([ID_to], [Ten_to], [Mo_ta], [ID_lop_tc]) VALUES (7, N'gt', N'gt', 46)
/****** Object:  Table [dbo].[MOD_NhomHocVien]    Script Date: 12/29/2012 09:55:00 ******/
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
INSERT [dbo].[MOD_NhomHocVien] ([ID_nhom], [Ten_nhom], [Mo_ta], [ID_to], [ID_lop_tc]) VALUES (9, N'VTT52DH2', N'VTT52DH2', 7, 46)
INSERT [dbo].[MOD_NhomHocVien] ([ID_nhom], [Ten_nhom], [Mo_ta], [ID_to], [ID_lop_tc]) VALUES (10, N'VTT52DH1', N'VTT52DH1', NULL, 46)
/****** Object:  Table [dbo].[MOD_DanhSachLopTinChi]    Script Date: 12/29/2012 09:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD_DanhSachLopTinChi]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MOD_DanhSachLopTinChi](
	[ID] [int] NOT NULL,
	[ID_sv] [int] NOT NULL,
	[ID_lop_tc] [int] NOT NULL,
	[ID_nhom] [int] NULL,
 CONSTRAINT [PK_DangKy] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[MOD_DanhSachLopTinChi] ([ID], [ID_sv], [ID_lop_tc], [ID_nhom]) VALUES (35288, 1128, 46, NULL)
INSERT [dbo].[MOD_DanhSachLopTinChi] ([ID], [ID_sv], [ID_lop_tc], [ID_nhom]) VALUES (61440, 1129, 46, NULL)
/****** Object:  Default [DF_MOD_DichVu_Root]    Script Date: 12/29/2012 09:55:00 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_MOD_DichVu_Root]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_DichVu]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MOD_DichVu_Root]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MOD_DichVu] ADD  CONSTRAINT [DF_MOD_DichVu_Root]  DEFAULT ((0)) FOR [Root]
END


End
GO
/****** Object:  Default [DF_MOD_NguoiDung_VaiTro_LopTinChi_Dinh_chi]    Script Date: 12/29/2012 09:55:00 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_MOD_NguoiDung_VaiTro_LopTinChi_Dinh_chi]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_LopTinChi]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MOD_NguoiDung_VaiTro_LopTinChi_Dinh_chi]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MOD_NguoiDung_VaiTro_LopTinChi] ADD  CONSTRAINT [DF_MOD_NguoiDung_VaiTro_LopTinChi_Dinh_chi]  DEFAULT ((0)) FOR [Dinh_chi]
END


End
GO
/****** Object:  ForeignKey [FK_MOD_DanhSachLopTinChi_MOD_LopTinChi_TC]    Script Date: 12/29/2012 09:55:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_DanhSachLopTinChi_MOD_LopTinChi_TC]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_DanhSachLopTinChi]'))
ALTER TABLE [dbo].[MOD_DanhSachLopTinChi]  WITH CHECK ADD  CONSTRAINT [FK_MOD_DanhSachLopTinChi_MOD_LopTinChi_TC] FOREIGN KEY([ID_lop_tc])
REFERENCES [dbo].[MOD_LopTinChi_TC] ([ID_moodle])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_DanhSachLopTinChi_MOD_LopTinChi_TC]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_DanhSachLopTinChi]'))
ALTER TABLE [dbo].[MOD_DanhSachLopTinChi] CHECK CONSTRAINT [FK_MOD_DanhSachLopTinChi_MOD_LopTinChi_TC]
GO
/****** Object:  ForeignKey [FK_MOD_DanhSachLopTinChi_MOD_NguoiDung]    Script Date: 12/29/2012 09:55:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_DanhSachLopTinChi_MOD_NguoiDung]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_DanhSachLopTinChi]'))
ALTER TABLE [dbo].[MOD_DanhSachLopTinChi]  WITH CHECK ADD  CONSTRAINT [FK_MOD_DanhSachLopTinChi_MOD_NguoiDung] FOREIGN KEY([ID_sv])
REFERENCES [dbo].[MOD_NguoiDung] ([ID_moodle])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_DanhSachLopTinChi_MOD_NguoiDung]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_DanhSachLopTinChi]'))
ALTER TABLE [dbo].[MOD_DanhSachLopTinChi] CHECK CONSTRAINT [FK_MOD_DanhSachLopTinChi_MOD_NguoiDung]
GO
/****** Object:  ForeignKey [FK_MOD_DanhSachLopTinChi_MOD_NhomHocVien]    Script Date: 12/29/2012 09:55:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_DanhSachLopTinChi_MOD_NhomHocVien]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_DanhSachLopTinChi]'))
ALTER TABLE [dbo].[MOD_DanhSachLopTinChi]  WITH CHECK ADD  CONSTRAINT [FK_MOD_DanhSachLopTinChi_MOD_NhomHocVien] FOREIGN KEY([ID_nhom])
REFERENCES [dbo].[MOD_NhomHocVien] ([ID_nhom])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_DanhSachLopTinChi_MOD_NhomHocVien]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_DanhSachLopTinChi]'))
ALTER TABLE [dbo].[MOD_DanhSachLopTinChi] CHECK CONSTRAINT [FK_MOD_DanhSachLopTinChi_MOD_NhomHocVien]
GO
/****** Object:  ForeignKey [FK_MOD_DichVu_Quyen_MOD_DichVu]    Script Date: 12/29/2012 09:55:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_DichVu_Quyen_MOD_DichVu]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_DichVu_Quyen]'))
ALTER TABLE [dbo].[MOD_DichVu_Quyen]  WITH CHECK ADD  CONSTRAINT [FK_MOD_DichVu_Quyen_MOD_DichVu] FOREIGN KEY([ID_dv])
REFERENCES [dbo].[MOD_DichVu] ([ID_dv])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_DichVu_Quyen_MOD_DichVu]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_DichVu_Quyen]'))
ALTER TABLE [dbo].[MOD_DichVu_Quyen] CHECK CONSTRAINT [FK_MOD_DichVu_Quyen_MOD_DichVu]
GO
/****** Object:  ForeignKey [FK_MOD_DichVu_Quyen_MOD_Quyen]    Script Date: 12/29/2012 09:55:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_DichVu_Quyen_MOD_Quyen]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_DichVu_Quyen]'))
ALTER TABLE [dbo].[MOD_DichVu_Quyen]  WITH CHECK ADD  CONSTRAINT [FK_MOD_DichVu_Quyen_MOD_Quyen] FOREIGN KEY([ID_quyen])
REFERENCES [dbo].[MOD_Quyen] ([ID_quyen])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_DichVu_Quyen_MOD_Quyen]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_DichVu_Quyen]'))
ALTER TABLE [dbo].[MOD_DichVu_Quyen] CHECK CONSTRAINT [FK_MOD_DichVu_Quyen_MOD_Quyen]
GO
/****** Object:  ForeignKey [FK_MOD_LopTinChi_TC_MOD_HocKy]    Script Date: 12/29/2012 09:55:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_LopTinChi_TC_MOD_HocKy]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_LopTinChi_TC]'))
ALTER TABLE [dbo].[MOD_LopTinChi_TC]  WITH CHECK ADD  CONSTRAINT [FK_MOD_LopTinChi_TC_MOD_HocKy] FOREIGN KEY([ID_danhmuc])
REFERENCES [dbo].[MOD_HocKy] ([ID_moodle])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_LopTinChi_TC_MOD_HocKy]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_LopTinChi_TC]'))
ALTER TABLE [dbo].[MOD_LopTinChi_TC] CHECK CONSTRAINT [FK_MOD_LopTinChi_TC_MOD_HocKy]
GO
/****** Object:  ForeignKey [FK_MOD_NguoiDung_MOD_NhomNguoiDung]    Script Date: 12/29/2012 09:55:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NguoiDung_MOD_NhomNguoiDung]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung]'))
ALTER TABLE [dbo].[MOD_NguoiDung]  WITH CHECK ADD  CONSTRAINT [FK_MOD_NguoiDung_MOD_NhomNguoiDung] FOREIGN KEY([ID_nhom_nd])
REFERENCES [dbo].[MOD_NhomNguoiDung] ([ID_nhom_nd])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NguoiDung_MOD_NhomNguoiDung]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung]'))
ALTER TABLE [dbo].[MOD_NguoiDung] CHECK CONSTRAINT [FK_MOD_NguoiDung_MOD_NhomNguoiDung]
GO
/****** Object:  ForeignKey [FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_LopTinChi_TC]    Script Date: 12/29/2012 09:55:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_LopTinChi_TC]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_LopTinChi]'))
ALTER TABLE [dbo].[MOD_NguoiDung_VaiTro_LopTinChi]  WITH CHECK ADD  CONSTRAINT [FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_LopTinChi_TC] FOREIGN KEY([ID_lop_tc])
REFERENCES [dbo].[MOD_LopTinChi_TC] ([ID_moodle])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_LopTinChi_TC]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_LopTinChi]'))
ALTER TABLE [dbo].[MOD_NguoiDung_VaiTro_LopTinChi] CHECK CONSTRAINT [FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_LopTinChi_TC]
GO
/****** Object:  ForeignKey [FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_NguoiDung]    Script Date: 12/29/2012 09:55:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_NguoiDung]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_LopTinChi]'))
ALTER TABLE [dbo].[MOD_NguoiDung_VaiTro_LopTinChi]  WITH CHECK ADD  CONSTRAINT [FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_NguoiDung] FOREIGN KEY([UserID])
REFERENCES [dbo].[MOD_NguoiDung] ([ID_moodle])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_NguoiDung]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NguoiDung_VaiTro_LopTinChi]'))
ALTER TABLE [dbo].[MOD_NguoiDung_VaiTro_LopTinChi] CHECK CONSTRAINT [FK_MOD_NguoiDung_VaiTro_LopTinChi_MOD_NguoiDung]
GO
/****** Object:  ForeignKey [FK_MOD_NhomHocVien_MOD_LopTinChi_TC]    Script Date: 12/29/2012 09:55:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NhomHocVien_MOD_LopTinChi_TC]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NhomHocVien]'))
ALTER TABLE [dbo].[MOD_NhomHocVien]  WITH CHECK ADD  CONSTRAINT [FK_MOD_NhomHocVien_MOD_LopTinChi_TC] FOREIGN KEY([ID_lop_tc])
REFERENCES [dbo].[MOD_LopTinChi_TC] ([ID_moodle])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_NhomHocVien_MOD_LopTinChi_TC]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NhomHocVien]'))
ALTER TABLE [dbo].[MOD_NhomHocVien] CHECK CONSTRAINT [FK_MOD_NhomHocVien_MOD_LopTinChi_TC]
GO
/****** Object:  ForeignKey [FK_Nhom_To]    Script Date: 12/29/2012 09:55:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Nhom_To]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NhomHocVien]'))
ALTER TABLE [dbo].[MOD_NhomHocVien]  WITH CHECK ADD  CONSTRAINT [FK_Nhom_To] FOREIGN KEY([ID_to])
REFERENCES [dbo].[MOD_ToNhom] ([ID_to])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Nhom_To]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_NhomHocVien]'))
ALTER TABLE [dbo].[MOD_NhomHocVien] CHECK CONSTRAINT [FK_Nhom_To]
GO
/****** Object:  ForeignKey [FK_MOD_ToNhom_MOD_LopTinChi_TC]    Script Date: 12/29/2012 09:55:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_ToNhom_MOD_LopTinChi_TC]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_ToNhom]'))
ALTER TABLE [dbo].[MOD_ToNhom]  WITH CHECK ADD  CONSTRAINT [FK_MOD_ToNhom_MOD_LopTinChi_TC] FOREIGN KEY([ID_lop_tc])
REFERENCES [dbo].[MOD_LopTinChi_TC] ([ID_moodle])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MOD_ToNhom_MOD_LopTinChi_TC]') AND parent_object_id = OBJECT_ID(N'[dbo].[MOD_ToNhom]'))
ALTER TABLE [dbo].[MOD_ToNhom] CHECK CONSTRAINT [FK_MOD_ToNhom_MOD_LopTinChi_TC]
GO
