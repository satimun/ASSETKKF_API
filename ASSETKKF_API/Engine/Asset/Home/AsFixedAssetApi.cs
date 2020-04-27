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

namespace ASSETKKF_API.Engine.Asset.Home
{
    public class AsFixedAssetApi : Base<AsFixedAsset>
    {
        public AsFixedAssetApi()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(AsFixedAsset dataReq, ResponseAPI dataRes)
        {
            var res = new AsFixedAsset();

            var obj = ASSETKKF_ADO.Mssql.Asset.AsFixedAssetAdo.GetInstant().Search(new ASSETKKF_MODEL.Data.Mssql.Asset.AsFixedAsset() { OFFICECODE = dataReq.OFFICECODE.Trim() });
            if (obj == null) { throw new Exception("ไม่พบข้อมูล"); }

            res.AsFixedAssetLST = obj;
            dataRes.data = res;

        }
    }
}
