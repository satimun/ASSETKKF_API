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
        //public List<ASAUDITCUTDATE> NoAuditLST { get; set; }
        public List<ASAUDITPOSTMSTTOTEMP> NoAuditLST { get; set; }
        public List<ASAUDITPOSTMSTTOTEMP> AuditToTEMPLST { get; set; }
        public List<AuditTmpCompareTRN> AuditTmpCompareTRNLST { get; set; }

        public ResultDataResponse _result = new ResultDataResponse();
    }

    public class AuditTmpCompareTRN
    {
        public string COMPANY_TMP { get; set; }
        public string AUDITNO_TMP { get; set; }
        public string SQNO_TMP { get; set; }
        public string ASSETNO_TMP { get; set; }
        public string MEMO_TMP { get; set; }
        public string FINDY { get; set; }
        public string PCODE { get; set; }
        public string PNAME { get; set; }
        public string MEMO1 { get; set; }
        public string COMPANY_TRN { get; set; }
        public string AUDITNO_TRN { get; set; }
        public string SQNO_TRN { get; set; }
        public string ASSETNO_TRN { get; set; }
        public string MEMO_TRN { get; set; }
        public string INPID_TRN { get; set; }
        public string ASSETNAME { get; set; }
        public string OFFICECODE { get; set; }
        public string OFNAME { get; set; }
        public string DEPCODEOL { get; set; }
        public string STNAME { get; set; }


    }
}
