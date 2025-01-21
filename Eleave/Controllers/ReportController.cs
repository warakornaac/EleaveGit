using Eleave.Data;
using Eleave.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        //LoadData
        private void LoadDepartments()
        {
            var departments = new List<StoreGetLookupData>();
            departments = new GetLookupData().GetLookupDataStore("DEPART");


            ViewBag.DepartmentList = departments;
        }
    }
}