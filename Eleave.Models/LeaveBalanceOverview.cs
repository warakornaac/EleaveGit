using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleave.Models
{
    public class LeaveBalanceOverview
    {
        public string ID { get; set; }
        public string LeaveType { get; set; }
        public decimal LeaveTaken { get; set; }
        public decimal ClosingBal { get; set; }
        public decimal AccuredLeave { get; set; }
        public decimal WaitApprove { get; set; }
    }
}
