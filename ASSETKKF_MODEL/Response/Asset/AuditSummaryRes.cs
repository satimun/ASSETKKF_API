﻿using System;
using System.Collections.Generic;
using System.Text;
using ASSETKKF_MODEL.Data.Mssql.Asset;

namespace ASSETKKF_MODEL.Response.Asset
{
    public class AuditSummaryRes
    {
        public AuditSummary AuditSummaryRPT { get; set; }

        public ResultDataResponse _result = new ResultDataResponse();
    }

    public class InspectionRes
    {
        public List<DashboardInspection> DashboardInspectionLST { get; set; }

        public ResultDataResponse _result = new ResultDataResponse();
    }

    public class AuditDeptSummaryRes
    {
        public List<AuditDeptSummary> AuditSummaryLST { get; set; }

        public ResultDataResponse _result = new ResultDataResponse();
    }

    public class AuditDeptSummary
    {
        public string depcodeol { get; set; }
        public string stname { get; set; }
        public float qty_total { get; set; }
        public float qty_wait { get; set; }
        public float qty_checked { get; set; }
        public float qty_trn { get; set; }
        public float progress { get; set; }
        public string sqno { get; set; }
        public string audit_no { get; set; }
        public string company { get; set; }
        public string DEPMST { get; set; }
        public string DEPNM { get; set; }
        public string yr { get; set; }
        public string mn { get; set; }
        public string yrmn { get; set; }

        public List<AuditDeptSummary> AuditDepSummaryLST { get; set; }
    }

    public class Multiselect
    {
        public string id { get; set; }
        public string description { get; set; }
    }

    public class AuditYearRes
    {
        public List<Multiselect> AuditYearLST { get; set; }

        public ResultDataResponse _result = new ResultDataResponse();
    }

    public class AuditMNRes
    {
        public List<Multiselect> AuditMNLST { get; set; }

        public ResultDataResponse _result = new ResultDataResponse();
    }

    public class AuditCutDTRes
    {
        public List<Multiselect> AuditCutDTLST { get; set; }

        public ResultDataResponse _result = new ResultDataResponse();
    }

}
