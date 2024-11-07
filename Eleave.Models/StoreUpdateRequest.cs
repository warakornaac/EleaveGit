using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Eleave.Models
{
    public class StoreUpdateRequest
    {
        public string ReqNo { get; set; }
        public string EmpId { get; set; }
        public string ReqType { get; set; }
        public string LeaveType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string PeriodTime { get; set; }
        public double NumDay { get; set; }
        public int NumHour { get; set; }
        public string Remark { get; set; }

    }
}
