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

    public class Quantity
    {
        public int QTY_ASSET { get; set; }
        public int QTY_AUDIT { get; set; }
    }
}
