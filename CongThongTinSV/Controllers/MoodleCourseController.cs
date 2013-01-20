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
            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.CreateCourses(list) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi tạo khóa học";
                    data.state = "error";
                }
                else
                {
                    data.title = "Success";
                    data.message = "Tạo khóa học thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Description("Xóa các khóa học trên moodle")]
        [Authorize(Roles = "MoodleCourse.DeleteCourses")]
        public ActionResult DeleteCourses(string selectedVals, string id_hocky)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetCourses(id_hocky).Where(t => t.ID_moodle > 0 && s.Contains(t.ID.ToString()));
            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.DeleteCourses(list) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi xóa khóa học";
                    data.state = "error";
                }
                else
                {
                    data.title = "Success";
                    data.message = "Xóa khóa học thành công";
                    data.state = "success";
                }
            }

            return Json(data);
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

        [Description("Xuất bảng điểm tổng kết khóa học dạng kết quả đánh giá học phần ra excel")]
        [Authorize(Roles = "MoodleCourse.ExportCourseGradeToExcel")]
        public FileResult ExportCourseGradeToExcel([DataSourceRequest]DataSourceRequest request, string courseid, string coursename)
        {
            // Get data
            IEnumerable<MoodleCourseStudentGrade> grades = MoodleLib.GetCourseStudentGrades(courseid).ToDataSourceResult(request).Data.Cast<MoodleCourseStudentGrade>();

            ExcelExportor workbook = MoodleLib.ExportCourseGradeToExcel(GlobalLib.GetExcelTemplateFolderPath() + "Grade.xls", GlobalLib.GetExcelTemplateFolderPath() + "GradeTemp.xls", "Kết quả đánh giá học phần", grades, courseid);

            //Save workbook
            workbook.SaveAs();

            return File(workbook.GetByteArray(),
                "application/vnd.ms-excel", 
                workbook.ExportSheetName + " " + coursename);
        }

        [Description("Xuất bảng điểm tổng kết khóa học dạng kết quả đánh giá học phần ra pdf")]
        [Authorize(Roles = "MoodleCourse.ExportCourseGradeToPdf")]
        public FileResult ExportCourseGradeToPdf([DataSourceRequest]DataSourceRequest request, string courseid, string coursename)
        {
            // Get data
            IEnumerable<MoodleCourseStudentGrade> grades = MoodleLib.GetCourseStudentGrades(courseid).ToDataSourceResult(request).Data.Cast<MoodleCourseStudentGrade>();

            ExcelExportor workbook = MoodleLib.ExportCourseGradeToExcel(GlobalLib.GetExcelTemplateFolderPath() + "Grade.pdf", GlobalLib.GetExcelTemplateFolderPath() + "GradeTemp.xls", "Kết quả đánh giá học phần", grades, courseid);

            //Save workbook
            workbook.ExportAsFixedFormat();

            return File(workbook.GetByteArray(),
                "application/pdf",
                workbook.ExportSheetName + " " + coursename);
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
