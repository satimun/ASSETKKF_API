using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Request.Report
{
    public class AuditSummaryReq
    {
        public string Company { get; set; }
        public string DeptCode { get; set; }
        public string DeptLST { get; set; }
        public bool Menu3 { get; set; }
    }
}
