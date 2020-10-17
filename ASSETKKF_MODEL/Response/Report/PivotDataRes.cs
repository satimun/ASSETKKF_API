using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Response.Report
{
    public class PivotDataRes
    {
        public string jsonresult { get; set; }
        public List<string> columns { get; set; }
        public List<string> rows { get; set; }
        public ResultDataResponse _result = new ResultDataResponse();
    }
}
