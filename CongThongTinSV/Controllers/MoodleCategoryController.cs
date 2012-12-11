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
    public class MoodleCategoryController : Controller
    {
        //
        // GET: /MoodleCategory/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HocKy()
        {
            return View();
        }

        //public ActionResult ChuyenNganh()
        //{
        //    return View();
        //}

        public ActionResult GetHocKy([DataSourceRequest] DataSourceRequest request)
        {
            return Json(MoodleHocKys().ToDataSourceResult(request));
        }

        public IEnumerable<MoodleHocKy> MoodleHocKys()
        {
            Entities db = new Entities();

            return (from h1 in db.PLAN_HocKyDangKy_TC
                    join h2 in db.MOD_HocKy on h1.Ky_dang_ky equals h2.Ky_dang_ky
                    into hocky
                    from h3 in hocky.DefaultIfEmpty()
                    select new MoodleHocKy
                    {
                        ID = h1.Ky_dang_ky,
                        ID_moodle = (h3 == null ? 0 : h3.ID_moodle),
                        Dot = h1.Dot,
                        Hoc_ky = h1.Hoc_ky,
                        Nam_hoc = h1.Nam_hoc
                    }).OrderByDescending(t => t.ID_moodle).ToList();

        }

        public JsonResult GetMoodleHocKy()
        {
            Entities db = new Entities();
            JsonResult result = new JsonResult();
            IEnumerable<SelectListItem> list = from h1 in db.PLAN_HocKyDangKy_TC.AsEnumerable()
                       join h2 in db.MOD_HocKy.AsEnumerable()
                       on h1.Ky_dang_ky equals h2.Ky_dang_ky
                       select new SelectListItem
                        {
                            Value = h2.ID_moodle.ToString(),
                            Text = h1.Nam_hoc + " Kỳ " + h1.Hoc_ky + "- Đợt" + h1.Dot
                        };
            int c = Convert.ToInt32(list.Count());
            result.Data = new SelectList(list,"Value", "Text").OrderByDescending(t => t.Text);
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }

        public ActionResult CreateHocKy(string selectedVals)
        {
            Entities db = new Entities();
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });

            var list = MoodleHocKys().Where(t => t.ID_moodle == 0 && s.Contains(t.ID.ToString())).ToList();

            if (list.Count() == 0) return View();

            int i = 0;
            string postData = "&wsfunction=core_course_create_categories";

            foreach (MoodleHocKy item in list)
            {
                postData += "&categories[" + i + "][name]=" + HttpUtility.UrlEncode(item.Nam_hoc + " Kỳ " + item.Hoc_ky + "- Đợt" + item.Dot);
                postData += "&categories[" + i + "][parent]=0";
                postData += "&categories[" + i + "][idnumber]=" + item.ID;
                postData += "&categories[" + i + "][description]=" + HttpUtility.UrlEncode("Năm học " + item.Nam_hoc + " Kỳ " + item.Hoc_ky + " Đợt" + item.Dot);
                postData += "&categories[" + i + "][descriptionformat]=1";
                i++;
            }

            WebRequestController web = new WebRequestController(4, "POST", postData);
            string response = web.GetResponse();
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

                foreach (MoodleHocKy item in list)
                {
                    MOD_HocKy entity = new MOD_HocKy();

                    entity.ID_moodle = Convert.ToInt32(results[i].id);
                    entity.Ky_dang_ky = item.ID;
                    db.MOD_HocKy.Add(entity);
                    i++;
                }

                db.SaveChanges();
            }

            UtilityController.WriteTextToFile("D:\\HocKyCreate.txt", response);

            return View();
        }

        public ActionResult DeleteHocKy(string selectedVals)
        {
            Entities db = new Entities();
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleHocKys().Where(t => t.ID_moodle > 0 && s.Contains(t.ID.ToString())).ToList();

            if (list.Count() == 0) return View();

            int i = 0;
            string postData = "&wsfunction=core_course_delete_categories";

            foreach (MoodleHocKy item in list)
            {
                var loptc = db.MOD_LopTinChi_TC.Select(t => t.ID_danhmuc == item.ID_moodle).AsEnumerable();
                if (loptc.Count() > 0) continue;
                postData += "&categories[" + i + "][id]=" + item.ID_moodle;
                //postData += "&categories[" + i + "][newparent]=0";
                postData += "&categories[" + i + "][recursive]=1";
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

                foreach (MoodleHocKy item in list)
                {
                    MOD_HocKy entity = db.MOD_HocKy.FirstOrDefault(t => t.ID_moodle == item.ID_moodle);
                    db.MOD_HocKy.Remove(entity);
                    i++;
                }

                db.SaveChanges();
            }

            UtilityController.WriteTextToFile("D:\\HocKyDelete.txt", response);

            return View();
        }
    }
}
