using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongThongTinSV.App_Lib;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using CongThongTinSV.Models;

namespace CongThongTinSV.Controllers
{
    public class MoodleCapabilityController : Controller
    {
        [Authorize(Roles = "MoodleAdmin")]
        public ActionResult Manage()
        {
            return View();
        }

        [Authorize(Roles = "MoodleAdmin")]
        public ActionResult GetCapabilities([DataSourceRequest] DataSourceRequest request, string id_dv)
        {
            return Json(MoodleLib.GetCapabilities(id_dv).ToDataSourceResult(request));
        }

        [Authorize(Roles = "MoodleAdmin")]
        public ActionResult AssignCapabilities(string selectedVals, string id_dv)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetCapabilities(id_dv).Where(t => !t.Trang_thai && s.Contains(t.ID_quyen.ToString()));
            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.AssignCapabilities(list, id_dv) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi gán các quyền cho dịch vụ";
                    data.state = "error";
                }
                else
                {

                    data.title = "Success";
                    data.message = "Gán các quyền cho dịch vụ thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Authorize(Roles = "MoodleAdmin")]
        public ActionResult UnassignCapabilities(string selectedVals, string id_dv)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetCapabilities(id_dv).Where(t => t.Trang_thai && s.Contains(t.ID_quyen.ToString()));

            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.UnassignCapabilities(list, id_dv) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi hủy các quyền của dịch vụ";
                    data.state = "error";
                }
                else
                {

                    data.title = "Success";
                    data.message = "Hủy các quyền của dịch vụ thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }
    }
}