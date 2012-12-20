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
    public class MoodleWebServiceController : Controller
    {
        [Authorize(Roles = "MoodleWebService.Manage")]
        public ActionResult Manage()
        {
            return View();
        }

        [Authorize(Roles = "MoodleWebService.GetWebServices")]
        public ActionResult GetWebServices([DataSourceRequest] DataSourceRequest request)
        {
            return Json(MoodleLib.GetWebServices().ToDataSourceResult(request));
        }

        [Authorize(Roles = "MoodleWebService.GetWebServiceList")]
        public JsonResult GetWebServiceList()
        {
            JsonResult result = new JsonResult();
            result.Data = MoodleLib.GetWebServiceList();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }

        [Authorize(Roles = "MoodleWebService.CreateWebService")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateWebService([DataSourceRequest] DataSourceRequest request, MoodleWebService webservice)
        {
            if (webservice != null && ModelState.IsValid)
            {
                MoodleLib.CreateWebService(webservice);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [Authorize(Roles = "MoodleWebService.DeleteWebService")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteWebService([DataSourceRequest] DataSourceRequest request, MoodleWebService webservice)
        {
            if (webservice != null)
            {
                MoodleLib.DeleteWebService(webservice);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [Authorize(Roles = "MoodleWebService.DeleteWebServices")]
        public ActionResult DeleteWebServices(string selectedVals)
        {
            IEnumerable<string> ids = selectedVals.Split(new char[] { ',' });

            if (ids.Count() != 0)
            {
                MoodleLib.DeleteWebServices(ids);
            }

            return View();
        }

        [Authorize(Roles = "MoodleWebService.UpdateWebService")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateWebService([DataSourceRequest] DataSourceRequest request, MoodleWebService webservice)
        {
            if (webservice != null && ModelState.IsValid)
            {
                MoodleLib.UpdateWebService(webservice);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [Authorize(Roles = "MoodleWebService.SyncWebService")]
        public ActionResult SyncWebService()
        {
            MoodleLib.SyncWebService();

            return View();
        }
    }
}
