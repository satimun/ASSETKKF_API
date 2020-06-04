using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Response.Report
{
    public class AuditProblems
    {
        public string company { get; set; }
        public string depcodeol { get; set; }
        public string stname { get; set; }
        public int qty_total { get; set; }
        public int qty_noproblem { get; set; }
        public int qty_problems { get; set; }
        public int qty_wait { get; set; }
        public float progress_noproblem { get; set; }
        public float progress_problems { get; set; }
        public float progress_wait { get; set; }
    }

    public class AuditProblemsRes
    {
        public AuditProblems auditProblem { get; set; }
        public ResultDataResponse _result = new ResultDataResponse();
    }

    public class AuditProblemsDeptRes
    {
        public List<AuditProblems> auditProblemLst { get; set; }
        public ResultDataResponse _result = new ResultDataResponse();
    }
}
