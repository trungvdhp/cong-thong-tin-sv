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
    public class MoodleCourseController : Controller
    {
        //
        // GET: /MoodleCourse/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LopHocPhan()
        {
            return View();
        }

        public ActionResult GetLopHocPhan([DataSourceRequest] DataSourceRequest request, string id_hocky)
        {

            return Json(MoodleLopHocPhans(id_hocky).ToDataSourceResult(request));
        }

        public IEnumerable<MoodleLopHocPhan> MoodleLopHocPhans(string id_hocky)
        {
            Entities db = new Entities();
            int hk = 0;

            try
            {
                hk = Convert.ToInt32(id_hocky);
            }
            catch (Exception) { }

            if (hk <= 0) return new List<MoodleLopHocPhan>();

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
                     select new MoodleLopHocPhan
                     {
                         ID = a.ID_lop_tc,
                         ID_moodle = (c == null ? 0 : c.ID_moodle),
                         Ky_hieu = a.Ky_hieu,
                         ID_number = a.ID_mon + "." + hocky.Hoc_ky + "." + hocky.Nam_hoc + "." + a.ID_lop_tc,
                         So_tin_chi = a.So_tin_chi,
                         Tu_ngay = a.Tu_ngay,
                         Den_ngay = a.Den_ngay,
                         Lop_hoc_phan = a.Ten_mon 
                            + a.Ky_hieu_lop_tc.Substring(a.Ky_hieu_lop_tc.IndexOf('-')) 
                            + " (N" + UtilityController.RightString("0" + a.STT_lop, 2)
                            + ")"
                     };

            return q3.OrderByDescending(t => t.ID_moodle).ToList();
        }

        public JsonResult GetMoodleLopHocPhan(string id_hocky)
        {
            Entities db = new Entities();
            JsonResult result = new JsonResult();
            result.Data = new SelectList(MoodleLopHocPhans(id_hocky).Where(t => t.ID_moodle > 0), "ID_moodle", "Lop_hoc_phan").OrderBy(t => t.Text);
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }

        public ActionResult CreateLopHocPhan(string selectedVals, string id_hocky)
        {
            Entities db = new Entities();
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            int cID = Convert.ToInt32(id_hocky);
            var list = MoodleLopHocPhans(id_hocky).Where(t => t.ID_moodle == 0 && s.Contains(t.ID.ToString())).ToList();

            if (list.Count() == 0) return View();

            int i = 0;
            string postData = "wsfunction=core_course_create_courses";

            foreach (MoodleLopHocPhan item in list)
            {
                postData += "&courses[" + i + "][fullname]=" + HttpUtility.UrlEncode(item.Lop_hoc_phan);
                postData += "&courses[" + i + "][shortname]=" + HttpUtility.UrlEncode(item.Lop_hoc_phan);
                postData += "&courses[" + i + "][categoryid]=" + id_hocky;
                postData += "&courses[" + i + "][idnumber]=" + item.ID_number;
                postData += "&courses[" + i + "][summary]=" + HttpUtility.UrlEncode(item.Lop_hoc_phan + "." + item.Ky_hieu + "." + item.ID_number +  "." + item.So_tin_chi + " tín chỉ");
                //postData += "&courses[" + i + "][summaryformat]=1";
                //postData += "&courses[" + i + "][format]=weeks";
                //postData += "&courses[" + i + "][showgrades]=1";
                postData += "&courses[" + i + "][newsitems]=0";
                postData += "&courses[" + i + "][startdate]=" + UtilityController.ConvertToTimestamp(item.Tu_ngay);
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

            WebRequestController web = new WebRequestController(4, "POST", postData);
            string response = web.GetResponse();
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

                foreach (MoodleLopHocPhan item in list)
                {
                    MOD_LopTinChi_TC entity = new MOD_LopTinChi_TC();

                    entity.ID_moodle = Convert.ToInt32(results[i].id);
                    entity.ID_danhmuc = cID;
                    entity.ID_lop_tc = item.ID;

                    db.MOD_LopTinChi_TC.Add(entity);
                    i++;
                }

                db.SaveChanges();
            }

            UtilityController.WriteTextToFile("D:\\LopHocPhanCreate.txt", response);

            return View();
        }

        public ActionResult DeleteLopHocPhan(string selectedVals, string id_hocky)
        {
            Entities db = new Entities();
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLopHocPhans(id_hocky).Where(t => t.ID_moodle > 0 && s.Contains(t.ID.ToString())).ToList();

            if (list.Count() == 0) return View();

            int i = 0;
            string postData = "wsfunction=core_course_delete_courses";

            foreach (MoodleLopHocPhan item in list)
            {
                postData += "&courseids[" + i + "]=" + item.ID_moodle;
                i++;
            }

            WebRequestController web = new WebRequestController(4, "POST", postData);
            string response = web.GetResponse();
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

                foreach (MoodleLopHocPhan item in list)
                {
                    MOD_LopTinChi_TC entity = db.MOD_LopTinChi_TC.FirstOrDefault(t => t.ID_moodle == item.ID_moodle);
                    db.MOD_LopTinChi_TC.Remove(entity);
                    i++;
                }

                db.SaveChanges();
            }

            UtilityController.WriteTextToFile("D:\\LopHocPhanDelete.txt", response);

            return View();
        }

        public ActionResult GetBaiKiemTra([DataSourceRequest] DataSourceRequest request, string courseid)
        {
            return Json(MoodleBaiKiemTras(courseid).ToDataSourceResult(request));
        }

        public IEnumerable<MoodleCourseContentResponse> MoodleBaiKiemTras(string courseid)
        {
            Entities db = new Entities();
            string postData = "wsfunction=core_course_get_contents";
            postData += "&courseid=" + courseid;
            WebRequestController web = new WebRequestController(4, "POST", postData);
            string response = web.GetResponse();
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

            UtilityController.WriteTextToFile("D:\\MoodleGetCourseContent.txt", response);
            return results;
        }

        public ActionResult DanhSachBaiKiemTra(string courseid="0")
        {
            MoodleEntities mdb = new MoodleEntities();

            try
            {
                ViewBag.CourseName = mdb.fit_course.AsEnumerable().SingleOrDefault(t => t.id.ToString() == courseid).fullname;
                ViewBag.CourseContent = MoodleBaiKiemTras(courseid);
            }
            catch (Exception)
            {
                ViewBag.CourseName = "";
                ViewBag.CourseContent = new List<MoodleCourseContentResponse>();
            }

            //ViewBag.CourseID = courseid;
            return View();
        }

        public ActionResult UpdateDiemThi(string selectedVals, string quizid = "0")
        {
            Entities db = new Entities();
            MoodleEntities mdb = new MoodleEntities();
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var quiz = mdb.fit_quiz.AsEnumerable().SingleOrDefault(t => t.id.ToString() == quizid);
            string courseid = quiz == null ? "0" : quiz.course.ToString();
            var diemthis = MoodleQuizGrades(quizid).Where(t => s.Contains(t.ID.ToString()) && t.ID_sv != 0 && t.NewGrade.HasValue).ToList();
            foreach (MoodleGradeBook grade in diemthis)
            {
                MARK_DiemThi_TC entity = db.MARK_DiemThi_TC.Single(t => t.ID_diem_thi == grade.ID_diem_thi);
                entity.Diem_thi = (float)grade.NewGrade;
                db.Entry(entity).State = System.Data.EntityState.Modified;
            }

            db.SaveChanges();

            return View();
        }

        public ActionResult BangDiemThanhPhan(string quizid = "0")
        {
            MoodleEntities mdb = new MoodleEntities();
            
            try
            {
                var quiz = mdb.fit_quiz.AsEnumerable().SingleOrDefault(t => (t.id + 10).ToString() == quizid);
                ViewBag.QuizID = quiz == null ? 0 : quiz.id;
                ViewBag.QuizName = quiz == null ? "" : quiz.name;
                ViewBag.CourseID = quiz == null ? 0 : quiz.course;
                ViewBag.CourseName = mdb.fit_course.AsEnumerable().SingleOrDefault(t => t.id == ViewBag.CourseID).fullname;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.CourseID = 0;
                ViewBag.CourseName = "";
                ViewBag.QuizID = 0;
                ViewBag.QuizName = "";
            }

            return View();
        }

        public ActionResult GetBangDiemThanhPhan([DataSourceRequest] DataSourceRequest request, string quizid = "0")
        {
            return Json(MoodleQuizGrades(quizid).ToDataSourceResult(request));
        }

        public IEnumerable<MoodleGradeBook> MoodleQuizGrades(string quizid = "0")
        {
            Entities db = new Entities();
            MoodleEntities mdb = new MoodleEntities();
            var quiz = mdb.fit_quiz.AsEnumerable().SingleOrDefault(t => t.id.ToString() == quizid);
            string courseid = quiz == null ? "0" : quiz.course.ToString();
            long qid = quiz == null ? 0 : quiz.id;
            var itemZ = mdb.fit_grade_items.SingleOrDefault(t => t.itemtype == "mod" && t.itemmodule == "quiz" && t.iteminstance == qid);
            long iz = itemZ != null ? itemZ.id : 0;
            var user = MoodleCourseHocViens(courseid);

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
                //user = from u in user
                //       join sv in students
                //       on u.ID equals sv.ID
                //       into d1
                //       from sinhvien in d1.DefaultIfEmpty()
                //       select new MoodleGradeBook
                //       {
                //           ID = u.ID,
                //           ID_sv = sinhvien == null ? 0 : sinhvien.ID_sv,
                //           UserName = u.UserName,
                //           LastName = u.LastName,
                //           FirstName = u.FirstName,
                //           NewGrade = u.NewGrade
                //       };
               
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
                           NewGrade = u.NewGrade
                       };
            }

            return user;
        }

        public CourseIDNumber GetCourseIDNumber(string courseid)
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

        public ActionResult BangDiemKhoaHoc(string courseid="0")
        {
            ViewBag.CourseID = courseid;
            MoodleEntities mdb = new MoodleEntities();

            try
            {
                ViewBag.CourseName = mdb.fit_course.AsEnumerable().SingleOrDefault(t => t.id.ToString() == courseid).fullname;
            }
            catch (Exception)
            {
                ViewBag.CourseName = "";
            }

            return View();
        }

        public ActionResult GetBangDiemKhoaHoc([DataSourceRequest] DataSourceRequest request, string courseid="0")
        {
            return Json(MoodleCourseGrades(courseid).ToDataSourceResult(request));
        }

        public IEnumerable<MoodleGradeBook> MoodleCourseHocViens(string courseid="0")
        {
            MoodleEntities mdb = new MoodleEntities();
            long context = UtilityController.GetContextID("50", courseid);
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

            return user.Where(t => !user_enrolments.Contains(t.ID));
        }

        public IEnumerable<MoodleGradeBook> MoodleCourseGrades(string courseid="0")
        {
            MoodleEntities mdb = new MoodleEntities();
            var itemZ = mdb.fit_grade_items.AsEnumerable().SingleOrDefault(t => t.courseid.ToString() == courseid && t.itemtype == "course");
            long iz = itemZ != null ? itemZ.id : 0;
            //var itemX = mdb.fit_grade_items.AsEnumerable().SingleOrDefault(t => t.courseid.ToString() == courseid && t.itemtype == "category");
            //long ix = itemX != null ? itemX.id : 0;
            var user = MoodleCourseHocViens(courseid);

            if(iz != 0)
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
    }
}
