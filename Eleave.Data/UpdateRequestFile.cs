using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Data;
using Eleave.Library;
using Eleave.Models;

namespace Eleave.Data
{
    public class UpdateRequestFile : MsSQL
    {
        public UpdateRequestFile() : base(Utils.GetConfig("HRIS_DB"))
        {

        }
        public List<StoreUpdateRequestFile> Save(string ReqNo, string FileName, string FilePath, int FileNo, string UserId)
        {
            var p = new SqlParameters();
            p.AddParams("@inReqNo", ReqNo.ToTrim());
            p.AddParams("@inFileName", FileName.ToTrim());
            p.AddParams("@inFilePath", FilePath.ToTrim());
            p.AddParams("@inFileNo", FileNo);
            p.AddParams("@inUserId", UserId.ToTrim());

            //p.AddParams("@outGenstatus", "");

            var table = GetData(CmdStore("P_Save_Request_File", p));
            return ConvertExtension.ConvertDataTable<StoreUpdateRequestFile>(GetData(CmdStore("P_Save_Request_File", p)));
        }
    }
}


//tac 3461-8534C
//453