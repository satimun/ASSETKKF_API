using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ASSETKKF_ADO.Mssql.Audit;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Audit;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Engine.Asset.AUDITACC
{
     public class AuditAccSendApi : Base<AuditPostReq>
    {
        public AuditAccSendApi(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }

        protected override void ExecuteChild(AuditPostReq dataReq, ResponseAPI dataRes)
        {
            var res = new AuditAccRes();
            try
            {
                DBMode = dataReq.DBMode;
                res._result.ServerAddr = ConnectionString();
                res._result.DBMode = DBMode;

                var mode = !String.IsNullOrEmpty(dataReq.mode) ? dataReq.mode.Trim().ToLower() : dataReq.mode;
                switch (mode)
                {
                    case "sendmst":
                        res = sendMst(dataReq, res,conString);
                        break;
                    case "sendtrn":
                        res = sendTrn(dataReq, res,conString);
                        break;

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
            dataRes.data = res;
        }

        private AuditAccRes sendMst(AuditPostReq dataReq, AuditAccRes res, string conStr = null)
        {
            try
            {
                AUDITPOSTMSTReq req = new AUDITPOSTMSTReq()
                {
                    SQNO = dataReq.SQNO,
                    COMPANY = dataReq.COMPANY,
                    ASSETNO = dataReq.ASSETNO,
                    INPID = dataReq.INPID,
                    UCODE = dataReq.UCODE,
                    FLAG = dataReq.FLAG,
                    MODE = "updateDep_SNDACC"

                };
                //var updateDep = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().SP_AUDITPOSTMSTTODEPPHONE(req);


                var task1 = System.Threading.Tasks.Task.Factory.StartNew(() => ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().SP_AUDITPOSTMSTTODEPPHONE(req,null,null,conStr));
                task1.Wait();
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

        private AuditAccRes sendTrn(AuditPostReq dataReq, AuditAccRes res, string conStr = null)
        {
            try
            {
                AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
                {
                    SQNO = dataReq.SQNO,
                    COMPANY = dataReq.COMPANY,
                    ASSETNO = dataReq.ASSETNO,
                    INPID = dataReq.INPID,
                    UCODE = dataReq.UCODE,
                    FLAG = dataReq.FLAG,
                    MODE = "updateTRN_SNDACC"

                };

                //var updateTrn = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().SP_AUDITPOSTTRNPHONE(req1);
                var task2 = System.Threading.Tasks.Task.Factory.StartNew(() => ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().SP_AUDITPOSTMSTTODEPPHONE(req1,null,null,conStr));
                task2.Wait();
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

        private AuditAccRes save(AuditPostReq dataReq, AuditAccRes res, string conStr = null)
        {
            try
            {
                AUDITPOSTMSTReq req = new AUDITPOSTMSTReq()
                {
                    SQNO = dataReq.SQNO,
                    COMPANY = dataReq.COMPANY,
                    ASSETNO = dataReq.ASSETNO,
                    INPID = dataReq.INPID,
                    UCODE = dataReq.UCODE,
                    FLAG = dataReq.FLAG,
                    MODE = "updateDep_SNDACCDT"

                };
                //var updateDep = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().SP_AUDITPOSTMSTTODEPPHONE(req);


                var task1 = System.Threading.Tasks.Task.Factory.StartNew(() => ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().SP_AUDITPOSTMSTTODEPPHONE(req,null,null,conStr));
                task1.Wait();

                AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
                {
                    SQNO = dataReq.SQNO,
                    COMPANY = dataReq.COMPANY,
                    ASSETNO = dataReq.ASSETNO,
                    INPID = dataReq.INPID,
                    UCODE = dataReq.UCODE,
                    FLAG = dataReq.FLAG,
                    MODE = "updateTRN_SNDACCDT"

                };

                var task2 = System.Threading.Tasks.Task.Factory.StartNew(() => ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().SP_AUDITPOSTMSTTODEPPHONE(req1,null,null,conStr));
                task2.Wait();
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
                AuditPostReq req = new AuditPostReq()
                {
                    SQNO = dataReq.SQNO,
                    COMPANY = dataReq.COMPANY,
                    filter = dataReq.filter
                };

                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getDataToACCEdit(req,null,null,conStr);
                res.AUDITPOSTMSTTODEPLST = lst;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNACC(req,null,null,conStr);
                res.POSTTRNDuplicateLST = lstPostTRN;


                var summaryAudit = AuditAccAdo.GetInstant().GetSummaryAudit(dataReq,null,conStr);
                res.AuditSummary = summaryAudit;

                var lstResult = AuditAccAdo.GetInstant().GetSummaryResult(dataReq,null,conStr);
                res.SummaryResultLst = lstResult;
            }


            return res;
        }

        private AuditAccRes getAuditResult(AuditPostReq dataReq, AuditAccRes res, string conStr = null)
        {
            try
            {
                AuditPostReq req = new AuditPostReq()
                {
                    SQNO = dataReq.SQNO,
                    COMPANY = dataReq.COMPANY,
                    filter = dataReq.filter
                };

                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getDataToACCEdit(req,null,null,conStr);
                res.AUDITPOSTMSTTODEPLST = lst;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNACC(req,null,null,conStr);
                res.POSTTRNDuplicateLST = lstPostTRN;


                var summaryAudit = AuditAccAdo.GetInstant().GetSummaryAudit(dataReq,null,conStr);
                res.AuditSummary = summaryAudit;

                var lstResult = AuditAccAdo.GetInstant().GetSummaryResult(dataReq,null,conStr);
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
