using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CongThongTinSV.Models
{
    public class MoodleCreateCategoryResponse
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class MoodleSemester
    {
        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("ID moodle")]
        public int ID_moodle { get; set; }

        [DisplayName("Đợt")]
        public int Dot { get; set; }

        [DisplayName("Học kỳ")]
        public int Hoc_ky { get; set; }

        [DisplayName("Năm học")]
        public string Nam_hoc { get; set; }
    }
}