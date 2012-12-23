using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CongThongTinSV.Models
{
    public class LopHocPhan
    {
        public int ID_lop_tc { get; set; }
        public int ID_mon { get; set; }
        public String Ten_lop { get; set; }

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

    public class DiemHocTap
    {
        public int Id_diem;

        [DisplayName("Mã MH")]
        public string Ma_mon { get; set; }

        [DisplayName("Tên môn")]
        public string Ten_mon { get; set; }

        [DisplayName("Điểm X")]
        public float X { get; set; }

        [DisplayName("Điểm Y")]
        public float Y { get; set; }

        [DisplayName("Điểm Z")]
        public float Z { get; set; }

        [DisplayName("Điểm chữ")]
        public string Diem_chu { get; set; }

        [DisplayName("Học kỳ")]
        public int Hoc_ky { get; set; }

        [DisplayName("Năm học")]
        public string Nam_hoc { get; set; }
    }

    public class ActionInfo
    {
        [DisplayName("Controller")]
        public Type Controller { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Action name")]
        public string ActionName { get; set; }
    }

    public class Capability
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Required]
        [DisplayName("ID quyền")]
        public int ID_quyen { get; set; }

        [Required]
        [DisplayName("Tên quyền")]
        public string Ten_quyen { get; set; }

        [Required]
        [DisplayName("Action name")]
        public string Action_name { get; set; }

        [DisplayName("ID nhóm")]
        public int ID_nhom { get; set; }

        [DisplayName("Tình trạng")]
        public string Tinh_trang { get; set; }
    }
}