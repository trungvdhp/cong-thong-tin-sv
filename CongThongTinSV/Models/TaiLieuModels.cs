using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CongThongTinSV.Models
{
    public class TaiLieuViewModel
    {
        [DisplayName("Mã tài liệu")]
        //[ScaffoldColumn(false)]
        public int ID_tl { get; set; }

        [DisplayName("Tên tài liệu")]
        public string Ten_tl { get; set; }

        [DisplayName("Tác giả")]
        public string Tac_gia { get; set; }

        [DisplayName("Download")]
        public string URL { get; set; }

        [DisplayName("Ngày đăng")]
        public DateTime Ngay_up { get; set; }

        [DisplayName("Tên file")]
        public string Ten_file { get; set; }

        [DisplayName("Người đăng")]
        public string Ten_gv { get; set; }

        [DisplayName("Mô tả")]
        public string Mo_ta { get; set; }

    }
}