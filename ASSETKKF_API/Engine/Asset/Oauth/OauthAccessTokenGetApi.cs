using ASSETKKF_API.Constant;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Oauth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Asset.Oauth
{
    public class OauthAccessTokenGetApi : Base<dynamic>
    {
        public OauthAccessTokenGetApi()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(dynamic dataReq, ResponseAPI dataRes)
        {

            var res = ASSETKKF_ADO.Mssql.Asset.muAccessTokenAdo.GetInstant().Search(AccessToken);

            if (res.Count == 1)
            {
                ASSETKKF_ADO.Mssql.Asset.muAccessTokenAdo.GetInstant().Update(this.AccessToken);
            }
            else
            {
                this.AccessToken = Core.Util.EncryptUtil.NewID(this.IPAddress);
                ASSETKKF_ADO.Mssql.Asset.muAccessTokenAdo.GetInstant().Insert(this.AccessToken, this.IPAddress, this.UserAgent);
            }

            dataRes.data = new OauthAccessTokenGetRes() { accessToken = this.AccessToken };

            StaticValue.GetInstant().AccessKey();

        }
    }
}
