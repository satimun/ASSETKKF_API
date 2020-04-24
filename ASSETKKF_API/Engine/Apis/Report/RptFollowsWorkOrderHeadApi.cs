using ASSETKKF_MODEL.Request.Report;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Apis.Report
{
    public class RptFollowsWorkOrderHeadApi : Base<RptWorkOrderReq>
    {
        public RptFollowsWorkOrderHeadApi()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(RptWorkOrderReq dataReq, ResponseAPI dataRes)
        {
            var tmp = new ASSETKKF_MODEL.Response.Report.RptFollowsWorkOrderHeadRes();
            var res = new List<ASSETKKF_MODEL.Response.Report.RptFollowsWorkOrderHeadRes>();
            try
            {
                var roles = ASSETKKF_ADO.Mssql.Mcis.Report.RptFollowsWorkOrderAdo.GetInstant().GetHead(dataReq.WorkOrderID);
                foreach (var x in roles)
                {
                    tmp = new  ASSETKKF_MODEL.Response.Report.RptFollowsWorkOrderHeadRes();

                    tmp.MchProjectID    = x.MchProjectID;
                    tmp.WorkOrderID     = x.WorkOrderID;    
                    tmp.WorkOrderDesc   = x.WorkOrderDesc;  
                    tmp.DrawingCd       = x.DrawingCd;     
                    tmp.MchProjectNm    = x.MchProjectNm;  
                    tmp.QTYAmt          = x.QTYAmt;         
                    tmp.Cuscod          = x.Cuscod;        
                    tmp.WorkOrderStatus = x.WorkOrderStatus;
                    tmp.JobTypeCd       = x.JobTypeCd;     
                    tmp.JobTypeNm       = x.JobTypeNm;     
                    tmp.JobPriorityCd   = x.JobPriorityCd; 
                    tmp.STATION_GRP     = x.STATION_GRP;    
                    tmp.JobPriorityNm   = x.JobPriorityNm; 

                    res.Add(tmp);
                }
            }
            catch (Exception)
            {

            }

            dataRes.data = res;

        }
    }
}
