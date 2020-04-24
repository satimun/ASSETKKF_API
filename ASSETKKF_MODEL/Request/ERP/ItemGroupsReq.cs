using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Request.ERP
{
    public class ItemGroupsReq
    {
        public string amw_refId { get; set; }
        public List<ItemGroupReq> item_group = new List<ItemGroupReq>();

    }

    public class ItemGroupReq
    {
        public int? seq_itemgroup { get; set; }
        public string itemgroup { get; set; }
        public string description { get; set; }

    }


}
