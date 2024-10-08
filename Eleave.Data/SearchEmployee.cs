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
    public class SearchEmployee : MsSQL
    {
        public SearchEmployee() : base(Utils.GetConfig("HRIS_DB"))
        {

        }
        public List<StoreGetProfile> GetStoreSearchEmployee()
        {
            var p = new SqlParameters();
            var table = GetData(CmdStore("P_Search_Employee", p));
            return ConvertExtension.ConvertDataTable<StoreGetProfile>(GetData(CmdStore("P_Search_Employee", p)));
        }
    }
}
