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
    
    public partial class PLAN_ChuongTrinhDaoTao
    {
        public PLAN_ChuongTrinhDaoTao()
        {
            this.MARK_Diem_TC = new HashSet<MARK_Diem_TC>();
        }
    
        public int ID_dt { get; set; }
        public int ID_he { get; set; }
        public int ID_khoa { get; set; }
        public int Khoa_hoc { get; set; }
        public int ID_chuyen_nganh { get; set; }
        public float So_hoc_trinh { get; set; }
        public int So_ky_hoc { get; set; }
        public Nullable<int> So { get; set; }
    
        public virtual ICollection<MARK_Diem_TC> MARK_Diem_TC { get; set; }
        public virtual STU_He STU_He { get; set; }
        public virtual STU_Khoa STU_Khoa { get; set; }
    }
}
