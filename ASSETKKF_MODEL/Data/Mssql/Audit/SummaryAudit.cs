using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Data.Mssql.Audit
{
    public class SummaryAudit
    {
        public int QTY_HUMAN { get; set; }
        public int QTY_ASSET { get; set; }
        public int QTY_AUDIT { get; set; }
        public float PROGRESS { get; set; }
    }

    public class SummaryResult
    {
        public string PCODE { get; set; }
        public string PNAME { get; set; }
        public string DEPCODE { get; set; }
        public string DEPCODEOL { get; set; }
        public string STNAME { get; set; }
        public int QTY { get; set; }
    }
}
