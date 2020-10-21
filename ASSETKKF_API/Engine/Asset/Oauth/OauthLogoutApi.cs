using ASSETKKF_API.Constant;
using ASSETKKF_MODEL.Request.Oauth;
using ASSETKKF_MODEL.Response;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Asset.Oauth
{
    public class OauthLogoutApi : Base<OauthLoginReq>
    {
        public OauthLogoutApi(IConfiguration configuration)
        {
            AllowAnonymous = false;
            Configuration = configuration;
        }

        protected override void ExecuteChild(OauthLoginReq dataReq, ResponseAPI dataRes)
        {
            DBMode = dataReq.DBMode;
            dataRes.ServerAddr = ConnectionString();
            ASSETKKF_ADO.Mssql.Asset.muTokenAdo.GetInstant(conString).Delete(this.Token, dataReq.usercode);

            StaticValue.GetInstant().TokenKey();
        }
    }
}
