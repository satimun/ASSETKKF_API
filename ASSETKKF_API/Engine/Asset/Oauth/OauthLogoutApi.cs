using ASSETKKF_API.Constant;
using ASSETKKF_MODEL.Request.Oauth;
using ASSETKKF_MODEL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Asset.Oauth
{
    public class OauthLogoutApi : Base<OauthLoginReq>
    {
        public OauthLogoutApi()
        {
            AllowAnonymous = false;
        }

        protected override void ExecuteChild(OauthLoginReq dataReq, ResponseAPI dataRes)
        {
            ASSETKKF_ADO.Mssql.Asset.muTokenAdo.GetInstant().Delete(this.Token, dataReq.usercode);

            StaticValue.GetInstant().TokenKey();
        }
    }
}
