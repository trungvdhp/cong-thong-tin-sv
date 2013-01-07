using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml;

namespace CongThongTinSV.App_Lib
{
    public class Utility
    {
        /// <summary>
        /// Write text to a file
        /// </summary>
        /// <param name="filePath">file path</param>
        /// <param name="text">text to write</param>
        public static void WriteTextToFile(string filePath, string text)
        {
            StreamWriter file = new StreamWriter(filePath);
            file.WriteLine(text);
            file.Close();
        }
        /// <summary>
        /// Read text from file
        /// </summary>
        /// <param name="filePath">file path</param>
        /// <returns>List line of file</returns>
        public static List<string> ReadTextFile(string filePath)
        {
            List<string> list = new List<string>();
            StreamReader file = new StreamReader(filePath);

            while (!file.EndOfStream)
            {
                list.Add(file.ReadLine());
            }

            file.Close();

            return list;
        }

        /// <summary>
        /// Get data table from file
        /// </summary>
        /// <param name="filePath">file path</param>
        /// <param name="separator">separator char array </param>
        /// <returns>DataTable</returns>
        public static DataTable GetTable(string filePath, char[] separator, string[] columnNames)
        {
            DataTable rs = new DataTable();
            int len = columnNames.Length;

            for (int i = 0; i < len; i++)
                rs.Columns.Add(columnNames[i]);

            StreamReader file = new StreamReader(filePath);

            while (!file.EndOfStream)
            {
                string[] s = file.ReadLine().Split(separator);
                int lens = s.Length;
                // Get min length
                lens = lens < len ? lens : len;
                DataRow row = rs.NewRow();

                for (int i = 0; i < lens; i++)
                    row[columnNames[i]] = s[i];

                rs.Rows.Add(row);
            }

            file.Close();

            return rs;
        }
        /// <summary>
        /// Get Service Table
        /// </summary>
        /// <returns>Service DataTable</returns>
        public static DataTable GetServiceTable()
        {
            return GetTable(
                System.Web.HttpContext.Current.Server.MapPath("~") + "./App_Data/ServiceList.txt"
                , new char[] { '-' }
                , new string[] { "FullName", "ShortName" }
                );
        }
        /// <summary>
        /// Method for transform all XmlNode from XmlDocument to a TreeView
        /// </summary>
        /// <param name="xmlNode">a XmlNode</param>
        /// <param name="treeNode">a TreeNode</param>
        public static void AddNode(XmlNode xmlNode, TreeNode treeNode)
        {
            XmlNode xNode;
            TreeNode tNode;
            XmlNodeList nodeList;
            int i = 0;
            if (xmlNode.HasChildNodes)
            {
                nodeList = xmlNode.ChildNodes;
                for (i = 0; i <= nodeList.Count - 1; i++)
                {
                    xNode = xmlNode.ChildNodes[i];
                    try
                    {
                        treeNode.ChildNodes.Add(new TreeNode(xNode.Attributes["name"].Value.ToString()));
                    }
                    catch //(System.Exception ex)
                    {
                        treeNode.ChildNodes.Add(new TreeNode(xNode.Name));
                    }

                    tNode = treeNode.ChildNodes[i];
                    AddNode(xNode, tNode);
                }
            }
            else
            {
                treeNode.Text = xmlNode.InnerText.ToString();
            }
        }

        /// <summary>
        /// Method for get id number from full name
        /// </summary>
        /// <param name="fullName">full name</param>
        /// <returns>id number string</returns>
        public static string GetIdnumber(string fullName)
        {
            string rs = "";

            //split full name by char ' ' into string array
            string[] split = fullName.Split(new char[] { ' ' });
            //get total elements of string array
            int len = split.Length - 1;

            //cut and join first letter of each word
            for (int i = 0; i < len; ++i)
            {
                char c = '-';
                int j = 0;
                int n = split[i].Length;

                while (j < n)
                {
                    c = split[i][j++];

                    if (char.IsLetterOrDigit(c))
                        break;
                }

                if (j == n) 
                    c = split[i][0];

                rs += c.ToString();
            }

            rs += split[len - 1].Substring(split[len - 1].IndexOf('-')) + split[len];

            //return lower result string 
            return rs.ToLower();
        }

        /// <summary>
        /// Method for converting a System.DateTime value to a UNIX Timestamp
        /// </summary>
        /// <param name="value">date to convert</param>
        /// <returns>Timestamp</returns>
        public static int ConvertToTimestamp(DateTime value)
        {
            //create Timespan by subtracting the value provided from
            //the Unix Epoch
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());

            //return the total seconds (which is a UNIX timestamp)
            return (int)span.TotalSeconds;
        }

        /// <summary>
        /// Method for converting a Date Time String to a UNIX Timestamp
        /// </summary>
        /// <param name="dateString">date string to convert</param>
        /// <returns>Timestamp</returns>
        public static int ConvertToTimestamp(string dateString)
        {
            //get a System.DateTime from DateTime String
            DateTime value = ConvertToDateTime(dateString);
            //create Timespan by subtracting the value provided from
            //the Unix Epoch
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());

