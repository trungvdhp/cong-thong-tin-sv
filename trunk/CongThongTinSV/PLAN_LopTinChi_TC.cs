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
    
    public partial class PLAN_LopTinChi_TC
    {
        public PLAN_LopTinChi_TC()
        {
            this.PLAN_SukiensTinChi_TC = new HashSet<PLAN_SukiensTinChi_TC>();
        }
    
        public int ID_lop_tc { get; set; }
        public int ID_lop_lt { get; set; }
        public int ID_mon_tc { get; set; }
        public int STT_lop { get; set; }
        public int So_sv_min { get; set; }
        public int So_sv_max { get; set; }
        public System.DateTime Tu_ngay { get; set; }
        public System.DateTime Den_ngay { get; set; }
        public int Ca_hoc { get; set; }
        public int So_tiet_tuan { get; set; }
        public int ID_phong { get; set; }
        public int ID_cb { get; set; }
        public bool Huy_lop { get; set; }
        public string Ly_do { get; set; }
        public string Nhom_dang_ky { get; set; }
        public Nullable<System.DateTime> Ngay_thi { get; set; }
        public Nullable<int> Cho_trong { get; set; }
    
        public virtual PLAN_MonTinChi_TC PLAN_MonTinChi_TC { get; set; }
        public virtual ICollection<PLAN_SukiensTinChi_TC> PLAN_SukiensTinChi_TC { get; set; }
    }
}