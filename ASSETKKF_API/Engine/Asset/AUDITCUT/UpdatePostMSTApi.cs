using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;


namespace ASSETKKF_API.Engine.Asset.AUDITCUT
{
    public class UpdatePostMSTApi : Base<ASSETASSETNOReq>
    {
        public UpdatePostMSTApi()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(ASSETASSETNOReq dataReq, ResponseAPI dataRes)
        {
            var res = new ASSETKKF_MODEL.Response.Asset.ASSETASSETNORes();
            try
            {
                var reqProblem = new STProblemReq
                {
                    Company = dataReq.COMPANY
                };
                var lstProblem = ASSETKKF_ADO.Mssql.Asset.STProblemADO.GetInstant().Search(reqProblem);
                var objProblem = lstProblem.FirstOrDefault();

                var reqPostMst = new AUDITPOSTMSTReq
                {
                    SQNO = dataReq.SQNO,
                    DEPCODEOL = dataReq.DEPCODEOL,
                    COMPANY = dataReq.COMPANY,
                    LEADERCODE = dataReq.LEADERCODE,
                    LEADERNAME = dataReq.LEADERNAME,
                    AREACODE = dataReq.AREACODE,
                    AREANAME = dataReq.AREANAME,
                    ASSETNO = dataReq.ASSETNO,
                    FINDY = objProblem.FINDY,
                    PCODE = objProblem.Pcode,
                    PNAME = objProblem.Pname,
                    UCODE = dataReq.UCODE,


                };

                var req1 = new ASSETKKF_MODEL.Request.Asset.AuditPostReq()
                {
                    SQNO = dataReq.SQNO,
                    DEPCODEOL = dataReq.DEPCODEOL,
                    COMPANY = dataReq.COMPANY,
                    LEADERCODE = dataReq.LEADERCODE,
                    AREACODE = dataReq.AREACODE,
                    UCODE = dataReq.UCODE
                };

                reqPostMst.MODE = "EDIT";
                ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().updateAUDITPOSTMST(reqPostMst);

                res.AUDITPOSTMSTWAITLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req1, "");
                res.AUDITPOSTMSTCHECKEDLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req1, "Y");
                res.AUDITPOSTTRNLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTTRN(req1);

                res._result._code = "200";
                res._result._message = "";
                res._result._status = "OK";

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
