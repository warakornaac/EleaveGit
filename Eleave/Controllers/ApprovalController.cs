using Eleave.Data;
using Eleave.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eleave.Controllers
{
    public class ApprovalController : Controller
    {
        // GET: Approval
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ManagerApproval()
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

            if (Session["UserType"].ToString() == "1")
            {
                return RedirectToAction("EmployeeHistory", "Leave");
            }
            var RequestApprv = new List<ApprovalRequest>();
            string Department = Session["DeptName"].ToString();
            try
            {
                RequestApprv = new GetApprovalRequest().GetRequests(Department);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "ไม่สามารถโหลดข้อมูลได้ กรุณาลองอีกครั้ง";
            }
            LoadDepartments();
            LoadRequestType();
            LoadLeavetype();
            LoadReqStatus();

            return View(RequestApprv);
        }
        [HttpPost]
        public ActionResult ManagerApproval(string LeaveType, string reqType, string ReqStatus, string ReqStart, string ReqEnd, string Dept, string reqId, string empName)
        {
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
            var requestOrder = new List<ApprovalRequest>();
            try
            {
                requestOrder = new SearchApprovalRequest().SearchApprv(LeaveType, reqType, ReqStatus, startDate, endDate, Dept, reqId, empName);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "ไม่สามารถโหลดข้อมูลได้ กรุณาลองอีกครั้ง";
            }


            LoadDepartments();
            LoadRequestType();
            LoadLeavetype();
            LoadReqStatus();
            return View(requestOrder);
        }
        public JsonResult GetRequestDetail(string ReqNO)
        {
            var ReqDetail = new List<RequestList>();
            string message = string.Empty;
            try
            {
                ReqDetail = new GetRequestDetail().Get(ReqNO);
                var reqDetailFormatted = ReqDetail.Select(x => new
                {
                    x.ReqNo,
                    x.ReqType,
                    x.CountryCode,
                    x.EmpId,
                    x.LeaveType,
                    x.ReqDate, //ReqDate = x.ReqDate.HasValue ? x.ReqDate.Value.ToString("dd/MM/yyyy") : "",  // ตรวจสอบ null ก่อนแปลง
                    x.StartDate, //= x.StartDate.HasValue ? x.StartDate.Value.ToString("dd/MM/yyyy") : "",  // ตรวจสอบ null ก่อนแปลง
                    x.EndDate, //= x.EndDate.HasValue ? x.EndDate.Value.ToString("dd/MM/yyyy") : "",  // ตรวจสอบ null ก่อนแปลง
                    x.ReqStatus,
                    x.ReqStaDesc,
                    x.Remark
                }).ToList();

                message = "Y";
                return Json(new { message = message, ReqDetail = reqDetailFormatted }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Json(new { message = message, ReqDetail = new List<object>() }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetLeavetype()
        {
            var LeaveType = new List<LeaveTypeModel>();
            string message = string.Empty;
            try
            {
                LeaveType = new GetLeaveType().GetLeaveTypeList();
                message = "Y";
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message = message, LeaveType }, JsonRequestBehavior.AllowGet);
        }
        private void LoadDepartments()
        {
            var departments = new List<StoreGetLookupData>();
            departments = new GetLookupData().GetLookupDataStore("DEPART");


            ViewBag.DepartmentList = departments;
        }
        private void LoadRequestType()
        {
            var REQ_Typ = new List<StoreGetLookupData>();
            REQ_Typ = new GetLookupData().GetLookupDataStore("REQ_TYPE");

            ViewBag.REQ_T = REQ_Typ;
        }
        private void LoadLeavetype()
        {
            var LeaveType = new List<LeaveTypeModel>();
            string message = string.Empty;
            try
            {
                LeaveType = new GetLeaveType().GetLeaveTypeList();
                message = "Y";
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            ViewBag.Leavetype = LeaveType;
        }
        private void LoadReqStatus()
        {
            var ReqSta = new List<StoreGetLookupData>();
            string message = string.Empty;
            try
            {

                ReqSta = new GetLookupData().GetLookupDataStore("REQ_STS");
                message = "Y";
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            ViewBag.ReqSta = ReqSta;
        }
    }
}