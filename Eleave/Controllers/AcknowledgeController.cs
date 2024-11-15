using Eleave.Data;
using Eleave.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eleave.Controllers
{
    public class AcknowledgeController : Controller
    {
        // GET: Acknowledge
        public ActionResult AdminAcknowledge()
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
            var Request = new List<ApprovalRequest>();
            try
            {
                Request = new GetRequestAcknowledge().GetRequestsAck(EmpID);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            LoadDepartments();
            LoadRequestType();
            LoadLeavetype();
            LoadReqStatus();
            return View(Request);
        }
        [HttpPost]
        public ActionResult AdminAcknowledge(string LeaveType, string reqType, string ReqStatus, string ReqStart, string ReqEnd, string Dept, string reqId, string empName)
        {

            LoadDepartments();
            LoadRequestType();
            LoadLeavetype();
            LoadReqStatus();
            return View(Request);
        }
        public ActionResult GetDetailAcknowledge(string ReqNo)
        {
            var Request = new List<RequestList>();
            try
            {
                Request = new GetRequestDetail().Get(ReqNo);
            }
            catch (Exception ex)
            {
                @ViewBag.Message = ex.Message;
            }
            @ViewBag.ReqDetail = Request;

            return PartialView("_DetailAcknowledge", new
            {
                @ViewBag.Message,
                @ViewBag.ReqDetail,
                @ViewBag.Flag,
            });
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