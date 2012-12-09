using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CongThongTinSV.Models
{
    public class MoodleGradeBook
    {
        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("Mã SV")]
        public string Username { get; set; }

        [DisplayName("Họ và đệm")]
        public string Lastname { get; set; }

        [DisplayName("Tên")]
        public string Firstname { get; set; }

        [DisplayName("Điểm X")]
        public decimal? GradeX { get; set; }

        [DisplayName("Điểm Z")]
        public decimal? GradeZ { get; set; }

        [DisplayName("Điểm")]
        public decimal? Grade { get; set; }
    }
}