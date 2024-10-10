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
    public class UpdateProfileEmployee : MsSQL
    {
        public UpdateProfileEmployee() : base(Utils.GetConfig("HRIS_DB"))
        {

        }
        public List<StoreUpdateEmployeeProfile> Update(StoreUpdateEmployeeProfile profile)
        {
            var p = new SqlParameters();
            p.AddParams("@inEmpId", profile.EmpId);
            p.AddParams("@inCompany", profile.Company);
            p.AddParams("@inPrefix", profile.TitleName);
            p.AddParams("@inFirstName", profile.FirstName);
            p.AddParams("@inLastName", profile.LastName);
            p.AddParams("@inDeptId", profile.DeptId);
            p.AddParams("@inPosition", profile.Position);
            p.AddParams("@inDirectorId", profile.DirectorId);
            p.AddParams("@inEmpLvl", profile.EmpLvl);
            p.AddParams("@inEmpTypeId", profile.EmpTypeId);
            p.AddParams("@inEmpStatus", profile.EmpStatus);

            return ConvertExtension.ConvertDataTable<StoreUpdateEmployeeProfile>(GetData(CmdStore("P_Update_Profile_Employee", p)));
        }
    }
}
