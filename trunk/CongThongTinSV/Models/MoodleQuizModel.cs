using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CongThongTinSV.Models
{
   
    public class MoodleQuizStudentGrade
    {
        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("ID điểm thi")]
        public int ID_diem_thi { get; set; }

        [DisplayName("ID SV")]
        public int ID_sv { get; set; }

        [DisplayName("Mã SV")]
        public string UserName { get; set; }

        [DisplayName("Họ và đệm")]
        public string LastName { get; set; }

        [DisplayName("Tên")]
        public string FirstName { get; set; }

        [DisplayName("Điểm cũ")]
        public float? OldGrade { get; set; }

        [DisplayName("Điểm mới")]
        public decimal? NewGrade { get; set; }

        [DisplayName("Khác điểm")]
        public bool IsDiffGrade { get; set; }
    }

    public class MoodleStudentQuizGrade
    {
        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("Bài kiểm tra")]
        public string QuizName { get; set; }

        [DisplayName("Điểm")]
        public decimal? Grade { get; set; }

        [DisplayName("Url")]
        public string Url { get; set; }
    }
}