using System;
using System.Collections.Generic;
using System.Configuration;
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
            GetEmployee = new SearchEmployee().GetStoreSearchEmployee();

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
            GetEmployee = new SearchEmployee().GetStoreSearchEmployee();
            if (!string.IsNullOrEmpty(Company))
            {
                GetEmployee = GetEmployee.Where(emp => emp.Company == Company).ToList();
            }
            if (!string.IsNullOrEmpty(Department))
            {
                GetEmployee = GetEmployee.Where(emp => emp.DeptId.ToString().ToTrim() == Department).ToList();
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
            this.Session["EmpId"] = "6601002";
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

            LoadEmployeeLevel();
            LoadDepartments();
            LoadEmpType();
            return View(model);
        }
        public ActionResult Setting()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateEmployee(StoreUpdateEmployeeProfile store)
        {
            var updateEmployee = new List<StoreUpdateEmployeeProfile>();
            try
            {
                updateEmployee = new UpdateProfileEmployee().Update(store);
                ViewBag.UpdateStatus = "Success";
                LoadDepartments();
                LoadEmpType();
                LoadEmployeeLevel();
                return View(store);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.ToString();
                LoadDepartments();
                LoadEmpType();
                LoadEmployeeLevel();
                return View(store);
            }
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
        public void LoadEmployeeLevel()
        {
            var EMP_LVL = new List<StoreGetLookupData>();
            EMP_LVL = new GetLookupData().GetLookupDataStore("EMP_LVL");

            ViewBag.EmpLevel = EMP_LVL;

            //var empLevelList = EMP_LVL.Select(x => new SelectListItem
            //{
            //    Value = x.LookValue.ToString(),
            //    Text = x.LookDesc
            //}).ToList();

            //ViewBag.EmpLvl = empLevelList;
        }
        private void LoadEmpType()
        {
            var Emp_Type = new List<StoreGetLookupData>();
            Emp_Type = new GetLookupData().GetLookupDataStore("EMP_TYPE");

            ViewBag.EmpTyp = Emp_Type;
        }
        private void LoadDepartments()
        {
            List<Department> departments = new List<Department>();
            var connectionString = ConfigurationManager.ConnectionStrings["HRIS_DB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [HRIS].[dbo].[Department]", conn);
            SqlDataReader reader_dept = cmd.ExecuteReader();
            while (reader_dept.Read())
            {
                departments.Add(new Department
                {
                    DeptId = reader_dept["DeptId"].ToString(),
                    DeptName = reader_dept["DeptName"].ToString()
                });
            }
            reader_dept.Close();
            reader_dept.Dispose();
            cmd.Dispose();
            conn.Close();
            ViewBag.DepartmentList = departments;
        }
    }
}