using ASSETKKF_MODEL.Request.Report;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Apis.Report
{
    public class RptFollowsWorkOrderLastApi : Base<RptWorkOrderReq>
    {
        public RptFollowsWorkOrderLastApi()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(RptWorkOrderReq dataReq, ResponseAPI dataRes)
        {

            var res = ASSETKKF_ADO.Mssql.Mcis.Report.RptFollowsWorkOrderAdo.GetInstant().GetLast(dataReq.WorkOrderID);

            dataRes.data = res;

        }
    }
}
