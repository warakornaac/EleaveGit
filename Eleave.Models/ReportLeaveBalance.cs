using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleave.Models
{
    public class ReportLeaveBalance
    {
        public string Company { get; set; }
        public string LeaveYear { get; set; }
        public string DeptNameShort { get; set; }
        public string DeptName { get; set; }
        public string EmpId { get; set; }
        public string Position { get; set; }
        public string StartDate { get; set; }
        public string Fullname { get; set; }
        public decimal AL02_Accured { get; set; }
        public decimal AL02_Leave { get; set; }
        public decimal AL02_Balance { get; set; }
        public decimal SL01_Accured { get; set; }
        public decimal SL01_Leave { get; set; }
        public decimal SL01_Balance { get; set; }
        public decimal CP03_Accured { get; set; }
        public decimal CP03_Leave { get; set; }
        public decimal CP03_Balance { get; set; }
        public decimal OL07_Accured { get; set; }
        public decimal ML06_Accured { get; set; }
        public decimal Other_Accured { get; set; }

    }
}
