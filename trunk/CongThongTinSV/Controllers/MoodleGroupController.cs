using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using CongThongTinSV.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

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

        public ActionResult NhomHocVien()
        {
            return View();
        }

        public ActionResult GetNhom([DataSourceRequest] DataSourceRequest request, string id_lop_tc)
        {
            return Json(MoodleNhoms(id_lop_tc).ToDataSourceResult(request));
        }

        public IEnumerable<MoodleNhom> MoodleNhoms(string id_lop_tc)
        {
            Entities db = new Entities();
            int idlop = 0;

            try
            {
                idlop = Convert.ToInt32(id_lop_tc);
            }
            catch (Exception) { }

            if (idlop <= 0) return new List<MoodleNhom>();

            var nhomhv = from nhom in db.MOD_NhomHocVien
                         where nhom.ID_lop_tc == idlop
                         select new MoodleNhom
                         {
                             ID_nhom = nhom.ID_nhom,
                             ID_lop_tc = idlop,
                             ID_to = (nhom.ID_to == null ? 0 : (int)nhom.ID_to),
                             Ten_to = (nhom.ID_to == null ? "": nhom.MOD_ToNhom.Ten_to),
                             Ten_nhom = nhom.Ten_nhom,
                             Mo_ta = nhom.Mo_ta
                         };
            return nhomhv.ToList();
        }

        public JsonResult GetMoodleNhom(int id_lop_tc)
        {
            Entities db = new Entities();
            JsonResult result = new JsonResult();
            result.Data = new SelectList(db.MOD_NhomHocVien.Where(t => t.ID_lop_tc == id_lop_tc), "ID_nhom", "Ten_nhom");
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }

        public JsonResult GetMoodleTo(int id_lop_tc)
        {
            Entities db = new Entities();
            JsonResult result = new JsonResult();
            result.Data = new SelectList(db.MOD_ToNhom.Where(t => t.ID_lop_tc == id_lop_tc), "ID_to", "Ten_to");
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
                        item.ID_nhom = null;
                        db.Entry(item).State = System.Data.EntityState.Modified;
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
                    entity.ID_nhom = (int?)Convert.ToInt32(id_nhom);
                    db.Entry(entity).State = System.Data.EntityState.Modified;
                    i++;
                }

                db.SaveChanges();
            }

            UtilityController.WriteTextToFile("D:\\NhomHocVienAddThanhVien.txt", response);
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

            UtilityController.WriteTextToFile("D:\\NhomHocVienDeleteThanhVien.txt", response);
        }

        public ActionResult AddThanhVien(string selectedVals, string id_lop_tc, string id_nhom)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleEnrolController.MoodleHocVienDiems(id_lop_tc).Where(t => t.Tinh_trang == "Đã ghi danh" && s.Contains(t.ID.ToString()) && t.Ten_nhom == "").ToList();

            if (list.Count() > 0)
                AddThanhVien(list, id_nhom);

            return View();
        }

        public ActionResult DeleteThanhVien(string selectedVals, string id_lop_tc, string id_nhom)
        {
            Entities db = new Entities();
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleEnrolController.MoodleHocVienDiems(id_lop_tc).Where(t => s.Contains(t.ID.ToString()) && t.ID_nhom.ToString() == id_nhom).ToList();

            if (list.Count() > 0)
                DeleteThanhVien(list, id_nhom);

            return View();
        }

        public ActionResult CreateTo(string ten_to, string mo_ta, int id_lop_tc)
        {
            Entities db = new Entities();
            string postData = "wsfunction=core_group_create_groupings";
            postData += "&groupings[0][courseid]=" + id_lop_tc;
            postData += "&groupings[0][name]=" + HttpUtility.UrlEncode(ten_to);
            postData += "&groupings[0][description]=" + HttpUtility.UrlEncode(mo_ta);
            //postData += "&groups[" + i + "][descriptionformat]=";

            WebRequestController web = new WebRequestController(4, "POST", postData);
            string response = web.GetResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            MoodleException moodleError = new MoodleException();
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

                db.SaveChanges();
            }

            UtilityController.WriteTextToFile("D:\\ToNhomCreate.txt", response);

            return View();
        }

        public ActionResult DeleteTo(string selectedVals)
        {
            string[] list = selectedVals.Split(new char[] { ',' });

            if (list.Count() == 0) return View();

            Entities db = new Entities();
            string postData = "wsfunction=core_group_delete_groupings";
            int len = list.Count();

            for (int i = 0; i < len; i++)
            {
                postData += "&groupingids[" + i + "]=" + list[i];
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

                db.SaveChanges();
            }

            UtilityController.WriteTextToFile("D:\\ToNhomDelete.txt", response);

            return View();
        }

        public ActionResult UpdateTo(int id_to, string ten_to, string mo_ta)
        {
            Entities db = new Entities();
            string postData = "wsfunction=core_group_update_groupings";
            postData += "&groupings[0][id]=" + id_to;
            postData += "&groupings[0][name]=" + HttpUtility.UrlEncode(ten_to);
            postData += "&groupings[0][description]=" + HttpUtility.UrlEncode(mo_ta);
            //postData += "&groups[" + i + "][descriptionformat]=";

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
                //results = serializer.Deserialize<List<MoodleCreateGroupingRespond>>(response);
                MOD_ToNhom entity = db.MOD_ToNhom.Find(id_to);
                entity.Ten_to = ten_to;
                entity.Mo_ta = mo_ta;
                db.Entry(entity).State = System.Data.EntityState.Modified;

                db.SaveChanges();
            }

            UtilityController.WriteTextToFile("D:\\ToNhomUpdate.txt", response);

            return View();
        }

        public ActionResult AssignToNhom(string selectedVals, string id_lop_tc, string id_to)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleNhoms(id_lop_tc).Where(t => t.ID_to == 0 && s.Contains(t.ID_nhom.ToString())).ToList();

            if (list.Count() > 0)
                AssignToNhom(list, id_to);

            return View();
        }

        public static void AssignToNhom(List<MoodleNhom> list, string id_to)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "wsfunction=core_group_assign_grouping";

            foreach (MoodleNhom item in list)
            {
                postData += "&assignments[" + i + "][groupingid]=" + id_to;
                postData += "&assignments[" + i + "][groupid]=" + item.ID_nhom;
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

                foreach (MoodleNhom item in list)
                {
                    MOD_NhomHocVien entity = db.MOD_NhomHocVien.Find(item.ID_nhom);
                    entity.ID_to = (int?)Convert.ToInt32(id_to);
                    db.Entry(entity).State = System.Data.EntityState.Modified;
                    i++;
                }

                db.SaveChanges();
            }

            UtilityController.WriteTextToFile("D:\\ToNhomAssign.txt", response);
        }

       
        public ActionResult UnassignToNhom(string selectedVals, string id_lop_tc, string id_to)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleNhoms(id_lop_tc).Where(t => t.ID_to.ToString() == id_to && s.Contains(t.ID_nhom.ToString())).ToList();

            if (list.Count() > 0)
                UnassignToNhom(list, id_to);

            return View();
        }

        public static void UnassignToNhom(List<MoodleNhom> list, string id_to)
        {
            Entities db = new Entities();
            int i = 0;
            string postData = "wsfunction=core_group_unassign_grouping";

            foreach (MoodleNhom item in list)
            {
                postData += "&unassignments[" + i + "][groupingid]=" + id_to;
                postData += "&unassignments[" + i + "][groupid]=" + item.ID_nhom;
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

                foreach (MoodleNhom item in list)
                {
                    MOD_NhomHocVien entity = db.MOD_NhomHocVien.Find(item.ID_nhom);
                    entity.ID_to = null;
                    db.Entry(entity).State = System.Data.EntityState.Modified;
                    i++;
                }

                db.SaveChanges();
            }

            UtilityController.WriteTextToFile("D:\\ToNhomUnassign.txt", response);
        }
    }
}