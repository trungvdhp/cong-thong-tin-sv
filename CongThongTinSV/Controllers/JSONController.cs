using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongThongTinSV.Models;

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
            var lop = db.STU_Lop.First(l => l.Khoa_hoc == Khoa_hoc);
            var kdk = db.PLAN_HocKyDangKy_TC.Single(k => k.Ky_dang_ky == Ky_dang_ky);
            int namhoc=Convert.ToInt32(kdk.Nam_hoc.Split(new string[]{"-"},StringSplitOptions.None)[0]);
            int namnhaphoc = Convert.ToInt32(lop.Nien_khoa.Split(new string[] { "-" }, StringSplitOptions.None)[0]);
            int hocky = 2 * (namhoc - namnhaphoc) + kdk.Hoc_ky;
            var q1 = from ctdt in db.PLAN_ChuongTrinhDaoTaoChiTiet
                     where ctdt.PLAN_ChuongTrinhDaoTao.ID_khoa == ID_khoa 
                     && ctdt.PLAN_ChuongTrinhDaoTao.ID_he == ID_he 
                     && ctdt.PLAN_ChuongTrinhDaoTao.Khoa_hoc == Khoa_hoc
                     && ctdt.Ky_thu==hocky
                     select ctdt;
            Dictionary<int, PLAN_ChuongTrinhDaoTaoChiTiet> mon = new Dictionary<int, PLAN_ChuongTrinhDaoTaoChiTiet>();
            foreach (var m in q1) if (!mon.ContainsKey(m.ID_mon)) mon.Add(m.ID_mon, m);

            var q = from ltc in db.PLAN_LopTinChi_TC
                    select new LopHocPhan
                    {
                        ID_lop_tc = ltc.ID_lop_tc,
                        ID_mon = ltc.ID_mon_tc,
                        Ten_lop = ltc.PLAN_MonTinChi_TC.MARK_MonHoc.Ten_mon + ltc.PLAN_MonTinChi_TC.Ky_hieu_lop_tc + "N" + ltc.STT_lop
                    };
            List<LopHocPhan> list = new List<LopHocPhan>();
            
            foreach (LopHocPhan lophp in q)
            {
                //if (mon.ContainsKey(lophp.ID_mon)) list.Add(lophp);
            }

            JsonResult result = new JsonResult();

            result.Data = new SelectList(list, "ID_lop_tc", "Ten_lop");
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
    }
}
