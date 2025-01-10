using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleave.Models
{
    public class ReportLeaveBalanceMonth
    {
        public string Company { get; set; }
        public string EmpId { get; set; }
        public string EmpName { get; set; }
        public string DeptNameShort { get; set; }
        public string DeptName { get; set; }
        public string Position { get; set; }
        public string StartDate { get; set; }
        public decimal Vacation { get; set; }
        public decimal SickLeave { get; set; }
        public decimal PersonalLeave { get; set; }
        public decimal OrdinationLeave { get; set; }
        public decimal MaternityLeave { get; set; }
        public decimal Other { get; set; }

    }
}
