using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Eleave.Models
{
    public class StoreUpdateEmployeeProfile
    {

        public string Company { get; set; }
        public int EmpId { get; set; }
        public string TitleName { get; set; }
        [Required(ErrorMessage = "กรุณากรอกชื่อ")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "กรุณากรอกนามสกุล")]
        public string LastName { get; set; }
        public string DeptId { get; set; }
        public string Position { get; set; }
        public string EmpLvl { get; set; }
        public string EmpTypeId { get; set; }
        public string ApprGrpID { get; set; }
        public string StartDate { get; set; }
        public string EmpStatus { get; set; }
        public string DirectorId { get; set; }

    }
}
