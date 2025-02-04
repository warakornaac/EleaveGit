using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleave.Models
{
    public class ReportLeaveLate
    {
        public string Company { get; set; }
        public string EmpId { get; set; }
        public string EmpName { get; set; }
        public string DateMonth { get; set; }
        public string Attend { get; set; }
        public string Finish { get; set; }
        public string Late { get; set; }
        public string BeforeW { get; set; }
        public string Missing { get; set; }
        public string Exception { get; set; }
        public string Department { get; set; }
        public string Total_late { get; set; }
        public string Late_work { get; set; }
        public string Missing_work { get; set; }
        public string AnnualLeave { get; set; }
        public string CompensateLeave { get; set; }
        public string BusinessLeave { get; set; }
        public string SickLeave { get; set; }
        public string BereavementLeave { get; set; }
        public string ContraceptiveLeave { get; set; }
        public string GraduationLeave { get; set; }
        public string MaternityLeave { get; set; }
        public string MilitaryLeave { get; set; }
        public string OrdinationLeave { get; set; }
        public string ProfessionalLeave { get; set; }
        public string OtherLeave { get; set; }
        public string WFH { get; set; }
        public string WFS { get; set; }

    }
}
