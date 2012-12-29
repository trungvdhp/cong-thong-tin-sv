using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace CongThongTinSV.Models
{
    public class MoodleCreateUserResponse
    {
        public string id { get; set; }
        public string username { get; set; }
    }

    public class MoodleUserCustomfield
    {
        public string type { get; set; } //The type of the custom field - text field, checkbox...
        public string value { get; set; } //The value of the custom field
        public string name { get; set; } //The name of the custom field
        public string shortname { get; set; } //The shortname of the custom field - to be able to build the field class in the code
    }

    public class MoodleUserPreference
    {
        public string name { get; set; } //The name of the preferences
        public string value { get; set; } //The value of the custom field
    }

    public class MoodleUserResponse
    {
        public double id { get; set; } //ID of the user
        public string username { get; set; } //Username policy is defined in Moodle security config
        public string firstname { get; set; } //The first name(s) of the user
        public string lastname { get; set; } //The family name of the user
        public string fullname { get; set; } //The fullname of the user
        public string email { get; set; } //An email address - allow email as root@localhost
        public string address { get; set; } //Postal address
        public string phone1 { get; set; } //Phone 1
        public string phone2 { get; set; } //Phone 2
        public string icq { get; set; } //icq number
        public string skype { get; set; } //skype id
        public string yahoo { get; set; } //yahoo id
        public string aim { get; set; } //aim id
        public string msn { get; set; } //msn number
        public string department { get; set; } //department
        public string institution { get; set; } //institution
        public string interests { get; set; } //user interests (separated by commas)
        public int firstaccess { get; set; } //first access to the site (0 if never)
        public int lastaccess { get; set; } //last access to the site (0 if never)
        public string auth { get; set; } //Auth plugins include manual, ldap, imap, etc
        public double confirmed { get; set; } //Active user: 1 if confirmed, 0 otherwise
        public string idnumber { get; set; } //An arbitrary ID code number perhaps from the institution
        public string lang { get; set; } //Language code such as "en", must exist on server
        public string theme { get; set; } //Theme name such as "standard", must exist on server
        public string timezone { get; set; } //Timezone code such as Australia/Perth, or 99 for default
        public int mailformat { get; set; } //Mail format code is 0 for plain text, 1 for HTML etc
        public string description { get; set; } //User profile description
        public int descriptionformat { get; set; } //description format (1 = HTML, 0 = MOODLE, 2 = PLAIN or 4 = MARKDOWN)
        public string city { get; set; } //Home city of the user
        public string url { get; set; } //URL of the user
        public string country { get; set; } //Home country code of the user, such as AU or CZ
        public string profileimageurlsmall { get; set; } //User image profile URL - small version
        public string profileimageurl { get; set; } //User image profile URL - big version
        public List<MoodleUserCustomfield> customfields { get; set; } //User custom fields (also known as user profil fields)
        public List<MoodleUserPreference> preferences { get; set; } //User preferences
        public List<MoodleEnrolledCourse> enrolledcourses { get; set; } //Courses where the user is enrolled - limited by which courses the user is able to see
        
        public MoodleUserResponse()
        {
            customfields = new List<MoodleUserCustomfield>();
            preferences = new List<MoodleUserPreference>();
            enrolledcourses = new List<MoodleEnrolledCourse>();
        }
    }

    public class MoodleCourseUserResponse
    {
        public double id { get; set; } //ID of the user
        public string username { get; set; } //Username policy is defined in Moodle security config
        public string firstname { get; set; } //The first name(s) of the user
        public string lastname { get; set; } //The family name of the user
        public string fullname { get; set; } //The fullname of the user
        public string email { get; set; } //An email address - allow email as root@localhost
        public string address { get; set; } //Postal address
        public string phone1 { get; set; } //Phone 1
        public string phone2 { get; set; } //Phone 2
        public string icq { get; set; } //icq number
        public string skype { get; set; } //skype id
        public string yahoo { get; set; } //yahoo id
        public string aim { get; set; } //aim id
        public string msn { get; set; } //msn number
        public string department { get; set; } //department
        public string institution { get; set; } //institution
        public string idnumber { get; set; } //An arbitrary ID code number perhaps from the institution
        public string interests { get; set; } //user interests (separated by commas)
        public int firstaccess { get; set; } //first access to the site (0 if never)
        public int lastaccess { get; set; } //last access to the site (0 if never)
        public string description { get; set; } //User profile description
        public int descriptionformat { get; set; } //description format (1 = HTML, 0 = MOODLE, 2 = PLAIN or 4 = MARKDOWN)
        public string city { get; set; } //Home city of the user
        public string url { get; set; } //URL of the user
        public string country { get; set; } //Home country code of the user, such as AU or CZ
        public string profileimageurlsmall { get; set; } //User image profile URL - small version
        public string profileimageurl { get; set; } //User image profile URL - big version
        public List<MoodleUserCustomfield> customfields { get; set; } //User custom fields (also known as user profil fields)
        public List<MoodleGroupRespond> groups { get; set; } //user groups
        public List<MoodleRole> roles { get; set; } //user roles
        public List<MoodleUserPreference> preferences { get; set; } //User preferences
        public List<MoodleEnrolledCourse> enrolledcourses { get; set; } //Courses where the user is enrolled - limited by which courses the user is able to see

        public MoodleCourseUserResponse()
        {
            customfields = new List<MoodleUserCustomfield>();
            groups = new List<MoodleGroupRespond>();
            roles = new List<MoodleRole>();
            preferences = new List<MoodleUserPreference>();
            enrolledcourses = new List<MoodleEnrolledCourse>();
        }
    }

    public class MoodleUser
    {
        [DisplayName("ID lớp học phần")]
        public int ID_lop_tc { get; set; }

        [DisplayName("ID người dùng")]
        public int ID { get; set; }

        [DisplayName("ID moodle")]
        public int ID_moodle { get; set; }

        [DisplayName("Tên đăng nhập")]
        public string UserName { get; set; }

        [DisplayName("Mật khẩu")]
        public string Password { get; set; }

        [DisplayName("Họ và đệm")]
        public string LastName{ get; set; }

        [DisplayName("Tên")]
        public string FirstName { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("ID vai trò")]
        public string ID_vai_tro { get; set; }

        [DisplayName("Vai trò")]
        public string Vai_tro { get; set; }

        [DisplayName("Trạng thái")]
        public bool Trang_thai { get; set; }
    }

    public class MoodleCourseMember
    {
        [DisplayName("UserID")]
        public int UserID { get; set; }

        [DisplayName("Tên đăng nhập")]
        public string UserName { get; set; }

        [DisplayName("Họ và đệm")]
        public string LastName { get; set; }

        [DisplayName("Tên")]
        public string FirstName { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("ID vai trò")]
        public string RoleIDs { get; set; }

        [DisplayName("Vai trò")]
        public string Roles { get; set; }

        [DisplayName("Ảnh đại diện")]
        public string ImageUrl { get; set; }

        [DisplayName("Truy cập gần nhất")]
        public string LastAccess { get; set; }

        [DisplayName("Tỉnh/Thành phố")]
        public string City { get; set; }

        [DisplayName("Quốc gia")]
        public string Country { get; set; }
    }

    public class MoodleStudent
    {
        [DisplayName("ID đăng ký")]
        public int ID { get; set; }

        [DisplayName("ID lớp học phần")]
        public int ID_lop_tc { get; set; }

        [DisplayName("ID sinh viên")]
        public int ID_sv { get; set; }

        [DisplayName("ID moodle")]
        public int ID_moodle { get; set; }

        [DisplayName("Mã SV")]
        public string Ma_sv { get; set; }

        [DisplayName("Họ và đệm")]
        public string Ho_dem { get; set; }

        [DisplayName("Tên")]
        public string Ten { get; set; }

        [DisplayName("Ngày sinh")]
        public DateTime? Ngay_sinh { get; set; }

        [DisplayName("Giới tính")]
        public string Gioi_tinh { get; set; }

        [DisplayName("Lớp")]
        public string Lop { get; set; }

        [DisplayName("Trạng thái")]
        public bool Trang_thai { get; set; }

        [DisplayName("Điểm X")]
        public float DiemX { get; set; }

        [DisplayName("ID Nhóm")]
        public int? ID_nhom { get; set; }

        [DisplayName("Nhóm")]
        public string Ten_nhom { get; set; }

        [DisplayName("Mật khẩu")]
        public string Mat_khau { get; set; }
    }

    public class MoodleTeacher
    {
        [DisplayName("ID đăng ký")]
        public int ID { get; set; }

        [DisplayName("ID lớp học phần")]
        public int ID_lop_tc { get; set; }

        [DisplayName("ID giáo viên")]
        public int ID_cb { get; set; }

        [DisplayName("ID moodle")]
        public int ID_moodle { get; set; }

        [DisplayName("Mã GV")]
        public string Ma_cb { get; set; }

        [DisplayName("Họ và đệm")]
        public string Ho_dem { get; set; }

        [DisplayName("Tên")]
        public string Ten { get; set; }

        [DisplayName("Khoa")]
        public string Khoa { get; set; }

        [DisplayName("Ngày sinh")]
        public DateTime? Ngay_sinh { get; set; }

        [DisplayName("Giới tính")]
        public string Gioi_tinh { get; set; }

        [DisplayName("ID vai trò")]
        public string ID_vai_tro { get; set; }

        [DisplayName("Vai trò")]
        public string Vai_tro { get; set; }

        [DisplayName("Trạng thái")]
        public bool Trang_thai { get; set; }
    }
}