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
    public class GetApprovalRequest : MsSQL
    {
        public GetApprovalRequest() : base(Utils.GetConfig("HRIS_DB")) { }
        public List<ApprovalRequest> GetRequests(string Department)
        {
            var p = new SqlParameters();
            p.AddParams("@inDepartment", Department);

            var table = GetData(CmdStore("P_Get_Request_Approval", p));
            return ConvertExtension.ConvertDataTable<ApprovalRequest>(GetData(CmdStore("P_Get_Request_Approval", p)));
        }
    }
}
