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

        [Authorize(Roles = "MoodleGroup.StudentGroup")]
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

            var data = new Message();

            if ( MoodleLib.CreateGroups(list) == -1)
            {
                data.title = "Error";
                data.message = "Lỗi khi tạo nhóm học viên";
                data.state = "error";
            }
            else
            {
                data.title = "Success";
                data.message = "Tạo nhóm học viên thành công";
                data.state = "success";
            } 

            return Json(data);
        }

        [Authorize(Roles = "MoodleGroup.DeleteGroups")]
        public ActionResult DeleteGroups(string selectedVals)
        {
            IEnumerable<string> list = selectedVals.Split(new char[] { ',' });
            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.DeleteGroups(list) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi xóa nhóm học viên";
                    data.state = "error";
                }
                else
                {
                    data.title = "Success";
                    data.message = "Xóa nhóm học viên thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Authorize(Roles = "MoodleGroup.AddGroupMembers")]
        public ActionResult AddGroupMembers(string selectedVals, string id_lop_tc, string id_nhom)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetEnrolStudents(id_lop_tc).Where(t => t.Trang_thai == true && s.Contains(t.ID.ToString()) && t.Ten_nhom == "");
            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.AddGroupMembers(list, id_nhom) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi thêm thành viên vào nhóm";
                    data.state = "error";
                }
                else
                {
                    data.title = "Success";
                    data.message = "Thêm thành viên vào nhóm thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Authorize(Roles = "MoodleGroup.DeleteGroupMembers")]
        public ActionResult DeleteGroupMembers(string selectedVals, string id_lop_tc, string id_nhom)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetEnrolStudents(id_lop_tc).Where(t => s.Contains(t.ID.ToString()) && t.ID_nhom.ToString() == id_nhom);
            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.DeleteGroupMembers(list, id_nhom) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi bớt các thành viên khỏi nhóm";
                    data.state = "error";
                }
                else
                {
                    data.title = "Success";
                    data.message = "Bớt các thành viên khỏi nhóm thành công";
                    data.state = "success";
                }
            }

            return Json(data);
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

            var data = new Message();

            if (MoodleLib.CreateGroupings(list) == -1)
            {
                data.title = "Error";
                data.message = "Lỗi khi tạo tổ nhóm học viên";
                data.state = "error";
            }
            else
            {
                data.title = "Success";
                data.message = "Tạo tổ nhóm học viên thành công";
                data.state = "success";
            }

            return Json(data);
        }

        [Authorize(Roles = "MoodleGroup.DeleteGroupings")]
        public ActionResult DeleteGroupings(string selectedVals)
        {
            IEnumerable<string> list = selectedVals.Split(new char[] { ',' });
            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.DeleteGroupings(list) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi xóa tổ nhóm học viên";
                    data.state = "error";
                }
                else
                {
                    data.title = "Success";
                    data.message = "Xóa nhóm tổ nhóm học viên thành công";
                    data.state = "success";
                }
            }

            return Json(data);
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

            var data = new Message();

            if (MoodleLib.UpdateGroupings(list) == -1)
            {
                data.title = "Error";
                data.message = "Lỗi khi cập nhật tổ nhóm học viên";
                data.state = "error";
            }
            else
            {
                data.title = "Success";
                data.message = "Cập nhật tổ nhóm học viên thành công";
                data.state = "success";
            }

            return Json(data);
        }

        [Authorize(Roles = "MoodleGroup.AssignGrouping")]
        public ActionResult AssignGrouping(string selectedVals, string id_lop_tc, string id_to)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetGroups(id_lop_tc).Where(t => t.ID_to == 0 && s.Contains(t.ID_nhom.ToString()));
            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.AssignGrouping(list, id_to) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi thêm các nhóm học viên vào tổ";
                    data.state = "error";
                }
                else
                {
                    data.title = "Success";
                    data.message = "Thêm các nhóm học viên vào tổ thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Authorize(Roles = "MoodleGroup.UnassignGrouping")]
        public ActionResult UnassignGrouping(string selectedVals, string id_lop_tc, string id_to)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetGroups(id_lop_tc).Where(t => t.ID_to.ToString() == id_to && s.Contains(t.ID_nhom.ToString()));

            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.UnassignGrouping(list, id_to) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi bớt các nhóm học viên khỏi tổ";
                    data.state = "error";
                }
                else
                {
                    data.title = "Success";
                    data.message = "Bớt các nhóm học viên khỏi tổ thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }
        #endregion
    }
}