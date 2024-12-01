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
    public class GetRequestFile : MsSQL
    {
        public GetRequestFile() : base(Utils.GetConfig("HRIS_DB"))
        {

        }
        public List<RequestFile> GetFile(string reqNo)
        {
            var p = new SqlParameters();
            p.AddParams("@inReqNo", reqNo);

            var table = GetData(CmdStore("P_Get_Request_File", p));
            return ConvertExtension.ConvertDataTable<RequestFile>(GetData(CmdStore("P_Get_Request_File", p)));
        }
    }
}
