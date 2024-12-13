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
            //var DocumentRequest = Utils.GetDocumentRequest("");
            LoadRequestType();
            ViewBag.DocumentRequest = "0";
            ViewBag.listLeaveBalance = GetLeaveBalance(EmpID);

            return View();
        }
        [HttpPost]
        public ActionResult GetFormLeaveBalance(string empId)
        {
            var listLeaveBalance = new List<StoreGetLeaveBalance>();
            string message = string.Empty;
            try
            {
                listLeaveBalance = new GetLeaveBalance().LeaveBalance(empId);
            }
            catch (Exception ex)
            {
                message = ex.Message;

            }
            ViewBag.Message = message;
            ViewBag.listLeaveBalance = listLeaveBalance;
            return PartialView("_LeaveBalance", new
            {
                @ViewBag.listLeaveBalance
            });
        }
        [HttpPost]
        public List<StoreGetLeaveBalance> GetLeaveBalance(string empId)
        {
            var listLeaveBalance = new List<StoreGetLeaveBalance>();
            listLeaveBalance = new GetLeaveBalance().LeaveBalance(empId);

            return listLeaveBalance;
        }
        [HttpPost]
        public List<StoreGetLogApprove> GetLogApprove(string reqId)
        {
            var listLogApprove = new List<StoreGetLogApprove>();
            listLogApprove = new GetLogApprove().LogApprove(reqId);

            return listLogApprove;
        }
        [HttpPost]
        public ActionResult SaveRequestForm(string ReqNo, string EmpId, string ReqType, string LeaveType, string StartDate, string EndDate, string PeriodTime, double NumDay, int NumHour, string Remark)
        {
            string fileNameNew = string.Empty;
            //data
            var UpdateRequest = new List<StoreUpdateRequest>();
            try
            {
                if (ReqNo == "0")
                {
                    ReqNo = Utils.GetDocumentRequest("");
                }
                UpdateRequest = new UpdateRequest().Save(ReqNo, EmpId, ReqType, LeaveType, StartDate, EndDate, PeriodTime, NumDay, NumHour, Remark);
                //file
                if (Request.Files != null)
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                        var originalFileName = Path.GetFileName(file.FileName);
                        var fileExtension = Path.GetExtension(originalFileName);
                        fileNameNew = ReqNo + "-" + (i + 1) + fileExtension;
                        var path = Path.Combine(Server.MapPath("~/FileUpload/"), fileNameNew);
                        file.SaveAs(path);

                        var UpdateRequestFile = new List<StoreUpdateRequestFile>();
                        UpdateRequestFile = new UpdateRequestFile().Save(ReqNo, fileNameNew, path, (i + 1), EmpId);
                    }
                }
                return Json(new { status = "success", message = "SaveRequestForm updated", getReqNo = ReqNo });
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = ex.Message, getReqNo = ReqNo });
            }
        }
        [HttpPost]
        public ActionResult DeleteFile(string IdFile, string NameFile)
        {
            string messageResult = string.Empty;
            try
            {
                if (IdFile != null)
                {
                    messageResult = Utils.deleteFile(IdFile);
                    if (messageResult == "Y")
                    {
                        string fullPath = Request.MapPath("~/FileUpload/" + NameFile);
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                    }
                }
                return Json(new { status = "success", message = "ลบไฟล์ [" + NameFile + "] เรียบร้อย" });
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = ex.Message });
            }
        }
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
                HisRequest = new GetRequestList().GetRequests(EmpID, EmpType, "");
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
                HisRequest = new GetRequestList().GetRequests(EmpID, EmpType, "");
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
        public ActionResult ManagerSearchHistory(string LeaveType, string reqType, string ReqStatus, string ReqStart, string ReqEnd, string reqId)
        {
            DateTime? startDate = null;
            DateTime? endDate = null;
            string EmpID = string.Empty;
            string EmpName = string.Empty;
            string EmpDept = string.Empty;
            if (Session["EmpId"] != null)
            {
                EmpID = Session["EmpId"].ToString();
                EmpName = Session["FullName"].ToString();
                EmpDept = Session["DeptName"].ToString();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

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
                leaveHis = new SearchHistoryRequest().GetHis(LeaveType, reqType, ReqStatus, startDate, endDate, EmpDept, reqId, EmpName);
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
        public ActionResult GetRequestDetail(string ReqNO, string EmpId, string Flag)
        {
            var ReqDetail = new List<RequestList>();
            var ReqFile = new List<RequestFile>();
            var AppComment = new List<ApproveCommentRequest>();
            var AckComment = new List<ApproveCommentRequest>();
            var listLeaveBalance = new List<StoreGetLeaveBalance>();
            var listLogApprove = new List<StoreGetLogApprove>();
            string message = string.Empty;
            try
            {

                ReqDetail = new GetRequestDetail().Get(ReqNO);
                ReqFile = new GetRequestFile().GetFile(ReqNO);
                AppComment = new GetCommentApprover().GetComment(ReqNO);
                AckComment = new GetCommentAcknowledge().GetComment(ReqNO);
                listLeaveBalance = new GetLeaveBalance().LeaveBalance(EmpId);
                listLogApprove = new GetLogApprove().LogApprove(ReqNO);

                message = "Y";
                ViewBag.ReqDetail = ReqDetail;
                ViewBag.ReqFile = ReqFile;
                ViewBag.ApprvComment = AppComment;
                ViewBag.AckComment = AckComment;
                ViewBag.listLeaveBalance = listLeaveBalance;
                ViewBag.listLogApprove = listLogApprove;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                ViewBag.ReqDetail = new List<object>();
                ViewBag.ReqFile = new List<object>();
                ViewBag.listLeaveBalance = new List<object>();

            }
            ViewBag.Message = message;
            ViewBag.Flag = Flag;
            return PartialView("_RequestDetail", new
            {
                @ViewBag.Message,
                @ViewBag.ReqDetail,
                @ViewBag.ReqFile,
                @ViewBag.Flag,
                @ViewBag.listLeaveBalance,
                @ViewBag.listLogApprove,
            });
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