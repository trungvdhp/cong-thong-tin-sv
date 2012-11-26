using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CongThongTinSV.Models
{
    public class MoodleCreateGroupRespond
    {
        public int id { get; set; }
        public int courseId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int descriptionformat { get; set; }
        public string enrolmentkey { get; set; }
    }

    public class MoodleCreateGroupingRespond
    {
        public int id { get; set; }
        public int courseId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int descriptionformat { get; set; }
    }

    public class MoodleNhom
    {
        [DisplayName("ID")]
        public int ID_nhom { get; set; }

        [DisplayName("ID lớp học phần")]
        public int ID_lop_tc { get; set; }

        [DisplayName("Tên nhóm")]
        public string Ten_nhom { get; set; }

        [DisplayName("Mô tả nhóm")]
        public string Mo_ta { get; set; }

        [DisplayName("ID tổ")]
        public int ID_to { get; set; }

        [DisplayName("Tổ")]
        public string Ten_to { get; set; }
    }
}