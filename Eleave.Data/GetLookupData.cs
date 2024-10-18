using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eleave.Models;
using Eleave.Library;
using My.Data;

namespace Eleave.Data
{
    public class GetLookupData : MsSQL
    {
        public GetLookupData() : base(Utils.GetConfig("HRIS_DB"))
        {

        }
        public List<StoreGetLookupData> GetLookupDataStore(string Lookcod)
        {
            var p = new SqlParameters();
            p.AddParams("@inLookCode", Lookcod);
            var table = GetData(CmdStore("P_Get_LookupList", p));
            return ConvertExtension.ConvertDataTable<StoreGetLookupData>(GetData(CmdStore("P_Get_LookupList", p)));
        }
    }
}
