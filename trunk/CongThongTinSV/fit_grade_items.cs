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
    
    public partial class fit_grade_items
    {
        public long id { get; set; }
        public Nullable<long> courseid { get; set; }
        public Nullable<long> categoryid { get; set; }
        public string itemname { get; set; }
        public string itemtype { get; set; }
        public string itemmodule { get; set; }
        public Nullable<long> iteminstance { get; set; }
        public Nullable<long> itemnumber { get; set; }
        public string iteminfo { get; set; }
        public string idnumber { get; set; }
        public string calculation { get; set; }
        public short gradetype { get; set; }
        public decimal grademax { get; set; }
        public decimal grademin { get; set; }
        public Nullable<long> scaleid { get; set; }
        public Nullable<long> outcomeid { get; set; }
        public decimal gradepass { get; set; }
        public decimal multfactor { get; set; }
        public decimal plusfactor { get; set; }
        public decimal aggregationcoef { get; set; }
        public long sortorder { get; set; }
        public long display { get; set; }
        public Nullable<bool> decimals { get; set; }
        public long hidden { get; set; }
        public long locked { get; set; }
        public long locktime { get; set; }
        public long needsupdate { get; set; }
        public Nullable<long> timecreated { get; set; }
        public Nullable<long> timemodified { get; set; }
    }
}
