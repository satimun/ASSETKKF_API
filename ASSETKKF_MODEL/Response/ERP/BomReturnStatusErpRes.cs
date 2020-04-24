using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Response.ERP
{
    public class BomReturnStatusErpRes
    {
        public string amw_refId { get; set; }
        public string status { get; set; }
        public string message { get; set; }

        public List<Bom_hRes> bom_h = new List<Bom_hRes>();
    }

    public class Bom_hRes
    {
        public string seq_product { get; set; }
        public string product { get; set; }
        public string code { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public List<Bom_dRes> bom_d = new List<Bom_dRes>();
    }

    public class Bom_dRes
    {
        public string seq_item { get; set; }
        public string item { get; set; }
        public string revision { get; set; }
        public string code { get; set; }
        public string status { get; set; }
        public string message { get; set; }

    }

}
