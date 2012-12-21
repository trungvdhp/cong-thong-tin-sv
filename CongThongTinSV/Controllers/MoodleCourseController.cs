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

namespace CongThongTinSV.Controllers
{
    public class MoodleCourseController : Controller
    {
        [Authorize(Roles = "MoodleCourse.Manage")]
        public ActionResult Manage()
        {
            return View();
        }

        //[Authorize(Roles = "MoodleCourse.GetCourses")]
        public ActionResult GetCourses([DataSourceRequest] DataSourceRequest request, string id_hocky)
        {
            return Json(MoodleLib.GetCourses(id_hocky).ToDataSourceResult(request));
        }

        //[Authorize(Roles = "MoodleCourse.GetCourseList")]
        public JsonResult GetCourseList(string id_hocky)
        {
            JsonResult result = new JsonResult();
            result.Data = MoodleLib.GetCourseList(id_hocky);
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }

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

        [Authorize(Roles = "MoodleCourse.MyTestList")]
        public ActionResult MyTestList(string courseid = "0")
        {
            MoodleEntities mdb = new MoodleEntities();

            try
            {
                ViewBag.CourseName = mdb.fit_course.AsEnumerable().SingleOrDefault(t => t.id.ToString() == courseid).fullname;
                ViewBag.CourseContent = MoodleLib.GetCourseContents(courseid);
            }
            catch (Exception)
            {
                ViewBag.CourseName = "";
                ViewBag.CourseContent = new List<MoodleCourseContentResponse>();
            }

            //ViewBag.CourseID = courseid;
            return View();
        }

        [Authorize(Roles = "MoodleCourse.TestList")]
        public ActionResult TestList(string courseid="0")
        {
            MoodleEntities mdb = new MoodleEntities();

            try
            {
                ViewBag.CourseName = mdb.fit_course.AsEnumerable().SingleOrDefault(t => t.id.ToString() == courseid).fullname;
                ViewBag.CourseContent = MoodleLib.GetCourseContents(courseid);
            }
            catch (Exception)
            {
                ViewBag.CourseName = "";
                ViewBag.CourseContent = new List<MoodleCourseContentResponse>();
            }

            //ViewBag.CourseID = courseid;
            return View();
        }

        [Authorize(Roles = "MoodleCourse.UpdateYGrades")]
        public ActionResult UpdateYGrades(string selectedVals, string quizid = "0")
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetQuizGrades(quizid).Where(t => s.Contains(t.ID.ToString()) && t.ID_sv != 0 && t.NewGrade.HasValue).ToList();

            if (list.Count() != 0)
            {
                MoodleLib.UpdateYGrades(list);
            }

            return View();
        }

        [Authorize(Roles = "MoodleCourse.ModuleGradeBook")]
        public ActionResult ModuleGradeBook(string quizid = "0")
        {
            MoodleEntities mdb = new MoodleEntities();
            
            try
            {
                var quiz = mdb.fit_quiz.AsEnumerable().SingleOrDefault(t => (t.id + 10).ToString() == quizid);
                ViewBag.QuizID = quiz == null ? 0 : quiz.id;
                ViewBag.QuizName = quiz == null ? "" : quiz.name;
                ViewBag.CourseID = quiz == null ? 0 : quiz.course;
                ViewBag.CourseName = mdb.fit_course.AsEnumerable().SingleOrDefault(t => t.id == ViewBag.CourseID).fullname;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.CourseID = 0;
                ViewBag.CourseName = "";
                ViewBag.QuizID = 0;
                ViewBag.QuizName = "";
            }

            return View();
        }

        [Authorize(Roles = "MoodleCourse.GetModuleGradeBook")]
        public ActionResult GetModuleGradeBook([DataSourceRequest] DataSourceRequest request, string quizid = "0")
        {
            return Json(MoodleLib.GetQuizGrades(quizid).ToDataSourceResult(request));
        }

        [Authorize(Roles = "MoodleCourse.CourseGradeBook")]
        public ActionResult CourseGradeBook(string courseid="0")
        {
            ViewBag.CourseID = courseid;
            MoodleEntities mdb = new MoodleEntities();

            try
            {
                ViewBag.CourseName = mdb.fit_course.AsEnumerable().SingleOrDefault(t => t.id.ToString() == courseid).fullname;
            }
            catch (Exception)
            {
                ViewBag.CourseName = "";
            }

            return View();
        }

        [Authorize(Roles = "MoodleCourse.GetCourseGradeBook")]
        public ActionResult GetCourseGradeBook([DataSourceRequest] DataSourceRequest request, string courseid="0")
        {
            return Json(MoodleLib.GetCourseGrades(courseid).ToDataSourceResult(request));
        }
    }
}
