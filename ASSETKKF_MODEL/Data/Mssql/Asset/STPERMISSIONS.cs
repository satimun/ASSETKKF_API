using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Data.Mssql.Asset
{
    public class STPERMISSIONS
    {
        public string GUCODE { get; set; }
        public string MENUCODE { get; set; }
        public string COMPANY { get; set; }
        public string P_ACCESS { get; set; }
        public string P_MANAGE { get; set; }
        public string P_DELETE { get; set; }
        public string P_APPROVE { get; set; }
        public string P_EXPORT { get; set; }
        public string INPID { get; set; }
        public DateTime? INPDT { get; set; }
    }
}
