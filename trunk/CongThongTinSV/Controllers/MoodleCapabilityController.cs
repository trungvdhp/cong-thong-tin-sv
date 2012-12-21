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
        [Authorize(Roles = "MoodleCapability.Manage")]
        public ActionResult Manage()
        {
            return View();
        }

        //[Authorize(Roles = "MoodleCapability.GetCapabilities")]
        public ActionResult GetCapabilities([DataSourceRequest] DataSourceRequest request, string id_dv)
        {
            return Json(MoodleLib.GetCapabilities(id_dv).ToDataSourceResult(request));
        }

        [Authorize(Roles = "MoodleCapability.AssignCapabilities")]
        public ActionResult AssignCapabilities(string selectedVals, string id_dv)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetCapabilities(id_dv).Where(t => t.Tinh_trang == "" && s.Contains(t.ID_quyen.ToString()));
            if (list.Count() != 0)
            {
                MoodleLib.AssignCapabilities(list, id_dv);
            }

            return View();
        }

        [Authorize(Roles = "MoodleCapability.UnassignCapabilities")]
        public ActionResult UnassignCapabilities(string selectedVals, string id_dv)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetCapabilities(id_dv).Where(t => t.Tinh_trang != "" && s.Contains(t.ID_quyen.ToString()));
            if (list.Count() != 0)
            {
                MoodleLib.UnassignCapabilities(list, id_dv);
            }

            return View();
        }
    }
}