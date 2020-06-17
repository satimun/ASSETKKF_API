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
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response.Asset;

namespace ASSETKKF_API.Engine.Asset.AUDITCUT
{
    public class AuditCutDepAPI : Base<AuditCutReq>
    {
        public AuditCutDepAPI()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(AuditCutReq dataReq, ResponseAPI dataRes)
        {
            var res = new AuditCutListRes();
            try
            {
                var req = new ASSETKKF_MODEL.Request.Asset.AuditCutReq()
                {
                    Company = dataReq.Company,
                    DeptCode = dataReq.DeptCode,
                    DeptLST = dataReq.DeptLST,
                    Menu3 = dataReq.Menu3
                };

                var obj = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAuditDepList(req);
                if (obj == null)
                {
                    res._result._code = "404";
                    res._result._message = "ไม่พบข้อมูล";
                    res._result._status = "Not Found";

                }
                else
                {
                    res._result._code = "200";
                    res._result._message = "";
                    res._result._status = "OK";
                }

                res.auditCutNoLst = obj;
            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }

            dataRes.data = res;

        }
    }
}
