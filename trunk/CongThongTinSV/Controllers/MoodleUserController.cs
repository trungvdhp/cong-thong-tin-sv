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
            //Entities db = new Entities();
            //SelectList cn = new SelectList(db.STU_ChuyenNganh.OrderBy(f => f.Chuyen_nganh), "ID_chuyen_nganh", "Chuyen_nganh");
            //SelectListItem first = cn.First();
            //first.Selected = true;
            //int ID_chuyen_nganh = Convert.ToInt32(first.Value);
            //ViewBag.Message = "Danh sách sinh viên!";
            //ViewBag.ChuyenNganh = cn;
            //ViewBag.Lop = new SelectList(db.STU_Lop.Where(t=>t.ID_chuyen_nganh == ID_chuyen_nganh).OrderBy(o=>o.Ten_lop), "ID_lop", "Ten_lop");
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
                          gt.Gioi_tinh
                      };

            var sv4 = from ds in sv3.AsEnumerable()
                      join nd in db.MOD_NguoiDung.AsEnumerable()
                      on ds.ID_sv equals nd.ID_nd
                      into nguoidung
                      from nd1 in nguoidung.DefaultIfEmpty()
                      select new MoodleSinhVien
                      {
                          ID_sv = ds.ID_sv,
                          ID_moodle = (nd1 == null ? 0 : nd1.ID_moodle),
                          Ma_sv = ds.Ma_sv,
                          Ho_dem = UtilityController.GetLastName(ds.Ho_ten),
                          Ten = UtilityController.GetFirstName(ds.Ho_ten),
                          Ngay_sinh = ds.Ngay_sinh,
                          Gioi_tinh = ds.Gioi_tinh,
                          Lop = ds.Ten_lop
                      };

            return sv4.OrderByDescending(t => t.ID_moodle).ToList();
        }

        //public ActionResult GetSinhVienLop(string sidx, string sord, int page, int rows, string id_lop="", string ma_sv="", string ho_dem="", string ten="", string ngay_sinh="", string gioi_tinh="")
        //{
        //    // Get the list of students
        //    Entities db = new Entities();
        //    List<SP_SinhVien_Result> sv = db.SP_SinhVien(id_lop, ma_sv, ho_dem, ten, ngay_sinh, gioi_tinh).ToList();
        //    var students = sv.AsQueryable();

        //    // Sort the student list
        //    var sortedStudents = UtilityController.SortIQueryable<SP_SinhVien_Result>(students, sidx, sord);
        //    // Calculate the total number of pages
        //    var totalRecords = students.Count();
        //    var totalPages = (int)Math.Ceiling((double)totalRecords / (double)rows);
        //    // Prepare the data to fit the requirement of jQGrid
        //    var data = (from s in sortedStudents
        //                select new
        //                {
        //                    id = s.ID_sv,
        //                    cell = new object[] { s.ID_sv, (object)s.Ma_sv.ToString(),
        //                        s.ID_moodle==null?0:s.ID_moodle,
        //                        (object)s.Ho_dem.ToString(),
        //                        (object)s.Ten.ToString(),
        //                        (object)(s.Ngay_sinh==null?"":s.Ngay_sinh.Value.ToString("yyyy/MM/dd")),
        //                        (object)s.Gioi_tinh.ToString()}
        //                }).ToArray();
        //    // Send the data to the jQGrid
        //    var jsonData = new
        //    {
        //        total = totalPages,
        //        page = page,
        //        records = totalRecords,
        //        rows = data.Skip((page - 1) * rows).Take(rows)
        //    };

        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}

        public static void CreateSinhVien(List<MoodleSinhVien> list)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "wsfunction=core_user_create_users";

            foreach (MoodleSinhVien item in list)
            {
                postData += "&users[" + i + "][username]=" + item.Ma_sv;
                postData += "&users[" + i + "][password]=" + ((DateTime)item.Ngay_sinh).ToString("ddMMyyyy");
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

            //ViewBag.SelectedIds = new SelectList(list, "Ma_sv", "ID_moodle");
            //ViewBag.Result = new SelectList(list, "ID_moodle", "Ma_sv");

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
                i = 0;

                foreach (MoodleSinhVien item in list)
                {
                    MOD_NguoiDung entity = db.MOD_NguoiDung.FirstOrDefault(t => t.ID_moodle == item.ID_moodle);
                    db.MOD_NguoiDung.Remove(entity);
                    i++;
                }

                db.SaveChanges();
            }

            UtilityController.WriteTextToFile("D:\\SinhVienDelete.txt", response);
        }

        public ActionResult DeleteSinhVien(string selectedVals, string id_chuyen_nganh)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });

            var list = MoodleSinhViens(Convert.ToInt32(id_chuyen_nganh)).Where(t => t.ID_moodle > 0 && s.Contains(t.ID_sv.ToString())).ToList();

            //ViewBag.SelectedIds = new SelectList(list, "Ma_sv", "ID_moodle");
            //ViewBag.Result = new SelectList(list, "ID_moodle", "Ma_sv");

            if (list.Count() == 0) return View();

            DeleteSinhVien(list);

            return View();
        }
    }
}
