using Eleave.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static Eleave.Models.ModelLogin;

namespace Eleave.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var leaveHis = Demodata();
            return View(leaveHis);
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
            if (ModelState.IsValid)
            {
                this.Session["UserType"] = null;
                this.Session["UserID"] = loginUser.User.Trim();
                this.Session["UserPassword"] = loginUser.Password.Trim();
                this.Session["SLMCOD"] = string.Empty;
                string UserType = string.Empty;

                var conntionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
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

                    conn.Open();
                    if (result == null)
                    {
                        if (ModelState.IsValid)
                        {
                            FormsAuthentication.SetAuthCookie(loginUser.User.Trim(), false);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Login details are wrong 1.");
                        }
                    }
                    else
                    {//มีใน AD จะไม่เช็ค password UsrTbl
                        string sqlString = "SELECT Usr.UsrTyp, Ad.Department, Ad.SLMCOD " +
                            "FROM UsrTbl_Budget Usr " +
                            "INNER JOIN v_ADUser Ad ON Ad.LogInName = Usr.UsrID " +
                            "WHERE UsrID =N'" + loginUser.User.Trim() + "'" +
                            "AND Usr.Password is null";
                        SqlCommand cmd = new SqlCommand(sqlString, conn);
                        SqlDataReader reader = cmd.ExecuteReader();
                        //มีใน table
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                this.Session["UserType"] = reader["UsrTyp"].ToString();
                                this.Session["Department"] = "";
                                this.Session["SLMCOD"] = reader["SLMCOD"].ToString();
                                UserType = Session["UserType"].ToString();
                            }
                        }
                        else //ไม่มีใน table
                        {
                            //Default IT
                            if (DepartmentAd == "MIS")
                            {
                                this.Session["UserType"] = 1;
                                UserType = Session["UserType"].ToString();
                            }
                            else
                            {
                                ModelState.AddModelError("", "You don't have permission, Please contact admin");
                            }
                        }
                        reader.Close();
                        reader.Dispose();
                        cmd.Dispose();
                        FormsAuthentication.SetAuthCookie(loginUser.User.Trim(), false);
                        if (UserType == "1" || UserType == "2")//admin pm
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else if (UserType == "3")//sale
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else if (UserType == "4") { return RedirectToAction("Index", "Home"); }
                        ModelState.AddModelError("", "Login Details are wrong 2. " + "Type: " + UserType);
                    }
                }
                catch //ไม่มีใน table
                {
                    conn.Open();
                    string txtSql = "";
                    txtSql = "SELECT Usr.UsrTyp, Ad.Department, ISNULL(Usr.SLMCOD ,Ad.SLMCOD) as SLMCOD " +
                        "FROM UsrTbl_Budget Usr " +
                        "LEFT JOIN v_ADUser Ad ON Ad.LogInName = Usr.UsrID " +
                        "WHERE UsrID =N'" + loginUser.User.Trim() + "'and [dbo].F_decrypt([Password])='" + loginUser.Password.Trim() + "'";
                    SqlCommand cmd = new SqlCommand(txtSql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        this.Session["UserType"] = reader["UsrTyp"].ToString();
                        this.Session["Department"] = "";
                        this.Session["SLMCOD"] = reader["SLMCOD"].ToString();
                        UserType = reader["UsrTyp"].ToString();
                    }
                    reader.Close();
                    reader.Dispose();
                    cmd.Dispose();
                    FormsAuthentication.SetAuthCookie(loginUser.User.Trim(), false);
                    if (UserType == "1" || UserType == "2")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (UserType == "3")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (UserType == "4")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", "Login details are wrong 3.");
                }
                conn.Close();
            }
            return View(loginUser);
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