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
        public int LeaveTaken { get; set; }
        public int ClosingBal { get; set; }
        public int AccuredLeave { get; set; }
        public int WaitApprove { get; set; }
    }
}
