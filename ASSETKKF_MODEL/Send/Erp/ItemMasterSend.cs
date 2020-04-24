using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Send.Erp
{
    public class ItemMasterSend
    {
        public string amw_refId{ get; set; }
        public List<ItemMaster> item_master = new List<ItemMaster>();
    }

    public class ItemMaster
    {
        public int? seq_item;

        public string item;
        public string description;
        public string search_key_i;
        public string search_key_ii;
        public string item_type;
        public string item_group;
        public string order_system;
        public string inventory_unit;
        public string serialized;
        public string derived_from_item;
        public string customizable;
        public string with_pcs;
        public string cost_component;
        public string size;
        public string standard;
        public decimal? weight { get; set; }
        public string weight_unit;
        public string product_type;
        public string product_class;
        public string product_line;
        public string default_supply_source;
        public string material;
        public decimal? order_quantity_increment;
        public decimal? minimum_order_quantity;
        public decimal? maximum_order_quantity;
        public decimal? fixed_order_quantity;
        public decimal? reorder_point;
        public decimal? order_interval;
        public decimal? safety_stock;
        public decimal? safety_time;
        public decimal? bom_quantity;
        public decimal? routing_quantity;
        public string purchase_unit;
        public string purchase_price_unit;
        public string purchase_price_group;
        public string purchase_statistics_group;
        public string purchase_currency;
        public decimal? purchase_price;

        public int? vendorRating;
        public int? inspection;

        public decimal? supply_time;
        public string sales_unit;
        public string sales_price_unit;
        public string sales_price_group;
        public string sales_statistics_group;
        public string sales_currency;
        public decimal? sales_price;
        public string warehouse_item_sales;
        public string warehouse_item_purch;
        public string warehouse_item_ordering;
        public string plan_level;
        public string ordering_warehouse;
        public string operations_horizon;
        public string order_horizon;
        public string planning_horizon;
        public string costing_source;
        public string standard_cost_component_scheme;
        public string sales_office;
        public string quality_group;
    }

}
