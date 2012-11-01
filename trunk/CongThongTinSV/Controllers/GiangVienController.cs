using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CongThongTinSV.Controllers
{
    public class GiangVienController : Controller
    {
        //
        // GET: /GiangVien/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult InDanhSachLop()
        {
            Entities db=new Entities();
            var namhoc = new SelectList(db.ViewNamHoc,"Nam_hoc","Nam_hoc");
            ViewBag.NamHoc = namhoc;
            return View();
        }

    }
}
