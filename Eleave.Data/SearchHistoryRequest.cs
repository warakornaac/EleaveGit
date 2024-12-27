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
    public class SearchHistoryRequest : MsSQL
    {
        public SearchHistoryRequest() : base(Utils.GetConfig("HRIS_DB"))
        {

        }
        public List<RequestList> GetHis(string LeaveType, string reqType, string ReqStatus, DateTime? ReqStart, DateTime? ReqEnd, string Department, string reqId, string empName)
        {
            var p = new SqlParameters();
            p.AddParams("@inReqTyp", LeaveType);
            p.AddParams("@inReqSub", reqType);
            p.AddParams("@inReqSta", ReqStatus);
            p.AddParams("@inStartDate", ReqStart);
            p.AddParams("@inEndDate", ReqEnd);
            p.AddParams("@inDepartment", Department);
            p.AddParams("@inReqId", reqId);
            p.AddParams("@inEmpName", empName);

            var table = GetData(CmdStore("P_Search_History_Request", p));
            return ConvertExtension.ConvertDataTable<RequestList>(GetData(CmdStore("P_Search_History_Request", p)));

        }
    }
}
