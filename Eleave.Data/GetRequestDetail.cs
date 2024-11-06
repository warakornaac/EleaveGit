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
    public class GetRequestDetail : MsSQL
    {
        public GetRequestDetail() : base(Utils.GetConfig("HRIS_DB"))
        {

        }
        public List<RequestList> Get(string ReqNO)
        {
            var p = new SqlParameters();
            p.AddParams("@inReqNo", ReqNO);

            var table = GetData(CmdStore("P_Get_Request_Detail", p));
            return ConvertExtension.ConvertDataTable<RequestList>(GetData(CmdStore("P_Get_Request_Detail", p)));
        }
    }
}
