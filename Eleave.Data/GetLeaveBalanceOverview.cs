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
    public class GetLeaveBalanceOverview : MsSQL
    {
        public GetLeaveBalanceOverview() : base(Utils.GetConfig("HRIS_DB"))
        {

        }
        public List<LeaveBalanceOverview> Get(string EmpID)
        {
            var p = new SqlParameters();
            p.AddParams("@inEmpId", EmpID);

            var table = GetData(CmdStore("P_Leave_Balance_Overview", p));
            return ConvertExtension.ConvertDataTable<LeaveBalanceOverview>(GetData(CmdStore("P_Leave_Balance_Overview", p)));
        }
    }
}
