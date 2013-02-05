using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CongThongTinSV.Models
{
    public class MoodleFileParent
    {
        public int contextid;
        public string component;
        public string filearea;
        public int itemid;
        public string filepath;
        public string filename;
    }

    public class MoodleFile
    {
        public int contextid;
        public string component;
        public string filearea;
        public int itemid;
        public string filepath;
        public string filename;
        public int isdir;
        public string url;
        public int timemodified;
    }

    public class MoodleFileResponse
    {
        public List<MoodleFileParent> parents { get; set; } //list of parents
        public List<MoodleFile> files { get; set; } //list of files

        public MoodleFileResponse()
        {
            parents = new List<MoodleFileParent>();
            files = new List<MoodleFile>();
        }
    }
}