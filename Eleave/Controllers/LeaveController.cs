using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eleave.Data;
using Eleave.Models;
using Eleave.Library;
using System.IO;

namespace Eleave.Controllers
{
    public class LeaveController : Controller
    {
        // GET: Leave
        public ActionResult RequestForm()
        {
            var DocumentRequest = Utils.GetDocumentRequest("");

            ViewBag.DocumentRequest = DocumentRequest;
            return View(new FileUploadModel());
        }
        [HttpPost]
        public ActionResult SaveRequestForm(string ReqNo)
        {
            string fileNameNew = string.Empty;
            if (Request.Files != null) { 
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    var originalFileName = Path.GetFileName(file.FileName);
                    var fileExtension = Path.GetExtension(originalFileName);

                    fileNameNew = ReqNo + "-" + (i + 1) + fileExtension;
                    var path = Path.Combine(Server.MapPath("~/FileUpload/"), fileNameNew);
                    file.SaveAs(path);
                }
            }

            return View("RequestForm");
        }
        public ActionResult ManagerHistory()
        {
            return View();
        }
    }
}