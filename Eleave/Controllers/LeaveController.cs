using System;
using System.Collections.Generic;
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
            return View();
        }
    }
}