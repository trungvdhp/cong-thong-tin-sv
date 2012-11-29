using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongThongTinSV.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Web.Security;

namespace CongThongTinSV.Controllers
{
    public class SinhVienController : Controller
    {
        //
        // GET: /SinhVien/
        FormsAuthenticationTicket ticket = ((FormsIdentity)System.Web.HttpContext.Current.User.Identity).Ticket;
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

        public ActionResult ThongTinCaNhan()
        {
            Entities db = new Entities();
            STU_HoSoSinhVien sv;
            ViewBag.sinhvien = db.STU_HoSoSinhVien.Single(t => t.Ma_sv == ticket.Name);
            return View();
        }
    }
}
