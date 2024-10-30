using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleave.Models
{
    public class LeaveTypeModel
    {
        public string RequestID { get; set; }
        public string RequestType { get; set; }
        public string Description { get; set; }
        public string RequestGroup { get; set; }
        public string Remark { get; set; }
        public int ApplyBeforeDay { get; set; }
        public decimal MinHour { get; set; }
        public decimal MaxHour { get; set; }
        public int CarryForward { get; set; }
        public Boolean IsRequiredAttach { get; set; }
        public int MaxDay { get; set; }
        public Boolean AllowAdd { get; set; }

    }
}
