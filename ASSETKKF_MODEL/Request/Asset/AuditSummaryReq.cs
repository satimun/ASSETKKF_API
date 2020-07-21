using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Request.Asset
{
    public class AuditSummaryReq
    {
        public string UCode { get; set; }
        public string Company { get; set; }
        public string DEPCODEOL { get; set; }
        public string DEPTCODELST { get; set; }
        public bool Menu3 { get; set; }
        public bool Menu4 { get; set; }
        public string sqno { get; set; }
        public string depmst { get; set; }
        public string year { get; set; }
        public string mn { get; set; }
        public string yrmn { get; set; }

    }
}
