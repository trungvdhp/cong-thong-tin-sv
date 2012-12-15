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

        public ActionResult GetHocVien([DataSourceRequest] DataSourceRequest request, string id_lop_tc)
        {

            return Json(MoodleHocVienDiems(id_lop_tc).ToDataSourceResult(request));
        }

        public static IEnumerable<MoodleSinhVien> MoodleHocVienDiems(string id_lop_tc)
        {
            Entities db = new Entities();
            int idlop = 0;

            try
            {
                idlop = Convert.ToInt32(id_lop_tc);
            }
            catch (Exception) { }

            if (idlop <= 0) return new List<MoodleSinhVien>();

            var hocvien = MoodleHocViens(id_lop_tc);
            var loptc = (from hocky in db.PLAN_HocKyDangKy_TC
                         join lop1 in db.MOD_LopTinChi_TC.Where(t => t.ID_moodle == idlop)
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
                          Tinh_trang = ds.Tinh_trang,
                          ID_nhom = ds.ID_nhom,
                          Ten_nhom = ds.Ten_nhom
                      };

            return hocviendiem.OrderByDescending(t => t.Tinh_trang).ToList();
        }

        public static IEnumerable<MoodleSinhVien> MoodleHocViens(string id_lop_tc)
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
                              Mat_khau = ds.Mat_khau,
                              Ma_sv = ds.Ma_sv,
                              Ho_dem = ds.Ho_dem,
                              Ten = ds.Ten,
                              Ngay_sinh = ds.Ngay_sinh,
                              Gioi_tinh = ds.Gioi_tinh,
                              Lop = ds.Lop,
                              ID = ds.ID,
                              Tinh_trang = ds.ID_moodle == 0 ? "Chưa có tài khoản" : (dk == null ? "Chưa ghi danh" : "Đã ghi danh"),
                              ID_nhom = (dk == null || dk.MOD_NhomHocVien == null ? null : (int?)dk.MOD_NhomHocVien.ID_nhom),
                              Ten_nhom = (dk == null || dk.MOD_NhomHocVien == null ? "" : dk.MOD_NhomHocVien.Ten_nhom)
                          };

            return hocvien.ToList();
        }

        public static IEnumerable<MoodleSinhVien> MoodleSinhViens(string id_lop_tc)
        {
            Entities db = new Entities();
            int idlop = 0;

            try
            {
                idlop = Convert.ToInt32(id_lop_tc);
            }
            catch (Exception) { }

            if (idlop <= 0) return new List<MoodleSinhVien>();

            var loptc = db.MOD_LopTinChi_TC.FirstOrDefault(t => t.ID_moodle == idlop);

            var dangky = from dk in db.STU_DanhSachLopTinChi
                         where dk.ID_lop_tc == loptc.ID_lop_tc
                         select new
                         {
                             ID = dk.ID,
                             ID_sv = dk.ID_sv,
                             ID_lop_tc = idlop
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
                          ds.Mat_khau,
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
                          ds.Mat_khau,
                          ds.Ho_ten,
                          ds.Ngay_sinh,
                          ds.Gioi_tinh,
                          lop.Ten_lop
                      };
            var nguoidungs = db.MOD_NguoiDung.Where(t => t.ID_nhom_nd == 3);
            var sv3 = from ds in sv2.AsEnumerable()
                      join nd1 in nguoidungs
                      on ds.ID_sv equals nd1.ID_nd
                      into nguoidung
                      from nd in nguoidung.DefaultIfEmpty()
                      select new MoodleSinhVien
                      {
                          ID_sv = ds.ID_sv,
                          Mat_khau = ds.Mat_khau,
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
            var list1 = MoodleSinhViens(id_lop_tc).Where(t => t.ID_moodle == 0 && s.Contains(t.ID.ToString())).ToList();  
            if (list1.Count() > 0)
                MoodleUserController.CreateSinhVien(list1);

            var list2 = MoodleHocVienDiems(id_lop_tc).Where(t => t.ID_moodle > 0 && t.Tinh_trang == "Chưa ghi danh" && s.Contains(t.ID.ToString())).ToList();

            if (list2.Count() > 0)
                CreateGhiDanhSinhVien(list2);

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
            var list = MoodleHocVienDiems(id_lop_tc).Where(t => t.Tinh_trang == "Đã ghi danh" && s.Contains(t.ID.ToString())).ToList();

            if (list.Count() > 0)
                DeleteGhiDanhSinhVien(list);

            return View();
        }

        public ActionResult GhiDanhGiaoVien()
        {
            return View();
        }

        public ActionResult GetGiaoVien([DataSourceRequest] DataSourceRequest request, string id_lop_tc)
        {
            return Json(MoodleGiaoViens(id_lop_tc).ToDataSourceResult(request));
        }

        public static IEnumerable<MoodleGiaoVien> MoodleGiaoViens(string id_lop_tc)
        {
            Entities db = new Entities();
            int idlop = 0;

            try
            {
                idlop = Convert.ToInt32(id_lop_tc);
            }
            catch (Exception) { }

            if (idlop <= 0) return new List<MoodleGiaoVien>();

            var danhsach = from ds in db.MOD_NguoiDung_VaiTro_LopTinChi.AsEnumerable()
                           where ds.ID_lop_tc == idlop
                           select new 
                           {
                               UserID = ds.UserID,
                               ID_lop_tc = ds.ID_lop_tc,
                               ID_vai_tro = ds.ID_vai_tro,
                               Vai_tro = string.Join(("\n"), MoodleRoleController.GetVaiTroKhoaHoc(ds.ID_vai_tro, new char[]{','})),
                               Dinh_chi = ds.Dinh_chi
                           };
            var nguoidungs = db.MOD_NguoiDung.Where(t => t.ID_nhom_nd == 2);
            var giaovien = from gv1 in db.PLAN_GiaoVien.AsEnumerable()
                           join gt in db.STU_GioiTinh
                           on gv1.ID_gioi_tinh equals gt.ID_gioi_tinh
                           join gv2 in nguoidungs
                           on gv1.ID_cb equals gv2.ID_nd
                           into dsgv from gv3 in dsgv.DefaultIfEmpty()
                           select new
                           {
                               ID_moodle = (gv3 == null ? 0 : gv3.ID_moodle),
                               ID_cb = gv1.ID_cb,
                               ID_khoa = gv1.ID_khoa,
                               Ma_cb = gv1.Ma_cb,
                               Ho_dem = UtilityController.GetLastName(gv1.Ho_ten),
                               Ten = UtilityController.GetFirstName(gv1.Ho_ten),
                               Ngay_sinh = gv1.Ngay_sinh,
                               Gioi_tinh = gt.Gioi_tinh
                           };

            var q = from gv1 in giaovien
                    join khoa in db.STU_Khoa
                    on gv1.ID_khoa equals khoa.ID_khoa
                    join gv2 in danhsach
                    on gv1.ID_moodle equals gv2.UserID
                    into danhsach1
                    from gv in danhsach1.DefaultIfEmpty()
                    select new MoodleGiaoVien
                    {
                        ID_lop_tc = idlop,
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
                        Tinh_trang = gv1.ID_moodle == 0 ? "Chưa có tài khoản" : (gv == null ? "Chưa ghi danh" : gv.Dinh_chi ? "Chưa kích hoạt":"Đã kích hoạt")
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

            var list1 = MoodleGiaoViens(id_lop_tc).Where(t => t.ID_moodle == 0 && s.Contains(t.ID_cb.ToString())).ToList();
            if (list1.Count() > 0)
                MoodleUserController.CreateGiaoVien(list1);

            var list2 = MoodleGiaoViens(id_lop_tc).Where(t => t.ID_moodle > 0 && t.Tinh_trang == "Chưa ghi danh" && s.Contains(t.ID_cb.ToString())).ToList();

            if (list2.Count() > 0)
                CreateGhiDanhGiaoVien(list2, id_vai_tro, suspended);

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
                postData += "&unassignments[" + i + "][contextid]=" + UtilityController.GetContextID("50", item.ID_lop_tc.ToString());
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
            var list = MoodleGiaoViens(id_lop_tc).Where(t => s.Contains(t.ID_moodle.ToString()) && UtilityController.InArray(t.ID_vai_tro, new char[] { ',' }, id_vai_tro)).ToList();

            if (list.Count() > 0)
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
                    postData += "&unassignments[" + i + "][contextid]=" + UtilityController.GetContextID("50", item.ID_lop_tc.ToString());
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
            var list = MoodleGiaoViens(id_lop_tc).Where(t => s.Contains(t.ID_moodle.ToString()) && t.ID_vai_tro != "").ToList();

            if (list.Count() > 0)
                UnassignAllVaiTroGiaoVien(list);

            return View();
        }
    }
}
