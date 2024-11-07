using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Data;
using Eleave.Library;
using Eleave.Models;

namespace Eleave.Data
{
    public class UpdateRequest : MsSQL
    {
        public UpdateRequest() : base(Utils.GetConfig("HRIS_DB"))
        {

        }
        public List<StoreUpdateRequest> Save(string ReqNo, string EmpId, string LeaveType, string StartDate, string EndDate, double NumDay, int NumHour, string Remark)
        {
            var p = new SqlParameters();
            p.AddParams("@inReqNo", ReqNo.ToTrim());
            p.AddParams("@inEmpId", EmpId.ToTrim());
            p.AddParams("@inLeaveType", LeaveType.ToTrim());
            p.AddParams("@inStartDate", StartDate.ToTrim());
            p.AddParams("@inEndDate", EndDate.ToTrim());
            p.AddParams("@inNumDay", NumDay);
            p.AddParams("@inNumHour", NumHour);
            p.AddParams("@inRemark", Remark.ToTrim());

            p.AddParams("@outGenstatus", '');

            SqlParameter p = new SqlParameter("@outResult", SqlDbType.NVarChar, 1000);

            var table = GetData(CmdStore("P_Save_Request", p));
            return ConvertExtension.ConvertDataTable<StoreUpdateRequest>(GetData(CmdStore("P_Save_Request", p)));
        }
    }
}


//tac 3461-8534C
//453