using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Request.ERP
{
    public class ItemMasterReturnStatusErpReq
    {
        public string amw_refId { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public List<item_masterRet> item_master = new List<item_masterRet>();
    }

    public class item_masterRet
    {
        public string seq_item { get; set; }
        public string item { get; set; }
        public string status { get; set; }
        public string message { get; set; }

    }
}
