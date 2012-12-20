using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CongThongTinSV.Models
{
    public class MoodleException
    {
        public string exception { get; set; }
        public string errorcode { get; set; }
        public string message { get; set; }
        public string debuginfo { get; set; }
    }
}