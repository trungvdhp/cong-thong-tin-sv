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
    
    public partial class tbl_inbox
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public Nullable<System.DateTime> PostDate { get; set; }
        public string Ma_sv { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> ID_nguoi_tra_loi { get; set; }
        public string Id_nguoi_gui { get; set; }
        public string Id_nguoi_nhan { get; set; }
        public Nullable<byte> Doi_tuong_nhan { get; set; }
        public Nullable<bool> Da_doc { get; set; }
    }
}
