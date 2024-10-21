using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleave.Models
{
    public class StoreGetProfile
    {
        public string Company { get; set; }
        public string CountryCode { get; set; }
        public int EmpId { get; set; }
        public string TitleName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Fullname { get; set; }
        public string Gender { get; set; }
        public string DeptName { get; set; }
        public string Position { get; set; }
        public string EmpLvlName { get; set; }
        public string EmpTypeName { get; set; }
        public string ApprGrpID { get; set; }
        public DateTime StartDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string EmpStatus { get; set; }
        public string UserType { get; set; }
        public string UserTypeName { get; set; }
        public string DirectorId { get; set; }
        public DateTime InsertedDate { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

    }
}
