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
    public class GetRequestAcknowledge : MsSQL
    {
        public GetRequestAcknowledge() : base(Utils.GetConfig("HRIS_DB"))
        { }
        public List<ApprovalRequest> GetRequestsAck(string user)
        {
            var p = new SqlParameters();
            p.AddParams("@inUser", user);

            var table = GetData(CmdStore("P_Get_Request_Acknowledge", p));
            return ConvertExtension.ConvertDataTable<ApprovalRequest>(GetData(CmdStore("P_Get_Request_Acknowledge", p)));
        }
    }
}
