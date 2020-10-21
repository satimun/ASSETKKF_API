using ASSETKKF_API.Constant;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Oauth;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Asset.Oauth
{
    public class OauthAccessTokenGetApi : Base<dynamic>
    {
        public OauthAccessTokenGetApi(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }

        protected override void ExecuteChild(dynamic dataReq, ResponseAPI dataRes)
        {
            DBMode = Convert.ToString(dataReq);
            dataRes.ServerAddr = ConnectionString();
            var res = ASSETKKF_ADO.Mssql.Asset.muAccessTokenAdo.GetInstant(conString).Search(AccessToken);

            if (res.Count == 1)
            {
                ASSETKKF_ADO.Mssql.Asset.muAccessTokenAdo.GetInstant(conString).Update(this.AccessToken);
            }
            else
            {
                this.AccessToken = Core.Util.EncryptUtil.NewID(this.IPAddress);
                ASSETKKF_ADO.Mssql.Asset.muAccessTokenAdo.GetInstant(conString).Insert(this.AccessToken, this.IPAddress, this.UserAgent);
            }

            dataRes.data = new OauthAccessTokenGetRes() { accessToken = this.AccessToken };

            StaticValue.GetInstant().AccessKey();

        }
    }
}
