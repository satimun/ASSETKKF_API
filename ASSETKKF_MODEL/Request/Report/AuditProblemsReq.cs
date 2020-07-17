using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Request.Report
{
    public class AuditProblemsReq
    {
        public string Company { get; set; }
        public string cutdt { get; set; }
        public string inpdt { get; set; }
        public string audit_no { get; set; }
        public string sqno { get; set; }
        public string sqno_copm { get; set; }
        public string DEPCODEOL { get; set; }
        public string LEADERCODE { get; set; }
        public string PCODE { get; set; }
        public string DEPMST { get; set; }
        public string DeptCode { get; set; }
        public string DeptLST { get; set; }
        public bool Menu3 { get; set; }
        public string YEAR { get; set; }
        public string MN { get; set; }
        public string OFFICECODE { get; set; }
        public string TYPECODE { get; set; }
        public string GASTCODE { get; set; }
    }
}
