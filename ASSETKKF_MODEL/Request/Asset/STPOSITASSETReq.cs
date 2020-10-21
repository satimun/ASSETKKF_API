using System;
using System.Collections.Generic;
using System.Text;
using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Response;

namespace ASSETKKF_MODEL.Request.Asset
{
    public class STPOSITASSETReq
    {
        public string Company { get; set; }
        public string DeptCode { get; set; }
        public string DeptLST { get; set; }
        public bool Menu3 { get; set; }
        public bool Menu4 { get; set; }
        public string DBMode { get; set; }
        public string ConnStr { get; set; }
        public List<STPOSITASSET> STPOSITASSETLst { get; set; }
        public ResultDataResponse _result = new ResultDataResponse();
    }
}
