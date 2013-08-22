using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq.Expressions;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CongThongTinSV.Models;
using System.Web.Script.Serialization;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using CongThongTinSV.App_Lib;
using System.ComponentModel;

namespace CongThongTinSV.Controllers
{
    public class MoodleUserController : Controller
    {
        #region Student
        [Authorize(Roles = "MoodleUser.ManageStudent")]
        public ActionResult ManageStudent()
        {
            return View();
        }

        [Authorize(Roles = "MoodleUser.SearchStudent")]
        public ActionResult SearchStudent()
        {
            return View();
        }

        [Authorize(Roles = "MoodleUser.SearchStudent")]
        public ActionResult GetSearchStudents([DataSourceRequest] DataSourceRequest request, string id_chuyen_nganh)
        {
            return Json(MoodleLib.GetStudents(id_chuyen_nganh).Where(t => t.ID_moodle != 0).ToDataSourceResult(request));
        }

        [Authorize(Roles = "MoodleUser.ManageStudent")]
        public ActionResult GetStudents([DataSourceRequest] DataSourceRequest request, string id_chuyen_nganh)
        {
            return Json(MoodleLib.GetStudents(id_chuyen_nganh).ToDataSourceResult(request));
        }

        [Authorize(Roles = "MoodleUser.CreateStudents")]
        public ActionResult CreateStudents(string selectedVals, string id_chuyen_nganh)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetStudents(id_chuyen_nganh).Where(t => t.ID_moodle == 0 && s.Contains(t.ID_sv.ToString()));
            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.CreateStudents(list) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi tạo tài khoản sinh viên";
                    data.state = "error";
                }
                else
                {

                    data.title = "Success";
                    data.message = "Tạo tài khoản sinh viên thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Authorize(Roles = "MoodleUser.DeleteStudents")]
        public ActionResult DeleteStudents(string selectedVals, string id_chuyen_nganh)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetStudents(id_chuyen_nganh).Where(t => t.ID_moodle > 0 && s.Contains(t.ID_sv.ToString()));

            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.DeleteStudents(list) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi xóa tài khoản sinh viên";
                    data.state = "error";
                }
                else
                {

                    data.title = "Success";
                    data.message = "Xóa tài khoản sinh viên thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Authorize(Roles = "MoodleUser.UpdateStudents")]
        public ActionResult UpdateStudents(string selectedVals, string id_chuyen_nganh)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list =MoodleLib.GetStudents(id_chuyen_nganh).Where(t => t.ID_moodle > 0 && s.Contains(t.ID_sv.ToString()));

            var data = new Message();

            if (list.Count() != 0)
            {
                if (!MoodleLib.UpdateStudents(list))
                {
                    data.title = "Error";
                    data.message = "Lỗi khi cập nhật tài khoản sinh viên";
                    data.state = "error";
                }
                else
                {

                    data.title = "Success";
                    data.message = "Cập nhật tài khoản sinh viên thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Authorize(Roles = "MoodleUser.SyncStudents")]
        public ActionResult SyncStudents(string id_chuyen_nganh)
        {
            var list = MoodleLib.GetStudents(id_chuyen_nganh);//.Where(t => t.ID_moodle == 0);

            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.SyncStudents(list) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi đồng bộ tài khoản sinh viên";
                    data.state = "error";
                }
                else
                {

                    data.title = "Success";
                    data.message = "Đồng bộ tài khoản sinh viên thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Description("Xuất danh sách sinh viên ra excel")]
        [Authorize(Roles = "MoodleUser.ExportStudentToExcel")]
        public FileResult ExportStudentToExcel([DataSourceRequest]DataSourceRequest request, string id_chuyen_nganh, string chuyen_nganh, string datafields, string datatitles)
        {
            // Get data
            IEnumerable<MoodleStudent> students = MoodleLib.GetStudents(id_chuyen_nganh).ToDataSourceResult(request).Data.Cast<MoodleStudent>();
            string[] fields = datafields.Split(new char[] { ',' });
            string[] titles = datatitles.Split(new char[] { ',' });
            int len = fields.Count();
            int startRow = 1, startColumn = 1;
            //Init workbook
            var workbook = new ExcelExportor(GlobalLib.GetExcelTemplateFolderPath() + "Students", "", "Danh sách sinh viên");
            //Set header
            workbook.SetCellValue(chuyen_nganh);
            workbook.SetFontBold();
            workbook.SetFontSize(14);
            //workbook.SetRowHeight(20);
            workbook.ExpandCellToRange(startRow, startColumn, 1, len);
            workbook.MergeColumns(len);
            workbook.SetHorizontalAlignment(Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter);
            //workbook.SetVerticalAlignment();
            //workbook.SetBoderLineStyles();
            //Set title
            startRow++;
            workbook.Set1DArrayValue(titles.ToArray(), false, startRow, startColumn);
            workbook.SetFreezePanes(true);
            workbook.SetFontBold(true);
            //workbook.SetColumnWidth();
            workbook.SetHorizontalAlignment(Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter);
            //workbook.SetBoderLineStyles();

            //Set data
            startRow++;
            for (int i = 0; i < len; i++)
            {
                if (fields[i] == "Ma_sv")
                {
                    workbook.Set1DArrayValue(students.Select(t => t.Ma_sv).ToArray(), true, startRow, i + 1);
                    //workbook.SetColumnWidth(12);
                    workbook.SetHorizontalAlignment(Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter);
                    //workbook.SetBoderLineStyles();
                }
                else if (fields[i] == "Ho_dem")
                {
                    workbook.Set1DArrayValue(students.Select(t => t.Ho_dem).ToArray(), true, startRow, i + 1);
                    //workbook.SetColumnWidth();
                    //workbook.SetBoderLineStyles();
                }
                else if (fields[i] == "Ten")
                {
                    workbook.Set1DArrayValue(students.Select(t => t.Ten).ToArray(), true, startRow, i + 1);
                    //workbook.SetColumnWidth();
                    //workbook.SetBoderLineStyles();
                }
                else if (fields[i] == "Ngay_sinh")
                {
                    workbook.Set1DArrayValue(students.Select(t => t.Ngay_sinh).Cast<object>().ToArray(), true, startRow, i + 1);
                    workbook.SetNumberFormat("dd-MM-yyyy");
                    //workbook.SetColumnWidth();
                    workbook.SetHorizontalAlignment(Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter);
                    //workbook.SetBoderLineStyles();
                }
                else if (fields[i] == "Gioi_tinh")
                {
                    workbook.Set1DArrayValue(students.Select(t => t.Gioi_tinh).ToArray(), true, startRow, i + 1);
                    //workbook.SetColumnWidth();
                    //workbook.SetBoderLineStyles();
                }
                else if (fields[i] == "Lop")
                {
                    workbook.Set1DArrayValue(students.Select(t => t.Lop).ToArray(), true, startRow, i + 1);
                    //workbook.SetColumnWidth();
                    //workbook.SetBoderLineStyles();
                }
            }

            //format entire workbook
            workbook.ExpandCellToRange(1, 1, startRow + students.Count() - 1, len);
            workbook.SetColumnWidth();
            workbook.SetRowHeight();
            workbook.SetVerticalAlignment();
            workbook.SetBoderLineStyles();

            //Save workbook
            workbook.SaveAs();

            //Return the result to the end user

            return File(workbook.GetByteArray(),    //The binary data of the XLS file
                "application/vnd.ms-excel",         //MIME type of Excel files
                workbook.ExportSheetName + " " + chuyen_nganh);          //Suggested file name in the "Save as" dialog which will be displayed to the end user
        }
        #endregion

        #region Teacher
        [Description("Tìm kiếm tài khoản moodle giảng viên")]
        [Authorize(Roles = "MoodleUser.SearchTeacher")]
        public ActionResult SearchTeacher()
        {
            return View();
        }

        [Authorize(Roles = "MoodleUser.SearchTeacher")]
        public ActionResult GetSearchTeachers([DataSourceRequest] DataSourceRequest request, string id_khoa)
        {

            return Json(MoodleLib.GetTeachers(id_khoa).Where(t => t.ID_moodle != 0).ToDataSourceResult(request));
        }

        [Authorize(Roles = "MoodleUser.ManageTeacher")]
        public ActionResult ManageTeacher()
        {
            return View();
        }

        [Authorize(Roles = "MoodleUser.ManageTeacher")]
        public ActionResult GetTeachers([DataSourceRequest] DataSourceRequest request, string id_khoa)
        {

            return Json(MoodleLib.GetTeachers(id_khoa).ToDataSourceResult(request));
        }

        [Authorize(Roles = "MoodleUser.CreateTeachers")]
        public ActionResult CreateTeachers(string selectedVals, string id_khoa)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });

            var list = MoodleLib.GetTeachers(id_khoa).Where(t => t.ID_moodle == 0 && s.Contains(t.ID_cb.ToString()));

            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.CreateTeachers(list) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi tạo tài khoản giảng viên";
                    data.state = "error";
                }
                else
                {

                    data.title = "Success";
                    data.message = "Tạo tài khoản giảng viên thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Authorize(Roles = "MoodleUser.DeleteTeachers")]
        public ActionResult DeleteTeachers(string selectedVals, string id_khoa)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });

