using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Web.Mvc;

namespace Eleave.Library
{
    public class Utils
    {
        public static int CInt(object value)
        {
            if (value == null)
                return (0);

            var data = value.ToString();
            data = data.Replace(",", "");
            var isNum = new System.Text.RegularExpressions.Regex("^([-]|[0-9])[0-9]*$");
            var m = isNum.Match(data);
            return m.Success ? (Convert.ToInt32(data)) : (0);
        }

        public static long CLong(object value)
        {
            if (value == null)
                return (0);

            var data = value.ToString();
            data = data.Replace(",", "");
            var isNum = new System.Text.RegularExpressions.Regex("^([-]|[0-9])[0-9]*$");
            var m = isNum.Match(data);
            return m.Success ? (Convert.ToInt64(data)) : (0);
        }

        public static double CDouble(object value)
        {
            var data = value.ToString();
            data = data.Replace(",", "");
            var isNum = new System.Text.RegularExpressions.Regex("^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$|^([-]|[0-9])[0-9]*$");
            var m = isNum.Match(data);
            return m.Success ? (Convert.ToDouble(data)) : (0.00);
        }

        public static decimal CDecimal(object value)
        {
            var data = value.ToString();
            data = data.ToString(CultureInfo.InvariantCulture).Replace(",", "");
            var isNum = new System.Text.RegularExpressions.Regex("^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$|^([-]|[0-9])[0-9]*$");
            var m = isNum.Match(data);
            return m.Success ? (Convert.ToDecimal(data)) : (Convert.ToDecimal("0.00"));
        }

        public static string GetPathOf(string fullpath)
        {
            var lastIdx = fullpath.LastIndexOf("\\", StringComparison.Ordinal);
            return (fullpath.Substring(0, lastIdx));
        }

