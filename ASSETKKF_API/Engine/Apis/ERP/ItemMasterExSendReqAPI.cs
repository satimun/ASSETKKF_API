using ASSETKKF_MODEL.Common;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Send.Erp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Apis.ERP
{
    
    public class ItemMasterExSendReqAPI : Base<IDCodeDesModel>
    {
        public ItemMasterExSendReqAPI()
        {
            PermissionKey = "ADMIN";
        }

        protected override void ExecuteChild(IDCodeDesModel dataReq, ResponseAPI dataRes)
        {
            var res = new ItemMasterSend();
            res.amw_refId = "SITM20191221-145250123-0001";
            res.item_master = new List<ItemMaster>();

            ItemMaster eg = new ItemMaster();

            eg.item = "B640715-0-00-0-0-00";
            eg.description = "SRM UNIT LOAD DDH15 M.นน700 KG.(PALLETSIZE1.10X1.35X1.45 M.)";
            eg.search_key_i = "SRM UNIT LOAD DD";
            eg.search_key_ii = "SRM UNIT LOAD DD";
            eg.item_type = "PRODUCT";
            eg.item_group = "MFG";
            eg.order_system = "PLANNED";
            eg.inventory_unit = "SET";
            eg.serialized = "YES";
            eg.derived_from_item = "B640715-0-00-0-0-00";
            eg.customizable = "YES";
            eg.with_pcs = "YES";
            eg.cost_component = "YES";
            eg.size = "YES";
            eg.standard = "YES";
            eg.weight = 0;
            eg.weight_unit = "YES";
            eg.product_type = "YES";
            eg.product_class = "YES";
            eg.product_line = "YES";
            eg.default_supply_source = "JOB SHOP";
            eg.material = "JOB SHOP";
            eg.order_quantity_increment = 1;
            eg.minimum_order_quantity = 0;
            eg.maximum_order_quantity = 9999999;
            eg.fixed_order_quantity = 1;
            eg.reorder_point = 0;
            eg.order_interval = 1;
            eg.safety_stock = 0;
            eg.bom_quantity = 1;
            eg.routing_quantity = 1;
            eg.purchase_unit = "SET";
            eg.purchase_price_unit = "SET";
            eg.purchase_price_group = "101";
            eg.purchase_statistics_group = "101";
            eg.purchase_currency = "THB";
            eg.purchase_price = 0;
            eg.supply_time = 0;
            eg.sales_unit = "SET";
            eg.sales_price_unit = "SET";
            eg.sales_price_group = "101";
            eg.sales_statistics_group = "101";
            eg.sales_currency = "THB";
            eg.sales_price = 0;
            eg.warehouse_item_sales = "FG";
            eg.warehouse_item_ordering = "FG";
            eg.plan_level = "1";
            eg.ordering_warehouse = "FG";
            eg.operations_horizon = "365";
            eg.order_horizon = "365";
            eg.planning_horizon = "365";
            eg.costing_source = "JOB SHOP";
            eg.standard_cost_component_scheme = "101";
            eg.sales_office = "DBI01";

            res.item_master.Add(eg);

            eg = new ItemMaster();

            eg.item = "B640715-A-00-1-0-01";
            eg.description = "A0001-11-009 ปลอกเพลาขับแกน X";
            eg.search_key_i = "A0001-11-009 ปลอ";
            eg.search_key_ii = "A0001-11-009 ปลอ";
            eg.item_type = "PRODUCT";
            eg.item_group = "WIP";
            eg.order_system = "PLANNED";
            eg.inventory_unit = "PCS";
            eg.serialized = "NO";
            eg.derived_from_item = "B640715-A-00-1-0-01";
            eg.customizable = "YES";
            eg.with_pcs = "YES";
            eg.cost_component = "YES";
            eg.size = "YES";
            eg.standard = "YES";
            eg.weight = 0;
            eg.weight_unit = "0";
            eg.product_type = "0";
            eg.product_class = "0";
            eg.product_line = "0";
            eg.default_supply_source = "JOB SHOP";
            eg.material = "JOB SHOP";
            eg.order_quantity_increment = 1;
            eg.minimum_order_quantity = 0;
            eg.maximum_order_quantity = 9999999;
            eg.fixed_order_quantity = 1;
            eg.reorder_point = 0;
            eg.order_interval = 1;
            eg.safety_stock = 0;
            eg.bom_quantity = 1;
            eg.routing_quantity = 1;
            eg.purchase_unit = "PCS";
            eg.purchase_price_unit = "PCS";
            eg.purchase_price_group = "101";
            eg.purchase_statistics_group = "101";
            eg.purchase_currency = "THB";
            eg.purchase_price = 0;
            eg.supply_time = 0;
            eg.sales_unit = "PCS";
            eg.sales_price_unit = "PCS";
            eg.sales_price_group = "101";
            eg.sales_statistics_group = "101";
            eg.sales_currency = "THB";
            eg.sales_price = 0;
            eg.warehouse_item_sales = "FG";
            eg.warehouse_item_ordering = "FG";
            eg.plan_level = "1";
            eg.ordering_warehouse = "FG";
            eg.operations_horizon = "365";
            eg.order_horizon = "365";
            eg.planning_horizon = "365";
            eg.costing_source = "JOB SHOP";
            eg.standard_cost_component_scheme = "101";
            eg.sales_office = "DBI01";

            res.item_master.Add(eg);

            dataRes.data = res;
        }

    }
}
