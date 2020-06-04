using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;

namespace ASSETKKF_API.Engine.Asset.AUDITCUT
{
    public class ASSETASSETNOCheckAPI : Base<ASSETASSETNOReq>
    {
        public ASSETASSETNOCheckAPI()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(ASSETASSETNOReq dataReq, ResponseAPI dataRes)
        {
            var res = new ASSETKKF_MODEL.Response.Asset.ASSETASSETNORes();

            try
            {
                var obj = ASSETKKF_ADO.Mssql.Asset.AUDITPOSTTRNADO.GetInstant().checkASSETASSETNO(dataReq);

                res.ASSETASSETNO = obj;

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
