using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Data.Mssql.Audit;

namespace ASSETKKF_MODEL.Response.Audit
{
    public class AuditDuplicateRes
    {
        public string IMGPATH { get; set; }
        public string IMGSRC { get; set; }
        public string FILEPATH { get; set; }
        public FileStream FileSRC { get; set; }
        public ASAUDITPOSTMSTTOTEMP AUDITPOSTMSTTOTEMP { get; set; }
        public List<AuditResult> AuditResultLst { get; set; }
        public List<ASAUDITPOSTMST> POSTMSTDuplicateLST { get; set; }
        public List<ASAUDITPOSTTRN> POSTTRNDuplicateLST { get; set; }
        public List<ASAUDITCUTDATE> NoAuditLST { get; set; }
        public List<ASAUDITPOSTMSTTOTEMP> AuditToTEMPLST { get; set; }

        public ResultDataResponse _result = new ResultDataResponse();
    }
}
