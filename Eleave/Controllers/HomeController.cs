using Eleave.Models;
using Eleave.Library;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static Eleave.Models.ModelLogin;
using Eleave.Data;

namespace Eleave.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
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

            return View(HisRequest);
        }
        [HttpGet]
        public ActionResult Login()
        {
            var model = new LoginUserModels();

            if (this.Session["UserType"] == null)
            {
                this.Session["UserType"] = "";
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Login(LoginUserModels loginUser)
        {
            string UserType = string.Empty;
            if (ModelState.IsValid)
            {
                var conntionString = ConfigurationManager.ConnectionStrings["HRIS_DB"].ConnectionString;
                SqlConnection conn = new SqlConnection(conntionString);
                try
                {
                    DirectoryEntry entry = new DirectoryEntry("LDAP://ADSRV2016-01/dc=Automotive,dc=com", loginUser.User, loginUser.Password);
                    DirectorySearcher searcher = new DirectorySearcher(entry);
                    searcher.Filter = "(SAMAccountName=" + loginUser.User.Trim() + ")";
                    searcher.PropertiesToLoad.Add("cn");
                    SearchResult result = searcher.FindOne();
                    var userSearch = searcher.FindOne();
                    DirectoryEntry directoryEntry = (DirectoryEntry)userSearch.GetDirectoryEntry();
                    string DepartmentAd = directoryEntry.Properties["Department"].Value.ToString();
                    string EmpId = directoryEntry.Properties["PhysicalDeliveryOfficeName"].Value.ToString();
                    conn.Open();
                    if (result == null)
                    {
                        if (ModelState.IsValid)
                        {
                            return RedirectToAction("Index", "Login");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Username or password ไม่ถูกต้อง");
                        }
                    }
                    else //User/Pass มีบน AD
                    {
                        var dateCheckLogin = CheckLoginEmployee(EmpId, "");
                        UserType = dateCheckLogin.Item2;
                        if (!string.IsNullOrEmpty(UserType))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        ModelState.AddModelError("", "ไม่พบข้อมูลพนักงานของท่านในระบบ HR | Username or password ไม่ถูกต้อง");
                    }
                }
                catch //User/Pass ไม่มีบน AD | พนักงานคลัง
                {
                    var dateCheckLogin = CheckLoginEmployee(loginUser.User, loginUser.Password);
                    UserType = dateCheckLogin.Item2;
                    if (!string.IsNullOrEmpty(UserType))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", "ไม่พบข้อมูลพนักงานของท่านในระบบ HR | Username or password ไม่ถูกต้อง");
                }
                conn.Close();
            }
            return View(loginUser);
        }
        public (string, string, string, string) CheckLoginEmployee(string Username, string Password)
        {
            this.Session["EmpId"] = null;
            this.Session["UserType"] = null;
            this.Session["FullName"] = null;
            this.Session["DeptName"] = null;
            this.Session["Username"] = null;

            string EmpId = "";
            string UserType = "";
            string FullName = "";
            string DeptName = "";
            using (SqlConnection Connection = new SqlConnection(Utils.GetConfig("HRIS_DB")))
            {
                Connection.Open();
                var command = new SqlCommand("P_Check_Login_Employee", Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@inUsername", Username);
                command.Parameters.AddWithValue("@inPassword", Password);
                SqlParameter returnEmpId = new SqlParameter("@getEmpId", SqlDbType.NVarChar, 1000);
                SqlParameter returnUserType = new SqlParameter("@getUserType", SqlDbType.NVarChar, 1000);
                SqlParameter returnFullName = new SqlParameter("@getFullName", SqlDbType.NVarChar, 1000);
                SqlParameter returnDeptName = new SqlParameter("@getDeptName", SqlDbType.NVarChar, 1000);
                returnEmpId.Direction = ParameterDirection.Output;
                returnUserType.Direction = ParameterDirection.Output;
                returnFullName.Direction = ParameterDirection.Output;
                returnDeptName.Direction = ParameterDirection.Output;
                command.Parameters.Add(returnEmpId);
                command.Parameters.Add(returnUserType);
                command.Parameters.Add(returnFullName);
                command.Parameters.Add(returnDeptName);
                int outputResult = command.ExecuteNonQuery();
                EmpId = command.Parameters["@getEmpId"].Value.ToString();
                UserType = command.Parameters["@getUserType"].Value.ToString();
                FullName = command.Parameters["@getFullName"].Value.ToString();
                DeptName = command.Parameters["@getDeptName"].Value.ToString();
                //keep session
                this.Session["EmpId"] = EmpId;
                this.Session["UserType"] = UserType;
                this.Session["FullName"] = FullName;
                this.Session["DeptName"] = DeptName;
                this.Session["Username"] = Username;
                command.Dispose();
                Connection.Close();
            }
            return (EmpId, UserType, FullName, DeptName);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }
        private bool TestLogin(string username, string password)
        {
            return (username == "testuser" && password == "password123");

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
    }
}