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
    public class GetDetailReport : MsSQL
    {
        public GetDetailReport() : base(Utils.GetConfig("HRIS_DB")) { }
        public List<DetailReport> Get(string empId, string levtyp, string year)
        {
            var p = new SqlParameters();
            p.AddParams("@inEmpId", empId);
            p.AddParams("@inLevTyp", levtyp);
            p.AddParams("@inyear", year);

            var table = GetData(CmdStore("P_Get_Detail_Report", p));
            return ConvertExtension.ConvertDataTable<DetailReport>(GetData(CmdStore("P_Get_Detail_Report", p)));
        }
    }
}
