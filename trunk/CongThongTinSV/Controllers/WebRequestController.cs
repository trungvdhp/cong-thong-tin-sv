using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace CongThongTinSV.Controllers
{
    public class WebRequestController : Controller
    {
        private string RestUrl = WebConfigurationManager.AppSettings["ServerUrl"] +
    "webservice/rest/server.php";
        private string LoginUrl = WebConfigurationManager.AppSettings["ServerUrl"] +
            "login/token.php";
        private string SoapUrl = WebConfigurationManager.AppSettings["ServerUrl"] +
            "webservice/soap/server.php";

        private WebRequest MyRequest { get; set; }
        private Stream MyDataStream { get; set; }
        public String Status { get; set; }

        public WebRequestController(string url)
        {
            MyRequest = WebRequest.Create(url);
        }

        public WebRequestController(int scriptType)
        {
            if (scriptType == 1)
                MyRequest = WebRequest.Create(LoginUrl);
            else if (scriptType == 2)
                MyRequest = WebRequest.Create(SoapUrl);
            else
                MyRequest = WebRequest.Create(RestUrl);
        }

        public WebRequestController(int scriptType, string queryData)
        {
            if(scriptType == 1)
                queryData = LoginUrl + "?" + queryData;
            else if(scriptType == 2)
                queryData = SoapUrl + "?" + queryData;
            else if(scriptType == 3)
                queryData = RestUrl + "?" + queryData + "&wstoken=" + AccountController.GetCurrentUserData()[3];
            else
                queryData = RestUrl + "?" + queryData + "&moodlewsrestformat=json" + "&wstoken=" + AccountController.GetCurrentUserData()[3];

            MyRequest = WebRequest.Create(queryData);
        }

        private void SetMethod(string method)
        {
            if (method.Equals("GET") || method.Equals("POST"))
            {
                MyRequest.Method = method;
            }
            else
            {
                throw new Exception("Invalid Method Type");
            }
        }

        public WebRequestController(string url, string method)
            : this(url)
        {
            SetMethod(method);
        }

        private void ProcessData(string queryData)
        {
            string postData = queryData;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            MyRequest.ContentType = "application/x-www-form-urlencoded";
            MyRequest.ContentLength = byteArray.Length;
            MyDataStream = MyRequest.GetRequestStream();
            MyDataStream.Write(byteArray, 0, byteArray.Length);
            MyDataStream.Close();
        }

        public WebRequestController(string url, string method, string queryData)
            : this(url, method)
        {
            ProcessData(queryData);
        }

        /// <summary>
        /// Init web request with url, method and query data
        /// </summary>
        /// <param name="scriptType">script type: 1 = login script, 2 = soap script, 3 = rest script with format xml, other = rest script with format json</param>
        /// <param name="method">method: POST or GET</param>
        /// <param name="queryData">query data</param>
        public WebRequestController(int scriptType, string method, string queryData)
            : this(scriptType)
        {
            if (scriptType == 3)
                queryData += "&wstoken=" + AccountController.GetCurrentUserData()[3];
            else if (scriptType > 3)
                queryData += "&moodlewsrestformat=json" + "&wstoken=" + AccountController.GetCurrentUserData()[3];
            //UtilityController.WriteTextToFile("D:\\Query.txt", queryData);
            SetMethod(method);
            ProcessData(queryData);
        }

        public string GetResponse()
        {
            WebResponse response;
            try
            {
                // Lấy dữ liệu trả về của yêu cầu
                response = MyRequest.GetResponse();
            }
            catch //(System.Exception ex)
            {
                // Không kết nối được đến host
                return "exception";
            }

            // Lấy tình trạng trả về .
            this.Status = ((HttpWebResponse)response).StatusDescription;

            // Lấy luồng dữ liệu trả về từ server được yêu cầu
            MyDataStream = response.GetResponseStream();

            // Mở một luồng sử dụng StreamReader để đọc dữ liệu trả về
            StreamReader reader = new StreamReader(MyDataStream);

            // Đọc toàn bộ nội dung trả về
            string responseFromServer = reader.ReadToEnd();

            // Đóng tất cả các luồng
            reader.Close();
            MyDataStream.Close();
            response.Close();

            return responseFromServer;
        }
    }
}
