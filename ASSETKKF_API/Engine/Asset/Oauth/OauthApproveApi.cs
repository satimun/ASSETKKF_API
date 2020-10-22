using Core.Util;
using ASSETKKF_API.Constant;
using ASSETKKF_MODEL.Request.Oauth;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Oauth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Engine.Asset.Oauth
{
    public class OauthApproveApi : Base<OauthLoginReq>
    {
        public OauthApproveApi(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }

        protected override void ExecuteChild(OauthLoginReq dataReq, ResponseAPI dataRes)
        {
            DBMode = dataReq.DBMode;
            
            var res = new OauthLoginRes();
            res._result.ServerAddr = ConnectionString();
            res._result.DBMode = DBMode;

            var userApprove = new ASSETKKF_MODEL.Data.Mssql.Asset.STUSERASSET();

            userApprove.UCODE = dataReq.usercode;



            try
            {
                var roles = ASSETKKF_ADO.Mssql.Asset.STUSERASSETAdo.GetInstant().CheckApprover(userApprove,conString);

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

                    var user = ASSETKKF_ADO.Mssql.Asset.STUSERASSETAdo.GetInstant().Search(new ASSETKKF_MODEL.Data.Mssql.Asset.STUSERASSET() { UCODE = dataReq.usercode.Trim() },null,conString).FirstOrDefault();
                    if (user == null) { throw new Exception("Username Not Found."); }
                    if (user.STAEMP == "9") { throw new Exception("พ้นสภาพพนักงาน ไม่มีสิทธิ์เข้าใช้โปรแกรม."); }
                    if (user.A_Review == "N") { throw new Exception("ถูกยกเลิกสิทธิ์เข้าใช้โปรแกรม."); }

                    //var pass = Core.Util.EncryptUtil.Hash(dataReq.password.Trim());
                    var pass = Core.Util.EncryptUtil.ENDCodeNEW(dataReq.password.Trim());

                    
                    if (user.PCODE == pass)
                    {
                        var _token = Core.Util.EncryptUtil.Hash(pass);
                        res.token = _token.NewID();
                        res.username = user.OFFICECODE;
                        res.usercode = user.UCODE;

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
