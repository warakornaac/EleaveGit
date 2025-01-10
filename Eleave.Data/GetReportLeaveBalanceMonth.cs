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
    public class GetReportLeaveBalanceMonth : MsSQL
    {
        public GetReportLeaveBalanceMonth() : base(Utils.GetConfig("HRIS_DB")) { }
        public List<ReportLeaveBalanceMonth> GetReportMonth(string COMP, string DEPT, string STARTDATE, string ENDDATE)
        {
            var p = new SqlParameters();
            p.AddParams("@inComp", COMP);
            p.AddParams("@inDept", DEPT);
            p.AddParams("@inStartdate", STARTDATE);
            p.AddParams("@inEnddate", ENDDATE);
            var table = GetData(CmdStore("P_Report_Leave_Balance_month", p));
            return ConvertExtension.ConvertDataTable<ReportLeaveBalanceMonth>(GetData(CmdStore("P_Report_Leave_Balance_month", p)));

        }
    }
}
