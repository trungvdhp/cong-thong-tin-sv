using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CongThongTinSV.Controllers
{
    public class LopHocPhanController : Controller
    {
        //
        // GET: /LopHocPhan/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult InDanhSachLop()
        {
            return View();
        }
        public ActionResult DangKyHocPhan()
        {
            return View();
        }
        public ActionResult YeuCauMoLop()
        {
            return View();
        }
    }
}
