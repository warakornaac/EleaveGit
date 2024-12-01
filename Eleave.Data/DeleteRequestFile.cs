using Eleave.Library;
using Eleave.Models;
using My.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleave.Data
{
    public class DeleteRequestFile : MsSQL
    {
        public DeleteRequestFile() : base(Utils.GetConfig("HRIS_DB"))
        {

        }
        public string Delete(string IdFile)
        {
            var getResult = string.Empty;
            var p = new SqlParameters();
            p.AddParams("@inIdFile", IdFile);
            var resultData = GetData(CmdStore("P_Delete_Request_File", p));
            if (resultData != null && resultData.Rows.Count > 0)
            {
                getResult = resultData.Rows[0][0].ToString();
                return getResult;  
            }
            return getResult;
        }
    }
}