        public static void ByteToFile(byte[] buffer, string targetFile)
        {
            try
            {
                var stf = new MemoryStream(buffer);
                var stt = File.Create(targetFile);
                var br = new BinaryReader(stf);
                var bw = new BinaryWriter(stt);
                bw.Write(br.ReadBytes((int)stf.Length));
                bw.Flush();
                bw.Close();
                br.Close();
                stf = null;
                stt = null;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static byte[] FileToByte(string sourceFile)
        {
            try
            {
                var b = File.ReadAllBytes(sourceFile);
                return (b);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static string GetConfig(string key)
        {
            /*
            if (key.Equals("CN"))
                return (EncryptData.Decrypt(ConfigurationSettings.AppSettings[key]));
            else
            */

            return (ConfigurationSettings.AppSettings[key]);
        }

        public static string Left(string str, int length)
        {
            return str.Substring(0, Math.Min(length, str.Length));
        }

        public static string Right(string original, int numberCharacters)
        {
            return original.Substring(numberCharacters > original.Length ? 0 : original.Length - numberCharacters);
        }

        public static string ConvertDatetime(string date)
        {
            var stratDate = date.Split('/');
            string newDate = new DateTime(Convert.ToInt32(stratDate[2]) - 543, Convert.ToInt32(stratDate[1]), Convert.ToInt32(stratDate[0])).ToString("yyyy-MM-dd", new CultureInfo("en-US"));

            return newDate;

        }

        public static string ConvertDatetimeToString(DateTime datetime)
        {
            return datetime.Date.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
        }

        public static DateTime ConvertStringToDatetime(string datetime)
        {
            var stratDate = datetime.Split('/');

            return new DateTime(Convert.ToInt32(stratDate[2]) - 543, Convert.ToInt32(stratDate[1]), Convert.ToInt32(stratDate[0]));

            //var stratDate = datetime.Split('-');
            //string newDate = new DateTime(Convert.ToInt32(stratDate[0]) + 543, Convert.ToInt32(stratDate[1]), Convert.ToInt32(stratDate[2])).ToString("dd/MM/yyyy", new CultureInfo("en-TH"));

            //return newDate;
        }

        public static DateTime ConvertStringToDatetimeEn(string datetime)
        {
            var stratDate = datetime.Split('-');

            return new DateTime(Convert.ToInt32(stratDate[0]), Convert.ToInt32(stratDate[1]), Convert.ToInt32(stratDate[2]));

            //var stratDate = datetime.Split('-');
            //string newDate = new DateTime(Convert.ToInt32(stratDate[0]) + 543, Convert.ToInt32(stratDate[1]), Convert.ToInt32(stratDate[2])).ToString("dd/MM/yyyy", new CultureInfo("en-TH"));

            //return newDate;
        }

        public static DateTime ConvertStringToDatetimeEnSlash(DateTime datetime)
        {
            var stratDate = datetime.ToString().Split('/');

            return new DateTime(Convert.ToInt32(stratDate[0]), Convert.ToInt32(stratDate[1]), Convert.ToInt32(stratDate[2]));

        }
        public static string ConvertTimeToString(DateTime datetime)
        {
            return datetime.ToString("H:mm", new CultureInfo("th-TH"));
        }

        public static DateTime ConvertStringToDatetimeEmail(string datetime)
        {
            var stratDate = datetime.Split('/');
            var strYear = stratDate[2].ToString().Split(' ');
            var strTime = strYear[1].ToString().Split(':');

            DateTime dateTime = new DateTime(Convert.ToInt32(strYear[0]) - 543, Convert.ToInt32(stratDate[1]), Convert.ToInt32(stratDate[0]), Convert.ToInt32(strTime[0]) + 7, Convert.ToInt32(strTime[1]), Convert.ToInt32(strTime[2]));
            return dateTime;
        }


        //Implemented based on interface, not part of algorithm
        public static string RemoveAllNamespaces(string xmlDocument)
        {
            XElement xmlDocumentWithoutNs = RemoveAllNamespaces(XElement.Parse(xmlDocument));

            return xmlDocumentWithoutNs.ToString();
        }

        //Core recursion function
        private static XElement RemoveAllNamespaces(XElement xmlDocument)
        {
            if (!xmlDocument.HasElements)
            {
                XElement xElement = new XElement(xmlDocument.Name.LocalName);
                xElement.Value = xmlDocument.Value;

                foreach (XAttribute attribute in xmlDocument.Attributes())
                    xElement.Add(attribute);

                return xElement;
            }
            return new XElement(xmlDocument.Name.LocalName, xmlDocument.Elements().Select(el => RemoveAllNamespaces(el)));
        }


        //get name month
        public Array ListMonth(string language = "EN", string format = "S")
        {
            var arrayMonth = new string[12];
            if (language == "EN")
            {
                if (format == "S")
                {
                    arrayMonth = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                }
            }
            return (arrayMonth);
        }
        public List<DateTime> getAllDates(int year, int month)
        {
            var ret = new List<DateTime>();
            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                ret.Add(new DateTime(year, month, i));
            }
            return ret;
        }
        public static string GetDocumentRequest(string DocumentNo)
        {
            string getDocumentNo = "";

            using (SqlConnection Connection = new SqlConnection(GetConfig("HRIS_DB")))
            {
                Connection.Open();
                var cmdSearch = new SqlCommand("P_Get_Document_Request", Connection);

                cmdSearch.CommandType = CommandType.StoredProcedure;
                cmdSearch.Parameters.AddWithValue("@inReqNo", DocumentNo);
                SqlParameter returnResult = new SqlParameter("@outResult", SqlDbType.NVarChar, 1000);
                returnResult.Direction = ParameterDirection.Output;
                cmdSearch.Parameters.Add(returnResult);
                int INSID = cmdSearch.ExecuteNonQuery();
                getDocumentNo = cmdSearch.Parameters["@outResult"].Value.ToString();
                cmdSearch.Dispose();
            }
            return getDocumentNo;
        }
        public static string savePathFile(string DocumentNo)
        {
            string getDocumentNo = "";

            using (SqlConnection Connection = new SqlConnection(GetConfig("HRIS_DB")))
            {
                Connection.Open();
                var cmdSearch = new SqlCommand("P_Save_PathFile", Connection);

                cmdSearch.CommandType = CommandType.StoredProcedure;
                cmdSearch.Parameters.AddWithValue("@inReqNo", DocumentNo);
                cmdSearch.Parameters.AddWithValue("@inFileName", DocumentNo);
                cmdSearch.Parameters.AddWithValue("@inFilePath", DocumentNo);
                cmdSearch.Parameters.AddWithValue("@inFileNo", DocumentNo);
                cmdSearch.Parameters.AddWithValue("@inUser", DocumentNo);
                SqlParameter returnResult = new SqlParameter("@outResult", SqlDbType.NVarChar, 1000);
                returnResult.Direction = ParameterDirection.Output;
                cmdSearch.Parameters.Add(returnResult);
                int INSID = cmdSearch.ExecuteNonQuery();
                getDocumentNo = cmdSearch.Parameters["@outResult"].Value.ToString();
                cmdSearch.Dispose();
                Connection.Close();
            }
            return getDocumentNo;
        }
        public static string deleteFile(string IdFile)
        {
            string getResult = "";

            using (SqlConnection Connection = new SqlConnection(GetConfig("HRIS_DB")))
            {
                Connection.Open();
                var cmdSearch = new SqlCommand("P_Delete_Request_File", Connection);

                cmdSearch.CommandType = CommandType.StoredProcedure;
                cmdSearch.Parameters.AddWithValue("@inIdFile", IdFile);
                SqlParameter returnResult = new SqlParameter("@outResult", SqlDbType.NVarChar, 1000);
                returnResult.Direction = ParameterDirection.Output;
                cmdSearch.Parameters.Add(returnResult);
                int INSID = cmdSearch.ExecuteNonQuery();
                getResult = cmdSearch.Parameters["@outResult"].Value.ToString();
                cmdSearch.Dispose();
                Connection.Close();
            }
            return getResult;
        }
        public static string checkApprovalFlow(string EmpId)
        {
            string getResult = "";
            using (SqlConnection Connection = new SqlConnection(GetConfig("HRIS_DB")))
            {
                Connection.Open();
                var cmdSearch = new SqlCommand("P_Check_ApprovalFlow_By_Employee", Connection);

                cmdSearch.CommandType = CommandType.StoredProcedure;
                cmdSearch.Parameters.AddWithValue("@inEmpId", EmpId);
                SqlParameter returnResult = new SqlParameter("@outResult", SqlDbType.NVarChar, 1000);
                returnResult.Direction = ParameterDirection.Output;
                cmdSearch.Parameters.Add(returnResult);
                int INSID = cmdSearch.ExecuteNonQuery();
                getResult = cmdSearch.Parameters["@outResult"].Value.ToString();
                cmdSearch.Dispose();
                Connection.Close();
            }
            return getResult;
        }
        public static string getLeaveTakenByDate(string EmpId, string StartDate, string EndDate)
        {
            double LeaveTakenDay = 0;

            using (SqlConnection Connection = new SqlConnection(GetConfig("HRIS_DB")))
            {
                Connection.Open();
                var cmdSearch = new SqlCommand("P_Get_LeaveTaken_By_Date", Connection);

                cmdSearch.CommandType = CommandType.StoredProcedure;
                cmdSearch.Parameters.AddWithValue("@inEmpId", EmpId);
                cmdSearch.Parameters.AddWithValue("@inStartDate", StartDate);
                cmdSearch.Parameters.AddWithValue("@inEndDate", EndDate);
                SqlParameter returnResult = new SqlParameter("@outLeaveTakenDay", SqlDbType.NVarChar, 1000);
                returnResult.Direction = ParameterDirection.Output;
                returnResult.Scale = 2;
                cmdSearch.Parameters.Add(returnResult);
                cmdSearch.ExecuteNonQuery();
                if (returnResult.Value != DBNull.Value)
                {
                    LeaveTakenDay = Convert.ToDouble(returnResult.Value);
                }
                else
                {
                    LeaveTakenDay = 0;
                }
                cmdSearch.Dispose();
                Connection.Close();
            }
            return LeaveTakenDay.ToString(); ;
        }
        public static string CalculateDayHour(double txtNumberDate)
        {
            string txtNumberDateOrg = string.Empty;
            txtNumberDateOrg = txtNumberDate.ToString("N2");
            var txtDay = string.Empty;
            var txthours = string.Empty;
            var day = string.Empty;
            var hours = string.Empty;
            var txtFullDayHours = string.Empty;
            if (!string.IsNullOrEmpty(txtNumberDateOrg) && txtNumberDate > 0)
            {
                string[] fullResultTxt = txtNumberDateOrg.Split('.');
                txtDay = fullResultTxt[0];
                txthours = fullResultTxt[1];
                //check จำนวนวัน
                if (txtDay != "0")
                {
                    day = txtDay + " วัน";
                }
                //check มีตัวเลขหลังจุดทศนิยมไหมถ้ามี คือ ชม
                if (!string.IsNullOrEmpty(txthours))
                {
                    if (txthours == "12") hours = "1";
                    else if (txthours == "13") hours = "1";
                    else if (txthours == "25") hours = "2";
                    else if (txthours == "26") hours = "2";
                    else if (txthours == "37") hours = "3";
                    else if (txthours == "38") hours = "3";
                    else if (txthours == "50") hours = "4";
                    else if (txthours == "62") hours = "5";
                    else if (txthours == "63") hours = "5";
                    else if (txthours == "74") hours = "6";
                    else if (txthours == "75") hours = "6";
                    else if (txthours == "87") hours = "7";
                    else if (txthours == "88") hours = "7";
                    else hours = "";
                }
            }
            else
            {
                return "0";
            }

            if (!string.IsNullOrEmpty(hours))
            {
                hours = hours + " ชม";
            }
            if (!string.IsNullOrEmpty(day) && !string.IsNullOrEmpty(hours))
            {
                txtFullDayHours = day + " " + hours;
            }
            else
            {
                txtFullDayHours = day + hours;
            }

            return txtFullDayHours;
        }
    }
}