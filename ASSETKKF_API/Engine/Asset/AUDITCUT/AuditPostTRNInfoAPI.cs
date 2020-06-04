using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;

namespace ASSETKKF_API.Engine.Asset.AUDITCUT
{
    public class AuditPostTRNInfoAPI : Base<AuditPostTRNReq>
    {
        public AuditPostTRNInfoAPI()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(AuditPostTRNReq dataReq, ResponseAPI dataRes)
        {
            var res = new ASSETKKF_MODEL.Response.Asset.AuditPostTRNRes();

            try
            {
                var obj = ASSETKKF_ADO.Mssql.Asset.AUDITPOSTTRNADO.GetInstant().getAUDITPOSTTRN(dataReq);

                res.AuditAssetPostTRN = obj.FirstOrDefault();

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
