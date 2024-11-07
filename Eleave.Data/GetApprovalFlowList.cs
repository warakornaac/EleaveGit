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
    public class GetApprovalFlowList : MsSQL
    {
        public GetApprovalFlowList() : base(Utils.GetConfig("HRIS_DB"))
        {

        }
        public List<ApprovalFlow> Get()
        {
            var p = new SqlParameters();

            var table = GetData(CmdStore("P_Get_ApprovalFlow_List", p));
            return ConvertExtension.ConvertDataTable<ApprovalFlow>(GetData(CmdStore("P_Get_ApprovalFlow_List", p)));
        }
    }
}
