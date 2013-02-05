using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongThongTinSV.App_Lib;
using CongThongTinSV.Models;

namespace CongThongTinSV.Controllers
{
    public class MoodleFileController : Controller
    {
        [Description("Truy cập file")]
        [Authorize(Roles = "MoodleFile.GetFile")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetFile(string contextid, string component, string filearea, string itemid, string filepath, string filename)
        {
            var fileInfo = MoodleLib.GetFileInfo(contextid, component, filearea, itemid, filepath, filename);

            return File(fileInfo.Key, fileInfo.Value);
        }

    }
}
