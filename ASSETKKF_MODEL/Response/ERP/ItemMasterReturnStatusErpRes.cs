using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Response.ERP
{
    public class ItemMasterReturnStatusErpRes
    {
        public int? seq_item;
        public string item;
       
        public string code { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }
}
