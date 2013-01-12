/*
* Kendo UI Localization Project for v2012.3.1114 
* Copyright 2012 Telerik AD. All rights reserved.
* 
* Ecuadorian Spanish (vi-VN) Language Pack
*
* Project home  : https://github.com/loudenvier/kendo-global
* Kendo UI home : http://kendoui.com
* Author        : Vũ Đình Trung
*                 
*
* This project is released to the public domain, although one must abide to the 
* licensing terms set forth by Telerik to use Kendo UI, as shown bellow.
*
* Telerik's original licensing terms:
* -----------------------------------
* Kendo UI Web commercial licenses may be obtained at
* https://www.kendoui.com/purchase/license-agreement/kendo-ui-web-commercial.aspx
* If you do not own a commercial license, this file shall be governed by the
* GNU General Public License (GPL) version 3.
* For GPL requirements, please review: http://www.gnu.org/copyleft/gpl.html
*/

kendo.ui.Locale = "Việt Nam (vi-VN)";
kendo.ui.ColumnMenu.prototype.options.messages = 
  $.extend(kendo.ui.ColumnMenu.prototype.options.messages, {

/* COLUMN MENU MESSAGES 
 ****************************************************************************/   
  sortAscending: "Tăng dần",
  sortDescending: "Giảm dần",
  filter: "Lọc",
  columns: "Các cột"
 /***************************************************************************/   
});

kendo.ui.Groupable.prototype.options.messages = 
  $.extend(kendo.ui.Groupable.prototype.options.messages, {

/* GRID GROUP PANEL MESSAGES 
 ****************************************************************************/   
  empty: "Kéo thả các tiêu đề cột để phân nhóm"
 /***************************************************************************/   
});

kendo.ui.FilterMenu.prototype.options.messages = 
  $.extend(kendo.ui.FilterMenu.prototype.options.messages, {
  
/* FILTER MENU MESSAGES 
 ***************************************************************************/   
	info: "Hiển thị các hàng với giá trị:",        // sets the text on top of the filter menu
	filter: "Lọc",      // sets the text for the "Filter" button
	clear: "Xóa lọc",        // sets the text for the "Clear" button
	// when filtering boolean numbers
	isTrue: "Có", // sets the text for "isTrue" radio button
	isFalse: "Không",     // sets the text for "isFalse" radio button
	//changes the text of the "And" and "Or" of the filter menu
	and: "Và",
	or: "Hoặc",
  selectValue: "-Lựa chọn một giá trị-"
 /***************************************************************************/   
});
         
kendo.ui.FilterMenu.prototype.options.operators =           
  $.extend(kendo.ui.FilterMenu.prototype.options.operators, {

/* FILTER MENU OPERATORS (for each supported data type) 
 ****************************************************************************/   
  string: {
      eq: "Bằng",
      neq: "Không bằng",
      startswith: "Bắt đầu với",
      contains: "Bao gồm",
      doesnotcontain: "Không bao gồm",
      endswith: "Kết thúc với"
  },
  number: {
      eq: "Bằng",
      neq: "Khác",
      gte: "Lớn hơn hoặc bằng",
      gt: "Lớn hơn",
      lte: "Nhỏ hơn hoặc bằng",
      lt: "Nhỏ hơn"
  },
  date: {
      eq: "Bằng",
      neq: "Khác",
      gte: "Sau hoặc bằng ngày",
      gt: "Sau ngày",
      lte: "Trước hoặc bằng ngày",
      lt: "Trước ngày"
  },
  enums: {
      eq: "Bằng",
      neq: "Khác"
  }
 /***************************************************************************/   
});

kendo.ui.Pager.prototype.options.messages = 
  $.extend(kendo.ui.Pager.prototype.options.messages, {
  
/* PAGER MESSAGES 
 ****************************************************************************/   
  display: "{0} - {1} của {2} dòng",
  empty: "Trống",
  page: "Trang",
  of: "của {0}",
  itemsPerPage: "dòng mỗi trang",
  first: "Trang đầu tiên",
  previous: "Trang trước",
  next: "Trang sau",
  last: "Trang cuối cùng"
  //refresh: "Làm mới"
 /***************************************************************************/   
});

