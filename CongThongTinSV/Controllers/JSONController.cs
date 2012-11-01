using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CongThongTinSV.Controllers
{
    public class JSONController : Controller
    {
        //
        // GET: /JSON/

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult HocKy_NamHoc(string NamHoc)
        {
            Entities db=new Entities();
            var q = from nh in db.PLAN_HocKyDangKy_TC
                     where nh.Nam_hoc == NamHoc
                     select nh.Hoc_ky;

            JsonResult result = new JsonResult();

            result.Data = new SelectList(q, "Hoc_ky", "Hoc_ky");
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }
    }
}
