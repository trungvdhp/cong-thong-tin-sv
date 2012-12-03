using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using CongThongTinSV.Models;

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
        public ActionResult GuiMonYeuCau(string MaMon)
        {
            return View();
        }
        public ActionResult MonYeuCau([DataSourceRequest]DataSourceRequest request,string filterDiem)
        {
            FormsAuthenticationTicket ticket = ((FormsIdentity)System.Web.HttpContext.Current.User.Identity).Ticket;
            var diem = TraCuuController.GetDiemHocTap(ticket.Name);
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
