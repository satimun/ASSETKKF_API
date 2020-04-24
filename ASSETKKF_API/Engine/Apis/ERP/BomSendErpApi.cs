using Core.Util;
using CORE.SendRequest;
using ASSETKKF_MODEL.Common;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.ERP;
using ASSETKKF_MODEL.Send.Erp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Apis.ERP
{
    public class BomSendErpApi : Base<IDCodeDesModel>
    {
        public BomSendErpApi()
        {
            PermissionKey = "ADMIN";
        }

        protected override void ExecuteChild(IDCodeDesModel dataReq, ResponseAPI dataRes)
        {

            var amw_refId = "SBOM" + DateTimeUtil.ToRefId(DateTime.Now) + "-" + "0001";
            var _sendUrl = "";
            var _json = "";
            var reJson = "";

            ResponseErp resErp = new ResponseErp();


            var res = new BomSend();
            res.amw_refId = amw_refId;
            res.bom_h = new List<BomH>();

            BomH eg = new BomH();
            eg.bom_d = new List<BomD>();

            BomD egd = new BomD();


            egd.product = "B000000-A-01-4-3-00";
            egd.bom_quantity = 1;
            egd.position = "10";
            egd.item = "2762000000";
            egd.warehouse = "PRD";
            egd.net_quantity = 4;
            egd.scrap_percentage = 0;
            egd.operation = "10";

            eg.bom_d.Add(egd);

            egd = new BomD();
            egd.seq_item = 1;
            egd.product = "B000000-A-01-4-3-00";
            egd.bom_quantity = 1;
            egd.position = "20";
            egd.item = "B000000-0-01-4-0-03";
            egd.warehouse = "WIP";
            egd.net_quantity = 5;
            egd.scrap_percentage = 0;
            egd.operation = "10";

            eg.bom_d.Add(egd);

            eg.seq_product = 1;
            eg.product = "B000000-A-01-4-3-00";
            eg.effective_date = "12/16/2019  22:27:15";
            eg.use_for_planning = "YES";
            eg.use_for_costing = "YES";
            eg.routing = "001";
            eg.bom_quantity = 1;

            res.bom_h.Add(eg);


            //---
            eg = new BomH();
            eg.bom_d = new List<BomD>();

            egd = new BomD();

            egd.seq_item = 1;
            egd.product = "";
            egd.bom_quantity = 0;
            egd.position = "";
            egd.item = "";
            egd.warehouse = "";
            egd.net_quantity = 0;
            egd.scrap_percentage = 0;
            egd.operation = "";

            eg.bom_d.Add(egd);


            eg.seq_product = 2;
            eg.product = "B000000-0-01-4-0-02";
            eg.effective_date = "12/16/2019  22:27:15";
            eg.use_for_planning = "YES";
            eg.use_for_costing = "YES";
            eg.routing = "001";
            eg.bom_quantity = 1;

            res.bom_h.Add(eg);

            dataRes.data = res;

            if (res != null)
            {
                if (res.bom_h.Count > 0)
                {
                    _sendUrl = "https://localhost:44347/weatherforecast/ExErpItemMaster"; 
                    _json = "";
                    reJson = "";
                    _json = Newtonsoft.Json.JsonConvert.SerializeObject(res);
                    reJson = SendErpRequest.SendRequest("POST", _sendUrl, _json);
                    resErp = JsonConvert.DeserializeObject<ResponseErp>(reJson);


                }
            }

        }

    }
}

