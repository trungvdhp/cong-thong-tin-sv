//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CongThongTinSV
{
    using System;
    using System.Collections.Generic;
    
    public partial class MOD_NguoiDung
    {
        public MOD_NguoiDung()
        {
            this.MOD_NguoiDung_VaiTro_HeThong = new HashSet<MOD_NguoiDung_VaiTro_HeThong>();
            this.MOD_NguoiDung_VaiTro_LopTinChi = new HashSet<MOD_NguoiDung_VaiTro_LopTinChi>();
            this.MOD_DanhSachLopTinChi = new HashSet<MOD_DanhSachLopTinChi>();
        }
    
        public int ID_moodle { get; set; }
        public int ID_nd { get; set; }
        public int ID_nhom_nd { get; set; }
    
        public virtual MOD_NhomNguoiDung MOD_NhomNguoiDung { get; set; }
        public virtual ICollection<MOD_NguoiDung_VaiTro_HeThong> MOD_NguoiDung_VaiTro_HeThong { get; set; }
        public virtual ICollection<MOD_NguoiDung_VaiTro_LopTinChi> MOD_NguoiDung_VaiTro_LopTinChi { get; set; }
        public virtual ICollection<MOD_DanhSachLopTinChi> MOD_DanhSachLopTinChi { get; set; }
    }
}
