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
    public class MoodleEnrolController : Controller
    {
        #region Student
        [Authorize(Roles = "MoodleEnrol.EnrolStudent")]
        public ActionResult EnrolStudent()
        {
            return View();
        }

        [Authorize(Roles = "MoodleEnrol.GetEnrolStudents")]
        public ActionResult GetEnrolStudents([DataSourceRequest] DataSourceRequest request, string id_lop_tc)
        {

            return Json(MoodleLib.GetEnrolStudentsXGrade(id_lop_tc).ToDataSourceResult(request));
        }

        [Authorize(Roles = "MoodleEnrol.ManualEnrolStudents")]
        public ActionResult ManualEnrolStudents(string selectedVals, string id_lop_tc)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetEnrolStudentsXGrade(id_lop_tc);
            var list1 = list.Where(t => t.ID_moodle == 0 && s.Contains(t.ID.ToString()));
            if (list1.Count() != 0)
            {
                MoodleLib.CreateStudents(list1);
            }

            var list2 = list.Where(t => t.ID_moodle > 0 && t.Tinh_trang == "Chưa ghi danh" && s.Contains(t.ID.ToString()));

            if (list2.Count() != 0)
            {
                MoodleLib.ManualEnrolStudents(list2);
            }

            return View();
        }

        [Authorize(Roles = "MoodleEnrol.SuspendEnrolStudents")]
        public ActionResult SuspendEnrolStudents(string selectedVals, string id_lop_tc)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetEnrolStudents(id_lop_tc).Where(t => t.Tinh_trang == "Đã ghi danh" && s.Contains(t.ID.ToString()));

            if (list.Count() != 0)
            {
                MoodleLib.SuspendEnrolStudents(list);
            }

            return View();
        }
        #endregion

        #region Teacher
        [Authorize(Roles = "MoodleEnrol.EnrolTeacher")]
        public ActionResult EnrolTeacher()
        {
            return View();
        }

        [Authorize(Roles = "MoodleEnrol.GetEnrolTeachers")]
        public ActionResult GetEnrolTeachers([DataSourceRequest] DataSourceRequest request, string id_lop_tc)
        {
            return Json(MoodleLib.GetEnrolTeachers(id_lop_tc).ToDataSourceResult(request));
        }

        [Authorize(Roles = "MoodleEnrol.ManualEnrolTeachers")]
        public ActionResult ManualEnrolTeachers(string selectedVals, string id_lop_tc, string id_vai_tro, string suspended)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetEnrolTeachers(id_lop_tc);
            var list1 = list.Where(t => t.ID_moodle == 0 && s.Contains(t.ID_cb.ToString()));

            if (list1.Count() != 0)
            {
                MoodleLib.CreateTeachers(list1);
            }

            var list2 = list.Where(t => t.ID_moodle > 0 && t.Tinh_trang == "Chưa ghi danh" && s.Contains(t.ID_cb.ToString()));

            if (list2.Count() > 0)
            {
                MoodleLib.ManualEnrolTeachers(list2, id_vai_tro, suspended);
            }

            return View();
        }

        [Authorize(Roles = "MoodleEnrol.UnassignTeachersRole")]
        public ActionResult UnassignTeachersRole(string selectedVals, string id_lop_tc, string id_vai_tro)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetEnrolTeachers(id_lop_tc).Where(t => s.Contains(t.ID_moodle.ToString()) && Utility.InArray(t.ID_vai_tro, new char[] { ',' }, id_vai_tro));

            if (list.Count() != 0)
            {
                MoodleLib.UnassignTeachersRole(list, id_vai_tro);
            }

            return View();
        }

        [Authorize(Roles = "MoodleEnrol.UnassignTeachersAllRole")]
        public ActionResult UnassignTeachersAllRoles(string selectedVals, string id_lop_tc)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetEnrolTeachers(id_lop_tc).Where(t => s.Contains(t.ID_moodle.ToString()) && t.ID_vai_tro != "");

            if (list.Count() != 0)
            {
                MoodleLib.UnassignTeachersAllRoles(list);
            }

            return View();
        }
        #endregion
    }
}
