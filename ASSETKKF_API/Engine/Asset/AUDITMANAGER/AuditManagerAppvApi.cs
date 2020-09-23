using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ASSETKKF_ADO.Mssql.Audit;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Request.Audit;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Audit;

namespace ASSETKKF_API.Engine.Asset.AUDITMANAGER
{
    public class AuditManagerAppvApi : Base<AuditPostReq>
    {
        public AuditManagerAppvApi()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(AuditPostReq dataReq, ResponseAPI dataRes)
        {
            var res = new AuditManagerRes();
            try
            {
                var mode = !String.IsNullOrEmpty(dataReq.mode) ? dataReq.mode.Trim().ToLower() : dataReq.mode;
                switch (mode)
                {
                    case "save":
                        res = save(dataReq, res);
                        break;

                    default:
                        res = getAuditResult(dataReq, res);
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

        private AuditManagerRes save(AuditPostReq dataReq, AuditManagerRes res)
        {
            try
            {
                AuditPostReq req = new AuditPostReq()
                {
                    SQNO = dataReq.SQNO,
                    COMPANY = dataReq.COMPANY,
                    UCODE = dataReq.UCODE,
                    FLAG = dataReq.FLAG,
                    mode = dataReq.ACTION

                };
                var updateCutMST = AuditManagerAdo.GetInstant().saveAUDITCUTDATEMST(req);
            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }
            finally
            {
                Thread.Sleep(1000);
                AuditPostReq req = new AuditPostReq()
                {
                    SQNO = dataReq.SQNO,
                    COMPANY = dataReq.COMPANY,
                    filter = dataReq.filter
                };

                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getDataToCompEdit(req);
                res.AUDITPOSTMSTTODEPLST = lst;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNComp(req);
                res.POSTTRNDuplicateLST = lstPostTRN;


                var summaryAudit = AuditManagerAdo.GetInstant().GetSummaryAudit(dataReq);
                res.AuditSummary = summaryAudit;

                var lstResult = AuditManagerAdo.GetInstant().GetSummaryResult(dataReq);
                res.SummaryResultLst = lstResult;
            }


            return res;

        }

        private AuditManagerRes getAuditResult(AuditPostReq dataReq, AuditManagerRes res)
        {
            try
            {
                AuditPostReq req = new AuditPostReq()
                {
                    SQNO = dataReq.SQNO,
                    COMPANY = dataReq.COMPANY,
                    filter = dataReq.filter
                };

                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getDataToCompEdit(req);
                res.AUDITPOSTMSTTODEPLST = lst;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNComp(req);
                res.POSTTRNDuplicateLST = lstPostTRN;


                var summaryAudit = AuditManagerAdo.GetInstant().GetSummaryAudit(dataReq);
                res.AuditSummary = summaryAudit;

                var lstResult = AuditManagerAdo.GetInstant().GetSummaryResult(dataReq);
                res.SummaryResultLst = lstResult;
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
