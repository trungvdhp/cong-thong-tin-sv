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
    public class CapabilityController : Controller
    {
        [Authorize(Roles = "Admin")]
        public ActionResult Manage()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetCapabilities([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GlobalLib.GetCapabilities().ToDataSourceResult(request));
        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateCapability([DataSourceRequest] DataSourceRequest request, Capability quyen)
        {
            if (quyen != null && ModelState.IsValid)
            {
                if (GlobalLib.CreateCapability(quyen) == -1)
                {
                    ModelState.AddModelError("Error", "Lỗi khi tạo mới quyền: " + quyen.Ten_quyen);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteCapability([DataSourceRequest] DataSourceRequest request, Capability quyen)
        {
            if (quyen != null)
            {
                if (GlobalLib.DeleteCapability(quyen) == -1)
                {
                    ModelState.AddModelError("Error", "Lỗi khi xóa quyền: " + quyen.Ten_quyen);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteCapabilities(string selectedVals)
        {
            IEnumerable<string> ids = selectedVals.Split(new char[] { ',' });
            var data = new Message();

            if (ids.Count() != 0)
            {
                if (GlobalLib.DeleteCapabilities(ids) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi xóa các quyền";
                    data.state = "error";
                }
                else
                {

                    data.title = "Success";
                    data.message = "Xóa các quyền thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateCapability([DataSourceRequest] DataSourceRequest request, Capability quyen)
        {
            var data = new Message();

            if (quyen != null && ModelState.IsValid)
            {
                if (!GlobalLib.UpdateCapability(quyen))
                {
                    ModelState.AddModelError("Error", "Lỗi khi cập nhật quyền: " + quyen.Ten_quyen);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult SyncCapability()
        {
            var data = new Message();

            if (GlobalLib.SyncCapability() == -1)
            {
                data.title = "Error";
                data.message = "Lỗi khi đồng bộ quyền";
                data.state = "error";
            }
            else
            {

                data.title = "Success";
                data.message = "Đồng bộ quyền thành công";
                data.state = "success";
            }

            return Json(data);
        }
    }
}
