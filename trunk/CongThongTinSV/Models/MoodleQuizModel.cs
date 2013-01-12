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
        public string Ma_sv { get; set; }

        [DisplayName("Họ và đệm")]
        public string Ho_dem { get; set; }

        [DisplayName("Tên")]
        public string Ten { get; set; }

        [DisplayName("Ngày sinh")]
        public DateTime? Ngay_sinh { get; set; }

        [DisplayName("Lớp")]
        public string Lop { get; set; }

        [DisplayName("X")]
        public float? DiemX { get; set; }

        [DisplayName("Y cũ")]
        public float? DiemY_cu { get; set; }

        [DisplayName("Y mới")]
        public decimal? DiemY_moi { get; set; }

        [DisplayName("Z mới")]
        public float? DiemZ_moi { get; set; }

        [DisplayName("Điểm chữ")]
        public string Diem_chu { get; set; }

        [DisplayName("Khác điểm")]
        public bool Khac_diem { get; set; }
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