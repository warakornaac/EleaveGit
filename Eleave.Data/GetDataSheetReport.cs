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
    public class GetDataSheetReport : MsSQL
    {
        public GetDataSheetReport() : base(Utils.GetConfig("HRIS_DB")) { }
        public List<ReportLeaveBalanceDetail> GetAck(string user, string Company, string LeaveType, string reqType, string ReqStatus, DateTime? ReqStart, DateTime? ReqEnd, string Department, string reqId, string empID, string flag)
        {
            var p = new SqlParameters();
            p.AddParams("@inUser", user);
            p.AddParams("@inCompany", Company);
            p.AddParams("@inReqTyp", LeaveType);
            p.AddParams("@inReqSub", reqType);
            p.AddParams("@inReqSta", ReqStatus);
            p.AddParams("@inStartDate", ReqStart);
            p.AddParams("@inEndDate", ReqEnd);
            p.AddParams("@inDepartment", Department);
            p.AddParams("@inReqId", reqId);
            p.AddParams("@inEmpId", empID);
            p.AddParams("@inSchFlag", flag);

            var table = GetData(CmdStore("P_Detail_Acknowledge_Request_Report", p));
            return ConvertExtension.ConvertDataTable<ReportLeaveBalanceDetail>(GetData(CmdStore("P_Detail_Acknowledge_Request_Report", p)));
        }

        public List<ReportLeaveBalanceDetail> GetBalanceManagerDetails(string user, string Company, string LeaveType, string reqType, string ReqStatus, DateTime? ReqStart, DateTime? ReqEnd, string Department, string reqId, string empID, string flag)
        {
            var p = new SqlParameters();
            p.AddParams("@inUser", user);
            p.AddParams("@inCompany", Company);
            p.AddParams("@inReqTyp", LeaveType);
            p.AddParams("@inReqSub", reqType);
            p.AddParams("@inReqSta", ReqStatus);
            p.AddParams("@inStartDate", ReqStart);
            p.AddParams("@inEndDate", ReqEnd);
            p.AddParams("@inDepartment", Department);
            p.AddParams("@inReqId", reqId);
            p.AddParams("@inEmpId", empID);
            p.AddParams("@inSchFlag", flag);

            var table = GetData(CmdStore("P_Detail_Report_Manager", p));
            return ConvertExtension.ConvertDataTable<ReportLeaveBalanceDetail>(GetData(CmdStore("P_Detail_Report_Manager", p)));
        }
    }
}
