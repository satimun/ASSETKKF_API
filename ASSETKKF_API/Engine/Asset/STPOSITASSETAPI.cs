using Core.Util;
using ASSETKKF_API.Constant;
using ASSETKKF_MODEL.Request.Oauth;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Oauth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Request.Asset;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Engine.Asset
{
    public class STPOSITASSETAPI : Base<STPOSITASSETReq>
    {
        public STPOSITASSETAPI(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }

        protected override void ExecuteChild(STPOSITASSETReq dataReq, ResponseAPI dataRes)
        {
            DBMode = dataReq.DBMode;
            
            var res = new STPOSITASSETReq();
            res._result.ServerAddr = ConnectionString();

            var req = new ASSETKKF_MODEL.Request.Asset.STPOSITASSETReq()
            {
                Company = dataReq.Company,
                DeptCode = dataReq.DeptCode,
                DeptLST = dataReq.DeptLST,
                Menu3 = dataReq.Menu3,
                Menu4 = dataReq.Menu4
            };

            //var obj = ASSETKKF_ADO.Mssql.Asset.STPOSITASSETADO.GetInstant().GetSTPOSITASSETLists(req);
            //if (obj == null) { throw new Exception("ไม่พบข้อมูล"); }

            res = req;
            dataRes.data = res;

        }
    }
}
