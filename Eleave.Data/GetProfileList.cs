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
    public class GetProfileList : MsSQL
    {
        public GetProfileList() : base(Utils.GetConfig("HRIS_DB"))
        {

        }
        public List<StoreGetProfile> GetProfileListS()
        {
            var p = new SqlParameters();
            var table = GetData(CmdStore("P_Get_Profile_List", p));
            return ConvertExtension.ConvertDataTable<StoreGetProfile>(GetData(CmdStore("P_Get_Profile_List", p)));
        }

    }
}
