using ASSETKKF_MODEL.Common;
using ASSETKKF_MODEL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ASSETKKF_MODEL.Send.Erp;
using CORE.SendRequest;
using System.Text;
using System.Net.Http;
using Core.Util;
using ASSETKKF_MODEL.Response.ERP;
using Newtonsoft.Json;

namespace ASSETKKF_API.Engine.Apis.ERP
{
    public class ItemMasterSendErpApi : Base<IDCodeDesModel>
    {
        public ItemMasterSendErpApi()
        {
            PermissionKey = "ADMIN";
        }

        protected override void ExecuteChild(IDCodeDesModel dataReq, ResponseAPI dataRes)
        {
            var amw_refId = "SITM" + DateTimeUtil.ToRefId(DateTime.Now) + "-" + "0002";
            var _sendUrl = "";
            var _json = "";
            var reJson = "";

            ResponseErp resErp = new ResponseErp();

            var res = new  ItemMasterSend();        

            res.amw_refId = amw_refId;
            res.item_master = new List<ItemMaster>();
            
            ItemMaster eg = new ItemMaster();
            eg.seq_item = 1;

            //eg.item = "B640715-0-00-0-0-00";
            eg.item = "T999999-0-00-0-0-05";
            eg.description = "SRM UNIT LOAD DDH15 M.นน700 KG.(PALLETSIZE1.10X1.35X1.45 M.)";
            eg.search_key_i = "SRM UNIT LOAD DD";
            eg.search_key_ii = "SRM UNIT LOAD DD";
            eg.item_type = "30";//PRODUCT
            eg.item_group = "MFG";
            eg.order_system = "2";//PLANNED
            eg.inventory_unit = "set";
            eg.serialized = "2";
            eg.derived_from_item = "T999999-0-00-0-0-05";
            eg.customizable = "1";
            eg.with_pcs = "1";
            eg.cost_component = "1";
            eg.size = "1";
            eg.standard = "1";
            eg.weight = (Decimal)0.0;
            eg.weight_unit = "1";
            eg.product_type = "";
            eg.product_class = "102";//101 insert 102 edit
            eg.product_line = "";
            eg.default_supply_source = "20";//JOB SHOP
            eg.material = "JOB SHOP";
            eg.order_quantity_increment = 1;
            eg.minimum_order_quantity = (Decimal)0.0;
            eg.maximum_order_quantity = (Decimal)9999999.0;
            eg.fixed_order_quantity = (Decimal)1.0;
            eg.reorder_point = (Decimal)0.0;
            eg.order_interval = (Decimal)1.0;
            eg.safety_stock = (Decimal)0.0;
            eg.safety_time = (Decimal)0.0;
            eg.bom_quantity = (Decimal)1.0;
            eg.routing_quantity = (Decimal)1.0;
            eg.purchase_unit = "set";
            eg.purchase_price_unit = "set";
            eg.purchase_price_group = "101";
            eg.purchase_statistics_group = "101";
            eg.purchase_currency = "THB";
            eg.purchase_price = (Decimal) 0.0;

            eg.vendorRating = 1;
            eg.inspection   = 2;

            eg.supply_time = (Decimal)0.0;
            eg.sales_unit = "set";
            eg.sales_price_unit = "set";
            eg.sales_price_group = "101";
            eg.sales_statistics_group = "101";
            eg.sales_currency = "THB";
            eg.sales_price = (Decimal)0.0;
            eg.warehouse_item_sales = "FG";
            eg.warehouse_item_purch = "FG";
            eg.warehouse_item_ordering = "FG";
            eg.plan_level = "1";
            eg.ordering_warehouse = "FG";
            eg.operations_horizon = "365";
            eg.order_horizon = "365";
            eg.planning_horizon = "365";
            eg.costing_source = "20";//JOB SHOP
            eg.standard_cost_component_scheme = "101";
            eg.sales_office = "DBI01";
            eg.quality_group = "";

            res.item_master.Add(eg);
            /*
            eg = new ItemMaster();

            eg.seq_item = 2;
            eg.item = "TEST20-1-24-0-0-02";
            eg.description = "TEST SEND ITEM MASTER 2";
            eg.search_key_i = "TEST SEND ITEM MASTER 2";
            eg.search_key_ii = "TEST SEND ITEM MASTER 2";
            eg.item_type = "30";//PRODUCT
            eg.item_group = "MFG";
            eg.order_system = "2";//PLANNED
            eg.inventory_unit = "set";
            eg.serialized = "2";
            eg.derived_from_item = "TEST20-1-24-0-0-02";
            eg.customizable = "1";
            eg.with_pcs = "1";
            eg.cost_component = "1";
            eg.size = "1";
            eg.standard = "1";
            eg.weight = (Decimal)0.0;
            eg.weight_unit = "1";
            eg.product_type = "";
            eg.product_class = "";
            eg.product_line = "";
            eg.default_supply_source = "20";//JOB SHOP
            eg.material = "JOB SHOP";
            eg.order_quantity_increment = 1;
            eg.minimum_order_quantity = (Decimal)0.0;
            eg.maximum_order_quantity = (Decimal)9999999.0;
            eg.fixed_order_quantity = (Decimal)1.0;
            eg.reorder_point = (Decimal)0.0;
            eg.order_interval = (Decimal)1.0;
            eg.safety_stock = (Decimal)0.0;
            eg.safety_time = (Decimal)0.0;
            eg.bom_quantity = (Decimal)1.0;
            eg.routing_quantity = (Decimal)1.0;
            eg.purchase_unit = "set";
            eg.purchase_price_unit = "set";
            eg.purchase_price_group = "101";
            eg.purchase_statistics_group = "101";
            eg.purchase_currency = "THB";
            eg.purchase_price = (Decimal)0.0;

            eg.vendorRating = 1;
            eg.inspection = 2;

            eg.supply_time = (Decimal)0.0;
            eg.sales_unit = "set";
            eg.sales_price_unit = "set";
            eg.sales_price_group = "101";
            eg.sales_statistics_group = "101";
            eg.sales_currency = "THB";
            eg.sales_price = (Decimal)0.0;
            eg.warehouse_item_sales = "FG";
            eg.warehouse_item_purch = "FG";
            eg.warehouse_item_ordering = "FG";
            eg.plan_level = "1";
            eg.ordering_warehouse = "FG";
            eg.operations_horizon = "365";
            eg.order_horizon = "365";
            eg.planning_horizon = "365";
            eg.costing_source = "20";//JOB SHOP
            eg.standard_cost_component_scheme = "101";
            eg.sales_office = "DBI01";
            eg.quality_group = "";

            res.item_master.Add(eg);
            */

            dataRes.data = res;

            if (res != null)
            {
                if (res.item_master.Count > 0)
                {
                     /*_sendUrl = "https://localhost:44347/weatherforecast/ExErpItemMaster";*/
                    _sendUrl = "https://erplnaddonapi.kigintergroup.com:4433/api/ItemMaster";
                    _json  = "";
                     reJson = "";

                     _json  = Newtonsoft.Json.JsonConvert.SerializeObject(res);                     
                     reJson = SendErpRequest.SendRequest("POST", _sendUrl, _json);
                   
                     resErp = JsonConvert.DeserializeObject<ResponseErp>(reJson);

                     
                }
            }
        }

        

    }
}
