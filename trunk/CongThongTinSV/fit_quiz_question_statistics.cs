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
    
    public partial class fit_quiz_question_statistics
    {
        public long id { get; set; }
        public long quizstatisticsid { get; set; }
        public long questionid { get; set; }
        public Nullable<long> slot { get; set; }
        public short subquestion { get; set; }
        public long s { get; set; }
        public Nullable<decimal> effectiveweight { get; set; }
        public sbyte negcovar { get; set; }
        public Nullable<decimal> discriminationindex { get; set; }
        public Nullable<decimal> discriminativeefficiency { get; set; }
        public Nullable<decimal> sd { get; set; }
        public Nullable<decimal> facility { get; set; }
        public string subquestions { get; set; }
        public Nullable<decimal> maxmark { get; set; }
        public string positions { get; set; }
        public Nullable<decimal> randomguessscore { get; set; }
    }
}
