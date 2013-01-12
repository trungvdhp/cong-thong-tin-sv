using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.IO;
using CongThongTinSV.Models;
using CongThongTinSV.App_Lib;

namespace CongThongTinSV.Controllers
{
    public class TaiLieuController : Controller
    {

        

        //
        // GET: /Upload/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Sua(int ID_tl)
        {

            var userData = GlobalLib.GetCurrentUserData();
            var ID_cb = Convert.ToInt32(userData.PortalUserID);
            Entities db = new Entities();
            var q = db.POR_TaiLieu.Where(t => t.ID_tl == ID_tl && t.ID_tl == ID_tl);
            if (q.Count() > 0)
            {
                ViewBag.TaiLieu = q.Select(t => new TaiLieuViewModel
                {
                    ID_tl = t.ID_tl,
                    //Ten_file = t.Ten_file,
                    Ten_tl = t.Ten_tl,
                    Tac_gia = t.Tac_gia,
                    //Ten_gv = t.PLAN_GiaoVien.Ho_ten,
                    //Ngay_up = (DateTime)t.Ngay_up,
                    //URL = t.URL,
                    Mo_ta = t.Mo_ta
                }).ToList().First();
            }
            return View();
        }
        public ActionResult Upload(HttpPostedFileBase file,string Ten_tl,string Tac_gia,string Mo_ta)
        {
            string str = Path.GetFileName(file.FileName);
            string uploadPath = HttpContext.Server.MapPath("../") + "/Content/Resource/";
            var fileName = DateTime.Now.Ticks + "_"+ Utility.RemoveSign4VietnameseString(str);
            file.SaveAs(uploadPath + fileName);
            Entities db = new Entities();
            var userData = GlobalLib.GetCurrentUserData();
            db.POR_TaiLieu.Add(new POR_TaiLieu()
            {
                Ten_tl =Ten_tl,
                Tac_gia=Tac_gia,
                Mo_ta = Mo_ta,
                ID_cb = userData.PortalUserID,
                Ngay_up = DateTime.Now,
                Ten_file = str,
                URL = fileName
            });
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult TaiLieuDaUp([DataSourceRequest]DataSourceRequest request)
        {
            var userData = GlobalLib.GetCurrentUserData();
            var ID_cb = Convert.ToInt32(userData.PortalUserID);
            Entities db=new Entities();

            var tl = db.POR_TaiLieu.Where(t => t.ID_cb==ID_cb).Select(t => new TaiLieuViewModel { 
                ID_tl = t.ID_tl,
                Ten_tl = t.Ten_tl,
                Ten_file = t.Ten_file,
                Tac_gia = t.Tac_gia,
                Ngay_up = (DateTime)t.Ngay_up,
                Ten_gv = t.PLAN_GiaoVien.Ho_ten,
                URL = t.URL
            }).ToList();
            return Json(tl.ToDataSourceResult(request));
        }

        public ActionResult Xoa([DataSourceRequest] DataSourceRequest request,TaiLieuViewModel tailieu)
        {
            Entities db = new Entities();
            string uploadPath = HttpContext.Server.MapPath("../") + "/Content/Resource/";
            var userData= GlobalLib.GetCurrentUserData();
            int ID_cb = Convert.ToInt32(userData.PortalUserID);
            var tl = db.POR_TaiLieu.Single(t => t.ID_tl == tailieu.ID_tl && t.ID_cb==ID_cb);
            FileInfo f=new FileInfo(uploadPath+tl.URL);
            f.Delete();
            db.POR_TaiLieu.Remove(tl);
            db.SaveChanges();
            return Json(ModelState.ToDataSourceResult());
        }
        public ActionResult CapNhat(TaiLieuViewModel tailieu)
        {
            Entities db = new Entities();
            var userData = GlobalLib.GetCurrentUserData();
            int ID_cb = Convert.ToInt32(userData.PortalUserID);
            var q = db.POR_TaiLieu.Where(t => t.ID_tl == tailieu.ID_tl && t.ID_cb == ID_cb);
            if (q.Count() > 0)
            {
                var tl = q.First();
                tl.Ten_tl = tailieu.Ten_tl;
                tl.Tac_gia = tailieu.Tac_gia;
                tl.Mo_ta = tailieu.Mo_ta;
                db.SaveChanges();
                ViewBag.Message = new
                {
                    Status="success",
                    Message="Cập nhật thành công!"
                };
            }
            else
                ViewBag.Message = new
                {
                    Status = "error",
                    Message = "Cập nhật thành công!"
                };
            
            return Json(ViewBag.Message);
        }

        public ActionResult ChiaSe()
        {
            return View();
        }

        public ActionResult TaiLieu2Lop(int ID_lop_tc,int ID_tl)
        {
            Entities db = new Entities();
            var userData = GlobalLib.GetCurrentUserData();
            db.POR_LopTC_TaiLieu.Add(new POR_LopTC_TaiLieu()
            {
                ID_lop_tc = ID_lop_tc,
                ID_tl = ID_tl,
                ID_cb = Convert.ToInt32(userData.PortalUserID)
            });
            db.SaveChanges();
            return View();
        }

        public ActionResult GetLopDay()
        {
            Entities db = new Entities();
            FormsAuthenticationTicket ticket = ((FormsIdentity)System.Web.HttpContext.Current.User.Identity).Ticket;
            var gv = db.PLAN_GiaoVien.Single(t => t.Ma_cb == ticket.Name);
            var lops = db.ViewLopTC.Where(t => t.ID_cb == gv.ID_cb).Select(t=>new{
                ID_lop_tc = t.ID_lop_tc,
                Ten_lop_tc = t.Ten_lop_tc
            }).ToList();
            JsonResult json = Json(lops);
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }
        public ActionResult GetTaiLieuChiaSe(int ID_lop_tc)
        {
            FormsAuthenticationTicket ticket = ((FormsIdentity)System.Web.HttpContext.Current.User.Identity).Ticket;
            Entities db = new Entities();

            var tl_uploaded = db.POR_TaiLieu.Where(t => t.PLAN_GiaoVien.Ma_cb == ticket.Name).Select(t => new
            {
                ID_tl = t.ID_tl,
                Ten_tl = t.Ten_tl,
            }).ToList();
            var tl_shared = db.POR_LopTC_TaiLieu.Where(t => t.ID_lop_tc == ID_lop_tc).Select(t => new
            {
                ID_tl = t.ID_tl
            }).ToList();
            
            foreach (var tl in tl_shared)
            {
                tl_uploaded.Remove(tl_uploaded.Single(t => t.ID_tl == tl.ID_tl));
            }
            JsonResult json = Json(tl_uploaded);
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }
        public ActionResult GetTaiLieuDaChiaSe([DataSourceRequest]DataSourceRequest request,int ID_lop_tc)
        {
            FormsAuthenticationTicket ticket = ((FormsIdentity)System.Web.HttpContext.Current.User.Identity).Ticket;
            Entities db=new Entities();
            var tl_shared = db.POR_LopTC_TaiLieu.Where(t => t.ID_lop_tc == ID_lop_tc).Select(t => new TaiLieuViewModel
            {
                ID_tl = (int)t.ID_tl,
                Ten_tl = t.POR_TaiLieu.Ten_tl,
                Tac_gia = t.POR_TaiLieu.Tac_gia,
                Ngay_up = (DateTime)t.POR_TaiLieu.Ngay_up,
                Ten_file = t.POR_TaiLieu.Ten_file,
                URL = t.POR_TaiLieu.URL,
            }).ToList();
            JsonResult json = Json(tl_shared.ToDataSourceResult(request));
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }
    }
}
