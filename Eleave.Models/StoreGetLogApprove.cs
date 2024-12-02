using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleave.Models
{
    public class StoreGetLogApprove
    {
        public string ReqID { get; set; }
        public string ApprStep { get; set; }
        public string ApproveDate { get; set; }
        public string ApproveBy { get; set; }
        public string ApproveByName { get; set; }
        public string ApproveEmail { get; set; }
        public string RequestStatus { get; set; }
        public string RequestStatusName { get; set; }
        public string ApprComment { get; set; }
    }
}
