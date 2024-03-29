﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CongThongTinSV.Models
{
    public class MoodleCreateCourseResponse
    {
        public string id { get; set; }
        public string shortname { get; set; }
    }

    public class MoodleEnrolledCourse
    {
        public int id { get; set; }
        public string fullname { get; set; }
        public string shortname { get; set; }
    }

    public class MoodleUserCourse
    {
        public int id { get; set; } //id of course
        public string shortname { get; set; } //short name of course
        public string fullname { get; set; } //long name of course
        public int enrolledusercount { get; set; } //Number of enrolled users in this course
        public string idnumber { get; set; } //id number of course
        public int visible { get; set; } //1 means visible, 0 means hidden course
    }

    public class MoodleModuleContent
    {
        public string type { get; set; } //a file or a folder or external link
        public string filename { get; set; } //filename
        public string filepath { get; set; } //filepath
        public int filesize { get; set; } //filesize
        public string fileurl { get; set; } //downloadable file url
        public string content { get; set; } //Raw content, will be used when type is content
        public int timecreated { get; set; } //Time created
        public int timemodified { get; set; } //Time modified
        public int sortorder { get; set; } //Content sort order
        public int userid { get; set; } //User who added this content to moodle
        public string author { get; set; } //Content owner
        public string license { get; set; } //Content license
    }

    public class MoodleCourseModule
    {
        public int id { get; set; } //activity id
        public string url { get; set; } //activity url
        public string name { get; set; } //activity module name
        public string description { get; set; } //activity description
        public int visible { get; set; } //is the module visible
        public string modicon { get; set; } //activity icon url
        public string modname { get; set; } //activity module type
        public string modplural { get; set; } //activity module plural name
        public int availablefrom { get; set; } //module availability start date
        public int availableuntil { get; set; } //module availability en date
        public int indent { get; set; } //number of identation in the site
        public List<MoodleModuleContent> contents { get; set; }
    }

    public class MoodleCourseContentResponse
    {
        public int id { get; set; } //Section ID
        public string name { get; set; } //Section name
        public int visible { get; set; } //is the section visible
        public string summary { get; set; } //Section description
        public int summaryformat { get; set; } //summary format (1 = HTML, 0 = MOODLE, 2 = PLAIN or 4 = MARKDOWN)
        public List<MoodleCourseModule> modules { get; set; } //list of module+
    }

    public class MoodleCourse
    {
        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("ID moodle")]
        public int ID_moodle { get; set; }

        [DisplayName("ID học kỳ")]
        public int ID_hocky { get; set; }

        [DisplayName("Lớp học phần")]
        public string Lop_hoc_phan { get; set; }

        [DisplayName("Học phần")]
        public string Ky_hieu { get; set; }

        [DisplayName("ID number")]
        public string ID_number { get; set; }

        [DisplayName("Số tín chỉ")]
        public int So_tin_chi { get; set; }

        [DisplayName("Từ ngày")]
        public DateTime Tu_ngay { get; set; }

        [DisplayName("Đến ngày")]
        public DateTime Den_ngay { get; set; }

        [DisplayName("Trạng thái")]
        public bool Trang_thai { get; set; }
    }

    public class CourseInfo
    {
        public MARK_MonHoc Mon_hoc { get; set; }
        public int Hoc_ky { get; set; }
        public string Nam_hoc { get; set; }
        public string Nhom { get; set; }
        public int So_tin_chi { get; set; }
        public int ID_lop_tc { get; set; }
    }

    public class MoodleCourseStudentGrade
    {
        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("ID điểm thi")]
        public int ID_diem_thi { get; set; }

        [DisplayName("ID SV")]
        public int ID_sv { get; set; }

        [DisplayName("Mã SV")]
        public string Ma_sv { get; set; }

        [DisplayName("Họ và đệm")]
        public string Ho_dem { get; set; }

        [DisplayName("Tên")]
        public string Ten { get; set; }

        [DisplayName("Ngày sinh")]
        public DateTime? Ngay_sinh { get; set; }

        [DisplayName("Lớp")]
        public string Lop { get; set; }

        [DisplayName("Điểm tổng kết")]
        public decimal? DiemZ { get; set; }
    }

    public class MoodleStudentCourseGrade
    {
        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("Lớp học phần")]
        public string Lop_hoc_phan { get; set; }

        [DisplayName("Học phần")]
        public string Ky_hieu { get; set; }

        [DisplayName("Điểm tổng kết")]
        public decimal? Diem { get; set; }

        [DisplayName("Năm học")]
        public string Nam_hoc { get; set; }

        [DisplayName("Học kỳ")]
        public int Hoc_ky { get; set; }

        [DisplayName("Số tín chỉ")]
        public int So_tin_chi { get; set; }
    }

}