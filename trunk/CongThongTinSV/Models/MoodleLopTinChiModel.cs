using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CongThongTinSV.Models
{
    public class MoodleCreateCourseResponse
    {
        public string id { get; set; }
        public string shortname { get; set; }
    }

    public class MoodleLopTinChi
    {
        [DisplayName("ID")]
        public int ID_lop_tc { get; set; }

        [DisplayName("ID moodle")]
        public int ID_moodle { get; set; }

        [DisplayName("Lớp học phần")]
        public string Lop_hoc_phan { get; set; }

        [DisplayName("Ký hiệu")]
        public string Ky_hieu { get; set; }

        [DisplayName("Từ ngày")]
        public DateTime Tu_ngay { get; set; }

        [DisplayName("Đến ngày")]
        public DateTime Den_ngay { get; set; }
    }
}