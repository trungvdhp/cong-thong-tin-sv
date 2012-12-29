using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongThongTinSV.App_Lib;
using CongThongTinSV.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace CongThongTinSV.Controllers
{
    public class MoodleQuizController : Controller
    {
        [Authorize(Roles = "MoodleQuiz.UpdateYGrades")]
        public ActionResult UpdateYGrades(string selectedVals, string quizid = "0")
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetQuizStudentGrades(quizid).Where(t => s.Contains(t.ID.ToString()) && t.ID_sv != 0 && t.NewGrade.HasValue).ToList();

            if (list.Count() != 0)
            {
                MoodleLib.UpdateYGrades(list);
            }

            return View();
        }

        [Authorize(Roles = "MoodleQuiz.UpdateYGrade")]
        public ActionResult UpdateYGrade(decimal? newGrade, string userid = "0", string courseid = "0")
        {
            MoodleLib.UpdateYGrade(newGrade, userid, courseid);

            return View();
        }

        [Authorize(Roles = "MoodleQuiz.CourseQuizList")]
        public ActionResult CourseQuizList(string courseid = "0")
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
                var diem = MoodleLib.GetYGrade(userid, courseid);

                if (diem != null)
                {
                    ViewBag.Grade = string.Format("{0:0.0}", diem.Diem_thi);
                }
                else
                {
                    ViewBag.Grade = "không xác định";
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

        [Authorize(Roles = "MoodleQuiz.MyQuizResult")]
        public ActionResult MyQuizResult(string quizid)
        {
            return View();
        }

        [Authorize(Roles = "MoodleQuiz.StudentQuizResult")]
        public ActionResult StudentQuizResult(string quizid, string userid)
        {
            return View();
        }
    }
}