            var list = MoodleLib.GetTeachers(id_khoa).Where(t => t.ID_moodle > 0 && s.Contains(t.ID_cb.ToString()));
            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.DeleteTeachers(list) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi xóa tài khoản giảng viên";
                    data.state = "error";
                }
                else
                {

                    data.title = "Success";
                    data.message = "Xóa tài khoản giảng viên thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Authorize(Roles = "MoodleUser.UpdateTeachers")]
        public ActionResult UpdateTeachers(string selectedVals, string id_khoa)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });

            var list = MoodleLib.GetTeachers(id_khoa).Where(t => t.ID_moodle > 0 && s.Contains(t.ID_cb.ToString()));

            var data = new Message();

            if (list.Count() != 0)
            {
                if (!MoodleLib.UpdateTeachers(list))
                {
                    data.title = "Error";
                    data.message = "Lỗi khi cập nhật tài khoản giảng viên";
                    data.state = "error";
                }
                else
                {

                    data.title = "Success";
                    data.message = "Cập nhật tài khoản giảng viên thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Authorize(Roles = "MoodleUser.SyncTeachers")]
        public ActionResult SyncTeachers(string id_khoa)
        {
            var list = MoodleLib.GetTeachers(id_khoa).Where(t => t.ID_moodle == 0);

            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.SyncTeachers(list) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi đồng bộ tài khoản giảng viên";
                    data.state = "error";
                }
                else
                {

                    data.title = "Success";
                    data.message = "Đồng bộ tài khoản giảng viên thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Authorize(Roles = "MoodleUser.AssignTeacherSystemRole")]
        public ActionResult AssignTeacherSystemRole(string selectedVals, string id_khoa, string id_vai_tro)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetTeachers(id_khoa);
            list = list.Where(t => t.ID_moodle == 0 && s.Contains(t.ID_cb.ToString()));

            if (list.Count() != 0)
            {
                MoodleLib.CreateTeachers(list);
            }

            list = MoodleLib.GetTeachers(id_khoa);
            list = list.Where(t => t.ID_moodle > 0 && s.Contains(t.ID_cb.ToString()));
            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.AssignTeacherSystemRole(list, id_vai_tro) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi gán quyền hệ thống cho giảng viên";
                    data.state = "error";
                }
                else
                {

                    data.title = "Success";
                    data.message = "Gán quyền hệ thống cho giảng viên thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Authorize(Roles = "MoodleUser.UnassignTeacherSystemRole")]
        public ActionResult UnassignTeacherSystemRole(string selectedVals, string id_khoa, string id_vai_tro)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetTeachers(id_khoa).Where(t => s.Contains(t.ID_cb.ToString()) && Utility.InArray(t.ID_vai_tro, new char[] { ',' }, id_vai_tro));

            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.UnassignTeacherSystemRole(list, id_vai_tro) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi hủy quyền hệ thống của giảng viên";
                    data.state = "error";
                }
                else
                {

                    data.title = "Success";
                    data.message = "Hủy quyền hệ thống của giảng viên thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Authorize(Roles = "MoodleUser.UnassignTeacherAllSystemRoles")]
        public ActionResult UnassignTeacherAllSystemRoles(string selectedVals, string id_khoa)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetTeachers(id_khoa).Where(t => s.Contains(t.ID_cb.ToString()) && t.ID_vai_tro != "");

            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.UnassignTeacherAllSystemRoles(list) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi hủy tất cả quyền hệ thống của giảng viên";
                    data.state = "error";
                }
                else
                {

                    data.title = "Success";
                    data.message = "Hủy tất cả quyền hệ thống của giảng viên thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Description("Xuất danh sách giảng viên ra excel")]
        [Authorize(Roles = "MoodleUser.ExportTeacherToExcel")]
        public FileResult ExportTeacherToExcel([DataSourceRequest]DataSourceRequest request, string id_khoa, string khoa, string datafields, string datatitles)
        {
            // Get data
            IEnumerable<MoodleTeacher> teachers = MoodleLib.GetTeachers(id_khoa).ToDataSourceResult(request).Data.Cast<MoodleTeacher>();
            string[] fields = datafields.Split(new char[] { ',' });
            string[] titles = datatitles.Split(new char[] { ',' });
            int len = fields.Count();
            int startRow = 1, startColumn = 1;
            //Init workbook
            var workbook = new ExcelExportor(GlobalLib.GetExcelTemplateFolderPath() + "Teachers", "", "Danh sách giảng viên");
            //Set header
            workbook.SetCellValue(khoa);
            workbook.SetFontBold();
            workbook.SetFontSize(14);
            //workbook.SetRowHeight(20);
            workbook.ExpandCellToRange(startRow, startColumn, 1, len);
            workbook.MergeColumns(len);
            workbook.SetHorizontalAlignment(Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter);
            //workbook.SetVerticalAlignment();
            //workbook.SetBoderLineStyles();
            //Set title
            startRow++;
            workbook.Set1DArrayValue(titles.ToArray(), false, startRow, startColumn);
            workbook.SetFreezePanes(true);
            workbook.SetFontBold(true);
            //workbook.SetColumnWidth();
            workbook.SetHorizontalAlignment(Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter);
            //workbook.SetBoderLineStyles();

            //Set data
            startRow++;
            for (int i = 0; i < len; i++)
            {
                if (fields[i] == "Ma_cb")
                {
                    workbook.Set1DArrayValue(teachers.Select(t => t.Ma_cb).ToArray(), true, startRow, i + 1);
                    //workbook.SetColumnWidth(12);
                    workbook.SetHorizontalAlignment(Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter);
                    //workbook.SetBoderLineStyles();
                }
                else if (fields[i] == "Ho_dem")
                {
                    workbook.Set1DArrayValue(teachers.Select(t => t.Ho_dem).ToArray(), true, startRow, i + 1);
                    //workbook.SetColumnWidth();
                    //workbook.SetBoderLineStyles();
                }
                else if (fields[i] == "Ten")
                {
                    workbook.Set1DArrayValue(teachers.Select(t => t.Ten).ToArray(), true, startRow, i + 1);
                    //workbook.SetColumnWidth();
                    //workbook.SetBoderLineStyles();
                }
                else if (fields[i] == "Ngay_sinh")
                {
                    workbook.Set1DArrayValue(teachers.Select(t => t.Ngay_sinh).Cast<object>().ToArray(), true, startRow, i + 1);
                    workbook.SetNumberFormat("dd-MM-yyyy");
                    //workbook.SetColumnWidth();
                    workbook.SetHorizontalAlignment(Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter);
                    //workbook.SetBoderLineStyles();
                }
                else if (fields[i] == "Gioi_tinh")
                {
                    workbook.Set1DArrayValue(teachers.Select(t => t.Gioi_tinh).ToArray(), true, startRow, i + 1);
                    //workbook.SetColumnWidth();
                    //workbook.SetBoderLineStyles();
                }
                else if (fields[i] == "Vai_tro")
                {
                    workbook.Set1DArrayValue(teachers.Select(t => t.Vai_tro).ToArray(), true, startRow, i + 1);
                    //workbook.SetColumnWidth();
                    //workbook.SetBoderLineStyles();
                }
            }

            //format entire workbook
            workbook.ExpandCellToRange(1, 1, startRow + teachers.Count() - 1, len);
            workbook.SetColumnWidth();
            workbook.SetRowHeight();
            workbook.SetVerticalAlignment();
            workbook.SetBoderLineStyles();

            //Save workbook
            workbook.SaveAs();

            //Return the result to the end user

            return File(workbook.GetByteArray(),    //The binary data of the XLS file
                "application/vnd.ms-excel",         //MIME type of Excel files
                workbook.ExportSheetName + " " + khoa);          //Suggested file name in the "Save as" dialog which will be displayed to the end user
        }
        #endregion

        #region AdminUser
        [Authorize(Roles = "MoodleUser.ManageAdminUser")]
        public ActionResult ManageAdminUser()
        {
            return View();
        }

        [Authorize(Roles = "MoodleUser.ManageAdminUser")]
        public ActionResult GetAdminUsers([DataSourceRequest] DataSourceRequest request)
        {
            return Json(MoodleLib.GetAdminUsers().ToDataSourceResult(request));
        }

        [Authorize(Roles = "MoodleUser.CreateAdminUsers")]
        public ActionResult CreateAdminUsers(string selectedVals)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });

            var list = MoodleLib.GetAdminUsers().Where(t => t.ID_moodle == 0 && s.Contains(t.ID.ToString()));
            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.CreateAdminUsers(list) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi tạo tài khoản quản trị viên";
                    data.state = "error";
                }
                else
                {

                    data.title = "Success";
                    data.message = "Tạo tài khoản quản trị viên thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Authorize(Roles = "MoodleUser.DeleteAdminUsers")]
        public ActionResult DeleteAdminUsers(string selectedVals)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });

            var list = MoodleLib.GetAdminUsers().Where(t => t.ID_moodle > 0 && s.Contains(t.ID.ToString()));
            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.DeleteAdminUsers(list) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi xóa tài khoản quản trị viên";
                    data.state = "error";
                }
                else
                {

                    data.title = "Success";
                    data.message = "Xóa tài khoản quản trị viên thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Authorize(Roles = "MoodleUser.UpdateAdminUsers")]
        public ActionResult UpdateAdminUsers(string selectedVals)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });

            var list = MoodleLib.GetAdminUsers().Where(t => t.ID_moodle > 0 && s.Contains(t.ID.ToString()));
            var data = new Message();

            if (list.Count() != 0)
            {
                if (!MoodleLib.UpdateAdminUsers(list))
                {
                    data.title = "Error";
                    data.message = "Lỗi khi cập nhật tài khoản quản trị viên";
                    data.state = "error";
                }
                else
                {

                    data.title = "Success";
                    data.message = "Cập nhật tài khoản quản trị viên thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Authorize(Roles = "MoodleUser.SyncAdminUsers")]
        public ActionResult SyncAdminUsers()
        {
            var list = MoodleLib.GetAdminUsers().Where(t => t.ID_moodle == 0);
            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.SyncAdminUsers(list) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi đồng bộ tài khoản quản trị viên";
                    data.state = "error";
                }
                else
                {

                    data.title = "Success";
                    data.message = "Đồng bộ tài khoản quản trị viên thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }


        [Authorize(Roles = "MoodleUser.AssignAdminUserSystemRole")]
        public ActionResult AssignAdminUserSystemRole(string selectedVals, string id_vai_tro)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetAdminUsers();
            list = list.Where(t => t.ID_moodle == 0 && s.Contains(t.ID.ToString()));

            if (list.Count() != 0)
            {
                MoodleLib.CreateAdminUsers(list);
            }

            list = MoodleLib.GetAdminUsers();
            list = list.Where(t => t.ID_moodle > 0 && s.Contains(t.ID.ToString()));
            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.AssignAdminUserSystemRole(list, id_vai_tro) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi gán quyền hệ thống cho quản trị viên";
                    data.state = "error";
                }
                else
                {

                    data.title = "Success";
                    data.message = "Gán quyền hệ thống cho quản trị viên thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Authorize(Roles = "MoodleUser.UnassignAdminUserSystemRole")]
        public ActionResult UnassignAdminUserSystemRole(string selectedVals, string id_vai_tro)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetAdminUsers().Where(t => s.Contains(t.ID.ToString()) && Utility.InArray(t.ID_vai_tro, new char[] { ',' }, id_vai_tro));
            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.UnassignAdminUserSystemRole(list, id_vai_tro) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi hủy quyền hệ thống của quản trị viên";
                    data.state = "error";
                }
                else
                {

                    data.title = "Success";
                    data.message = "Hủy quyền hệ thống của quản trị viên thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Authorize(Roles = "MoodleUser.UnassignAdminUserAllSystemRoles")]
        public ActionResult UnassignAdminUserAllSystemRoles(string selectedVals)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetAdminUsers().Where(t => s.Contains(t.ID.ToString()) && t.ID_vai_tro != "");

            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.UnassignAdminUserAllSystemRoles(list) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi hủy tất cả quyền hệ thống của quản trị viên";
                    data.state = "error";
                }
                else
                {

                    data.title = "Success";
                    data.message = "Hủy tất cả quyền hệ thống của quản trị viên thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }
        #endregion

        #region UserProfile
        [Authorize(Roles = "MoodleUser.ChangePassword")]
        public ActionResult ChangePassword(string message)
        {
            ViewBag.StatusMessage = message;
            ViewBag.ReturnUrl = Url.Action("ChangePassword");
            return View();
        }

        [Authorize(Roles = "MoodleUser.ChangePassword")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(LocalPasswordModel model)
        {
            ViewBag.ReturnUrl = Url.Action("ChangePassword");

            if (ModelState.IsValid)
            {
                // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    UserData userData = GlobalLib.GetCurrentUserData();

                    if(MoodleLib.GetToken(userData.UserName, model.OldPassword, userData.MoodleService) == userData.MoodleToken)
                    {
                        List<MoodleUser> list = new List<MoodleUser>();
                        list.Add(new MoodleUser
                        {
                            ID = Convert.ToInt32(userData.MoodleUserID),
                            Password = model.NewPassword
                        });
                        changePasswordSucceeded = MoodleLib.UpdateUser(list);
                    }
                    else
                    {
                         changePasswordSucceeded = false;
                    }
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePassword", new { Message = "Bạn đã đổi mật khẩu thành công." });
                }
                else
                {
                    ModelState.AddModelError("","Bạn nhập sai mật khẩu cũ hoặc mật khẩu mới không hợp lệ.");
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [Authorize(Roles = "MoodleUser.MyProfile")]
        public ActionResult MyProfile()
        {
            List<string> list = new List<string>();
            string userid = GlobalLib.GetCurrentUserData().MoodleUserID.ToString();
            list.Add(userid);

            var q = MoodleLib.GetUserByID(list);

            if (q.Count() > 0)
                ViewBag.UserProfile = q.ElementAt(0);
            else
                ViewBag.UserProfile = new MoodleUserResponse();

            return View();
        }

        [Authorize(Roles = "MoodleUser.MyCourseProfile")]
        public ActionResult MyCourseProfile(string courseid="0")
        {
            UserData userData = GlobalLib.GetCurrentUserData();
            string userid = userData.MoodleUserID.ToString();

            if (!MoodleLib.IsUserInCourse(userid, courseid))
            {
                ViewBag.Error = "Bạn chưa được ghi danh vào khóa học này!";
            }
            else
            {
                ViewBag.Error = "";
                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                list.Add(new KeyValuePair<string, string>(userid, courseid));
                var q = MoodleLib.GetCourseUserProfiles(list);

                if (q.Count() > 0)
                {
                    ViewBag.CourseUser = q.ElementAt(0);
                }
                else
                {
                    ViewBag.CourseUser = new MoodleCourseUserResponse();
                }
            }

            var course = MoodleLib.GetCourseByID(courseid);
            ViewBag.CourseName = course == null ? "" : course.fullname;
            ViewBag.FullName = userData.MoodleFullName;

            return View();
        }

        [Authorize(Roles = "MoodleUser.UserProfile")]
        public ActionResult UserProfile(string userid="0")
        {
            List<string> list = new List<string>();
            list.Add(userid);
            var q = MoodleLib.GetUserByID(list);

            if (q.Count() > 0)
                ViewBag.UserProfile = q.ElementAt(0);
            else
                ViewBag.UserProfile = new MoodleUserResponse();

            return View();
        }

        [Authorize(Roles = "MoodleUser.UserCourseProfile")]
        public ActionResult UserCourseProfile(string userid="0", string courseid="0")
        {
            if (!MoodleLib.IsUserInCourse(userid, courseid))
            {
                ViewBag.Error = "Người dùng chưa được ghi danh vào khóa học này!";
            }
            else
            {
                ViewBag.Error = "";
                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                list.Add(new KeyValuePair<string, string>(userid, courseid));
                var q = MoodleLib.GetCourseUserProfiles(list);

                if (q.Count() > 0)
                {
                    ViewBag.CourseUser = q.ElementAt(0);
                }
                else
                {
                    ViewBag.CourseUser = new MoodleCourseUserResponse();
                }
            }

            var course = MoodleLib.GetCourseByID(courseid);
            ViewBag.CourseName = course == null ? "" : course.fullname;
            ViewBag.FullName = MoodleLib.GetUserFullNameByID(userid);
            ViewBag.UserID = userid;

            return View();
        }
        #endregion
    }
}
