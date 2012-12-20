using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using CongThongTinSV.Models;
using CongThongTinSV.App_Lib;

namespace CongThongTinSV.Controllers
{
    public class MoodleRoleController : Controller
    {
        public JsonResult GetRoles(long contextLevel)
        {
            JsonResult result = new JsonResult();
            result.Data = MoodleLib.GetRoles(contextLevel);
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }

        public JsonResult GetCourseRoles()
        {
            return GetRoles(50);
        }

        public JsonResult GetSystemRoles()
        {
            return GetRoles(10);
        }
    }
}
