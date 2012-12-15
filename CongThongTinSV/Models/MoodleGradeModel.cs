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

        [DisplayName("ID SV")]
        public int ID_sv { get; set; }

        [DisplayName("Mã SV")]
        public string UserName { get; set; }

        [DisplayName("Họ và đệm")]
        public string LastName { get; set; }

        [DisplayName("Tên")]
        public string FirstName { get; set; }

        [DisplayName("Điểm thi cũ")]
        public float? OldGrade { get; set; }

        [DisplayName("Điểm thi mới")]
        public decimal? NewGrade { get; set; }

        [DisplayName("Điểm Z")]
        public decimal? ZGrade { get; set; }
    }
}