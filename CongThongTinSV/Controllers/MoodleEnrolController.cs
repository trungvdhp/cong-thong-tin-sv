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

            if (list.Count() != 0)
            {
                MoodleLib.ManualEnrolStudents(list);
            }

            return View();
        }

        [Authorize(Roles = "MoodleEnrol.SuspendEnrolStudents")]
        public ActionResult SuspendEnrolStudents(string selectedVals, string id_lop_tc)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetEnrolStudents(id_lop_tc).Where(t => t.Trang_thai && s.Contains(t.ID.ToString()));

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

            if (list.Count() > 0)
            {
                MoodleLib.ManualEnrolTeachers(list, id_vai_tro, suspended);
            }

            return View();
        }

        [Authorize(Roles = "MoodleEnrol.UnassignTeacherRole")]
        public ActionResult UnassignTeacherRole(string selectedVals, string id_lop_tc, string id_vai_tro)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetEnrolTeachers(id_lop_tc).Where(t => s.Contains(t.ID_cb.ToString()) && Utility.InArray(t.ID_vai_tro, new char[] { ',' }, id_vai_tro));

            if (list.Count() != 0)
            {
                MoodleLib.UnassignTeacherRole(list, id_vai_tro);
            }

            return View();
        }

        [Authorize(Roles = "MoodleEnrol.UnassignTeacherAllRoles")]
        public ActionResult UnassignTeacherAllRoles(string selectedVals, string id_lop_tc)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetEnrolTeachers(id_lop_tc).Where(t => s.Contains(t.ID_cb.ToString()) && t.ID_vai_tro != "");

            if (list.Count() != 0)
            {
                MoodleLib.UnassignTeacherAllRoles(list);
            }

            return View();
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

            if (list.Count() > 0)
            {
                MoodleLib.ManualEnrolAdminUsers(list, id_vai_tro, suspended);
            }

            return View();
        }

        [Authorize(Roles = "MoodleEnrol.UnassignAdminUserRole")]
        public ActionResult UnassignAdminUserRole(string selectedVals, string id_lop_tc, string id_vai_tro)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetEnrolAdminUsers(id_lop_tc).Where(t => s.Contains(t.ID.ToString()) && Utility.InArray(t.ID_vai_tro, new char[] { ',' }, id_vai_tro));

            if (list.Count() != 0)
            {
                MoodleLib.UnassignAdminUserRole(list, id_vai_tro);
            }

            return View();
        }

        [Authorize(Roles = "MoodleEnrol.UnassignAdminUsersAllRoles")]
        public ActionResult UnassignAdminUserAllRoles(string selectedVals, string id_lop_tc)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetEnrolAdminUsers(id_lop_tc).Where(t => s.Contains(t.ID.ToString()) && t.ID_vai_tro != "");

            if (list.Count() != 0)
            {
                MoodleLib.UnassignAdminUserAllRoles(list);
            }

            return View();
        }
        #endregion
    }
}
