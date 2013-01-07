using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongThongTinSV.App_Lib;
using CongThongTinSV.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.ComponentModel;

namespace CongThongTinSV.Controllers
{
    public class MoodleQuizController : Controller
    {
        [Description("Cập nhật điểm thi của các sinh viên")]
        [Authorize(Roles = "MoodleQuiz.UpdateYGrades")]
        public ActionResult UpdateYGrades(string selectedVals, string quizid = "0")
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetQuizStudentGrades(quizid).Where(t => s.Contains(t.ID.ToString()) && t.IsDiffGrade).ToList();

            if (list.Count() != 0)
            {
                MoodleLib.UpdateYGrades(list);
            }

            return View();
        }

        [Authorize(Roles = "MoodleQuiz.UpdateYGrades")]
        public ActionResult UpdateYGrade(string newgrade, string id_diem_thi="0")
        {
            MoodleLib.UpdateYGrade(newgrade, id_diem_thi);

            return View();
        }

        [Description("Xem danh sách các bài thi trắc nghiệm của khóa học bất kỳ")]
        [Authorize(Roles = "MoodleQuiz.QuizList")]
        public ActionResult QuizList(string courseid = "0")
        {
            var course = MoodleLib.GetCourseByID(courseid);
            if (course != null)
            {
                ViewBag.CourseName = MoodleLib.GetCourseByID(courseid).fullname;
                ViewBag.CourseContent = MoodleLib.GetCourseContents(courseid);
            }
            else
            {
                ViewBag.CourseName = "";
                ViewBag.CourseContent = new List<MoodleCourseContentResponse>();
            }

            return View();
        }

        [Description("Xem bảng điểm chi tiết một bài thi trắc nghiệm của mình")]
        [Authorize(Roles = "MoodleQuiz.MyQuizStudentGrade")]
        public ActionResult MyQuizStudentGrade(string quizid = "0")
        {
            string userid = GlobalLib.GetCurrentUserData().MoodleUserID.ToString();
            var quiz = MoodleLib.GetQuizByID(quizid);
            ViewBag.QuizID = quiz == null ? 0 : quiz.id + 10;
            ViewBag.QuizName = quiz == null ? "" : quiz.name;
            var course = MoodleLib.GetCourseByQuiz(quiz);
            ViewBag.CourseID = course == null ? 0 : course.id;
            ViewBag.CourseName = course == null ? "" : course.fullname;

            if (!MoodleLib.IsUserInCourse(userid, "" + ViewBag.CourseID))
            {
                ViewBag.Error = "Bài thi này thuộc khóa học mà bạn chưa được ghi danh!";
            }
            else
            {
                ViewBag.Error = "";
            }

            return View();
        }

        [Authorize(Roles = "MoodleQuiz.MyQuizStudentGrade")]
        public ActionResult GetMyQuizStudentGrades([DataSourceRequest] DataSourceRequest request, string quizid = "0")
        {
            return Json(MoodleLib.GetQuizStudentNewGrades(quizid).ToDataSourceResult(request));
        }

        [Description("Xem bảng điểm chi tiết của một bài thi trắc nghiệm bất kỳ")]
        [Authorize(Roles = "MoodleQuiz.QuizStudentGrade")]
        public ActionResult QuizStudentGrade(string quizid = "0")
        {
            var quiz = MoodleLib.GetQuizByID(quizid);
            ViewBag.QuizID = quiz == null ? 0 : quiz.id + 10;
            ViewBag.QuizName = quiz == null ? "" : quiz.name;
            var course = MoodleLib.GetCourseByQuiz(quiz);
            ViewBag.CourseID = course == null ? 0 : course.id;
            ViewBag.CourseName = course == null ? "" : course.fullname;

            return View();
        }

        [Authorize(Roles = "MoodleQuiz.QuizStudentGrade")]
        public ActionResult GetQuizStudentGrades([DataSourceRequest] DataSourceRequest request, string quizid = "0")
        {
            return Json(MoodleLib.GetQuizStudentGrades(quizid).ToDataSourceResult(request));
        }

        [Description("Xem bảng điểm tổng hợp các bài thi trắc nghiệm trong khóa học của mình")]
        [Authorize(Roles = "MoodleQuiz.MyQuizGrade")]
        public ActionResult MyQuizGrade(string courseid = "0")
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

