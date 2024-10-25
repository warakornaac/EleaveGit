using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleave.Models
{
    public class ApprovalFlow
    {
        public string CountryCode { get; set; }
        public string ApprGrpId { get; set; }
        public string ApprGrpNm { get; set; }
        public string DepId { get; set; }
        public int ApprStep { get; set; }
        public int EmpId { get; set; }
        public string ActionType { get; set; }
        public string ApprDesc { get; set; }
    }
}
