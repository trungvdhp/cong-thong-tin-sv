using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using CongThongTinSV.Models;

namespace CongThongTinSV.Controllers
{
    public class MoodleGroupController : Controller
    {
        //
        // GET: /MoodleGroup/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetNhom(int id_lop_tc)
        {
            Entities db = new Entities();
            JsonResult result = new JsonResult();
            result.Data = new SelectList(db.MOD_NhomHocVien.Where(t => t.ID_lop_tc == id_lop_tc), "ID_nhom", "Ten_nhom");
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }

        public ActionResult CreateNhom(int id_lop_tc, string ten_nhom, string mo_ta)
        {
            Entities db = new Entities();
            string postData = "wsfunction=core_group_create_groups";
            postData += "&groups[0][courseid]=" + id_lop_tc;
            postData += "&groups[0][name]=" + HttpUtility.UrlEncode(ten_nhom);
            postData += "&groups[0][description]=" + HttpUtility.UrlEncode(mo_ta);
            //postData += "&groups[" + i + "][descriptionformat]=";
            //postData += "&groups[" + i + "][enrolmentkey]=" + HttpUtility.UrlEncode();

            WebRequestController web = new WebRequestController(4, "POST", postData);
            string response = web.GetResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            MoodleException moodleError = new MoodleException();
            List<MoodleGroup> results = new List<MoodleGroup>();

            if (response.Contains("exception"))
            {
                // Error
                //moodleError = serializer.Deserialize<MoodleException>(rs);
            }
            else
            {
                // Good
                results = serializer.Deserialize<List<MoodleGroup>>(response);

                foreach (MoodleGroup item in results)
                {
                    MOD_NhomHocVien entity = new MOD_NhomHocVien();

                    entity.ID_nhom = item.id;
                    entity.Ten_nhom = item.name;
                    entity.Mo_ta = item.description;
                    entity.ID_lop_tc = item.courseId;

                    db.MOD_NhomHocVien.Add(entity);
                }

                db.SaveChanges();
            }

            UtilityController.WriteTextToFile("D:\\NhomHocVienCreate.txt", response);

            return View();
        }
    }
}
