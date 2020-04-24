using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Response.ERP
{
    public class ResponseErp  
    {
        public string amw_refId { get; set; }
        public string code { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public dynamic data = "";
    }
}
