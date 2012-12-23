using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongThongTinSV.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

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

            var q1 = from ct in db.PLAN_ChuongTrinhDaoTaoChiTiet
                     join dt in db.PLAN_ChuongTrinhDaoTao on ct.ID_dt equals dt.ID_dt
                     where dt.ID_khoa == ID_khoa 
                     && dt.ID_he == ID_he 
                     && dt.Khoa_hoc == Khoa_hoc
                     && ct.Ky_thu == hocky
                     select ct;

            Dictionary<int, PLAN_ChuongTrinhDaoTaoChiTiet> mon = new Dictionary<int, PLAN_ChuongTrinhDaoTaoChiTiet>();
            foreach (var m in q1) if (!mon.ContainsKey(m.ID_mon)) mon.Add(m.ID_mon, m);

            var q = from ltc in db.ViewLopTC
                    select ltc;
            List<ViewLopTC> list = new List<ViewLopTC>();
            
            foreach (var lophp in q)
            {
                if (mon.ContainsKey(lophp.ID_mon)) list.Add(lophp);
            }

            JsonResult result = new JsonResult();

            result.Data = new SelectList(list, "ID_lop_tc", "Ten_lop_tc");
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public ActionResult GetSinhVienLopTC([DataSourceRequest] DataSourceRequest request, int ID_lop_tc)
        {

            return Json(SinhVienLopTC(ID_lop_tc).ToDataSourceResult(request));
        }
        public static IEnumerable<SinhVien> SinhVienLopTC(int ID_lop_tc)
        {
            Entities db=new Entities();
            
            return db.SP_SinhVienLopTC(ID_lop_tc).Select(sv => new SinhVien
            {
                ID_sv = sv.ID_sv,
                Ho_ten = sv.Ho_ten,
                Lop = sv.Ten_lop,
                Ma_sv = sv.Ma_sv
            }).ToList();
        }
       
        public JsonResult DiemHocTap([DataSourceRequest] DataSourceRequest request, string TuKhoa, string NamHoc, string HocKy)
        {
            int ID_sv = TraCuuController.GetIdSv(TuKhoa);
            Entities db = new Entities();
            int hk = HocKy == "" ? 0 : Convert.ToInt32(HocKy);

            var diem = TraCuuController.GetDiemHocTap(ID_sv);
            if (NamHoc != "") diem = diem.Where(t => t.Nam_hoc == NamHoc).ToList();
            if (hk != 0) diem = diem.Where(t => t.Hoc_ky == hk).ToList();
            return Json(diem.ToDataSourceResult(request));
        }
        public ActionResult GetNamHocTraCuu(string TuKhoa)
        {
            int ID_sv = TraCuuController.GetIdSv(TuKhoa);
            Entities db = new Entities();
            var sv = db.STU_HoSoSinhVien.First(s => s.ID_sv == ID_sv);

            var namhoc = sv.MARK_Diem_TC.Select(t => new
            {
                Nam_hoc = t.Nam_hoc
            }).Distinct().OrderBy(t => t.Nam_hoc).ToList();

            JsonResult result = new JsonResult();
            result.Data = new SelectList(namhoc, "Nam_hoc", "Nam_hoc");
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
        public ActionResult GetHocKyTraCuu(string TuKhoa, string NamHoc)
        {
            int ID_sv = TraCuuController.GetIdSv(TuKhoa);
            Entities db = new Entities();
            var sv = db.STU_HoSoSinhVien.First(s => s.ID_sv == ID_sv);

            var namhoc = sv.MARK_Diem_TC.Where(t => t.Nam_hoc == NamHoc).Select(t => new
            {
                Hoc_ky = t.Hoc_ky
            }).Distinct().OrderBy(t => t.Hoc_ky).ToList();

            JsonResult result = new JsonResult();
            result.Data = new SelectList(namhoc, "Hoc_ky", "Hoc_ky");
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
    }
}
