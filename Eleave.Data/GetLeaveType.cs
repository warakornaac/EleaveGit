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
    public class GetLeaveType : MsSQL
    {
        public GetLeaveType() : base(Utils.GetConfig("HRIS_DB"))
        {

        }
        public List<LeaveTypeModel> GetLeaveTypeList()
        {
            var p = new SqlParameters();

            var table = GetData(CmdStore("P_Get_LeaveType_List", p));
            return ConvertExtension.ConvertDataTable<LeaveTypeModel>(GetData(CmdStore("P_Get_LeaveType_List", p)));
        }
    }
}
