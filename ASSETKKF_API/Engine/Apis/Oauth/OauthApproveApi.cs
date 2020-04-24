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
    public class OauthApproveApi : Base<OauthLoginReq>
    {
        public OauthApproveApi()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(OauthLoginReq dataReq, ResponseAPI dataRes)
        {
            var res = new OauthLoginRes();

            var userApprove = new ASSETKKF_MODEL.Request.Mcis.msUserSequenceReq();

            userApprove.USERCODE = dataReq.usercode;



            try
            {
                var roles = ASSETKKF_ADO.Mssql.Mcis.msUserSequenceAdo.GetInstant().GetData(userApprove);

                if (roles.Count <= 0)
                {
                    res = new OauthLoginRes();
                    res.usercode = dataReq.usercode;
                    res._result._status = "F";
                    res._result._code = "F0002";
                    res._result._message = "ไม่มีสิทธิ์ การอนุมัติ";
                                       
                }
                else
                {

                    var user = ASSETKKF_ADO.Mssql.Mcis.zUserAdo.GetInstant().Search(new ASSETKKF_MODEL.Data.Mssql.Mcis.zUser() { UserCode = dataReq.usercode.Trim() }).FirstOrDefault();
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
                    if (user.UserPw == pass)
                    {
                        var _token = Core.Util.EncryptUtil.Hash(pass);
                        res.token = _token.NewID();
                        res.username = user.UserName;
                        res.usercode = user.UserCode;

                        res._result._status = "S";
                        res._result._code = "S0000";
                        res._result._message = "username และ password ถูกต้อง";
                                                
                        
                        StaticValue.GetInstant().TokenKey();
                        
                    }
                    else
                    {
                        res = new OauthLoginRes();
                        res.usercode = dataReq.usercode;
                        res._result._status = "F";
                        res._result._code = "F0002";
                        res._result._message = "username และ password ไม่ถูกต้อง";
                    }
                }

            }
            catch
            {
                res.usercode = dataReq.usercode;
                res._result._status = "F";
                res._result._code = "F0002";
                res._result._message = "การเชื่อมต่อฐานข้อมูลมีปัญหา...";



            }

            dataRes.data = res;
        }
    }
}
