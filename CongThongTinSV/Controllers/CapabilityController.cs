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
               GlobalLib.CreateCapability(quyen);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteCapability([DataSourceRequest] DataSourceRequest request, Capability quyen)
        {
            if (quyen != null)
            {
                GlobalLib.DeleteCapability(quyen);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteCapabilities(string selectedVals)
        {
            IEnumerable<string> ids = selectedVals.Split(new char[] { ',' });

            if (ids.Count() != 0)
            {
                GlobalLib.DeleteCapabilities(ids);
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateCapability([DataSourceRequest] DataSourceRequest request, Capability quyen)
        {
            if (quyen != null && ModelState.IsValid)
            {
                GlobalLib.UpdateCapability(quyen);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult SyncCapability()
        {
            GlobalLib.SyncCapability();

            return View();
        }
    }
}
