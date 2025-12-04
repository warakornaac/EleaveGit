using Eleave.Data;
using Eleave.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace Eleave.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult ReportLeaveBalance()
        {
            string EmpID = string.Empty;
            string EmpType = string.Empty;
            var connectionString = ConfigurationManager.ConnectionStrings["HRIS_DB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            if (Session["EmpId"] != null)
            {
                EmpID = Session["EmpId"].ToString();
                EmpType = Session["UserType"].ToString();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

            if (EmpType == "1")
            {
                return RedirectToAction("Index", "Home");
            }
            var Report_leav_bal = new List<ReportLeaveBalance>();
            conn.Open();
            try
            {
                //Report_leav_bal = new GetReportLeaveBalance().GetReportLeaveBalances("", DateTime.Now.Year.ToString(), "", "", EmpID);
                var cmd = new SqlCommand("P_Report_Leave_Balance", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@inComp", "");
                cmd.Parameters.AddWithValue("@inDept", "");
                cmd.Parameters.AddWithValue("@inYear", DateTime.Now.Year.ToString());
                cmd.Parameters.AddWithValue("@inEmpId", "");
                cmd.Parameters.AddWithValue("@inUser", EmpID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Report_leav_bal.Add(new ReportLeaveBalance
                    {
                        Company = reader["Company"]?.ToString() ?? "",
                        LeaveYear = reader["LeaveYear"]?.ToString() ?? "",
                        DeptNameShort = reader["DeptNameShort"]?.ToString() ?? "",
                        DeptName = reader["DeptName"]?.ToString() ?? "",
                        EmpId = reader["EmpId"]?.ToString() ?? "",
                        Position = reader["Position"]?.ToString() ?? "",
                        EmpStatus = reader["EmpStatus"]?.ToString() ?? "",
                        StartDate = reader["StartDate"]?.ToString() ?? "",
                        Fullname = reader["Fullname"]?.ToString() ?? "",
                        Years = reader["Years"]?.ToString() ?? "",
                        Months = reader["Months"]?.ToString() ?? "",
                        Days = reader["Days"]?.ToString() ?? "",

                        AL02_Accured = reader["AL02_Accured"] as decimal? ?? 0,
                        AL02_Leave = reader["AL02_Leave"] as decimal? ?? 0,
                        AL02_Balance = reader["AL02_Balance"] as decimal? ?? 0,
                        AL02_File = reader["AL02_File"] as int? ?? 0,

                        AL01_Accured = reader["AL01_Accured"] as decimal? ?? 0,
                        AL01_Leave = reader["AL01_Leave"] as decimal? ?? 0,
                        AL01_Balance = reader["AL01_Balance"] as decimal? ?? 0,
                        AL01_File = reader["AL01_File"] as int? ?? 0,

                        CP03_Accured = reader["CP03_Accured"] as decimal? ?? 0,
                        CP03_Leave = reader["CP03_Leave"] as decimal? ?? 0,
                        CP03_Balance = reader["CP03_Balance"] as decimal? ?? 0,
                        CP03_File = reader["CP03_File"] as int? ?? 0,

                        SL01_Accured = reader["SL01_Accured"] as decimal? ?? 0,
                        SL01_Leave = reader["SL01_Leave"] as decimal? ?? 0,
                        SL01_Balance = reader["SL01_Balance"] as decimal? ?? 0,
                        SL01_File = reader["SL01_File"] as int? ?? 0,

                        BU04_Accured = reader["BU04_Accured"] as decimal? ?? 0,
                        BU04_Leave = reader["BU04_Leave"] as decimal? ?? 0,
                        BU04_Balance = reader["BU04_Balance"] as decimal? ?? 0,
                        BU04_File = reader["BU04_File"] as int? ?? 0,

                        OT13_Accured = reader["OT13_Accured"] as decimal? ?? 0,
                        OT13_Leave = reader["OT13_Leave"] as decimal? ?? 0,
                        OT13_Balance = reader["OT13_Balance"] as decimal? ?? 0,
                        OT13_File = reader["OT13_File"] as int? ?? 0,

                        OL07_Accured = reader["OL07_Accured"] as decimal? ?? 0,
                        OL07_Leave = reader["OL07_Leave"] as decimal? ?? 0,
                        OL07_Balance = reader["OL07_Balance"] as decimal? ?? 0,
                        OL07_File = reader["OL07_File"] as int? ?? 0,

                        ML06_Accured = reader["ML06_Accured"] as decimal? ?? 0,
                        ML06_Leave = reader["ML06_Leave"] as decimal? ?? 0,
                        ML06_Balance = reader["ML06_Balance"] as decimal? ?? 0,
                        ML06_File = reader["ML06_File"] as int? ?? 0,

                        CO11_Accured = reader["CO11_Accured"] as decimal? ?? 0,
                        CO11_Leave = reader["CO11_Leave"] as decimal? ?? 0,
                        CO11_Balance = reader["CO11_Balance"] as decimal? ?? 0,
                        CO11_File = reader["CO11_File"] as int? ?? 0,

                        BL10_Accured = reader["BL10_Accured"] as decimal? ?? 0,
                        BL10_Leave = reader["BL10_Leave"] as decimal? ?? 0,
                        BL10_Balance = reader["BL10_Balance"] as decimal? ?? 0,
                        BL10_File = reader["BL10_File"] as int? ?? 0,

                        GL09_Accured = reader["GL09_Accured"] as decimal? ?? 0,
                        GL09_Leave = reader["GL09_Leave"] as decimal? ?? 0,
                        GL09_Balance = reader["GL09_Balance"] as decimal? ?? 0,
                        GL09_File = reader["GL09_File"] as int? ?? 0,

                        MS08_Accured = reader["MS08_Accured"] as decimal? ?? 0,
                        MS08_Leave = reader["MS08_Leave"] as decimal? ?? 0,
                        MS08_Balance = reader["MS08_Balance"] as decimal? ?? 0,
                        MS08_File = reader["MS08_File"] as int? ?? 0,

                        WFH_Approve = reader["WFH_Approve"] as decimal? ?? 0,
                        WFH_Waiting = reader["WFH_Waiting"] as decimal? ?? 0,
                        OffSite_Approve = reader["OffSite_Approve"] as decimal? ?? 0,
                        OffSite_Waiting = reader["OffSite_Waiting"] as decimal? ?? 0,

                        SL02_Accured = reader["SL02_Accured"] as decimal? ?? 0,
                        SL02_Leave = reader["SL02_Leave"] as decimal? ?? 0,
                        SL02_Balance = reader["SL02_Balance"] as decimal? ?? 0,
                        SL02_File = reader["SL02_File"] as int? ?? 0,
                    });

                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            finally
            {
                conn.Close();
            }


            LoadDepartments();
            return View(Report_leav_bal);
        }
        [HttpPost]
        public ActionResult SearchReportLeaveBalance(string Company, string Year, string Department, string EmpID)
        {
            string emp = string.Empty;
            string EmpType = string.Empty;
            var connectionString = ConfigurationManager.ConnectionStrings["HRIS_DB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            var Report_leav_bal = new List<ReportLeaveBalance>();
            if (Session["EmpId"] != null)
            {
                emp = Session["EmpId"].ToString();
                EmpType = Session["UserType"].ToString();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

            if (EmpType == "1")
            {
                return RedirectToAction("Index", "Home");
            }
            conn.Open();
            try
            {
                //Report_leav_bal = new GetReportLeaveBalance().GetReportLeaveBalances(Company, Year, Department, EmpID, emp);
                var cmd = new SqlCommand("P_Report_Leave_Balance", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@inComp", Company);
                cmd.Parameters.AddWithValue("@inDept", Department);
                cmd.Parameters.AddWithValue("@inYear", Year);
                cmd.Parameters.AddWithValue("@inEmpId", EmpID);
                cmd.Parameters.AddWithValue("@inUser", emp);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Report_leav_bal.Add(new ReportLeaveBalance
                    {
                        Company = reader["Company"]?.ToString() ?? "",
                        LeaveYear = reader["LeaveYear"]?.ToString() ?? "",
                        DeptNameShort = reader["DeptNameShort"]?.ToString() ?? "",
                        DeptName = reader["DeptName"]?.ToString() ?? "",
                        EmpId = reader["EmpId"]?.ToString() ?? "",
                        Position = reader["Position"]?.ToString() ?? "",
                        EmpStatus = reader["EmpStatus"]?.ToString() ?? "",
                        StartDate = reader["StartDate"]?.ToString() ?? "",
                        Fullname = reader["Fullname"]?.ToString() ?? "",
                        Years = reader["Years"]?.ToString() ?? "",
                        Months = reader["Months"]?.ToString() ?? "",
                        Days = reader["Days"]?.ToString() ?? "",

                        AL02_Accured = reader["AL02_Accured"] as decimal? ?? 0,
                        AL02_Leave = reader["AL02_Leave"] as decimal? ?? 0,
                        AL02_Balance = reader["AL02_Balance"] as decimal? ?? 0,
                        AL02_File = reader["AL02_File"] as int? ?? 0,

                        AL01_Accured = reader["AL01_Accured"] as decimal? ?? 0,
                        AL01_Leave = reader["AL01_Leave"] as decimal? ?? 0,
                        AL01_Balance = reader["AL01_Balance"] as decimal? ?? 0,
                        AL01_File = reader["AL01_File"] as int? ?? 0,

                        CP03_Accured = reader["CP03_Accured"] as decimal? ?? 0,
                        CP03_Leave = reader["CP03_Leave"] as decimal? ?? 0,
                        CP03_Balance = reader["CP03_Balance"] as decimal? ?? 0,
                        CP03_File = reader["CP03_File"] as int? ?? 0,

                        SL01_Accured = reader["SL01_Accured"] as decimal? ?? 0,
                        SL01_Leave = reader["SL01_Leave"] as decimal? ?? 0,
                        SL01_Balance = reader["SL01_Balance"] as decimal? ?? 0,
                        SL01_File = reader["SL01_File"] as int? ?? 0,

                        BU04_Accured = reader["BU04_Accured"] as decimal? ?? 0,
                        BU04_Leave = reader["BU04_Leave"] as decimal? ?? 0,
                        BU04_Balance = reader["BU04_Balance"] as decimal? ?? 0,
                        BU04_File = reader["BU04_File"] as int? ?? 0,

                        OT13_Accured = reader["OT13_Accured"] as decimal? ?? 0,
                        OT13_Leave = reader["OT13_Leave"] as decimal? ?? 0,
                        OT13_Balance = reader["OT13_Balance"] as decimal? ?? 0,
                        OT13_File = reader["OT13_File"] as int? ?? 0,

                        OL07_Accured = reader["OL07_Accured"] as decimal? ?? 0,
                        OL07_Leave = reader["OL07_Leave"] as decimal? ?? 0,
                        OL07_Balance = reader["OL07_Balance"] as decimal? ?? 0,
                        OL07_File = reader["OL07_File"] as int? ?? 0,

                        ML06_Accured = reader["ML06_Accured"] as decimal? ?? 0,
                        ML06_Leave = reader["ML06_Leave"] as decimal? ?? 0,
                        ML06_Balance = reader["ML06_Balance"] as decimal? ?? 0,
                        ML06_File = reader["ML06_File"] as int? ?? 0,

                        CO11_Accured = reader["CO11_Accured"] as decimal? ?? 0,
                        CO11_Leave = reader["CO11_Leave"] as decimal? ?? 0,
                        CO11_Balance = reader["CO11_Balance"] as decimal? ?? 0,
                        CO11_File = reader["CO11_File"] as int? ?? 0,

                        BL10_Accured = reader["BL10_Accured"] as decimal? ?? 0,
                        BL10_Leave = reader["BL10_Leave"] as decimal? ?? 0,
                        BL10_Balance = reader["BL10_Balance"] as decimal? ?? 0,
                        BL10_File = reader["BL10_File"] as int? ?? 0,

                        GL09_Accured = reader["GL09_Accured"] as decimal? ?? 0,
                        GL09_Leave = reader["GL09_Leave"] as decimal? ?? 0,
                        GL09_Balance = reader["GL09_Balance"] as decimal? ?? 0,
                        GL09_File = reader["GL09_File"] as int? ?? 0,

                        MS08_Accured = reader["MS08_Accured"] as decimal? ?? 0,
                        MS08_Leave = reader["MS08_Leave"] as decimal? ?? 0,
                        MS08_Balance = reader["MS08_Balance"] as decimal? ?? 0,
                        MS08_File = reader["MS08_File"] as int? ?? 0,

                        WFH_Approve = reader["WFH_Approve"] as decimal? ?? 0,
                        WFH_Waiting = reader["WFH_Waiting"] as decimal? ?? 0,
                        OffSite_Approve = reader["OffSite_Approve"] as decimal? ?? 0,
                        OffSite_Waiting = reader["OffSite_Waiting"] as decimal? ?? 0,

                        SL02_Accured = reader["SL02_Accured"] as decimal? ?? 0,
                        SL02_Leave = reader["SL02_Leave"] as decimal? ?? 0,
                        SL02_Balance = reader["SL02_Balance"] as decimal? ?? 0,
                        SL02_File = reader["SL02_File"] as int? ?? 0,
                    });

                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            LoadDepartments();
            return View("ReportLeaveBalance", Report_leav_bal);
        }
        public ActionResult ReportLeaveBalanceMonth()
        {
            var sw = new Stopwatch();

            string emp = string.Empty;
            string EmpType = string.Empty;
            var Report_leav_bal = new List<ReportLeaveBalance>();
            var connectionString = ConfigurationManager.ConnectionStrings["HRIS_DB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            if (Session["EmpId"] != null)
            {
                emp = Session["EmpId"].ToString();
                EmpType = Session["UserType"].ToString();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            // วันแรกของเดือน
            DateTime firstDayOfMonth = new DateTime(year, month, 1);

            // วันสุดท้ายของเดือน
            DateTime lastDayOfMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            conn.Open();
            sw.Start();
            try
            {
                //Report_leav_bal = new GetReportLeaveBalanceMonth().GetReportMonth("", "", firstDayOfMonth.ToString("yyyy-MM-dd"), lastDayOfMonth.ToString("yyyy-MM-dd"));
                var cmd = new SqlCommand("P_Report_Leave_Balance_month", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@inComp", "");
                cmd.Parameters.AddWithValue("@inDept", "");
                cmd.Parameters.AddWithValue("@inStartdate", firstDayOfMonth.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@inEnddate", lastDayOfMonth.ToString("yyyy-MM-dd"));
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Report_leav_bal.Add(new ReportLeaveBalance
                    {
                        Company = reader["Company"]?.ToString() ?? "",
                        DeptNameShort = reader["DeptNameShort"]?.ToString() ?? "",
                        DeptName = reader["DeptName"]?.ToString() ?? "",
                        EmpId = reader["EmpId"]?.ToString() ?? "",
                        Position = reader["Position"]?.ToString() ?? "",
                        EmpStatus = reader["EmpStatus"]?.ToString() ?? "",
                        StartDate = reader["StartDate"]?.ToString() ?? "",
                        Fullname = reader["Fullname"]?.ToString() ?? "",
                        Years = reader["Years"]?.ToString() ?? "",
                        Months = reader["Months"]?.ToString() ?? "",
                        Days = reader["Days"]?.ToString() ?? "",

                        AL02_Accured = reader["AL02_Accured"] as decimal? ?? 0,
                        AL02_Leave = reader["AL02_Leave"] as decimal? ?? 0,
                        AL02_Balance = reader["AL02_Balance"] as decimal? ?? 0,
                        AL02_File = reader["AL02_File"] as int? ?? 0,

                        AL01_Accured = reader["AL01_Accured"] as decimal? ?? 0,
                        AL01_Leave = reader["AL01_Leave"] as decimal? ?? 0,
                        AL01_Balance = reader["AL01_Balance"] as decimal? ?? 0,
                        AL01_File = reader["AL01_File"] as int? ?? 0,

                        CP03_Accured = reader["CP03_Accured"] as decimal? ?? 0,
                        CP03_Leave = reader["CP03_Leave"] as decimal? ?? 0,
                        CP03_Balance = reader["CP03_Balance"] as decimal? ?? 0,
                        CP03_File = reader["CP03_File"] as int? ?? 0,

                        SL01_Accured = reader["SL01_Accured"] as decimal? ?? 0,
                        SL01_Leave = reader["SL01_Leave"] as decimal? ?? 0,
                        SL01_Balance = reader["SL01_Balance"] as decimal? ?? 0,
                        SL01_File = reader["SL01_File"] as int? ?? 0,

                        BU04_Accured = reader["BU04_Accured"] as decimal? ?? 0,
                        BU04_Leave = reader["BU04_Leave"] as decimal? ?? 0,
                        BU04_Balance = reader["BU04_Balance"] as decimal? ?? 0,
                        BU04_File = reader["BU04_File"] as int? ?? 0,

                        OT13_Accured = reader["OT13_Accured"] as decimal? ?? 0,
                        OT13_Leave = reader["OT13_Leave"] as decimal? ?? 0,
                        OT13_Balance = reader["OT13_Balance"] as decimal? ?? 0,
                        OT13_File = reader["OT13_File"] as int? ?? 0,

                        OL07_Accured = reader["OL07_Accured"] as decimal? ?? 0,
                        OL07_Leave = reader["OL07_Leave"] as decimal? ?? 0,
                        OL07_Balance = reader["OL07_Balance"] as decimal? ?? 0,
                        OL07_File = reader["OL07_File"] as int? ?? 0,

                        ML06_Accured = reader["ML06_Accured"] as decimal? ?? 0,
                        ML06_Leave = reader["ML06_Leave"] as decimal? ?? 0,
                        ML06_Balance = reader["ML06_Balance"] as decimal? ?? 0,
                        ML06_File = reader["ML06_File"] as int? ?? 0,

                        CO11_Accured = reader["CO11_Accured"] as decimal? ?? 0,
                        CO11_Leave = reader["CO11_Leave"] as decimal? ?? 0,
                        CO11_Balance = reader["CO11_Balance"] as decimal? ?? 0,
                        CO11_File = reader["CO11_File"] as int? ?? 0,

                        BL10_Accured = reader["BL10_Accured"] as decimal? ?? 0,
                        BL10_Leave = reader["BL10_Leave"] as decimal? ?? 0,
                        BL10_Balance = reader["BL10_Balance"] as decimal? ?? 0,
                        BL10_File = reader["BL10_File"] as int? ?? 0,

                        GL09_Accured = reader["GL09_Accured"] as decimal? ?? 0,
                        GL09_Leave = reader["GL09_Leave"] as decimal? ?? 0,
                        GL09_Balance = reader["GL09_Balance"] as decimal? ?? 0,
                        GL09_File = reader["GL09_File"] as int? ?? 0,

                        MS08_Accured = reader["MS08_Accured"] as decimal? ?? 0,
                        MS08_Leave = reader["MS08_Leave"] as decimal? ?? 0,
                        MS08_Balance = reader["MS08_Balance"] as decimal? ?? 0,
                        MS08_File = reader["MS08_File"] as int? ?? 0,

                        WFH_Approve = reader["WFH_Approve"] as decimal? ?? 0,
                        WFH_Waiting = reader["WFH_Waiting"] as decimal? ?? 0,
                        OffSite_Approve = reader["OffSite_Approve"] as decimal? ?? 0,
                        OffSite_Waiting = reader["OffSite_Waiting"] as decimal? ?? 0,

                        SL02_Accured = reader["SL02_Accured"] as decimal? ?? 0,
                        SL02_Leave = reader["SL02_Leave"] as decimal? ?? 0,
                        SL02_Balance = reader["SL02_Balance"] as decimal? ?? 0,
                        SL02_File = reader["SL02_File"] as int? ?? 0,
                    });

                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            LoadDepartments();
            sw.Stop();
            var elapased = sw.Elapsed;
            Debug.WriteLine("Time Data: " + elapased.ToString());
            Console.WriteLine("Time Data: " + elapased.ToString());
            return View(Report_leav_bal);
        }
        [HttpPost]
        public ActionResult ReportLeaveBalanceMonthSearch(string Company, string Department, string StartDate, string EndDate)
        {
            string emp = string.Empty;
            string EmpType = string.Empty;
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;

            DateTime firstDayOfMonth;
            DateTime lastDayOfMonth;
            string formatStartDate = string.Empty;
            string formatEndDate = string.Empty;
            var Report_leav_bal = new List<ReportLeaveBalance>();
            var connectionString = ConfigurationManager.ConnectionStrings["HRIS_DB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            if (Session["EmpId"] != null)
            {
                emp = Session["EmpId"].ToString();
                EmpType = Session["UserType"].ToString();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            //formateDate to dd/mm/yyyy
            //start
            if (!string.IsNullOrEmpty(StartDate))
            {
                if (DateTime.TryParseExact(StartDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dateST))
                {
                    formatStartDate = dateST.ToString("yyyy/MM/dd");
                }
            }
            else
            {
                firstDayOfMonth = new DateTime(year, month, 1);
                formatStartDate = firstDayOfMonth.ToString("yyyy/MM/dd");
            }

            //end
            if (!string.IsNullOrEmpty(EndDate))
            {
                if (DateTime.TryParseExact(EndDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dateEN))
                {
                    formatEndDate = dateEN.ToString("yyyy/MM/dd");
                }
            }
            else
            {
                lastDayOfMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                formatEndDate = lastDayOfMonth.ToString("yyyy/MM/dd");
            }
            conn.Open();
            try
            {
                //Report_leav_bal = new GetReportLeaveBalanceMonth().GetReportMonth(Company, Department, formatStartDate, formatEndDate);
                var cmd = new SqlCommand("P_Report_Leave_Balance_month", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@inComp", Company);
                cmd.Parameters.AddWithValue("@inDept", Department);
                cmd.Parameters.AddWithValue("@inStartdate", formatStartDate);
                cmd.Parameters.AddWithValue("@inEnddate", formatEndDate);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Report_leav_bal.Add(new ReportLeaveBalance
                    {
                        Company = reader["Company"]?.ToString() ?? "",
                        DeptNameShort = reader["DeptNameShort"]?.ToString() ?? "",
                        DeptName = reader["DeptName"]?.ToString() ?? "",
                        EmpId = reader["EmpId"]?.ToString() ?? "",
                        Position = reader["Position"]?.ToString() ?? "",
                        EmpStatus = reader["EmpStatus"]?.ToString() ?? "",
                        StartDate = reader["StartDate"]?.ToString() ?? "",
                        Fullname = reader["Fullname"]?.ToString() ?? "",
                        Years = reader["Years"]?.ToString() ?? "",
                        Months = reader["Months"]?.ToString() ?? "",
                        Days = reader["Days"]?.ToString() ?? "",

                        AL02_Accured = reader["AL02_Accured"] as decimal? ?? 0,
                        AL02_Leave = reader["AL02_Leave"] as decimal? ?? 0,
                        AL02_Balance = reader["AL02_Balance"] as decimal? ?? 0,
                        AL02_File = reader["AL02_File"] as int? ?? 0,

                        AL01_Accured = reader["AL01_Accured"] as decimal? ?? 0,
                        AL01_Leave = reader["AL01_Leave"] as decimal? ?? 0,
                        AL01_Balance = reader["AL01_Balance"] as decimal? ?? 0,
                        AL01_File = reader["AL01_File"] as int? ?? 0,

                        CP03_Accured = reader["CP03_Accured"] as decimal? ?? 0,
                        CP03_Leave = reader["CP03_Leave"] as decimal? ?? 0,
                        CP03_Balance = reader["CP03_Balance"] as decimal? ?? 0,
                        CP03_File = reader["CP03_File"] as int? ?? 0,

                        SL01_Accured = reader["SL01_Accured"] as decimal? ?? 0,
                        SL01_Leave = reader["SL01_Leave"] as decimal? ?? 0,
                        SL01_Balance = reader["SL01_Balance"] as decimal? ?? 0,
                        SL01_File = reader["SL01_File"] as int? ?? 0,

                        BU04_Accured = reader["BU04_Accured"] as decimal? ?? 0,
                        BU04_Leave = reader["BU04_Leave"] as decimal? ?? 0,
                        BU04_Balance = reader["BU04_Balance"] as decimal? ?? 0,
                        BU04_File = reader["BU04_File"] as int? ?? 0,

                        OT13_Accured = reader["OT13_Accured"] as decimal? ?? 0,
                        OT13_Leave = reader["OT13_Leave"] as decimal? ?? 0,
                        OT13_Balance = reader["OT13_Balance"] as decimal? ?? 0,
                        OT13_File = reader["OT13_File"] as int? ?? 0,

                        OL07_Accured = reader["OL07_Accured"] as decimal? ?? 0,
                        OL07_Leave = reader["OL07_Leave"] as decimal? ?? 0,
                        OL07_Balance = reader["OL07_Balance"] as decimal? ?? 0,
                        OL07_File = reader["OL07_File"] as int? ?? 0,

                        ML06_Accured = reader["ML06_Accured"] as decimal? ?? 0,
                        ML06_Leave = reader["ML06_Leave"] as decimal? ?? 0,
                        ML06_Balance = reader["ML06_Balance"] as decimal? ?? 0,
                        ML06_File = reader["ML06_File"] as int? ?? 0,

                        CO11_Accured = reader["CO11_Accured"] as decimal? ?? 0,
                        CO11_Leave = reader["CO11_Leave"] as decimal? ?? 0,
                        CO11_Balance = reader["CO11_Balance"] as decimal? ?? 0,
                        CO11_File = reader["CO11_File"] as int? ?? 0,

                        BL10_Accured = reader["BL10_Accured"] as decimal? ?? 0,
                        BL10_Leave = reader["BL10_Leave"] as decimal? ?? 0,
                        BL10_Balance = reader["BL10_Balance"] as decimal? ?? 0,
                        BL10_File = reader["BL10_File"] as int? ?? 0,

                        GL09_Accured = reader["GL09_Accured"] as decimal? ?? 0,
                        GL09_Leave = reader["GL09_Leave"] as decimal? ?? 0,
                        GL09_Balance = reader["GL09_Balance"] as decimal? ?? 0,
                        GL09_File = reader["GL09_File"] as int? ?? 0,

                        MS08_Accured = reader["MS08_Accured"] as decimal? ?? 0,
                        MS08_Leave = reader["MS08_Leave"] as decimal? ?? 0,
                        MS08_Balance = reader["MS08_Balance"] as decimal? ?? 0,
                        MS08_File = reader["MS08_File"] as int? ?? 0,

                        WFH_Approve = reader["WFH_Approve"] as decimal? ?? 0,
                        WFH_Waiting = reader["WFH_Waiting"] as decimal? ?? 0,
                        OffSite_Approve = reader["OffSite_Approve"] as decimal? ?? 0,
                        OffSite_Waiting = reader["OffSite_Waiting"] as decimal? ?? 0,

                        SL02_Accured = reader["SL02_Accured"] as decimal? ?? 0,
                        SL02_Leave = reader["SL02_Leave"] as decimal? ?? 0,
                        SL02_Balance = reader["SL02_Balance"] as decimal? ?? 0,
                        SL02_File = reader["SL02_File"] as int? ?? 0,
                    });

                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            LoadDepartments();
            return View("ReportLeaveBalanceMonth", Report_leav_bal);
        }

        public ActionResult ReportLeaveLateMonth()
        {
            string emp = string.Empty;
            string EmpType = string.Empty;
            if (Session["EmpId"] != null)
            {
                emp = Session["EmpId"].ToString();
                EmpType = Session["UserType"].ToString();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

            LoadDepartments();
            return View();
        }
        [HttpPost]
        public ActionResult ReportLeaveLateMonth(string Company, string Department, string Month, string Year)
        {
            string emp = string.Empty;
            string EmpType = string.Empty;
            if (Session["EmpId"] != null)
            {
                emp = Session["EmpId"].ToString();
                EmpType = Session["UserType"].ToString();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

            LoadDepartments();
            return View();
        }

        //Detail Report Year
        public JsonResult ReportDetail(string EMP, string LEVTYP, string YEAR)
        {
            var Detail = new List<DetailReport>();
            string message = string.Empty;
            try
            {
                Detail = new GetDetailReport().Get(EMP, LEVTYP, YEAR);
                foreach (var item in Detail)
                {
                    item.CalculatedHour = Eleave.Library.Utils.CalculateDayHour((double)item.NumDay);
                }
                message = "Y";
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            //return Json(new { message = message, Detail }, JsonRequestBehavior.AllowGet);
            return new JsonResult
            {
                Data = new { message = message, Detail },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };

        }
        //Detail Report Month
        public JsonResult ReportDetailMonth(string EmpID, string LeavTyp, string StartDate, string EndDate)
        {
            string message = string.Empty;
            var Detail = new List<DetailReport>();
            try
            {
                Detail = new GetDetailReportMonth().GetDetail(EmpID, LeavTyp, StartDate, EndDate);
                foreach (var item in Detail)
                {
                    item.CalculatedHour = Eleave.Library.Utils.CalculateDayHour((double)item.NumDay);
                }
                message = "Y";
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            //return Json(new { message = message, Detail }, JsonRequestBehavior.AllowGet);
            return new JsonResult
            {
                Data = new { message = message, Detail },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };
        }

        public JsonResult DetailReportMonthSheet(string ReqStart, string ReqEnd, string Dept, string Comp)
        {
            string message = string.Empty;
            string GetEmp = string.Empty;
            DateTime? startDate = null;
            DateTime? endDate = null;
            // แปลง ReqStart เป็น DateTime 
            if (!string.IsNullOrEmpty(ReqStart))
            {
                if (DateTime.TryParseExact(ReqStart, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedStartDate))
                {
                    startDate = parsedStartDate;
                }
            }

            // แปลง ReqEnd เป็น DateTime 
            if (!string.IsNullOrEmpty(ReqEnd))
            {
                if (DateTime.TryParseExact(ReqEnd, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedEndDate))
                {
                    endDate = parsedEndDate;
                }
            }
            if (Session["EmpId"] != null)
            {
                GetEmp = Session["EmpId"].ToString();
            }
            var Detail = new List<ReportLeaveBalanceDetail>();
            try
            {
                Detail = new GetDataSheetReport().GetAck(GetEmp, Comp, "", "", "1", startDate, endDate, Dept, "", "", "0");

                foreach (var item in Detail)
                {
                    if (!string.IsNullOrEmpty(item.ApproveBy))
                    {
                        var approvers = item.ApproveBy.Split(',');
                        var approverList = new List<string>();

                        foreach (var approver in approvers)
                        {
                            item.NumDayCal = Eleave.Library.Utils.CalculateDayHour((double)item.NumDay);
                            if (approver.Contains("|"))
                            {
                                var parts = approver.Split('|');
                                var status = parts[0].Trim();
                                var name = parts.Length > 1 ? parts[1].Trim() : string.Empty;

                                // แปลงค่า Status เป็นสัญลักษณ์
                                string icon;
                                switch (status)
                                {
                                    case "0":
                                        icon = "↻"; // Repeat
                                        break;
                                    case "1":
                                        icon = "✔"; // Success
                                        break;
                                    case "2":
                                        icon = "✖"; // Error
                                        break;
                                    case "3":
                                        icon = "✔"; // Success (อีกแบบ)
                                        break;
                                    case "4":
                                        icon = "⊗"; // Cancel
                                        break;
                                    default:
                                        icon = "?";
                                        break;
                                }
                                approverList.Add($"{icon} {name}");
                            }
                        }
                        item.ApproveBy = string.Join(", ", approverList);
                    }
                }
                message = "Y";
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine(ex.Message);
            }
            //return Json(new { message = message, Detail }, JsonRequestBehavior.AllowGet);
            return new JsonResult
            {
                Data = new { message = message, Detail },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };
        }

        public JsonResult GetLeavelateJson(string Company, string Dept, string Month, string Year = "")
        {
            string message;
            var Report = new List<ReportLeaveLate>();

            try
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["HRIS_DB"].ConnectionString))
                using (var cmd = new SqlCommand("P_Report_Leave_Attend", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@inCompany", Company);
                    cmd.Parameters.AddWithValue("@inDept", Dept);
                    cmd.Parameters.AddWithValue("@inMonth", Month);
                    cmd.Parameters.AddWithValue("@inYear", Year);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Report.Add(new ReportLeaveLate
                            {
                                KEYMAP = reader["KEYMAP"] != DBNull.Value ? reader["KEYMAP"].ToString() : string.Empty,
                                Company = reader["Company"] != DBNull.Value ? reader["Company"].ToString() : string.Empty,
                                EmpId = reader["EmpId"] != DBNull.Value ? reader["EmpId"].ToString() : string.Empty,
                                EmpName = reader["EmpName"] != DBNull.Value ? reader["EmpName"].ToString() : string.Empty,
                                DateMonth = reader["DateMonth"] != DBNull.Value ? reader["DateMonth"].ToString() : string.Empty,
                                Attend = reader["Attend"] != DBNull.Value ? reader["Attend"].ToString() : string.Empty,
                                Finish = reader["Finish"] != DBNull.Value ? reader["Finish"].ToString() : string.Empty,
                                Late = reader["Late"] != DBNull.Value ? reader["Late"].ToString() : string.Empty,
                                BeforeW = reader["BeforeW"] != DBNull.Value ? reader["BeforeW"].ToString() : string.Empty,
                                Missing = reader["Missing"] != DBNull.Value ? reader["Missing"].ToString() : string.Empty,
                                AttachFile = reader["AttachFile"] != DBNull.Value ? reader["AttachFile"].ToString() : string.Empty,
                                Exception = reader["Exception"] != DBNull.Value ? reader["Exception"].ToString() : string.Empty,
                                Department = reader["Department"] != DBNull.Value ? reader["Department"].ToString() : string.Empty,
                                Total_late = reader["Total_late"] != DBNull.Value ? reader["Total_late"].ToString() : string.Empty,
                                Late_work = reader["Late_work"] != DBNull.Value ? reader["Late_work"].ToString() : string.Empty,
                                Missing_work = reader["Missing_work"] != DBNull.Value ? reader["Missing_work"].ToString() : string.Empty,
                                AnnualLeave = reader["AnnualLeave"] != DBNull.Value ? reader["AnnualLeave"].ToString() : string.Empty,
                                CompensateLeave = reader["CompensateLeave"] != DBNull.Value ? reader["CompensateLeave"].ToString() : string.Empty,
                                BusinessLeave = reader["BusinessLeave"] != DBNull.Value ? reader["BusinessLeave"].ToString() : string.Empty,
                                SickLeave = reader["SickLeave"] != DBNull.Value ? reader["SickLeave"].ToString() : string.Empty,
                                BereavementLeave = reader["BereavementLeave"] != DBNull.Value ? reader["BereavementLeave"].ToString() : string.Empty,
                                ContraceptiveLeave = reader["ContraceptiveLeave"] != DBNull.Value ? reader["ContraceptiveLeave"].ToString() : string.Empty,
                                GraduationLeave = reader["GraduationLeave"] != DBNull.Value ? reader["GraduationLeave"].ToString() : string.Empty,
                                MaternityLeave = reader["MaternityLeave"] != DBNull.Value ? reader["MaternityLeave"].ToString() : string.Empty,
                                MilitaryLeave = reader["MilitaryLeave"] != DBNull.Value ? reader["MilitaryLeave"].ToString() : string.Empty,
                                OrdinationLeave = reader["OrdinationLeave"] != DBNull.Value ? reader["OrdinationLeave"].ToString() : string.Empty,
                                ProfessionalLeave = reader["ProfessionalLeave"] != DBNull.Value ? reader["ProfessionalLeave"].ToString() : string.Empty,
                                OtherLeave = reader["OtherLeave"] != DBNull.Value ? reader["OtherLeave"].ToString() : string.Empty,
                                SickLeaveWithoutPay = reader["SickLeaveWithoutPay"] != DBNull.Value ? reader["SickLeaveWithoutPay"].ToString() : string.Empty,
                                WFH = reader["WFH"] != DBNull.Value ? reader["WFH"].ToString() : string.Empty,
                                WFS = reader["WFS"] != DBNull.Value ? reader["WFS"].ToString() : string.Empty
                            });
                        }
                    }
                }

                message = "Y";
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return new JsonResult
            {
                Data = new { message, Report },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue // รองรับ JSON ที่มีขนาดใหญ่
            };
        }


        //public JsonResult GetLeavelateJson(string Company, string Dept, string Month)
        //{
        //    string message = string.Empty;
        //    var connectionString = ConfigurationManager.ConnectionStrings["HRIS_DB"].ConnectionString;
        //    SqlConnection conn = new SqlConnection(connectionString);
        //    var Report = new List<ReportLeaveLate>();
        //    try
        //    {
        //        Report = new GetReportLeaveLate().GetReports(Company, Dept, Month);
        //        conn.Open();
        //        var cmd = new SqlCommand("P_Report_Leave_Attend", conn);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@inCompany", Company);
        //        cmd.Parameters.AddWithValue("@inDept", Dept);
        //        cmd.Parameters.AddWithValue("@inMonth", Month);
        //        SqlDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            Report.Add(new ReportLeaveLate
        //            {
        //                Company = reader["Company"] != DBNull.Value ? reader["Company"].ToString() : string.Empty,
        //                EmpId = reader["EmpId"] != DBNull.Value ? reader["EmpId"].ToString() : string.Empty,
        //                EmpName = reader["EmpName"] != DBNull.Value ? reader["EmpName"].ToString() : string.Empty,
        //                DateMonth = reader["DateMonth"] != DBNull.Value ? reader["DateMonth"].ToString() : string.Empty,
        //                Attend = reader["Attend"] != DBNull.Value ? reader["Attend"].ToString() : string.Empty,
        //                Finish = reader["Finish"] != DBNull.Value ? reader["Finish"].ToString() : string.Empty,
        //                Late = reader["Late"] != DBNull.Value ? reader["Late"].ToString() : string.Empty,
        //                BeforeW = reader["BeforeW"] != DBNull.Value ? reader["BeforeW"].ToString() : string.Empty,
        //                Missing = reader["Missing"] != DBNull.Value ? reader["Missing"].ToString() : string.Empty,
        //                Exception = reader["Exception"] != DBNull.Value ? reader["Exception"].ToString() : string.Empty,
        //                Department = reader["Department"] != DBNull.Value ? reader["Department"].ToString() : string.Empty,
        //                Total_late = reader["Total_late"] != DBNull.Value ? reader["Total_late"].ToString() : string.Empty,
        //                Late_work = reader["Late_work"] != DBNull.Value ? reader["Late_work"].ToString() : string.Empty,
        //                Missing_work = reader["Missing_work"] != DBNull.Value ? reader["Missing_work"].ToString() : string.Empty,
        //                AnnualLeave = reader["AnnualLeave"] != DBNull.Value ? reader["AnnualLeave"].ToString() : string.Empty,
        //                CompensateLeave = reader["CompensateLeave"] != DBNull.Value ? reader["CompensateLeave"].ToString() : string.Empty,
        //                BusinessLeave = reader["BusinessLeave"] != DBNull.Value ? reader["BusinessLeave"].ToString() : string.Empty,
        //                SickLeave = reader["SickLeave"] != DBNull.Value ? reader["SickLeave"].ToString() : string.Empty,
        //                BereavementLeave = reader["BereavementLeave"] != DBNull.Value ? reader["BereavementLeave"].ToString() : string.Empty,
        //                ContraceptiveLeave = reader["ContraceptiveLeave"] != DBNull.Value ? reader["ContraceptiveLeave"].ToString() : string.Empty,
        //                GraduationLeave = reader["GraduationLeave"] != DBNull.Value ? reader["GraduationLeave"].ToString() : string.Empty,
        //                MaternityLeave = reader["MaternityLeave"] != DBNull.Value ? reader["MaternityLeave"].ToString() : string.Empty,
        //                MilitaryLeave = reader["MilitaryLeave"] != DBNull.Value ? reader["MilitaryLeave"].ToString() : string.Empty,
        //                OrdinationLeave = reader["OrdinationLeave"] != DBNull.Value ? reader["OrdinationLeave"].ToString() : string.Empty,
        //                ProfessionalLeave = reader["ProfessionalLeave"] != DBNull.Value ? reader["ProfessionalLeave"].ToString() : string.Empty,
        //                OtherLeave = reader["OtherLeave"] != DBNull.Value ? reader["OtherLeave"].ToString() : string.Empty,
        //                WFH = reader["WFH"] != DBNull.Value ? reader["WFH"].ToString() : string.Empty,
        //                WFS = reader["WFS"] != DBNull.Value ? reader["WFS"].ToString() : string.Empty
        //            });
        //        }
        //        message = "Y";
        //    }
        //    catch (Exception ex)
        //    {
        //        message = ex.Message;
        //    }
        //    return Json(new { message = message, Report }, JsonRequestBehavior.AllowGet);

        //}

        public JsonResult DetailReportYearSheet(string Yearly, string Dept, string EmpID, string Comp)
        {
            string message = string.Empty;
            string GetEmp = string.Empty;
            // แปลง ReqStart เป็น DateTime 
            int GetYear = int.Parse(Yearly);
            DateTime firstDay = new DateTime(GetYear, 1, 1);
            DateTime lastDay = new DateTime(GetYear, 12, 31);


            var Detail = new List<ReportLeaveBalanceDetail>();
            if (Session["EmpId"] != null)
            {
                GetEmp = Session["EmpId"].ToString();
            }
            try
            {
                Detail = new GetDataSheetReport().GetAck(GetEmp, Comp, "", "", "1", firstDay, lastDay, Dept, "", EmpID, "0");

                foreach (var item in Detail)
                {
                    if (!string.IsNullOrEmpty(item.ApproveBy))
                    {
                        var approvers = item.ApproveBy.Split(',');
                        var approverList = new List<string>();

                        foreach (var approver in approvers)
                        {
                            item.NumDayCal = Eleave.Library.Utils.CalculateDayHour((double)item.NumDay);
                            if (approver.Contains("|"))
                            {
                                var parts = approver.Split('|');
                                var status = parts[0].Trim();
                                var name = parts.Length > 1 ? parts[1].Trim() : string.Empty;

                                // แปลงค่า Status เป็นสัญลักษณ์
                                string icon;
                                switch (status)
                                {
                                    case "0":
                                        icon = "↻"; // Repeat
                                        break;
                                    case "1":
                                        icon = "✔"; // Success
                                        break;
                                    case "2":
                                        icon = "✖"; // Error
                                        break;
                                    case "3":
                                        icon = "✔"; // Success (อีกแบบ)
                                        break;
                                    case "4":
                                        icon = "⊗"; // Cancel
                                        break;
                                    default:
                                        icon = "?";
                                        break;
                                }
                                approverList.Add($"{icon} {name}");
                            }
                        }
                        item.ApproveBy = string.Join(", ", approverList);
                    }
                }
                message = "Y";
            }
            catch (Exception ex)
            {
                message = ex.Message;
                // Handle exceptions
                Console.WriteLine(ex.Message);
            }
            //return Json(new { message = message, Detail }, JsonRequestBehavior.AllowGet);
            return new JsonResult
            {
                Data = new { message = message, Detail },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };
        }

        //ReportManager
        public ActionResult ReportApprover()
        {
            string EmpID = string.Empty;
            string EmpType = string.Empty;
            var connectionString = ConfigurationManager.ConnectionStrings["HRIS_DB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            if (Session["EmpId"] != null)
            {
                EmpID = Session["EmpId"].ToString();
                EmpType = Session["UserType"].ToString();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

            if (EmpType == "1")
            {
                return RedirectToAction("Index", "Home");
            }

            var Report_leav_bal = new List<ReportLeaveBalance>();
            conn.Open();
            try
            {
                //Report_leav_bal = new GetReportLeaveBalance().GetReportLeaveBalances("", DateTime.Now.Year.ToString(), "", "", EmpID);
                var cmd = new SqlCommand("P_Report_Leave_Balance_Manager", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@inComp", "");
                cmd.Parameters.AddWithValue("@inDept", "");
                cmd.Parameters.AddWithValue("@inYear", DateTime.Now.Year.ToString());
                cmd.Parameters.AddWithValue("@inEmpId", "");
                cmd.Parameters.AddWithValue("@inUser", EmpID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Report_leav_bal.Add(new ReportLeaveBalance
                    {
                        Company = reader["Company"]?.ToString() ?? "",
                        LeaveYear = reader["LeaveYear"]?.ToString() ?? "",
                        DeptNameShort = reader["DeptNameShort"]?.ToString() ?? "",
                        DeptName = reader["DeptName"]?.ToString() ?? "",
                        EmpId = reader["EmpId"]?.ToString() ?? "",
                        Position = reader["Position"]?.ToString() ?? "",
                        EmpStatus = reader["EmpStatus"]?.ToString() ?? "",
                        StartDate = reader["StartDate"]?.ToString() ?? "",
                        Fullname = reader["Fullname"]?.ToString() ?? "",
                        Years = reader["Years"]?.ToString() ?? "",
                        Months = reader["Months"]?.ToString() ?? "",
                        Days = reader["Days"]?.ToString() ?? "",

                        AL02_Accured = reader["AL02_Accured"] as decimal? ?? 0,
                        AL02_Leave = reader["AL02_Leave"] as decimal? ?? 0,
                        AL02_Balance = reader["AL02_Balance"] as decimal? ?? 0,
                        AL02_File = reader["AL02_File"] as int? ?? 0,

                        AL01_Accured = reader["AL01_Accured"] as decimal? ?? 0,
                        AL01_Leave = reader["AL01_Leave"] as decimal? ?? 0,
                        AL01_Balance = reader["AL01_Balance"] as decimal? ?? 0,
                        AL01_File = reader["AL01_File"] as int? ?? 0,

                        CP03_Accured = reader["CP03_Accured"] as decimal? ?? 0,
                        CP03_Leave = reader["CP03_Leave"] as decimal? ?? 0,
                        CP03_Balance = reader["CP03_Balance"] as decimal? ?? 0,
                        CP03_File = reader["CP03_File"] as int? ?? 0,

                        SL01_Accured = reader["SL01_Accured"] as decimal? ?? 0,
                        SL01_Leave = reader["SL01_Leave"] as decimal? ?? 0,
                        SL01_Balance = reader["SL01_Balance"] as decimal? ?? 0,
                        SL01_File = reader["SL01_File"] as int? ?? 0,

                        BU04_Accured = reader["BU04_Accured"] as decimal? ?? 0,
                        BU04_Leave = reader["BU04_Leave"] as decimal? ?? 0,
                        BU04_Balance = reader["BU04_Balance"] as decimal? ?? 0,
                        BU04_File = reader["BU04_File"] as int? ?? 0,

                        OT13_Accured = reader["OT13_Accured"] as decimal? ?? 0,
                        OT13_Leave = reader["OT13_Leave"] as decimal? ?? 0,
                        OT13_Balance = reader["OT13_Balance"] as decimal? ?? 0,
                        OT13_File = reader["OT13_File"] as int? ?? 0,

                        OL07_Accured = reader["OL07_Accured"] as decimal? ?? 0,
                        OL07_Leave = reader["OL07_Leave"] as decimal? ?? 0,
                        OL07_Balance = reader["OL07_Balance"] as decimal? ?? 0,
                        OL07_File = reader["OL07_File"] as int? ?? 0,

                        ML06_Accured = reader["ML06_Accured"] as decimal? ?? 0,
                        ML06_Leave = reader["ML06_Leave"] as decimal? ?? 0,
                        ML06_Balance = reader["ML06_Balance"] as decimal? ?? 0,
                        ML06_File = reader["ML06_File"] as int? ?? 0,

                        CO11_Accured = reader["CO11_Accured"] as decimal? ?? 0,
                        CO11_Leave = reader["CO11_Leave"] as decimal? ?? 0,
                        CO11_Balance = reader["CO11_Balance"] as decimal? ?? 0,
                        CO11_File = reader["CO11_File"] as int? ?? 0,

                        BL10_Accured = reader["BL10_Accured"] as decimal? ?? 0,
                        BL10_Leave = reader["BL10_Leave"] as decimal? ?? 0,
                        BL10_Balance = reader["BL10_Balance"] as decimal? ?? 0,
                        BL10_File = reader["BL10_File"] as int? ?? 0,

                        GL09_Accured = reader["GL09_Accured"] as decimal? ?? 0,
                        GL09_Leave = reader["GL09_Leave"] as decimal? ?? 0,
                        GL09_Balance = reader["GL09_Balance"] as decimal? ?? 0,
                        GL09_File = reader["GL09_File"] as int? ?? 0,

                        MS08_Accured = reader["MS08_Accured"] as decimal? ?? 0,
                        MS08_Leave = reader["MS08_Leave"] as decimal? ?? 0,
                        MS08_Balance = reader["MS08_Balance"] as decimal? ?? 0,
                        MS08_File = reader["MS08_File"] as int? ?? 0,

                        WFH_Approve = reader["WFH_Approve"] as decimal? ?? 0,
                        WFH_Waiting = reader["WFH_Waiting"] as decimal? ?? 0,
                        OffSite_Approve = reader["OffSite_Approve"] as decimal? ?? 0,
                        OffSite_Waiting = reader["OffSite_Waiting"] as decimal? ?? 0,

                        SL02_Accured = reader["SL02_Accured"] as decimal? ?? 0,
                        SL02_Leave = reader["SL02_Leave"] as decimal? ?? 0,
                        SL02_Balance = reader["SL02_Balance"] as decimal? ?? 0,
                        SL02_File = reader["SL02_File"] as int? ?? 0,
                    });

                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            finally
            {
                conn.Close();
            }


            LoadDepartments();
            return View(Report_leav_bal);
        }
        [HttpPost]
        public ActionResult SearchReportApprover(string Company, string Year, string Department, string EmpID)
        {
            string emp = string.Empty;
            string EmpType = string.Empty;
            var connectionString = ConfigurationManager.ConnectionStrings["HRIS_DB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            var Report_leav_bal = new List<ReportLeaveBalance>();
            if (Session["EmpId"] != null)
            {
                emp = Session["EmpId"].ToString();
                EmpType = Session["UserType"].ToString();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

            if (EmpType == "1")
            {
                return RedirectToAction("Index", "Home");
            }
            conn.Open();
            try
            {
                //Report_leav_bal = new GetReportLeaveBalance().GetReportLeaveBalances(Company, Year, Department, EmpID, emp);
                var cmd = new SqlCommand("P_Report_Leave_Balance_Manager", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@inComp", Company);
                cmd.Parameters.AddWithValue("@inDept", Department);
                cmd.Parameters.AddWithValue("@inYear", Year);
                cmd.Parameters.AddWithValue("@inEmpId", EmpID);
                cmd.Parameters.AddWithValue("@inUser", emp);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Report_leav_bal.Add(new ReportLeaveBalance
                    {
                        Company = reader["Company"]?.ToString() ?? "",
                        LeaveYear = reader["LeaveYear"]?.ToString() ?? "",
                        DeptNameShort = reader["DeptNameShort"]?.ToString() ?? "",
                        DeptName = reader["DeptName"]?.ToString() ?? "",
                        EmpId = reader["EmpId"]?.ToString() ?? "",
                        Position = reader["Position"]?.ToString() ?? "",
                        EmpStatus = reader["EmpStatus"]?.ToString() ?? "",
                        StartDate = reader["StartDate"]?.ToString() ?? "",
                        Fullname = reader["Fullname"]?.ToString() ?? "",
                        Years = reader["Years"]?.ToString() ?? "",
                        Months = reader["Months"]?.ToString() ?? "",
                        Days = reader["Days"]?.ToString() ?? "",

                        AL02_Accured = reader["AL02_Accured"] as decimal? ?? 0,
                        AL02_Leave = reader["AL02_Leave"] as decimal? ?? 0,
                        AL02_Balance = reader["AL02_Balance"] as decimal? ?? 0,
                        AL02_File = reader["AL02_File"] as int? ?? 0,

                        AL01_Accured = reader["AL01_Accured"] as decimal? ?? 0,
                        AL01_Leave = reader["AL01_Leave"] as decimal? ?? 0,
                        AL01_Balance = reader["AL01_Balance"] as decimal? ?? 0,
                        AL01_File = reader["AL01_File"] as int? ?? 0,

                        CP03_Accured = reader["CP03_Accured"] as decimal? ?? 0,
                        CP03_Leave = reader["CP03_Leave"] as decimal? ?? 0,
                        CP03_Balance = reader["CP03_Balance"] as decimal? ?? 0,
                        CP03_File = reader["CP03_File"] as int? ?? 0,

                        SL01_Accured = reader["SL01_Accured"] as decimal? ?? 0,
                        SL01_Leave = reader["SL01_Leave"] as decimal? ?? 0,
                        SL01_Balance = reader["SL01_Balance"] as decimal? ?? 0,
                        SL01_File = reader["SL01_File"] as int? ?? 0,

                        BU04_Accured = reader["BU04_Accured"] as decimal? ?? 0,
                        BU04_Leave = reader["BU04_Leave"] as decimal? ?? 0,
                        BU04_Balance = reader["BU04_Balance"] as decimal? ?? 0,
                        BU04_File = reader["BU04_File"] as int? ?? 0,

                        OT13_Accured = reader["OT13_Accured"] as decimal? ?? 0,
                        OT13_Leave = reader["OT13_Leave"] as decimal? ?? 0,
                        OT13_Balance = reader["OT13_Balance"] as decimal? ?? 0,
                        OT13_File = reader["OT13_File"] as int? ?? 0,

                        OL07_Accured = reader["OL07_Accured"] as decimal? ?? 0,
                        OL07_Leave = reader["OL07_Leave"] as decimal? ?? 0,
                        OL07_Balance = reader["OL07_Balance"] as decimal? ?? 0,
                        OL07_File = reader["OL07_File"] as int? ?? 0,

                        ML06_Accured = reader["ML06_Accured"] as decimal? ?? 0,
                        ML06_Leave = reader["ML06_Leave"] as decimal? ?? 0,
                        ML06_Balance = reader["ML06_Balance"] as decimal? ?? 0,
                        ML06_File = reader["ML06_File"] as int? ?? 0,

                        CO11_Accured = reader["CO11_Accured"] as decimal? ?? 0,
                        CO11_Leave = reader["CO11_Leave"] as decimal? ?? 0,
                        CO11_Balance = reader["CO11_Balance"] as decimal? ?? 0,
                        CO11_File = reader["CO11_File"] as int? ?? 0,

                        BL10_Accured = reader["BL10_Accured"] as decimal? ?? 0,
                        BL10_Leave = reader["BL10_Leave"] as decimal? ?? 0,
                        BL10_Balance = reader["BL10_Balance"] as decimal? ?? 0,
                        BL10_File = reader["BL10_File"] as int? ?? 0,

                        GL09_Accured = reader["GL09_Accured"] as decimal? ?? 0,
                        GL09_Leave = reader["GL09_Leave"] as decimal? ?? 0,
                        GL09_Balance = reader["GL09_Balance"] as decimal? ?? 0,
                        GL09_File = reader["GL09_File"] as int? ?? 0,

                        MS08_Accured = reader["MS08_Accured"] as decimal? ?? 0,
                        MS08_Leave = reader["MS08_Leave"] as decimal? ?? 0,
                        MS08_Balance = reader["MS08_Balance"] as decimal? ?? 0,
                        MS08_File = reader["MS08_File"] as int? ?? 0,

                        WFH_Approve = reader["WFH_Approve"] as decimal? ?? 0,
                        WFH_Waiting = reader["WFH_Waiting"] as decimal? ?? 0,
                        OffSite_Approve = reader["OffSite_Approve"] as decimal? ?? 0,
                        OffSite_Waiting = reader["OffSite_Waiting"] as decimal? ?? 0,

                        SL02_Accured = reader["SL02_Accured"] as decimal? ?? 0,
                        SL02_Leave = reader["SL02_Leave"] as decimal? ?? 0,
                        SL02_Balance = reader["SL02_Balance"] as decimal? ?? 0,
                        SL02_File = reader["SL02_File"] as int? ?? 0,
                    });

                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            LoadDepartments();
            return View("ReportApprover", Report_leav_bal);
        }

        //Excel
        public JsonResult DetailReportManagerSheet(string Yearly, string Dept, string EmpID, string Comp)
        {
            string message = string.Empty;
            string GetEmp = string.Empty;
            // แปลง ReqStart เป็น DateTime 
            int GetYear = int.Parse(Yearly);
            DateTime firstDay = new DateTime(GetYear, 1, 1);
            DateTime lastDay = new DateTime(GetYear, 12, 31);


            var Detail = new List<ReportLeaveBalanceDetail>();
            if (Session["EmpId"] != null)
            {
                GetEmp = Session["EmpId"].ToString();
            }
            try
            {
                Detail = new GetDataSheetReport().GetBalanceManagerDetails(GetEmp, Comp, "", "", "1", firstDay, lastDay, Dept, "", EmpID, "0");

                foreach (var item in Detail)
                {
                    if (!string.IsNullOrEmpty(item.ApproveBy))
                    {
                        var approvers = item.ApproveBy.Split(',');
                        var approverList = new List<string>();

                        foreach (var approver in approvers)
                        {
                            item.NumDayCal = Eleave.Library.Utils.CalculateDayHour((double)item.NumDay);
                            if (approver.Contains("|"))
                            {
                                var parts = approver.Split('|');
                                var status = parts[0].Trim();
                                var name = parts.Length > 1 ? parts[1].Trim() : string.Empty;

                                // แปลงค่า Status เป็นสัญลักษณ์
                                string icon;
                                switch (status)
                                {
                                    case "0":
                                        icon = "↻"; // Repeat
                                        break;
                                    case "1":
                                        icon = "✔"; // Success
                                        break;
                                    case "2":
                                        icon = "✖"; // Error
                                        break;
                                    case "3":
                                        icon = "✔"; // Success (อีกแบบ)
                                        break;
                                    case "4":
                                        icon = "⊗"; // Cancel
                                        break;
                                    default:
                                        icon = "?";
                                        break;
                                }
                                approverList.Add($"{icon} {name}");
                            }
                        }
                        item.ApproveBy = string.Join(", ", approverList);
                    }
                }
                message = "Y";
            }
            catch (Exception ex)
            {
                message = ex.Message;
                // Handle exceptions
                Console.WriteLine(ex.Message);
            }
            return Json(new { message = message, Detail }, JsonRequestBehavior.AllowGet);
        }


        //LoadData
        private void LoadDepartments()
        {
            var departments = new List<StoreGetLookupData>();
            departments = new GetLookupData().GetLookupDataStore("DEPART");


            ViewBag.DepartmentList = departments;
        }

    }
}