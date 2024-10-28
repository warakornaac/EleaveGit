using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleave.Models
{
    public class UserPermission
    {
        public int RowId { get; set; }
        public string CountryCode { get; set; }
        public string UsrPerID { get; set; }
        public string UsrType { get; set; }
        public string UsrDesc { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public Boolean Approval { get; set; }
        public Boolean Request { get; set; }
        public Boolean Actknowlege { get; set; }
        public Boolean Setting { get; set; }

    }
}
