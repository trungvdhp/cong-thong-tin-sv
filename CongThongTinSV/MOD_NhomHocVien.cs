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
    
    public partial class MOD_NhomHocVien
    {
        public MOD_NhomHocVien()
        {
            this.MOD_DanhSachLopTinChi = new HashSet<MOD_DanhSachLopTinChi>();
        }
    
        public int ID_nhom { get; set; }
        public string Ten_nhom { get; set; }
        public string Mo_ta { get; set; }
        public Nullable<int> ID_to { get; set; }
        public int ID_lop_tc { get; set; }
    
        public virtual MOD_LopTinChi_TC MOD_LopTinChi_TC { get; set; }
        public virtual MOD_ToNhom MOD_ToNhom { get; set; }
        public virtual ICollection<MOD_DanhSachLopTinChi> MOD_DanhSachLopTinChi { get; set; }
    }
}
