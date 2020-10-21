using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Request.Audit
{
    public class AuditResultReq
    {
        public string COMPANY { get; set; }
        public string SQNO { get; set; }
        public string AUDIT_NO { get; set; }
        public string INPID { get; set; }
        public string YR { get; set; }
        public string MN { get; set; }
        public string YRMN { get; set; }
        public string CUTDT { get; set; }
        public string DEPMST { get; set; }
        public string DEPCODEOL { get; set; }
        public int QTY_TOTAL { get; set; }
        public int QTY_CHECKED { get; set; }
        public int QTY_WAIT { get; set; }
        public int QTY_TRN { get; set; }
        public float PROGRESS { get; set; }
        public DateTime? STARTDT { get; set; }
        public DateTime? LASTDT { get; set; }
        public string ICONFAFA { get; set; }
        public string ICONCOLOR { get; set; }
        public string MODE { get; set; }

        public string DEPNM { get; set; }
        public bool Menu3 { get; set; }
        public bool Menu4 { get; set; }

        public string FLAG { get; set; }
        public string DBMode { get; set; }
        public string ConnStr { get; set; }
    }
}
