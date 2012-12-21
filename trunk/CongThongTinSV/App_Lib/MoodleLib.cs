using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using CongThongTinSV.Models;

namespace CongThongTinSV.App_Lib
{
    public class MoodleLib
    {
        #region MoodleHelper
        /// <summary>
        /// Get context id in moodle elearning
        /// </summary>
        /// <param name="contextLevel">context level</param>
        /// <param name="instanceID">instance id</param>
        /// <returns></returns>
        public static long GetContextID(string contextLevel, string instanceID)
        {
            MoodleEntities mdb = new MoodleEntities();
            var q = mdb.fit_context.AsEnumerable().FirstOrDefault(t => t.contextlevel.ToString() == contextLevel && t.instanceid.ToString() == instanceID);

            if (q == null)
            {
                return -1;
            }
            else
            {
                return q.id;
            }
        }

        /// <summary>
        /// Get moodle user token string
        /// </summary>
        /// <param name="username">UserName</param>
        /// <param name="password">Password</param>
        /// <param name="service">Serivce shortname</param>
        /// <returns>User token string, return "exception" string if has a error</returns>
        public static string GetToken(string username, string password, string service)
        {
            //Get user token
            MyWebRequest web = new MyWebRequest(1, "POST", "username=" + username + "&password=" + password + "&service=" + service);
            string s = web.GetResponse();

            if (s.Contains("exception"))
            {
                return "exception";
            }

            string[] rs = s.Split(new char[] { '"' });
            Utility.WriteTextToFile("D:\\token.txt", service + " : " + s + " username: " + username);

            if (rs.Length == 5)
                return rs[3].Trim();

            return "exception";
        }

        /// <summary>
        /// Get course idnumber
        /// </summary>
        /// <param name="courseid">ID of course</param>
        /// <returns>CourseIDNumber(ID_mon, Hoc_ky, Nam_hoc)</returns>
        public static CourseIDNumber GetCourseIDNumber(string courseid)
        {
            Entities db = new Entities();
            MoodleEntities mdb = new MoodleEntities();
            var rs = new CourseIDNumber();
            var course = mdb.fit_course.AsEnumerable().SingleOrDefault(t => t.id.ToString() == courseid);

            if (course != null)
            {
                string[] s = course.idnumber.Split(new char[] { '.' });

                if (s.Length > 2)
                {
                    int tmp;
                    int.TryParse(s[0], out tmp);
                    rs.ID_mon = tmp;
                    int.TryParse(s[1], out tmp);
                    rs.Hoc_ky = tmp;
                    rs.Nam_hoc = s[2];
                }
            }
            else
                return null;

            return rs;
        }
        #endregion

        #region MoodleSemester
        /// <summary>
        /// Get semester table
        /// </summary>
        /// <returns>List of semesters</returns>
        public static IEnumerable<MoodleSemester> GetSemesters()
        {
            Entities db = new Entities();

            return (from h1 in db.PLAN_HocKyDangKy_TC
                    join h2 in db.MOD_HocKy on h1.Ky_dang_ky equals h2.Ky_dang_ky
                    into hocky
                    from h3 in hocky.DefaultIfEmpty()
                    select new MoodleSemester
                    {
                        ID = h1.Ky_dang_ky,
                        ID_moodle = (h3 == null ? 0 : h3.ID_moodle),
                        Dot = h1.Dot,
                        Hoc_ky = h1.Hoc_ky,
                        Nam_hoc = h1.Nam_hoc
                    }).OrderByDescending(t => t.ID_moodle).ToList();
        }

        /// <summary>
        /// Get semester list
        /// </summary>
        /// <returns>Semester SelectList</returns>
        public static SelectList GetSemesterList()
        {
            Entities db = new Entities();
            IEnumerable<SelectListItem> list = from h1 in db.PLAN_HocKyDangKy_TC.AsEnumerable()
                                               join h2 in db.MOD_HocKy
                                               on h1.Ky_dang_ky equals h2.Ky_dang_ky
                                               select new SelectListItem
                                               {
                                                   Value = h2.ID_moodle.ToString(),
                                                   Text = h1.Nam_hoc + " Kỳ " + h1.Hoc_ky + "- Đợt" + h1.Dot
                                               };
            return new SelectList(list.OrderByDescending(t => t.Text), "Value", "Text");
        }

        /// <summary>
        /// Create semester
        /// </summary>
        /// <param name="list">List of semesters</param>
        /// <returns></returns>
        public static int CreateSemesters(IEnumerable<MoodleSemester> list)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "&wsfunction=core_course_create_categories";

            foreach (MoodleSemester item in list)
            {
                postData += "&categories[" + i + "][name]=" + HttpUtility.UrlEncode(item.Nam_hoc + " Kỳ " + item.Hoc_ky + "- Đợt" + item.Dot);
                postData += "&categories[" + i + "][parent]=0";
                postData += "&categories[" + i + "][idnumber]=" + item.ID;
                postData += "&categories[" + i + "][description]=" + HttpUtility.UrlEncode("Năm học " + item.Nam_hoc + " Kỳ " + item.Hoc_ky + " Đợt" + item.Dot);
                postData += "&categories[" + i + "][descriptionformat]=1";
                i++;
            }

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\HocKyCreate.txt", response);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            //MoodleException moodleError = new MoodleException();
            List<MoodleCreateCategoryResponse> results = new List<MoodleCreateCategoryResponse>();

            if (response.Contains("exception"))
            {
                // Error
                // moodleError = serializer.Deserialize<MoodleException>(rs);
            }
            else
            {
                // Good
                results = serializer.Deserialize<List<MoodleCreateCategoryResponse>>(response);
                i = 0;

                foreach (MoodleSemester item in list)
                {
                    MOD_HocKy entity = new MOD_HocKy();

                    entity.ID_moodle = Convert.ToInt32(results[i].id);
                    entity.Ky_dang_ky = item.ID;
                    db.MOD_HocKy.Add(entity);
                    i++;
                }

                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                }
            }

