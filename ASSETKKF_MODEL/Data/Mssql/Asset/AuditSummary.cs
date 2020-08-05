using ASSETKKF_MODEL.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Data.Mssql.Asset
{
    public class AuditSummary
    {
        public string Company { get; set; }
        public string Depcodeol { get; set; }
        public string STName { get; set; }
        public int QTY_TOTAL { get; set; }
        public int QTY_CHECKED { get; set; }
        public int QTY_WAIT { get; set; }
        public int QTY_TRN { get; set; }
        public float PROGRESS { get; set; }
        public string sqno { get; set; }
        public string audit_no { get; set; }
        public string DEPMST { get; set; }
        public string DEPNM { get; set; }
    }

    public class DashboardInspection
    {
        public string COMPANY { get; set; }
        public int YR { get; set; }
        public int MN { get; set; }
        public int YRMN { get; set; }
        public string DEPMST { get; set; }
        public string DEPNM { get; set; }
        public string DEPCODEOL { get; set; }
        public string STNAME { get; set; }
        public string SQNO { get; set; }
        public string AUDIT_NO { get; set; }
        public DateTime? STARTDT { get; set; }
        public DateTime? LASTDT { get; set; }
        public string OFFICECODE { get; set; }
        public string OFNAME { get; set; }
        public string TYPECODE { get; set; }
        public string TYPENAME { get; set; }
        public string GASTCODE { get; set; }
        public string GASTNAME { get; set; }
        public int QTY_TOTAL { get; set; }
        public int QTY_CHECKED { get; set; }
        public int QTY_WAIT { get; set; }
        public int QTY_TRN { get; set; }
        public float PROGRESS { get; set; }
        public string ASSETNO { get; set; }
        public string ASSETNAME { get; set; }
        public string PCODE { get; set; }
        public string PNAME { get; set; }
        public string INPID { get; set; }
        public DateTime? INPDT { get; set; }

    }
}
