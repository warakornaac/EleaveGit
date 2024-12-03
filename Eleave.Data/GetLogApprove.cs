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
    public class GetLogApprove : MsSQL
    {
        public GetLogApprove() : base(Utils.GetConfig("HRIS_DB"))
        {

        }
        public List<StoreGetLogApprove> LogApprove(string reqId)
        {
            var p = new SqlParameters();
            p.AddParams("@inReqId", reqId);

            var table = GetData(CmdStore("P_Get_Log_Approve", p));
            return ConvertExtension.ConvertDataTable<StoreGetLogApprove>(GetData(CmdStore("P_Get_Log_Approve", p)));
        }
    }
}