            //return the total seconds (which is a UNIX timestamp)
            return (int)span.TotalSeconds;
        }
        /// <summary>
        /// Method for converting a UNIX timestamp to a regular
        /// System.DateTime value (and also to the current local time)
        /// </summary>
        /// <param name="timestamp">value to be converted</param>
        /// <returns>System.DateTime</returns>
        public static DateTime ConvertToDateTime(int timestamp)
        {
            //create a new DateTime value based on the Unix Epoch
            DateTime mindate = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            //add the timestamp to the value
            DateTime newDateTime = mindate.AddSeconds(timestamp);

            //return the System.DateTime value
            return newDateTime.ToLocalTime();
        }
        /// <summary>
        /// Method for converting a UNIX timestamp to a
        /// datetime string in a specified format
        /// </summary>
        /// <param name="timestamp">value to be converted</param>
        /// <param name="format">datetime format</param>
        /// <returns>Vietnamese datetime string</returns>
        public static string ConvertToDateTimeString(int timestamp, string format)
        {
            DateTime date = ConvertToDateTime(timestamp);
            DateTime mindate = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            if (date.ToShortDateString() == mindate.ToShortDateString())
            {
                return "Never";
            }

            return date.ToString(format);
        }
        /// <summary>
        /// Method for converting a Date Time String in cultureInfo("fr-FR")
        ///  to a regular System.DateTime value (and also to the current local time)
        /// </summary>
        /// <param name="dateString">date string to be converted</param>
        /// <returns>System.DateTime</returns>
        public static DateTime ConvertToDateTime(string dateString)
        {
            //get string array from date string in format "dd-MM-yyyy" 
            string[] s = dateString.Split(new char[] { '-', '/' });

            //return the System.DateTime value
            return new DateTime(Convert.ToInt32(s[2]), Convert.ToInt32(s[1]), Convert.ToInt32(s[0])).ToLocalTime();
        }
        
        public static string ConvertToString(TimeSpan span)
        {
            string formatted = string.Format("{0}{1}{2}{3}",
            span.Duration().Days > 0 ? string.Format("{0:0} days ", span.Days) : string.Empty,
            span.Duration().Hours > 0 ? string.Format("{0:0} hours ", span.Hours) : string.Empty,
            span.Duration().Minutes > 0 ? string.Format("{0:0} minutes ", span.Minutes) : string.Empty,
            span.Duration().Seconds > 0 ? string.Format("{0:0} secs", span.Seconds) : string.Empty);

            if (formatted.EndsWith(" ")) formatted = formatted.Substring(0, formatted.Length - 2);

            if (string.IsNullOrEmpty(formatted)) formatted = "0 secs";

            return formatted;
        }

        public static string ConvertToDetailDateTimeString(int timestamp)
        {
            DateTime date = ConvertToDateTime(timestamp);
            DateTime mindate = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            if (date.ToShortDateString() == mindate.ToShortDateString())
            {
                return "Never";
            }

            TimeSpan span = DateTime.Now.Subtract(date);

            return date.ToLongDateString() + ", " + date.ToLongTimeString() + 
                " (" + ConvertToString(span) + ")";
        }

        /// <summary>
        /// Get firstname from name
        /// </summary>
        /// <param name="name">name</param>
        /// <returns></returns>
        public static string GetFirstName(string name)
        {
            return name.Trim().Substring(name.LastIndexOf(' ') + 1);
        }

        /// <summary>
        /// Get lastname from name
        /// </summary>
        /// <param name="name">name</param>
        /// <returns></returns>
        public static string GetLastName(string name)
        {
            return name.Trim().Substring(0, name.LastIndexOf(' ')).Trim();
        }

        /// <summary>
        /// Get right substring of a input string
        /// </summary>
        /// <param name="input"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RightString(string input, int length)
        {
            int startIndex = input.Length - length;

            return input.Substring(startIndex);
        }

        /// <summary>
        /// Checks if a specified value exists in a string array that contains the substrings in this string that are delimited by a separator string
        /// </summary>
        /// <param name="array">string array</param>
        /// <param name="separator">separator</param>
        /// <param name="value">value needs to check</param>
        /// <returns></returns>
        public static bool InArray(string array, char[] separator,  string value)
        {
            if (array == null)
            {
                return false;
            }

            string[] arr = array.Split(separator);

            return arr.Contains(value);
        }

        /// <summary>
        /// Validate a name as file name, sheet name...
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="maxLength">Max length</param>
        /// <param name="replacement">Replacement string</param>
        /// <returns></returns>
        public static string ValidateName(string name, int maxLength = 0, string replacement = "")
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                name = name.Replace(c.ToString(), replacement);
            }

            if (maxLength > 0 && name.Length > maxLength)
            {
                name = name.Substring(0, maxLength);
            }

            return name;
        }
    }
}
