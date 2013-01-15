using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using CongThongTinSV.App_Lib;
using CongThongTinSV.Models;
using Microsoft.Reporting.WebForms;
using Kendo.Mvc.Infrastructure;

namespace CongThongTinSV.Reports
{
    [Serializable()]
    public partial class KetQuaDanhGiaHocPhan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string quizid = Request.QueryString["quizid"];
                string sortStr = Request.QueryString["sort"].Trim();
                IEnumerable<string> sorts = Request.QueryString["sort"].Trim().Split(new char[]{'~'});
                string filters = Request.QueryString["filter"].Trim();
                DataSourceRequest request = new DataSourceRequest();
                IList<SortDescriptor> lstSort = new List<SortDescriptor>();
                IList<FilterDescriptor> lstFilter = new List<FilterDescriptor>();

                try
                {
                    if (sortStr != "")
                    {
                        foreach (string item in sorts)
                        {
                            SortDescriptor sort = new SortDescriptor();
                            sort.Deserialize(item);
                            lstSort.Add(sort);
                        }

                        if (lstSort.Count > 0)
                        {
                            request.Sorts = lstSort;
                        }
                    }

                    if (filters != "")
                    {
                        request.Filters = FilterDescriptorFactory.Create(filters);
                    }
                }
                catch (Exception)
                {
                }

                IEnumerable<MoodleQuizStudentGrade> data = MoodleLib.GetQuizStudentGrades(quizid).ToDataSourceResult(request).Data.Cast<MoodleQuizStudentGrade>();
                int tongSoSV = data.Count();
                int soSVDuThi = data.Count(t => t.DiemY_moi != null);
                int soSVVangMat = tongSoSV - soSVDuThi;

                float tyLeDiemGioi = data.Count(t => t.DiemY_moi >= 8.45M) * 100.0f / soSVDuThi;
                float tyLeDiemKha = data.Count(t => t.DiemY_moi >= 6.95M && t.DiemY_moi < 8.45M) * 100.0f / soSVDuThi;
                float tyLeDiemTB = data.Count(t => t.DiemY_moi >= 5.45M && t.DiemY_moi < 6.95M) * 100.0f / soSVDuThi;
                float tyLeDiemYeuKem = data.Count(t =>t.DiemY_moi < 5.45M) * 100.0f / soSVDuThi;

                var quiz = MoodleLib.GetQuizByID(quizid);
                var course = MoodleLib.GetCourseByQuiz(quiz);
                string courseid = course == null ? "0" : course.id.ToString();
                IEnumerable<MoodleUser> users = MoodleLib.GetNoneStudents(courseid);
                var truongBM = users.SingleOrDefault(t => t.ID_vai_tro == "9");
                string tenTruongBM = truongBM == null ? "" : truongBM.LastName + " " + truongBM.FirstName;
                string tenGV1 = "", tenGV2 = "";
                var giaoViens = users.Where(t => t.ID_vai_tro == "3").ToList();
                var giaoVienTroGiangs = users.Where(t => t.ID_vai_tro == "4").ToList();
                var giaoVienBienSoans = users.Where(t => t.ID_vai_tro == "2").ToList();
                int n = giaoViens.Count, m = giaoVienTroGiangs.Count, k = giaoVienBienSoans.Count;

                if (n > 0)
                {
                    tenGV1 = giaoViens[0].LastName + " " + giaoViens[0].FirstName;

                    if (n > 1)
                    {
                        tenGV2 = giaoViens[1].LastName + " " + giaoViens[1].FirstName;
                    }
                    else if (m > 0)
                    {
                        tenGV2 = giaoVienTroGiangs[0].LastName + " " + giaoVienTroGiangs[0].FirstName;
                    }
                    else if (k > 0)
                    {
                        tenGV2 = giaoVienBienSoans[0].LastName + " " + giaoVienBienSoans[0].FirstName;
                    }
                }
                else if (m > 0)
                {
                    tenGV1 = tenGV2 = giaoVienTroGiangs[0].LastName + " " + giaoVienTroGiangs[0].FirstName;
                    if (m > 1)
                    {
                        tenGV2 = giaoVienTroGiangs[1].LastName + " " + giaoVienTroGiangs[1].FirstName;
                    }
                    else if (k > 0)
                    {
                        tenGV2 = giaoVienBienSoans[0].LastName + " " + giaoVienBienSoans[0].FirstName;
                    }
                }
                else
                {
                    if (k > 0)
                    {
                        tenGV1 = giaoVienBienSoans[0].LastName + " " + giaoVienBienSoans[0].FirstName;

                        if (n > 1)
                        {
                            tenGV2 = giaoVienBienSoans[1].LastName + " " + giaoVienBienSoans[1].FirstName;
                        }
                    }
                }

                var courseinfo = MoodleLib.GetCourseInfo(courseid);
                List<ReportParameter> param = new List<ReportParameter>();
                string hocPhan = courseinfo.Mon_hoc.Ten_mon + " (" + courseinfo.Mon_hoc.Ky_hieu + ") - Nhóm " + courseinfo.Nhom;

                param.Add(new ReportParameter("TongSoSV", tongSoSV.ToString()));
                param.Add(new ReportParameter("HocKy", courseinfo.Hoc_ky.ToString()));
                param.Add(new ReportParameter("NamHoc", courseinfo.Nam_hoc));
                param.Add(new ReportParameter("HocPhan", hocPhan));
                param.Add(new ReportParameter("SoTinChi", courseinfo.So_tin_chi.ToString()));
                param.Add(new ReportParameter("SoSVDuThi", soSVDuThi.ToString()));
                param.Add(new ReportParameter("SoSVVangMat", soSVVangMat.ToString()));
                param.Add(new ReportParameter("TyLeDiemGioi", tyLeDiemGioi.ToString()));
                param.Add(new ReportParameter("TyLeDiemKha", tyLeDiemKha.ToString()));
                param.Add(new ReportParameter("TyLeDiemTB", tyLeDiemTB.ToString()));
                param.Add(new ReportParameter("TyLeDiemYeuKem", tyLeDiemYeuKem.ToString()));
                param.Add(new ReportParameter("TruongBoMon", tenTruongBM));
                param.Add(new ReportParameter("CanBoCoiThi1", tenGV1));
                param.Add(new ReportParameter("CanBoCoiThi2", tenGV2));

                rpvKetQuaDanhGiaHocPhan.LocalReport.SetParameters(param);
                ReportDataSource rds = new ReportDataSource
                ("dsKetQuaDanhGiaHocPhan", data);
                rpvKetQuaDanhGiaHocPhan.LocalReport.DataSources.Clear();
                rpvKetQuaDanhGiaHocPhan.LocalReport.DataSources.Add(rds);
                rpvKetQuaDanhGiaHocPhan.LocalReport.DisplayName = Utility.RemoveSign4VietnameseString(hocPhan);
                rpvKetQuaDanhGiaHocPhan.LocalReport.Refresh();
            }
        }
    }
}