using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleave.Models
{
    public class ApprovalRequest
    {
        public string ReqNo { get; set; }
        public string ReqType { get; set; }
        public string CountryCode { get; set; }
        public string EmpId { get; set; }
        public string Empname { get; set; }
        public string LeaveType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal NumDay { get; set; }
        public DateTime? ReqDate { get; set; }
        public string ApprGrpID { get; set; }
        public string ApproverId { get; set; }
        public string ReqStatus { get; set; }
        public string ReqStaDesc { get; set; }
        public string Remark { get; set; }
        public string AcceptBy { get; set; }
        public DateTime? AcceptDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
