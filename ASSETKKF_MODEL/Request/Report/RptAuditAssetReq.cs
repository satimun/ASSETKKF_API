using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Request.Report
{
    public class RptAuditAssetReq
    {
        public string company { get; set; }
        public DateTime? cutdt { get; set; }
        public DateTime? inpdt { get; set; }
        public string audit_no { get; set; }
        public string sqno { get; set; }
        public string sqno_copm { get; set; }
        public string DEPCODEOL { get; set; }
        public string LEADERCODE { get; set; }
        public string PCODE { get; set; }

    }
}
