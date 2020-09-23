using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Data.Mssql.Audit
{
    public class AuditAcc
    {
        public string COMPANY { get; set; }
        public string SQNO { get; set; }
        public string AUDIT_NO { get; set; }
        public string INPID { get; set; }
        public int YR { get; set; }
        public int MN { get; set; }
        public int YRMN { get; set; }
        public DateTime? CUTDT { get; set; }
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

        public string STY { get; set; }
        public string STID { get; set; }
        public string STNAME { get; set; }
        public DateTime? STDT { get; set; }
        public string MGR1Y { get; set; }
        public string MGR1ID { get; set; }
        public string MGR1NAME { get; set; }
        public DateTime? MGR1DT { get; set; }
        public string MGR2Y { get; set; }
        public string MGR2ID { get; set; }
        public string MGR2NAME { get; set; }
        public DateTime? MGR2DT { get; set; }
        public string FLAG_ACCEPT { get; set; }
        public string FLAG_COMPLETE { get; set; }
        public DateTime? SNNSTDT { get; set; }
        public DateTime? EXPSTDT { get; set; }
        public DateTime? ACCDT { get; set; }
        public DateTime? EXPACCDT { get; set; }
    }
}
