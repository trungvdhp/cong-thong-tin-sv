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
    
    public partial class MARK_MonHoc
    {
        public MARK_MonHoc()
        {
            this.MARK_Diem_TC = new HashSet<MARK_Diem_TC>();
            this.PLAN_ChuongTrinhDaoTaoChiTiet = new HashSet<PLAN_ChuongTrinhDaoTaoChiTiet>();
            this.PLAN_MonTinChi_TC = new HashSet<PLAN_MonTinChi_TC>();
        }
    
        public int ID_mon { get; set; }
        public string Ky_hieu { get; set; }
        public string Ten_mon { get; set; }
        public string Ten_tieng_anh { get; set; }
        public int ID_bm { get; set; }
        public Nullable<int> ID_he_dt { get; set; }
        public Nullable<int> ID_nhom_hp { get; set; }
    
        public virtual ICollection<MARK_Diem_TC> MARK_Diem_TC { get; set; }
        public virtual ICollection<PLAN_ChuongTrinhDaoTaoChiTiet> PLAN_ChuongTrinhDaoTaoChiTiet { get; set; }
        public virtual ICollection<PLAN_MonTinChi_TC> PLAN_MonTinChi_TC { get; set; }
    }
}
