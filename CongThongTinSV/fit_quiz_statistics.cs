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
    
    public partial class fit_quiz_statistics
    {
        public long id { get; set; }
        public long quizid { get; set; }
        public long groupid { get; set; }
        public short allattempts { get; set; }
        public long timemodified { get; set; }
        public long firstattemptscount { get; set; }
        public long allattemptscount { get; set; }
        public Nullable<decimal> firstattemptsavg { get; set; }
        public Nullable<decimal> allattemptsavg { get; set; }
        public Nullable<decimal> median { get; set; }
        public Nullable<decimal> standarddeviation { get; set; }
        public Nullable<decimal> skewness { get; set; }
        public Nullable<decimal> kurtosis { get; set; }
        public Nullable<decimal> cic { get; set; }
        public Nullable<decimal> errorratio { get; set; }
        public Nullable<decimal> standarderror { get; set; }
    }
}
