using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Eleave.Models
{
    public class ModelLogin
    {
        public class LoginUserModels
        {
            [Required(ErrorMessage = "กรุณากรอกอีเมล")]
            //[DataType(DataType.EmailAddress)]
            [Display(Name = "อีเมล")]
            public string User { get; set; }

            [Required(ErrorMessage = "กรุณากรอกรหัสผ่าน")]
            [DataType(DataType.Password)]
            [StringLength(150, MinimumLength = 8, ErrorMessage = "ความยาวของ {0} ต้องอยู่ระหว่าง {2} ถึง {1} ตัวอักษร")]
            [Display(Name = "รหัสผ่าน")]
            public string Password { get; set; }
        }
    }
}