﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace ASSETKKF_MODEL.Request.Asset
{
    public class AuditPostTRNReq
    {
        public string SQNO { get; set; }
        public string DEPCODEOL { get; set; }
        public string COMPANY { get; set; }
        public string LEADERCODE { get; set; }
        public string LEADERNAME { get; set; }
        public string AREACODE { get; set; }
        public string AREANAME { get; set; }
        public string IMGPATH { get; set; }
        public string UCODE { get; set; }
        public IFormFile FileToUpload { get; set; }
        public string ASSETNO { get; set; }

    }

    public class AUDITPOSTTRNReq
    {
        public int YR { get; set; }
        public int MN { get; set; }
        public int YRMN { get; set; }
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
        public string POSITNAME { get; set; }
        public string LEADERCODE { get; set; }
        public string LEADERNAME { get; set; }
        public string AREANAME { get; set; }
        public string AREACODE { get; set; }
        public string IMGPATH { get; set; }
        public string UCODE { get; set; }
        public IFormFile FileToUpload { get; set; }
    }

    public class ASSETOFFICECODEReq
    {
        public string COMPANY { get; set; }
        public string DEPCODEOL { get; set; }
        public string OFFICECODE { get; set; }
    }

    public class ASSETASSETNOReq
    {
        public string COMPANY { get; set; }
        public string DEPCODEOL { get; set; }
        public string ASSETNO { get; set; }
        public string SQNO { get; set; }
        public string LEADERCODE { get; set; }
        public string LEADERNAME { get; set; }
        public string AREANAME { get; set; }
        public string AREACODE { get; set; }
        public string UCODE { get; set; }
    }
}