        [Authorize(Roles = "MoodleQuiz.MyQuizGrade")]
        public ActionResult GetMyQuizGrades([DataSourceRequest] DataSourceRequest request, string courseid = "0")
        {
            string userid = GlobalLib.GetCurrentUserData().MoodleUserID.ToString();

            return Json(MoodleLib.GetStudentQuizGrades(userid, courseid).ToDataSourceResult(request));
        }

        [Description("Xem bảng điểm tổng hợp các bài thi trắc nghiệm trong khóa học bất kỳ")]
        [Authorize(Roles = "MoodleQuiz.StudentQuizGrade")]
        public ActionResult StudentQuizGrade(string userid = "0", string courseid = "0")
        {
            if (!MoodleLib.IsUserInCourse(userid, courseid))
            {
                ViewBag.Error = "Người dùng chưa được ghi danh vào khóa học này!";
            }
            else
            {
                ViewBag.Error = "";
                MARK_DiemThi_TC diem = MoodleLib.GetYGrade(userid, courseid);

                if (diem != null)
                {
                    ViewBag.Grade = string.Format("{0:0.0}", diem.Diem_thi);
                    ViewBag.GradeID = diem.ID_diem_thi;
                }
                else
                {
                    ViewBag.Grade = "không xác định";
                    ViewBag.GradeID = 0;
                }
            }

            var user = MoodleLib.GetUserByID(userid);
            ViewBag.FullName = user == null ? "" : user.lastname + " " + user.firstname;
            ViewBag.UserID = user == null ? "0" : userid;
            var course = MoodleLib.GetCourseByID(courseid);
            ViewBag.CourseName = course == null ? "" : course.fullname;
            ViewBag.CourseID = courseid;
          
            return View();
        }

        [Authorize(Roles = "MoodleQuiz.StudentQuizGrade")]
        public ActionResult GetStudentQuizGrades([DataSourceRequest] DataSourceRequest request, string userid = "0", string courseid = "0")
        {
            return Json(MoodleLib.GetStudentQuizGrades(userid, courseid).ToDataSourceResult(request));
        }

        [Description("Xem kết quả chi tiết bài làm trắc nghiệm của mình")]
        [Authorize(Roles = "MoodleQuiz.MyQuizResult")]
        public ActionResult MyQuizResult(string quizid)
        {
            return View();
        }

        [Description("Xem kết quả chi tiết bài làm trắc nghiệm bất kỳ")]
        [Authorize(Roles = "MoodleQuiz.StudentQuizResult")]
        public ActionResult StudentQuizResult(string quizid, string userid)
        {
            return View();
        }

        [Description("Xuất bảng điểm chi tiết bài thi trắc nghiệm ra excel")]
        [Authorize(Roles = "MoodleQuiz.ExportQuizGradeToExcel")]
        public FileResult ExportQuizGradeToExcel([DataSourceRequest]DataSourceRequest request, string quizid, string quizname, string datafields, string datatitles)
        {
            // Get data
            IEnumerable<MoodleQuizStudentGrade> grades = MoodleLib.GetQuizStudentGrades(quizid).ToDataSourceResult(request).Data.Cast<MoodleQuizStudentGrade>();

            string[] fields = datafields.Split(new char[] { ',' });
            string[] titles = datatitles.Split(new char[] { ',' });
            int len = fields.Count();
            int startRow = 1, startColumn = 1;
            //Init workbook
            var workbook = new ExcelExportor(GlobalLib.GetExcelTemplateFolderPath() + "QuizGrades", "", "Bảng điểm bài thi");
            //Set header
            workbook.SetCellValue(quizname);
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
                else if (fields[i] == "NewGrade")
                {
                    workbook.Set1DArrayValue(grades.Select(t => t.NewGrade.ToString()).ToArray(), true, startRow, i + 1);
                    workbook.SetHorizontalAlignment(Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter);
                    workbook.SetNumberFormat("0.0");
                    //workbook.SetBoderLineStyles();
                }
                else if (fields[i] == "OldGrade")
                {
                    workbook.Set1DArrayValue(grades.Select(t => t.OldGrade.ToString()).ToArray(), true, startRow, i + 1);
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
                workbook.ExportSheetName + " " + quizname);          //Suggested file name in the "Save as" dialog which will be displayed to the end user
        }
    }
}
