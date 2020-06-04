using ASSETKKF_MODEL.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Data.Mssql.Asset
{
    public class AuditSummary
    {
        public string Company { get; set; }
        public string Depcodeol { get; set; }
        public string STName { get; set; }
        public int QTY_TOTAL { get; set; }
        public int QTY_CHECKED { get; set; }
        public int QTY_WAIT { get; set; }
        public float PROGRESS { get; set; }
        public string sqno { get; set; }
        public string audit_no { get; set; }
    }
}
