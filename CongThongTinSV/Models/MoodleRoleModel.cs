using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CongThongTinSV.Models
{
    public class MoodleRole
    {
        public int roleid { get; set; } //role id
        public string name { get; set; } //role name
        public string shortname { get; set; } //role shortname
        public int sortorder { get; set; } //role sortorder
    }
}