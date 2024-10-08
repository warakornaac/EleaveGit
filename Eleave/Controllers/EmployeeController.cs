using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eleave.Data;
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
            return View("ManagerEmployee", new
            {
                @ViewBag.EmployeeList
            });
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
            return View("ViewProfile", new
            {
                @ViewBag.ProfileList
            });
        } 
        
        public ActionResult UpdateEmployee()
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
            return View("UpdateEmployee", new
            {
                @ViewBag.ProfileList
            });
        }
    }
}