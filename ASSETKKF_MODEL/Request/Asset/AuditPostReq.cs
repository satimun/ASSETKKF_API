﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Request.Asset
{
    public class AuditPostReq
    {
        public string SQNO { get; set; }
        public string DEPCODEOL { get; set; }
        public string COMPANY { get; set; }
        public string ASSETNO { get; set; }
        public string LEADERCODE { get; set; }
        public string LEADERNAME { get; set; }
        public string AREACODE { get; set; }
        public string AREANAME { get; set; }
        public string IMGPATH { get; set; }
        public string UCODE { get; set; }
        public IFormFile FileToUpload { get; set; }
    }

    public class AuditPostCheckReq
    {
        public string SQNO { get; set; }
        public string COMPANY { get; set; }
        public string DEPCODEOL { get; set; }
        public string ASSETNO { get; set; }
        public string LEADERCODE { get; set; }
        public string LEADERNAME { get; set; }
        public string AREANAME { get; set; }
        public string AREACODE { get; set; }
        public string UCODE { get; set; }
        public string MODE { get; set; }
    }

    public class AUDITPOSTMSTReq
    {
        public string SQNO { get; set; }
        public string DEPCODEOL { get; set; }
        public string COMPANY { get; set; }
        public string LEADERCODE { get; set; }
        public string LEADERNAME { get; set; }
        public string AREACODE { get; set; }
        public string AREANAME { get; set; }
        public string ASSETNO { get; set; }
        public string FINDY { get; set; }
        public string PCODE { get; set; }
        public string PNAME { get; set; }
        public string MEMO1 { get; set; }
        public string UCODE { get; set; }
        public string IMGPATH { get; set; }
        public string MODE { get; set; }

        public IFormFile FileToUpload { get; set; }
    }
}