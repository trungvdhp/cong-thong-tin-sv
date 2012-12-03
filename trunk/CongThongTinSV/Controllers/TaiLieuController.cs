using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CongThongTinSV.Controllers
{
    public class TaiLieuController : Controller
    {
        //
        // GET: /Upload/
        FormsAuthenticationTicket ticket = ((FormsIdentity)System.Web.HttpContext.Current.User.Identity).Ticket;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Upload()
        {
            return View();
        }
        public ActionResult TaiLieuDaUp()
        {
            return View();
        }
        public ActionResult GetLopDay()
        {   
            Entities db=new Entities();
            PLAN_GiaoVien gv=db.PLAN_GiaoVien.First(t=>t.Ma_cb == ticket.Name);
            var lop = db.ViewLopTC.Where(t => t.ID_cb == gv.ID_cb).ToList();

            return View();
        }
    }
}
