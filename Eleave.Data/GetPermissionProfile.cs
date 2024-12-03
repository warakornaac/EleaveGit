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
    public class GetPermissionProfile : MsSQL
    {
        public GetPermissionProfile() : base(Utils.GetConfig("HRIS_DB")) { }
        public List<PermissionProfile> Get(string EmpId)
        {
            var p = new SqlParameters();
            p.AddParams("@inEmpId", EmpId);


            var table = GetData(CmdStore("P_Get_Permission_Profile", p));
            return ConvertExtension.ConvertDataTable<PermissionProfile>(GetData(CmdStore("P_Get_Permission_Profile", p)));
        }
    }
}
