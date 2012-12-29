using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq.Expressions;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CongThongTinSV.Models;
using System.Web.Script.Serialization;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using CongThongTinSV.App_Lib;
using System.ComponentModel;

namespace CongThongTinSV.Controllers
{
    public class MoodleUserController : Controller
    {
        #region Student
        [Authorize(Roles = "MoodleUser.ManageStudent")]
        public ActionResult ManageStudent()
        {
            return View();
        }

        [Authorize(Roles = "MoodleUser.SearchStudent")]
        public ActionResult SearchStudent()
        {
            return View();
        }

        [Authorize(Roles = "MoodleUser.SearchStudent")]
        public ActionResult GetSearchStudents([DataSourceRequest] DataSourceRequest request, string id_chuyen_nganh)
        {
            return Json(MoodleLib.GetStudents(id_chuyen_nganh).Where(t => t.ID_moodle != 0).ToDataSourceResult(request));
        }

        [Authorize(Roles = "MoodleUser.ManageStudent")]
        public ActionResult GetStudents([DataSourceRequest] DataSourceRequest request, string id_chuyen_nganh)
        {
            return Json(MoodleLib.GetStudents(id_chuyen_nganh).ToDataSourceResult(request));
        }

        [Authorize(Roles = "MoodleUser.CreateStudents")]
        public ActionResult CreateStudents(string selectedVals, string id_chuyen_nganh)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetStudents(id_chuyen_nganh).Where(t => t.ID_moodle == 0 && s.Contains(t.ID_sv.ToString()));

            if (list.Count() != 0)
            {
                MoodleLib.CreateStudents(list);
            }
            
            return View();
        }

        [Authorize(Roles = "MoodleUser.DeleteStudents")]
        public ActionResult DeleteStudents(string selectedVals, string id_chuyen_nganh)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetStudents(id_chuyen_nganh).Where(t => t.ID_moodle > 0 && s.Contains(t.ID_sv.ToString()));

            if (list.Count() != 0)
            {
                MoodleLib.DeleteStudents(list);
            }

