using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Response.Report
{
    public class AuditProblemSummary
    {
        public string company { get; set; }
        public string depcodeol { get; set; }
        public string stname { get; set; }
        public string pcode { get; set; }
        public string pname { get; set; }
        public int qty { get; set; }
    }

    public class AuditProblemSummaryRes
    {
        public List<AuditProblemSummary> auditProblemSummaryLst { get; set; }
        public ResultDataResponse _result = new ResultDataResponse();
    }
}
