using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace CongThongTinSV.Models
{
    public class LopHocPhan
    {
        public int ID_lop_tc { get; set; }
        public int ID_mon { get; set; }
        public String Ten_lop { get; set; }

    }

    public class MoodleException
    {
        public string exception { get; set; }
        public string errorcode { get; set; }
        public string message { get; set; }
        public string debuginfo { get; set; }
    }

    public class SinhVien
    {
        [DisplayName("ID")]
        public int ID_sv { get; set; }

        [DisplayName("Mã SV")]
        public string Ma_sv { get; set; }

        [DisplayName("Họ tên")]
        public string Ho_ten { get; set; }

        [DisplayName("Lớp")]
        public string Lop { get; set; }
    }
}