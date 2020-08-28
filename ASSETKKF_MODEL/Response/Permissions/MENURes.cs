using System;
using System.Collections.Generic;
using System.Text;
using ASSETKKF_MODEL.Data.Mssql.Asset;

namespace ASSETKKF_MODEL.Response.Permissions
{
    public class MENURes
    {
        public List<STMENU> MENULST { get; set; }
        public ResultDataResponse _result = new ResultDataResponse();
    }
}
