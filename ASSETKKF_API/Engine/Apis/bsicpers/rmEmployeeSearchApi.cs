using ASSETKKF_MODEL.Request.bsicpers;
using ASSETKKF_MODEL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Apis.bsicpers
{
    public class rmEmployeeSearchApi : Base<rmEmployeeReq>
    {
        public rmEmployeeSearchApi()
        {
            PermissionKey = "ADMIN";

        }

        protected override void ExecuteChild(rmEmployeeReq dataReq, ResponseAPI dataRes)
        {
            var res = new List<ASSETKKF_MODEL.Response.bsicpers.rmEmployeeRes>();
            var roles = ASSETKKF_ADO.Mssql.bsicpers.rmEmployeeAdo.GetInstant().GetData(dataReq);

            foreach (var x in roles)
            {
                var tmp = new ASSETKKF_MODEL.Response.bsicpers.rmEmployeeRes();

                tmp.EmployeeID = x.EmployeeID;
                tmp.EmploType = x.EmploType;
                tmp.Status = x.Status;
                tmp.Shift = x.Shift;
                tmp.Weekend = x.Weekend;
                tmp.DepCode = x.DepCode;
                tmp.Position = x.Position;
                tmp.TitleName = x.TitleName;
                tmp.FirstName = x.FirstName;
                tmp.LastName = x.LastName;
                tmp.StartDate = x.StartDate;
                tmp.EmploDate = x.EmploDate;
                tmp.EndDate = x.EndDate;
                tmp.EditDate = x.EditDate;

                res.Add(tmp);
            }
            dataRes.data = res;
        }
    }
}
