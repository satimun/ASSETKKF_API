using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Request.ERP
{
    public class BomReturnStatusErpReq
    {
        public string amw_refId { get; set; }
        public string status { get; set; }
        public string message { get; set; }

        public List<Bom_hRet> bom_h = new List<Bom_hRet>();
    }

    public class Bom_hRet
    {
        public string seq_product { get; set; }
        public string product { get; set; }

        public string status { get; set; }
        public string message { get; set; }
        public List<Bom_dRet> bom_d = new List<Bom_dRet>();
    }

    public class Bom_dRet
    {
        public string seq_item { get; set; }       
        public string item { get; set; }
        public string revision { get; set; }

        public string status { get; set; }
        public string message { get; set; }

    }
}
