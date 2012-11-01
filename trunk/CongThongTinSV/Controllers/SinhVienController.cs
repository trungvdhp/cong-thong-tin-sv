using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CongThongTinSV.Controllers
{
    public class SinhVienController : Controller
    {
        //
        // GET: /SinhVien/

        public ActionResult Index()
        {
            return RedirectToAction("DangKyTC","SinhVien");
        }

        public ActionResult DangKyHocPhan()
        {

            return View();
        }
        public ActionResult YeucauMoLop()
        {
            return View();
        }
    }
}
