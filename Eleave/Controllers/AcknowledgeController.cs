using Eleave.Data;
using Eleave.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Drawing.Imaging;
using System.Globalization;
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
        public ActionResult AdminAcknowledge(string LeaveType, string reqType, string ReqStatus, string ReqStart, string ReqEnd, string Dept, string reqId, string empName, bool actionFlag)
        {
            string EmpID = string.Empty;
            string EmpType = string.Empty;
            DateTime? startDate = null;
            DateTime? endDate = null;
            string flag = actionFlag ? "1" : "0";
            var Request = new List<ApprovalRequest>();
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
                EmpID = Session["EmpId"].ToString();
                EmpType = Session["UserType"].ToString();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

            try
            {
                Request = new SearchAcknowledgeRequest().GetAck(EmpID, LeaveType, reqType, ReqStatus, startDate, endDate, Dept, reqId, empName, flag);
            }
            catch (Exception ex)
            {

            }
            LoadDepartments();
            LoadRequestType();
            LoadLeavetype();
            LoadReqStatus();
            return View(Request);
        }
        public ActionResult GetDetailAcknowledge(string ReqNo)
        {
            var Request = new List<RequestList>();
            var Comment = new List<ApproveCommentRequest>();
            var AckComment = new List<ApproveCommentRequest>();
            try
            {
                Request = new GetRequestDetail().Get(ReqNo);
                Comment = new GetCommentApprover().GetComment(ReqNo);
                AckComment = new GetCommentAcknowledge().GetComment(ReqNo);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            ViewBag.ReqDetail = Request;
            ViewBag.CommentAppv = Comment;
            ViewBag.AckComment = AckComment;

            return PartialView("_DetailAcknowledge", new
            {
                ViewBag.Message,
                ViewBag.ReqDetail,
                ViewBag.CommentAppv,
                ViewBag.Flag,
            });
        }
        public JsonResult CheckAcknowledge(string ReqNo)
        {
            string message = string.Empty;
            string EmpID = string.Empty;
            string EmpType = string.Empty;
            if (Session["EmpId"] != null)
            {
                EmpID = Session["EmpId"].ToString();
                EmpType = Session["UserType"].ToString();
            }
            var connectionString = ConfigurationManager.ConnectionStrings["HRIS_DB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            try
            {
                var cmd = new SqlCommand("P_Check_Acknowledge_Request", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@inUser", EmpID);
                cmd.Parameters.AddWithValue("@inReqNo", ReqNo);
                SqlParameter p = new SqlParameter("@OutGenstatus", SqlDbType.NVarChar, 100);
                p.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p);
                cmd.ExecuteNonQuery();
                message = cmd.Parameters["@OutGenstatus"].Value.ToString();
                conn.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                conn.Close();
                message = ex.Message;
            }
            return Json(new { message = message }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveAcknowledge(string ReqNo, string AckComment)
        {
            string message = string.Empty;
            string EmpID = string.Empty;
            string EmpType = string.Empty;
            if (Session["EmpId"] != null)
            {
                EmpID = Session["EmpId"].ToString();
                EmpType = Session["UserType"].ToString();
            }
            var connectionString = ConfigurationManager.ConnectionStrings["HRIS_DB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            try
            {
                var cmd = new SqlCommand("P_Save_Ackonwledge_Request", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@inReqNo", ReqNo.Trim());
                cmd.Parameters.AddWithValue("@inUser", EmpID);
                cmd.Parameters.AddWithValue("@inComment", AckComment);
                SqlParameter p = new SqlParameter("@OutGenstatus", SqlDbType.NVarChar, 100);
                p.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p);
                cmd.ExecuteNonQuery();
                message = cmd.Parameters["@OutGenstatus"].Value.ToString();
                conn.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                message = ex.Message;
                conn.Close();
            }
            return Json(new { message = message }, JsonRequestBehavior.AllowGet);
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
                LeaveType = new GetLeaveType().GetLeaveTypeList("");
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