            return View();
        }

        [Authorize(Roles = "MoodleUser.UpdateStudents")]
        public ActionResult UpdateStudents(string selectedVals, string id_chuyen_nganh)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list =MoodleLib.GetStudents(id_chuyen_nganh).Where(t => t.ID_moodle > 0 && s.Contains(t.ID_sv.ToString()));

            if (list.Count() != 0)
            {
                MoodleLib.UpdateStudents(list);
            }

            return View();
        }

        [Authorize(Roles = "MoodleUser.SyncStudents")]
        public ActionResult SyncStudents(string selectedVals, string id_chuyen_nganh)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetStudents(id_chuyen_nganh).Where(t => t.ID_moodle == 0 && s.Contains(t.ID_sv.ToString()));

            if (list.Count() != 0)
            {
                MoodleLib.SyncStudents(list);
            }

            return View();
        }
        #endregion

        #region Teacher
        [Description("Tìm kiếm tài khoản moodle giáo viên")]
        [Authorize(Roles = "MoodleUser.SearchTeacher")]
        public ActionResult SearchTeacher()
        {
            return View();
        }

        [Authorize(Roles = "MoodleUser.SearchTeacher")]
        public ActionResult GetSearchTeachers([DataSourceRequest] DataSourceRequest request, string id_khoa)
        {

            return Json(MoodleLib.GetTeachers(id_khoa).Where(t => t.ID_moodle != 0).ToDataSourceResult(request));
        }

        [Authorize(Roles = "MoodleUser.ManageTeacher")]
        public ActionResult ManageTeacher()
        {
            return View();
        }

        [Authorize(Roles = "MoodleUser.ManageTeacher")]
        public ActionResult GetTeachers([DataSourceRequest] DataSourceRequest request, string id_khoa)
        {

            return Json(MoodleLib.GetTeachers(id_khoa).ToDataSourceResult(request));
        }

        [Authorize(Roles = "MoodleUser.CreateTeachers")]
        public ActionResult CreateTeachers(string selectedVals, string id_khoa)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });

            var list = MoodleLib.GetTeachers(id_khoa).Where(t => t.ID_moodle == 0 && s.Contains(t.ID_cb.ToString()));

            if (list.Count() != 0)
            {
                MoodleLib.CreateTeachers(list);
            }

            return View();
        }

        [Authorize(Roles = "MoodleUser.DeleteTeachers")]
        public ActionResult DeleteTeachers(string selectedVals, string id_khoa)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });

            var list = MoodleLib.GetTeachers(id_khoa).Where(t => t.ID_moodle > 0 && s.Contains(t.ID_cb.ToString()));

            if (list.Count() != 0)
            {
                MoodleLib.DeleteTeachers(list);
            }

            return View();
        }

        [Authorize(Roles = "MoodleUser.UpdateTeachers")]
        public ActionResult UpdateTeachers(string selectedVals, string id_khoa)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });

            var list = MoodleLib.GetTeachers(id_khoa).Where(t => t.ID_moodle > 0 && s.Contains(t.ID_cb.ToString()));

            if (list.Count() != 0)
            {
                MoodleLib.UpdateTeachers(list);
            }

            return View();
        }

        [Authorize(Roles = "MoodleUser.SyncTeachers")]
        public ActionResult SyncTeachers(string selectedVals, string id_khoa)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetTeachers(id_khoa).Where(t => t.ID_moodle == 0 && s.Contains(t.ID_cb.ToString()));

            if (list.Count() != 0)
            {
                MoodleLib.SyncTeachers(list);
            }

            return View();
        }

        [Authorize(Roles = "MoodleUser.AssignTeacherSystemRole")]
        public ActionResult AssignTeacherSystemRole(string selectedVals, string id_khoa, string id_vai_tro)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetTeachers(id_khoa);
            list = list.Where(t => t.ID_moodle == 0 && s.Contains(t.ID_cb.ToString()));

            if (list.Count() != 0)
            {
                MoodleLib.CreateTeachers(list);
            }

            list = MoodleLib.GetTeachers(id_khoa);
            list = list.Where(t => t.ID_moodle > 0 && s.Contains(t.ID_cb.ToString()));

            if (list.Count() > 0)
            {
                MoodleLib.AssignTeacherSystemRole(list, id_vai_tro);
            }

            return View();
        }

        [Authorize(Roles = "MoodleUser.UnassignTeacherSystemRole")]
        public ActionResult UnassignTeacherSystemRole(string selectedVals, string id_khoa, string id_vai_tro)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetTeachers(id_khoa).Where(t => s.Contains(t.ID_cb.ToString()) && Utility.InArray(t.ID_vai_tro, new char[] { ',' }, id_vai_tro));

            if (list.Count() != 0)
            {
                MoodleLib.UnassignTeacherSystemRole(list, id_vai_tro);
            }

            return View();
        }

        [Authorize(Roles = "MoodleUser.UnassignTeacherAllSystemRoles")]
        public ActionResult UnassignTeacherAllSystemRoles(string selectedVals, string id_khoa)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetTeachers(id_khoa).Where(t => s.Contains(t.ID_cb.ToString()) && t.ID_vai_tro != "");

            if (list.Count() != 0)
            {
                MoodleLib.UnassignTeacherAllSystemRoles(list);
            }

            return View();
        }
        #endregion

        #region AdminUser
        [Authorize(Roles = "MoodleUser.ManageAdminUser")]
        public ActionResult ManageAdminUser()
        {
            return View();
        }

        [Authorize(Roles = "MoodleUser.ManageAdminUser")]
        public ActionResult GetAdminUsers([DataSourceRequest] DataSourceRequest request)
        {
            return Json(MoodleLib.GetAdminUsers().ToDataSourceResult(request));
        }

        [Authorize(Roles = "MoodleUser.CreateAdminUsers")]
        public ActionResult CreateAdminUsers(string selectedVals)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });

            var list = MoodleLib.GetAdminUsers().Where(t => t.ID_moodle == 0 && s.Contains(t.ID.ToString()));

            if (list.Count() != 0)
            {
                MoodleLib.CreateAdminUsers(list);
            }

            return View();
        }

        [Authorize(Roles = "MoodleUser.DeleteAdminUsers")]
        public ActionResult DeleteAdminUsers(string selectedVals)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });

            var list = MoodleLib.GetAdminUsers().Where(t => t.ID_moodle > 0 && s.Contains(t.ID.ToString()));

            if (list.Count() != 0)
            {
                MoodleLib.DeleteAdminUsers(list);
            }

            return View();
        }

        [Authorize(Roles = "MoodleUser.UpdateAdminUsers")]
        public ActionResult UpdateAdminUsers(string selectedVals)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });

            var list = MoodleLib.GetAdminUsers().Where(t => t.ID_moodle > 0 && s.Contains(t.ID.ToString()));

            if (list.Count() != 0)
            {
                MoodleLib.UpdateAdminUsers(list);
            }

            return View();
        }

        [Authorize(Roles = "MoodleUser.SyncAdminUsers")]
        public ActionResult SyncAdminUsers(string selectedVals)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetAdminUsers().Where(t => t.ID_moodle == 0 && s.Contains(t.ID.ToString()));

            if (list.Count() != 0)
            {
                MoodleLib.SyncAdminUsers(list);
            }

            return View();
        }


        [Authorize(Roles = "MoodleUser.AssignAdminUserSystemRole")]
        public ActionResult AssignAdminUserSystemRole(string selectedVals, string id_vai_tro)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetAdminUsers();
            list = list.Where(t => t.ID_moodle == 0 && s.Contains(t.ID.ToString()));

            if (list.Count() != 0)
            {
                MoodleLib.CreateAdminUsers(list);
            }

            list = MoodleLib.GetAdminUsers();
            list = list.Where(t => t.ID_moodle > 0 && s.Contains(t.ID.ToString()));

            if (list.Count() > 0)
            {
                MoodleLib.AssignAdminUserSystemRole(list, id_vai_tro);
            }

            return View();
        }

        [Authorize(Roles = "MoodleUser.UnassignAdminUserSystemRole")]
        public ActionResult UnassignAdminUserSystemRole(string selectedVals, string id_vai_tro)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetAdminUsers().Where(t => s.Contains(t.ID.ToString()) && Utility.InArray(t.ID_vai_tro, new char[] { ',' }, id_vai_tro));

            if (list.Count() != 0)
            {
                MoodleLib.UnassignAdminUserSystemRole(list, id_vai_tro);
            }

            return View();
        }

        [Authorize(Roles = "MoodleUser.UnassignAdminUserAllSystemRoles")]
        public ActionResult UnassignAdminUserAllSystemRoles(string selectedVals)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetAdminUsers().Where(t => s.Contains(t.ID.ToString()) && t.ID_vai_tro != "");

            if (list.Count() != 0)
            {
                MoodleLib.UnassignAdminUserAllSystemRoles(list);
            }

            return View();
        }
        #endregion

        #region UserProfile
        [Authorize(Roles = "MoodleUser.ChangePassword")]
        public ActionResult ChangePassword(string message)
        {
            ViewBag.StatusMessage = message;
            ViewBag.ReturnUrl = Url.Action("ChangePassword");
            return View();
        }

        [Authorize(Roles = "MoodleUser.ChangePassword")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(LocalPasswordModel model)
        {
            ViewBag.ReturnUrl = Url.Action("ChangePassword");

            if (ModelState.IsValid)
            {
                // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    UserData userData = GlobalLib.GetCurrentUserData();

                    if(MoodleLib.GetToken(userData.UserName, model.OldPassword, userData.MoodleService) == userData.MoodleToken)
                    {
                        List<MoodleUser> list = new List<MoodleUser>();
                        list.Add(new MoodleUser
                        {
                            ID = Convert.ToInt32(userData.MoodleUserID),
                            Password = model.NewPassword
                        });
                        changePasswordSucceeded = MoodleLib.UpdateUser(list);
                    }
                    else
                    {
                         changePasswordSucceeded = false;
                    }
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePassword", new { Message = "Bạn đã đổi mật khẩu thành công." });
                }
                else
                {
                    ModelState.AddModelError("","Bạn nhập sai mật khẩu cũ hoặc mật khẩu mới không hợp lệ.");
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [Authorize(Roles = "MoodleUser.MyProfile")]
        public ActionResult MyProfile()
        {
            List<string> list = new List<string>();
            string userid = GlobalLib.GetCurrentUserData().MoodleUserID.ToString();
            list.Add(userid);

            var q = MoodleLib.GetUserByID(list);

            if (q.Count() > 0)
                ViewBag.UserProfile = q.ElementAt(0);
            else
                ViewBag.UserProfile = new MoodleUserResponse();

            return View();
        }

        [Authorize(Roles = "MoodleUser.MyCourseProfile")]
        public ActionResult MyCourseProfile(string courseid="0")
        {
            UserData userData = GlobalLib.GetCurrentUserData();
            string userid = userData.MoodleUserID.ToString();

            if (!MoodleLib.IsUserInCourse(userid, courseid))
            {
                ViewBag.Error = "Bạn chưa được ghi danh vào khóa học này!";
            }
            else
            {
                ViewBag.Error = "";
                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                list.Add(new KeyValuePair<string, string>(userid, courseid));
                var q = MoodleLib.GetCourseUserProfiles(list);

                if (q.Count() > 0)
                {
                    ViewBag.CourseUser = q.ElementAt(0);
                }
                else
                {
                    ViewBag.CourseUser = new MoodleCourseUserResponse();
                }
            }

            var course = MoodleLib.GetCourseByID(courseid);
            ViewBag.CourseName = course == null ? "" : course.fullname;
            ViewBag.FullName = userData.MoodleFullName;

            return View();
        }

        [Authorize(Roles = "MoodleUser.UserProfile")]
        public ActionResult UserProfile(string userid="0")
        {
            List<string> list = new List<string>();
            list.Add(userid);
            var q = MoodleLib.GetUserByID(list);

            if (q.Count() > 0)
                ViewBag.UserProfile = q.ElementAt(0);
            else
                ViewBag.UserProfile = new MoodleUserResponse();

            return View();
        }

        [Authorize(Roles = "MoodleUser.UserCourseProfile")]
        public ActionResult UserCourseProfile(string userid="0", string courseid="0")
        {
            if (!MoodleLib.IsUserInCourse(userid, courseid))
            {
                ViewBag.Error = "Người dùng chưa được ghi danh vào khóa học này!";
            }
            else
            {
                ViewBag.Error = "";
                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                list.Add(new KeyValuePair<string, string>(userid, courseid));
                var q = MoodleLib.GetCourseUserProfiles(list);

                if (q.Count() > 0)
                {
                    ViewBag.CourseUser = q.ElementAt(0);
                }
                else
                {
                    ViewBag.CourseUser = new MoodleCourseUserResponse();
                }
            }

            var course = MoodleLib.GetCourseByID(courseid);
            ViewBag.CourseName = course == null ? "" : course.fullname;
            ViewBag.FullName = MoodleLib.GetUserFullNameByID(userid);
            ViewBag.UserID = userid;

            return View();
        }
        #endregion
    }
}
