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
            var list = MoodleLib.GetQuizStudentGrades(quizid).Where(t => s.Contains(t.ID.ToString()) && t.Khac_diem).ToList();
            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.UpdateYGrades(list) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi cập nhật điểm thi";
                    data.state = "error";
                }
                else
                {
                    data.title = "Success";
                    data.message = "Cập nhật điểm thi thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Authorize(Roles = "MoodleQuiz.UpdateYGrades")]
        public ActionResult UpdateYGrade(string newgrade, string id_diem_thi="0")
        {
            var data = new Message();

            if (MoodleLib.UpdateYGrade(newgrade, id_diem_thi) == -1)
            {
                data.title = "Error";
                data.message = "Lỗi khi cập nhật điểm thi";
                data.state = "error";
            }
            else
            {
                data.title = "Success";
                data.message = "Cập nhật điểm thi thành công";
                data.state = "success";
            }

            return Json(data);
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

        [Description("Xem lại kết quả chi tiết bài làm trắc nghiệm của mình")]
        [Authorize(Roles = "MoodleQuiz.MyQuizReview")]
        public ActionResult MyQuizReview(string quizid = "0")
        {
            var user = GlobalLib.GetCurrentUserData();
            var quiz = MoodleLib.GetQuizByID(quizid);
            long qid = quiz == null ? 0 : quiz.id;
            ViewBag.QuizID = quiz == null ? 0 : quiz.id + 10;
            ViewBag.QuizName = quiz == null ? "" : quiz.name;
            var course = MoodleLib.GetCourseByQuiz(quiz);
            ViewBag.CourseID = course == null ? 0 : course.id;
            ViewBag.CourseName = course == null ? "" : course.fullname;

            if (!MoodleLib.IsUserInCourse("" + user.MoodleUserID, "" + ViewBag.CourseID))
            {
                ViewBag.Error = "Bài thi này thuộc khóa học mà bạn chưa được ghi danh!";
            }
            else
            {
                ViewBag.Error = "";
            }

            var attempt = new MoodleQuizAttempt();

            if (user != null)
            {
                attempt.ID = user.MoodleUserID;
                attempt.Ten = user.MoodleFullName;

                MoodleEntities mdb = new MoodleEntities();
                var q = mdb.fit_quiz_attempts.SingleOrDefault(t => t.userid == user.MoodleUserID && t.quiz == qid && t.state == "finished");

                if (q != null)
                {
                    attempt.Bat_dau = Utility.ConvertToDateTime((int)q.timestart);
                    attempt.Ket_thuc = Utility.ConvertToDateTime((int)q.timefinish);
                    attempt.Thoi_gian_lam = Utility.ConvertToString(attempt.Ket_thuc.Subtract(attempt.Bat_dau));
                    long gioi_han = q.timestart + quiz.timelimit;
                    attempt.Qua_han = gioi_han - q.timefinish >= 0 ? "0" : Utility.ConvertToString(attempt.Ket_thuc.Subtract(Utility.ConvertToDateTime((int)gioi_han)));
                    attempt.Tong_diem = string.Format("{0:0.00}/{1:0.00}", q.sumgrades.Value, quiz == null ? 0 : quiz.sumgrades);
                    decimal? diem_he_10 = q.sumgrades == null ? null : (decimal?)(q.sumgrades.Value * 10 / quiz.sumgrades);
                    attempt.Diem_he_10 = string.Format("{0:0.0}", diem_he_10.Value);
                    decimal diem_he_4 = Utility.Convert10To4Scale(diem_he_10).Value;
                    attempt.Diem_he_4 = string.Format("{0:0.0} ({1})", diem_he_4, Utility.Convert10ScaleToText((float?)diem_he_10));
                }
                else
                {
                    ViewBag.Error = "Bạn không làm bài thi này!";
                }
            }

            ViewBag.Attempt = attempt;
            return View();
        }

        [Authorize(Roles = "MoodleQuiz.MyQuizReview")]
        public ActionResult GetMyQuizQuestions([DataSourceRequest] DataSourceRequest request, string quizid = "0")
        {
            string userid = GlobalLib.GetCurrentUserData().MoodleUserID.ToString();

            return Json(MoodleLib.GetStudentQuizQuestions(userid, quizid).ToDataSourceResult(request));
        }

        [Description("Xem kết quả chi tiết bài làm trắc nghiệm bất kỳ")]
        [Authorize(Roles = "MoodleQuiz.StudentQuizReview")]
        public ActionResult StudentQuizReview(string userid="0", string quizid = "0")
        {
            var user = MoodleLib.GetUserByID(userid);
            var quiz = MoodleLib.GetQuizByID(quizid);
            long qid =  quiz == null ? 0 : quiz.id;
            ViewBag.QuizID = quiz == null ? 0 : quiz.id + 10;
            ViewBag.QuizName = quiz == null ? "" : quiz.name;
            var course = MoodleLib.GetCourseByQuiz(quiz);
            ViewBag.CourseID = course == null ? 0 : course.id;
            ViewBag.CourseName = course == null ? "" : course.fullname;

            if (!MoodleLib.IsUserInCourse(userid, "" + ViewBag.CourseID))
            {
                ViewBag.Error = "Bài thi này thuộc khóa học mà người dùng chưa được ghi danh!";
            }
            else
            {
                ViewBag.Error = "";
            }

            var attempt = new MoodleQuizAttempt();

            if (user != null)
            {
                attempt.ID = user.id;
                attempt.Ho_dem = user.lastname;
                attempt.Ten = user.firstname;

                MoodleEntities mdb = new MoodleEntities();
                var q = mdb.fit_quiz_attempts.SingleOrDefault(t => t.userid == user.id && t.quiz == qid && t.state == "finished");

                if (q != null)
                {
                    attempt.Bat_dau = Utility.ConvertToDateTime((int)q.timestart);
                    attempt.Ket_thuc =Utility.ConvertToDateTime((int)q.timefinish);
                    attempt.Thoi_gian_lam = Utility.ConvertToString(attempt.Ket_thuc.Subtract(attempt.Bat_dau));
                    long gioi_han = q.timestart + quiz.timelimit;
                    attempt.Qua_han = gioi_han - q.timefinish >= 0 ? "0" : Utility.ConvertToString(attempt.Ket_thuc.Subtract(Utility.ConvertToDateTime((int)gioi_han)));
                    attempt.Tong_diem = string.Format("{0:0.00}/{1:0.00}", q.sumgrades.Value, quiz == null ? 0 : quiz.sumgrades);
                    decimal? diem_he_10 = q.sumgrades == null ? null : (decimal?)(q.sumgrades.Value * 10 / quiz.sumgrades);
                    attempt.Diem_he_10 = string.Format("{0:0.0}", diem_he_10.Value);
                    decimal diem_he_4 = Utility.Convert10To4Scale(diem_he_10).Value;
                    attempt.Diem_he_4 = string.Format("{0:0.0} ({1})", diem_he_4, Utility.Convert10ScaleToText((float?)diem_he_10));
                }
                else
                {
                    ViewBag.Error = "Người dùng không làm bài thi này!";
                }
            }

            ViewBag.Attempt = attempt;

            return View();
        }

        [Authorize(Roles = "MoodleQuiz.StudentQuizReview")]
        public ActionResult GetStudentQuizQuestions([DataSourceRequest] DataSourceRequest request, string userid = "0", string quizid = "0")
        {
            return Json(MoodleLib.GetStudentQuizQuestions(userid, quizid).ToDataSourceResult(request));
        }

        [Description("Xuất bảng điểm bài thi trắc nghiệm dạng kết quả đánh giá học phần ra excel")]
        [Authorize(Roles = "MoodleQuiz.ExportQuizGradeToExcel")]
        public FileResult ExportQuizGradeToExcel([DataSourceRequest]DataSourceRequest request, string quizid, string courseid, string coursename)
        {
            // Get data
            IEnumerable<MoodleQuizStudentGrade> grades = MoodleLib.GetQuizStudentGrades(quizid).ToDataSourceResult(request).Data.Cast<MoodleQuizStudentGrade>();

            ExcelExportor workbook = MoodleLib.ExportQuizGradeToExcel(GlobalLib.GetExcelTemplateFolderPath() + "Grade.xls", GlobalLib.GetExcelTemplateFolderPath() + "GradeTemp.xls", "Kết quả đánh giá học phần", grades, courseid);
           
            //Save workbook
            workbook.SaveAs();
            
            return File(workbook.GetByteArray(),
                "application/vnd.ms-excel", 
                workbook.ExportSheetName + " " + coursename);
        }

        [Description("Xuất bảng điểm bài thi trắc nghiệm dạng kết quả đánh giá học phần ra pdf")]
        [Authorize(Roles = "MoodleQuiz.ExportQuizGradeToPdf")]
        public FileResult ExportQuizGradeToPdf([DataSourceRequest]DataSourceRequest request, string quizid, string courseid, string coursename)
        {
            // Get data
            IEnumerable<MoodleQuizStudentGrade> grades = MoodleLib.GetQuizStudentGrades(quizid).ToDataSourceResult(request).Data.Cast<MoodleQuizStudentGrade>();

            ExcelExportor workbook = MoodleLib.ExportQuizGradeToExcel(GlobalLib.GetExcelTemplateFolderPath() + "Grade.pdf", GlobalLib.GetExcelTemplateFolderPath() + "GradeTemp.xls", "Kết quả đánh giá học phần", grades, courseid);

            //Save workbook
            workbook.ExportAsFixedFormat();

            return File(workbook.GetByteArray(), 
                "application/pdf",
                workbook.ExportSheetName + " " + coursename);
        }

        [Description("Xuất báo cáo kết quả đánh giá học phần")]
        [Authorize(Roles = "MoodleQuiz.ExportQuizGradeToReport")]
        public ActionResult ExportQuizGradeToReport([DataSourceRequest]DataSourceRequest request, string quizid)
        {
            ViewBag.QuizID = quizid;
            ViewBag.Request = request;

            return View();
        }
    }
}