kendo.ui.Validator.prototype.options.messages = 
  $.extend(kendo.ui.Validator.prototype.options.messages, {

/* VALIDATOR MESSAGES 
 ****************************************************************************/   
  required: "{0} là bắt buộc",
  pattern: "{0} không hợp lệ",
  min: "{0} phải lớn hơn hoặc bằng {1}",
  max: "{0} phải nhỏ hơn hoặc bằng {1}",
  step: "{0} không hợp lệ",
  email: "{0} không phải là một địa chỉ hợp lệ",
  url: "{0} không phải là một địa chỉ hợp lệ",
  date: "{0} không phải là một ngày hợp lệ"
 /***************************************************************************/   
});

kendo.ui.ImageBrowser.prototype.options.messages = 
  $.extend(kendo.ui.ImageBrowser.prototype.options.messages, {

/* IMAGE BROWSER MESSAGES 
 ****************************************************************************/   
  uploadFile: "Gửi đi",
  orderBy: "Sắp xếp theo",
  orderByName: "Tên",
  orderBySize: "Kích thước",
  directoryNotFound: "Không tìm thấy thư mục này.",
  emptyFolder: "Thư mục rỗng",
  deleteFile: 'Bạn có chắc bạn muốn xóa "{0}"?',
  invalidFileType: "Các tập tin được lựa chọn \"{0}\" là không hợp lệ. Các loại tập tin được hỗ trợ là {1}.",
  overwriteFile: "Một tập tin có tên \"{0}\" đã tồn tại trong thư mục hiện hành. Bạn có muốn ghi đè lên không?",
  dropFilesHere: "Thả các tập tin ở đây"
 /***************************************************************************/   
});

kendo.ui.Editor.prototype.options.messages = 
  $.extend(kendo.ui.Editor.prototype.options.messages, {

/* EDITOR MESSAGES 
 ****************************************************************************/   
  bold: "Đậm",
  italic: "Nghiêng",
  underline: "Gạch chân",
  strikethrough: "Gạch ngang giữa",
  superscript: "Chỉ số trên",
  subscript: "Chỉ số dưới",
  justifyCenter: "Canh giữa",
  justifyLeft: "Canh trái",
  justifyRight: "Canh phải",
  justifyFull: "Canh đều hai bên",
  insertUnorderedList: "Chèn một danh sách không có thứ tự",
  insertOrderedList: "Chèn một danh sách có thứ tự",
  indent: "Thụt lề",
  outdent: "Nhô lề",
  createLink: "Tạo liên kết",
  unlink: "Xóa liên kết",
  insertImage: "Chèn ảnh",
  insertHtml: "Chèn HTML",
  fontName: "Lựa chọn font chữ",
  fontNameInherit: "(Font chữ kế thừa)",
  fontSize: "Chọn kích thước font chữ",
  fontSizeInherit: "(Kích thước font kế thừa)",
  formatBlock: "Định dạng",
  foreColor: "Màu chữ",
  backColor: "Màu nền chữ",
  style: "Kiểu chữ",
  emptyFolder: "Thư mục rỗng",
  uploadFile: "Gửi đi",
  orderBy: "Sắp xếp theo:",
  orderBySize: "Kích thước",
  orderByName: "Tên",
  invalidFileType: "Các tập tin được lựa chọn \"{0}\" là không hợp lệ. Các loại tập tin được hỗ trợ là {1}.",
  deleteFile: 'Bạn có chắc bạn muốn xóa "{0}"?',
  overwriteFile: "Một tập tin có tên \"{0}\" đã tồn tại trong thư mục hiện hành. Bạn có muốn ghi đè lên không?",
  directoryNotFound: "Không tìm thấy thư mục này.",
  imageWebAddress: "địa chỉ ảnh",
  imageAltText: "Tiêu đề thay thế",
  dialogInsert: "Chèn",
  dialogButtonSeparator: "o",
  dialogCancel: "Hủy bỏ"
 /***************************************************************************/   
});
