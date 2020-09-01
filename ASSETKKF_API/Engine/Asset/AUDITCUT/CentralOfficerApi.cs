using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;

namespace ASSETKKF_API.Engine.Asset.AUDITCUT
{
    public class CentralOfficerApi : Base<AuditCutInfoReq>
    {
        public CentralOfficerApi()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(AuditCutInfoReq dataReq, ResponseAPI dataRes)
        {
            var res = new ASSETKKF_MODEL.Response.Asset.AuditCutInfoRes();
            try
            {
                var objLeaderList = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getCentralOfficerLst(dataReq);
                res.auditCutLeaderList = objLeaderList;

                res._result._code = "200";
                res._result._message = "";
                res._result._status = "OK";
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
