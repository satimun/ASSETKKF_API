using ASSETKKF_MODEL.Data.Mssql.Mcis;
using ASSETKKF_MODEL.Request.Report;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Apis.Report
{
    public class RptFollowsWorkOrderApi : Base<RptWorkOrderReq>
    {
        public RptFollowsWorkOrderApi()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(RptWorkOrderReq dataReq, ResponseAPI dataRes)
        {
             


            List<RptFollowsWorkOrderHeadRes> resHead = new List<RptFollowsWorkOrderHeadRes>();
            resHead = ASSETKKF_ADO.Mssql.Mcis.Report.RptFollowsWorkOrderAdo.GetInstant().GetHead(dataReq.WorkOrderID);
            
            List<RptFollowsWorkOrderDetailRes> resDetail = new List<RptFollowsWorkOrderDetailRes>();
            resDetail = ASSETKKF_ADO.Mssql.Mcis.Report.RptFollowsWorkOrderAdo.GetInstant().GetDetail(dataReq.WorkOrderID);

            List<RptFollowsWorkOrderLastRes> resLast = new List<RptFollowsWorkOrderLastRes>();
            resLast = ASSETKKF_ADO.Mssql.Mcis.Report.RptFollowsWorkOrderAdo.GetInstant().GetLast(dataReq.WorkOrderID);

            RptFollowsWorkOrderRes _data = new RptFollowsWorkOrderRes();

            _data.WorkOrderID = dataReq.WorkOrderID;
            _data.RptFollowsWorkOrder = new RptFollowsWorkOrderViewRes();

            if (resHead.Count > 0)
            {
                
                _data.RptFollowsWorkOrder.WorkOrderHead = resHead[0];
                _data.RptFollowsWorkOrder.WorkOrderDetail= resDetail;
                _data.RptFollowsWorkOrder.WorkOrderLast = resLast;
            }                    

             

            
            dataRes.data = _data;




        }
    }
}
