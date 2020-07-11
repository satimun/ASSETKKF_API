using System;
using System.Collections.Generic;
using System.Text;
using ASSETKKF_MODEL.Data.Mssql.Asset;

namespace ASSETKKF_MODEL.Response.Report
{
    public class RptAuditAssetRes
    {
        public List<RptAuditAsset> auditAssetLst { get; set; }
        public List<RptAuditAsset> auditAssetTRNLst { get; set; }
        public ResultDataResponse _result = new ResultDataResponse();
    }

    public class RptAuditAsset
    {
        public string ASSETNO { get; set; }
        public string ASSETNAME { get; set; }
        public string TYPECODE { get; set; }
        public string TYPENAME { get; set; }
        public string GASTCODE { get; set; }
        public string GASTNAME { get; set; }
        public string OFFICECODE { get; set; }
        public string OFNAME { get; set; }
        public string DEPCODEOL { get; set; }
        public string STNAME { get; set; }
        public string AUDIT_RESULT { get; set; }
        public string AUDIT_NOTE { get; set; }
        public string AUDIT_NO { get; set; }
        public string SQNO { get; set; }
        public string AUDIT_AT { get; set; }
        public DateTime? ACCDT { get; set; }
        public DateTime? COMPDT { get; set; }
        public string LEADERCODE { get; set; }
        public string LEADERNAME { get; set; }
        public string INPID { get; set; }
        public string INPNAME { get; set; }
        public DateTime? INPDT { get; set; }
        public string DEPMST { get; set; }
        public string IMGPATH { get; set; }
        public string POSITCODE { get; set; }
        public string POSITNAME { get; set; }

    }


}
