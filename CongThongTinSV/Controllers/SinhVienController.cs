using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongThongTinSV.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace CongThongTinSV.Controllers
{
    public class SinhVienController : Controller
    {
        //
        // GET: /SinhVien/

        public ActionResult Index()
        {
            return RedirectToAction("DangKy","LopHocPhan");
        }

        public ActionResult InDanhSachLop()
        {
            return RedirectToAction("InDanhSachLop", "LopHocPhan");
        }
        public ActionResult DangKyHocPhan()
        {
            return RedirectToAction("DangKyHocPhan", "LopHocPhan");
        }
        public ActionResult YeuCauMoLop()
        {
            return RedirectToAction("YeuCauMoLop", "LopHocPhan");
        }
    }
}
