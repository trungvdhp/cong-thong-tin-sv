using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq.Expressions;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CongThongTinSV.Models;
using System.Web.Script.Serialization;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace CongThongTinSV.Controllers
{
    public class MoodleUserController : Controller
    {
        public ActionResult SinhVien()
        {
            return View();
        }

        public ActionResult GetSinhVien([DataSourceRequest] DataSourceRequest request, string id_chuyen_nganh)
        {

            return Json(MoodleSinhViens(id_chuyen_nganh).ToDataSourceResult(request));
        }

        public IEnumerable<MoodleSinhVien> MoodleSinhViens(string id_chuyen_nganh)
        {
            Entities db = new Entities();
            int idcn = 0;
            try
            {
                idcn = Convert.ToInt32(id_chuyen_nganh);
            }
            catch (Exception) { }

            var sv1 = (from ds in db.STU_DanhSach
                      join hs in db.STU_HoSoSinhVien
                      on ds.ID_sv equals hs.ID_sv
                      where ds.Active == true
                      select new { ds, hs });

            var sv2 = from ds1 in sv1
                      join lop in db.STU_Lop
                      on ds1.ds.ID_lop equals lop.ID_lop
                      where lop.ID_chuyen_nganh == idcn
                      select new { ds1, lop };

            var sv3 = from ds2 in sv2
                      join gt in db.STU_GioiTinh
                      on ds2.ds1.hs.ID_gioi_tinh equals gt.ID_gioi_tinh
                      select new
                      {
                          ds2.ds1.ds.ID_sv,
                          ds2.ds1.hs.Ma_sv,
                          ds2.ds1.hs.Ho_ten,
                          ds2.ds1.hs.Ngay_sinh,
                          ds2.lop.Ten_lop,
                          ds2.ds1.ds.Mat_khau,
                          gt.Gioi_tinh
                      };

            var sv4 = from ds in sv3.AsEnumerable()
                      join nd in db.MOD_NguoiDung.AsEnumerable()
                      on ds.ID_sv equals nd.ID_nd
                      into nguoidung
                      from nd1 in nguoidung.DefaultIfEmpty()
                      where nd1 == null || (nd1 != null && nd1.ID_nhom_nd == 3)
                      select new MoodleSinhVien
                      {
                          ID_sv = ds.ID_sv,
                          ID_moodle = (nd1 == null ? 0 : nd1.ID_moodle),
                          Ma_sv = ds.Ma_sv,
                          Ho_dem = UtilityController.GetLastName(ds.Ho_ten),
                          Ten = UtilityController.GetFirstName(ds.Ho_ten),
                          Ngay_sinh = ds.Ngay_sinh,
                          Gioi_tinh = ds.Gioi_tinh,
                          Lop = ds.Ten_lop,
                          Mat_khau = ds.Mat_khau
                      };

            return sv4.OrderByDescending(t => t.ID_moodle).ToList();
        }

        public static void CreateSinhVien(List<MoodleSinhVien> list)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "wsfunction=core_user_create_users";

            foreach (MoodleSinhVien item in list)
            {
                postData += "&users[" + i + "][username]=" + item.Ma_sv;

                if (item.Mat_khau != "")
                {
                    postData += "&users[" + i + "][password]=" + item.Mat_khau;
                }
                else
                {
                    try
                    {
                        postData += "&users[" + i + "][password]=" + ((DateTime)item.Ngay_sinh).ToString("ddMMyyyy");
                    }
                    catch
                    {
                        postData += "&users[" + i + "][password]=" + item.Ma_sv;
                    }
                }

                postData += "&users[" + i + "][firstname]=" + HttpUtility.UrlEncode(item.Ten);
                postData += "&users[" + i + "][lastname]=" + HttpUtility.UrlEncode(item.Ho_dem);
                postData += "&users[" + i + "][email]=" + "st" + item.Ma_sv + "@st.vimaru.edu.vn";
                postData += "&users[" + i + "][timezone]=7.0";
                postData += "&users[" + i + "][city]=Hai Phong";
                postData += "&users[" + i + "][country]=VN";
                postData += "&users[" + i + "][idnumber]=st" + item.ID_sv;
                i++;
            }

            WebRequestController web = new WebRequestController(4, "POST", postData);
            string response = web.GetResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            // MoodleException moodleError = new MoodleException();
            List<MoodleCreateUserResponse> results = new List<MoodleCreateUserResponse>();

            if (response.Contains("exception"))
            {
                // Error
                // moodleError = serializer.Deserialize<MoodleException>(rs);
            }
            else
            {
                // Good
                results = serializer.Deserialize<List<MoodleCreateUserResponse>>(response);
                i = 0;

                foreach (MoodleSinhVien item in list)
                {
                    MOD_NguoiDung entity = new MOD_NguoiDung();

                    entity.ID_moodle = Convert.ToInt32(results[i].id);
                    entity.ID_nd = item.ID_sv;
                    entity.ID_nhom_nd = 3;

                    db.MOD_NguoiDung.Add(entity);
                    i++;
                }

                db.SaveChanges();
            }

            UtilityController.WriteTextToFile("D:\\SinhVienCreate.txt", response);
        }

        public ActionResult CreateSinhVien(string selectedVals, string id_chuyen_nganh)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });

            var list = MoodleSinhViens(id_chuyen_nganh).Where(t => t.ID_moodle == 0 && s.Contains(t.ID_sv.ToString())).ToList();

            if (list.Count() > 0)
                CreateSinhVien(list);
            
            return View();
        }

        public static bool DeleteSinhVien(List<MoodleSinhVien> list)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "wsfunction=core_user_delete_users";

            foreach (MoodleSinhVien item in list)
            {
                postData += "&userids[" + i + "]=" + item.ID_moodle;
                i++;
            }

            WebRequestController web = new WebRequestController(4, "POST", postData);
            string response = web.GetResponse();
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //MoodleException moodleError = new MoodleException();
            UtilityController.WriteTextToFile("D:\\SinhVienDelete.txt", response);

            if (response.Contains("exception"))
            {
                // Error
                //moodleError = serializer.Deserialize<MoodleException>(rs);
                return false;
            }
            else
            {
                foreach (MoodleSinhVien item in list)
                {
                    MOD_NguoiDung entity = db.MOD_NguoiDung.Find(item.ID_moodle);
                    db.MOD_NguoiDung.Remove(entity);
                }

                db.SaveChanges();
            }

            return true;
        }

        public ActionResult DeleteSinhVien(string selectedVals, string id_chuyen_nganh)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });

            var list = MoodleSinhViens(id_chuyen_nganh).Where(t => t.ID_moodle > 0 && s.Contains(t.ID_sv.ToString())).ToList();

            if (list.Count() > 0)
                DeleteSinhVien(list);

            return View();
        }

        public static bool UpdateSinhVien(List<MoodleSinhVien> list)
        {
            int i = 0;
            string postData = "wsfunction=core_user_update_users";

            foreach (MoodleSinhVien item in list)
            {
                postData += "&users[" + i + "][id]=" + item.ID_moodle;
                postData += "&users[" + i + "][username]=" + item.Ma_sv;

                if (item.Mat_khau != "")
                {
                    postData += "&users[" + i + "][password]=" + item.Mat_khau;
                }
                else
                {
                    try
                    {
                        postData += "&users[" + i + "][password]=" + ((DateTime)item.Ngay_sinh).ToString("ddMMyyyy");
                    }
                    catch
                    {
                        postData += "&users[" + i + "][password]=" + item.Ma_sv;
                    }
                }

                postData += "&users[" + i + "][firstname]=" + HttpUtility.UrlEncode(item.Ten);
                postData += "&users[" + i + "][lastname]=" + HttpUtility.UrlEncode(item.Ho_dem);
                postData += "&users[" + i + "][email]=" + "st" + item.Ma_sv + "@st.vimaru.edu.vn";
                i++;
            }

            WebRequestController web = new WebRequestController(4, "POST", postData);
            string response = web.GetResponse();
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //MoodleException moodleError = new MoodleException();
            //List<MoodleCreateUserResponse> results = new List<MoodleCreateUserResponse>();
            UtilityController.WriteTextToFile("D:\\SinhVienUpdate.txt", response);

            if (response.Contains("exception"))
            {
                // Error
                // moodleError = serializer.Deserialize<MoodleException>(rs);
                return false;
            }

            return true;
        }

        public ActionResult UpdateSinhVien(string selectedVals, string id_chuyen_nganh)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });

            var list = MoodleSinhViens(id_chuyen_nganh).Where(t => t.ID_moodle > 0 && s.Contains(t.ID_sv.ToString())).ToList();

            if (list.Count() > 0)
                UpdateSinhVien(list);

            return View();
        }
        //
        //Đồng bộ tài khoản moodle và DHHH của sinh viên
        public ActionResult SyncSinhVien(string selectedVals, string id_chuyen_nganh)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            Entities db = new Entities();
            MoodleEntities mdb = new MoodleEntities();
            var list = MoodleSinhViens(id_chuyen_nganh).Where(t => t.ID_moodle == 0 && s.Contains(t.ID_sv.ToString()));
            var sinhviens = from sv in list
                            join user in mdb.fit_user.AsEnumerable()
                            on sv.Ma_sv equals user.username
                            select new
                            {
                                ID_sv = sv.ID_sv,
                                ID_moodle = user.id
                            };

            foreach (var item in sinhviens)
            {
                MOD_NguoiDung entity = new MOD_NguoiDung();

                entity.ID_moodle = Convert.ToInt32(item.ID_moodle);
                entity.ID_nd = item.ID_sv;
                entity.ID_nhom_nd = 3;

                db.MOD_NguoiDung.Add(entity);
            }

            db.SaveChanges();

            return View();
        }

        public ActionResult GiaoVien()
        {
            return View();
        }

        public ActionResult GetGiaoVien([DataSourceRequest] DataSourceRequest request, string id_khoa)
        {

            return Json(MoodleGiaoViens(id_khoa).ToDataSourceResult(request));
        }

        public IEnumerable<MoodleGiaoVien> MoodleGiaoViens(string id_khoa)
        {
            Entities db = new Entities();
            int idKhoa = 0;

            try
            {
                idKhoa = Convert.ToInt32(id_khoa);
            }
            catch (Exception) { }

            var gv1 = from  gv in db.PLAN_GiaoVien.AsEnumerable()
                      join gt in db.STU_GioiTinh
                      on gv.ID_gioi_tinh equals gt.ID_gioi_tinh
                      where gv.ID_khoa == idKhoa
                      select new
                      {
                          gv.Ho_ten,
                          gv.ID_cb,
                          gv.Ma_cb,
                          gv.Ngay_sinh,
                          gt.Gioi_tinh
                      };

            var gv2 = from gv in gv1.AsEnumerable()
                      join nd in db.MOD_NguoiDung.AsEnumerable()
                      on gv.ID_cb equals nd.ID_nd
                      into nguoidung
                      from nd1 in nguoidung.DefaultIfEmpty()
                      where nd1 == null || (nd1 != null && nd1.ID_nhom_nd == 2)
                      select new MoodleGiaoVien
                      {
                          ID_cb = gv.ID_cb,
                          ID_moodle = (nd1 == null ? 0 : nd1.ID_moodle),
                          Ma_cb = gv.Ma_cb,
                          Ho_dem = UtilityController.GetLastName(gv.Ho_ten),
                          Ten = UtilityController.GetFirstName(gv.Ho_ten),
                          Ngay_sinh = gv.Ngay_sinh,
                          Gioi_tinh = gv.Gioi_tinh
                      };

            return gv2.OrderByDescending(t => t.ID_moodle).ToList();
        }

        public static void CreateGiaoVien(List<MoodleGiaoVien> list)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "wsfunction=core_user_create_users";

            foreach (MoodleGiaoVien item in list)
            {
                postData += "&users[" + i + "][username]=" + item.Ma_cb;
                try
                {
                    postData += "&users[" + i + "][password]=" + ((DateTime)item.Ngay_sinh).ToString("ddMMyyyy");
                }
                catch
                {
                    postData += "&users[" + i + "][password]=" + item.Ma_cb;
                }
                postData += "&users[" + i + "][firstname]=" + HttpUtility.UrlEncode(item.Ten);
                postData += "&users[" + i + "][lastname]=" + HttpUtility.UrlEncode(item.Ho_dem);
                postData += "&users[" + i + "][email]=" + "te" + item.Ma_cb + "@te.vimaru.edu.vn";
                postData += "&users[" + i + "][timezone]=7.0";
                postData += "&users[" + i + "][city]=Hai Phong";
                postData += "&users[" + i + "][country]=VN";
                postData += "&users[" + i + "][idnumber]=te" + item.ID_cb;
                i++;
            }

            WebRequestController web = new WebRequestController(4, "POST", postData);
            string response = web.GetResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            // MoodleException moodleError = new MoodleException();
            List<MoodleCreateUserResponse> results = new List<MoodleCreateUserResponse>();

            if (response.Contains("exception"))
            {
                // Error
                // moodleError = serializer.Deserialize<MoodleException>(rs);
            }
            else
            {
                // Good
                results = serializer.Deserialize<List<MoodleCreateUserResponse>>(response);
                i = 0;

                foreach (MoodleGiaoVien item in list)
                {
                    MOD_NguoiDung entity = new MOD_NguoiDung();

                    entity.ID_moodle = Convert.ToInt32(results[i].id);
                    entity.ID_nd = item.ID_cb;
                    entity.ID_nhom_nd = 2;

                    db.MOD_NguoiDung.Add(entity);
                    i++;
                }

                db.SaveChanges();
            }

            UtilityController.WriteTextToFile("D:\\GiaoVienCreate.txt", response);
        }

        public ActionResult CreateGiaoVien(string selectedVals, string id_khoa)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });

            var list = MoodleGiaoViens(id_khoa).Where(t => t.ID_moodle == 0 && s.Contains(t.ID_cb.ToString())).ToList();

            if (list.Count() > 0)
                CreateGiaoVien(list);

            return View();
        }

        public static bool DeleteGiaoVien(List<MoodleGiaoVien> list)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "wsfunction=core_user_delete_users";

            foreach (MoodleGiaoVien item in list)
            {
                postData += "&userids[" + i + "]=" + item.ID_moodle;
                i++;
            }

            WebRequestController web = new WebRequestController(4, "POST", postData);
            string response = web.GetResponse();
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //MoodleException moodleError = new MoodleException();
            UtilityController.WriteTextToFile("D:\\GiaoVienDelete.txt", response);

            if (response.Contains("exception"))
            {
                // Error
                //moodleError = serializer.Deserialize<MoodleException>(rs);
                return false;
            }
            else
            {
                foreach (MoodleGiaoVien item in list)
                {
                    MOD_NguoiDung entity = db.MOD_NguoiDung.Find(item.ID_moodle);
                    db.MOD_NguoiDung.Remove(entity);
                }

                db.SaveChanges();
            }

            return true;
        }

        public ActionResult DeleteGiaoVien(string selectedVals, string id_khoa)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });

            var list = MoodleGiaoViens(id_khoa).Where(t => t.ID_moodle > 0 && s.Contains(t.ID_cb.ToString())).ToList();

            if (list.Count() > 0)
                DeleteGiaoVien(list);

            return View();
        }

        public static bool UpdateGiaoVien(List<MoodleGiaoVien> list)
        {
            int i = 0;
            string postData = "wsfunction=core_user_update_users";

            foreach (MoodleGiaoVien item in list)
            {
                postData += "&users[" + i + "][id]=" + item.ID_moodle;
                postData += "&users[" + i + "][username]=" + item.Ma_cb;

                try
                {
                    postData += "&users[" + i + "][password]=" + ((DateTime)item.Ngay_sinh).ToString("ddMMyyyy");
                }
                catch
                {
                    postData += "&users[" + i + "][password]=" + item.Ma_cb;
                }

                postData += "&users[" + i + "][firstname]=" + HttpUtility.UrlEncode(item.Ten);
                postData += "&users[" + i + "][lastname]=" + HttpUtility.UrlEncode(item.Ho_dem);
                postData += "&users[" + i + "][email]=" + "te" + item.Ma_cb + "@te.vimaru.edu.vn";
                i++;
            }

            WebRequestController web = new WebRequestController(4, "POST", postData);
            string response = web.GetResponse();
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //MoodleException moodleError = new MoodleException();
            //List<MoodleCreateUserResponse> results = new List<MoodleCreateUserResponse>();
            UtilityController.WriteTextToFile("D:\\GiaoVienUpdate.txt", response);

            if (response.Contains("exception"))
            {
                // Error
                // moodleError = serializer.Deserialize<MoodleException>(rs);
                return false;
            }

            return true;
        }

        public ActionResult UpdateGiaoVien(string selectedVals, string id_khoa)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });

            var list = MoodleGiaoViens(id_khoa).Where(t => t.ID_moodle > 0 && s.Contains(t.ID_cb.ToString())).ToList();

            if (list.Count() > 0)
                UpdateGiaoVien(list);

            return View();
        }

        //
        //Đồng bộ tài khoản moodle và DHHH của giáo viên
        public ActionResult SyncGiaoVien(string selectedVals, string id_khoa)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            Entities db = new Entities();
            MoodleEntities mdb = new MoodleEntities();
            var list = MoodleGiaoViens(id_khoa).Where(t => t.ID_moodle == 0 && s.Contains(t.ID_cb.ToString()));
            var giaoviens = from gv in list
                            join user in mdb.fit_user.AsEnumerable()
                            on gv.Ma_cb equals user.username
                            select new
                            {
                                ID_cb = gv.ID_cb,
                                ID_moodle = user.id
                            };

            foreach (var item in giaoviens)
            {
                MOD_NguoiDung entity = new MOD_NguoiDung();

                entity.ID_moodle = Convert.ToInt32(item.ID_moodle);
                entity.ID_nd = item.ID_cb;
                entity.ID_nhom_nd = 2;

                db.MOD_NguoiDung.Add(entity);
            }

            db.SaveChanges();

            return View();
        }

        public ActionResult QuanTriVien()
        {
            return View();
        }

        public ActionResult GetQuanTriVien([DataSourceRequest] DataSourceRequest request)
        {

            return Json(MoodleQuanTriViens().ToDataSourceResult(request));
        }

        public IEnumerable<MoodleUser> MoodleQuanTriViens()
        {
            Entities db = new Entities();
            var user = db.SYS_NguoiDung.AsEnumerable().Where(t => t.Active == 1);

            var quantris = from ds in user
                      join nd in db.MOD_NguoiDung.AsEnumerable()
                      on ds.UserID equals nd.ID_nd
                      into nguoidung
                      from nd1 in nguoidung.DefaultIfEmpty()
                      where nd1 == null || (nd1 != null && nd1.ID_nhom_nd == 1)
                      select new MoodleUser
                      {
                          ID = ds.UserID,
                          ID_moodle = (nd1 == null ? 0 : nd1.ID_moodle),
                          UserName = ds.UserName,
                          LastName = UtilityController.GetLastName(ds.FullName),
                          FirstName = UtilityController.GetFirstName(ds.FullName),
                          Email = ds.Email
                      };

            return quantris.OrderByDescending(t => t.ID_moodle).ToList();
        }

        public static void CreateQuanTriVien(List<MoodleUser> list)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "wsfunction=core_user_create_users";

            foreach (MoodleUser item in list)
            {
                postData += "&users[" + i + "][username]=" + item.UserName;
                postData += "&users[" + i + "][password]=" + item.UserName;
                postData += "&users[" + i + "][firstname]=" + HttpUtility.UrlEncode(item.FirstName);
                postData += "&users[" + i + "][lastname]=" + HttpUtility.UrlEncode(item.LastName);

                if (item.Email != "")
                    postData += "&users[" + i + "][email]=" + item.Email;
                else
                    postData += "&users[" + i + "][email]=" + item.UserName + "@vimaru.edu.vn";

                postData += "&users[" + i + "][timezone]=7.0";
                postData += "&users[" + i + "][city]=Hai Phong";
                postData += "&users[" + i + "][country]=VN";
                postData += "&users[" + i + "][idnumber]=st" + item.ID;
                i++;
            }

            WebRequestController web = new WebRequestController(4, "POST", postData);
            string response = web.GetResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            // MoodleException moodleError = new MoodleException();
            List<MoodleCreateUserResponse> results = new List<MoodleCreateUserResponse>();

            if (response.Contains("exception"))
            {
                // Error
                // moodleError = serializer.Deserialize<MoodleException>(rs);
            }
            else
            {
                // Good
                results = serializer.Deserialize<List<MoodleCreateUserResponse>>(response);
                i = 0;

                foreach (MoodleUser item in list)
                {
                    MOD_NguoiDung entity = new MOD_NguoiDung();

                    entity.ID_moodle = Convert.ToInt32(results[i].id);
                    entity.ID_nd = item.ID;
                    entity.ID_nhom_nd = 1;

                    db.MOD_NguoiDung.Add(entity);
                    i++;
                }

                db.SaveChanges();
            }

            UtilityController.WriteTextToFile("D:\\QuanTriVienCreate.txt", response);
        }

        public ActionResult CreateQuanTriVien(string selectedVals)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });

            var list = MoodleQuanTriViens().Where(t => t.ID_moodle == 0 && s.Contains(t.ID.ToString())).ToList();

            if (list.Count() > 0)
                CreateQuanTriVien(list);

            return View();
        }

        public static void DeleteQuanTriVien(List<MoodleUser> list)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "wsfunction=core_user_delete_users";

            foreach (MoodleUser item in list)
            {
                postData += "&userids[" + i + "]=" + item.ID_moodle;
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
                foreach (MoodleUser item in list)
                {
                    MOD_NguoiDung entity = db.MOD_NguoiDung.Find(item.ID_moodle);
                    db.MOD_NguoiDung.Remove(entity);
                }

                db.SaveChanges();
            }

            UtilityController.WriteTextToFile("D:\\QuanTriVienDelete.txt", response);
        }

        public ActionResult DeleteQuanTriVien(string selectedVals)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });

            var list = MoodleQuanTriViens().Where(t => t.ID_moodle > 0 && s.Contains(t.ID.ToString())).ToList();

            if (list.Count() > 0)
                DeleteQuanTriVien(list);

            return View();
        }

        public static bool UpdateQuanTriVien(List<MoodleUser> list)
        {
            int i = 0;
            string postData = "wsfunction=core_user_update_users";

            foreach (MoodleUser item in list)
            {
                postData += "&users[" + i + "][id]=" + item.ID_moodle;
                postData += "&users[" + i + "][username]=" + item.UserName;
                postData += "&users[" + i + "][password]=" + item.UserName;
                postData += "&users[" + i + "][firstname]=" + HttpUtility.UrlEncode(item.FirstName);
                postData += "&users[" + i + "][lastname]=" + HttpUtility.UrlEncode(item.LastName);

                if (item.Email != "")
                    postData += "&users[" + i + "][email]=" + item.Email;
                else
                    postData += "&users[" + i + "][email]=" + item.UserName + "@vimaru.edu.vn";

                i++;
            }

            WebRequestController web = new WebRequestController(4, "POST", postData);
            string response = web.GetResponse();
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //MoodleException moodleError = new MoodleException();
            //List<MoodleCreateUserResponse> results = new List<MoodleCreateUserResponse>();
            UtilityController.WriteTextToFile("D:\\QuanTriVienUpdate.txt", response);

            if (response.Contains("exception"))
            {
                // Error
                // moodleError = serializer.Deserialize<MoodleException>(rs);
                return false;
            }

            return true;
        }

        public ActionResult UpdateQuanTriVien(string selectedVals)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });

            var list = MoodleQuanTriViens().Where(t => t.ID_moodle > 0 && s.Contains(t.ID.ToString())).ToList();

            if (list.Count() > 0)
                UpdateQuanTriVien(list);

            return View();
        }

        public static string GetToken(string username, string password, string service)
        {
            //Get user token
            WebRequestController web = new WebRequestController(1, "POST", "username=" + username + "&password=" + password + "&service=" + service);
            string s = web.GetResponse();

            if(s.Contains("exception")) 
            {
                return "exception";
            }

            string[] rs = s.Split(new char[] { '"' });
            UtilityController.WriteTextToFile("D:\\token.txt", service + " : " + s + " username: " + username);

            if(rs.Length == 5) 
                return rs[3].Trim();
            
            return "exception";
        }

        public static bool UpdateUser(List<MoodleUser> list)
        {
            int i = 0;
            string postData = "wsfunction=core_user_update_users";

            foreach (MoodleUser item in list)
            {
                if(item.ID == 0) continue;
                postData += "&users[" + i + "][id]=" + item.ID;
                if(item.UserName != null)
                    postData += "&users[" + i + "][username]=" + item.UserName;
                if(item.Password != null)
                    postData += "&users[" + i + "][password]=" + item.Password;
                if(item.FirstName != null)
                    postData += "&users[" + i + "][firstname]=" + HttpUtility.UrlEncode(item.FirstName);
                if(item.LastName != null)
                    postData += "&users[" + i + "][lastname]=" + HttpUtility.UrlEncode(item.LastName);
                if(item.Email != null)
                    postData += "&users[" + i + "][email]=" + item.Email;
                i++;
            }

            WebRequestController web = new WebRequestController(4, "POST", postData);
            string response = web.GetResponse();
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            // MoodleException moodleError = new MoodleException();
            //List<MoodleCreateUserResponse> results = new List<MoodleCreateUserResponse>();
            UtilityController.WriteTextToFile("D:\\MoodleUserUpdate.txt", response);

            if (response.Contains("exception"))
            {
                // Error
                // moodleError = serializer.Deserialize<MoodleException>(rs);
                return false;
            }

            return true;
        }
        //
        // GET: /MoodleUser/DoiMatKhau

        public ActionResult DoiMatKhau(string message)
        {
            ViewBag.StatusMessage = message;
            ViewBag.ReturnUrl = Url.Action("DoiMatKhau");
            return View();
        }

        //
        // POST: /MoodleUser/DoiMatKhau

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoiMatKhau(LocalPasswordModel model)
        {
            ViewBag.ReturnUrl = Url.Action("DoiMatKhau");

            if (ModelState.IsValid)
            {
                // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    UserData userData = AccountController.GetCurrentUserData();

                    if(GetToken(userData.UserName, model.OldPassword, userData.MoodleService) == userData.MoodleToken)
                    {
                        List<MoodleUser> list = new List<MoodleUser>();
                        list.Add(new MoodleUser
                        {
                            ID = Convert.ToInt32(userData.MoodleUserID),
                            Password = model.NewPassword
                        });

                        changePasswordSucceeded = UpdateUser(list);
                    }
                    else
                    {
                         changePasswordSucceeded = false;
                    }
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("DoiMatKhau", new { Message = "Bạn đã đổi mật khẩu thành công." });
                }
                else
                {
                    ModelState.AddModelError("","Bạn nhập sai mật khẩu cũ hoặc mật khẩu mới không hợp lệ.");
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        
        public static List<MoodleUserResponse> GetUserByID(List<string> list)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "wsfunction=core_user_get_users_by_id";

            foreach (string item in list)
            {
                postData += "&userids[" + i + "]=" + item;
                i++;
            }

            WebRequestController web = new WebRequestController(4, "POST", postData);
            string response = web.GetResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            // MoodleException moodleError = new MoodleException();
            List<MoodleUserResponse> results = new List<MoodleUserResponse>();

            if (response.Contains("exception"))
            {
                // Error
                // moodleError = serializer.Deserialize<MoodleException>(rs);
            }
            else
            {
                // Good
                results = serializer.Deserialize<List<MoodleUserResponse>>(response);
            }

            UtilityController.WriteTextToFile("D:\\MoodleGetUserByID.txt", response);
            return results;
        }

        public ActionResult UserProfile(string userid)
        {
            List<string> list = new List<string>();
            list.Add(userid);
            var q = GetUserByID(list);

            if (q.Count() > 0)
                ViewBag.UserProfile = q.ElementAt(0);
            else
                ViewBag.UserProfile = new MoodleUserResponse();

            return View();
        }

        public static List<MoodleCourseUserResponse> GetCourseUserProfile(List<KeyValuePair<string, string>> list)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "wsfunction=core_user_get_course_user_profiles";

            foreach (KeyValuePair<string, string> item in list)
            {
                postData += "&userlist[" + i + "][userid]=" + item.Key;
                postData += "&userlist[" + i + "][courseid]=" + item.Value;
                i++;
            }

            WebRequestController web = new WebRequestController(4, "POST", postData);
            string response = web.GetResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            // MoodleException moodleError = new MoodleException();
            List<MoodleCourseUserResponse> results = new List<MoodleCourseUserResponse>();

            if (response.Contains("exception"))
            {
                // Error
                // moodleError = serializer.Deserialize<MoodleException>(rs);
            }
            else
            {
                // Good
                results = serializer.Deserialize<List<MoodleCourseUserResponse>>(response);
            }

            UtilityController.WriteTextToFile("D:\\MoodleGetCourseUserProfile.txt", response);
            return results;
        }

        public ActionResult CourseUserProfile(string userid, string courseid)
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string,string>(userid, courseid));
            var q = GetCourseUserProfile(list);

            if (q.Count() > 0)
            {
                var user= q.ElementAt(0);
                ViewBag.CourseUser = user;
                var course = user.enrolledcourses.AsEnumerable().SingleOrDefault(t => t.id.ToString() == courseid);

                if (course == null)
                {
                    ViewBag.CourseName = "";
                }
                else
                {
                    ViewBag.CourseName = course.fullname;
                }
            }
            else
            {
                ViewBag.CourseUser = new MoodleCourseUserResponse();
                ViewBag.CourseName = "";
            }

            return View();
        }

        public ActionResult BaiLamCaNhan(string quizid)
        {
            return View();
        }

        public ActionResult BaiLamHocVien(string quizid, string userid)
        {
            return View();
        }
    }
}
