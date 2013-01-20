using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongThongTinSV.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Web.Script.Serialization;
using CongThongTinSV.App_Lib;
using System.ComponentModel;

namespace CongThongTinSV.Controllers
{
    public class MoodleEnrolController : Controller
    {
        #region Student
        [Authorize(Roles = "MoodleEnrol.EnrolStudent")]
        public ActionResult EnrolStudent()
        {
            return View();
        }

        [Authorize(Roles = "MoodleEnrol.EnrolStudent")]
        public ActionResult GetEnrolStudents([DataSourceRequest] DataSourceRequest request, string id_lop_tc)
        {
            return Json(MoodleLib.GetEnrolStudentXGrades(id_lop_tc).ToDataSourceResult(request));
        }

        [Authorize(Roles = "MoodleEnrol.ManualEnrolStudents")]
        public ActionResult ManualEnrolStudents(string selectedVals, string id_lop_tc)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetEnrolStudentXGrades(id_lop_tc);
            list = list.Where(t => t.ID_moodle == 0 && s.Contains(t.ID.ToString()));

            if (list.Count() != 0)
            {
                MoodleLib.CreateStudents(list);
            }

            list = MoodleLib.GetEnrolStudentXGrades(id_lop_tc);
            list = list.Where(t => t.ID_moodle != 0 && !t.Trang_thai && s.Contains(t.ID.ToString()));
            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.ManualEnrolStudents(list) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi ghi danh cho sinh viên";
                    data.state = "error";
                }
                else
                {
                    data.title = "Success";
                    data.message = "Ghi danh cho sinh viên thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Authorize(Roles = "MoodleEnrol.SuspendEnrolStudents")]
        public ActionResult SuspendEnrolStudents(string selectedVals, string id_lop_tc)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetEnrolStudents(id_lop_tc).Where(t => t.Trang_thai && s.Contains(t.ID.ToString()));
            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.SuspendEnrolStudents(list) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi đình chỉ ghi danh của sinh viên";
                    data.state = "error";
                }
                else
                {
                    data.title = "Success";
                    data.message = "Đình chỉ ghi danh của sinh viên thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Description("Xuất danh sách sinh viên thuộc lớp học phần ra excel")]
        [Authorize(Roles = "MoodleEnrol.ExportEnrolStudentToExcel")]
        public FileResult ExportEnrolStudentToExcel([DataSourceRequest]DataSourceRequest request, string id_lop_tc, string ten_lop, string datafields, string datatitles)
        {
            // Get data
            IEnumerable<MoodleStudent> students = MoodleLib.GetEnrolStudents(id_lop_tc).ToDataSourceResult(request).Data.Cast<MoodleStudent>();
            
            string[] fields = datafields.Split(new char[] { ',' });
            string[] titles = datatitles.Split(new char[] { ',' });
            int len = fields.Count();
            int startRow = 1, startColumn = 1;
            //Init workbook
            var workbook = new ExcelExportor(GlobalLib.GetExcelTemplateFolderPath() + "Students", "", "Danh sách sinh viên lớp");
            //Set header
            workbook.SetCellValue(ten_lop);
            workbook.SetFontBold();
            workbook.SetFontSize(14);
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
                else if (fields[i] == "DiemX")
                {
                    workbook.Set1DArrayValue(students.Select(t => t.DiemX.ToString()).ToArray(), true, startRow, i + 1);
                    workbook.SetHorizontalAlignment(Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter);
                    workbook.SetNumberFormat("0.0");
                    //workbook.SetBoderLineStyles();
                }
                else if (fields[i] == "Ten_nhom")
                {
                    workbook.Set1DArrayValue(students.Select(t => t.Ten_nhom).ToArray(), true, startRow, i + 1);
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
                workbook.ExportSheetName + " " + ten_lop);          //Suggested file name in the "Save as" dialog which will be displayed to the end user
        }
        #endregion

        #region Teacher
        [Authorize(Roles = "MoodleEnrol.EnrolTeacher")]
        public ActionResult EnrolTeacher()
        {
            return View();
        }

        //[Authorize(Roles = "MoodleEnrol.GetEnrolTeachers")]
        public ActionResult GetEnrolTeachers([DataSourceRequest] DataSourceRequest request, string id_lop_tc)
        {
            return Json(MoodleLib.GetEnrolTeachers(id_lop_tc).ToDataSourceResult(request));
        }

        [Authorize(Roles = "MoodleEnrol.ManualEnrolTeachers")]
        public ActionResult ManualEnrolTeachers(string selectedVals, string id_lop_tc, string id_vai_tro, string suspended)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetEnrolTeachers(id_lop_tc);
            list = list.Where(t => t.ID_moodle == 0 && s.Contains(t.ID_cb.ToString()));

            if (list.Count() != 0)
            {
                MoodleLib.CreateTeachers(list);
            }

            list = MoodleLib.GetEnrolTeachers(id_lop_tc);
            list = list.Where(t => t.ID_moodle > 0 && s.Contains(t.ID_cb.ToString()));
            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.ManualEnrolTeachers(list, id_vai_tro, suspended) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi " + (suspended == "0" ? "ghi danh cho " : "đình chỉ ghi danh của ") + "giảng viên";
                    data.state = "error";
                }
                else
                {
                    data.title = "Success";
                    data.message = (suspended == "0" ? "Ghi danh cho " : "Đình chỉ ghi danh của ") + "giảng viên thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Authorize(Roles = "MoodleEnrol.UnassignTeacherRole")]
        public ActionResult UnassignTeacherRole(string selectedVals, string id_lop_tc, string id_vai_tro)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetEnrolTeachers(id_lop_tc).Where(t => s.Contains(t.ID_cb.ToString()) && Utility.InArray(t.ID_vai_tro, new char[] { ',' }, id_vai_tro));
            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.UnassignTeacherRole(list, id_vai_tro) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi hủy vai trò của giảng viên";
                    data.state = "error";
                }
                else
                {
                    data.title = "Success";
                    data.message = "Hủy vai trò của giảng viên thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Authorize(Roles = "MoodleEnrol.UnassignTeacherAllRoles")]
        public ActionResult UnassignTeacherAllRoles(string selectedVals, string id_lop_tc)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetEnrolTeachers(id_lop_tc).Where(t => s.Contains(t.ID_cb.ToString()) && t.ID_vai_tro != "");
            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.UnassignTeacherAllRoles(list) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi hủy tất cả vai trò của giảng viên";
                    data.state = "error";
                }
                else
                {
                    data.title = "Success";
                    data.message = "Hủy tất cả vai trò của giảng viên thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }
        #endregion

        #region AdminUser
        [Authorize(Roles = "MoodleEnrol.EnrolAdminUser")]
        public ActionResult EnrolAdminUser()
        {
            return View();
        }

        //[Authorize(Roles = "MoodleEnrol.GetEnrolAdminUsers")]
        public ActionResult GetEnrolAdminUsers([DataSourceRequest] DataSourceRequest request, string id_lop_tc)
        {
            return Json(MoodleLib.GetEnrolAdminUsers(id_lop_tc).ToDataSourceResult(request));
        }

        [Authorize(Roles = "MoodleEnrol.ManualEnrolAdminUsers")]
        public ActionResult ManualEnrolAdminUsers(string selectedVals, string id_lop_tc, string id_vai_tro, string suspended)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetEnrolAdminUsers(id_lop_tc);
            list = list.Where(t => t.ID_moodle == 0 && s.Contains(t.ID.ToString()));

            if (list.Count() != 0)
            {
                MoodleLib.CreateAdminUsers(list);
            }

            list = MoodleLib.GetEnrolAdminUsers(id_lop_tc);
            list = list.Where(t => t.ID_moodle > 0 && s.Contains(t.ID.ToString()));
            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.ManualEnrolAdminUsers(list, id_vai_tro, suspended) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi " + (suspended == "0" ? "ghi danh cho " : "đình chỉ ghi danh của ") + "quản trị viên";
                    data.state = "error";
                }
                else
                {
                    data.title = "Success";
                    data.message = (suspended == "0" ? "Ghi danh cho " : "Đình chỉ ghi danh của ") + "quản trị viên thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Authorize(Roles = "MoodleEnrol.UnassignAdminUserRole")]
        public ActionResult UnassignAdminUserRole(string selectedVals, string id_lop_tc, string id_vai_tro)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetEnrolAdminUsers(id_lop_tc).Where(t => s.Contains(t.ID.ToString()) && Utility.InArray(t.ID_vai_tro, new char[] { ',' }, id_vai_tro));
            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.UnassignAdminUserRole(list, id_vai_tro) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi hủy vai trò của quản trị viên";
                    data.state = "error";
                }
                else
                {
                    data.title = "Success";
                    data.message = "Hủy vai trò của quản trị viên thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Authorize(Roles = "MoodleEnrol.UnassignAdminUsersAllRoles")]
        public ActionResult UnassignAdminUserAllRoles(string selectedVals, string id_lop_tc)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetEnrolAdminUsers(id_lop_tc).Where(t => s.Contains(t.ID.ToString()) && t.ID_vai_tro != "");

            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.UnassignAdminUserAllRoles(list) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi hủy tất cả vai trò của quản trị viên";
                    data.state = "error";
                }
                else
                {
                    data.title = "Success";
                    data.message = "Hủy tất cả vai trò của quản trị viên thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }
        #endregion
    }
}
