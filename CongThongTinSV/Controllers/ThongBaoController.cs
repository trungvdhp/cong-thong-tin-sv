using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Web.Security;

namespace CongThongTinSV.Controllers
{
    public class ThongBaoController : Controller
    {
        //
        // GET: /ThongBao/
        FormsAuthenticationTicket ticket = ((FormsIdentity)System.Web.HttpContext.Current.User.Identity).Ticket;
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GuiThongBao()
        {
            return View();
        }
        public void GuiThongBaoSV(string Id_gui,string MaSV, string NoiDung)
        {
            Entities db = new Entities();
            tbl_inbox message = new tbl_inbox() {
               Id_nguoi_nhan = MaSV,
               PostDate = DateTime.Now,
               Contents = NoiDung
            };
            db.tbl_inbox.Add(message);
            db.SaveChanges();
            
        }
        public void GuiThongBaoLopHP(string Id_gui, string MaSV, string NoiDung)
        {
            Entities db = new Entities();
            
            db.SaveChanges();

        }
        public ActionResult Gui(int DoiTuong,string NguoiNhan,string NoiDung)
        {
            Regex regex = new Regex(@"(?<to>[^\s]+)\s*;?");
            MatchCollection matches = regex.Matches(NguoiNhan);    
            switch (DoiTuong)
            {
                case 1:
                    foreach (Match match in matches)
                    {
                        GuiThongBaoSV(ticket.Name, NguoiNhan, NoiDung);
                    }
                    return RedirectToAction("GuiThongBao");
                    //break;
                case 2:
                    break;
                case 3:
                    break;
            }
            return View();
        }
        public ActionResult Moi()
        {
            return View();
        }
    }
}
