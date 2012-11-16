using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongThongTinSV.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Web.Script.Serialization;

namespace CongThongTinSV.Controllers
{
    public class MoodleEnrolController : Controller
    {
        // GET: /MoodleEnrol/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GhiDanh()
        {
            return View();
        }

        public ActionResult GetHocVien([DataSourceRequest] DataSourceRequest request, int id_lop_tc)
        {

            return Json(MoodleHocVienDiems(id_lop_tc).ToDataSourceResult(request));
        }

        public IEnumerable<MoodleSinhVien> MoodleHocVienDiems(int id_lop_tc)
        {
            Entities db = new Entities();

            var loptc = (from hocky in db.PLAN_HocKyDangKy_TC
                         join lop1 in db.MOD_LopTinChi_TC.Where(t => t.ID_moodle == id_lop_tc)
                         on hocky.Ky_dang_ky equals lop1.MOD_HocKy.Ky_dang_ky
                         join lop2 in db.PLAN_LopTinChi_TC
                         on lop1.ID_moodle equals lop2.ID_lop_tc
                         select new
                         {
                             lop2.PLAN_MonTinChi_TC.ID_mon,
                             hocky.Hoc_ky,
                             hocky.Nam_hoc
                         }).FirstOrDefault();

            var danhsachdiem = from dtc in db.MARK_Diem_TC
                               join dtp in db.MARK_DiemThanhPhan_TC
                               on dtc.ID_diem equals dtp.ID_diem
                               where dtc.ID_mon == loptc.ID_mon
                                   && dtp.Hoc_ky_TP == loptc.Hoc_ky
                                   && dtp.Nam_hoc_TP == loptc.Nam_hoc
                                   && dtp.ID_thanh_phan == 1
                               select new
                               {
                                   dtc.ID_sv,
                                   dtp.Diem
                               };
            var hocvien = MoodleHocViens(id_lop_tc);

            var hocviendiem = from ds in hocvien
                         join d in danhsachdiem
                         on ds.ID_sv equals d.ID_sv
                         into diem
                         from d1 in diem.DefaultIfEmpty()
                      select new MoodleSinhVien
                      {
                          ID_moodle = ds.ID_moodle,
                          Ma_sv = ds.Ma_sv,
                          Ho_dem = ds.Ho_dem,
                          Ten = ds.Ten,
                          Ngay_sinh = ds.Ngay_sinh,
                          Gioi_tinh = ds.Gioi_tinh,
                          Lop = ds.Lop,
                          ID = ds.ID,
                          DiemX =  (d1 == null ? 0 : d1.Diem),
                          Ghi_danh = ds.Ghi_danh,
                          Ten_nhom = ds.Ten_nhom
                      };

            return hocviendiem.OrderByDescending(t => t.Ghi_danh).ToList();
        }

        public IEnumerable<MoodleSinhVien> MoodleHocViens(int id_lop_tc)
        {
            Entities db = new Entities();
            var sinhvien = MoodleSinhViens(id_lop_tc);
            var hocvien = from ds in sinhvien
                          join dk1 in db.MOD_DanhSachLopTinChi
                          on ds.ID equals dk1.ID
                          into dky
                          from dk in dky.DefaultIfEmpty()
                          select new MoodleSinhVien
                          {
                              ID_moodle = ds.ID_moodle,
                              Ma_sv = ds.Ma_sv,
                              Ho_dem = ds.Ho_dem,
                              Ten = ds.Ten,
                              Ngay_sinh = ds.Ngay_sinh,
                              Gioi_tinh = ds.Gioi_tinh,
                              Lop = ds.Lop,
                              ID = ds.ID,
                              Ghi_danh = (dk == null ? false : true),
                              Ten_nhom = (dk == null ? "" : dk.MOD_NhomHocVien.Ten_nhom)
                          };

            return hocvien.ToList();
        }
        public IEnumerable<MoodleSinhVien> MoodleSinhViens(int id_lop_tc)
        {
            Entities db = new Entities();

            var loptc = db.MOD_LopTinChi_TC.FirstOrDefault(t => t.ID_moodle == id_lop_tc);

            var dangky = from dk in db.STU_DanhSachLopTinChi
                         where dk.ID_lop_tc == loptc.ID_lop_tc
                         select new
                         {
                             ID = dk.ID,
                             ID_sv = dk.ID_sv
                         };

            var sv1 = from dk in dangky
                      join ds in db.STU_DanhSach
                      on dk.ID_sv equals ds.ID_sv
                      join hs in db.STU_HoSoSinhVien
                      on dk.ID_sv equals hs.ID_sv
                      join gt in db.STU_GioiTinh
                      on hs.ID_gioi_tinh equals gt.ID_gioi_tinh
                      select new
                      {
                          dk.ID,
                          dk.ID_sv,
                          ds.ID_lop,
                          hs.Ma_sv,
                          hs.Ho_ten,
                          hs.Ngay_sinh,
                          gt.Gioi_tinh
                      };

            var sv2 = from ds in sv1
                      join lop in db.STU_Lop
                      on ds.ID_lop equals lop.ID_lop
                      select new
                      {
                          ds.ID,
                          ds.ID_sv,
                          ds.Ma_sv,
                          ds.Ho_ten,
                          ds.Ngay_sinh,
                          ds.Gioi_tinh,
                          lop.Ten_lop
                      };

            var sv3 = from ds in sv2.AsEnumerable()
                      join nd1 in db.MOD_NguoiDung.AsEnumerable()
                      on ds.ID_sv equals nd1.ID_nd
                      into nguoidung
                      from nd in nguoidung.DefaultIfEmpty()
                      select new MoodleSinhVien
                      {
                          ID_sv = ds.ID_sv,
                          ID_moodle = (nd == null ? 0 : nd.ID_moodle),
                          Ma_sv = ds.Ma_sv,
                          Ho_dem = UtilityController.GetLastName(ds.Ho_ten),
                          Ten = UtilityController.GetFirstName(ds.Ho_ten),
                          Ngay_sinh = ds.Ngay_sinh,
                          Gioi_tinh = ds.Gioi_tinh,
                          Lop = ds.Ten_lop,
                          ID = ds.ID,
                      };

            return sv3.ToList();
        }

        public ActionResult CreateSinhVien(string selectedVals, string id_lop_tc)
        {
            Entities db = new Entities();
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleSinhViens(Convert.ToInt32(id_lop_tc)).Where(t1 => t1.ID_moodle == 0 && s.Contains(t1.ID.ToString())).ToList();

            //ViewBag.SelectedIds = new SelectList(list, "Ma_sv", "ID_moodle");
            //ViewBag.Result = new SelectList(list, "ID_moodle", "Ma_sv");

            if (list.Count() == 0) return View();

            MoodleUserController.CreateSinhVien(list);

            return View();
        }

        public ActionResult DeleteSinhVien(string selectedVals, string id_lop_tc)
        {
            Entities db = new Entities();
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleSinhViens(Convert.ToInt32(id_lop_tc)).Where(t1 => t1.ID_moodle > 0 && s.Contains(t1.ID.ToString())).ToList();

            //ViewBag.SelectedIds = new SelectList(list, "Ma_sv", "ID_moodle");
            //ViewBag.Result = new SelectList(list, "ID_moodle", "Ma_sv");

            if (list.Count() == 0) return View();

            MoodleUserController.DeleteSinhVien(list);

            return View();
        }
    }
}
