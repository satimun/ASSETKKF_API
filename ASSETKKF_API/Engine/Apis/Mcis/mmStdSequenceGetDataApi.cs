using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Core.Util;
using ASSETKKF_API.Constant;

using ASSETKKF_MODEL.Request.Mcis; 
using ASSETKKF_MODEL.Response;

namespace ASSETKKF_API.Engine.Apis.Mcis
{
    public class mmStdSequenceGetDataApi : Base<mmStdSequenceReq>
    {
        public mmStdSequenceGetDataApi()
        {
            PermissionKey = "ADMIN";

        }

        protected override void ExecuteChild(mmStdSequenceReq dataReq, ResponseAPI dataRes)
        {
            var res = new List<ASSETKKF_MODEL.Response.Mcis.mmStdSequenceRes>();
            var roles = ASSETKKF_ADO.Mssql.Mcis.mmStdSequenceAdo.GetInstant().GetData(dataReq);

            foreach (var x in roles)
            {
                var tmp = new ASSETKKF_MODEL.Response.Mcis.mmStdSequenceRes();

                tmp.DrawingCd = x.DrawingCd;
                tmp.SeqNo = x.SeqNo;
                tmp.WorkStationGrp = x.WorkStationGrp;
                tmp.TaskCode = x.TaskCode;
                tmp.WorkCenterCode = x.WorkCenterCode;
                tmp.MachCode = x.MachCode;
                tmp.SetupTime = x.SetupTime;
                tmp.NoOfMins = x.NoOfMins;
                tmp.ProductionRate = x.ProductionRate;
                tmp.UnitCode = x.UnitCode;
                tmp.TimeCode = x.TimeCode;
                tmp.CycleTime = x.CycleTime;
                tmp.LastUpdDt = x.LastUpdDt;
                tmp.LastUpdUser = x.LastUpdUser;
                tmp.SECM_NO = x.SECM_NO;
                tmp.Tm = x.Tm;
                tmp.USER_ID = x.USER_ID;
                tmp.USER_DATE = x.USER_DATE;
                tmp.RoutingDateSt = x.RoutingDateSt;
                tmp.RoutingDateEnd = x.RoutingDateEnd;
                tmp.NewFlag = x.NewFlag;

                res.Add(tmp);
            }
            dataRes.data = res;
        }
    }
}
