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

        [Authorize(Roles = "MoodleQuiz.UpdateYGrades")]
        public ActionResult UpdateYGrades(decimal? newGrade, string userid = "0", string courseid = "0")
        {
            MoodleLib.UpdateYGrades(newGrade, userid, courseid);

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

        [Authorize(Roles = "MoodleQuiz.QuizStudentGrade")]
        public ActionResult QuizStudentGrade(string quizid = "0")
        {
            try
            {
                var quiz = MoodleLib.GetQuizByID(quizid);
                ViewBag.QuizID = quiz == null ? 0 : quiz.id + 10;
                ViewBag.QuizName = quiz == null ? "" : quiz.name;
                var course = MoodleLib.GetCourseByQuiz(quiz);
                ViewBag.CourseID = course == null ? 0 : course.id;
                ViewBag.CourseName = course == null ? "0" : course.fullname;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.CourseID = 0;
                ViewBag.CourseName = "";
                ViewBag.QuizID = 10;
                ViewBag.QuizName = "";
            }

            return View();
        }

        [Authorize(Roles = "MoodleQuiz.GetQuizStudentGrades")]
        public ActionResult GetQuizStudentGrades([DataSourceRequest] DataSourceRequest request, string quizid = "0")
        {
            return Json(MoodleLib.GetQuizStudentGrades(quizid).ToDataSourceResult(request));
        }

        [Authorize(Roles = "MoodleQuiz.MyQuizGrade")]
        public ActionResult MyQuizGrade(string courseid = "0")
        {
            var course = MoodleLib.GetCourseByID(courseid);
            ViewBag.CourseName = course == null ? "" : course.fullname;
            ViewBag.CourseID = courseid;

            return View();
        }

        [Authorize(Roles = "MoodleQuiz.GetMyQuizGrades")]
        public ActionResult GetMyQuizGrades([DataSourceRequest] DataSourceRequest request, string courseid = "0")
        {
            string userid = GlobalLib.GetCurrentUserData().MoodleUserID.ToString();

            return Json(MoodleLib.GetStudentQuizGrades(userid, courseid).ToDataSourceResult(request));
        }

        [Authorize(Roles = "MoodleQuiz.StudentQuizGrade")]
        public ActionResult StudentQuizGrade(string userid = "0", string courseid = "0")
        {
            var user = MoodleLib.GetUserByID(userid);
            ViewBag.FullName = user == null ? "" : user.lastname + " " + user.firstname;
            ViewBag.UserID = user == null ? "0" : userid;
            var course = MoodleLib.GetCourseByID(courseid);
            ViewBag.CourseName = course == null ? "" : course.fullname;
            ViewBag.CourseID = courseid;
            var diem = MoodleLib.GetGrade(userid, courseid);

            if (diem != null)
            {
                ViewBag.Grade = diem.Diem_thi;
            }
            else
            {
                ViewBag.Grade = 0;
            }

            return View();
        }

        [Authorize(Roles = "MoodleQuiz.GetStudentQuizGrades")]
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
