using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ASSETKKF_ADO.Mssql.Audit;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Request.Audit;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Audit;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Engine.Asset.AUDITMANAGER
{
    public class AuditManagerAppvApi : Base<AuditPostReq>
    {
        public AuditManagerAppvApi(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }

        protected override void ExecuteChild(AuditPostReq dataReq, ResponseAPI dataRes)
        {
            var res = new AuditManagerRes();
            try
            {
                DBMode = dataReq.DBMode;
                res._result.ServerAddr = ConnectionString();
                res._result.DBMode = DBMode;

                var mode = !String.IsNullOrEmpty(dataReq.mode) ? dataReq.mode.Trim().ToLower() : dataReq.mode;
                switch (mode)
                {
                    case "save":
                        res = save(dataReq, res,conString);
                        break;

                    default:
                        res = getAuditResult(dataReq, res,conString);
                        break;
                }

            }
            catch (SqlException ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Execute exception Error";
            }
            catch (InvalidOperationException ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Connection Exception Error";
            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }
            finally
            {
                if (res != null)
                {
                    if (res.AUDITPOSTMSTTODEPLST != null && res.AUDITPOSTMSTTODEPLST.Count > 0)
                    {
                        var obj = res.AUDITPOSTMSTTODEPLST.Where(x => !String.IsNullOrEmpty(x.FILEPATH)).FirstOrDefault();
                        var attachedFile = obj != null ? obj.FILEPATH : null;
                        res.FILEPATH = attachedFile;
                    }
                }
            }
            dataRes.data = res;
        }

        private AuditManagerRes save(AuditPostReq dataReq, AuditManagerRes res, string conStr = null)
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
                var updateCutMST = AuditManagerAdo.GetInstant().saveAUDITCUTDATEMST(req,null,conStr);
            }
            catch (SqlException ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Execute exception Error";
            }
            catch (InvalidOperationException ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Connection Exception Error";
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

                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getDataToCompEdit(req,null,null,conStr);
                res.AUDITPOSTMSTTODEPLST = lst;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNComp(req,null,null,conStr);
                res.POSTTRNDuplicateLST = lstPostTRN;


                var summaryAudit = AuditManagerAdo.GetInstant().GetSummaryAudit(dataReq,null,conStr);
                res.AuditSummary = summaryAudit;

                var lstResult = AuditManagerAdo.GetInstant().GetSummaryResult(dataReq,null,conStr);
                res.SummaryResultLst = lstResult;
            }


            return res;

        }

        private AuditManagerRes getAuditResult(AuditPostReq dataReq, AuditManagerRes res, string conStr = null)
        {
            try
            {
                AuditPostReq req = new AuditPostReq()
                {
                    SQNO = dataReq.SQNO,
                    COMPANY = dataReq.COMPANY,
                    filter = dataReq.filter
                };

                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getDataToCompEdit(req,null,null,conStr);
                res.AUDITPOSTMSTTODEPLST = lst;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNComp(req,null,null,conStr);
                res.POSTTRNDuplicateLST = lstPostTRN;


                var summaryAudit = AuditManagerAdo.GetInstant().GetSummaryAudit(dataReq,null,conStr);
                res.AuditSummary = summaryAudit;

                var lstResult = AuditManagerAdo.GetInstant().GetSummaryResult(dataReq,null,conStr);
                res.SummaryResultLst = lstResult;
            }
            catch (SqlException ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Execute exception Error";
            }
            catch (InvalidOperationException ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Connection Exception Error";
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
