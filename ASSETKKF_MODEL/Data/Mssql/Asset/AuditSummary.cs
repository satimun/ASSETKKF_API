using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Data.Mssql.Asset
{
    public class AuditSummary
    {
        public int QTY_TOTAL { get; set; }
        public int QTY_CHECKED { get; set; }
        public int QTY_WAIT { get; set; }
        public float QTY_CHECKED_PERCENT { get; set; }
        public float QTY_WAIT_PERCENT { get; set; }
        public float AUDIT_PROGRESS_PERCENT { get; set; }
        public string OFFICECODE { get; set; }
    }
}
