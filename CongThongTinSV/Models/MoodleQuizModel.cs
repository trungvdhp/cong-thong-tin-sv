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

    public class MoodleQuizQuestion
    {
        [DisplayName("ID")]
        public long ID { get; set; }

        [DisplayName("ID câu hỏi")]
        public long ID_cau_hoi { get; set; }

        [DisplayName("STT")]
        public long STT { get; set; }

        [DisplayName("Điểm")]
        public string Diem { get; set; }

        [DisplayName("Câu hỏi")]
        public string Cau_hoi { get; set; }

        [DisplayName("Nội dung")]
        public string Noi_dung { get; set; }
    }

    public class MoodleQuizAttempt
    {
        [DisplayName("ID học viên")]
        public long ID { get; set; }

        [DisplayName("Họ và đệm")]
        public string Ho_dem { get; set; }

        [DisplayName("Tên")]
        public string Ten { get; set; }

        [DisplayName("Bắt đầu vào lúc")]
        public DateTime Bat_dau { get; set; }

        [DisplayName("Kết thúc lúc")]
        public DateTime Ket_thuc { get; set; }

        [DisplayName("Thời gian thực hiện")]
        public string Thoi_gian_lam { get; set; }

        [DisplayName("Quá hạn")]
        public string Qua_han { get; set; }

        [DisplayName("Tổng điểm")]
        public string Tong_diem { get; set; }

        [DisplayName("Điểm hệ 10")]
        public string Diem_he_10 { get; set; }

        [DisplayName("Điểm hệ 4")]
        public string Diem_he_4 { get; set; }
    }
}