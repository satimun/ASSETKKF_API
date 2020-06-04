using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;

namespace ASSETKKF_API.Engine.Asset.AUDITCUT
{
    public class ASSETOFFICECODEAPI : Base<ASSETOFFICECODEReq>
    {
        public ASSETOFFICECODEAPI()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(ASSETOFFICECODEReq dataReq, ResponseAPI dataRes)
        {
            var res = new ASSETKKF_MODEL.Response.Asset.ASSETOFFICECODERes();

            try
            {
                var lst = ASSETKKF_ADO.Mssql.Asset.AUDITPOSTTRNADO.GetInstant().getASSETOFFICECODELST(dataReq);

                res.ASSETOFFICECODELST = lst;

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
