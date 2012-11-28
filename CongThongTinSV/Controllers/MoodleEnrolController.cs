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

                foreach (MoodleSinhVien item in list)
                {
                    MOD_DanhSachLopTinChi entity = new MOD_DanhSachLopTinChi();

                    entity.ID = item.ID;
                    entity.ID_sv = item.ID_moodle;
                    entity.ID_lop_tc = item.ID_lop_tc;

                    db.MOD_DanhSachLopTinChi.Add(entity);
                }

                db.SaveChanges();
            }

            UtilityController.WriteTextToFile("D:\\GhiDanhSinhVienCreate.txt", response);
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

                foreach (MoodleSinhVien item in list)
                {
                    MOD_DanhSachLopTinChi entity = db.MOD_DanhSachLopTinChi.FirstOrDefault(t => t.ID == item.ID);
                    db.MOD_DanhSachLopTinChi.Remove(entity);
                }

                db.SaveChanges();
            }

            UtilityController.WriteTextToFile("D:\\GhiDanhSinhVienDelete.txt", response);
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

        public ActionResult GhiDanhGiaoVien()
        {
            return View();
        }

        public ActionResult GetGiaoVien([DataSourceRequest] DataSourceRequest request, int id_lop_tc)
        {
            return Json(MoodleGiaoViens(id_lop_tc).ToDataSourceResult(request));
        }

        public IEnumerable<MoodleGiaoVien> MoodleGiaoViens(int id_lop_tc)
        {
            Entities db = new Entities();
            MoodleEntities mdb = new MoodleEntities();

            var danhsach = from ds in db.MOD_NguoiDung_VaiTro_LopTinChi.AsEnumerable()
                           where ds.ID_lop_tc == id_lop_tc
                           select new 
                           {
                               UserID = ds.UserID,
                               ID_lop_tc = ds.ID_lop_tc,
                               ID_vai_tro = ds.ID_vai_tro,
                               Vai_tro = string.Join(("\n"), MoodleRoleController.GetVaiTroKhoaHoc(ds.ID_vai_tro, new char[]{','})),
                               Dinh_chi = ds.Dinh_chi
                           };

            var giaovien = from gv1 in db.MOD_NguoiDung.AsEnumerable()
                           join gv2 in db.PLAN_GiaoVien.AsEnumerable()
                           on gv1.ID_nd equals gv2.ID_cb
                           join gt in db.STU_GioiTinh.AsEnumerable()
                           on gv2.ID_gioi_tinh equals gt.ID_gioi_tinh
                           where gv1.ID_nhom_nd == 2
                           select new
                           {
                               ID_cb = gv2.ID_cb,
                               ID_khoa = gv2.ID_khoa,
                               ID_moodle = gv1.ID_moodle,
                               Ma_cb = gv2.Ma_cb,
                               Ho_dem = UtilityController.GetLastName(gv2.Ho_ten),
                               Ten = UtilityController.GetFirstName(gv2.Ho_ten),
                               Ngay_sinh = gv2.Ngay_sinh,
                               Gioi_tinh = gt.Gioi_tinh
                           };

            var q = from gv1 in giaovien.AsEnumerable()
                    join khoa in db.STU_Khoa.AsEnumerable()
                    on gv1.ID_khoa equals khoa.ID_khoa
                    join gv2 in danhsach.AsEnumerable()
                    on gv1.ID_moodle equals gv2.UserID
                    into danhsach1
                    from gv in danhsach1.DefaultIfEmpty()
                    select new MoodleGiaoVien
                    {
                        ID_lop_tc = id_lop_tc,
                        ID_cb = gv1.ID_cb,
                        ID_moodle = gv1.ID_moodle,
                        Ma_cb = gv1.Ma_cb,
                        Ho_dem = gv1.Ho_dem,
                        Ten = gv1.Ten,
                        Khoa = khoa.Ten_khoa,
                        Ngay_sinh = gv1.Ngay_sinh,
                        Gioi_tinh = gv1.Gioi_tinh,
                        ID_vai_tro = (gv == null ? "" : gv.ID_vai_tro),
                        Vai_tro = (gv == null ? "" : gv.Vai_tro),
                        Tinh_trang = (gv == null ? "Chưa ghi danh" : gv.Dinh_chi ? "Chưa kích hoạt":"Đã kích hoạt")
                    };

            return q.OrderByDescending(t => t.ID_vai_tro).ToList();
        }

        public static void CreateGhiDanhGiaoVien(List<MoodleGiaoVien> list, string id_vai_tro, string suspended)
        {
            Entities db = new Entities();
            int i = 0;

            string postData = "wsfunction=enrol_manual_enrol_users";

            foreach (MoodleGiaoVien item in list)
            {
                postData += "&enrolments[" + i + "][roleid]=" + id_vai_tro;
                postData += "&enrolments[" + i + "][userid]=" + item.ID_moodle;
                postData += "&enrolments[" + i + "][courseid]=" + item.ID_lop_tc;
                postData += "&enrolments[" + i + "][timestart]=" + UtilityController.ConvertToTimestamp(DateTime.Now);
                //postData += "&enrolments[" + i + "][timeend]=0";
                postData += "&enrolments[" + i + "][suspend]="  + suspended;
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

                foreach (MoodleGiaoVien item in list)
                {
                    MOD_NguoiDung_VaiTro_LopTinChi entity;

                    if (item.Tinh_trang == "Chưa ghi danh")
                    {
                        entity = new MOD_NguoiDung_VaiTro_LopTinChi();
                        entity.UserID = item.ID_moodle;
                        entity.ID_vai_tro = "" + id_vai_tro;
                        entity.ID_lop_tc = item.ID_lop_tc;
                        entity.Dinh_chi = suspended == "0" ? false : true;
                        db.MOD_NguoiDung_VaiTro_LopTinChi.Add(entity);
                    }
                    else
                    {
                        entity = db.MOD_NguoiDung_VaiTro_LopTinChi.SingleOrDefault(t => t.ID_lop_tc == item.ID_lop_tc && t.UserID == item.ID_moodle);
                        if (!UtilityController.InArray(item.ID_vai_tro, new char[] { ',' }, id_vai_tro))
                        {
                            if (entity.ID_vai_tro == "")
                            {
                                entity.ID_vai_tro = id_vai_tro;
                            }
                            else
                            {
                                entity.ID_vai_tro += "," + id_vai_tro;
                            }

                        }
                        entity.Dinh_chi = suspended == "0" ? false : true;

                        db.Entry(entity).State = System.Data.EntityState.Modified;
                    }
                }

                db.SaveChanges();
            }

            UtilityController.WriteTextToFile("D:\\GhiDanhGiaoVienCreate.txt", response);
        }

        public ActionResult CreateGhiDanhGiaoVien(string selectedVals, string id_lop_tc, string id_vai_tro, string suspended)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleGiaoViens(Convert.ToInt32(id_lop_tc)).Where(t => s.Contains(t.ID_moodle.ToString())).ToList();

            if (list.Count() == 0) return View();

            CreateGhiDanhGiaoVien(list, id_vai_tro, suspended);

            return View();
        }

        public static void UnassignVaiTroGiaoVien(List<MoodleGiaoVien> list, string id_vai_tro)
        {
            Entities db = new Entities();
            int i = 0;

            string postData = "wsfunction=core_role_unassign_roles";

            foreach (MoodleGiaoVien item in list)
            {
                postData += "&unassignments[" + i + "][roleid]=" + id_vai_tro;
                postData += "&unassignments[" + i + "][userid]=" + item.ID_moodle;
                postData += "&unassignments[" + i + "][contextid]=" + UtilityController.GetContextID(50, Convert.ToInt64(item.ID_lop_tc));
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

                foreach (MoodleGiaoVien item in list)
                {
                    MOD_NguoiDung_VaiTro_LopTinChi entity;
                    entity = db.MOD_NguoiDung_VaiTro_LopTinChi.SingleOrDefault(t => t.ID_lop_tc == item.ID_lop_tc && t.UserID == item.ID_moodle);
                    List<string> vaitro = entity.ID_vai_tro.Split(new char[]{','}).ToList();

                    if (vaitro.Remove(id_vai_tro))
                    {
                        entity.ID_vai_tro = string.Join(",", vaitro);
                    }

                    db.Entry(entity).State = System.Data.EntityState.Modified;
                }

                db.SaveChanges();
            }

            UtilityController.WriteTextToFile("D:\\GhiDanhGiaoVien_Unassign_VaiTro.txt", response);
        }

        public ActionResult UnassignVaiTroGiaoVien(string selectedVals, string id_lop_tc, string id_vai_tro)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleGiaoViens(Convert.ToInt32(id_lop_tc)).Where(t => s.Contains(t.ID_moodle.ToString()) && UtilityController.InArray(t.ID_vai_tro, new char[] { ',' }, id_vai_tro)).ToList();

            if (list.Count() == 0) return View();

            UnassignVaiTroGiaoVien(list, id_vai_tro);

            return View();
        }
            
        public static void UnassignAllVaiTroGiaoVien(List<MoodleGiaoVien> list)
        {
            Entities db = new Entities();
            int i = 0;

            string postData = "wsfunction=core_role_unassign_roles";

            foreach (MoodleGiaoVien item in list)
            {
                string[] ids = item.ID_vai_tro.Split(new char[]{','});

                foreach(string id in ids)
                {
                    postData += "&unassignments[" + i + "][roleid]=" + id;
                    postData += "&unassignments[" + i + "][userid]=" + item.ID_moodle;
                    postData += "&unassignments[" + i + "][contextid]=" + UtilityController.GetContextID(50, Convert.ToInt64(item.ID_lop_tc));
                    i++;
                }
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

                foreach (MoodleGiaoVien item in list)
                {
                    MOD_NguoiDung_VaiTro_LopTinChi entity;
                    entity = db.MOD_NguoiDung_VaiTro_LopTinChi.SingleOrDefault(t => t.ID_lop_tc == item.ID_lop_tc && t.UserID == item.ID_moodle);
                    entity.ID_vai_tro = "";

                    db.Entry(entity).State = System.Data.EntityState.Modified;
                }

                db.SaveChanges();
            }

            UtilityController.WriteTextToFile("D:\\GhiDanhGiaoVien_UnassignAll_VaiTro.txt", response);
        }

        public ActionResult UnassignAllVaiTroGiaoVien(string selectedVals, string id_lop_tc)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleGiaoViens(Convert.ToInt32(id_lop_tc)).Where(t => s.Contains(t.ID_moodle.ToString()) && t.ID_vai_tro != "").ToList();

            if (list.Count() == 0) return View();

            UnassignAllVaiTroGiaoVien(list);

            return View();
        }
    }
}
