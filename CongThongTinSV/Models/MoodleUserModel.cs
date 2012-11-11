using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace CongThongTinSV.Models
{
    public class MoodleCreateUserResponse
    {
        public string id { get; set; }
        public string username { get; set; }
    }

    public class MoodleSinhVien
    {
        [DisplayName("STT")]
        public int STT { get; set; }

        [DisplayName("ID")]
        public int ID_sv { get; set; }

        [DisplayName("ID moodle")]
        public int? ID_moodle { get; set; }

        [DisplayName("Mã SV")]
        public string Ma_sv { get; set; }

        [DisplayName("Họ và đệm")]
        public string Ho_dem { get; set; }

        [DisplayName("Tên")]
        public string Ten { get; set; }

        [DisplayName("Ngày sinh")]
        public DateTime? Ngay_sinh { get; set; }

        [DisplayName("Giới tính")]
        public string Gioi_tinh { get; set; }

        [DisplayName("Lớp")]
        public string Lop { get; set; }

    }
}