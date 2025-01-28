using Eleave.Data;
using Eleave.Models;
using System;
using System.Collections.Generic;
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
            try
            {
                Report_leav_bal = new GetReportLeaveBalance().GetReportLeaveBalances("", DateTime.Now.Year.ToString(), "", "", EmpID);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }


            LoadDepartments();
            return View(Report_leav_bal);
        }
        [HttpPost]
        public ActionResult SearchReportLeaveBalance(string Company, string Year, string Department, string EmpID)
        {
            string emp = string.Empty;
            string EmpType = string.Empty;
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
            try
            {
                Report_leav_bal = new GetReportLeaveBalance().GetReportLeaveBalances(Company, Year, Department, EmpID, emp);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            LoadDepartments();
            return View("ReportLeaveBalance", Report_leav_bal);
        }
        public ActionResult ReportLeaveBalanceMonth()
        {
            string emp = string.Empty;
            string EmpType = string.Empty;
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



            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            // วันแรกของเดือน
            DateTime firstDayOfMonth = new DateTime(year, month, 1);

            // วันสุดท้ายของเดือน
            DateTime lastDayOfMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            Console.WriteLine("วันแรกของเดือน: " + firstDayOfMonth.ToString("yyyy-MM-dd"));
            Console.WriteLine("วันสุดท้ายของเดือน: " + lastDayOfMonth.ToString("yyyy-MM-dd"));




            try
            {
                Report_leav_bal = new GetReportLeaveBalanceMonth().GetReportMonth("", "", firstDayOfMonth.ToString("yyyy-MM-dd"), lastDayOfMonth.ToString("yyyy-MM-dd"));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            LoadDepartments();
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

            try
            {
                Report_leav_bal = new GetReportLeaveBalanceMonth().GetReportMonth(Company, Department, formatStartDate, formatEndDate);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            LoadDepartments();
            return View("ReportLeaveBalanceMonth", Report_leav_bal);
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
            return Json(new { message = message, Detail }, JsonRequestBehavior.AllowGet);
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
            return Json(new { message = message, Detail }, JsonRequestBehavior.AllowGet);
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
            return Json(new { message = message, Detail }, JsonRequestBehavior.AllowGet);
        }

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