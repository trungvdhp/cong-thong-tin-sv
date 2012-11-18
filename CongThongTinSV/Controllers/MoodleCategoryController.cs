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

            //ViewBag.SelectedIds = new SelectList(list, "ID", "ID_moodle");
            //ViewBag.Result = new SelectList(list, "ID_moodle", "ID");

            if (list.Count() == 0) return View();

            int i = 0;
            string postData = "&wsfunction=core_course_create_categories";

            foreach (MoodleHocKy item in list)
            {
                postData += "&categories[" + i + "][name]=" + HttpUtility.UrlEncode(item.Nam_hoc + " Kỳ " + item.Hoc_ky + "- Đợt" + item.Dot);
                postData += "&categories[" + i + "][parent]=0";
                postData += "&categories[" + i + "][idnumber]=" + HttpUtility.UrlEncode(item.Nam_hoc + "-" + item.Hoc_ky + "-" + item.Dot);
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

            //ViewBag.SelectedIds = new SelectList(list, "ID", "ID_moodle");
            //ViewBag.Result = new SelectList(list, "ID_moodle", "ID");

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

        //public ActionResult GetChuyenNganh([DataSourceRequest] DataSourceRequest request, int ky_dang_ky)
        //{
        //    return Json(MoodleChuyenNganhs(ky_dang_ky).ToDataSourceResult(request));
        //}

        //public IEnumerable<MoodleChuyenNganh> MoodleChuyenNganhs(int ky_dang_ky)
        //{
        //    Entities db = new Entities();

        //    return (from cn1 in db.STU_ChuyenNganh
        //            join cn2 in db.MOD_HocKy_ChuyenNganh.Where(t => t.Ky_dang_ky == ky_dang_ky)
        //            on cn1.ID_chuyen_nganh equals cn2.ID_chuyen_nganh
        //            into chuyennganh
        //            from cn3 in chuyennganh.DefaultIfEmpty()
        //            select new MoodleChuyenNganh
        //            {
        //                ID = cn1.ID_chuyen_nganh,
        //                ID_moodle = (cn3 == null ? 0 : cn3.ID_moodle),
        //                Ma_chuyen_nganh = cn1.Ma_chuyen_nganh,
        //                Chuyen_nganh = cn1.Chuyen_nganh
        //            }).OrderByDescending(t => t.ID_moodle).ToList();
        //}

        //public JsonResult GetMoodleChuyenNganh(int ky_dang_ky)
        //{
        //    Entities db = new Entities();
        //    JsonResult result = new JsonResult();
        //    IEnumerable<SelectListItem> list = 
        //        from cn1 in db.STU_ChuyenNganh.AsEnumerable() 
        //        join cn2 in db.MOD_HocKy_ChuyenNganh.Where(t => t.Ky_dang_ky == ky_dang_ky)
        //        on cn1.ID_chuyen_nganh equals cn2.ID_chuyen_nganh
        //        select new SelectListItem
        //        {
        //            Value = cn2.ID_moodle.ToString(),
        //            Text = cn1.Chuyen_nganh
        //        };
        //    int c = Convert.ToInt32(list.Count());
        //    result.Data = new SelectList(list, "Value", "Text").OrderBy(t => t.Text);
        //    result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

        //    return result;
        //}

        //public ActionResult CreateChuyenNganh(string selectedVals, string ky_dang_ky)
        //{
        //    Entities db = new Entities();
        //    IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
        //    int ky = Convert.ToInt32(ky_dang_ky);
        //    var list = MoodleChuyenNganhs(ky).Where(t => t.ID_moodle == 0 && s.Contains(t.ID.ToString())).ToList();

        //    //ViewBag.SelectedIds = new SelectList(list, "ID", "ID_moodle");
        //    //ViewBag.Result = new SelectList(list, "ID_moodle", "ID");

        //    if (list.Count() == 0) return View();

        //    int i = 0;
        //    string postData = "&wsfunction=core_course_create_categories";

        //    foreach (MoodleChuyenNganh item in list)
        //    {
        //        postData += "&categories[" + i + "][name]=" + HttpUtility.UrlEncode(item.Chuyen_nganh);
        //        postData += "&categories[" + i + "][parent]=" + ky_dang_ky;
        //        postData += "&categories[" + i + "][idnumber]=" + HttpUtility.UrlEncode(item.Ma_chuyen_nganh);
        //        postData += "&categories[" + i + "][description]=" + HttpUtility.UrlEncode(item.Chuyen_nganh + "-" + item.Ma_chuyen_nganh);
        //        postData += "&categories[" + i + "][descriptionformat]=1";
        //        i++;
        //    }

        //    WebRequestController web = new WebRequestController(4, "POST", postData);
        //    string response = web.GetResponse();
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    //MoodleException moodleError = new MoodleException();
        //    List<MoodleCreateCategoryResponse> results = new List<MoodleCreateCategoryResponse>();

        //    if (response.Contains("exception"))
        //    {
        //        // Error
        //        // moodleError = serializer.Deserialize<MoodleException>(rs);
        //    }
        //    else
        //    {
        //        // Good
        //        results = serializer.Deserialize<List<MoodleCreateCategoryResponse>>(response);
        //        i = 0;

        //        foreach (MoodleChuyenNganh item in list)
        //        {
        //            MOD_HocKy_ChuyenNganh entity = new MOD_HocKy_ChuyenNganh();

        //            entity.ID_moodle = Convert.ToInt32(results[i].id);
        //            entity.Ky_dang_ky = ky;
        //            entity.ID_chuyen_nganh = item.ID;
        //            db.MOD_HocKy_ChuyenNganh.Add(entity);
        //            i++;
        //        }

        //        db.SaveChanges();
        //    }

        //    UtilityController.WriteTextToFile("D:\\ChuyenNganhCreate.txt", response);

        //    return View();
        //}

        //public ActionResult DeleteChuyenNganh(string selectedVals, string ky_dang_ky)
        //{
        //    Entities db = new Entities();
        //    IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
        //    int ky = Convert.ToInt32(ky_dang_ky);
        //    var list = MoodleChuyenNganhs(ky).Where(t => t.ID_moodle > 0 && s.Contains(t.ID.ToString())).ToList();

        //    //ViewBag.SelectedIds = new SelectList(list, "ID", "ID_moodle");
        //    //ViewBag.Result = new SelectList(list, "ID_moodle", "ID");

        //    if (list.Count() == 0) return View();

        //    int i = 0;
        //    string postData = "&wsfunction=core_course_delete_categories";

        //    foreach (MoodleChuyenNganh item in list)
        //    {
        //        postData += "&categories[" + i + "][id]=" + item.ID_moodle;
        //        //postData += "&categories[" + i + "][newparent]=0";
        //        postData += "&categories[" + i + "][recursive]=1";
        //        i++;
        //    }

        //    WebRequestController web = new WebRequestController(4, "POST", postData);
        //    string response = web.GetResponse();
        //    //JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    //MoodleException moodleError = new MoodleException();

        //    if (response.Contains("exception"))
        //    {
        //        // Error
        //        // moodleError = serializer.Deserialize<MoodleException>(rs);
        //    }
        //    else
        //    {
        //        // Good
        //        // res = serializer.Deserialize<List<MoodleCreateCategoryResponse>>(rs);
        //        i = 0;

        //        foreach (MoodleChuyenNganh item in list)
        //        {
        //            MOD_HocKy_ChuyenNganh entity = db.MOD_HocKy_ChuyenNganh.Single(t => t.ID_moodle == item.ID_moodle);
        //            db.MOD_HocKy_ChuyenNganh.Remove(entity);
        //            i++;
        //        }

        //        db.SaveChanges();
        //    }

        //    UtilityController.WriteTextToFile("D:\\ChuyenNganhDelete.txt", response);

        //    return View();
        //}

    }
}
