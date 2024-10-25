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
    public class GetApprovalFlowEdit : MsSQL
    {
        public GetApprovalFlowEdit() : base(Utils.GetConfig("HRIS_DB"))
        {

        }
        public List<ApprovalFlow> GetApprovalFlows(string ApprvID)
        {
            var p = new SqlParameters();
            p.AddParams("@inApprvID", ApprvID);
            return ConvertExtension.ConvertDataTable<ApprovalFlow>(GetData(CmdStore("P_Get_ApprovalFlow_edit", p)));
        }
    }
}
