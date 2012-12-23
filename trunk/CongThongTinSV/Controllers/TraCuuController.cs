using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongThongTinSV.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using CongThongTinSV.App_Lib;

namespace CongThongTinSV.Controllers
{
    public class TraCuuController : Controller
    {
        //
        // GET: /TraCuu/

        public ActionResult Index()
        {
            return View();
        }
        public static List<DiemHocTap> GetDiemHocTap(int ID_sv)
        {
            Entities db = new Entities();

            var diem = db.MARK_DiemThanhPhan_TC.Where(t => t.MARK_Diem_TC.ID_sv == ID_sv && t.MARK_ThanhPhanMon_TC.Ky_hieu == "X").Select(t => new DiemHocTap
            {
                Id_diem = t.MARK_Diem_TC.ID_diem,
                Ma_mon = t.MARK_Diem_TC.MARK_MonHoc.Ky_hieu,
                Ten_mon = t.MARK_Diem_TC.MARK_MonHoc.Ten_mon,
                X = t.Diem,
                Hoc_ky = t.Hoc_ky_TP,
                Nam_hoc = t.Nam_hoc_TP
            }).ToList();
            foreach (var d in diem)
            {
                var q=db.MARK_DiemThi_TC.Where(t => t.ID_diem == d.Id_diem && t.Nam_hoc_thi == d.Nam_hoc && t.Hoc_ky_thi == d.Hoc_ky);
                if (q.Count() > 0)
                {
                    var dt = q.First();
                    d.Y = dt.Diem_thi;
                    d.Z = dt.TBCMH;
                    d.Diem_chu = dt.Diem_chu;
                }
            }
            return diem;
        }
        public ActionResult DiemHocTap()
        {
            return View();
        }
        public static int GetIdSv(String TuKhoa)
        {
            Entities db=new Entities();
            TuKhoa =TuKhoa.Replace("  ", " ").Trim();
            string[] buf = TuKhoa.Split(new char[] { ' ' });
            string Ma_sv = buf[0];
            var sv = db.STU_HoSoSinhVien.Where(t => t.Ma_sv == Ma_sv);
            if (sv.Count() == 0) sv = db.STU_HoSoSinhVien.Where(t => t.Ho_ten == TuKhoa);
            if (sv.Count() > 0) return (int)sv.First().ID_sv;
            return 0;

        }
        public ActionResult FilterSinhVien(string TuKhoa)
        {
            Entities db = new Entities();
            TuKhoa = TuKhoa.Trim();
            var list = new List<AutoCompleteData>();
            try
            {
                int.Parse(TuKhoa);
                list = db.STU_HoSoSinhVien.Where(t => t.Ma_sv.StartsWith(TuKhoa)).Take(10).Select(t => new AutoCompleteData
                {
                    Text = t.Ma_sv + " " + t.Ho_ten
                }).ToList();
            }
            catch
            {
                list = db.STU_HoSoSinhVien.Where(t => t.Ho_ten.Contains(TuKhoa)).Take(10).Select(t => new AutoCompleteData
                {
                    Text = t.Ma_sv + " " + t.Ho_ten
                }).ToList();
            }
            JsonResult json = Json(list);
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }
        public ActionResult SinhVien()
        {
            return View();
        }

        public ActionResult GetSinhVien([DataSourceRequest] DataSourceRequest request, int id_chuyen_nganh)
        {

            return Json(MoodleStudents(id_chuyen_nganh).ToDataSourceResult(request));
        }

        public IEnumerable<MoodleStudent> MoodleStudents(int id_chuyen_nganh)
        {
            Entities db = new Entities();

            var sv1 = from ds in db.STU_DanhSach
                      join hs in db.STU_HoSoSinhVien
                      on ds.ID_sv equals hs.ID_sv
                      select new
                      {
                          ds,
                          hs
                      };

            var sv2 = from ds1 in sv1
                      join lop in db.STU_Lop
                      on ds1.ds.ID_lop equals lop.ID_lop
                      where lop.ID_chuyen_nganh == id_chuyen_nganh
                      select new
                      {
                          ds1,
                          lop
                      };

            var sv3 = from ds2 in sv2.AsEnumerable()
                      join gt in db.STU_GioiTinh.AsEnumerable()
                      on ds2.ds1.hs.ID_gioi_tinh equals gt.ID_gioi_tinh
                      select new MoodleStudent
                      {
                          ID_sv = ds2.ds1.ds.ID_sv,
                          Ma_sv = ds2.ds1.hs.Ma_sv,
                          Ho_dem = Utility.GetLastName(ds2.ds1.hs.Ho_ten),
                          Ten = Utility.GetFirstName(ds2.ds1.hs.Ho_ten),
                          Ngay_sinh = ds2.ds1.hs.Ngay_sinh,
                          Gioi_tinh = gt.Gioi_tinh,
                          Lop = ds2.lop.Ten_lop
                      };

            return sv3.ToList();
        }
    }
}
