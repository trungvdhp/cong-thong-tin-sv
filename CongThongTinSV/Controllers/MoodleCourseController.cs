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

        [Authorize(Roles = "MoodleCourse.CourseStudentGrade")]
        public ActionResult CourseStudentGrade(string courseid = "0")
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

        [Authorize(Roles = "MoodleCourse.GetCourseStudentGrades")]
        public ActionResult GetCourseStudentGrades([DataSourceRequest] DataSourceRequest request, string courseid = "0")
        {
            return Json(MoodleLib.GetCourseStudentGrades(courseid).ToDataSourceResult(request));
        }

        [Authorize(Roles = "MoodleCourse.MyCourseGrade")]
        public ActionResult MyCourseGrade()
        {
            ViewBag.FullName = GlobalLib.GetCurrentUserData().MoodleFullName;
            return View();
        }

        [Authorize(Roles = "MoodleCourse.GetMyCourseGrades")]
        public ActionResult GetMyCourseGrades([DataSourceRequest] DataSourceRequest request)
        {
            string userid = GlobalLib.GetCurrentUserData().MoodleUserID.ToString();

            return Json(MoodleLib.GetStudentCourseGrades(userid).ToDataSourceResult(request));
        }

        [Authorize(Roles = "MoodleCourse.StudentCourseGrade")]
        public ActionResult StudentCourseGrade(string userid = "0")
        {
            var user = MoodleLib.GetUserByID(userid);
            ViewBag.UserID = user == null ? "0" : userid;
            ViewBag.FullName = MoodleLib.GetUserFullNameByID(userid);

            return View();
        }

        [Authorize(Roles = "MoodleCourse.GetStudentCourseGrades")]
        public ActionResult GetStudentCourseGrades([DataSourceRequest] DataSourceRequest request, string userid)
        {
            return Json(MoodleLib.GetStudentCourseGrades(userid).ToDataSourceResult(request));
        }
    }
}
