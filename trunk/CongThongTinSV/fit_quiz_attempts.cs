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
    
    public partial class fit_quiz_attempts
    {
        public long id { get; set; }
        public long uniqueid { get; set; }
        public long quiz { get; set; }
        public long userid { get; set; }
        public int attempt { get; set; }
        public Nullable<decimal> sumgrades { get; set; }
        public long timestart { get; set; }
        public long timefinish { get; set; }
        public long timemodified { get; set; }
        public string layout { get; set; }
        public short preview { get; set; }
        public string state { get; set; }
        public short needsupgradetonewqe { get; set; }
        public long currentpage { get; set; }
        public Nullable<long> timecheckstate { get; set; }
    }
}