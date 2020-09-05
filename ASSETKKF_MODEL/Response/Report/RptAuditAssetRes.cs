﻿using System;
using System.Collections.Generic;
using System.Text;
using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Response.Asset;

namespace ASSETKKF_MODEL.Response.Report
{
    public class RptAuditAssetRes
    {
        public List<RptAuditAsset> auditAssetLst { get; set; }
        public List<RptAuditAsset> auditAssetMainLst { get; set; }
        public List<RptAuditAssetTRN> auditAssetTRNLst { get; set; }
        public ResultDataResponse _result = new ResultDataResponse();
    }

    public class RptAuditAsset
    {
        public string SQNO { get; set; }
        public string DEPMST { get; set; }
        public int? YR { get; set; }
        public int? MN { get; set; }
        public int? YRMN { get; set; }
        public string FINDY { get; set; }
        public string PCODE { get; set; }
        public string PNAME { get; set; }
        public string MEMO1 { get; set; }
        public string INPID { get; set; }
        public string INPNAME { get; set; }
        public DateTime? INPDT { get; set; }
        public DateTime? CUTDT { get; set; }
        public string BRNCODE { get; set; }
        public string BRNNAME { get; set; }

        public string ASSETNO { get; set; }
        public string ASSETNAME { get; set; }
        public string TYPECODE { get; set; }
        public string TYPENAME { get; set; }
        public string GASTCODE { get; set; }
        public string GASTNAME { get; set; }
        public string DEPCODE { get; set; }
        public string DEPCODEOL { get; set; }
        public string STNAME { get; set; }
        public string OFFICECODE { get; set; }
        public string OFNAME { get; set; }
        public string POSITCODE { get; set; }
        public string POSITNAME { get; set; }
        public string FLAG { get; set; }
        public string SNDST { get; set; }
        public string SNDACC { get; set; }
        public string STY { get; set; }
        public string STID { get; set; }
        public DateTime? STDT { get; set; }
        public string ACCY { get; set; }
        public string ADDID { get; set; }
        public DateTime? ACCDT { get; set; }
        public string POTH { get; set; }
        public string STPL { get; set; }
        public string REFNO { get; set; }
        public string TYPPL { get; set; }
        public DateTime? COMPDT { get; set; }
        public DateTime? SNNSTDT { get; set; }
        public DateTime? SNDACCDT { get; set; }
        public string MEMST { get; set; }
        public DateTime? EXPSTDT { get; set; }
        public DateTime? EXPACCDT { get; set; }
        public string DEPCODPRE { get; set; }
        public string MEMACC { get; set; }
        public string ACCEDIT { get; set; }
        public DateTime? ACCEDITDT { get; set; }
        public string COMPANY { get; set; }
        public string AUDIT_NOTE { get; set; }
        public string AUDIT_NO { get; set; }

        public string AUDIT_RESULT { get; set; }      
        public string AUDIT_AT { get; set; }


        public string LEADERCODE { get; set; }
        public string LEADERNAME { get; set; }
        public string IMGPATH { get; set; }
        public string AREACODE { get; set; }
        public string IMGURL { get; set; }

    }

    public class RptAuditAssetTRN
    {
        public string SQNO { get; set; }
        public string DEPMST { get; set; }
        public int? YR { get; set; }
        public int? MN { get; set; }
        public int? YRMN { get; set; }
        public string FINDY { get; set; }
        public string PCODE { get; set; }
        public string PNAME { get; set; }
        public string MEMO1 { get; set; }
        public string INPID { get; set; }
        public string INPNAME { get; set; }
        public DateTime? INPDT { get; set; }
        public DateTime? CUTDT { get; set; }
        public string BRNCODE { get; set; }
        public string BRNNAME { get; set; }

        public string ASSETNO { get; set; }
        public string ASSETNAME { get; set; }
        public string TYPECODE { get; set; }
        public string TYPENAME { get; set; }
        public string GASTCODE { get; set; }
        public string GASTNAME { get; set; }
        public string DEPCODE { get; set; }
        public string DEPCODEOL { get; set; }
        public string STNAME { get; set; }
        public string OFFICECODE { get; set; }
        public string OFNAME { get; set; }
        public string POSITCODE { get; set; }
        public string POSITNAME { get; set; }
        public string FLAG { get; set; }
        public string SNDST { get; set; }
        public string SNDACC { get; set; }
        public string STY { get; set; }
        public string STID { get; set; }
        public DateTime? STDT { get; set; }
        public string ACCY { get; set; }
        public string ADDID { get; set; }
        public DateTime? ACCDT { get; set; }
        public string POTH { get; set; }
        public string STPL { get; set; }
        public string REFNO { get; set; }
        public string TYPPL { get; set; }
        public DateTime? COMPDT { get; set; }
        public DateTime? SNNSTDT { get; set; }
        public DateTime? SNDACCDT { get; set; }
        public string MEMST { get; set; }
        public DateTime? EXPSTDT { get; set; }
        public DateTime? EXPACCDT { get; set; }
        public string DEPCODPRE { get; set; }
        public string MEMACC { get; set; }
        public string ACCEDIT { get; set; }
        public DateTime? ACCEDITDT { get; set; }
        public string COMPANY { get; set; }
        public string AUDIT_NOTE { get; set; }
        public string AUDIT_NO { get; set; }

        public string AUDIT_RESULT { get; set; }
        public string AUDIT_AT { get; set; }


        public string LEADERCODE { get; set; }
        public string LEADERNAME { get; set; }
        public string IMGPATH { get; set; }
        public string AREACODE { get; set; }
        public string IMGURL { get; set; }

       
       
       
        public string OFFOLD { get; set; }
        public string OFFNAMOLD { get; set; }
       

    }

    public class OfficeASSETRes
    {
        public List<Multiselect> OfficeASSETLST { get; set; }

        public ResultDataResponse _result = new ResultDataResponse();
    }

    public class TYPEASSETRes
    {
        public List<Multiselect> TYPEASSETLST { get; set; }

        public ResultDataResponse _result = new ResultDataResponse();
    }

    public class GROUPASSETRes
    {
        public List<Multiselect> GROUPASSETLST { get; set; }

        public ResultDataResponse _result = new ResultDataResponse();
    }


}
