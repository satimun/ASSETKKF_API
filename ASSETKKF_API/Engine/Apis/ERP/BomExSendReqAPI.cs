using ASSETKKF_MODEL.Common;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Send.Erp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Apis.ERP
{
    public class BomExSendReqAPI : Base<IDCodeDesModel>
    {
        public BomExSendReqAPI()
        {
            PermissionKey = "ADMIN";
        }

        protected override void ExecuteChild(IDCodeDesModel dataReq, ResponseAPI dataRes)
        {
            var res = new BomSend();
            res.amw_refId = "SBOM20191221-145250123-0001";
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

            egd.product = "B000000-A-01-4-3-00";
            egd.bom_quantity = 1;
            egd.position = "20";
            egd.item = "B000000-0-01-4-0-03";
            egd.warehouse = "WIP";
            egd.net_quantity = 5;
            egd.scrap_percentage = 0;
            egd.operation = "10";

            eg.bom_d.Add(egd);

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

            egd.product = "";
            egd.bom_quantity = 0;
            egd.position = "";
            egd.item = "";
            egd.warehouse = "";
            egd.net_quantity = 0;
            egd.scrap_percentage = 0;
            egd.operation = "";

            eg.bom_d.Add(egd);



            eg.product = "B000000-0-01-4-0-02";
            eg.effective_date = "12/16/2019  22:27:15";
            eg.use_for_planning = "YES";
            eg.use_for_costing = "YES";
            eg.routing = "001";
            eg.bom_quantity = 1;

            res.bom_h.Add(eg);

            dataRes.data = res;
        }

    }
}


