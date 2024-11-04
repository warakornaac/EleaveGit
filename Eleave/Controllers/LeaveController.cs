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
        public ActionResult SaveRequestForm(string ReqNo)
        {

            string fileNameNew = string.Empty;
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
                }
            }

            return View("RequestForm");
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

        private List<LeaveHisDemo> Demodata()
        {
            LeaveHisDemo leave1 = new LeaveHisDemo()
            {
                LeavId = "01",
                ReqType = "ลา",
                LeaveType = "ลาป่วย",
                ReqBy = "ธีระพล ประทาน",
                TotalReq = "1",
                ApprvBy = "โกศล พิมลศรี",
                HrBy = "นรี กรพิทัพิทักษ์",
                ReqDate = DateTime.ParseExact("16/6/2024", "d/M/yyyy", CultureInfo.InvariantCulture),
                StartDate = DateTime.ParseExact("16/6/2024", "d/M/yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("17/6/2024", "d/M/yyyy", CultureInfo.InvariantCulture),
                ApprDate = DateTime.ParseExact("15/6/2024", "d/M/yyyy", CultureInfo.InvariantCulture),
                HrDate = DateTime.ParseExact("15/6/2024", "d/M/yyyy", CultureInfo.InvariantCulture),
                ReqStatus = "S1"
            };
            LeaveHisDemo leave2 = new LeaveHisDemo()
            {
                LeavId = "02",
                ReqType = "ลา",
                ReqBy = "ธีระพล ประทาน",
                LeaveType = "ลากิจ",
                TotalReq = "2",
                ApprvBy = "โกศล พิมลศรี",
                HrBy = "นรี กรพิทัพิทักษ์",
                ReqDate = DateTime.ParseExact("19/6/2024", "d/M/yyyy", CultureInfo.InvariantCulture),
                StartDate = DateTime.ParseExact("20/6/2024", "d/M/yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("21/6/2024", "d/M/yyyy", CultureInfo.InvariantCulture),
                ApprDate = DateTime.ParseExact("19/6/2024", "d/M/yyyy", CultureInfo.InvariantCulture),
                HrDate = DateTime.ParseExact("19/6/2024", "d/M/yyyy", CultureInfo.InvariantCulture),
                ReqStatus = "S1"
            };
            LeaveHisDemo leave3 = new LeaveHisDemo()
            {
                LeavId = "03",
                ReqType = "ลา",
                LeaveType = "ลาพักร้อน",
                ReqBy = "ธีระพล ประทาน",
                TotalReq = "2",
                ApprvBy = "โกศล พิมลศรี",
                HrBy = "นรี กรพิทัพิทักษ์",
                ReqDate = DateTime.Now,
                StartDate = DateTime.ParseExact("16/10/2024", "d/M/yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("30/10/2024", "d/M/yyyy", CultureInfo.InvariantCulture),
                ApprDate = DateTime.ParseExact("15/10/2024", "d/M/yyyy", CultureInfo.InvariantCulture),
                HrDate = DateTime.ParseExact("15/10/2024", "d/M/yyyy", CultureInfo.InvariantCulture),
                ReqStatus = "S2"
            };
            LeaveHisDemo leave4 = new LeaveHisDemo()
            {
                LeavId = "04",
                ReqType = "ลา",
                LeaveType = "ลาป่วย",
                ReqBy = "ธีระพล ประทาน",
                TotalReq = "1",
                ApprvBy = "โกศล พิมลศรี",
                HrBy = "นรี กรพิทัพิทักษ์",
                ReqDate = DateTime.Now,
                StartDate = DateTime.ParseExact("16/10/2024", "d/M/yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("30/10/2024", "d/M/yyyy", CultureInfo.InvariantCulture),
                ApprDate = DateTime.ParseExact("15/10/2024", "d/M/yyyy", CultureInfo.InvariantCulture),
                HrDate = DateTime.ParseExact("15/10/2024", "d/M/yyyy", CultureInfo.InvariantCulture),
                ReqStatus = "S3"
            };
            List<LeaveHisDemo> allLeave = new List<LeaveHisDemo>
            {
                leave1,
                leave2,
                leave3,
                leave4
            };

            return allLeave;
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