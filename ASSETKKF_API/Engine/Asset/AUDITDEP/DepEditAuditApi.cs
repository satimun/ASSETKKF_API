using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_ADO.Mssql.Audit;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Audit;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Engine.Asset.AUDITDEP
{
    public class DepEditAuditApi : Base<AuditPostReq>
    {
        public DepEditAuditApi(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }

        protected override void ExecuteChild(AuditPostReq dataReq, ResponseAPI dataRes)
        {
            var res = new AuditDepRes();
            try
            {
                DBMode = dataReq.DBMode;
                res._result.ServerAddr = ConnectionString();
                res._result.DBMode = DBMode;

                var mode = !String.IsNullOrEmpty(dataReq.mode) ? dataReq.mode.Trim().ToLower() : dataReq.mode;
                switch (mode)
                {
                    case "getaudittoclear":
                        res = getaudittoclear(dataReq, res,conString);
                        break;

                    case "getaudittoconfirm":
                        res = getaudittoconfirm(dataReq, res,conString);
                        break;

                    case "getauditassetno":
                        res = getauditassetno(dataReq, res,conString);
                        break;

                    case "confirminformedmst":
                        res = confirminformedmst(dataReq, res,conString);
                        break;

                    case "confirminformedtrn":
                        res = confirminformedtrn(dataReq, res,conString);
                        break;

                    case "depeditmst":
                        res = depeditmst(dataReq, res,conString);
                        break;

                    case "depedittrn":
                        res = depedittrn(dataReq, res,conString);
                        break;

                    case "depimgmst":
                        res = depimgmst(dataReq, res,conString);
                        break;

                    case "depimgtrn":
                        res = depimgtrn(dataReq, res,conString);
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

        private AuditDepRes getaudittoclear(AuditPostReq dataReq, AuditDepRes res, string conStr = null)
        {
            try
            {
                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getDataToClear(dataReq,null,null,conStr);
                res.AUDITPOSTMSTTODEPLST = lst;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNDep(dataReq,null,null,conStr);
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

        private AuditDepRes getaudittoconfirm(AuditPostReq dataReq, AuditDepRes res, string conStr = null)
        {
            try
            {
                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getDataToConfirm(dataReq,null,null,conStr);
                res.AUDITPOSTMSTTODEPLST = lst;

                res._result._code = "200";
                res._result._message = "";
                res._result._status = "OK";
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

        private AuditDepRes getauditassetno(AuditPostReq dataReq, AuditDepRes res, string conStr = null)
        {
            try
            {
                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getAuditAssetNo(dataReq,null,conStr);
                res.AUDITPOSTMSTTODEP = lst != null ? lst.FirstOrDefault() : null;

                res._result._code = "200";
                res._result._message = "";
                res._result._status = "OK";
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

        private AuditDepRes confirminformedmst(AuditPostReq dataReq, AuditDepRes res, string conStr = null)
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
                    MODE = "updateDEP_STY"

                };
                var updateSTCY = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().SP_AUDITPOSTMSTTODEPPHONE(req,null,null,conStr);

                res._result._code = "200";
                res._result._message = "";
                res._result._status = "OK";
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

        private AuditDepRes confirminformedtrn(AuditPostReq dataReq, AuditDepRes res, string conStr = null)
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
                    MODE = "updateTRN_STY"

                };

                var updateSNDST = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().SP_AUDITPOSTTRNPHONE(req,null,null,conStr);


                res._result._code = "200";
                res._result._message = "";
                res._result._status = "OK";
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

        private AuditDepRes depeditmst(AuditPostReq dataReq, AuditDepRes res, string conStr = null)
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
                    FLAG = "",
                    MEMO1 = dataReq.MEMO1,
                    MODE = "updateDEP_STY"

                };
                var updateSTY = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().SP_AUDITPOSTMSTTODEPPHONE(req,null,null,conStr);

                res._result._code = "200";
                res._result._message = "";
                res._result._status = "OK";
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
                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getDataToClear(req,null,null,conStr);
                res.AUDITPOSTMSTTODEPLST = lst;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNDep(req,null,null,conStr);
                res.POSTTRNDuplicateLST = lstPostTRN;
            }


            return res;
        }

        private AuditDepRes depedittrn(AuditPostReq dataReq, AuditDepRes res, string conStr = null)
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
                    FLAG = "",
                    MEMO1 = dataReq.MEMO1,
                    MODE = "updateTRN_STY"

                };

                var updateSTY = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().SP_AUDITPOSTTRNPHONE(req,null,null,conStr);


                res._result._code = "200";
                res._result._message = "";
                res._result._status = "OK";
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
                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getDataToClear(req,null,null,conStr);
                res.AUDITPOSTMSTTODEPLST = lst;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNDep(req,null,null,conStr);
                res.POSTTRNDuplicateLST = lstPostTRN;
            }


            return res;
        }

        private AuditDepRes depimgmst(AuditPostReq dataReq, AuditDepRes res, string conStr = null)
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
                    FLAG = "",
                    IMGPATH = dataReq.IMGPATH,
                    MODE = "update_IMG"

                };
                var updateSTY = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().SP_AUDITPOSTMSTTODEPPHONE(req,null,null,conStr);

                res._result._code = "200";
                res._result._message = "";
                res._result._status = "OK";
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
                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getDataToClear(req,null,null,conStr);
                res.AUDITPOSTMSTTODEPLST = lst;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNDep(req,null,null,conStr);
                res.POSTTRNDuplicateLST = lstPostTRN;
            }


            return res;
        }

        private AuditDepRes depimgtrn(AuditPostReq dataReq, AuditDepRes res, string conStr = null)
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
                    FLAG = "",
                    IMGPATH = dataReq.IMGPATH,
                    MODE = "update_IMG"

                };

                var updateSTY = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().SP_AUDITPOSTTRNPHONE(req,null,null,conStr);


                res._result._code = "200";
                res._result._message = "";
                res._result._status = "OK";
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
                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getDataToClear(req,null,null,conStr);
                res.AUDITPOSTMSTTODEPLST = lst;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNDep(req,null,null,conStr);
                res.POSTTRNDuplicateLST = lstPostTRN;
            }


            return res;
        }

    }
}
