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
    public class GetProfile : MsSQL
    {
        public GetProfile() : base(Utils.GetConfig("HRIS_DB"))
        {

        }
        public List<StoreGetProfile> GetStoreGetProfile(string empId)
        {
            var p = new SqlParameters();
            p.AddParams("@inEmpId", empId);

            var table = GetData(CmdStore("P_Get_Profile", p));
            return ConvertExtension.ConvertDataTable<StoreGetProfile>(GetData(CmdStore("P_Get_Profile", p)));
        }
    }
}
