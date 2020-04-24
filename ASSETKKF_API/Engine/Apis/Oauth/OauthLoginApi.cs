using Core.Util;
using ASSETKKF_API.Constant;
using ASSETKKF_MODEL.Request.Oauth;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Oauth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Apis.Oauth
{
    public class OauthLoginApi : Base<OauthLoginReq>
    {
        public OauthLoginApi()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(OauthLoginReq dataReq, ResponseAPI dataRes)
        {
            var res = new OauthLoginRes();

            var user = ASSETKKF_ADO.Mssql.Mcis.zUserAdo.GetInstant().Search(new ASSETKKF_MODEL.Data.Mssql.Mcis.zUser() { UserCode = dataReq.username.Trim() }).FirstOrDefault();
            if (user == null) { throw new Exception("Username Not Found."); }
            if (user.Status != "A") { throw new Exception("Username is not Confirm."); }

            //var pass = Core.Util.EncryptUtil.Hash(dataReq.password.Trim());
            var pass = Core.Util.EncryptUtil.ENDCodeNEW(dataReq.password.Trim());

            /*
              var config = Ado.Mssql.Table.UserConfig.GetInstant().Search(user.ID);
              if (config.Where(x => x.TwoFactorEnable).ToList().Count != 0)
              {
                  var authenticator = new TwoFactorAuthenticator();
                  var isValid = authenticator.ValidateTwoFactorPIN(user.Code, dataReq.twofactor.Replace(" ", ""));
                  if (!isValid)
                  {
                      throw new Exception("T000: 2FA Code invalid.");
                  }
              }

           */
            // if (user.UserPw == Core.Util.EncryptUtil.Hash(pass + user.SoftPassword))
            if (user.UserPw ==  pass  )
            {
                var _token = Core.Util.EncryptUtil.Hash(pass);
                res.token = _token.NewID();
                res.username = user.UserName;
                res.usercode = user.UserCode;

                ASSETKKF_ADO.Mssql.Mcis.muTokenAdo.GetInstant().Insert(new ASSETKKF_MODEL.Data.Mssql.Mcis.muToken()
                {
                    UserCode = user.UserCode,
                    AccessToken_Code = this.AccessToken,
                    Code = res.token,
                    Status = "A",
                    Type = "L",
                    ExpiryTime = DateTime.Now.AddMinutes(480)

                }, user.UserCode);

                /*
                if (config.TrueForAll(x => x.EmailLogin == true))
                {
                    var access = Ado.Mssql.Table.AccessToken.GetInstant().Search(this.AccessToken).FirstOrDefault();
                    string subject = "Login Notification";
                    string body = $"<p><b>Dear {user.Username} ,</b></p>" +
                    $"<p>This is notify you of a successful login to your account.</p>" +
                    $"<p>Login Time: {DateTime.UtcNow.ToString()}</p>" +
                    $"<p>IP Address: {access.IPAddress}</p>" +
                    $"<p>User Agent: {access.Agent}</p>";

                    Task.Run(() => Core.SendMail.SendMail.Send(user.Email, subject, body));
                }
               */
                dataRes.data = res;
                StaticValue.GetInstant().TokenKey();
            }
            else
            {
                throw new Exception("Username or Password was incorrect");
            }
             
        }
    }
}
