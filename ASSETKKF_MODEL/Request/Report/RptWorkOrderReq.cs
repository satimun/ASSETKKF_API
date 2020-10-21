using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Request.Report
{
    public class RptWorkOrderReq
    {
        public string MchProjectID;
        public string WorkOrderID;
        public string DBMode { get; set; }
        public string ConnStr { get; set; }

    }
}
