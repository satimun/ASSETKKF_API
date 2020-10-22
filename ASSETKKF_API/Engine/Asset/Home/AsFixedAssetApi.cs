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
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Engine.Asset.Home
{
    public class AsFixedAssetApi : Base<AsFixedAsset>
    {
        public AsFixedAssetApi(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }

        protected override void ExecuteChild(AsFixedAsset dataReq, ResponseAPI dataRes)
        {
            var res = new AsFixedAsset();

            try
            {
                DBMode = dataReq.DBMode;
                res._result.ServerAddr = ConnectionString();
                res._result.DBMode = DBMode;

                var obj = ASSETKKF_ADO.Mssql.Asset.AsFixedAssetAdo.GetInstant().Search(dataReq,null,conString);
                //if (obj == null) { throw new Exception("ไม่พบข้อมูล"); }

                res.AsFixedAssetLST = obj;
                dataRes.data = res;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
           

           

        }
    }
}
