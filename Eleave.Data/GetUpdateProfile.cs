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
    public class GetUpdateProfile : MsSQL
    {
        public GetUpdateProfile() : base(Utils.GetConfig("HRIS_DB"))
        {

        }
        public List<StoreUpdateEmployeeProfile> GetStoreGetUpdateProfile(string empId)
        {
            var p = new SqlParameters();
            p.AddParams("@inEmpId", empId);

            var table = GetData(CmdStore("P_Get_Update_Profile", p));
            return ConvertExtension.ConvertDataTable<StoreUpdateEmployeeProfile>(GetData(CmdStore("P_Get_Update_Profile", p)));
        }
    }
}
