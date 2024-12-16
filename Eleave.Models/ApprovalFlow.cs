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
        public string ApprGrpName { get; set; }
        public string DepId { get; set; }
        public string DeptShort { get; set; }
        public string DepName { get; set; }
        public int ApprStep { get; set; }
        public string EmpId { get; set; }
        public string EmpName { get; set; }
        public string ActionType { get; set; }
        public string ApprDesc { get; set; }
    }
}
