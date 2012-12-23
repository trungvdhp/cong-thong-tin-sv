using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using CongThongTinSV.Models;
using CongThongTinSV.App_Lib;

namespace CongThongTinSV.Controllers
{
    public class LopHocPhanController : Controller
    {
        //
        // GET: /Course/

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
        public ActionResult GuiMonYeuCau(string MaMon)
        {
            FormsAuthenticationTicket ticket = ((FormsIdentity)System.Web.HttpContext.Current.User.Identity).Ticket;
            Entities db=new Entities();
            POR_YeuCauMoLop yc = new POR_YeuCauMoLop()
            {
                ID_sv = db.STU_HoSoSinhVien.Where(t=>t.Ma_sv==ticket.Name).First().ID_sv,
                ID_mon = db.MARK_MonHoc.Where(t=>t.Ky_hieu==MaMon).First().ID_mon,
                Ngay_yeu_cau = DateTime.Now,
            };
            db.POR_YeuCauMoLop.Add(yc);
            db.SaveChanges();
            return View();
        }
        public ActionResult MonYeuCau([DataSourceRequest]DataSourceRequest request,string filterDiem)
        {
            var ID_sv = GlobalLib.GetCurrentUserData().PortalUserID;
            var diem = TraCuuController.GetDiemHocTap(ID_sv);
            var diem_max = new Dictionary<string,DiemHocTap>();
            foreach (var d in diem)
            {
                if (!diem_max.ContainsKey(d.Ma_mon)) diem_max.Add(d.Ma_mon, d);
                else
                {
                    if (diem_max[d.Ma_mon].Z <= d.Z)
                    {
                        diem_max.Remove(d.Ma_mon);
                        diem_max.Add(d.Ma_mon, d);
                    }
                }
            }
            diem = diem_max.Values.ToList();
            if (filterDiem != "")
                diem = diem.Where(t => t.Z < Convert.ToDouble(filterDiem)).ToList();
            return Json(diem.ToDataSourceResult(request));
        }
        public ActionResult YeuCauMoLop()
        {
            
            return View();
        }
        public ActionResult MoLopHocPhan()
        {
            return View();
        }

    }
}
