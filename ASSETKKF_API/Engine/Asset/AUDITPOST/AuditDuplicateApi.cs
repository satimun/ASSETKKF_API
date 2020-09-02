using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Audit;

namespace ASSETKKF_API.Engine.Asset.AUDITPOST
{
    public class AuditDuplicateApi : Base<AuditPostReq>
    {
        public AuditDuplicateApi()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(AuditPostReq dataReq, ResponseAPI dataRes)
        {
            var res = new AuditDuplicateRes();

            try
            {
                switch (dataReq.mode.Trim().ToLower())
                {
                    case "getduplicate":
                        res = getDuplicate(dataReq,res);
                        break;
                }
            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }
            dataRes.data = res;
        }

        private AuditDuplicateRes getDuplicate(AuditPostReq dataReq, AuditDuplicateRes res)
        {
            try
            {
                var lstPostMST = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTAdo.GetInstant().getPOSTMSTDuplicate(dataReq);
                res.POSTMSTDuplicateLST = lstPostMST;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNDuplicate(dataReq);
                res.POSTTRNDuplicateLST = lstPostTRN;

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


            return res;
        }



    }
}
