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
    public class GetReportLeaveLate : MsSQL
    {
        public GetReportLeaveLate() : base(Utils.GetConfig("HRIS_DB")) { }
        public List<ReportLeaveLate> GetReports(string Company, string Dept, string Month)
        {
            var p = new SqlParameters();
            p.AddParams("@inCompany", Company);
            p.AddParams("@inDept", Dept);
            p.AddParams("@inMonth", Month);
            var table = GetData(CmdStore("P_Report_Leave_Attend", p));
            return ConvertExtension.ConvertDataTable<ReportLeaveLate>(GetData(CmdStore("P_Report_Leave_Attend", p)));
        }
    }
}
