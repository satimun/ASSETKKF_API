using System;
using System.Collections.Generic;
using System.Text;
using ASSETKKF_MODEL.Data.Mssql.Asset;

namespace ASSETKKF_MODEL.Request.Asset
{
    /*--Map to data request--*/
    #region Request
    public class AuditCutReq
    {
        public string Company { get; set; }
        public string DeptCode { get; set; }
        public string DeptLST { get; set; }
        public bool Menu3 { get; set; }
        public bool Menu4 { get; set; }
        public string DEPMST { get; set; }
        public string MODE { get; set; }
        public string SQNO { get; set; }

    }

    /*--Map to data request--*/
    public class AuditCutInfoReq
    {
        public string sqno { get; set; }
        public string Company { get; set; }
        public string DEPCODEOL { get; set; }
        public string DeptCode { get; set; }
        public string DeptLST { get; set; }
        public bool Menu3 { get; set; }
        public bool Menu4 { get; set; }
        public string DEPMST { get; set; }
    }

    public class AuditCutPostReq
    {
        public string SQNO { get; set; }
        public string DEPCODEOL { get; set; }
        public string COMPANY { get; set; }
        public string LEADERCODE { get; set; }
        public string AREACODE { get; set; }
    }

    #endregion

    #region Model

    /*--Map to data ado--*/
    public class AuditCutList
    {
        public string Company { get; set; }
        public string DEPCODEOL { get; set; }
        public string SQNO { get; set; }
        public string Audit_NO { get; set; }
        public string DEPMST { get; set; }
        public string DEPNM { get; set; }
        public int? YR { get; set; }
        public int? MN { get; set; }
        public int? YRMN { get; set; }
        public string DEPCODE { get; set; }
        public string CUTDT { get; set; }
        public string STNAME { get; set; }
    }

    /*--Map to data ado--*/
    public class DEPTList
    {
        public string DEPCODEOL { get; set; }
        public string STNAME { get; set; }
        public string DEPCODE { get; set; }
        public string Company { get; set; }
    }

    /*--Map to data ado--*/
    public class Leader
    {
        public string OFFICECODE { get; set; }
        public string OFNAME { get; set; }
    }

    #endregion
}
