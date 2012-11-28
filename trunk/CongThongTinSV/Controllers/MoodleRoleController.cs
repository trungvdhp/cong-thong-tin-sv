using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using CongThongTinSV.Models;

namespace CongThongTinSV.Controllers
{
    public class MoodleRoleController : Controller
    {
        //
        // GET: /MoodleRole/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetVaiTro(long contextLevel)
        {
            MoodleEntities mdb = new MoodleEntities();
            JsonResult result = new JsonResult();
            var role = from rc in mdb.fit_role_context_levels
                       join r in mdb.fit_role
                       on rc.roleid equals r.id
                       where rc.contextlevel == contextLevel
                       select r;

            result.Data = new SelectList(role.ToList(), "id", "name");
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }

        public JsonResult GetVaiTroKhoaHoc()
        {
            return GetVaiTro(50);
        }

        public JsonResult GetVaiTroHeThong()
        {
            return GetVaiTro(10);
        }

        public static string[] GetVaiTroKhoaHoc(string idArray, char[] separator)
        {
            MoodleEntities mdb = new MoodleEntities();
            string[] ids = idArray.Split(separator);
            int len = ids.Length;

            for (int i = 0; i < len; i++)
            {
                if (ids[i] == "")
                {
                    continue;
                }

                long id = Convert.ToInt64(ids[i]);
                ids[i] = mdb.fit_role.Single(t => t.id == id).name;
            }

            return ids;
        }
        

    }
}
