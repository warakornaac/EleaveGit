using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Eleave.Data;
using Eleave.Library;
using Eleave.Models;
using System.Data.OleDb;
using System.IO;
<<<<<<< HEAD
using System.Drawing.Imaging;
using Microsoft.Ajax.Utilities;
using System.Xml.Linq;
using System.Text;
=======
>>>>>>> 7d0d7ebb0667871658df72006ebcbba135064eb1

namespace Eleave.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult ManagerEmployee()
        {
            var GetEmployee = new List<StoreGetProfile>();
            GetEmployee = new GetProfileList().GetProfileListS();

            ViewBag.EmployeeList = GetEmployee;
            LoadDepartments();
            LoadEmployeeLevel();
            ViewBag.seachCompany = "";
            ViewBag.seachDept = "";
            ViewBag.seachName = "";
            ViewBag.seachEmpId = "";
            return View();

            //return RedirectToAction("ManagerEmployee", new
            //{
            //    @ViewBag.EmployeeList,
            //    @ViewBag.DepartmentList,
            //});
        }
        [HttpPost]
        public ActionResult ManagerEmployee(string Company, string Department, string Name, string EmpID)
        {
            var GetEmployee = new List<StoreGetProfile>();
            GetEmployee = new GetProfileList().GetProfileListS();
            if (!string.IsNullOrEmpty(Company))
            {
                GetEmployee = GetEmployee.Where(emp => emp.Company == Company).ToList();
            }
            if (!string.IsNullOrEmpty(Department))
            {
                GetEmployee = GetEmployee.Where(emp => emp.DeptNameShort.ToString().ToTrim() == Department.Trim()).ToList();
            }
            if (!string.IsNullOrEmpty(Name))
            {
                GetEmployee = GetEmployee.Where(emp => emp.Fullname.Contains(Name.Trim())).ToList();
            }

            if (!string.IsNullOrEmpty(EmpID))
            {
                GetEmployee = GetEmployee.Where(emp => emp.EmpId.ToString().Contains(EmpID.Trim())).ToList();
            }
            ViewBag.EmployeeList = GetEmployee;
            ViewBag.seachCompany = (Company == null ? "" : Company);
            ViewBag.seachDept = (Department == null ? "" : Department);
            ViewBag.seachName = (Name == null ? "" : Name);
            ViewBag.seachEmpId = (EmpID == null ? "" : EmpID);
            LoadDepartments();

            return View();
        }
        public ActionResult ViewProfile()
        {
            string EmpId = string.Empty;
            //this.Session["EmpId"] = "6601002";
            if (Session["EmpId"] != null)
            {
                EmpId = Session["EmpId"].ToString();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            var Permission = new List<PermissionProfile>();
            var GetProfile = new List<StoreGetProfile>();
            var OverviewLeave = new List<LeaveOverview>();
            //if (EmpId != null)
            //{
            GetProfile = new GetProfile().GetStoreGetProfile(EmpId);
            OverviewLeave = new GetLeaveConsumptionOverview().Get(EmpId);
            Permission = new GetPermissionProfile().Get(EmpId);
            //}
            ViewBag.ProfileList = GetProfile[0];
            ViewBag.UpdateStatus = "";
            ViewBag.listLeaveBalance = GetLeaveBalance(EmpId);
            ViewBag.LeaveOverview = OverviewLeave;
            ViewBag.Permission = Permission;
            return View("ViewProfile", new
            {
                @ViewBag.ProfileList
            });
        }
        [HttpPost]
        public List<StoreGetLeaveBalance> GetLeaveBalance(string empId)
        {
            var listLeaveBalance = new List<StoreGetLeaveBalance>();
            listLeaveBalance = new GetLeaveBalance().LeaveBalance(empId);

            return listLeaveBalance;
        }

        public ActionResult UpdateEmployee(string EmpId, string Companys, string Departments, string Names, string EmpIDs)
        {
            string decodedCompany = string.IsNullOrEmpty(Companys) ? "" : Encoding.UTF8.GetString(Convert.FromBase64String(Companys));
            string decodedDepartment = string.IsNullOrEmpty(Departments) ? "" : Encoding.UTF8.GetString(Convert.FromBase64String(Departments));
            string decodedName = string.IsNullOrEmpty(Names) ? "" : Encoding.UTF8.GetString(Convert.FromBase64String(Names));
            string decodedEmpID = string.IsNullOrEmpty(EmpIDs) ? "" : Encoding.UTF8.GetString(Convert.FromBase64String(EmpIDs));
            var model = new StoreUpdateEmployeeProfile();
            var modelData = new List<StoreUpdateEmployeeProfile>();
            modelData = new GetUpdateProfile().GetStoreGetUpdateProfile(EmpId);
            ViewBag.UpdateStatus = "";
            if (modelData != null && modelData.Count > 0)
            {
                model = modelData.FirstOrDefault();
            }
            var flow = LoadGroupApprovalDropdown();
            var dropdownData = flow
                        .GroupBy(f => f.ApprGrpId)
                        .Select(group => new
                        {
                            ApprGrpId = group.Key,
                            DisplayText = group.Key + " / " + group.First().ApprGrpName // สร้าง DisplayText
                        })
                        .ToList();

            ViewBag.ApprovFlow = dropdownData;
            ViewBag.SearchCompany = decodedCompany;
            ViewBag.SearchDept = decodedDepartment;
            ViewBag.SearchName = decodedName;
            ViewBag.SearchEmpId = decodedEmpID;

            TempData["com"] = decodedCompany;
            TempData["dept"] = decodedDepartment;
            TempData["name"] = decodedName;
            TempData["emp"] = decodedEmpID;
            LoadStatusEmp();
            LoadDirector();
            LoadEmployeeLevel();
            LoadDepartments();
            LoadEmpType();
            LoadUsrtype();
            return View(model);
        }
        public ActionResult Setting()
        {
            var UsrPermisstion = new List<UserPermission>();
            UsrPermisstion = new GetUserPermisstion().GetUser_Permissions();

            return View(UsrPermisstion);
        }

        [HttpPost]
        public ActionResult UpdateEmployee(StoreUpdateEmployeeProfile store)
        {
            var updateEmployee = new List<StoreUpdateEmployeeProfile>();

            try
            {
                string usr = Session["EmpId"]?.ToString(); // ตรวจสอบค่า Session ให้ปลอดภัย
                if (string.IsNullOrEmpty(usr))
                {
                    throw new Exception("Session 'EmpId' is null or empty."); // จัดการกรณีที่ Session ไม่มีค่า
                }

                updateEmployee = new UpdateProfileEmployee().Update(store, usr);
                ViewBag.UpdateStatus = "Success";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message; // เก็บเฉพาะข้อความแสดงข้อผิดพลาด
            }


            SetViewBagData();

            return View(store);
        }

        /// <summary>
        /// เมธอดสำหรับตั้งค่า ViewBag และโหลดข้อมูลที่จำเป็น
        /// </summary>
        private void SetViewBagData()
        {

            var flow = LoadGroupApprovalDropdown();
            var dropdownData = flow
                .GroupBy(f => f.ApprGrpId)
                .Select(group => new
                {
                    ApprGrpId = group.Key,
                    DisplayText = group.Key + " / " + group.First().ApprGrpName
                })
                .ToList();

            ViewBag.ApprovFlow = dropdownData;

            // ตั้งค่า ViewBag สำหรับข้อมูลการค้นหา
            ViewBag.SearchCompany = TempData["com"]?.ToString() ?? "";
            ViewBag.SearchDept = TempData["dept"]?.ToString() ?? "";
            ViewBag.SearchName = TempData["name"]?.ToString() ?? "";
            ViewBag.SearchEmpId = TempData["emp"]?.ToString() ?? "";

            // โหลดข้อมูลเพิ่มเติม
            LoadStatusEmp();
            LoadDirector();
            LoadDepartments();
            LoadEmpType();
            LoadEmployeeLevel();
            LoadUsrtype();
        }


        //[HttpPost]
        //public ActionResult UpdateEmployee(StoreUpdateEmployeeProfile store)
        //{
        //    var updateEmployee = new List<StoreUpdateEmployeeProfile>();

        //    try
        //    {
        //        string usr = Session["EmpId"].ToString();
        //        updateEmployee = new UpdateProfileEmployee().Update(store, usr);
        //        ViewBag.UpdateStatus = "Success";
        //        var flow = LoadGroupApprovalDropdown();
        //        //flow = flow
        //        //        .GroupBy(emp => emp.ApprGrpId) // Group by value
        //        //        .Select(group => group.First()) // เลือกรายการแรก
        //        //        .ToList();

        //        var dropdownData = flow
        //                .GroupBy(f => f.ApprGrpId)
        //                .Select(group => new
        //                {
        //                    ApprGrpId = group.Key,
        //                    DisplayText = group.Key + " / " + group.First().ApprGrpName // สร้าง DisplayText
        //                })
        //                .ToList();

        //        ViewBag.ApprovFlow = dropdownData;
        //        ViewBag.SearchCompany = (TempData["com"] == null ? "" : TempData["com"].ToString());
        //        ViewBag.SearchDept = (TempData["dept"] == null ? "" : TempData["dept"].ToString());
        //        ViewBag.SearchName = (TempData["name"] == null ? "" : TempData["name"].ToString());
        //        ViewBag.SearchEmpId = (TempData["emp"] == null ? "" : TempData["emp"].ToString());
        //        LoadStatusEmp();
        //        LoadDirector();
        //        LoadDepartments();
        //        LoadEmpType();
        //        LoadEmployeeLevel();
        //        LoadUsrtype();
        //        return View(store);
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = ex.ToString();
        //        var flow = LoadGroupApprovalDropdown();
        //        var dropdownData = flow
        //                .GroupBy(f => f.ApprGrpId)
        //                .Select(group => new
        //                {
        //                    ApprGrpId = group.Key,
        //                    DisplayText = group.Key + " / " + group.First().ApprGrpName // สร้าง DisplayText
        //                })
        //                .ToList();

        //        ViewBag.ApprovFlow = dropdownData;
        //        ViewBag.SearchCompany = (TempData["com"] == null ? "" : TempData["com"].ToString());
        //        ViewBag.SearchDept = (TempData["dept"] == null ? "" : TempData["dept"].ToString());
        //        ViewBag.SearchName = (TempData["name"] == null ? "" : TempData["name"].ToString());
        //        ViewBag.SearchEmpId = (TempData["emp"] == null ? "" : TempData["emp"].ToString());
        //        LoadDirector();
        //        LoadDepartments();
        //        LoadEmpType();
        //        LoadEmployeeLevel();
        //        LoadUsrtype();
        //        return View(store);
        //    }
        //}
        //import excel
        public ActionResult ImportExcel()
        {
            return View();
        }
     
        public ActionResult SaveImportExcel(HttpPostedFileBase fileInput, string empId)
        {
            List<string> empIdList = new List<string>();
            //List<StoreUpdateEmployeeProfile> listEmployee = new List<StoreUpdateEmployeeProfile>();
            string filePath = string.Empty;
            string exerror = string.Empty;
            string txtMessage = string.Empty;
            string txtStatus = string.Empty;
            var countRowImport = 0;
            var countStatusSuccess = 0;
            var countStatusFail = 0;
            List<StoreImportEmployee> listEmployee = new List<StoreImportEmployee>();
            try
            {
                if (fileInput != null)
                {
                    string path = Server.MapPath("~/FileExcel/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    filePath = path + Path.GetFileName(fileInput.FileName);
                    string extension = Path.GetExtension(fileInput.FileName);
                    fileInput.SaveAs(filePath);
                    string conString = string.Empty;
                    var connectionString = Utils.GetConfig("HRIS_DB");
                    SqlConnection Connection = new SqlConnection(connectionString);
                    Connection.Open();
                    switch (extension)
                    {
                        case ".xls": //Excel 97-03.
                            conString = Utils.GetConfig("HRIS_EXCEL03");
                            break;
                        case ".xlsx": //Excel 07 and above.
                            conString = Utils.GetConfig("HRIS_EXCEL07");
                            break;
                    }
                    conString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;Persist Security Info=False";
                    DataTable dt = new DataTable();
                    //conString = string.Format(conString, filePath);
                    OleDbConnection excelConnection = new OleDbConnection(conString);
                    OleDbCommand cmd = new OleDbCommand("Select * from [Employee Template$]", excelConnection);
                    excelConnection.Open();
                    OleDbDataReader dReader;
                    dReader = cmd.ExecuteReader();
                    while (dReader.Read())
                    {
                        if (dReader.GetValue(2).ToString() != "")
                        {
                            SqlCommand cmdUpload = new SqlCommand("P_Import_Employee", Connection);
                            cmdUpload.Connection = Connection;
                            cmdUpload.CommandType = CommandType.StoredProcedure;
                            cmdUpload.Parameters.AddWithValue("@inCompany", dReader.GetValue(0).ToString());
                            cmdUpload.Parameters.AddWithValue("@inCountryCode", dReader.GetValue(1).ToString());
                            cmdUpload.Parameters.AddWithValue("@inEmpId", dReader.GetValue(2).ToString());
                            cmdUpload.Parameters.AddWithValue("@inTitleName", dReader.GetValue(3).ToString());
                            cmdUpload.Parameters.AddWithValue("@inFirstName", dReader.GetValue(4).ToString());
                            cmdUpload.Parameters.AddWithValue("@inLastName", dReader.GetValue(5).ToString());
                            cmdUpload.Parameters.AddWithValue("@inGender", dReader.GetValue(6).ToString());
                            cmdUpload.Parameters.AddWithValue("@inDeptId", dReader.GetValue(7).ToString());
                            cmdUpload.Parameters.AddWithValue("@inPosition", dReader.GetValue(8).ToString());
                            cmdUpload.Parameters.AddWithValue("@inEmpLvl", dReader.GetValue(9).ToString());
                            cmdUpload.Parameters.AddWithValue("@inEmpTypeId", dReader.GetValue(10).ToString());
                            cmdUpload.Parameters.AddWithValue("@inApprGrpId", dReader.GetValue(11).ToString());
                            cmdUpload.Parameters.AddWithValue("@inStartDate", dReader.GetValue(12).ToString());
                            cmdUpload.Parameters.AddWithValue("@inEmail", dReader.GetValue(13).ToString());
                            cmdUpload.Parameters.AddWithValue("@inEmpStatus", dReader.GetValue(14).ToString());
                            cmdUpload.Parameters.AddWithValue("@inUserType", dReader.GetValue(15).ToString());
                            cmdUpload.Parameters.AddWithValue("@inDirectorId", dReader.GetValue(16).ToString());
                            cmdUpload.Parameters.AddWithValue("@inInsertedBy", empId.ToString());
                            SqlParameter returnValue = new SqlParameter("@outGenstatus", SqlDbType.NVarChar, 100);
                            returnValue.Direction = System.Data.ParameterDirection.Output;
                            cmdUpload.Parameters.Add(returnValue);
                            SqlDataReader dr = cmdUpload.ExecuteReader();
                            while (dr.Read())
                            {
                                countRowImport++;
                                if (dr["StatusImport"].ToString() == "Y")
                                {
                                    countStatusSuccess++;
                                }
                                if (dr["StatusImport"].ToString() == "N")
                                {
                                    countStatusFail++;
                                }
                                listEmployee.Add(new StoreImportEmployee()
                                {
                                    Company = dr["Company"].ToString(),
                                    CountryCode = dr["CountryCode"].ToString(),
                                    EmpId = dr["EmpId"].ToString(),
                                    TitleName = dr["TitleName"].ToString(),
                                    FirstName = dr["FirstName"].ToString(),
                                    LastName = dr["LastName"].ToString(),
                                    Gender = dr["Gender"].ToString(),
                                    DeptId = dr["DeptId"].ToString(),
                                    Position = dr["Position"].ToString(),
                                    EmpLvl = dr["EmpLvl"].ToString(),
                                    EmpTypeId = dr["EmpTypeId"].ToString(),
                                    ApprGrpID = dr["ApprGrpID"].ToString(),
                                    StartDate = dr["StartDate"].ToString(),
                                    Email = dr["Email"].ToString(),
                                    EmpStatus = dr["EmpStatus"].ToString(),
                                    UserType = dr["UserType"].ToString(),
                                    DirectorId = dr["DirectorId"].ToString(),
                                    InsertedBy = dr["InsertedBy"].ToString(),
                                    StatusImport = dr["StatusImport"].ToString(),
                                    ErrorImport = dr["ErrorImport"].ToString()
                                });
                            }
                            cmdUpload.Dispose();
                        }
                    }
                    excelConnection.Close();
                    Connection.Close();
                }
            }
            catch (Exception ex)
            {
                txtMessage = ex.Message + '/' + ex.Source + '/' + ex.HelpLink + '/' + ex.HResult;
            }
            ViewBag.status = txtStatus;
            ViewBag.message = txtMessage;
            ViewBag.listEmployee = listEmployee;
            ViewBag.countStatusSuccess = countStatusSuccess;
            ViewBag.countStatusFail = countStatusFail;
            ViewBag.countRowImport = countRowImport;
            return PartialView("_ListImportExcel", new
            {
                @ViewBag.status,
                @ViewBag.message,
                @ViewBag.listEmployee,
                @ViewBag.countStatusSuccess,
                @ViewBag.countStatusFail,
                @ViewBag.countRowImport,
            });
        }
        [HttpGet]
        public ActionResult ApprovflowSetting()
        {
            string EmpId = string.Empty;
            if (Session["EmpId"] != null)
            {
                EmpId = Session["EmpId"].ToString();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            var list = LoadGroupApprovalDropdown();
            var flow = LoadGroupApprovalDropdown();
            flow = flow
                    .GroupBy(emp => emp.ApprGrpId) // Group by value
                    .Select(group => group.First()) // เลือกรายการแรก
                    .ToList();
            ViewBag.apprvFlow = list.ToArray();
            ViewBag.apprvFlowSelect = flow.ToArray();
            LoadDepartments();
            return View();
        }
        [HttpPost]
        public ActionResult ApprovflowSetting(string DeptID, string ApprvGrp)
        {
            string EmpId = string.Empty;
            //this.Session["EmpId"] = "6601002";
            if (Session["EmpId"] != null)
            {
                EmpId = Session["EmpId"].ToString();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            var list = LoadGroupApprovalDropdown();
            var flow = LoadGroupApprovalDropdown();
            LoadDepartments();
            if (!string.IsNullOrEmpty(DeptID))
            {
                list = list.Where(emp => emp.DepId.Trim() == DeptID.Trim()).ToList();
            }
            if (!string.IsNullOrEmpty(ApprvGrp))
            {
                list = list.Where(emp => emp.ApprGrpId.Trim() == ApprvGrp).ToList();
            }
            if (!string.IsNullOrEmpty(DeptID))
            {
                flow = flow.Where(emp => emp.DepId.Trim() == DeptID.Trim()).ToList();
            }
            flow = flow
                    .GroupBy(emp => emp.ApprGrpId) // Group by value
                    .Select(group => group.First()) // เลือกรายการแรก
                    .ToList();
            ViewBag.apprvFlow = list.ToList();
            ViewBag.apprvFlowSelect = flow.ToArray();
            return View();
        }
        //ApprvFlowSetting 
        public JsonResult GenerateApprvFlowID(string Dept)
        {
            string message = string.Empty;
            string apprvGrbID = string.Empty;
            string stepGrb = string.Empty;
            var connectionString = ConfigurationManager.ConnectionStrings["HRIS_DB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            try
            {
                var cmd = new SqlCommand("P_Generate_ArrovalFlow_GroupID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@inDepartment", Dept);
                SqlParameter p = new SqlParameter("@OutGenstatus", SqlDbType.NVarChar, 100);
                p.Direction = ParameterDirection.Output;
                SqlParameter gen = new SqlParameter("@NewApprGrpId", SqlDbType.VarChar, 20);
                gen.Direction = ParameterDirection.Output;
                SqlParameter step = new SqlParameter("@NewStep", SqlDbType.VarChar, 3);
                step.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p);
                cmd.Parameters.Add(gen);
                cmd.Parameters.Add(step);
                cmd.ExecuteNonQuery();
                message = cmd.Parameters["@OutGenstatus"].Value.ToString();
                apprvGrbID = cmd.Parameters["@NewApprGrpId"].Value.ToString();
                stepGrb = cmd.Parameters["@NewStep"].Value.ToString();
                conn.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                conn.Close();
                message = ex.Message;
            }
            return Json(new { message = message, apprvGrbID = apprvGrbID.Trim(), stepGrb = stepGrb.Trim() }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetApprvID(string Dept)
        {
            string message = string.Empty;
            //string GrpvID = string.Empty;
            List<object> GrpvID = new List<object>();
            var connectionString = ConfigurationManager.ConnectionStrings["HRIS_DB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            try
            {
                var cmd = new SqlCommand("P_Get_ArrovalFlow_GrpID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@inDepartment", Dept);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    GrpvID.Add(new
                    {
                        ApprGrpId = reader["ApprGrpId"].ToString(),
                        ApprGrpName = reader["ApprGrpName"].ToString()
                    });

                }
                conn.Close();
                conn.Dispose();
                reader.Close();
                reader.Dispose();
                message = "Y";

            }
            catch (Exception ex)
            {
                conn.Close();
                message = ex.Message;
            }
            return Json(new { message = message, GrpvID = GrpvID }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GenerateApprvFlowStep(string GrpID)
        {
            string message = string.Empty;
            string stepGrb = string.Empty;
            var connectionString = ConfigurationManager.ConnectionStrings["HRIS_DB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            try
            {
                var cmd = new SqlCommand("P_Generate_ArrovalFlow_Step", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@inGrpID", GrpID);
                SqlParameter p = new SqlParameter("@OutGenstatus", SqlDbType.NVarChar, 100);
                p.Direction = ParameterDirection.Output;
                SqlParameter step = new SqlParameter("@NewStep", SqlDbType.VarChar, 3);
                step.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p);
                cmd.Parameters.Add(step);
                cmd.ExecuteNonQuery();
                message = cmd.Parameters["@OutGenstatus"].Value.ToString();
                stepGrb = cmd.Parameters["@NewStep"].Value.ToString();
                conn.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                conn.Close();
                message = ex.Message;
            }
            return Json(new { message = message, stepGrb = stepGrb.Trim() }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDirectorDepartment(string deptID)
        {
            string message = string.Empty;
            var getDirector = new List<DirectorProfile>();
            var connectionString = ConfigurationManager.ConnectionStrings["HRIS_DB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            try
            {
                var cmd = new SqlCommand("P_Get_ProfileDirector_Dept_List", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@inDepartment", deptID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    getDirector.Add(new DirectorProfile()
                    {
                        Company = reader["Company"].ToString(),
                        CountryCode = reader["CountryCode"].ToString(),
                        EmpId = reader["EmpId"].ToString().Trim(),
                        Fullname = reader["Fullname"].ToString(),
                        DeptId = reader["DeptId"].ToString(),
                        Position = reader["Position"].ToString(),
                        EmpTypeId = reader["EmpTypeId"].ToString(),
                        UserType = reader["UserType"].ToString()

                    });
                }
                reader.Close();
                reader.Dispose();
                conn.Close();
                message = "Y";
            }
            catch (Exception ex)
            {
                conn.Close();
                message = ex.Message;
            }
            return Json(new { message = message, getDirector }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetApprovflowSettingEdit(string ApprvGrp, string ApprvName, string ApprvStep)
        {
            string message = string.Empty;
            var getApproval = new List<ApprovalFlow>();
            try
            {

                getApproval = new GetApprovalFlowEdit().GetApprovalFlows(ApprvGrp, ApprvName, ApprvStep);
                message = "Y";


            }
            catch (Exception ex)
            {
                message = ex.Message;
            }


            return Json(new { message = message, getApproval }, JsonRequestBehavior.AllowGet);
        }
        //AddFlow
        public JsonResult AddApprvFlow(string apprvID, string apprvName, string dept, string step, string empId, string action, string desc)
        {
            string message = string.Empty;
            string username = Session["EmpId"].ToString();
            var connectionString = ConfigurationManager.ConnectionStrings["HRIS_DB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            try
            {
                var cmd = new SqlCommand("P_Add_ApprovalFlow", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@inApprvID", apprvID);
                cmd.Parameters.AddWithValue("@inApprvName", apprvName);
                cmd.Parameters.AddWithValue("@inDept", dept);
                cmd.Parameters.AddWithValue("@inStep", step);
                cmd.Parameters.AddWithValue("@inEmpID", empId);
                cmd.Parameters.AddWithValue("@inAction", action);
                cmd.Parameters.AddWithValue("@inApprvDes", desc);
                cmd.Parameters.AddWithValue("@inUser", username.Trim());
                SqlParameter p = new SqlParameter("@outGenstatus", SqlDbType.NVarChar, 100);
                p.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p);
                cmd.ExecuteNonQuery();
                message = cmd.Parameters["@OutGenstatus"].Value.ToString();

                cmd.Dispose();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                message = ex.Message;
            }
            return Json(new { message = message }, JsonRequestBehavior.AllowGet);
        }
        //UpdateFlow
        public JsonResult UpdateApprvFlow(string apprvID, string oldApprvName, string apprvName, string dept, string step, string oldStep, string empId, string action, string desc)
        {
            string message = string.Empty;
            string username = Session["EmpId"].ToString();
            var connectionString = ConfigurationManager.ConnectionStrings["HRIS_DB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            try
            {
                var cmd = new SqlCommand("P_Update_ApprovalFlow", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@inApprvID", apprvID);
                cmd.Parameters.AddWithValue("@inOldApprvName", oldApprvName);
                cmd.Parameters.AddWithValue("@inApprvName", apprvName);
                cmd.Parameters.AddWithValue("@inDept", dept);
                cmd.Parameters.AddWithValue("@inOldStep", oldStep);
                cmd.Parameters.AddWithValue("@inStep", step);
                cmd.Parameters.AddWithValue("@inEmpID", empId);
                cmd.Parameters.AddWithValue("@inAction", action);
                cmd.Parameters.AddWithValue("@inDesc", desc);
                cmd.Parameters.AddWithValue("@inUser", username.Trim());
                SqlParameter p = new SqlParameter("@outGenstatus", SqlDbType.NVarChar, 100);
                p.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p);
                cmd.ExecuteNonQuery();
                message = cmd.Parameters["@OutGenstatus"].Value.ToString();

                cmd.Dispose();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                message = ex.Message;
            }
            return Json(new { message = message }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteApprvFlow(string ApprvID, string ApprvStep, string ApprvAction)
        {
            string message = string.Empty;
            string username = Session["EmpId"].ToString();
            string EmpID = string.Empty;
            if (Session["EmpId"] is null)
            {
                return Json(new { success = true, message = "Session expired. Please log in again." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                EmpID = Session["EmpId"].ToString();
            }
            var connectionString = ConfigurationManager.ConnectionStrings["HRIS_DB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            try
            {
                var cmd = new SqlCommand("P_Delete_ApprovalFlowByStep", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@inApprvID", ApprvID);
                cmd.Parameters.AddWithValue("@inStep", ApprvStep);
                cmd.Parameters.AddWithValue("@inAction", ApprvAction);
                cmd.Parameters.AddWithValue("@inUser", EmpID);
                SqlParameter p = new SqlParameter("@outGenstatus", SqlDbType.NVarChar, 100);
                p.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p);
                cmd.ExecuteNonQuery();
                message = cmd.Parameters["@OutGenstatus"].Value.ToString();
                cmd.Dispose();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                message = ex.Message;
            }
            return Json(new { message = message }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDepartmentEditApprvFlow(string GrpId)
        {
            string message = string.Empty;
            string DepId = string.Empty;
            var connectionString = ConfigurationManager.ConnectionStrings["HRIS_DB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            try
            {
                var cmd = new SqlCommand("P_Get_Department_Edit_ApprvFlow", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@inGrpId", GrpId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    DepId = reader[0].ToString();
                }
                message = "Y";
                cmd.Dispose();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                message = ex.Message;
            }
            return Json(new { message = message, DepID = DepId.Trim() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DetailEmployee(string EMPID)
        {
            //string EmpId = string.Empty;
            //this.Session["EmpId"] = "6601002";
            //EmpId = Session["EmpId"].ToString();
            var GetProfile = new List<StoreGetProfile>();
            //if (EmpId != null)
            //{
            GetProfile = new GetProfile().GetStoreGetProfile(EMPID);
            //}
            ViewBag.ProfileList = GetProfile[0];

            return PartialView("_DetailEmployee", new
            {
                ViewBag.ProfileList
            });
        }
        private JsonResult LoadEmployeeHead(string Depart)
        {
            string message = string.Empty;
            var connectionString = ConfigurationManager.ConnectionStrings["HRIS_DB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            List<Object> empH = new List<Object>();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [HRIS].[dbo].[Employee] where UserType = '3' and DeptId = '" + Depart + "'", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    empH.Add(new
                    {
                        emID = reader["EmpId"].ToString(),
                        emName = reader["FirstName"].ToString() + " " + reader["LastName"].ToString()
                    });
                }
                reader.Close();
                reader.Dispose();
                conn.Close();
                message = "Y";
            }
            catch (Exception ex)
            {
                message = ex.Message;

            }


            return Json(new { message = message, empH }, JsonRequestBehavior.AllowGet);
        }
        public void LoadStatusEmp()
        {
            var EMP_STS = new List<StoreGetLookupData>();
            EMP_STS = new GetLookupData().GetLookupDataStore("EMP_STS");
            ViewBag.EmpSta = EMP_STS;
        }
        public void LoadEmployeeLevel()
        {
            var EMP_LVL = new List<StoreGetLookupData>();
            EMP_LVL = new GetLookupData().GetLookupDataStore("EMP_LVL");

            ViewBag.EmpLevel = EMP_LVL;

        }
        private void LoadEmpType()
        {
            var Emp_Type = new List<StoreGetLookupData>();
            Emp_Type = new GetLookupData().GetLookupDataStore("EMP_TYPE");

            ViewBag.EmpTyp = Emp_Type;
        }
        private void LoadDepartments()
        {

            var departments = new List<StoreGetLookupData>();
            departments = new GetLookupData().GetLookupDataStore("DEPART");


            ViewBag.DepartmentList = departments;
        }
        private List<ApprovalFlow> LoadGroupApprovalDropdown()
        {
            var apprvFlow = new List<ApprovalFlow>();
            apprvFlow = new GetApprovalFlowList().Get();
            return apprvFlow;
        }
        private void LoadDirector()
        {
            string message = string.Empty;
            var getDirector = new List<DirectorProfile>();
            var connectionString = ConfigurationManager.ConnectionStrings["HRIS_DB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            try
            {
                var cmd = new SqlCommand("P_Get_ProfileDirector_Dept_List", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@inDepartment", "123");
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    getDirector.Add(new DirectorProfile()
                    {
                        Company = reader["Company"].ToString(),
                        CountryCode = reader["CountryCode"].ToString(),
                        EmpId = reader["EmpId"].ToString().Trim(),
                        Fullname = reader["Fullname"].ToString(),
                        DeptId = reader["DeptId"].ToString(),
                        Position = reader["Position"].ToString(),
                        EmpTypeId = reader["EmpTypeId"].ToString(),
                        UserType = reader["UserType"].ToString()

                    });
                }
                reader.Close();
                reader.Dispose();
                conn.Close();
                message = "Y";
            }
            catch (Exception ex)
            {
                conn.Close();
                message = ex.Message;
            }
            ViewBag.Directors = getDirector;
        }
        private void LoadUsrtype()
        {
            string message = string.Empty;
            var connectionString = ConfigurationManager.ConnectionStrings["HRIS_DB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            List<Object> typ = new List<Object>();
            try
            {
                SqlCommand cmd = new SqlCommand("select LookDesc,LookValue from [HRIS].[dbo].[LookupData] where LookCode = 'USR_TYPE'", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    typ.Add(new
                    {
                        UsrTypeName = reader["LookDesc"].ToString(),
                        TypeVal = reader["LookValue"].ToString()
                    });
                }
                reader.Close();
                reader.Dispose();

                conn.Close();
            }
            catch
            {
                conn.Close();
            }
            ViewBag.usrtyp = typ;
        }
    }
}