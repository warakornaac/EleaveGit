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
    public class SearchAcknowledgeRequest : MsSQL
    {
        public SearchAcknowledgeRequest() : base(Utils.GetConfig("HRIS_DB")) { }
        public List<ApprovalRequest> GetAck(string user, string LeaveType, string reqType, string ReqStatus, DateTime? ReqStart, DateTime? ReqEnd, string Department, string reqId, string empName, string flag)
        {
            var p = new SqlParameters();
            p.AddParams("@inUser", user);
            p.AddParams("@inReqSub", reqType);
            p.AddParams("@inReqSta", ReqStatus);
            p.AddParams("@inStartDate", ReqStart);
            p.AddParams("@inEndDate", ReqEnd);
            p.AddParams("@inDepartment", Department);
            p.AddParams("@inReqId", reqId);
            p.AddParams("@inEmpName", empName);
            p.AddParams("@inSchFlag", flag);

            var table = GetData(CmdStore("P_Search_Acknowledge_Request", p));
            return ConvertExtension.ConvertDataTable<ApprovalRequest>(GetData(CmdStore("P_Search_Acknowledge_Request", p)));
        }
    }
}
