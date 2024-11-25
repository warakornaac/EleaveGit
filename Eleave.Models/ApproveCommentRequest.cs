using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleave.Models
{
    public class ApproveCommentRequest
    {
        public string ReqID { get; set; }
        public string ApprGrpID { get; set; }
        public string ReqType { get; set; }
        public string ApprStep { get; set; }
        public string EmdID { get; set; }
        public string ApproveBy { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string RequestStatus { get; set; }
        public string ReqStaDesc { get; set; }
        public string ApprComment { get; set; }

    }
}
