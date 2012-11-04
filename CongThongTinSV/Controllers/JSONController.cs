using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CongThongTinSV.Controllers
{
    public class JSONController : Controller
    {
        //
        // GET: /JSON/

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetNamHoc()
        {
            Entities db = new Entities();
            JsonResult result = new JsonResult();
            result.Data = new SelectList(db.ViewNamHoc, "Nam_hoc", "Nam_hoc");
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public JsonResult GetHocKy(string NamHoc)
        {
            Entities db=new Entities();
            var q = (from nh in db.PLAN_HocKyDangKy_TC
                     where nh.Nam_hoc == NamHoc
                     select new
                     {
                         Hoc_ky = nh.Hoc_ky
                     }).Distinct();
            JsonResult result = new JsonResult();

            result.Data = new SelectList(q, "Hoc_ky", "Hoc_ky");
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }
        public JsonResult GetDotHoc(string NamHoc, int HocKy)
        {
            Entities db = new Entities();
            var q = from dh in db.PLAN_HocKyDangKy_TC
                    where dh.Hoc_ky == HocKy && dh.Nam_hoc == NamHoc
                    select dh;
            JsonResult result = new JsonResult();
            result.Data = new SelectList(q, "Ky_dang_ky", "Dot");
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        //public JsonResult GetLopTC(string KyDangKy)
        //{
        //    Entities db=new Entities();
        //    var q=from ctdt in db.PLAN_ChuongTrinhDaoTaoChiTiet
        //          where ctdt.ky
        //}
        public JsonResult GetHeDT()
        {
            Entities db = new Entities();
            JsonResult result = new JsonResult();
            result.Data = new SelectList(db.STU_He, "Id_he", "Ten_he");
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public JsonResult GetKhoa()
        {
            Entities db = new Entities();
            JsonResult result = new JsonResult();
            result.Data = new SelectList(db.STU_Khoa, "Id_khoa", "Ten_khoa");
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public JsonResult GetChuyenNganh()
        {
            Entities db = new Entities();
            JsonResult result = new JsonResult();
            result.Data = new SelectList(db.STU_ChuyenNganh, "Id_chuyen_nganh", "Ten_chuyen_nganh");
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public JsonResult GetKhoaHoc()
        {
            Entities db = new Entities();
            var q = (from ctdt in db.PLAN_ChuongTrinhDaoTao
                     select new
                     {
                         Khoa_hoc = ctdt.Khoa_hoc
                     }).Distinct();
            JsonResult result = new JsonResult();

            result.Data = new SelectList(q, "Khoa_hoc", "Khoa_hoc");
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }
        public JsonResult GetLopTC(int Ky_dang_ky,int ID_khoa,int ID_he,int Khoa_hoc)
        {
            Entities db = new Entities();
            var q1 = from ctdt in db.PLAN_ChuongTrinhDaoTaoChiTiet
                     where ctdt.PLAN_ChuongTrinhDaoTao.ID_khoa == ID_khoa && ctdt.PLAN_ChuongTrinhDaoTao.ID_he == ID_he && ctdt.PLAN_ChuongTrinhDaoTao.Khoa_hoc == Khoa_hoc
                     select ctdt;
            Dictionary<int, PLAN_ChuongTrinhDaoTaoChiTiet> mon = q1.ToDictionary(ct => ct.ID_mon);
            var q = from loptc in db.ViewLopTC
                    where loptc.Ky_dang_ky==Ky_dang_ky
                    select loptc;
            List<ViewLopTC> list = new List<ViewLopTC>();
            foreach (var loptc in q)
            {
                if (mon.ContainsKey(loptc.ID_mon)) list.Add(loptc);
            }

            JsonResult result = new JsonResult();

            result.Data = new SelectList(list, "ID_lop_tc", "Ten_mon");
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
    }
}
