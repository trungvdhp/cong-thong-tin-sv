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

        public JsonResult GetMoodleNhom(int id_lop_tc)
        {
            Entities db = new Entities();
            JsonResult result = new JsonResult();
            result.Data = new SelectList(db.MOD_NhomHocVien.Where(t => t.ID_lop_tc == id_lop_tc), "ID_nhom", "Ten_nhom");
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }

        public ActionResult CreateNhom(string ten_nhom, string mo_ta, int id_lop_tc)
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

        public ActionResult DeleteNhom(string selectedVals)
        {
            string[] list = selectedVals.Split(new char[] { ',' });
            
            if (list.Count() == 0) return View();

            Entities db = new Entities();
            string postData = "wsfunction=core_group_delete_groups";
            int len = list.Count();

            for (int i = 0; i < len; i++)
            {
                postData += "&groupids[" + i + "]=" + list[i];
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
                        db.MOD_DanhSachLopTinChi.Remove(item);
                    }
                }

                db.SaveChanges();
            }

            UtilityController.WriteTextToFile("D:\\NhomHocVienDelete.txt", response);

            return View();
        }

        public static void AddThanhVien(List<MoodleSinhVien> list, string id_nhom)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "wsfunction=core_group_add_group_members";

            foreach (MoodleSinhVien item in list)
            {
                postData += "&members[" + i + "][groupid]=" + id_nhom;
                postData += "&members[" + i + "][userid]=" + item.ID_moodle;
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
                // Good
                //results = serializer.Deserialize<List<MoodleCreateUserResponse>>(response);
                i = 0;

                foreach (MoodleSinhVien item in list)
                {
                    MOD_DanhSachLopTinChi entity = db.MOD_DanhSachLopTinChi.Find(item.ID);
                    entity.ID_nhom = (int?) Convert.ToInt32(id_nhom);
                    db.Entry(entity).State = System.Data.EntityState.Modified;
                    i++;
                }

                db.SaveChanges();
            }

            UtilityController.WriteTextToFile("D:\\GroupAddThanhVien.txt", response);
        }

        public static void DeleteThanhVien(List<MoodleSinhVien> list, string id_nhom)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "wsfunction=core_group_delete_group_members";

            foreach (MoodleSinhVien item in list)
            {
                postData += "&members[" + i + "][groupid]=" + id_nhom;
                postData += "&members[" + i + "][userid]=" + item.ID_moodle;
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
                // Good
                //results = serializer.Deserialize<List<MoodleCreateUserResponse>>(response);
                i = 0;

                foreach (MoodleSinhVien item in list)
                {
                    MOD_DanhSachLopTinChi entity = db.MOD_DanhSachLopTinChi.Find(item.ID);
                    entity.ID_nhom = null;
                    db.Entry(entity).State = System.Data.EntityState.Modified;
                    i++;
                }

                db.SaveChanges();
            }

            UtilityController.WriteTextToFile("D:\\GroupDeleteThanhVien.txt", response);
        }
    }
}