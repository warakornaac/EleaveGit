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
            return View();

            //return RedirectToAction("ManagerEmployee", new
            //{
            //    @ViewBag.EmployeeList,
            //    @ViewBag.DepartmentList,
            //});
        }
        [HttpPost]
        public ActionResult SearchUser(int? EmpId, string Department, string Name, string Level)
        {
            var GetEmployee = new List<StoreGetProfile>();
            GetEmployee = new SearchEmployee().GetStoreSearchEmployee();
            if (EmpId.HasValue)
            {
                GetEmployee = GetEmployee.Where(emp => emp.EmpId.ToString().Contains(EmpId.ToString())).ToList();
            }
            if (!string.IsNullOrEmpty(Department))
            {
                GetEmployee = GetEmployee.Where(emp => emp.DeptId.ToString().ToTrim() == Department).ToList();
            }
            if (!string.IsNullOrEmpty(Name))
            {
                GetEmployee = GetEmployee.Where(emp => emp.Fullname.Contains(Name.Trim())).ToList();
            }

            if (!string.IsNullOrEmpty(Level))
            {
                GetEmployee = GetEmployee.Where(emp => emp.EmpLvl.Contains(Level.Trim())).ToList();
            }
            ViewBag.EmployeeList = GetEmployee;
            LoadDepartments();

            return View("ManagerEmployee");
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
            LoadDepartments();
            return View(model);
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
                return View(store);
            }
            catch
            {
                ViewBag.UpdateStatus = "Error";
                LoadDepartments();
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