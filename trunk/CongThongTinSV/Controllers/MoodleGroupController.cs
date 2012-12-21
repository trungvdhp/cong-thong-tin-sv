using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using CongThongTinSV.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using CongThongTinSV.App_Lib;

namespace CongThongTinSV.Controllers
{
    public class MoodleGroupController : Controller
    {
        #region Group
        [Authorize(Roles = "MoodleGroup.StudentGroup")]
        public ActionResult StudentGroup()
        {
            return View();
        }

        //[Authorize(Roles = "MoodleGroup.GetGroups")]
        public ActionResult GetGroups([DataSourceRequest] DataSourceRequest request, string id_lop_tc)
        {
            return Json(MoodleLib.GetGroups(id_lop_tc).ToDataSourceResult(request));
        }

        //[Authorize(Roles = "MoodleGroup.GetGroupList")]
        public JsonResult GetGroupList(int id_lop_tc)
        {
            JsonResult result = new JsonResult();
            result.Data = MoodleLib.GetGroupList(id_lop_tc);
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }

        [Authorize(Roles = "MoodleGroup.CreateGroup")]
        public ActionResult CreateGroup(string ten_nhom, string mo_ta, int id_lop_tc)
        {
            List<MoodleGroup> list = new List<MoodleGroup>();
            list.Add(new MoodleGroup
            {
                Ten_nhom = ten_nhom,
                Mo_ta = mo_ta,
                ID_lop_tc = id_lop_tc
            });

            MoodleLib.CreateGroups(list);

            return View();
        }

        [Authorize(Roles = "MoodleGroup.DeleteGroups")]
        public ActionResult DeleteGroups(string selectedVals)
        {
            IEnumerable<string> list = selectedVals.Split(new char[] { ',' });

            if (list.Count() != 0)
            {
                MoodleLib.DeleteGroups(list);
            }

            return View();
        }

        [Authorize(Roles = "MoodleGroup.AddGroupMembers")]
        public ActionResult AddGroupMembers(string selectedVals, string id_lop_tc, string id_nhom)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetEnrolStudents(id_lop_tc).Where(t => t.Tinh_trang == "Đã ghi danh" && s.Contains(t.ID.ToString()) && t.Ten_nhom == "");

            if (list.Count() != 0)
            {
                MoodleLib.AddGroupMembers(list, id_nhom);
            }

            return View();
        }

        [Authorize(Roles = "MoodleGroup.DeleteGroupMembers")]
        public ActionResult DeleteGroupMembers(string selectedVals, string id_lop_tc, string id_nhom)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetEnrolStudents(id_lop_tc).Where(t => s.Contains(t.ID.ToString()) && t.ID_nhom.ToString() == id_nhom);

            if (list.Count() != 0)
            {
                MoodleLib.DeleteGroupMembers(list, id_nhom);
            }

            return View();
        }
        #endregion

        #region Grouping
        //[Authorize(Roles = "MoodleGroup.GetGroupingList")]
        public JsonResult GetGroupingList(int id_lop_tc)
        {
            JsonResult result = new JsonResult();
            result.Data = MoodleLib.GetGroupingList(id_lop_tc);
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }

        [Authorize(Roles = "MoodleGroup.CreateGrouping")]
        public ActionResult CreateGrouping(string ten_to, string mo_ta, int id_lop_tc)
        {
            List<MoodleGroup> list = new List<MoodleGroup>();
            list.Add(new MoodleGroup
            {
                Ten_to = ten_to,
                Mo_ta = mo_ta,
                ID_lop_tc = id_lop_tc
            });

            MoodleLib.CreateGroupings(list);

            return View();
        }

        [Authorize(Roles = "MoodleGroup.DeleteGroupings")]
        public ActionResult DeleteGroupings(string selectedVals)
        {
            IEnumerable<string> list = selectedVals.Split(new char[] { ',' });

            if (list.Count() != 0)
            {
                MoodleLib.DeleteGroupings(list);
            }
            return View();
        }

        [Authorize(Roles = "MoodleGroup.UpdateGrouping")]
        public ActionResult UpdateGrouping(int id_to, string ten_to, string mo_ta)
        {
            List<MoodleGroup> list = new List<MoodleGroup>();
            list.Add(new MoodleGroup
            {
                ID_to = id_to,
                Ten_to = ten_to,
                Mo_ta = mo_ta
            });

            MoodleLib.UpdateGroupings(list);

            return View();
        }

        [Authorize(Roles = "MoodleGroup.AssignGrouping")]
        public ActionResult AssignGrouping(string selectedVals, string id_lop_tc, string id_to)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetGroups(id_lop_tc).Where(t => t.ID_to == 0 && s.Contains(t.ID_nhom.ToString()));

            if (list.Count() != 0)
            {
                MoodleLib.AssignGrouping(list, id_to);
            }

            return View();
        }

        [Authorize(Roles = "MoodleGroup.UnassignGrouping")]
        public ActionResult UnassignGrouping(string selectedVals, string id_lop_tc, string id_to)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetGroups(id_lop_tc).Where(t => t.ID_to.ToString() == id_to && s.Contains(t.ID_nhom.ToString()));

            if (list.Count() != 0)
            {
                MoodleLib.UnassignGrouping(list, id_to);
            }

            return View();
        }
        #endregion
    }
}