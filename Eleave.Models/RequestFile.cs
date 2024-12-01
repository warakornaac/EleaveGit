using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleave.Models
{
    public class RequestFile
    {
        public int Id { get; set; }
        public string ReqNo { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileNo { get; set; }
        public string InsertedBy { get; set; }
        public string InsertedDate { get; set; }
    }
}
