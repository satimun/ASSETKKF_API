using ASSETKKF_MODEL.Request.Mcis;
using ASSETKKF_MODEL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Apis.Mcis
{
    public class msWorkStationGrpGetDataApi : Base<msWorkStationGrpReq>
    {
        public msWorkStationGrpGetDataApi()
        {
            PermissionKey = "ADMIN";

        }

        protected override void ExecuteChild(msWorkStationGrpReq dataReq, ResponseAPI dataRes)
        {
            var res = new List<ASSETKKF_MODEL.Response.Mcis.msWorkStationGrpRes>();
            var roles = ASSETKKF_ADO.Mssql.Mcis.msWorkStationGrpAdo.GetInstant().GetData(dataReq);

            foreach (var x in roles)
            {
                var tmp = new ASSETKKF_MODEL.Response.Mcis.msWorkStationGrpRes();

                tmp.WorkStationGrpCd = x.WorkStationGrpCd;
                tmp.WorkStationGrpNm = x.WorkStationGrpNm;
                tmp.DLCost = x.DLCost;
                tmp.FOHCost = x.FOHCost;
                tmp.MDCost = x.MDCost;
                tmp.WorkStationCD = x.WorkStationCD;
                tmp.LastUpdUsr = x.LastUpdUsr;
                tmp.LastUpdDt = x.LastUpdDt;
                tmp.UnionDLCost = x.UnionDLCost;
                tmp.WorkStationNM = x.WorkStationNM;
                tmp.MulEmp_flag = x.MulEmp_flag;
                tmp.ExtGrp_Flag = x.ExtGrp_Flag;

                res.Add(tmp);
            }
            dataRes.data = res;
        }
    }
}
