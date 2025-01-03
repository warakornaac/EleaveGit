using Eleave.Data;
using Eleave.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
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
        public JsonResult ReportDetail(string EMP, string LEVTYP, string YEAR)
        {
            var Detail = new List<DetailReport>();
            string message = string.Empty;
            try
            {
                Detail = new GetDetailReport().Get(EMP, LEVTYP, YEAR);
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