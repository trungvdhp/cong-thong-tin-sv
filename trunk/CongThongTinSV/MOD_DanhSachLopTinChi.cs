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
    
    public partial class MOD_DanhSachLopTinChi
    {
        public int ID { get; set; }
        public Nullable<int> ID_nhom { get; set; }
    
        public virtual MOD_NhomHocVien MOD_NhomHocVien { get; set; }
    }
}
