﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongThongTinSV.Models;

namespace CongThongTinSV.Controllers
{
    public class ChuyenNganhController : Controller
    {
        //
        // GET: /ChuyenNganh/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetChuyenNganh()
        {
            Entities db = new Entities();
            JsonResult result = new JsonResult();

            result.Data = new SelectList(db.STU_ChuyenNganh, "ID_chuyen_nganh", "Chuyen_nganh").OrderBy(t => t.Text);
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }
    }
}
