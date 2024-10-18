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

namespace Eleave.Controllers
{
    public class LeaveController : Controller
    {
        // GET: Leave
        public ActionResult RequestForm()
        {
            return View();
        }
        public ActionResult ManagerHistory()
        {
            var leaveHis = Demodata();
            LoadDepartments();

            return View(leaveHis);
        }
        public ActionResult EmployeeHistory()
        {
            var leaveHis = Demodata();
            LoadDepartments();
            return View(leaveHis);
        }
        [HttpPost]
        public ActionResult ManagerSearchHistory(string LeaveType, string reqType, string ReqStatus, string ReqStart, string ReqEnd, string Department, string reqId, string empName)
        {
            var leaveHis = Demodata();


            if (!string.IsNullOrEmpty(reqType))
            {
                leaveHis = leaveHis.Where(l => l.LeaveType == reqType).ToList();
            }
            if (!string.IsNullOrEmpty(ReqStatus))
            {
                leaveHis = leaveHis.Where(l => l.ReqStatus == ReqStatus).ToList();
            }
            if (!string.IsNullOrEmpty(ReqStart))
            {
                DateTime startParsed;
                if (DateTime.TryParseExact(ReqStart, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startParsed))
                {
                    leaveHis = leaveHis.Where(l => l.StartDate.Date >= startParsed.Date).ToList();
                }
            }
            if (!string.IsNullOrEmpty(ReqEnd))
            {
                DateTime endParsed;
                if (DateTime.TryParseExact(ReqEnd, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endParsed))
                {
                    leaveHis = leaveHis.Where(l => l.EndDate.Date <= endParsed.Date).ToList();
                }
            }
            if (!string.IsNullOrEmpty(Department))
            {

            }

            if (!string.IsNullOrEmpty(reqId))
            {
                leaveHis = leaveHis.Where(l => l.LeavId == reqId).ToList();
            }
            if (!string.IsNullOrEmpty(empName))
            {
                leaveHis = leaveHis.Where(l => l.ReqBy == empName).ToList();
            }
            LoadDepartments();
            return View("ManagerHistory", leaveHis);
        }
        [HttpPost]
        public ActionResult EmployeeHistory(string LeaveType, string reqType, string ReqStatus, string ReqStart, string ReqEnd, string reqId)
        {
            var leaveHis = Demodata();
            if (!string.IsNullOrEmpty(LeaveType))
            {

            }

            if (!string.IsNullOrEmpty(reqType))
            {
                leaveHis = leaveHis.Where(l => l.LeaveType == reqType).ToList();
            }
            if (!string.IsNullOrEmpty(ReqStatus))
            {
                leaveHis = leaveHis.Where(l => l.ReqStatus == ReqStatus).ToList();
            }
            if (!string.IsNullOrEmpty(ReqStart))
            {
                DateTime startParsed;
                if (DateTime.TryParseExact(ReqStart, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startParsed))
                {
                    leaveHis = leaveHis.Where(l => l.StartDate.Date >= startParsed.Date).ToList();
                }
            }
            if (!string.IsNullOrEmpty(ReqEnd))
            {
                DateTime endParsed;
                if (DateTime.TryParseExact(ReqEnd, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endParsed))
                {
                    leaveHis = leaveHis.Where(l => l.EndDate.Date <= endParsed.Date).ToList();
                }
            }

            if (!string.IsNullOrEmpty(reqId))
            {
                leaveHis = leaveHis.Where(l => l.LeavId == reqId).ToList();
            }
            LoadDepartments();
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
        private void LoadReqType()
        {
            var EMP_LVL = new List<StoreGetLookupData>();
            EMP_LVL = new GetLookupData().GetLookupDataStore("EMP_LVL");

            ViewBag.ReqType = EMP_LVL;
        }
        private void LoadDepartments()
        {
            List<SelectListItem> departmentList = new List<SelectListItem>();
            var connectionString = ConfigurationManager.ConnectionStrings["HRIS_DB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [HRIS].[dbo].[Department]", conn);
            SqlDataReader reader_dept = cmd.ExecuteReader();
            while (reader_dept.Read())
            {
                departmentList.Add(new SelectListItem
                {
                    Value = reader_dept["DeptName"].ToString(),
                    Text = reader_dept["DeptName"].ToString()
                });
            }
            reader_dept.Close();
            reader_dept.Dispose();
            cmd.Dispose();
            conn.Close();
            ViewBag.DepartmentList = departmentList;
        }
    }
}