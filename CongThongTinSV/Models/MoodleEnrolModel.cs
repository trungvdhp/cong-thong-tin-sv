﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace CongThongTinSV.Models
{
    public class MoodleEnrolStudent
    {
        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("ID moodle")]
        public int ID_moodle { get; set; }

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

        [DisplayName("Ghi danh")]
        public bool Ghi_danh { get; set; }

        [DisplayName("Điểm X")]
        public float DiemX { get; set; }

        [DisplayName("Nhóm")]
        public string Ten_nhom { get; set; }

    }
}