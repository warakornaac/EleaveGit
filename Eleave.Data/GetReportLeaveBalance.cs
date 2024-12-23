using Eleave.Library;
using Eleave.Models;
using My.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleave.Data
{
    public class GetReportLeaveBalance : MsSQL
    {
        public GetReportLeaveBalance() : base(Utils.GetConfig("HRIS_DB")) { }
        public List<ReportLeaveBalance> GetReportLeaveBalances(string Company, string Year, string Dept, string EmpID, string User)
        {
            var p = new SqlParameters();
            p.AddParams("@inComp", Company);
            p.AddParams("@inYear", Year);
            p.AddParams("@inDept", Dept);
            p.AddParams("@inEmpId", EmpID);
            p.AddParams("@inUser", User);
            var table = GetData(CmdStore("P_Report_Leave_Balance", p));
            return ConvertExtension.ConvertDataTable<ReportLeaveBalance>(GetData(CmdStore("P_Report_Leave_Balance", p)));
        }
    }
}
