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
    public class GetLeaveBalance : MsSQL
    {
        public GetLeaveBalance() : base(Utils.GetConfig("HRIS_DB"))
        {

        }
        public List<StoreGetLeaveBalance> LeaveBalance(string empId)
        {
            var p = new SqlParameters();
            p.AddParams("@inEmpId", empId);

            var table = GetData(CmdStore("P_Get_Leave_Balance", p));
            return ConvertExtension.ConvertDataTable<StoreGetLeaveBalance>(GetData(CmdStore("P_Get_Leave_Balance", p)));
        }
    }
}
