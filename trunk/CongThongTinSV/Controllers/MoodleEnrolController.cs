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

        public ActionResult GhiDanhSinhVien()
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
                          ID_sv = ds.ID_sv,
                          ID_lop_tc = ds.ID_lop_tc,
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
                          ID_nhom = ds.ID_nhom,
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
                              ID_sv = ds.ID_sv,
                              ID_lop_tc = ds.ID_lop_tc,
                              ID_moodle = ds.ID_moodle,
                              Ma_sv = ds.Ma_sv,
                              Ho_dem = ds.Ho_dem,
                              Ten = ds.Ten,
                              Ngay_sinh = ds.Ngay_sinh,
                              Gioi_tinh = ds.Gioi_tinh,
                              Lop = ds.Lop,
                              ID = ds.ID,
                              Ghi_danh = (dk == null ? false : true),
                              ID_nhom = (dk == null || dk.MOD_NhomHocVien == null ? null : (int?)dk.MOD_NhomHocVien.ID_nhom),
                              Ten_nhom = (dk == null || dk.MOD_NhomHocVien == null ? "" : dk.MOD_NhomHocVien.Ten_nhom)
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
                             ID_sv = dk.ID_sv,
                             ID_lop_tc = id_lop_tc
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
                          dk.ID_lop_tc,
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
                          ds.ID_lop_tc,
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
                      where nd == null || (nd != null && nd.ID_nhom_nd == 3)
                      select new MoodleSinhVien
                      {
                          ID_sv = ds.ID_sv,
                          ID_lop_tc = ds.ID_lop_tc,
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
            var list = MoodleSinhViens(Convert.ToInt32(id_lop_tc)).Where(t => t.ID_moodle == 0 && s.Contains(t.ID.ToString())).ToList();

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
            var list = MoodleSinhViens(Convert.ToInt32(id_lop_tc)).Where(t => t.ID_moodle > 0 && s.Contains(t.ID.ToString())).ToList();

            //ViewBag.SelectedIds = new SelectList(list, "Ma_sv", "ID_moodle");
            //ViewBag.Result = new SelectList(list, "ID_moodle", "Ma_sv");

            if (list.Count() == 0) return View();

            MoodleUserController.DeleteSinhVien(list);

            return View();
        }

        public static void CreateGhiDanhSinhVien(List<MoodleSinhVien> list)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "wsfunction=enrol_manual_enrol_users";

            foreach (MoodleSinhVien item in list)
            {
                postData += "&enrolments[" + i + "][roleid]=5";
                postData += "&enrolments[" + i + "][userid]=" + item.ID_moodle;
                postData += "&enrolments[" + i + "][courseid]=" + item.ID_lop_tc;
                postData += "&enrolments[" + i + "][timestart]=" + UtilityController.ConvertToTimestamp(DateTime.Now);
                //postData += "&enrolments[" + i + "][timeend]=0";
                //postData += "&enrolments[" + i + "][suspend]=0";
                i++;
            }

            WebRequestController web = new WebRequestController(4, "POST", postData);
            string response = web.GetResponse();
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //MoodleException moodleError = new MoodleException();

            if (response.Contains("exception"))
            {
                // Error
                //moodleError = serializer.Deserialize<MoodleException>(rs);
            }
            else
            {
                // Good
                //results = serializer.Deserialize<List<MoodleCreateUserResponse>>(response);
                i = 0;

                foreach (MoodleSinhVien item in list)
                {
                    MOD_DanhSachLopTinChi entity = new MOD_DanhSachLopTinChi();

                    entity.ID = item.ID;
                    entity.ID_sv = item.ID_moodle;
                    entity.ID_lop_tc = item.ID_lop_tc;

                    db.MOD_DanhSachLopTinChi.Add(entity);
                    i++;
                }

                db.SaveChanges();
            }

            UtilityController.WriteTextToFile("D:\\GhiDanhCreate.txt", response);
        }

        public ActionResult CreateGhiDanhSinhVien(string selectedVals, string id_lop_tc)
        {

            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleHocVienDiems(Convert.ToInt32(id_lop_tc)).Where(t => t.ID_moodle > 0 && t.Ghi_danh == false && s.Contains(t.ID.ToString())).ToList();

            if (list.Count() == 0) return View();

            CreateGhiDanhSinhVien(list);

            return View();
        }

        public static void DeleteGhiDanhSinhVien(List<MoodleSinhVien> list)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "wsfunction=enrol_manual_enrol_users";

            foreach (MoodleSinhVien item in list)
            {
                postData += "&enrolments[" + i + "][roleid]=5";
                postData += "&enrolments[" + i + "][userid]=" + item.ID_moodle;
                postData += "&enrolments[" + i + "][courseid]=" + item.ID_lop_tc;
                postData += "&enrolments[" + i + "][suspend]=1";
                i++;
            }

            WebRequestController web = new WebRequestController(4, "POST", postData);
            string response = web.GetResponse();
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //MoodleException moodleError = new MoodleException();

            if (response.Contains("exception"))
            {
                // Error
                //moodleError = serializer.Deserialize<MoodleException>(rs);
            }
            else
            {
                // Good
                //results = serializer.Deserialize<List<MoodleCreateUserResponse>>(response);
                i = 0;

                foreach (MoodleSinhVien item in list)
                {
                    MOD_DanhSachLopTinChi entity = db.MOD_DanhSachLopTinChi.FirstOrDefault(t => t.ID == item.ID);
                    db.MOD_DanhSachLopTinChi.Remove(entity);
                    i++;
                }

                db.SaveChanges();
            }

            UtilityController.WriteTextToFile("D:\\GhiDanhDelete.txt", response);
        }

        public ActionResult DeleteGhiDanhSinhVien(string selectedVals, string id_lop_tc)
        {

            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleHocVienDiems(Convert.ToInt32(id_lop_tc)).Where(t => t.Ghi_danh == true && s.Contains(t.ID.ToString())).ToList();

            if (list.Count() == 0) return View();

            DeleteGhiDanhSinhVien(list);

            return View();
        }

        public ActionResult AddThanhVien(string selectedVals, string id_lop_tc, string id_nhom)
        {

            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleHocVienDiems(Convert.ToInt32(id_lop_tc)).Where(t => t.Ghi_danh == true && s.Contains(t.ID.ToString()) && t.Ten_nhom == "").ToList();

            if (list.Count() == 0) return View();

            MoodleGroupController.AddThanhVien(list, id_nhom);

            return View();
        }

        public ActionResult DeleteThanhVien(string selectedVals, string id_lop_tc, string id_nhom)
        {
            Entities db = new Entities();
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleHocVienDiems(Convert.ToInt32(id_lop_tc)).Where(t => s.Contains(t.ID.ToString()) && t.ID_nhom.ToString() == id_nhom).ToList();

            if (list.Count() == 0) return View();

            MoodleGroupController.DeleteThanhVien(list, id_nhom);

            return View();
        }
    }
}
