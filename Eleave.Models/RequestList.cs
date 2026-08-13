using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleave.Models
{
    public class RequestList
    {
        public string ReqNo { get; set; } = string.Empty;
        public string ReqType { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public string EmpId { get; set; } = string.Empty;
        public string Empname { get; set; } = string.Empty;
        public string LeaveType { get; set; } = string.Empty;
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public decimal NumDay { get; set; } = 0;
        public string PeriodTime { get; set; } = string.Empty;
        public string ReqDate { get; set; } = string.Empty;
        public string ApprGrpID { get; set; } = string.Empty;
        public string ReqStatus { get; set; } = string.Empty;
        public string ReqStaDesc { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;
        public string ApproveBy { get; set; } = string.Empty;
        public string ApproveDate { get; set; } = string.Empty;
        public string AcknowledgeBy { get; set; } = string.Empty;
        public string AcknowledgeDate { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdateDate { get; set; } = null;
    }
}
