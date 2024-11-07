using Eleave.Data;
using Eleave.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eleave.Library;
using System.IO;
using Microsoft.Ajax.Utilities;
using System.Web.Services.Description;

namespace Eleave.Controllers
{
    public class LeaveController : Controller
    {
        // GET: Leave
        public ActionResult RequestForm()
        {
            var DocumentRequest = Utils.GetDocumentRequest("");
            LoadRequestType();
            ViewBag.DocumentRequest = DocumentRequest;

            return View();
        }
        [HttpPost]
        //public ActionResult SaveRequestForm(List<StoreUpdateRequest> request)
        //{

        //    string fileNameNew = string.Empty;
        //    var UpdateRequest = new List<StoreUpdateRequest>();
        //    foreach (var listData in (List<StoreUpdateRequest>)request)
        //    {
        //        //data
        //        UpdateRequest = new UpdateRequest().Save(listData.ReqNo, listData.EmpId, listData.LeaveType, listData.StartDate, listData.EndDate, listData.NumDay, listData.NumHour, listData.Remark);
        //        //file
        //        if (Request.Files != null)
        //        {
        //            for (int i = 0; i < Request.Files.Count; i++)
        //            {
        //                var file = Request.Files[i];
        //                var originalFileName = Path.GetFileName(file.FileName);
        //                var fileExtension = Path.GetExtension(originalFileName);

        //                fileNameNew = listData.ReqNo + "-" + (i + 1) + fileExtension;
        //                var path = Path.Combine(Server.MapPath("~/FileUpload/"), fileNameNew);
        //                file.SaveAs(path);
        //            }
        //        }
        //    }

        //    return View("RequestForm");
        //}
        public ActionResult ManagerHistory()
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
            var HisRequest = new List<RequestList>();

            try
            {
                HisRequest = new GetRequestList().GetRequests(EmpID, EmpType);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "ไม่สามารถโหลดข้อมูลได้ กรุณาลองอีกครั้ง";
            }
            LoadDepartments();
            LoadRequestType();
            LoadLeavetype();
            LoadReqStatus();

            return View(HisRequest);
        }
        public ActionResult EmployeeHistory()
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
            var HisRequest = new List<RequestList>();

            try
            {
                HisRequest = new GetRequestList().GetRequests(EmpID, EmpType);
            }
            catch (Exception ex)
            {

            }
            LoadDepartments();
            LoadRequestType();
            LoadLeavetype();
            LoadReqStatus();

            return View(HisRequest);
        }
        [HttpPost]
        public ActionResult ManagerSearchHistory(string LeaveType, string reqType, string ReqStatus, string ReqStart, string ReqEnd, string Department, string reqId, string empName)
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
            var leaveHis = new List<RequestList>();
            try
            {
                leaveHis = new SearchHistoryRequest().GetHis(LeaveType, reqType, ReqStatus, startDate, endDate, Department, reqId, empName);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "ไม่สามารถโหลดข้อมูลได้ กรุณาลองอีกครั้ง";
            }


            LoadDepartments();
            LoadRequestType();
            LoadLeavetype();
            LoadReqStatus();
            return View("ManagerHistory", leaveHis);
        }
        [HttpPost]
        public ActionResult EmployeeHistory(string LeaveType, string reqType, string ReqStatus, string ReqStart, string ReqEnd, string reqId)
        {
            string EmpID = string.Empty;
            string EmpName = string.Empty;
            if (Session["EmpId"] != null)
            {
                EmpID = Session["EmpId"].ToString();
                EmpName = Session["FullName"].ToString();

            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
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
            var leaveHis = new List<RequestList>();
            try
            {
                leaveHis = new SearchHistoryRequest().GetHis(LeaveType, reqType, ReqStatus, startDate, endDate, string.Empty, reqId, EmpName);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "ไม่สามารถโหลดข้อมูลได้ กรุณาลองอีกครั้ง";
            }
            LoadDepartments();
            LoadRequestType();
            LoadLeavetype();
            LoadReqStatus();
            return View(leaveHis);
        }
        public JsonResult CancelRequest(string ReqNo)
        {
            string message = string.Empty;
            string user = string.Empty;
            var connectionString = ConfigurationManager.ConnectionStrings["HRIS_DB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            try
            {
                user = Session["EmpId"].ToString();
                var cmd = new SqlCommand("P_Cancel_Request", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@inReqNo", ReqNo);
                cmd.Parameters.AddWithValue("@inUser", user);

                int INSID = cmd.ExecuteNonQuery();
                if (INSID > 0)
                {
                    message = "Y";
                }
                else { message = "Failed"; }
                cmd.Dispose();
                conn.Close();

            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message = message }, JsonRequestBehavior.AllowGet);
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
                    ReqDate = x.ReqDate.HasValue ? x.ReqDate.Value.ToString("dd/MM/yyyy") : "",  // ตรวจสอบ null ก่อนแปลง
                    StartDate = x.StartDate.HasValue ? x.StartDate.Value.ToString("dd/MM/yyyy") : "",  // ตรวจสอบ null ก่อนแปลง
                    EndDate = x.EndDate.HasValue ? x.EndDate.Value.ToString("dd/MM/yyyy") : "",  // ตรวจสอบ null ก่อนแปลง
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
        private void LoadReqType()
        {
            var EMP_LVL = new List<StoreGetLookupData>();
            EMP_LVL = new GetLookupData().GetLookupDataStore("EMP_LVL");

            ViewBag.ReqType = EMP_LVL;
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

    }
}