using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Web.Security;
using CongThongTinSV.App_Lib;

namespace CongThongTinSV.Controllers
{
    public class ThongBaoController : Controller
    {
        //
        // GET: /ThongBao/
        
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GuiThongBao()
        {
            return View();
        }
        public void GuiThongBaoSV(int ID_gui,int ID_nhan, string NoiDung)
        {
            Entities db = new Entities();
            tbl_inbox message = new tbl_inbox() {
               ID_nhan = ID_nhan,
               ID_gui = ID_gui,
               PostDate = DateTime.Now,
               Contents = NoiDung
            };
            db.tbl_inbox.Add(message);
            db.SaveChanges();
            
        }
        
        public void GuiThongBaoLopHP(string Id_gui, string Ma_lop_tc, string NoiDung)
        {
            Entities db = new Entities();
            
            db.SaveChanges();

        }
        public void GuiThongBaoGV(int ID_gui, int ID_nhan, string NoiDung)
        {
            Entities db = new Entities();
            tbl_inbox message = new tbl_inbox()
            {
                ID_nhan = ID_nhan,
                PostDate = DateTime.Now,
                ID_gui = ID_gui,
                Contents = NoiDung
            };
            db.tbl_inbox.Add(message);
            db.SaveChanges();

        }
        public ActionResult Gui(int DoiTuong,string NguoiNhan,string NoiDung)
        {
            Entities db = new Entities();
            Regex regex = new Regex(@"(?<to>[^\s]+)\s*;?");
            MatchCollection matches = regex.Matches(NguoiNhan);    
            var userData= GlobalLib.GetCurrentUserData();
            var ID_gui = Convert.ToInt32(userData.PortalUserID);
            switch (DoiTuong)
            {
                case 1:
                    foreach (Match match in matches)
                    {
                        var sv = db.STU_HoSoSinhVien.First(t=>t.Ma_sv==match.Groups["to"].ToString());
                        GuiThongBaoSV(ID_gui, sv.ID_sv , NoiDung);
                    }
                    return RedirectToAction("GuiThongBao");
                    //break;
                case 2:
                    break;
                case 3:
                    foreach(Match match in matches)
                    {
                        var gv = db.PLAN_GiaoVien.First(t => t.Ma_cb == match.Groups["to"].ToString());
                        GuiThongBaoSV(ID_gui, gv.ID_cb, NoiDung);
                    }
                    break;
            }
            return Json(new {
                Status = "success",
                Message = "Đã gửi thông báo thành công!"
            },JsonRequestBehavior.AllowGet); ;
        }
    }
}
