using ASSETKKF_MODEL.Data.Mssql.Asset;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Response.Asset
{
    public class AuditPostRes
    {
        public string SQNO { get; set; }
        public string DEPCODEOL { get; set; }
        public string COMPANY { get; set; }
        public string LEADERCODE { get; set; }
        public string AREACODE { get; set; }
        public string IMGPATH { get; set; }
        public string IMGSRC { get; set; }
        public string AREA { get; set; }
        public ASAUDITPOSTMST AUDITPOSTMST { get; set; }
        public List<ASAUDITCUTDATE> AUDITCUTDATELST { get; set; }
        public List<ASAUDITPOSTMST> AUDITPOSTMSTNOPROBLEMLST { get; set; }
        public List<ASAUDITPOSTMST> AUDITPOSTMSTPROBLEMLST { get; set; }
        public List<ASAUDITPOSTMST> AUDITPOSTMSTWAITLST { get; set; }
        public List<ASAUDITPOSTMST> AUDITPOSTMSTCHECKEDLST { get; set; }
        public List<ASAUDITPOSTTRN> AUDITPOSTTRNLST { get; set; }
        public List<ASSETOFFICECODE> ASSETOFFICECODELST { get; set; }
        public List<ASSETASSETNO> ASSETASSETNOLST { get; set; }

        public ResultDataResponse _result = new ResultDataResponse();
    }
}
