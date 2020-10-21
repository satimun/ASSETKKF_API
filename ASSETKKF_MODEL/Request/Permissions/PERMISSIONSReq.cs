using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Request.Permissions
{
    public class PERMISSIONSReq
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
        public string MODE { get; set; }
        public string DBMode { get; set; }
        public string ConnStr { get; set; }
    }
}
