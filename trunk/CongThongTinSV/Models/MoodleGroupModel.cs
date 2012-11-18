using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CongThongTinSV.Models
{
    public class MoodleGroup
    {
        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("ID lớp học phần")]
        public int courseId { get; set; }

        [DisplayName("Tên nhóm")]
        public string name { get; set; }

        [DisplayName("Mô tả")]
        public string description { get; set; }

        [DisplayName("Định dạng mô tả")]
        public int descriptionformat { get; set; }

        [DisplayName("Từ khóa tham gia")]
        public string enrolmentkey { get; set; }
    }
}