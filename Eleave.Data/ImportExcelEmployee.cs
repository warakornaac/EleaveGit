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
    public class ImportExcelEmployee : MsSQL
    {
        public ImportExcelEmployee() : base(Utils.GetConfig("HRIS_DB"))
        {

        }
        public List<StoreUpdateEmployeeProfile> Save(string Company, string CountryCode, string EmpId, string TitleName, string FirstName, string LastName, string Gender, string DeptId, string Position, string EmpLvl, string EmpTypeId, string StartDate, string Email, string EmpStatus, string UserType, string DirectorId, string InsertedBy)
        {
            var p = new SqlParameters();

            p.AddParams("@inCompany", Company);
            p.AddParams("@inCountryCode", CountryCode);
            p.AddParams("@inEmpId", EmpId);
            p.AddParams("@inTitleName", TitleName);
            p.AddParams("@inFirstName", FirstName);
            p.AddParams("@inLastName", LastName);
            p.AddParams("@inGender", Gender);
            p.AddParams("@inDeptId", DeptId);
            p.AddParams("@inPosition", Position);
            p.AddParams("@inEmpLvl", EmpLvl);
            p.AddParams("@inEmpTypeId", EmpTypeId);
            p.AddParams("@inStartDate", StartDate);
            p.AddParams("@inEmail", Email);
            p.AddParams("@inEmpStatus", EmpStatus);
            p.AddParams("@inUserType", UserType);
            p.AddParams("@inDirectorId", DirectorId);
            p.AddParams("@inInsertedBy", InsertedBy);

            //var table = GetData(CmdStore("P_Import_Employee", p));
            return ConvertExtension.ConvertDataTable<StoreUpdateEmployeeProfile>(GetData(CmdStore("P_Import_Employee", p)));
        }
    }
}
