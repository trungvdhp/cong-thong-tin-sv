using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CongThongTinSV.Models
{
    public class MoodleWebService
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [DisplayName("ID dịch vụ")]
        public int ID_dv { get; set; }

        [Required(ErrorMessage="{0} không được bỏ trống.")]
        [StringLength(50, ErrorMessage="{0} phải dài tối đa là {1} ký tự và ít nhất là {2} ký tự", MinimumLength = 1)]
        [DisplayName("Tên dịch vụ")]
        public string Ten_dv { get; set; }

        [Required(ErrorMessage = "{0} không được bỏ trống.")]
        [StringLength(50, ErrorMessage = "{0} phải dài tối đa là {1} ký tự và ít nhất là {2} ký tự", MinimumLength = 1)]
        [DisplayName("Tên rút gọn")]
        public string Ten_rut_gon { get; set; }
    }
}