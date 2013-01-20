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
        [Authorize(Roles = "MoodleAdmin")]
        public ActionResult Manage()
        {
            return View();
        }

        [Authorize(Roles = "MoodleAdmin")]
        public ActionResult GetWebServices([DataSourceRequest] DataSourceRequest request)
        {
            return Json(MoodleLib.GetWebServices().ToDataSourceResult(request));
        }

        [Authorize(Roles = "MoodleAdmin")]
        public JsonResult GetWebServiceList()
        {
            JsonResult result = new JsonResult();
            result.Data = MoodleLib.GetWebServiceList();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }

        [Authorize(Roles = "MoodleAdmin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateWebService([DataSourceRequest] DataSourceRequest request, MoodleWebService webservice)
        {
            if (webservice != null && ModelState.IsValid)
            {
                MoodleLib.CreateWebService(webservice);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [Authorize(Roles = "MoodleAdmin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteWebService([DataSourceRequest] DataSourceRequest request, MoodleWebService webservice)
        {
            if (webservice != null)
            {
                MoodleLib.DeleteWebService(webservice);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [Authorize(Roles = "MoodleAdmin")]
        public ActionResult DeleteWebServices(string selectedVals)
        {
            IEnumerable<string> ids = selectedVals.Split(new char[] { ',' });
            var data = new Message();

            if (ids.Count() != 0)
            {
                if ( MoodleLib.DeleteWebServices(ids) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi xóa các dịch vụ web";
                    data.state = "error";
                }
                else
                {
                    data.title = "Success";
                    data.message = "Xóa các dịch vụ web thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Authorize(Roles = "MoodleAdmin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateWebService([DataSourceRequest] DataSourceRequest request, MoodleWebService webservice)
        {
            if (webservice != null && ModelState.IsValid)
            {
                MoodleLib.UpdateWebService(webservice);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [Authorize(Roles = "MoodleAdmin")]
        public ActionResult SyncWebService()
        {
            var data = new Message();

            if (MoodleLib.SyncWebService() == -1)
            {
                data.title = "Error";
                data.message = "Lỗi khi đồng bộ các dịch vụ web";
                data.state = "error";
            }
            else
            {
                data.title = "Success";
                data.message = "Đồng bộ các dịch vụ web thành công";
                data.state = "success";
            }

            return Json(data);
        }
    }
}
