using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CongThongTinSV.Controllers
{
    public class LopController : Controller
    {
        //
        // GET: /Lop/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetLopChuyenNganh(string ID_chuyen_nganh)
        {
            Entities db = new Entities();
            JsonResult result = new JsonResult();
            int cn = Convert.ToInt32(ID_chuyen_nganh);

            result.Data = new SelectList(db.STU_Lop.Where(t => t.ID_chuyen_nganh == cn).OrderBy(o => o.Ten_lop), "ID_lop", "Ten_lop");
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }
    }
}
