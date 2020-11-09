using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Audit;
using ASSETKKF_API.Service;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace ASSETKKF_API.Engine.Asset.AUDITPOST
{
    public class AuditDuplicateApi : Base<AuditPostReq>
    {
        private FilesUtil FilesUtilSvc;
        public AuditDuplicateApi(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
            FilesUtilSvc = new FilesUtil(Configuration);
        }

        protected override void ExecuteChild(AuditPostReq dataReq, ResponseAPI dataRes)
        {
            var res = new AuditDuplicateRes();

            try
            {
                DBMode = dataReq.DBMode;
                res._result.ServerAddr = ConnectionString();
                res._result.DBMode = DBMode;

                var mode = !String.IsNullOrEmpty(dataReq.mode) ? dataReq.mode.Trim().ToLower() : dataReq.mode;
                switch (mode)
                {
                    case "getduplicate":
                        res = getDuplicate(dataReq,res,conString);
                        break;
                    case "confirmduplicatemst":
                        res = confirmDuplicateMST(dataReq, res,conString);
                        break;
                    case "confirmduplicatetrn":
                        res = confirmDuplicateTRN(dataReq, res,conString);
                        break;
                    case "savemst2tmp":
                        res = savemst2tmp(dataReq, res,conString);
                        break;

                    case "getdata2dep":
                        res = getdata2dep(dataReq, res,conString);
                        break;

                    case "getauditassetno":
                        res = getauditassetno(dataReq, res,conString);
                        break;

                    case "updateaudittotemp":
                        res = updateaudittotemp(dataReq, res,conString);
                        break;

                    case "savebfsend":
                        res = savebfsend(dataReq, res,conString);
                        break;

                    case "confirmduplicatetotemp":
                        res = confirmDuplicateTOTEMP(dataReq, res,conString);
                        break;

                    case "sendaudit2dep":
                        res = sendaudit2dep(dataReq, res,conString);
                        break;

                    case "uploadfile":
                        res = uploadfile(dataReq, res,conString);
                        break;

                    case "auditcomparetotmp":
                        res = auditcomparetotmp(dataReq, res, conString);
                        break;

                    case "confirmtrntotmp":
                        res = confirmtrntotmp(dataReq, res, conString);
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
                if(res != null)
                {
                    if (res.AuditToTEMPLST != null && res.AuditToTEMPLST.Count > 0)
                    {
                        var obj = res.AuditToTEMPLST.Where(x => !String.IsNullOrEmpty(x.FILEPATH)).FirstOrDefault();
                        var attachedFile = obj != null ? obj.FILEPATH : null;
                        res.FILEPATH = attachedFile;
                    }
                }
            }
            dataRes.data = res;
        }

        private AuditDuplicateRes confirmtrntotmp(AuditPostReq dataReq, AuditDuplicateRes res, string conStr = null)
        {
            try
            {
                //Save to AuditTMP
                AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
                {
                    MODE = "update",
                    SQNO = dataReq.SQNO,
                    COMPANY = dataReq.COMPANY,
                    ASSETNO = dataReq.ASSETNO,
                    UCODE = dataReq.UCODE,
                    FINDY = dataReq.FINDY,
                    PCODE = dataReq.PCODE,
                    PNAME = dataReq.PNAME,
                    MEMO1 = dataReq.MEMO1,
                };
                ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req1, null, null, conStr);


                //Update to AuditTRN
                AUDITPOSTMSTReq req = new AUDITPOSTMSTReq()
                {
                    SQNO = dataReq.SQNO_TRN,
                    COMPANY = dataReq.COMPANY_TRN,
                    ASSETNO = dataReq.ASSETNO,
                    INPID = dataReq.INPID_TRN,
                    MEMO1 = dataReq.MEMO_TRN,
                    UCODE = dataReq.UCODE,
                    MODE = "updateTRN_TMP"

                };

                var updateSNDST = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().SP_AUDITPOSTTRNPHONE(req, null, null, conStr);

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

        private AuditDuplicateRes auditcomparetotmp(AuditPostReq dataReq, AuditDuplicateRes res, string conStr = null)
        {
            try
            {
               var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().getAuditTmpComparetoTRN(dataReq,  null, conStr);
                res.AuditTmpCompareTRNLST = lst;

                if(lst != null && lst.Count > 0)
                {
                    res._result._code = "200";
                    res._result._message = "";
                    res._result._status = "OK";
                }
                else
                {
                    res._result._code = "404";
                    res._result._message = "";
                    res._result._status = "NOT FOUND";
                }
            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }


            return res;
        }

        private AuditDuplicateRes getDuplicate(AuditPostReq dataReq, AuditDuplicateRes res, string conStr = null)
        {
            try
            {              

                var lstPostMST = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTAdo.GetInstant().getPOSTMSTDuplicate(dataReq,null,null,conStr);
                res.POSTMSTDuplicateLST = lstPostMST;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNDuplicate(dataReq,null,null,conStr);
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

        private AuditDuplicateRes confirmDuplicateMST(AuditPostReq dataReq, AuditDuplicateRes res, string conStr = null)
        {
            try
            {
                AUDITPOSTMSTReq req = new AUDITPOSTMSTReq()
                {
                    SQNO = dataReq.SQNO,
                    COMPANY = dataReq.COMPANY,
                    ASSETNO = dataReq.ASSETNO,
                    UCODE = dataReq.UCODE,
                    MODE = "updateSNDST_Y_ASSETNO_INPID"

                };
                var updateSNDST = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTAdo.GetInstant().saveAUDITPOSTMST(req,null,conStr);

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

        private AuditDuplicateRes confirmDuplicateTRN(AuditPostReq dataReq, AuditDuplicateRes res, string conStr = null)
        {
            try
            {
                AUDITPOSTTRNReq req = new AUDITPOSTTRNReq()
                {
                    SQNO = dataReq.SQNO,
                    COMPANY = dataReq.COMPANY,
                    ASSETNO = dataReq.ASSETNO,
                    UCODE = dataReq.UCODE,
                    INPID = dataReq.INPID,
                    MODE = "updateSNDST_Y_ASSETNO_INPID"
                };

                var updateSNDST = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().saveAUDITPOSTTRN(req,null,conStr);


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

        private AuditDuplicateRes confirmDuplicateTOTEMP(AuditPostReq dataReq, AuditDuplicateRes res, string conStr = null)
        {
            try
            {
                AUDITPOSTMSTReq req = new AUDITPOSTMSTReq()
                {
                    SQNO = dataReq.SQNO,
                    COMPANY = dataReq.COMPANY,
                    ASSETNO = dataReq.ASSETNO,
                    UCODE = dataReq.UCODE,
                    MODE = "updateSNDST_Y_ASSETNO_INPID"

                };
                var updateSNDST = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req,null,null,conStr);

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

        private AuditDuplicateRes savemst2tmp(AuditPostReq dataReq, AuditDuplicateRes res, string conStr = null)
        {
            try
            {
                int action = 0;
                var lstNoDuplicate = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTAdo.GetInstant().getNoDuplicateAll(dataReq,null,null,conStr);
                var lstDuplicate = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTAdo.GetInstant().getDuplicateAll(dataReq,null,null,conStr);

                if(lstNoDuplicate != null && lstNoDuplicate.Count > 0)
                {
                    action += 1;
                    lstNoDuplicate.ForEach(c => {
                        AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
                        {
                            MODE = "deleteByAssetno",
                            SQNO = c.SQNO,
                            COMPANY = c.COMPANY,
                            ASSETNO = c.ASSETNO,
                            UCODE = c.INPID,
                            FINDY = c.FINDY,
                            PCODE = c.PCODE,
                            PNAME = c.PNAME

                        };
                        ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req1,null,null,conStr);

                        var req2 = req1;
                        req2.MODE = "addByAssetnoInpid";
                        ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req2,null,null,conStr);
                    });
                }

                if (lstDuplicate != null && lstDuplicate.Count > 0)
                {
                    action += 1;
                    lstDuplicate.ForEach(c => {
                        AUDITPOSTMSTReq req3 = new AUDITPOSTMSTReq()
                        {
                            MODE = "deleteByAssetno",
                            SQNO = c.SQNO,
                            COMPANY = c.COMPANY,
                            ASSETNO = c.ASSETNO,
                            UCODE = c.INPID,
                            FINDY = c.FINDY,
                            PCODE = c.PCODE,
                            PNAME = c.PNAME

                        };
                        ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req3,null,null,conStr);

                        var req4 = req3;
                        req4.MODE = "addByAssetnoInpid";
                        ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req4,null,null,conStr);
                    });
                }

                if(action > 0)
                {
                    AUDITPOSTMSTReq req5 = new AUDITPOSTMSTReq()
                    {
                        MODE = "updateSNDST",
                        SQNO = dataReq.SQNO,
                        COMPANY = dataReq.COMPANY,
                        UCODE = dataReq.UCODE

                    };
                    ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req5,null,null,conStr);
                }

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
            finally
            {
                var lstPostMST = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTAdo.GetInstant().getPOSTMSTDuplicate(dataReq,null,null,conStr);
                res.POSTMSTDuplicateLST = lstPostMST;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNDuplicate(dataReq,null,null,conStr);
                res.POSTTRNDuplicateLST = lstPostTRN;
            }


            return res;
        }

        private AuditDuplicateRes getdata2dep(AuditPostReq dataReq, AuditDuplicateRes res, string conStr = null)
        {
            try
            {
                updateCUTDT(dataReq,conStr);

                updatePOSTMST_SNDST_Y(dataReq,conStr);
                addPOSTTEMP_NotExists_PCODE_SNDST_Y(dataReq,conStr);
                addPOSTTEMP_NotExists_PCODE(dataReq,conStr);
                addPOSTTEMP_NotExists(dataReq,conStr);
                updatePOSTTEMP_SNDST_Y(dataReq,conStr);
                updatePOSTTEMP_PCODE_PostMST(dataReq,conStr);
                updatePOSTDEP_PCODE_PostMST(dataReq,conStr);
                updatePOSTTEMP_PCODE_PostMST_Dup(dataReq,conStr);
                updatePOSTDEP_PCODE_PostMST_Dup(dataReq,conStr);

                var lstPostMSTToTEMP = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().getDataToSendDep(dataReq,null,null,conString);
                if (lstPostMSTToTEMP == null || lstPostMSTToTEMP != null && lstPostMSTToTEMP.Count == 0)
                {
                    InSdataToTEMP(dataReq);
                }

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
            finally
            {
                var lstPostMSTToTEMP = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().getDataToSendDep(dataReq,null,null, conStr);
                res.AuditToTEMPLST = lstPostMSTToTEMP;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRN(dataReq,null,null,conStr);
                res.POSTTRNDuplicateLST = lstPostTRN;

                var lstNoAudit = ASSETKKF_ADO.Mssql.Audit.AUDITCUTDATEAdo.GetInstant().getNoAudit(dataReq,null,null,conStr);
                //var lstNoAudit = lstPostMSTToTEMP.Where(p => String.IsNullOrEmpty(p.PCODE)).ToList();
                res.NoAuditLST = lstNoAudit;
            }
            return res;
        }

        private void updateCUTDT(AuditPostReq dataReq, string conStr = null)
        {
            AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
            {
                MODE = "updateCUTDT",
                SQNO = dataReq.SQNO,
                COMPANY = dataReq.COMPANY,
                UCODE = dataReq.UCODE

            };
            ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req1,null,null,conStr);
        }

        private void updatePOSTMST_SNDST_Y(AuditPostReq dataReq, string conStr = null)
        {
            AUDITPOSTMSTReq req = new AUDITPOSTMSTReq()
            {
                SQNO = dataReq.SQNO,
                COMPANY = dataReq.COMPANY,
                ASSETNO = dataReq.ASSETNO,
                UCODE = dataReq.UCODE,
                MODE = "updateSNDST_Y"

            };
            var updateSNDST = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTAdo.GetInstant().saveAUDITPOSTMST(req,null,conStr);
        }

        private void addPOSTTEMP_NotExists_PCODE_SNDST_Y(AuditPostReq dataReq, string conStr = null)
        {
            AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
            {
                MODE = "addNotExists_PCODE_SNDST_Y",
                SQNO = dataReq.SQNO,
                COMPANY = dataReq.COMPANY,
                UCODE = dataReq.UCODE

            };
            ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req1,null,null,conStr);
        }

        private void addPOSTTEMP_NotExists_PCODE(AuditPostReq dataReq, string conStr = null)
        {
            AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
            {
                MODE = "addNotExists_PCODE",
                SQNO = dataReq.SQNO,
                COMPANY = dataReq.COMPANY,
                UCODE = dataReq.UCODE

            };
            ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req1,null,null,conStr);
        }

        private void addPOSTTEMP_NotExists(AuditPostReq dataReq, string conStr = null)
        {
            AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
            {
                MODE = "addNotExists",
                SQNO = dataReq.SQNO,
                COMPANY = dataReq.COMPANY,
                UCODE = dataReq.UCODE

            };
            ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req1,null,null,conStr);
        }

        private void updatePOSTTEMP_SNDST_Y(AuditPostReq dataReq, string conStr = null)
        {
            AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
            {
                MODE = "updateSNDST_Y",
                SQNO = dataReq.SQNO,
                COMPANY = dataReq.COMPANY,
                UCODE = dataReq.UCODE

            };
            ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req1,null,null,conStr);
        }

        private void updatePOSTTEMP_PCODE_PostMST(AuditPostReq dataReq, string conStr = null)
        {
            AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
            {
                MODE = "updatePCODE_PostMST",
                SQNO = dataReq.SQNO,
                COMPANY = dataReq.COMPANY,
                UCODE = dataReq.UCODE

            };
            ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req1,null,null,conStr);
        }

        private void updatePOSTDEP_PCODE_PostMST(AuditPostReq dataReq, string conStr = null)
        {
            AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
            {
                MODE = "updatePCODE_PostMST",
                SQNO = dataReq.SQNO,
                COMPANY = dataReq.COMPANY,
                UCODE = dataReq.UCODE

            };
            ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().SP_AUDITPOSTMSTTODEP(req1,null,null,conStr);
        }

        private void updatePOSTTEMP_PCODE_PostMST_Dup(AuditPostReq dataReq, string conStr = null)
        {
            AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
            {
                MODE = "updatePCODE_PostMST_Dup",
                SQNO = dataReq.SQNO,
                COMPANY = dataReq.COMPANY,
                UCODE = dataReq.UCODE

            };
            ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req1,null,null,conStr);
        }

        private void updatePOSTDEP_PCODE_PostMST_Dup(AuditPostReq dataReq, string conStr = null)
        {
            AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
            {
                MODE = "updatePCODE_PostMST_Dup",
                SQNO = dataReq.SQNO,
                COMPANY = dataReq.COMPANY,
                UCODE = dataReq.UCODE

            };
            ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().SP_AUDITPOSTMSTTODEP(req1,null,null,conStr);
        }

        private void InSdataToTEMP(AuditPostReq dataReq, string conStr = null)
        {
            AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
            {
                MODE = "add_PostMST_NoDup",
                SQNO = dataReq.SQNO,
                COMPANY = dataReq.COMPANY,
                UCODE = dataReq.UCODE

            };
            ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req1,null,null,conString);

            var req2 = req1;
            req2.MODE = "add_PostMST_Dup";
            ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req2,null,null,conString);
        }


        private AuditDuplicateRes getauditassetno(AuditPostReq dataReq, AuditDuplicateRes res, string conStr = null)
        {
            try
            {
                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().getAuditAssetNo(dataReq,null,conStr);
                res.AUDITPOSTMSTTOTEMP = lst != null ? lst.FirstOrDefault() : null;

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
        
            private AuditDuplicateRes updateaudittotemp(AuditPostReq dataReq, AuditDuplicateRes res, string conStr = null)
            {
            try
            {
                AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
                {
                    MODE = "update",
                    SQNO = dataReq.SQNO,
                    COMPANY = dataReq.COMPANY,
                    ASSETNO = dataReq.ASSETNO,
                    UCODE = dataReq.UCODE,
                    FINDY = dataReq.FINDY,
                    PCODE = dataReq.PCODE,
                    PNAME = dataReq.PNAME,
                    MEMO1 = dataReq.MEMO1,
                    poth = dataReq.poth,
                    snnstdt = dataReq.snnstdt,
                    expstdt = dataReq.expstdt


                };
                ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req1,null,null,conStr);

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
            finally
            {
                var lstPostMSTToTEMP = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().getDataToSendDep(dataReq,null,null,conStr);
                res.AuditToTEMPLST = lstPostMSTToTEMP;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRN(dataReq,null,null,conStr);
                res.POSTTRNDuplicateLST = lstPostTRN;

                var lstNoAudit = ASSETKKF_ADO.Mssql.Audit.AUDITCUTDATEAdo.GetInstant().getNoAudit(dataReq,null,null,conStr);
                //var lstNoAudit = lstPostMSTToTEMP.Where(p => String.IsNullOrEmpty(p.PCODE)).ToList();
                res.NoAuditLST = lstNoAudit;
            }
            return res;
        }

        private AuditDuplicateRes savebfsend(AuditPostReq dataReq, AuditDuplicateRes res, string conStr = null)
        {
            try
            {
                AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
                {
                    MODE = "update",
                    SQNO = dataReq.SQNO,
                    COMPANY = dataReq.COMPANY,
                    ASSETNO = dataReq.ASSETNO,
                    UCODE = dataReq.UCODE,
                    FINDY = dataReq.FINDY,
                    PCODE = dataReq.PCODE,
                    PNAME = dataReq.PNAME,
                    MEMO1 = dataReq.MEMO1,
                    poth = dataReq.poth,
                    snnstdt = dataReq.snnstdt,
                    expstdt = dataReq.expstdt


                };
                ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req1,null,null,conStr);

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
            finally
            {
                var lstPostMSTToTEMP = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().getDataToSendDep(dataReq,null,null,conStr);
                res.AuditToTEMPLST = lstPostMSTToTEMP;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRN(dataReq,null,null,conStr);
                res.POSTTRNDuplicateLST = lstPostTRN;

                var lstNoAudit = ASSETKKF_ADO.Mssql.Audit.AUDITCUTDATEAdo.GetInstant().getNoAudit(dataReq,null,null,conStr);
                //var lstNoAudit = lstPostMSTToTEMP.Where(p => String.IsNullOrEmpty(p.PCODE)).ToList();
                res.NoAuditLST = lstNoAudit;
            }
            return res;
        }

        private AuditDuplicateRes sendaudit2dep(AuditPostReq dataReq, AuditDuplicateRes res, string conStr = null)
        {
            try
            {
                updateCUTDT(dataReq,conStr);

                updatePOSTMST_SNDST_Y(dataReq,conStr);
                addPOSTTEMP_NotExists_PCODE_SNDST_Y(dataReq,conStr);
                addPOSTTEMP_NotExists_PCODE(dataReq,conStr);
                addPOSTTEMP_NotExists(dataReq,conStr);
                updatePOSTTEMP_SNDST_Y(dataReq,conStr);
                updatePOSTTEMP_PCODE_PostMST(dataReq,conStr);
                updatePOSTDEP_PCODE_PostMST(dataReq,conStr);
                updatePOSTTEMP_PCODE_PostMST_Dup(dataReq,conStr);
                updatePOSTDEP_PCODE_PostMST_Dup(dataReq,conStr);

                var lstPostMSTToTEMP = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().getDataToSendDep(dataReq,null,null,conStr);
                if (lstPostMSTToTEMP == null || lstPostMSTToTEMP != null && lstPostMSTToTEMP.Count == 0)
                {
                    InSdataToTEMP(dataReq,conStr);
                }

                AUDITPOSTTRNReq req = new AUDITPOSTTRNReq()
                {
                    SQNO = dataReq.SQNO,
                    COMPANY = dataReq.COMPANY,
                    ASSETNO = dataReq.ASSETNO,
                    UCODE = dataReq.UCODE,
                    INPID = dataReq.INPID,
                    MODE = "update_senddep"
                };

                var updateSNDST = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().saveAUDITPOSTTRN(req,null,conStr);

                AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
                {
                    MODE = "insert_PostTEMP",
                    SQNO = dataReq.SQNO,
                    COMPANY = dataReq.COMPANY,
                    ASSETNO = dataReq.ASSETNO,
                    UCODE = dataReq.UCODE,
                    FINDY = dataReq.FINDY,
                    PCODE = dataReq.PCODE,
                    PNAME = dataReq.PNAME,
                    MEMO1 = dataReq.MEMO1,
                    poth = dataReq.poth,
                    snnstdt = dataReq.snnstdt,
                    expstdt = dataReq.expstdt


                };
                ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().SP_AUDITPOSTMSTTODEP(req1,null,null,conStr);

                

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
            finally
            {
                var lstPostMSTToTEMP = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().getDataToSendDep(dataReq,null,null,conStr);
                res.AuditToTEMPLST = lstPostMSTToTEMP;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRN(dataReq,null,null,conStr);
                res.POSTTRNDuplicateLST = lstPostTRN;

                var lstNoAudit = ASSETKKF_ADO.Mssql.Audit.AUDITCUTDATEAdo.GetInstant().getNoAudit(dataReq,null,null,conStr);
                //var lstNoAudit = lstPostMSTToTEMP.Where(p => String.IsNullOrEmpty(p.PCODE)).ToList();
                res.NoAuditLST = lstNoAudit;
            }
            return res;
        }

        private AuditDuplicateRes uploadfile(AuditPostReq dataReq, AuditDuplicateRes res, string conStr = null)
        {
            try
            {
                if (dataReq.FileToUpload != null)
                {
                    res.FILEPATH = FilesUtilSvc.uploadAttachFile(dataReq.FileToUpload);
                    dataReq.IMGPATH = res.FILEPATH;
                }

                if (!String.IsNullOrEmpty(dataReq.IMGPATH))
                {
                    var attachedFile = "";
                    var task1 = System.Threading.Tasks.Task.Factory.StartNew(() => attachedFile = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getAttachedFile(dataReq,null,null,conStr));
                    task1.Wait();

                    if (!String.IsNullOrEmpty(attachedFile))
                    {
                        FilesUtilSvc.deleteFile(attachedFile);
                    }

                    var mode = String.IsNullOrEmpty(attachedFile) ? "attach_FILE" : "update_FILE";


                    AUDITPOSTMSTReq req = new AUDITPOSTMSTReq()
                    {
                        SQNO = dataReq.SQNO,
                        COMPANY = dataReq.COMPANY,
                        ASSETNO = dataReq.ASSETNO,
                        INPID = dataReq.INPID,
                        UCODE = dataReq.UCODE,
                        FLAG = "",
                        IMGPATH = dataReq.IMGPATH,
                        MODE = mode

                    };
                    //var updateFILE = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().SP_AUDITPOSTMSTTODEPPHONE(req);

                    var task3 = System.Threading.Tasks.Task.Factory.StartNew(() => ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().SP_AUDITPOSTMSTTODEPPHONE(req,null,null,conStr));
                    task3.Wait();

                    res.FILEPATH = dataReq.IMGPATH;
                    res.FileSRC = FilesUtilSvc.getFile(res.FILEPATH);
                }


                

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
            finally
            {
                var lstPostMSTToTEMP = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().getDataToSendDep(dataReq,null,null,conStr);
                res.AuditToTEMPLST = lstPostMSTToTEMP;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRN(dataReq,null,null,conStr);
                res.POSTTRNDuplicateLST = lstPostTRN;

                var lstNoAudit = ASSETKKF_ADO.Mssql.Audit.AUDITCUTDATEAdo.GetInstant().getNoAudit(dataReq,null,null,conStr);
                //var lstNoAudit = lstPostMSTToTEMP.Where(p => String.IsNullOrEmpty(p.PCODE)).ToList();
                res.NoAuditLST = lstNoAudit;
            }


            return res;
        }



    }
}
