using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleave.Models
{
    public class ApprovalRequest
    {
        public string Company { get; set; }
        public string ReqNo { get; set; }
        public string ReqType { get; set; }
        public string CountryCode { get; set; }
        public string EmpId { get; set; }
        public string Empname { get; set; }
        public string LeaveType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public decimal NumDay { get; set; }
        public string NumDayCal { get; set; }
        public string ReqDate { get; set; }
        public string ApprGrpID { get; set; }
        public string ApproverId { get; set; }
        public string ReqStatus { get; set; }
        public string ReqStaDesc { get; set; }
        public string Remark { get; set; }
        public string ApproveBy { get; set; }
        public string ApproveDate { get; set; }
        public string AcknowledgeBy { get; set; }
        public string AcknowledgeDate { get; set; }
    }
}
