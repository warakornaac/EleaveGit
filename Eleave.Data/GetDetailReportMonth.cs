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
    public class GetDetailReportMonth : MsSQL
    {
        public GetDetailReportMonth() : base(Utils.GetConfig("HRIS_DB")) { }

        public List<DetailReport> GetDetail(string EmpID, string LeavTyp, string StartDate, string EndDate)
        {
            var p = new SqlParameters();
            p.AddParams("@inEmpId", EmpID);
            p.AddParams("@inLevTyp", LeavTyp);
            p.AddParams("@inStartDate", StartDate);
            p.AddParams("@inEndDate", EndDate);
            var table = GetData(CmdStore("P_Get_Detail_Report_Month", p));
            return ConvertExtension.ConvertDataTable<DetailReport>(GetData(CmdStore("P_Get_Detail_Report_Month", p)));
        }
    }
}
