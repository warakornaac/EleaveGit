using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleave.Models
{
    public class DetailReport
    {
        public string EmpId { get; set; }
        public string Fullname { get; set; }
        public string ReqNo { get; set; }
        public string LeaveType { get; set; }
        public string Description { get; set; }
        public string ReqDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string TimeLev { get; set; }
        public string RequestStatus { get; set; }
        public decimal NumDay { get; set; }
        public string CalculatedHour { get; set; }
        public int NumHour { get; set; }
        public string PeriodTime { get; set; }
        public string AttachFile { get; set; }
    }
}
