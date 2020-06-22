using System;
using System.Collections.Generic;
using System.Text;
using ASSETKKF_MODEL.Data.Mssql.Asset;

namespace ASSETKKF_MODEL.Response.Asset
{
    #region Response
    public class AuditPostTRNRes
    {
        public string SQNO { get; set; }
        public string DEPCODEOL { get; set; }
        public string COMPANY { get; set; }
        public string LEADERCODE { get; set; }
        public string AREACODE { get; set; }
        public string IMGPATH { get; set; }
        public string IMGSRC { get; set; }
        public AuditPostTRN AuditAssetPostTRN { get; set; }
        public List<ASAUDITPOSTMST> AUDITPOSTMSTWAITLST { get; set; }
        public List<ASAUDITPOSTMST> AUDITPOSTMSTCHECKEDLST { get; set; }
        public List<ASAUDITPOSTTRN> AUDITPOSTTRNLST { get; set; }
        public ResultDataResponse _result = new ResultDataResponse();
    }

    public class ASSETOFFICECODERes
    {
        public ASSETOFFICECODE ASSETOFFICECODE { get; set; }
        public List<ASSETOFFICECODE> ASSETOFFICECODELST { get; set; }
        public ResultDataResponse _result = new ResultDataResponse();
    }

    public class ASSETASSETNORes
    {
        public ASSETASSETNO ASSETASSETNO { get; set; }
        public List<ASSETASSETNO> ASSETASSETNOLST { get; set; }
        public List<ASAUDITPOSTMST> AUDITPOSTMSTWAITLST { get; set; }
        public List<ASAUDITPOSTMST> AUDITPOSTMSTCHECKEDLST { get; set; }
        public List<ASAUDITPOSTTRN> AUDITPOSTTRNLST { get; set; }
        public ResultDataResponse _result = new ResultDataResponse();
    }

    #endregion

    #region Model
    public class AuditPostTRN
    {
        public int? YR { get; set; }
        public int? MN { get; set; }
        public int? YRMN { get; set; }
        public string MODE { get; set; }
        public string SQNO { get; set; }
        public string COMPANY { get; set; }
        public string DEPMST { get; set; }
        public string FINDY { get; set; }
        public string PCODE { get; set; }
        public string PNAME { get; set; }
        public string MEMO1 { get; set; }
        public string CUTDT { get; set; }
        public string ASSETNO { get; set; }
        public string ASSETNAME { get; set; }
        public string OFFOLD { get; set; }
        public string OFFNAMOLD { get; set; }
        public string OFFICECODE { get; set; }
        public string OFNAME { get; set; }
        public string DEPCODE { get; set; }
        public string DEPCODEOL { get; set; }
        public string STNAME { get; set; }
        public string LEADERCODE { get; set; }
        public string LEADERNAME { get; set; }
        public string AREANAME { get; set; }
        public string AREACODE { get; set; }
        public string IMGPATH { get; set; }
        public string UCODE { get; set; }
    }

    public class ASSETOFFICECODE
    {
        public string COMPANY { get; set; }
        public string DEPCODE { get; set; }
        public string OFFICECODE { get; set; }
        public string OFNAME { get; set; }
        public string STNAME { get; set; }
        public string DEPCODEOL { get; set; }
    }

    public class ASSETASSETNO
    {
        public string COMPANY { get; set; }
        public string ASSETNO { get; set; }
        public string ASSETNAME { get; set; }
        public string OFFICECODE { get; set; }
        public string OFNAME { get; set; }
    }

    #endregion

}
