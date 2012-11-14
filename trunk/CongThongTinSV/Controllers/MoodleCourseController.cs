using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongThongTinSV.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

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

        public ActionResult GetLopTinChi([DataSourceRequest] DataSourceRequest request, int id_chuyen_nganh)
        {

            return Json(MoodleLopTinChis(id_chuyen_nganh).ToDataSourceResult(request));
        }

        public IEnumerable<MoodleLopTinChi> MoodleLopTinChis(int id_chuyen_nganh)
        {
            Entities db = new Entities();
            
            var q = (from cn in db.MOD_HocKy_ChuyenNganh
                    join hk in db.MOD_HocKy
                    on cn.Ky_dang_ky equals hk.ID_moodle
                    where cn.ID_moodle == id_chuyen_nganh
                    select new
                    {
                        cn.ID_chuyen_nganh,
                        hk.Ky_dang_ky
                    }).FirstOrDefault();

            var q1 = from dt in db.PLAN_ChuongTrinhDaoTao
                      join ct in db.PLAN_ChuongTrinhDaoTaoChiTiet
                      on dt.ID_dt equals ct.ID_dt
                      where dt.ID_chuyen_nganh == q.ID_chuyen_nganh
                      group ct by new { ct.ID_mon }
                          into mon
                          select mon.FirstOrDefault();

            var q2 = (from ltc in db.PLAN_LopTinChi_TC
                     join mtc in db.PLAN_MonTinChi_TC
                     on ltc.ID_mon_tc equals mtc.ID_mon_tc
                     join mon in db.MARK_MonHoc
                     on mtc.ID_mon equals mon.ID_mon
                     join mh in q1
                     on mon.ID_mon equals mh.ID_mon
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
                         Ky_hieu = a.Ky_hieu_lop_tc,
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
    }
}
