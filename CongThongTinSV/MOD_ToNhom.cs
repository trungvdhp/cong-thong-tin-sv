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
    
    public partial class MOD_ToNhom
    {
        public MOD_ToNhom()
        {
            this.MOD_NhomHocVien = new HashSet<MOD_NhomHocVien>();
        }
    
        public int ID_to { get; set; }
        public string Ten_to { get; set; }
        public string Mo_ta { get; set; }
        public int ID_lop_tc { get; set; }
    
        public virtual MOD_LopTinChi_TC MOD_LopTinChi_TC { get; set; }
        public virtual ICollection<MOD_NhomHocVien> MOD_NhomHocVien { get; set; }
    }
}
