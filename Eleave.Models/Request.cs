using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleave.Models
{
    public class Request
    {
        public string ReqNo { get; set; }
        public string ReqType { get; set; }
        public int CountryCode { get; set; }
        public int EmpId { get; set; }
        public int ReqId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ReqDate { get; set; }
        public int ApprGrdId { get; set; }
        public string RequestStatus { get; set; }
        public string AcceptBy { get; set; }
        public DateTime AcceptDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateBy { get; set; }
    }
}
