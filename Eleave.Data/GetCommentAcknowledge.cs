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
    public class GetCommentAcknowledge : MsSQL
    {
        public GetCommentAcknowledge() : base(Utils.GetConfig("HRIS_DB")) { }
        public List<ApproveCommentRequest> GetComment(string ReqNo)
        {
            var p = new SqlParameters();
            p.AddParams("@inReqNo", ReqNo);


            var table = GetData(CmdStore("P_Get_Comment_Acknowledge_Detail", p));
            return ConvertExtension.ConvertDataTable<ApproveCommentRequest>(GetData(CmdStore("P_Get_Comment_Acknowledge_Detail", p)));
        }
    }
}
