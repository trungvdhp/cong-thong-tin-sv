using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongThongTinSV.Models;

namespace CongThongTinSV.Controllers
{
    public class KhoaController : Controller
    {
        //
        // GET: /Khoa/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetKhoa()
        {
            Entities db = new Entities();
            JsonResult result = new JsonResult();

            result.Data = new SelectList(db.STU_Khoa, "ID_khoa", "Ten_khoa");
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }
    }
}
