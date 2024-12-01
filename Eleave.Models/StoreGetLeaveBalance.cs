using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleave.Models
{
    public class StoreGetLeaveBalance
    {
        public string EmpId { get; set; }
        public string Fullname { get; set; }
        public string LeaveType { get; set; }
        public string Description { get; set; }
        public decimal AccuredLeave { get; set; }
        public decimal LeaveTaken { get; set; }
        public decimal ClosingBal { get; set; }
        public decimal WaitApprove { get; set; }
    }
}
