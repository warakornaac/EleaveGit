using Eleave.Library;
using Eleave.Models;
using My.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        public List<StoreUpdateEmployeeProfile> Save(string Company, string CountryCode, string EmpId, string TitleName, string FirstName, string LastName, string Gender, string DeptId, string Position, string EmpLvl, string EmpTypeId, string ApprGrpId, string StartDate, string Email, string EmpStatus, string UserType, string DirectorId, string InsertedBy)
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
            p.AddParams("@inApprGrpId", ApprGrpId);
            p.AddParams("@inStartDate", StartDate);
            p.AddParams("@inEmail", Email);
            p.AddParams("@inEmpStatus", EmpStatus);
            p.AddParams("@inUserType", UserType);
            p.AddParams("@inDirectorId", DirectorId);
            p.AddParams("@inInsertedBy", InsertedBy);

            //var table = GetData(CmdStore("P_Import_Employee", p));
            //return ConvertExtension.ConvertDataTable<StoreUpdateEmployeeProfile>(GetData(CmdStore("P_Import_Employee", p)));
            // เพิ่ม parameter สำหรับรับค่า @OutGenstatus
            //SqlParameter outGenstatus = new SqlParameter("@OutGenstatus", SqlDbType.NVarChar, 100)
            //{
            //    Direction = ParameterDirection.Output
            //};
            //p.AddParams("@outGenstatus", outGenstatus);

            SqlParameter outGenstatus = new SqlParameter("@outGenstatus", SqlDbType.NVarChar, 100);
            outGenstatus.Direction = System.Data.ParameterDirection.Output;
            //p.AddParams(outGenstatus);

            // รัน Stored Procedure
            var cmd = CmdStore("P_Import_Employee", p); // สมมติว่าฟังก์ชัน CmdStore สร้าง SqlCommand ที่พร้อมใช้งาน
            ExecuteNoneQuery(cmd); // รันคำสั่ง SQL ผ่าน ExecuteNonQuery หรือ ExecuteReader (ขึ้นอยู่กับลักษณะการใช้งาน)

            // ดึงค่าผลลัพธ์จาก @OutGenstatus
            string outStatus = outGenstatus.Value.ToString();

            // ถ้าคุณต้องการคืนค่าเป็น List<StoreUpdateEmployeeProfile>
            // คุณอาจจะเพิ่มการจัดการกับข้อมูลที่ถูกส่งกลับจากฐานข้อมูล
            var resultList = new List<StoreUpdateEmployeeProfile>();

            // ตรวจสอบสถานะการดำเนินการจาก @OutGenstatus
            if (outStatus == "Success")
            {
                // หากสถานะสำเร็จให้ทำอะไรบางอย่าง เช่น เพิ่มข้อมูลลงใน resultList
                // คุณสามารถเพิ่มเติมการจัดการข้อมูลที่ได้รับจากฐานข้อมูลที่นี่
            }
            else
            {
                // ถ้าสถานะไม่สำเร็จ อาจจะต้องจัดการตามกรณี
            }

            // คืนค่าผลลัพธ์
            return resultList;
        }
    }
}
