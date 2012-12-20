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
        [Required]
        [DisplayName("ID dịch vụ")]
        public int ID_dv { get; set; }

        [Required]
        [DisplayName("Tên dịch vụ")]
        public string Ten_dv { get; set; }

        [Required]
        [DisplayName("Tên rút gọn")]
        public string Ten_rut_gon { get; set; }
    }
}