            return -1;
        }

        /// <summary>
        /// Delete semester
        /// </summary>
        /// <param name="list">List of semesters</param>
        /// <returns></returns>
        public static int DeleteSemesters(IEnumerable<MoodleSemester> list)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "&wsfunction=core_course_delete_categories";
            foreach (MoodleSemester item in list)
            {
                var loptc = db.MOD_LopTinChi_TC.Where(t => t.ID_danhmuc == item.ID_moodle);
                if (loptc.Count() > 0) continue;
                postData += "&categories[" + i + "][id]=" + item.ID_moodle;
                //postData += "&categories[" + i + "][newparent]=0";
                postData += "&categories[" + i + "][recursive]=1";
                i++;
            }

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\HocKyDelete.txt", response);
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //MoodleException moodleError = new MoodleException();
            if (response.Contains("exception"))
            {
                // Error
                // moodleError = serializer.Deserialize<MoodleException>(rs);
            }
            else
            {
                // Good
                i = 0;

                foreach (MoodleSemester item in list)
                {
                    MOD_HocKy entity = db.MOD_HocKy.FirstOrDefault(t => t.ID_moodle == item.ID_moodle);
                    db.MOD_HocKy.Remove(entity);
                    i++;
                }

                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                }
            }

            return -1;
        }
        #endregion

        #region MoodleUser
        #region Student
        /// <summary>
        /// Get student table
        /// </summary>
        /// <param name="id_chuyen_nganh">ID chuyen nganh</param>
        /// <returns>List of students</returns>
        public static IEnumerable<MoodleStudent> GetStudents(string id_chuyen_nganh)
        {
            Entities db = new Entities();
            int idcn = 0;

            try
            {
                idcn = Convert.ToInt32(id_chuyen_nganh);
            }
            catch (Exception) { }

            if (idcn <= 0) return new List<MoodleStudent>();

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
            var nguoidungs = db.MOD_NguoiDung.Where(t => t.ID_nhom_nd == 3);
            var sv4 = from ds in sv3.AsEnumerable()
                      join nd in nguoidungs
                      on ds.ID_sv equals nd.ID_nd
                      into nguoidung
                      from nd1 in nguoidung.DefaultIfEmpty()
                      select new MoodleStudent
                      {
                          ID_sv = ds.ID_sv,
                          ID_moodle = (nd1 == null ? 0 : nd1.ID_moodle),
                          Ma_sv = ds.Ma_sv,
                          Ho_dem = Utility.GetLastName(ds.Ho_ten),
                          Ten = Utility.GetFirstName(ds.Ho_ten),
                          Ngay_sinh = ds.Ngay_sinh,
                          Gioi_tinh = ds.Gioi_tinh,
                          Lop = ds.Ten_lop,
                          Mat_khau = ds.Mat_khau
                      };

            return sv4.OrderByDescending(t => t.ID_moodle).ToList();
        }

        /// <summary>
        /// Create moodle account for students
        /// </summary>
        /// <param name="list">List of students</param>
        /// <returns></returns>
        public static int CreateStudents(IEnumerable<MoodleStudent> list)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "wsfunction=core_user_create_users";

            foreach (MoodleStudent item in list)
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

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\SinhVienCreate.txt", response);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            //MoodleException moodleError = new MoodleException();
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

                foreach (MoodleStudent item in list)
                {
                    MOD_NguoiDung entity = new MOD_NguoiDung();

                    entity.ID_moodle = Convert.ToInt32(results[i].id);
                    entity.ID_nd = item.ID_sv;
                    entity.ID_nhom_nd = 3;

                    db.MOD_NguoiDung.Add(entity);
                    i++;
                }

                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                }
            }
            return -1;
        }

        /// <summary>
        /// Delete moodle account of students
        /// </summary>
        /// <param name="list">List of students</param>
        /// <returns></returns>
        public static int DeleteStudents(IEnumerable<MoodleStudent> list)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "wsfunction=core_user_delete_users";

            foreach (MoodleStudent item in list)
            {
                postData += "&userids[" + i + "]=" + item.ID_moodle;
                i++;
            }

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\SinhVienDelete.txt", response);
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //MoodleException moodleError = new MoodleException();

            if (response.Contains("exception"))
            {
                // Error
                //moodleError = serializer.Deserialize<MoodleException>(rs);
            }
            else
            {
                foreach (MoodleStudent item in list)
                {
                    MOD_NguoiDung entity = db.MOD_NguoiDung.Find(item.ID_moodle);
                    db.MOD_NguoiDung.Remove(entity);
                }

                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                }
            }

            return -1;
        }

        /// <summary>
        /// Update moodle account of students
        /// </summary>
        /// <param name="list">List of students</param>
        /// <returns></returns>
        public static bool UpdateStudents(IEnumerable<MoodleStudent> list)
        {
            int i = 0;
            string postData = "wsfunction=core_user_update_users";

            foreach (MoodleStudent item in list)
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
                postData += "&users[" + i + "][idnumber]=st" + item.ID_sv;
                i++;
            }

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\SinhVienUpdate.txt", response);
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //MoodleException moodleError = new MoodleException();
            //List<MoodleCreateUserResponse> results = new List<MoodleCreateUserResponse>();

            if (response.Contains("exception"))
            {
                // Error
                // moodleError = serializer.Deserialize<MoodleException>(rs);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Synchronize moodle account of students
        /// </summary>
        /// <param name="list">List of students</param>
        public static int SyncStudents(IEnumerable<MoodleStudent> list)
        {
            Entities db = new Entities();
            MoodleEntities mdb = new MoodleEntities();
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

            try
            {
                return db.SaveChanges();
            }
            catch (Exception)
            {
                return -1;
            }
        }
        #endregion

        #region Teacher
        /// <summary>
        /// Get teacher table
        /// </summary>
        /// <param name="id_khoa">ID khoa</param>
        /// <returns>List of teachers</returns>
        public static IEnumerable<MoodleTeacher> GetTeachers(string id_khoa)
        {
            Entities db = new Entities();
            int idKhoa = 0;

            try
            {
                idKhoa = Convert.ToInt32(id_khoa);
            }
            catch (Exception) { }

            if (idKhoa <= 0) return new List<MoodleTeacher>();

            var gv1 = from gv in db.PLAN_GiaoVien.AsEnumerable()
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
            var nguoidungs = db.MOD_NguoiDung.Where(t => t.ID_nhom_nd == 2);
            var gv2 = from gv in gv1.AsEnumerable()
                      join nd in nguoidungs
                      on gv.ID_cb equals nd.ID_nd
                      into nguoidung
                      from nd1 in nguoidung.DefaultIfEmpty()
                      select new MoodleTeacher
                      {
                          ID_cb = gv.ID_cb,
                          ID_moodle = (nd1 == null ? 0 : nd1.ID_moodle),
                          Ma_cb = gv.Ma_cb,
                          Ho_dem = Utility.GetLastName(gv.Ho_ten),
                          Ten = Utility.GetFirstName(gv.Ho_ten),
                          Ngay_sinh = gv.Ngay_sinh,
                          Gioi_tinh = gv.Gioi_tinh
                      };

            return gv2.OrderByDescending(t => t.ID_moodle).ToList();
        }

        /// <summary>
        /// Create moodle account for teachers
        /// </summary>
        /// <param name="list">List of teachers</param>
        /// <returns></returns>
        public static int CreateTeachers(IEnumerable<MoodleTeacher> list)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "wsfunction=core_user_create_users";

            foreach (MoodleTeacher item in list)
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

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\GiaoVienCreate.txt", response);
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

                foreach (MoodleTeacher item in list)
                {
                    MOD_NguoiDung entity = new MOD_NguoiDung();

                    entity.ID_moodle = Convert.ToInt32(results[i].id);
                    entity.ID_nd = item.ID_cb;
                    entity.ID_nhom_nd = 2;

                    db.MOD_NguoiDung.Add(entity);
                    i++;
                }

                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                }
            }

            return -1;
        }

        /// <summary>
        /// Delete moodle account of teachers
        /// </summary>
        /// <param name="list">List of teachers</param>
        /// <returns></returns>
        public static int DeleteTeachers(IEnumerable<MoodleTeacher> list)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "wsfunction=core_user_delete_users";

            foreach (MoodleTeacher item in list)
            {
                postData += "&userids[" + i + "]=" + item.ID_moodle;
                i++;
            }

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\GiaoVienDelete.txt", response);
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //MoodleException moodleError = new MoodleException();

            if (response.Contains("exception"))
            {
                // Error
                //moodleError = serializer.Deserialize<MoodleException>(rs);
            }
            else
            {
                foreach (MoodleTeacher item in list)
                {
                    MOD_NguoiDung entity = db.MOD_NguoiDung.Find(item.ID_moodle);
                    db.MOD_NguoiDung.Remove(entity);
                }

                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                }
            }

            return -1;
        }

        /// <summary>
        /// Update moodle account of teachers
        /// </summary>
        /// <param name="list">List of teachers</param>
        /// <returns></returns>
        public static bool UpdateTeachers(IEnumerable<MoodleTeacher> list)
        {
            int i = 0;
            string postData = "wsfunction=core_user_update_users";

            foreach (MoodleTeacher item in list)
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
                postData += "&users[" + i + "][idnumber]=te" + item.ID_cb;
                i++;
            }

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //MoodleException moodleError = new MoodleException();
            //List<MoodleCreateUserResponse> results = new List<MoodleCreateUserResponse>();
            Utility.WriteTextToFile("D:\\GiaoVienUpdate.txt", response);

            if (response.Contains("exception"))
            {
                // Error
                // moodleError = serializer.Deserialize<MoodleException>(rs);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Synchronize moodle account of teachers
        /// </summary>
        /// <param name="list">List of teachers</param>
        /// <returns></returns>
        public static int SyncTeachers(IEnumerable<MoodleTeacher> list)
        {
            Entities db = new Entities();
            MoodleEntities mdb = new MoodleEntities();
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

            try
            {
                return db.SaveChanges();
            }
            catch (Exception)
            {
                return -1;
            }
        }
        #endregion

        #region AdminUser
        /// <summary>
        /// Get admin user table
        /// </summary>
        /// <returns>Admin user table</returns>
        public static IEnumerable<MoodleUser> GetAdminUsers()
        {
            Entities db = new Entities();
            var user = db.SYS_NguoiDung.AsEnumerable().Where(t => t.Active == 1);
            var nguoidungs = db.MOD_NguoiDung.Where(t => t.ID_nhom_nd == 1);
            var quantris = from ds in user
                           join nd in nguoidungs
                           on ds.UserID equals nd.ID_nd
                           into nguoidung
                           from nd1 in nguoidung.DefaultIfEmpty()
                           select new MoodleUser
                           {
                               ID = ds.UserID,
                               ID_moodle = (nd1 == null ? 0 : nd1.ID_moodle),
                               UserName = ds.UserName,
                               LastName = Utility.GetLastName(ds.FullName),
                               FirstName = Utility.GetFirstName(ds.FullName),
                               Email = ds.Email
                           };

            return quantris.OrderByDescending(t => t.ID_moodle).ToList();
        }

        /// <summary>
        /// Create moodle account for admin users
        /// </summary>
        /// <param name="list">List of admin users</param>
        /// <returns></returns>
        public static int CreateAdminUsers(IEnumerable<MoodleUser> list)
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
                postData += "&users[" + i + "][idnumber]=" + item.ID;
                i++;
            }

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\QuanTriVienCreate.txt", response);
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

                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                }
            }

            return -1;
        }

        /// <summary>
        /// Delete moodle account of admin users
        /// </summary>
        /// <param name="list">List of admin users</param>
        public static int DeleteAdminUsers(IEnumerable<MoodleUser> list)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "wsfunction=core_user_delete_users";

            foreach (MoodleUser item in list)
            {
                postData += "&userids[" + i + "]=" + item.ID_moodle;
                i++;
            }

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\QuanTriVienDelete.txt", response);
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

                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                }
            }

            return -1;
        }

        /// <summary>
        /// Update moodle account of admin users
        /// </summary>
        /// <param name="list">List of admin users</param>
        /// <returns></returns>
        public static bool UpdateAdminUsers(IEnumerable<MoodleUser> list)
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

                postData += "&users[" + i + "][idnumber]=" + item.ID;
                i++;
            }

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\QuanTriVienUpdate.txt", response);
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //MoodleException moodleError = new MoodleException();
            //List<MoodleCreateUserResponse> results = new List<MoodleCreateUserResponse>();

            if (response.Contains("exception"))
            {
                // Error
                // moodleError = serializer.Deserialize<MoodleException>(rs);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Synchronize moodle account of admin users
        /// </summary>
        /// <param name="list">List of admins users</param>
        /// <returns></returns>
        public static int SyncAdminUsers(IEnumerable<MoodleUser> list)
        {
            Entities db = new Entities();
            MoodleEntities mdb = new MoodleEntities();
            var quantriviens = from qt in list
                               join user in mdb.fit_user.AsEnumerable()
                               on qt.UserName equals user.username
                               select new
                               {
                                   ID = qt.ID,
                                   ID_moodle = user.id
                               };

            foreach (var item in quantriviens)
            {
                MOD_NguoiDung entity = new MOD_NguoiDung();

                entity.ID_moodle = Convert.ToInt32(item.ID_moodle);
                entity.ID_nd = item.ID;
                entity.ID_nhom_nd = 1;

                db.MOD_NguoiDung.Add(entity);
            }

            try
            {
                return db.SaveChanges();
            }
            catch (Exception)
            {
                return -1;
            }
        }
        #endregion

        #region UserProfile
        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="list">List of users</param>
        /// <returns></returns>
        public static bool UpdateUser(List<MoodleUser> list)
        {
            int i = 0;
            string postData = "wsfunction=core_user_update_users";

            foreach (MoodleUser item in list)
            {
                if (item.ID == 0) continue;
                postData += "&users[" + i + "][id]=" + item.ID;
                if (item.UserName != null)
                    postData += "&users[" + i + "][username]=" + item.UserName;
                if (item.Password != null)
                    postData += "&users[" + i + "][password]=" + item.Password;
                if (item.FirstName != null)
                    postData += "&users[" + i + "][firstname]=" + HttpUtility.UrlEncode(item.FirstName);
                if (item.LastName != null)
                    postData += "&users[" + i + "][lastname]=" + HttpUtility.UrlEncode(item.LastName);
                if (item.Email != null)
                    postData += "&users[" + i + "][email]=" + item.Email;
                i++;
            }

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\MoodleUserUpdate.txt", response);
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            // MoodleException moodleError = new MoodleException();

            if (response.Contains("exception"))
            {
                // Error
                // moodleError = serializer.Deserialize<MoodleException>(rs);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get moodle user by id
        /// </summary>
        /// <param name="list">List of users ids</param>
        /// <returns>List of moodle users profiles</returns>
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

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\MoodleGetUserByID.txt", response);
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

            return results;
        }

        /// <summary>
        /// Get course users profiles
        /// </summary>
        /// <param name="list">List of pairs (userid, courseid)</param>
        /// <returns>List of course users profiles</returns>
        public static List<MoodleCourseUserResponse> GetCourseUserProfiles(List<KeyValuePair<string, string>> list)
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

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\MoodleGetCourseUserProfile.txt", response);
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

            return results;
        }

        /// <summary>
        /// Get enrolled courses
        /// </summary>
        /// <param name="userid">ID of user</param>
        /// <returns></returns>
        public static IEnumerable<MoodleEnrolledCourse> GetEnrolledCourses(string userid)
        {
            List<string> users = new List<string>();
            users.Add(userid);
            var rs = GetUserByID(users);

            if (rs.Count == 0)
            {
                return new List<MoodleEnrolledCourse>();
            }

            return rs[0].enrolledcourses;
        }
        #endregion
        #endregion

        #region MoodleCourse
        /// <summary>
        /// Get courses table
        /// </summary>
        /// <param name="id_hocky">ID Hoc ky</param>
        /// <returns>List of courses</returns>
        public static IEnumerable<MoodleCourse> GetCourses(string id_hocky)
        {
            Entities db = new Entities();
            int hk = 0;

            try
            {
                hk = Convert.ToInt32(id_hocky);
            }
            catch (Exception) { }

            if (hk <= 0) return new List<MoodleCourse>();

            var q = db.MOD_HocKy.FirstOrDefault(t => t.ID_moodle == hk);
            var hocky = db.PLAN_HocKyDangKy_TC.FirstOrDefault(t => t.Ky_dang_ky == q.Ky_dang_ky);

            var q2 = (from ltc in db.PLAN_LopTinChi_TC
                      join mtc in db.PLAN_MonTinChi_TC
                      on ltc.ID_mon_tc equals mtc.ID_mon_tc
                      join mon in db.MARK_MonHoc
                      on mtc.ID_mon equals mon.ID_mon
                      where mtc.Ky_dang_ky == q.Ky_dang_ky && ltc.ID_lop_lt == 0
                      select new
                      {
                          ltc.ID_lop_tc,
                          ltc.ID_lop_lt,
                          ltc.STT_lop,
                          ltc.Tu_ngay,
                          ltc.Den_ngay,
                          mtc.Ky_hieu_lop_tc,
                          mtc.So_tin_chi,
                          mon.ID_mon,
                          mon.Ky_hieu,
                          mon.Ten_mon
                      });

            var q3 = from a in q2.AsEnumerable()
                     join b in db.MOD_LopTinChi_TC
                     on a.ID_lop_tc equals b.ID_lop_tc
                     into lophp
                     from c in lophp.DefaultIfEmpty()
                     select new MoodleCourse
                     {
                         ID = a.ID_lop_tc,
                         ID_moodle = (c == null ? 0 : c.ID_moodle),
                         ID_hocky = hk,
                         Ky_hieu = a.Ky_hieu,
                         ID_number = a.ID_mon + "." + hocky.Hoc_ky + "." + hocky.Nam_hoc + "." + a.ID_lop_tc,
                         So_tin_chi = a.So_tin_chi,
                         Tu_ngay = a.Tu_ngay,
                         Den_ngay = a.Den_ngay,
                         Lop_hoc_phan = a.Ten_mon
                            + a.Ky_hieu_lop_tc.Substring(a.Ky_hieu_lop_tc.IndexOf('-'))
                            + " (N" + Utility.RightString("0" + a.STT_lop, 2)
                            + ")"
                     };

            return q3.OrderByDescending(t => t.ID_moodle).AsEnumerable();
        }

        /// <summary>
        /// Get course list
        /// </summary>
        /// <param name="id_hocky">Semester ID</param>
        /// <returns>Course SelecList</returns>
        public static SelectList GetCourseList(string id_hocky)
        {
            return new SelectList(GetCourses(id_hocky).OrderBy(t => t.Lop_hoc_phan).Where(t => t.ID_moodle > 0), "ID_moodle", "Lop_hoc_phan");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int CreateCourses(IEnumerable<MoodleCourse> list)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "wsfunction=core_course_create_courses";

            foreach (MoodleCourse item in list)
            {
                postData += "&courses[" + i + "][fullname]=" + HttpUtility.UrlEncode(item.Lop_hoc_phan);
                postData += "&courses[" + i + "][shortname]=" + HttpUtility.UrlEncode(item.Lop_hoc_phan);
                postData += "&courses[" + i + "][categoryid]=" + item.ID_hocky;
                postData += "&courses[" + i + "][idnumber]=" + item.ID_number;
                postData += "&courses[" + i + "][summary]=" + HttpUtility.UrlEncode(item.Lop_hoc_phan + "." + item.Ky_hieu + "." + item.ID_number + "." + item.So_tin_chi + " tín chỉ");
                //postData += "&courses[" + i + "][summaryformat]=1";
                //postData += "&courses[" + i + "][format]=weeks";
                //postData += "&courses[" + i + "][showgrades]=1";
                postData += "&courses[" + i + "][newsitems]=0";
                postData += "&courses[" + i + "][startdate]=" + Utility.ConvertToTimestamp(item.Tu_ngay);
                postData += "&courses[" + i + "][numsections]=1";
                postData += "&courses[" + i + "][maxbytes]=20971520";
                //postData += "&courses[" + i + "][showreports]=0";
                postData += "&courses[" + i + "][visible]=1";
                postData += "&courses[" + i + "][hiddensections]=1";
                //postData += "&courses[" + i + "][groupmode]=0";
                //postData += "&courses[" + i + "][groupmodeforce]=0";
                //postData += "&courses[" + i + "][defaultgroupingid]=0";
                //postData += "&courses[" + i + "][enablecompletion]=0";
                //postData += "&courses[" + i + "][completionstartonenrol]=0";
                //postData += "&courses[" + i + "][completionnotify]=0";
                postData += "&courses[" + i + "][lang]=vi";
                //postData += "&courses[" + i + "][forcetheme]=";
                i++;
            }

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\LopHocPhanCreate.txt", response);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            //MoodleException moodleError = new MoodleException();
            List<MoodleCreateCourseResponse> results = new List<MoodleCreateCourseResponse>();

            if (response.Contains("exception"))
            {
                // Error
                // moodleError = serializer.Deserialize<MoodleException>(rs);
            }
            else
            {
                // Good
                results = serializer.Deserialize<List<MoodleCreateCourseResponse>>(response);
                i = 0;

                foreach (MoodleCourse item in list)
                {
                    MOD_LopTinChi_TC entity = new MOD_LopTinChi_TC();

                    entity.ID_moodle = Convert.ToInt32(results[i].id);
                    entity.ID_danhmuc = item.ID_hocky;
                    entity.ID_lop_tc = item.ID;

                    db.MOD_LopTinChi_TC.Add(entity);
                    i++;
                }

                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                }
            }

            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int DeleteCourses(IEnumerable<MoodleCourse> list)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "wsfunction=core_course_delete_courses";

            foreach (MoodleCourse item in list)
            {
                postData += "&courseids[" + i + "]=" + item.ID_moodle;
                i++;
            }

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\LopHocPhanDelete.txt", response);
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //MoodleException moodleError = new MoodleException();

            if (response.Contains("exception"))
            {
                // Error
                // moodleError = serializer.Deserialize<MoodleException>(rs);
            }
            else
            {
                // Good
                foreach (MoodleCourse item in list)
                {
                    MOD_LopTinChi_TC entity = db.MOD_LopTinChi_TC.FirstOrDefault(t => t.ID_moodle == item.ID_moodle);
                    db.MOD_LopTinChi_TC.Remove(entity);
                }

                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                }
            }

            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="courseid"></param>
        /// <returns></returns>
        public static IEnumerable<MoodleCourseContentResponse> GetCourseContents(string courseid)
        {
            Entities db = new Entities();
            string postData = "wsfunction=core_course_get_contents";
            postData += "&courseid=" + courseid;
            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\MoodleGetCourseContent.txt", response);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            // MoodleException moodleError = new MoodleException();
            List<MoodleCourseContentResponse> results = new List<MoodleCourseContentResponse>();

            if (response.Contains("exception"))
            {
                // Error
                // moodleError = serializer.Deserialize<MoodleException>(rs);
            }
            else
            {
                // Good
                results = serializer.Deserialize<List<MoodleCourseContentResponse>>(response).ToList();
            }

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quizid"></param>
        /// <returns></returns>
        public static IEnumerable<MoodleGradeBook> GetQuizGrades(string quizid = "0")
        {
            Entities db = new Entities();
            MoodleEntities mdb = new MoodleEntities();
            var quiz = mdb.fit_quiz.AsEnumerable().SingleOrDefault(t => t.id.ToString() == quizid);
            string courseid = quiz == null ? "0" : quiz.course.ToString();
            long qid = quiz == null ? 0 : quiz.id;
            var itemZ = mdb.fit_grade_items.SingleOrDefault(t => t.itemtype == "mod" && t.itemmodule == "quiz" && t.iteminstance == qid);
            long iz = itemZ != null ? itemZ.id : 0;
            var user = GetEnrolledUsers(courseid);

            if (iz != 0)
            {
                var grades = mdb.fit_grade_grades.Where(t => t.itemid == iz);
                user = from u in user
                       join z in grades
                       on u.ID equals z.userid
                       into grade
                       from g in grade.DefaultIfEmpty()
                       select new MoodleGradeBook
                       {
                           ID = u.ID,
                           UserName = u.UserName,
                           LastName = u.LastName,
                           FirstName = u.FirstName,
                           NewGrade = (g == null ? null : (g.finalgrade.HasValue ? (g.finalgrade > 10 ? g.finalgrade / 10 : g.finalgrade) : g.finalgrade))
                       };
            }

            CourseIDNumber idnumber = GetCourseIDNumber(courseid);
            if (idnumber != null)
            {
                var students = from u in user
                               join sv in db.STU_HoSoSinhVien
                               on u.UserName equals sv.Ma_sv
                               select new
                               {
                                   ID = u.ID,
                                   ID_sv = sv.ID_sv
                               };

                var diemtc = db.MARK_Diem_TC.Where(t => t.ID_mon == idnumber.ID_mon);
                var diemthitc = db.MARK_DiemThi_TC.Where(t => t.Hoc_ky_thi == idnumber.Hoc_ky && t.Nam_hoc_thi == idnumber.Nam_hoc);
                var diemthis = from sv in students
                               join d1 in diemtc
                               on sv.ID_sv equals d1.ID_sv
                               join d2 in diemthitc
                               on d1.ID_diem equals d2.ID_diem
                               select new
                               {
                                   sv,
                                   d2.Diem_thi,
                                   d2.ID_diem_thi
                               };

                user = from u in user
                       join d in diemthis
                       on u.ID equals d.sv.ID
                       into d1
                       from diem in d1.DefaultIfEmpty()
                       select new MoodleGradeBook
                       {
                           ID = u.ID,
                           ID_diem_thi = diem == null ? 0 : diem.ID_diem_thi,
                           ID_sv = diem == null ? 0 : diem.sv.ID_sv,
                           OldGrade = diem == null ? null : (float?)diem.Diem_thi,
                           UserName = u.UserName,
                           LastName = u.LastName,
                           FirstName = u.FirstName,
                           NewGrade = u.NewGrade,
                           IsDiffGrade = (diem == null && !u.NewGrade.HasValue) || 
                                         (diem != null && u.NewGrade.HasValue && string.Format("{0:0.0}", diem.Diem_thi) == string.Format("{0:0.0}", u.NewGrade))
                                         ? false : true
                       };
            }

            return user.OrderByDescending(t => t.IsDiffGrade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="courseid"></param>
        /// <returns></returns>
        public static IEnumerable<MoodleGradeBook> GetCourseGrades(string courseid = "0")
        {
            MoodleEntities mdb = new MoodleEntities();
            var itemZ = mdb.fit_grade_items.AsEnumerable().SingleOrDefault(t => t.courseid.ToString() == courseid && t.itemtype == "course");
            long iz = itemZ != null ? itemZ.id : 0;
            //var itemX = mdb.fit_grade_items.AsEnumerable().SingleOrDefault(t => t.courseid.ToString() == courseid && t.itemtype == "category");
            //long ix = itemX != null ? itemX.id : 0;
            var user = GetEnrolledUsers(courseid);

            if (iz != 0)
            {
                var grades = mdb.fit_grade_grades.Where(t => t.itemid == iz);
                user = from u in user
                       join z in grades
                       on u.ID equals z.userid
                       into grade
                       from g in grade.DefaultIfEmpty()
                       select new MoodleGradeBook
                       {
                           ID = u.ID,
                           UserName = u.UserName,
                           LastName = u.LastName,
                           FirstName = u.FirstName,
                           ZGrade = (g == null ? null : (g.finalgrade.HasValue ? (g.finalgrade > 10 ? g.finalgrade / 10 : g.finalgrade) : g.finalgrade))
                       };
            }

            //if(ix != 0)
            //{
            //    user = from u in user
            //           join x in mdb.fit_grade_grades
            //           on u.ID equals x.userid
            //           into grade
            //           from g in grade.DefaultIfEmpty()
            //           where (g == null) || (g != null && g.itemid == ix)
            //           select new MoodleGradeBook
            //           {
            //               ID = u.ID,
            //               Username = u.Username,
            //               Lastname = u.Lastname,
            //               Firstname = u.Firstname,
            //               GradeZ = u.GradeZ,
            //               GradeX = (g == null ? null : (g.finalgrade.HasValue ? (g.finalgrade > 10 ? g.finalgrade / 10 : g.finalgrade) : g.finalgrade))
            //           };
            //}

            return user;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int UpdateYGrades(IEnumerable<MoodleGradeBook> list)
        {
            Entities db = new Entities();

            foreach (MoodleGradeBook grade in list)
            {
                MARK_DiemThi_TC entity = db.MARK_DiemThi_TC.Single(t => t.ID_diem_thi == grade.ID_diem_thi);
                entity.Diem_thi = (float)grade.NewGrade;
                db.Entry(entity).State = System.Data.EntityState.Modified;
            }

            try
            {
                return db.SaveChanges();
            }
            catch (Exception)
            {
                return -1;
            }
        }
        #endregion

        #region MoodleEnrol
        #region Student
        /// <summary>
        /// Get enrolled users
        /// </summary>
        /// <param name="courseid">ID of course</param>
        /// <returns></returns>
        public static IEnumerable<MoodleGradeBook> GetEnrolledUsers(string courseid = "0")
        {
            MoodleEntities mdb = new MoodleEntities();
            long context = MoodleLib.GetContextID("50", courseid);
            long cid;
            long.TryParse(courseid, out cid);
            var role = mdb.fit_role_assignments.Where(t => t.contextid == context && t.roleid == 5);
            var enrol = mdb.fit_enrol.Where(t => t.courseid == cid);
            List<int> user_enrolments = (from u in mdb.fit_user_enrolments
                                         join e in enrol
                                         on u.enrolid equals e.id
                                         where u.status == 1
                                         select (int)u.userid).ToList();

            var user = from r in role
                       join u in mdb.fit_user
                       on r.userid equals u.id
                       select new MoodleGradeBook
                       {
                           ID = (int)u.id,
                           UserName = u.username,
                           LastName = u.lastname,
                           FirstName = u.firstname
                       };

            return user.Where(t => !user_enrolments.Contains(t.ID)).OrderBy(t => t.FirstName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_lop_tc"></param>
        /// <returns></returns>
        public static IEnumerable<MoodleStudent> GetEnrolStudentsXGrade(string id_lop_tc)
        {
            Entities db = new Entities();
            int idlop = 0;

            try
            {
                idlop = Convert.ToInt32(id_lop_tc);
            }
            catch (Exception) { }

            if (idlop <= 0) return new List<MoodleStudent>();

            var hocvien = GetEnrolStudents(id_lop_tc);
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
                              select new MoodleStudent
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
                                  DiemX = (d1 == null ? 0 : d1.Diem),
                                  Tinh_trang = ds.Tinh_trang,
                                  ID_nhom = ds.ID_nhom,
                                  Ten_nhom = ds.Ten_nhom
                              };

            return hocviendiem.OrderByDescending(t => t.Tinh_trang).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_lop_tc"></param>
        /// <returns></returns>
        public static IEnumerable<MoodleStudent> GetEnrolStudents(string id_lop_tc)
        {
            Entities db = new Entities();
            var sinhvien = GetEnrolStudentsProfile(id_lop_tc);
            var hocvien = from ds in sinhvien
                          join dk1 in db.MOD_DanhSachLopTinChi
                          on ds.ID equals dk1.ID
                          into dky
                          from dk in dky.DefaultIfEmpty()
                          select new MoodleStudent
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

            return hocvien;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_lop_tc"></param>
        /// <returns></returns>
        public static IEnumerable<MoodleStudent> GetEnrolStudentsProfile(string id_lop_tc)
        {
            Entities db = new Entities();
            int idlop = 0;

            try
            {
                idlop = Convert.ToInt32(id_lop_tc);
            }
            catch (Exception) { }

            if (idlop <= 0) return new List<MoodleStudent>();

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
                      select new MoodleStudent
                      {
                          ID_sv = ds.ID_sv,
                          Mat_khau = ds.Mat_khau,
                          ID_lop_tc = ds.ID_lop_tc,
                          ID_moodle = (nd == null ? 0 : nd.ID_moodle),
                          Ma_sv = ds.Ma_sv,
                          Ho_dem = Utility.GetLastName(ds.Ho_ten),
                          Ten = Utility.GetFirstName(ds.Ho_ten),
                          Ngay_sinh = ds.Ngay_sinh,
                          Gioi_tinh = ds.Gioi_tinh,
                          Lop = ds.Ten_lop,
                          ID = ds.ID,
                      };

            return sv3.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int ManualEnrolStudents(IEnumerable<MoodleStudent> list)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "wsfunction=enrol_manual_enrol_users";

            foreach (MoodleStudent item in list)
            {
                postData += "&enrolments[" + i + "][roleid]=5";
                postData += "&enrolments[" + i + "][userid]=" + item.ID_moodle;
                postData += "&enrolments[" + i + "][courseid]=" + item.ID_lop_tc;
                postData += "&enrolments[" + i + "][timestart]=" + Utility.ConvertToTimestamp(DateTime.Now);
                //postData += "&enrolments[" + i + "][timeend]=0";
                //postData += "&enrolments[" + i + "][suspend]=0";
                i++;
            }

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\GhiDanhSinhVienCreate.txt", response);
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

                foreach (MoodleStudent item in list)
                {
                    MOD_DanhSachLopTinChi entity = new MOD_DanhSachLopTinChi();

                    entity.ID = item.ID;
                    entity.ID_sv = item.ID_moodle;
                    entity.ID_lop_tc = item.ID_lop_tc;

                    db.MOD_DanhSachLopTinChi.Add(entity);
                }

                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                }
            }

            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int SuspendEnrolStudents(IEnumerable<MoodleStudent> list)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "wsfunction=enrol_manual_enrol_users";

            foreach (MoodleStudent item in list)
            {
                postData += "&enrolments[" + i + "][roleid]=5";
                postData += "&enrolments[" + i + "][userid]=" + item.ID_moodle;
                postData += "&enrolments[" + i + "][courseid]=" + item.ID_lop_tc;
                postData += "&enrolments[" + i + "][suspend]=1";
                i++;
            }

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\GhiDanhSinhVienDelete.txt", response);
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

                foreach (MoodleStudent item in list)
                {
                    MOD_DanhSachLopTinChi entity = db.MOD_DanhSachLopTinChi.FirstOrDefault(t => t.ID == item.ID);
                    db.MOD_DanhSachLopTinChi.Remove(entity);
                }

               try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                }
            }

            return -1;
        }
        #endregion

        #region Teacher
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_lop_tc"></param>
        /// <returns></returns>
        public static IEnumerable<MoodleTeacher> GetEnrolTeachers(string id_lop_tc)
        {
            Entities db = new Entities();
            int idlop = 0;

            try
            {
                idlop = Convert.ToInt32(id_lop_tc);
            }
            catch (Exception) { }

            if (idlop <= 0) return new List<MoodleTeacher>();

            var danhsach = from ds in db.MOD_NguoiDung_VaiTro_LopTinChi.AsEnumerable()
                           where ds.ID_lop_tc == idlop
                           select new
                           {
                               UserID = ds.UserID,
                               ID_lop_tc = ds.ID_lop_tc,
                               ID_vai_tro = ds.ID_vai_tro,
                               Vai_tro = string.Join(("\n"), GetCourseRoles(ds.ID_vai_tro, new char[] { ',' })),
                               Dinh_chi = ds.Dinh_chi
                           };
            var nguoidungs = db.MOD_NguoiDung.Where(t => t.ID_nhom_nd == 2);
            var giaovien = from gv1 in db.PLAN_GiaoVien.AsEnumerable()
                           join gt in db.STU_GioiTinh
                           on gv1.ID_gioi_tinh equals gt.ID_gioi_tinh
                           join gv2 in nguoidungs
                           on gv1.ID_cb equals gv2.ID_nd
                           into dsgv
                           from gv3 in dsgv.DefaultIfEmpty()
                           select new
                           {
                               ID_moodle = (gv3 == null ? 0 : gv3.ID_moodle),
                               ID_cb = gv1.ID_cb,
                               ID_khoa = gv1.ID_khoa,
                               Ma_cb = gv1.Ma_cb,
                               Ho_dem = Utility.GetLastName(gv1.Ho_ten),
                               Ten = Utility.GetFirstName(gv1.Ho_ten),
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
                    select new MoodleTeacher
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
                        Tinh_trang = gv1.ID_moodle == 0 ? "Chưa có tài khoản" : (gv == null ? "Chưa ghi danh" : gv.Dinh_chi ? "Chưa kích hoạt" : "Đã kích hoạt")
                    };

            return q.OrderByDescending(t => t.ID_vai_tro).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="id_vai_tro"></param>
        /// <param name="suspended"></param>
        /// <returns></returns>
        public static int ManualEnrolTeachers(IEnumerable<MoodleTeacher> list, string id_vai_tro, string suspended)
        {
            Entities db = new Entities();
            int i = 0;

            string postData = "wsfunction=enrol_manual_enrol_users";

            foreach (MoodleTeacher item in list)
            {
                postData += "&enrolments[" + i + "][roleid]=" + id_vai_tro;
                postData += "&enrolments[" + i + "][userid]=" + item.ID_moodle;
                postData += "&enrolments[" + i + "][courseid]=" + item.ID_lop_tc;
                postData += "&enrolments[" + i + "][timestart]=" + Utility.ConvertToTimestamp(DateTime.Now);
                //postData += "&enrolments[" + i + "][timeend]=0";
                postData += "&enrolments[" + i + "][suspend]=" + suspended;
                i++;
            }

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\GhiDanhGiaoVienCreate.txt", response);
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

                foreach (MoodleTeacher item in list)
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
                        if (!Utility.InArray(item.ID_vai_tro, new char[] { ',' }, id_vai_tro))
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

                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                }
            }

            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="id_vai_tro"></param>
        /// <returns></returns>
        public static int UnassignTeachersRole(IEnumerable<MoodleTeacher> list, string id_vai_tro)
        {
            Entities db = new Entities();
            int i = 0;

            string postData = "wsfunction=core_role_unassign_roles";

            foreach (MoodleTeacher item in list)
            {
                postData += "&unassignments[" + i + "][roleid]=" + id_vai_tro;
                postData += "&unassignments[" + i + "][userid]=" + item.ID_moodle;
                postData += "&unassignments[" + i + "][contextid]=" + MoodleLib.GetContextID("50", item.ID_lop_tc.ToString());
                i++;
            }

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\GhiDanhGiaoVien_Unassign_VaiTro.txt", response);
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

                foreach (MoodleTeacher item in list)
                {
                    MOD_NguoiDung_VaiTro_LopTinChi entity;
                    entity = db.MOD_NguoiDung_VaiTro_LopTinChi.SingleOrDefault(t => t.ID_lop_tc == item.ID_lop_tc && t.UserID == item.ID_moodle);
                    List<string> vaitro = entity.ID_vai_tro.Split(new char[] { ',' }).ToList();

                    if (vaitro.Remove(id_vai_tro))
                    {
                        entity.ID_vai_tro = string.Join(",", vaitro);
                    }

                    db.Entry(entity).State = System.Data.EntityState.Modified;
                }

                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                }
            }

            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int UnassignTeachersAllRoles(IEnumerable<MoodleTeacher> list)
        {
            Entities db = new Entities();
            int i = 0;

            string postData = "wsfunction=core_role_unassign_roles";

            foreach (MoodleTeacher item in list)
            {
                string[] ids = item.ID_vai_tro.Split(new char[] { ',' });

                foreach (string id in ids)
                {
                    postData += "&unassignments[" + i + "][roleid]=" + id;
                    postData += "&unassignments[" + i + "][userid]=" + item.ID_moodle;
                    postData += "&unassignments[" + i + "][contextid]=" + MoodleLib.GetContextID("50", item.ID_lop_tc.ToString());
                    i++;
                }
            }

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\GhiDanhGiaoVien_UnassignAll_VaiTro.txt", response);
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

                foreach (MoodleTeacher item in list)
                {
                    MOD_NguoiDung_VaiTro_LopTinChi entity;
                    entity = db.MOD_NguoiDung_VaiTro_LopTinChi.SingleOrDefault(t => t.ID_lop_tc == item.ID_lop_tc && t.UserID == item.ID_moodle);
                    entity.ID_vai_tro = "";

                    db.Entry(entity).State = System.Data.EntityState.Modified;
                }

                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                }
            }

            return -1;
        }
        #endregion
        #endregion

        #region MoodleGroup
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_lop_tc"></param>
        /// <returns></returns>
        public static IEnumerable<MoodleGroup> GetGroups(string id_lop_tc)
        {
            Entities db = new Entities();
            int idlop = 0;

            try
            {
                idlop = Convert.ToInt32(id_lop_tc);
            }
            catch (Exception) { }

            if (idlop <= 0) return new List<MoodleGroup>();

            var nhomhv = from nhom in db.MOD_NhomHocVien
                         where nhom.ID_lop_tc == idlop
                         select new MoodleGroup
                         {
                             ID_nhom = nhom.ID_nhom,
                             ID_lop_tc = idlop,
                             ID_to = (nhom.ID_to == null ? 0 : (int)nhom.ID_to),
                             Ten_to = (nhom.ID_to == null ? "" : nhom.MOD_ToNhom.Ten_to),
                             Ten_nhom = nhom.Ten_nhom,
                             Mo_ta = nhom.Mo_ta
                         };
            return nhomhv.OrderByDescending(t => t.ID_nhom).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_lop_tc"></param>
        /// <returns></returns>
        public static SelectList GetGroupList(int id_lop_tc)
        {
            Entities db = new Entities();
            return new SelectList(db.MOD_NhomHocVien.Where(t => t.ID_lop_tc == id_lop_tc).OrderByDescending(t => t.ID_nhom), "ID_nhom", "Ten_nhom");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ten_nhom"></param>
        /// <param name="mo_ta"></param>
        /// <param name="id_lop_tc"></param>
        /// <returns></returns>
        public static int CreateGroups(IEnumerable<MoodleGroup> list)
        {
            Entities db = new Entities();
            string postData = "wsfunction=core_group_create_groups";
            int i = 0;

            foreach (MoodleGroup item in list)
            {
                postData += "&groups[" + i + "][courseid]=" + item.ID_lop_tc;
                postData += "&groups[" + i + "][name]=" + HttpUtility.UrlEncode(item.Ten_nhom);
                postData += "&groups[" + i + "][description]=" + HttpUtility.UrlEncode(item.Mo_ta);
                //postData += "&groups[" + i + "][descriptionformat]=";
                //postData += "&groups[" + i + "][enrolmentkey]=" + HttpUtility.UrlEncode();
                i++;
            }

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\NhomHocVienCreate.txt", response);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            //MoodleException moodleError = new MoodleException();
            List<MoodleCreateGroupRespond> results = new List<MoodleCreateGroupRespond>();

            if (response.Contains("exception"))
            {
                // Error
                //moodleError = serializer.Deserialize<MoodleException>(rs);
            }
            else
            {
                // Good
                results = serializer.Deserialize<List<MoodleCreateGroupRespond>>(response);

                foreach (MoodleCreateGroupRespond item in results)
                {
                    MOD_NhomHocVien entity = new MOD_NhomHocVien();

                    entity.ID_nhom = item.id;
                    entity.Ten_nhom = item.name;
                    entity.Mo_ta = item.description;
                    entity.ID_lop_tc = item.courseId;

                    db.MOD_NhomHocVien.Add(entity);
                }

                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                }
            }

            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int DeleteGroups(IEnumerable<string> list)
        {
            Entities db = new Entities();
            string postData = "wsfunction=core_group_delete_groups";
            int i = 0;

            foreach(string item in list)
            {
                postData += "&groupids[" + i + "]=" + item;
                i++;
            }

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\NhomHocVienDelete.txt", response);
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
                //results = serializer.Deserialize<List<MoodleGroup>>(response);

                foreach (string sid in list)
                {
                    int id_nhom = Convert.ToInt32(sid);
                    MOD_NhomHocVien entity = db.MOD_NhomHocVien.FirstOrDefault(t => t.ID_nhom == id_nhom);
                    db.MOD_NhomHocVien.Remove(entity);
                    var danhsach = db.MOD_DanhSachLopTinChi.Where(t => t.ID_nhom == id_nhom);

                    foreach (MOD_DanhSachLopTinChi item in danhsach)
                    {
                        item.ID_nhom = null;
                        db.Entry(item).State = System.Data.EntityState.Modified;
                    }
                }
                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                }
            }

            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="id_nhom"></param>
        /// <returns></returns>
        public static int AddGroupMembers(IEnumerable<MoodleStudent> list, string id_nhom)
        {
            Entities db = new Entities();
            string postData = "wsfunction=core_group_add_group_members";
            int i = 0;

            foreach (MoodleStudent item in list)
            {
                postData += "&members[" + i + "][groupid]=" + id_nhom;
                postData += "&members[" + i + "][userid]=" + item.ID_moodle;
                i++;
            }

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\NhomHocVienAddThanhVien.txt", response);
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

                foreach (MoodleStudent item in list)
                {
                    MOD_DanhSachLopTinChi entity = db.MOD_DanhSachLopTinChi.Find(item.ID);
                    entity.ID_nhom = (int?)Convert.ToInt32(id_nhom);
                    db.Entry(entity).State = System.Data.EntityState.Modified;
                    i++;
                }
                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                }
            }

            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="id_nhom"></param>
        /// <returns></returns>
        public static int DeleteGroupMembers(IEnumerable<MoodleStudent> list, string id_nhom)
        {
            Entities db = new Entities();
            string postData = "wsfunction=core_group_delete_group_members";
            int i = 0;

            foreach (MoodleStudent item in list)
            {
                postData += "&members[" + i + "][groupid]=" + id_nhom;
                postData += "&members[" + i + "][userid]=" + item.ID_moodle;
                i++;
            }

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\NhomHocVienDeleteThanhVien.txt", response);
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

                foreach (MoodleStudent item in list)
                {
                    MOD_DanhSachLopTinChi entity = db.MOD_DanhSachLopTinChi.Find(item.ID);
                    entity.ID_nhom = null;
                    db.Entry(entity).State = System.Data.EntityState.Modified;
                    i++;
                }

                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                }
            }

            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_lop_tc"></param>
        /// <returns></returns>
        public static SelectList GetGroupingList(int id_lop_tc)
        {
            Entities db = new Entities();
            return new SelectList(db.MOD_ToNhom.Where(t => t.ID_lop_tc == id_lop_tc).OrderByDescending(t => t.ID_to), "ID_to", "Ten_to");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int CreateGroupings(IEnumerable<MoodleGroup> list)
        {
            Entities db = new Entities();
            string postData = "wsfunction=core_group_create_groupings";
            int i = 0;

            foreach (MoodleGroup item in list)
            {
                postData += "&groupings[" + i + "][courseid]=" + item.ID_lop_tc;
                postData += "&groupings[" + i + "][name]=" + HttpUtility.UrlEncode(item.Ten_to);
                postData += "&groupings[" + i + "][description]=" + HttpUtility.UrlEncode(item.Mo_ta);
                //postData += "&groups[" + i + "][descriptionformat]=";
            }

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\ToNhomCreate.txt", response);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            //MoodleException moodleError = new MoodleException();
            List<MoodleCreateGroupingRespond> results = new List<MoodleCreateGroupingRespond>();

            if (response.Contains("exception"))
            {
                // Error
                //moodleError = serializer.Deserialize<MoodleException>(rs);
            }
            else
            {
                // Good
                results = serializer.Deserialize<List<MoodleCreateGroupingRespond>>(response);

                foreach (MoodleCreateGroupingRespond item in results)
                {
                    MOD_ToNhom entity = new MOD_ToNhom();

                    entity.ID_to = item.id;
                    entity.Ten_to = item.name;
                    entity.Mo_ta = item.description;
                    entity.ID_lop_tc = item.courseId;

                    db.MOD_ToNhom.Add(entity);
                }

                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                }
            }

            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int DeleteGroupings(IEnumerable<string> list)
        {
            Entities db = new Entities();
            string postData = "wsfunction=core_group_delete_groupings";
            int i = 0;

            foreach(string item in list)
            {
                postData += "&groupingids[" + i + "]=" + item;
                i++;
            }

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\ToNhomDelete.txt", response);
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
                //results = serializer.Deserialize<List<MoodleGroup>>(response);

                foreach (string sid in list)
                {
                    int id_to = Convert.ToInt32(sid);
                    MOD_ToNhom entity = db.MOD_ToNhom.FirstOrDefault(t => t.ID_to == id_to);
                    db.MOD_ToNhom.Remove(entity);
                    var danhsach = db.MOD_NhomHocVien.Where(t => t.ID_to == id_to);

                    foreach (MOD_NhomHocVien item in danhsach)
                    {
                        item.ID_to = null;
                        db.Entry(item).State = System.Data.EntityState.Modified;
                    }
                }

                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                }
            }

            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int UpdateGroupings(IEnumerable<MoodleGroup> list)
        {
            Entities db = new Entities();
            string postData = "wsfunction=core_group_update_groupings";
            int i = 0;

            foreach (MoodleGroup item in list)
            {
                postData += "&groupings[" + i + "][id]=" + item.ID_to;
                postData += "&groupings[" + i + "][name]=" + HttpUtility.UrlEncode(item.Ten_to);
                postData += "&groupings[" + i + "][description]=" + HttpUtility.UrlEncode(item.Mo_ta);
                //postData += "&groups[" + i + "][descriptionformat]=";
                i++;
            }

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\ToNhomUpdate.txt", response);
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
                //results = serializer.Deserialize<List<MoodleCreateGroupingRespond>>(response);
                foreach (MoodleGroup item in list)
                {
                    MOD_ToNhom entity = db.MOD_ToNhom.Find(item.ID_to);
                    entity.Ten_to = item.Ten_to;
                    entity.Mo_ta = item.Mo_ta;
                    db.Entry(entity).State = System.Data.EntityState.Modified;
                }
                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                }
            }

            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="id_to"></param>
        public static int AssignGrouping(IEnumerable<MoodleGroup> list, string id_to)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "wsfunction=core_group_assign_grouping";

            foreach (MoodleGroup item in list)
            {
                postData += "&assignments[" + i + "][groupingid]=" + id_to;
                postData += "&assignments[" + i + "][groupid]=" + item.ID_nhom;
                i++;
            }

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\ToNhomAssign.txt", response);
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

                foreach (MoodleGroup item in list)
                {
                    MOD_NhomHocVien entity = db.MOD_NhomHocVien.Find(item.ID_nhom);
                    entity.ID_to = (int?)Convert.ToInt32(id_to);
                    db.Entry(entity).State = System.Data.EntityState.Modified;
                    i++;
                }

                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                }
            }

            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedVals"></param>
        /// <param name="id_lop_tc"></param>
        /// <param name="id_to"></param>
        /// <returns></returns>
        public static int UnassignGrouping(IEnumerable<MoodleGroup> list, string id_to)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "wsfunction=core_group_unassign_grouping";

            foreach (MoodleGroup item in list)
            {
                postData += "&unassignments[" + i + "][groupingid]=" + id_to;
                postData += "&unassignments[" + i + "][groupid]=" + item.ID_nhom;
                i++;
            }

            MyWebRequest web = new MyWebRequest(4, "POST", postData);
            string response = web.GetResponse();
            //Utility.WriteTextToFile("D:\\ToNhomUnassign.txt", response);
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

                foreach (MoodleGroup item in list)
                {
                    MOD_NhomHocVien entity = db.MOD_NhomHocVien.Find(item.ID_nhom);
                    entity.ID_to = null;
                    db.Entry(entity).State = System.Data.EntityState.Modified;
                    i++;
                }

                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                }
            }

            return -1;
        }
        #endregion

        #region MoodleRole
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idArray"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string[] GetCourseRoles(string idArray, char[] separator)
        {
            MoodleEntities mdb = new MoodleEntities();
            string[] ids = idArray.Split(separator);
            int len = ids.Length;

            for (int i = 0; i < len; i++)
            {
                if (ids[i] == "")
                {
                    continue;
                }

                long id = Convert.ToInt64(ids[i]);
                ids[i] = mdb.fit_role.Single(t => t.id == id).name;
            }

            return ids;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextLevel"></param>
        /// <returns></returns>
        public static SelectList GetRoles(long contextLevel)
        {
            MoodleEntities mdb = new MoodleEntities();
            var role = from rc in mdb.fit_role_context_levels
                       join r in mdb.fit_role
                       on rc.roleid equals r.id
                       where rc.contextlevel == contextLevel
                       select r;

            return new SelectList(role.ToList(), "id", "name");
        }
        #endregion

        #region MoodleWebService
        /// <summary>
        /// Get moodle webservices
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<MoodleWebService> GetWebServices()
        {
            Entities db = new Entities();

            return from dv in db.MOD_DichVu
                   where dv.Root == null
                   select new MoodleWebService
                   {
                       ID_dv= dv.ID_dv,
                       Ten_dv = dv.Ten_dv,
                       Ten_rut_gon = dv.Ten_rut_gon
                   };
        }

        /// <summary>
        /// Get webservice list
        /// </summary>
        /// <returns></returns>
        public static SelectList GetWebServiceList()
        {
            Entities db = new Entities();

            return new SelectList(db.MOD_DichVu.Where(t => t.Root == null), "ID_dv", "Ten_dv");
        }

        /// <summary>
        /// Create moodle webService
        /// </summary>
        /// <param name="webservice">WebService</param>
        /// <returns></returns>
        public static int CreateWebService(MoodleWebService webservice)
        {
            Entities db = new Entities();
            db.MOD_DichVu.Add(new MOD_DichVu
            {
                Ten_dv = webservice.Ten_dv,
                Ten_rut_gon = webservice.Ten_rut_gon
            });

            try
            {
                return db.SaveChanges();
            }
            catch (Exception)
            {
            }

            return -1;
        }

        /// <summary>
        /// Delete webservice
        /// </summary>
        /// <param name="webservice">webservice</param>
        /// <returns></returns>
        public static int DeleteWebService(MoodleWebService webservice)
        {
            Entities db = new Entities();
            MOD_DichVu entity = db.MOD_DichVu.SingleOrDefault(t => t.ID_dv == webservice.ID_dv);

            if (entity != null)
            {
                db.MOD_DichVu.Remove(entity);
            }

            try
            {
                return db.SaveChanges();
            }
            catch (Exception)
            {
            }

            return -1;
        }

        /// <summary>
        /// Delete list of webservices
        /// </summary>
        /// <param name="ids">list of webServices ids</param>
        /// <returns></returns>
        public static int DeleteWebServices(IEnumerable<string> ids)
        {
            Entities db = new Entities();

            foreach (string id in ids)
            {
                MOD_DichVu entity = db.MOD_DichVu.AsEnumerable().SingleOrDefault(t => t.ID_dv.ToString() == id);

                if (entity != null)
                {
                    db.MOD_DichVu.Remove(entity);
                }
            }

            try
            {
                return db.SaveChanges();
            }
            catch (Exception)
            {
            }

            return -1;
        }

        /// <summary>
        /// Update moodle webservice
        /// </summary>
        /// <param name="webservice">WebService</param>
        /// <returns></returns>
        public static int UpdateWebService(MoodleWebService webservice)
        {
            Entities db = new Entities();
            MOD_DichVu entity = db.MOD_DichVu.SingleOrDefault(t => t.ID_dv == webservice.ID_dv);

            if (entity != null)
            {
                entity.Ten_dv = webservice.Ten_dv;
                entity.Ten_rut_gon = webservice.Ten_rut_gon;
                db.Entry(entity).State = System.Data.EntityState.Modified;
            }

            try
            {
                return db.SaveChanges();
            }
            catch (Exception)
            {
            }

            return -1;
        }

        /// <summary>
        /// Synchronize webservice
        /// </summary>
        /// <returns></returns>
        public static int SyncWebService()
        {
            Entities db = new Entities();
            MoodleEntities mdb = new MoodleEntities();
            List<string> shortnames = db.MOD_DichVu.Select(t => t.Ten_rut_gon).ToList();
            IEnumerable<fit_external_services> services = mdb.fit_external_services.Where(t => !shortnames.Contains(t.shortname)).AsEnumerable();

            foreach (fit_external_services item in services)
            {
                if (item.shortname != null && item.shortname != "")
                {
                    db.MOD_DichVu.Add(new MOD_DichVu
                    {
                        Ten_dv = item.name,
                        Ten_rut_gon = item.shortname
                    });
                }
            }

            try
            {
                return db.SaveChanges();
            }
            catch (Exception)
            {
            }

            return -1;
        }
        #endregion

        #region MoodleCapability
        /// <summary>
        /// Get capabilities
        /// </summary>
        /// <param name="id_dv">ID of webservice</param>
        /// <returns></returns>
        public static IEnumerable<Capability> GetCapabilities(string id_dv)
        {
            int id;
            int.TryParse(id_dv, out id);

            if (id <= 0) return new List<Capability>();

            Entities db = new Entities();
            var dichvu = db.MOD_DichVu.SingleOrDefault(t => t.ID_dv == id);

            if (dichvu == null) return new List<Capability>();

            var quyens = db.MOD_Quyen.AsEnumerable().OrderBy(t => t.Action_name.Split(new char[] { '.' })[0]);

            return (from q1 in quyens
                   join q2 in dichvu.MOD_Quyen
                   on q1.ID_quyen equals q2.ID_quyen
                   into q
                   from quyen in q.DefaultIfEmpty()
                   select new Capability
                   {
                       ID_nhom = dichvu.ID_dv,
                       ID_quyen = q1.ID_quyen,
                       Ten_quyen = q1.Ten_quyen,
                       Action_name = q1.Action_name,
                       Tinh_trang = (quyen == null ? "" : "Đã kích hoạt")
                   }).OrderByDescending(t => t.Tinh_trang);
        }

        /// <summary>
        /// Assign capabilities
        /// </summary>
        /// <param name="list">List of capabilities</param>
        /// <param name="id_dv">ID of webservice</param>
        /// <returns></returns>
        public static int AssignCapabilities(IEnumerable<Capability> list, string id_dv)
        {
            int id;
            int.TryParse(id_dv, out id);
            if (id <= 0) return -1;

            Entities db = new Entities();

            var dichvu = db.MOD_DichVu.Find(id);

            if (dichvu == null) return -1;

            foreach (Capability item in list)
            {
                MOD_Quyen entity = db.MOD_Quyen.Find(item.ID_quyen);
                dichvu.MOD_Quyen.Add(entity);
            }

            try
            {
                return db.SaveChanges();
            }
            catch (Exception)
            {
            }

            return -1;
        }

        /// <summary>
        /// Unassign capabilities
        /// </summary>
        /// <param name="list">List of capabilities</param>
        /// <param name="id_dv">ID of webservice</param>
        /// <returns></returns>
        public static int UnassignCapabilities(IEnumerable<Capability> list, string id_dv)
        {
            int id;
            int.TryParse(id_dv, out id);
            if (id <= 0) return -1;

            Entities db = new Entities();

            var dichvu = db.MOD_DichVu.Find(id);

            if (dichvu == null) return -1;

            foreach (Capability item in list)
            {
                MOD_Quyen entity = dichvu.MOD_Quyen.SingleOrDefault(t => t.ID_quyen == item.ID_quyen);
                dichvu.MOD_Quyen.Remove(entity);
            }

            try
            {
                return db.SaveChanges();
            }
            catch (Exception)
            {
            }

            return -1;
        }
        #endregion
    }
}