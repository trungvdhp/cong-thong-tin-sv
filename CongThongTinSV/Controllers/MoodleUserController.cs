using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq.Expressions;
using System.Collections;

namespace CongThongTinSV.Controllers
{
    public class MoodleUserController : Controller
    {
        //
        // GET: /MoodleUser/

        public ActionResult Index()
        {
            Entities db = new Entities();
            SelectList cn = new SelectList(db.STU_ChuyenNganh.OrderBy(f => f.Chuyen_nganh), "ID_chuyen_nganh", "Chuyen_nganh");
            SelectListItem first = cn.First();
            first.Selected = true;
            int ID_chuyen_nganh = Convert.ToInt32(first.Value);
            ViewBag.Message = "Danh sách sinh viên!";
            ViewBag.ChuyenNganh = cn;
            ViewBag.Lop = new SelectList(db.STU_Lop.Where(t=>t.ID_chuyen_nganh == ID_chuyen_nganh).OrderBy(o=>o.Ten_lop), "ID_lop", "Ten_lop");
            return View();
        }

        public ActionResult GetSinhVienLop(string sidx, string sord, int page, int rows, string id_lop="", string ma_sv="", string ho_dem="", string ten="", string ngay_sinh="", string gioi_tinh="")
        {
            // Get the list of students
            Entities db = new Entities();
            List<SP_SinhVien_Result> sv = db.SP_SinhVien(id_lop, ma_sv, ho_dem, ten, ngay_sinh, gioi_tinh).ToList();
            var students = sv.AsQueryable();

            // Sort the student list
            var sortedStudents = UtilityController.SortIQueryable<SP_SinhVien_Result>(students, sidx, sord);
            // Calculate the total number of pages
            var totalRecords = students.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / (double)rows);
            // Prepare the data to fit the requirement of jQGrid
            var data = (from s in sortedStudents
                        select new
                        {
                            id = s.ID_sv,
                            cell = new object[] { s.ID_sv, (object)s.Ma_sv.ToString(),
                                s.ID_moodle==null?0:s.ID_moodle,
                                (object)s.Ho_dem.ToString(),
                                (object)s.Ten.ToString(),
                                (object)(s.Ngay_sinh==null?"":s.Ngay_sinh.Value.ToString("yyyy/MM/dd")),
                                (object)s.Gioi_tinh.ToString()}
                        }).ToArray();
            // Send the data to the jQGrid
            var jsonData = new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = data.Skip((page - 1) * rows).Take(rows)
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Register(string ids, string id_lop)
        {
            Entities db = new Entities();
            IEnumerable<string> s = ids.Split(new char[] { ',' });
            var sp = db.SP_SinhVien(id_lop, "", "", "", "", "").Where(t1 => !t1.ID_moodle.HasValue && s.Contains(t1.ID_sv.ToString()));
            int i = 0;
            string postData = "wsfunction=core_user_create_users";

            foreach(SP_SinhVien_Result sv in sp)
            {
                postData += "&users[" + i + "][username]=" + sv.Ma_sv;
                postData += "&users[" + i + "][password]=" 
                    + sv.Ngay_sinh.Value.Day.ToString()
                    + sv.Ngay_sinh.Value.Month.ToString() 
                    + sv.Ngay_sinh.Value.Year.ToString();
                postData += "&users[" + i + "][firstname]=" + HttpUtility.UrlEncode(sv.Ten);
                postData += "&users[" + i + "][lastname]=" + HttpUtility.UrlEncode(sv.Ho_dem);
                postData += "&users[" + i + "][email]=" + "st" + sv.Ma_sv + "@st.vimaru.edu.vn";
                postData += "&users[" + i + "][timezone]=7.0";
                postData += "&users[" + i + "][city]=Hai Phong";
                postData += "&users[" + i + "][country]=VN";
                i++;
            }

            WebRequestController web = new WebRequestController(4, "POST", postData);
            string rs = web.GetResponse();
            UtilityController.WriteTextToFile("D:\\user.txt", rs);
            ViewBag.SelectedIds = new SelectList(sp, "ID_sv", "Ma_sv");
            ViewBag.Result = rs;
            return View();
        }
    }
}
