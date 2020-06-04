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
namespace ASSETKKF_API.Engine.Asset
{
    public class STPOSITASSETAPI : Base<STPOSITASSETReq>
    {
        public STPOSITASSETAPI()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(STPOSITASSETReq dataReq, ResponseAPI dataRes)
        {
            var res = new STPOSITASSETReq();
            var req = new ASSETKKF_MODEL.Request.Asset.STPOSITASSETReq()
            {
                Company = dataReq.Company,
                DeptCode = dataReq.DeptCode,
                DeptLST = dataReq.DeptLST,
                Menu3 = dataReq.Menu3
            };

            //var obj = ASSETKKF_ADO.Mssql.Asset.STPOSITASSETADO.GetInstant().GetSTPOSITASSETLists(req);
            //if (obj == null) { throw new Exception("ไม่พบข้อมูล"); }

            res = req;
            dataRes.data = res;

        }
    }
}
