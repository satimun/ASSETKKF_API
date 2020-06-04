using ASSETKKF_MODEL.Data.Mssql.Asset;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Response.Asset
{
    #region Response Data
    public class AuditCutPostRes
    {
        public string SQNO { get; set; }
        public string DEPCODEOL { get; set; }
        public string COMPANY { get; set; }
        public string LEADERCODE { get; set; }
        public string AREACODE { get; set; }
        public ASAUDITCUTDATEMST AUDITCUTDATEMST = new ASAUDITCUTDATEMST();
        public List<ASAUDITCUTDATE> AUDITCUTDATELST { get; set; }
        public bool validAUDITCUTDATEMST { get; set; }
        public bool validAUDITCUTDATE { get; set; }
        public bool validAUDITPOSTMST { get; set; }
        public bool validAUDITPOSTTRN { get; set; }


        public ResultDataResponse _result = new ResultDataResponse();
    }

    public class AuditCutInfoRes
    {
        public List<DEPTList> auditCutDEPTList { get; set; }
        public List<LeaderList> auditCutLeaderList { get; set; }
        public List<String> deplkList { get; set; }
        public List<STPOSITASSET> STPOSITASSETLst { get; set; }

        public ResultDataResponse _result = new ResultDataResponse();
    }

    public class AuditCutListRes
    {
        public List<AuditCutList> auditCutNoLst { get; set; }

        public ResultDataResponse _result = new ResultDataResponse();
    }

    public class DEPTListRes
    {
        public List<DEPTList> auditCutNoLst { get; set; }
        public ResultDataResponse _result = new ResultDataResponse();
    }

    #endregion

    #region Model
    public class AuditCutList
    {
        public string id { get; set; }
        public string descriptions { get; set; }
        public string dept { get; set; }
        public string DEPCODEOL { get; set; }
        public string COMPANY { get; set; }
        public string audit_no { get; set; }
        public int? YR { get; set; }
        public int? MN { get; set; }
        public int? YRMN { get; set; }
        public string DEPCODE { get; set; }
        public string DEPMST { get; set; }
        public string CUTDT { get; set; }
        public string STNAME { get; set; }

    }

    public class DEPTList
    {
        public string id { get; set; }
        public string name { get; set; }
        public string descriptions { get; set; }
    }

    public class LeaderList
    {
        public string id { get; set; }
        public string name { get; set; }
        public string descriptions { get; set; }
    }
    #endregion
}
