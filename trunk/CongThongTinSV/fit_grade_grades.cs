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
    
    public partial class fit_grade_grades
    {
        public long id { get; set; }
        public long itemid { get; set; }
        public long userid { get; set; }
        public Nullable<decimal> rawgrade { get; set; }
        public decimal rawgrademax { get; set; }
        public decimal rawgrademin { get; set; }
        public Nullable<long> rawscaleid { get; set; }
        public Nullable<long> usermodified { get; set; }
        public Nullable<decimal> finalgrade { get; set; }
        public long hidden { get; set; }
        public long locked { get; set; }
        public long locktime { get; set; }
        public long exported { get; set; }
        public long overridden { get; set; }
        public long excluded { get; set; }
        public string feedback { get; set; }
        public long feedbackformat { get; set; }
        public string information { get; set; }
        public long informationformat { get; set; }
        public Nullable<long> timecreated { get; set; }
        public Nullable<long> timemodified { get; set; }
    }
}
