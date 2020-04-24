using Core.Util;
using ASSETKKF_API.Constant;
using ASSETKKF_MODEL.Request.Oauth;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Oauth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Asset.Oauth
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

            var user = ASSETKKF_ADO.Mssql.Asset.STUSERASSETAdo.GetInstant().Search(new ASSETKKF_MODEL.Data.Mssql.Asset.STUSERASSET() { UCODE = dataReq.username.Trim() });
            if (user == null) { throw new Exception("ไม่พบชื่อผู้ใช้งาน"); }

            var STAEMP = user.Where(s => s.STAEMP == "9")
                              .Select(s => s)
                              .ToList();
            if (STAEMP.Count > 0) { throw new Exception("พ้นสภาพพนักงาน ไม่มีสิทธิ์เข้าใช้โปรแกรม."); }

            var Permission = user.Where(s => s.A_Review == "Y")
                              .Select(s => s)
                              .ToList();

            if (Permission.Count == 0) { throw new Exception("ถูกยกเลิกสิทธิ์เข้าใช้โปรแกรม."); }

            var pass = Core.Util.EncryptUtil.ENDCodeNEW(dataReq.password.Trim());
            pass = dataReq.password.Trim();
            var Password = user.Where(s => s.PCODE == pass)
                              .Select(s => s)
                              .ToList();

            
            if (Password.Count > 0)
            {
                var _token = Core.Util.EncryptUtil.Hash(pass);
                res.token = _token.NewID();

                var obj = user.FirstOrDefault();
                res.username = obj.OFNAME;
                res.usercode = obj.OFFICECODE;
                res.deptcode = obj.DEPCODEEOL;
                res.deptname = obj.NAMCENTTHA;
                res.codcomp = obj.CODCOMP;
                res.codposname = obj.CODPOSNAME;
                res.cospos = obj.COSPOS;

                res.COMPANYLST = user.Where(s => s.A_Review == "Y")
                              .Select(s => s.COMPANY)
                              .ToList();

                ASSETKKF_ADO.Mssql.Asset.muTokenAdo.GetInstant().Insert(new ASSETKKF_MODEL.Data.Mssql.Asset.muToken()
                {
                    UserCode = obj.UCODE,
                    AccessToken_Code = this.AccessToken,
                    Code = res.token,
                    Status = "A",
                    Type = "L",
                    ExpiryTime = DateTime.Now.AddMinutes(480)

                }, obj.UCODE);

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
