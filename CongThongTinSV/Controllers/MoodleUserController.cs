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
        //
        // GET: /MoodleUser/

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
                      where ds.Active == true
                      select new { ds, hs };

            var sv2 = from ds1 in sv1
                      join lop in db.STU_Lop
                      on ds1.ds.ID_lop equals lop.ID_lop
                      where lop.ID_chuyen_nganh == id_chuyen_nganh
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

            var list = MoodleSinhViens(Convert.ToInt32(id_chuyen_nganh)).Where(t => t.ID_moodle == 0 && s.Contains(t.ID_sv.ToString())).ToList();

            if (list.Count() == 0) return View();

            CreateSinhVien(list);
            
            return View();
        }

        public static void DeleteSinhVien(List<MoodleSinhVien> list)
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

            if (response.Contains("exception"))
            {
                // Error
                //moodleError = serializer.Deserialize<MoodleException>(rs);
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

            UtilityController.WriteTextToFile("D:\\SinhVienDelete.txt", response);
        }

        public ActionResult DeleteSinhVien(string selectedVals, string id_chuyen_nganh)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });

            var list = MoodleSinhViens(Convert.ToInt32(id_chuyen_nganh)).Where(t => t.ID_moodle > 0 && s.Contains(t.ID_sv.ToString())).ToList();

            if (list.Count() == 0) return View();

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
            // MoodleException moodleError = new MoodleException();
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

            var list = MoodleSinhViens(Convert.ToInt32(id_chuyen_nganh)).Where(t => t.ID_moodle > 0 && s.Contains(t.ID_sv.ToString())).ToList();

            if (list.Count() == 0) return View();

            UpdateSinhVien(list);

            return View();
        }

        public ActionResult GiaoVien()
        {
            return View();
        }

        public ActionResult GetGiaoVien([DataSourceRequest] DataSourceRequest request, int id_khoa)
        {

            return Json(MoodleGiaoViens(id_khoa).ToDataSourceResult(request));
        }

        public IEnumerable<MoodleGiaoVien> MoodleGiaoViens(int id_khoa)
        {
            Entities db = new Entities();

            var gv1 = from  gv in db.PLAN_GiaoVien
                      join gt in db.STU_GioiTinh
                      on gv.ID_gioi_tinh equals gt.ID_gioi_tinh
                      where gv.ID_khoa == id_khoa
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

            var list = MoodleGiaoViens(Convert.ToInt32(id_khoa)).Where(t => t.ID_moodle == 0 && s.Contains(t.ID_cb.ToString())).ToList();

            //ViewBag.SelectedIds = new SelectList(list, "Ma_sv", "ID_moodle");
            //ViewBag.Result = new SelectList(list, "ID_moodle", "Ma_sv");

            if (list.Count() == 0) return View();

            CreateGiaoVien(list);

            return View();
        }

        public static void DeleteGiaoVien(List<MoodleGiaoVien> list)
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

            if (response.Contains("exception"))
            {
                // Error
                //moodleError = serializer.Deserialize<MoodleException>(rs);
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

            UtilityController.WriteTextToFile("D:\\GiaoVienDelete.txt", response);
        }

        public ActionResult DeleteGiaoVien(string selectedVals, string id_khoa)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });

            var list = MoodleGiaoViens(Convert.ToInt32(id_khoa)).Where(t => t.ID_moodle > 0 && s.Contains(t.ID_cb.ToString())).ToList();

            if (list.Count() == 0) return View();

            DeleteGiaoVien(list);

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
                if(item.ID == "0") continue;
                    postData += "&users[" + i + "][id]=" + item.ID;
                if(item.Username != null)
                    postData += "&users[" + i + "][username]=" + item.Username;
                if(item.Password != null)
                    postData += "&users[" + i + "][password]=" + item.Password;
                if(item.Firstname != null)
                    postData += "&users[" + i + "][firstname]=" + HttpUtility.UrlEncode(item.Firstname);
                if(item.Lastname != null)
                    postData += "&users[" + i + "][lastname]=" + HttpUtility.UrlEncode(item.Lastname);
                if(item.Email != null)
                    postData += "&users[" + i + "][email]=" + "st" + item.Email;
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
                    string[] userData = AccountController.GetCurrentUserData();

                    if(GetToken(User.Identity.Name, model.OldPassword, userData[4]) == userData[3])
                    {
                        List<MoodleUser> list = new List<MoodleUser>();
                        list.Add(new MoodleUser
                        {
                            ID = userData[5],
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
        
        public static List<MoodleGetUserResponse> GetUserByID(List<string> list)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "wsfunction=core_user_get_users_by_id ";

            foreach (string item in list)
            {
                postData += "&userids[" + i + "]=" + item;
                i++;
            }

            WebRequestController web = new WebRequestController(4, "POST", postData);
            string response = web.GetResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            // MoodleException moodleError = new MoodleException();
            List<MoodleGetUserResponse> results = new List<MoodleGetUserResponse>();

            if (response.Contains("exception"))
            {
                // Error
                // moodleError = serializer.Deserialize<MoodleException>(rs);
            }
            else
            {
                // Good
                results = serializer.Deserialize<List<MoodleGetUserResponse>>(response);
            }

            UtilityController.WriteTextToFile("D:\\MoodleGetUserByID.txt", response);
            return results; ;
        }

        public ActionResult UserProfile(string userid)
        {
            List<string> list = new List<string>();
            list.Add(userid);
            var q = GetUserByID(list);
            if (q.Count() > 0)
                ViewBag.UserProfile = q.ElementAt(0);
            else
                ViewBag.UserProfile = new MoodleGetUserResponse();
            return View();
        }
    }
}
