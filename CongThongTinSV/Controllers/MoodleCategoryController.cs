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
        public ActionResult GetHocKy([DataSourceRequest] DataSourceRequest request)
        {
            return Json(MoodleHocKy().ToDataSourceResult(request));
        }

        public IEnumerable<MoodleHocKy> MoodleHocKy()
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
                    }).ToList();

        }

        public ActionResult CreateHocKy(string selectedVals)
        {
            Entities db = new Entities();
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });

            var sp = MoodleHocKy().Where(t => t.ID_moodle == 0 && s.Contains(t.ID.ToString())).ToList();
            int i = 0;
            string postData = "&wsfunction=core_course_create_categories";

            foreach (MoodleHocKy hk in sp)
            {
                postData += "&categories[" + i + "][name]=" + HttpUtility.UrlEncode(hk.Nam_hoc + " Kỳ " + hk.Hoc_ky + "- Đợt" + hk.Dot);
                postData += "&categories[" + i + "][parent]=0";
                postData += "&categories[" + i + "][idnumber]=" + HttpUtility.UrlEncode(hk.Nam_hoc + "-" + hk.Hoc_ky + "-" + hk.Dot);
                postData += "&categories[" + i + "][description]=" + HttpUtility.UrlEncode("Năm học " + hk.Nam_hoc + " Kỳ " + hk.Hoc_ky + " Đợt" + hk.Dot);
                postData += "&categories[" + i + "][descriptionformat]=1";
                i++;
            }

            WebRequestController web = new WebRequestController(4, "POST", postData);
            string rs = web.GetResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            MoodleException moodleError = new MoodleException();
            List<MoodleCreateCategoryResponse> res = new List<MoodleCreateCategoryResponse>();

            if (rs.Contains("exception"))
            {
                // Error
                moodleError = serializer.Deserialize<MoodleException>(rs);
            }
            else
            {
                // Good
                res = serializer.Deserialize<List<MoodleCreateCategoryResponse>>(rs);
                i = 0;

                foreach (MoodleHocKy hk in sp)
                {
                    MOD_HocKy hocky = new MOD_HocKy();

                    hocky.ID_moodle = Convert.ToInt32(res[i].id);
                    hocky.Ky_dang_ky = hk.ID;
                    db.MOD_HocKy.Add(hocky);
                    i++;
                }

                db.SaveChanges();
            }

            UtilityController.WriteTextToFile("D:\\HocKyCreate.txt", rs);

            //ViewBag.SelectedIds = new SelectList(sp, "Ma_sv", "ID_moodle");
            //ViewBag.Result = new SelectList(sp, "ID_moodle", "Ma_sv");
            return View();
        }

        public ActionResult DeleteHocKy(string selectedVals)
        {
            Entities db = new Entities();
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            return View();
        }
    }
}
