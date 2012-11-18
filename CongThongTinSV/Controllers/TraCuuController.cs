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
    public class TraCuuController : Controller
    {
        //
        // GET: /TraCuu/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DiemHocTap()
        {
            return View();
        }

        public ActionResult SinhVien()
        {
            return View();
        }

        public ActionResult GetSinhVien([DataSourceRequest] DataSourceRequest request, int id_chuyen_nganh)
        {

            return Json(MoodleSinhViens(id_chuyen_nganh).ToDataSourceResult(request));
        }

        public IEnumerable<MoodleSinhVien> MoodleSinhViens(int id_chuyen_nganh)
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
                      select new MoodleSinhVien
                      {
                          ID_sv = ds2.ds1.ds.ID_sv,
                          Ma_sv = ds2.ds1.hs.Ma_sv,
                          Ho_dem = UtilityController.GetLastName(ds2.ds1.hs.Ho_ten),
                          Ten = UtilityController.GetFirstName(ds2.ds1.hs.Ho_ten),
                          Ngay_sinh = ds2.ds1.hs.Ngay_sinh,
                          Gioi_tinh = gt.Gioi_tinh,
                          Lop = ds2.lop.Ten_lop
                      };

            return sv3.ToList();
        }
    }
}
