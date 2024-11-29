using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleave.Models
{
    public class LeaveOverview
    {
        public int ID { get; set; }
        public string LeaveType { get; set; }
        public int TotalCount { get; set; }
    }
}
