﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongThongTinSV.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Web.Script.Serialization;
using CongThongTinSV.App_Lib;
using System.ComponentModel;

namespace CongThongTinSV.Controllers
{
    public class MoodleCategoryController : Controller
    {
        [Description("Quản lý danh mục học kỳ")]
        [Authorize(Roles = "MoodleCategory.ManageSemester")]
        public ActionResult ManageSemester()
        {
            return View();
        }

        [Authorize(Roles = "MoodleCategory.ManageSemester")]
        public ActionResult GetSemesters([DataSourceRequest] DataSourceRequest request)
        {
            return Json(MoodleLib.GetSemesters().ToDataSourceResult(request));
        }

        //[Authorize(Roles = "MoodleCategory.GetSemesterList")]
        public JsonResult GetSemesterList()
        {
            JsonResult result = new JsonResult();
            result.Data = MoodleLib.GetSemesterList();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }

        [Description("Tạo các học kỳ")]
        [Authorize(Roles = "MoodleCategory.CreateSemesters")]
        public ActionResult CreateSemesters(string selectedVals)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetSemesters().Where(t => t.ID_moodle == 0 && s.Contains(t.ID.ToString()));
            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.CreateSemesters(list) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi tạo danh mục học kỳ";
                    data.state = "error";
                }
                else
                {
                    data.title = "Success";
                    data.message = "Tạo danh mục học kỳ thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }

        [Description("Xóa các học kỳ")]
        [Authorize(Roles = "MoodleCategory.DeleteSemesters")]
        public ActionResult DeleteSemesters(string selectedVals)
        {
            IEnumerable<string> s = selectedVals.Split(new char[] { ',' });
            var list = MoodleLib.GetSemesters().Where(t => t.ID_moodle > 0 && s.Contains(t.ID.ToString()));
            var data = new Message();

            if (list.Count() != 0)
            {
                if (MoodleLib.DeleteSemesters(list) == -1)
                {
                    data.title = "Error";
                    data.message = "Lỗi khi xóa danh mục học kỳ";
                    data.state = "error";
                }
                else
                {
                    data.title = "Success";
                    data.message = "Xóa danh mục học kỳ thành công";
                    data.state = "success";
                }
            }

            return Json(data);
        }
    }
}
