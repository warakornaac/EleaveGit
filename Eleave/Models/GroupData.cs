using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eleave.Models
{
    public class Department
    {
        public string DeptId { get; set; }
        public string DeptName { get; set; }
    }
    public class LeaveHisDemo
    {
        public string LeavId { get; set; }
        public string ReqType { get; set; }
        public string LeaveType { get; set; }
        public string ReqBy { get; set; }
        public string TotalReq { get; set; }
        public string ApprvBy { get; set; }
        public string HrBy { get; set; }
        public DateTime ReqDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ApprDate { get; set; }
        public DateTime HrDate { get; set; }
        public string ReqStatus { get; set; }

    }
}