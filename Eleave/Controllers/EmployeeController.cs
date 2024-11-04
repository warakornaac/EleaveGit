using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Eleave.Data;
using Eleave.Library;
using Eleave.Models;

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
            LoadDepartments();

            return View();
        }

        public ActionResult ViewProfile()
        {
            string EmpId = string.Empty;
            //this.Session["EmpId"] = "6601002";
            EmpId = Session["EmpId"].ToString();
            var GetProfile = new List<StoreGetProfile>();
            //if (EmpId != null)
            //{
            GetProfile = new GetProfile().GetStoreGetProfile(EmpId);
            //}
            ViewBag.ProfileList = GetProfile[0];
            ViewBag.UpdateStatus = "";
            return View("ViewProfile", new
            {
                @ViewBag.ProfileList
            });
        }

        public ActionResult UpdateEmployee(string EmpId)
        {
            var model = new StoreUpdateEmployeeProfile();
            var modelData = new List<StoreUpdateEmployeeProfile>();
            modelData = new GetUpdateProfile().GetStoreGetUpdateProfile(EmpId);
            ViewBag.UpdateStatus = "";
            if (modelData != null && modelData.Count > 0)
            {
                model = modelData.FirstOrDefault();
            }
            LoadStatusEmp();
            LoadDirector();
            LoadEmployeeLevel();
            LoadDepartments();
            LoadEmpType();
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
                string usr = Session["EmpId"].ToString();
                updateEmployee = new UpdateProfileEmployee().Update(store, usr);
                ViewBag.UpdateStatus = "Success";
                LoadStatusEmp();
                LoadDirector();
                LoadDepartments();
                LoadEmpType();
                LoadEmployeeLevel();
                return View(store);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.ToString();
                LoadDirector();
                LoadDepartments();
                LoadEmpType();
                LoadEmployeeLevel();
                return View(store);
            }
        }
        public ActionResult ApprovflowSetting()
        {
            var list = LoadGroupApprovalDropdown();
            ViewBag.apprvFlow = list.ToArray();
            LoadDepartments();
            return View();
        }
        [HttpPost]
        public ActionResult ApprovflowSetting(string DeptID, string ApprvGrp)
        {
            var list = LoadGroupApprovalDropdown();
            LoadDepartments();
            if (!string.IsNullOrEmpty(DeptID))
            {
                list = list.Where(emp => emp.DepId.Trim() == DeptID.Trim()).ToList();
            }
            if (!string.IsNullOrEmpty(ApprvGrp))
            {
                list = list.Where(emp => emp.ApprGrpId.Trim() == ApprvGrp).ToList();
            }
            ViewBag.apprvFlow = list.ToList();
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

        public JsonResult GetApprovflowSettingEdit(string ApprvGrp)
        {
            string message = string.Empty;
            var getApproval = new List<ApprovalFlow>();
            try
            {

                getApproval = new GetApprovalFlowEdit().GetApprovalFlows(ApprvGrp);
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
        public JsonResult UpdateApprvFlow(string apprvID, string apprvName, string dept, string step, string empId, string action, string desc)
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
                cmd.Parameters.AddWithValue("@inApprvName", apprvName);
                cmd.Parameters.AddWithValue("@inDept", dept);
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
    }
}