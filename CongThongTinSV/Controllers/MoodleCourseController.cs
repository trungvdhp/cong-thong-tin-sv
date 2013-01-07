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
    public class MoodleCourseController : Controller
    {
        [Description("Quản lý khóa học trên moodle")]
        [Authorize(Roles = "MoodleCourse.Manage")]
        public ActionResult Manage()
        {
            return View();
        }

        [Authorize(Roles = "MoodleCourse.Manage")]
        public ActionResult GetCourses([DataSourceRequest] DataSourceRequest request, string id_hocky)
        {
            return Json(MoodleLib.GetCourses(id_hocky).ToDataSourceResult(request));
        }

        [Authorize(Roles = "MoodleCourse.Manage")]
        public JsonResult GetCourseList(string id_hocky)
        {
            JsonResult result = new JsonResult();
            result.Data = MoodleLib.GetCourseList(id_hocky);
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }

        [Description("Tạo các khóa học trên moodle")]
        [Authorize(Roles = "MoodleCourse.CreateCourses")]
        public ActionResult CreateCourses(string selectedVals, string id_hocky)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetCourses(id_hocky).Where(t => t.ID_moodle == 0 && s.Contains(t.ID.ToString()));

            if (list.Count() != 0)
            {
                MoodleLib.CreateCourses(list);
            }

            return View();
        }

        [Description("Xóa các khóa học trên moodle")]
        [Authorize(Roles = "MoodleCourse.DeleteCourses")]
        public ActionResult DeleteCourses(string selectedVals, string id_hocky)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetCourses(id_hocky).Where(t => t.ID_moodle > 0 && s.Contains(t.ID.ToString()));

            if (list.Count() != 0)
            {
                 MoodleLib.DeleteCourses(list);
            }
           
            return View();
        }

        [Description("Xem bảng điểm tổng kết khóa học tôi được ghi danh")]
        [Authorize(Roles = "MoodleCourse.MyCourseStudentGrade")]
        public ActionResult MyCourseStudentGrade(string courseid = "0")
        {
            string userid = GlobalLib.GetCurrentUserData().MoodleUserID.ToString();

            if (!MoodleLib.IsUserInCourse(userid, courseid))
            {
                ViewBag.Error = "Bạn chưa được ghi danh vào khóa học này!";
            }
            else
            {
                ViewBag.Error = "";
            }

            var course = MoodleLib.GetCourseByID(courseid);
            ViewBag.CourseName = course == null ? "" : course.fullname;
            ViewBag.CourseID = courseid;

            return View();
        }

        [Authorize(Roles = "MoodleCourse.MyCourseStudentGrade")]
        public ActionResult GetMyCourseStudentGrades([DataSourceRequest] DataSourceRequest request, string courseid = "0")
        {
            return Json(MoodleLib.GetCourseStudentGrades(courseid).ToDataSourceResult(request));
        }

        [Description("Xem bảng điểm tổng kết của tất cả các khóa học")]
        [Authorize(Roles = "MoodleCourse.CourseStudentGrade")]
        public ActionResult CourseStudentGrade(string courseid = "0")
        {
            var course = MoodleLib.GetCourseByID(courseid);
            ViewBag.CourseName = course == null ? "" : course.fullname;
            ViewBag.CourseID = courseid;

            return View();
        }

        [Authorize(Roles = "MoodleCourse.CourseStudentGrade")]
        public ActionResult GetCourseStudentGrades([DataSourceRequest] DataSourceRequest request, string courseid = "0")
        {
            return Json(MoodleLib.GetCourseStudentGrades(courseid).ToDataSourceResult(request));
        }

        [Description("Xuất bảng điểm tổng kết khóa học ra excel")]
        [Authorize(Roles = "MoodleCourse.ExportCourseGradeToExcel")]
        public FileResult ExportCourseGradeToExcel([DataSourceRequest]DataSourceRequest request, string courseid, string coursename, string datafields, string datatitles)
        {
            // Get data
            IEnumerable<MoodleCourseStudentGrade> grades = MoodleLib.GetCourseStudentGrades(courseid).ToDataSourceResult(request).Data.Cast<MoodleCourseStudentGrade>();

            string[] fields = datafields.Split(new char[] { ',' });
            string[] titles = datatitles.Split(new char[] { ',' });
            int len = fields.Count();
            int startRow = 1, startColumn = 1;
            //Init workbook
            var workbook = new ExcelExportor(GlobalLib.GetExcelTemplateFolderPath() + "CourseGrades", "", "Bảng điểm tổng kết");
            //Set header
            workbook.SetCellValue(coursename);
            workbook.SetFontBold();
            workbook.SetFontSize(14);
            //workbook.ExpandCellToRange(startRow, startColumn, 1, len);
            //workbook.MergeColumns(len);
            //workbook.SetHorizontalAlignment(Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter);
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
                if (fields[i] == "UserName")
                {
                    workbook.Set1DArrayValue(grades.Select(t => t.UserName).ToArray(), true, startRow, i + 1);
                    //workbook.SetColumnWidth(12);
                    workbook.SetHorizontalAlignment(Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter);
                    //workbook.SetBoderLineStyles();
                }
                else if (fields[i] == "FirstName")
                {
                    workbook.Set1DArrayValue(grades.Select(t => t.FirstName).ToArray(), true, startRow, i + 1);
                    //workbook.SetColumnWidth();
                    //workbook.SetBoderLineStyles();
                }
                else if (fields[i] == "LastName")
                {
                    workbook.Set1DArrayValue(grades.Select(t => t.LastName).ToArray(), true, startRow, i + 1);
                    //workbook.SetColumnWidth();
                    //workbook.SetBoderLineStyles();
                }
                else if (fields[i] == "ZGrade")
                {
                    workbook.Set1DArrayValue(grades.Select(t => t.ZGrade.ToString()).ToArray(), true, startRow, i + 1);
                    workbook.SetHorizontalAlignment(Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter);
                    workbook.SetNumberFormat("0.0");
                    //workbook.SetBoderLineStyles();
                }
            }

            //format entire workbook

            workbook.ExpandCellToRange(2, 1, startRow + grades.Count() - 2, len);
            workbook.SetColumnWidth();
            workbook.SetRowHeight();
            workbook.SetVerticalAlignment();
            workbook.SetBoderLineStyles();

            //Save workbook
            workbook.SaveAs();

            //Return the result to the end user

            return File(workbook.GetByteArray(),    //The binary data of the XLS file
                "application/vnd.ms-excel",         //MIME type of Excel files
                workbook.ExportSheetName + " " + coursename);          //Suggested file name in the "Save as" dialog which will be displayed to the end user
        }

        [Description("Xem bảng điểm các khóa học tôi được ghi danh")]
        [Authorize(Roles = "MoodleCourse.MyCourseGrade")]
        public ActionResult MyCourseGrade()
        {
            ViewBag.FullName = GlobalLib.GetCurrentUserData().MoodleFullName;
            return View();
        }

        [Authorize(Roles = "MoodleCourse.MyCourseGrade")]
        public ActionResult GetMyCourseGrades([DataSourceRequest] DataSourceRequest request)
        {
            string userid = GlobalLib.GetCurrentUserData().MoodleUserID.ToString();

            return Json(MoodleLib.GetStudentCourseGrades(userid).ToDataSourceResult(request));
        }

        [Description("Xem bảng điểm các khóa học của một sinh viên")]
        [Authorize(Roles = "MoodleCourse.StudentCourseGrade")]
        public ActionResult StudentCourseGrade(string userid = "0")
        {
            var user = MoodleLib.GetUserByID(userid);
            ViewBag.UserID = user == null ? "0" : userid;
            ViewBag.FullName = MoodleLib.GetUserFullNameByID(userid);

            return View();
        }

        [Authorize(Roles = "MoodleCourse.StudentCourseGrade")]
        public ActionResult GetStudentCourseGrades([DataSourceRequest] DataSourceRequest request, string userid)
        {
            return Json(MoodleLib.GetStudentCourseGrades(userid).ToDataSourceResult(request));
        }

        public ActionResult GetCourseMembers([DataSourceRequest] DataSourceRequest request, string courseid = "0")
        {
            return Json(MoodleLib.GetCourseMembers(courseid).ToDataSourceResult(request));
        }
    }
}
