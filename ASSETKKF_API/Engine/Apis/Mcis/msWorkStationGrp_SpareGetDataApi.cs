using ASSETKKF_MODEL.Request.Mcis;
using ASSETKKF_MODEL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Apis.Mcis
{
    
    public class msWorkStationGrp_SpareGetDataApi : Base<msWorkStationGrp_SpareReq>
    {
        public msWorkStationGrp_SpareGetDataApi()
        {
            PermissionKey = "ADMIN";

        }

        protected override void ExecuteChild(msWorkStationGrp_SpareReq dataReq, ResponseAPI dataRes)
        {
            var res = new List<ASSETKKF_MODEL.Response.Mcis.msWorkStationGrp_SpareRes>();
            var roles = ASSETKKF_ADO.Mssql.Mcis.msWorkStationGrp_SpareAdo.GetInstant().GetData(dataReq);

            foreach (var x in roles)
            {
                var tmp = new ASSETKKF_MODEL.Response.Mcis.msWorkStationGrp_SpareRes();

                tmp.WorkStationGrpCd = x.WorkStationGrpCd;
                tmp.WorkStationGrpCd_Spare = x.WorkStationGrpCd_Spare;
                tmp.User_Id = x.User_Id;
                tmp.User_date = x.User_date;

                res.Add(tmp);
            }
            dataRes.data = res;
        }
    }
}
