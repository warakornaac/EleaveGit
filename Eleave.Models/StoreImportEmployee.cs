using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleave.Models
{
    public class StoreImportEmployee
    {
        public string Company { get; set; }
        public string CountryCode { get; set; }
        public string EmpId { get; set; }
        public string TitleName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string DeptId { get; set; }
        public string Position { get; set; }
        public string EmpLvl { get; set; }
        public string EmpTypeId { get; set; }
        public string ApprGrpID { get; set; }
        public string StartDate { get; set; }
        public string Email { get; set; }
        public string EmpStatus { get; set; }
        public string UserType { get; set; }
        public string DirectorId { get; set; }
        public string InsertedBy { get; set; }
        public string InsertedDate { get; set; }
        public string StatusImport { get; set; }
        public string ErrorImport { get; set; }
    }
}
