using System;
using System.Collections.Generic;
using System.Text;
using ASSETKKF_MODEL.Data.Mssql.Asset;

namespace ASSETKKF_MODEL.Response.Permissions
{
    public class STPERMISSIONSRes
    {
        public string GUCODE { get; set; }
        public string GUNAME { get; set; }
        public string MENUCODE { get; set; }
        public string MENUNAME { get; set; }
        public string COMPANY { get; set; }
        public string P_ACCESS { get; set; }
        public string P_MANAGE { get; set; }
        public string P_DELETE { get; set; }
        public string P_APPROVE { get; set; }
        public string P_EXPORT { get; set; }
        public string INPID { get; set; }
        public DateTime? INPDT { get; set; }
    }

    public class PERMISSIONSRes
    {
        public bool hasPermission { get; set; }
        public List<STGROUPUSER> GROUPUSERLST { get; set; }
        public List<STMENU> MENULST { get; set; }
        public List<STPERMISSIONSRes> PERMISSIONSLST { get; set; }
        public ResultDataResponse _result = new ResultDataResponse();
    }
}
