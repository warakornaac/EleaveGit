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
    public class GetLeaveConsumptionOverview : MsSQL
    {
        public GetLeaveConsumptionOverview() : base(Utils.GetConfig("HRIS_DB"))
        {

        }
        public List<LeaveOverview> Get(string EmpID)
        {
            var p = new SqlParameters();
            p.AddParams("@inEmpId", EmpID);

            var table = GetData(CmdStore("P_Leave_Consumption_Overview", p));
            return ConvertExtension.ConvertDataTable<LeaveOverview>(GetData(CmdStore("P_Leave_Consumption_Overview", p)));
        }

    }
}
