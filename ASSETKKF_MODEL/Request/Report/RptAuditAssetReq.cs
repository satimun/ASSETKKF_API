﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Request.Report
{
    public class RptAuditAssetReq
    {
        public string company { get; set; }
        //public DateTime? cutdt { get; set; }
        //public DateTime? inpdt { get; set; }
        public string cutdt { get; set; }
        public string inpdt { get; set; }
        public string audit_no { get; set; }
        public string sqno { get; set; }
        public string sqno_copm { get; set; }
        public string DEPCODEOL { get; set; }
        public string LEADERCODE { get; set; }
        public string PCODE { get; set; }
        public string DEPMST { get; set; }

        public string DeptCode { get; set; }
        public string DeptLST { get; set; }
        public bool Menu3 { get; set; }
        public bool Menu4 { get; set; }

        public string YEAR { get; set; }
        public string MN { get; set; }
        public string YRMN { get; set; }

        public string OFFICECODE { get; set; }
        public string TYPECODE { get; set; }
        public string GASTCODE { get; set; }
        public string orderby { get; set; }

        public string MODE { get; set; }
        public string DBMode { get; set; }
        public string ConnStr { get; set; }


    }
}
