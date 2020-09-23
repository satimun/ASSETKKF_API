using System;
using System.Collections.Generic;
using System.Text;
using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Data.Mssql.Audit;

namespace ASSETKKF_MODEL.Response.Audit
{
    public class AuditAccRes
    {
        public SummaryAudit AuditSummary { get; set; }
        public List<SummaryResult> SummaryResultLst { get; set; }
        public ASAUDITPOSTMSTTODEP AUDITPOSTMSTTODEP { get; set; }
        public List<AuditAcc> AuditAccLst { get; set; }
        public List<ASAUDITPOSTMST> POSTMSTDuplicateLST { get; set; }
        public List<ASAUDITPOSTTRN> POSTTRNDuplicateLST { get; set; }
        public List<ASAUDITCUTDATE> NoAuditLST { get; set; }
        public List<ASAUDITPOSTMSTTOTEMP> AuditToTEMPLST { get; set; }
        public List<ASAUDITPOSTMSTTODEP> AUDITPOSTMSTTODEPLST { get; set; }

        public ResultDataResponse _result = new ResultDataResponse();
    }
}
