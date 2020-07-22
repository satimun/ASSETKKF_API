using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Asset;

namespace ASSETKKF_API.Engine.Asset.AUDITCUT
{
    public class AuditAddsetAPI : Base<AuditPostReq>
    {
        public AuditAddsetAPI()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(AuditPostReq dataReq, ResponseAPI dataRes)
        {
            var res = new AuditPostRes();
            try
            {
                var lstAUDITPOSTMST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(dataReq);
                var lstWait = lstAUDITPOSTMST.Where(p => String.IsNullOrEmpty(p.PCODE)).ToList();
                var lstChecked = lstAUDITPOSTMST.Where(p => !String.IsNullOrEmpty(p.PCODE)).ToList();
                res.AUDITPOSTMSTWAITLST = lstWait;
                res.AUDITPOSTMSTCHECKEDLST = lstChecked;

                //res.AUDITPOSTMSTWAITLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(dataReq, "");
                //res.AUDITPOSTMSTCHECKEDLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(dataReq, "Y");
                res.AUDITPOSTTRNLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTTRN(dataReq);

                res.AREACODE = dataReq.AREACODE;
                res.COMPANY = dataReq.COMPANY;
                res.DEPCODEOL = dataReq.DEPCODEOL;
                res.LEADERCODE = dataReq.LEADERCODE;
                res.SQNO = dataReq.SQNO;

                res._result._code = "201";
                res._result._message = "";
                res._result._status = "Created";

            }
            catch(Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";

            }
            dataRes.data = res;
        }
    }
}
