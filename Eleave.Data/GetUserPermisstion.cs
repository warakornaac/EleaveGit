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
    public class GetUserPermisstion : MsSQL
    {
        public GetUserPermisstion() : base(Utils.GetConfig("HRIS_DB"))
        {

        }
        public List<UserPermission> GetUser_Permissions()
        {
            var p = new SqlParameters();

            return ConvertExtension.ConvertDataTable<UserPermission>(GetData(CmdStore("P_Get_UserPermission", p)));
        }
    }
}
