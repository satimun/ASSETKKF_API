﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Request.Report
{
    public class AuditSummaryReq
    {
        public string Company { get; set; }
        public string DeptCode { get; set; }
        public string DeptLST { get; set; }
        public bool Menu3 { get; set; }
        public bool Menu4 { get; set; }
        public string Dept { get; set; }
        public string yr { get; set; }
        public string DBMode { get; set; }
        public string ConnStr { get; set; }
    }
}
