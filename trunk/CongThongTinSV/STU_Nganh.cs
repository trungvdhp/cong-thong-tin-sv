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
    
    public partial class STU_Nganh
    {
        public STU_Nganh()
        {
            this.STU_ChuyenNganh = new HashSet<STU_ChuyenNganh>();
        }
    
        public int ID_nganh { get; set; }
        public string Ma_nganh { get; set; }
        public string Ten_nganh { get; set; }
        public string Ten_nganh_En { get; set; }
    
        public virtual ICollection<STU_ChuyenNganh> STU_ChuyenNganh { get; set; }
    }
}