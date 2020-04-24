using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Send.Erp
{
    public class BomSend
    {
        public string amw_refId { get; set; }
        public List<BomH> bom_h = new List<BomH>();
    }

    public class BomH
    {
        public int? seq_product;
        public string product;
        public string effective_date;
        public string use_for_planning;
        public string use_for_costing;
        public string routing;
        public decimal? bom_quantity;

        public List<BomD> bom_d = new List<BomD>();
    }

    public class BomD
    {
        public int? seq_item;
        public string product;
        public decimal? bom_quantity;
        public string position;
        public string item;
        public string warehouse;
        public decimal? net_quantity;
        public decimal? scrap_percentage;
        public string operation;
    }


}
