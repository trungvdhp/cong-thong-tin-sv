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

        public ActionResult LopTinChi()
        {
            return View();
        }

        public ActionResult GetLopTinChi([DataSourceRequest] DataSourceRequest request, int id_hocky)
        {

            return Json(MoodleLopTinChis(id_hocky).ToDataSourceResult(request));
        }

        public IEnumerable<MoodleLopTinChi> MoodleLopTinChis(int id_hocky)
        {
            Entities db = new Entities();
            
            var q = db.MOD_HocKy.Single(t => t.ID_moodle == id_hocky);

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
                         mon.Ky_hieu,
                         mon.Ten_mon
                     });

            var q3 = from a in q2.AsEnumerable()
                     join b in db.MOD_LopTinChi_TC.AsEnumerable()
                     on a.ID_lop_tc equals b.ID_lop_tc
                     into lophp
                     from c in lophp.DefaultIfEmpty()
                     select new MoodleLopTinChi
                     {
                         ID = a.ID_lop_tc,
                         ID_moodle = (c == null ? 0 : c.ID_moodle),
                         Ky_hieu = a.Ky_hieu,
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

        public ActionResult CreateLopTinChi(string selectedVals, string id_hocky)
        {
            Entities db = new Entities();
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            int cID = Convert.ToInt32(id_hocky);
            var list = MoodleLopTinChis(Convert.ToInt32(cID)).Where(t => t.ID_moodle == 0 && s.Contains(t.ID.ToString())).ToList();

            //ViewBag.SelectedIds = new SelectList(list, "", "ID_moodle");
            //ViewBag.Result = new SelectList(list, "ID_moodle", "");

            if (list.Count() == 0) return View();

            int i = 0;
            string postData = "wsfunction=core_course_create_courses";

            foreach (MoodleLopTinChi item in list)
            {
                postData += "&courses[" + i + "][fullname]=" + HttpUtility.UrlEncode(item.Lop_hoc_phan);
                postData += "&courses[" + i + "][shortname]=" + HttpUtility.UrlEncode(item.Lop_hoc_phan);
                postData += "&courses[" + i + "][categoryid]=" + id_hocky;
                postData += "&courses[" + i + "][idnumber]=" + HttpUtility.UrlEncode(UtilityController.GetIdnumber(item.Lop_hoc_phan));
                postData += "&courses[" + i + "][summary]=" + HttpUtility.UrlEncode(item.Lop_hoc_phan + "-" + item.Ky_hieu + "-" + item.So_tin_chi + " tín chỉ");
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

                foreach (MoodleLopTinChi item in list)
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

            UtilityController.WriteTextToFile("D:\\LopTinChiCreate.txt", response);

            return View();
        }

        public ActionResult DeleteLopTinChi(string selectedVals, string id_hocky)
        {
            Entities db = new Entities();
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            int cID = Convert.ToInt32(id_hocky);
            var list = MoodleLopTinChis(Convert.ToInt32(cID)).Where(t => t.ID_moodle > 0 && s.Contains(t.ID.ToString())).ToList();

            //ViewBag.SelectedIds = new SelectList(list, "", "ID_moodle");
            //ViewBag.Result = new SelectList(list, "ID_moodle", "");

            if (list.Count() == 0) return View();

            int i = 0;
            string postData = "wsfunction=core_course_delete_courses";

            foreach (MoodleLopTinChi item in list)
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

                foreach (MoodleLopTinChi item in list)
                {
                    MOD_LopTinChi_TC entity = db.MOD_LopTinChi_TC.Single(t => t.ID_moodle == item.ID_moodle);
                    db.MOD_LopTinChi_TC.Remove(entity);
                    i++;
                }

                db.SaveChanges();
            }

            UtilityController.WriteTextToFile("D:\\LopTinChiDelete.txt", response);

            return View();
        }
    }
}
