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
    
    public partial class POR_YeuCauMoLop
    {
        public int ID_yeu_cau { get; set; }
        public Nullable<int> ID_sv { get; set; }
        public Nullable<int> ID_mon { get; set; }
        public Nullable<System.DateTime> Ngay_yeu_cau { get; set; }
        public Nullable<bool> Duyet { get; set; }
        public Nullable<System.DateTime> Ngay_duyet { get; set; }
        public Nullable<bool> Trang_thai { get; set; }
        public Nullable<int> Nguoi_duyet { get; set; }
    
        public virtual MARK_MonHoc MARK_MonHoc { get; set; }
        public virtual STU_HoSoSinhVien STU_HoSoSinhVien { get; set; }
    }
}
