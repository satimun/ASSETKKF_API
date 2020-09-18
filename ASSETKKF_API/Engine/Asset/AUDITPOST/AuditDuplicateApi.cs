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
                var mode = !String.IsNullOrEmpty(dataReq.mode) ? dataReq.mode.Trim().ToLower() : dataReq.mode;
                switch (mode)
                {
                    case "getduplicate":
                        res = getDuplicate(dataReq,res);
                        break;
                    case "confirmduplicatemst":
                        res = confirmDuplicateMST(dataReq, res);
                        break;
                    case "confirmduplicatetrn":
                        res = confirmDuplicateTRN(dataReq, res);
                        break;
                    case "savemst2tmp":
                        res = savemst2tmp(dataReq, res);
                        break;

                    case "getdata2dep":
                        res = getdata2dep(dataReq, res);
                        break;

                    case "getauditassetno":
                        res = getauditassetno(dataReq, res);
                        break;

                    case "updateaudittotemp":
                        res = updateaudittotemp(dataReq, res);
                        break;

                    case "savebfsend":
                        res = savebfsend(dataReq, res);
                        break;

                    case "confirmduplicatetotemp":
                        res = confirmDuplicateTOTEMP(dataReq, res);
                        break;

                    case "sendaudit2dep":
                        res = sendaudit2dep(dataReq, res);
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

        private AuditDuplicateRes confirmDuplicateMST(AuditPostReq dataReq, AuditDuplicateRes res)
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
                var updateSNDST = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTAdo.GetInstant().saveAUDITPOSTMST(req);

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

        private AuditDuplicateRes confirmDuplicateTRN(AuditPostReq dataReq, AuditDuplicateRes res)
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

                var updateSNDST = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().saveAUDITPOSTTRN(req);


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

        private AuditDuplicateRes confirmDuplicateTOTEMP(AuditPostReq dataReq, AuditDuplicateRes res)
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
                var updateSNDST = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req);

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

        private AuditDuplicateRes savemst2tmp(AuditPostReq dataReq, AuditDuplicateRes res)
        {
            try
            {
                int action = 0;
                var lstNoDuplicate = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTAdo.GetInstant().getNoDuplicateAll(dataReq);
                var lstDuplicate = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTAdo.GetInstant().getDuplicateAll(dataReq);

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
                        ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req1);

                        var req2 = req1;
                        req2.MODE = "addByAssetnoInpid";
                        ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req2);
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
                        ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req3);

                        var req4 = req3;
                        req4.MODE = "addByAssetnoInpid";
                        ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req4);
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
                    ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req5);
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
                var lstPostMST = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTAdo.GetInstant().getPOSTMSTDuplicate(dataReq);
                res.POSTMSTDuplicateLST = lstPostMST;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNDuplicate(dataReq);
                res.POSTTRNDuplicateLST = lstPostTRN;
            }


            return res;
        }

        private AuditDuplicateRes getdata2dep(AuditPostReq dataReq, AuditDuplicateRes res)
        {
            try
            {
                updateCUTDT(dataReq);

                updatePOSTMST_SNDST_Y(dataReq);
                addPOSTTEMP_NotExists_PCODE_SNDST_Y(dataReq);
                addPOSTTEMP_NotExists_PCODE(dataReq);
                addPOSTTEMP_NotExists(dataReq);
                updatePOSTTEMP_SNDST_Y(dataReq);
                updatePOSTTEMP_PCODE_PostMST(dataReq);
                updatePOSTDEP_PCODE_PostMST(dataReq);
                updatePOSTTEMP_PCODE_PostMST_Dup(dataReq);
                updatePOSTDEP_PCODE_PostMST_Dup(dataReq);

                var lstPostMSTToTEMP = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().getDataToSendDep(dataReq);
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
                var lstPostMSTToTEMP = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().getDataToSendDep(dataReq);
                res.AuditToTEMPLST = lstPostMSTToTEMP;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRN(dataReq);
                res.POSTTRNDuplicateLST = lstPostTRN;

                var lstNoAudit = ASSETKKF_ADO.Mssql.Audit.AUDITCUTDATEAdo.GetInstant().getNoAudit(dataReq);
                res.NoAuditLST = lstNoAudit;
            }
            return res;
        }

        private void updateCUTDT(AuditPostReq dataReq)
        {
            AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
            {
                MODE = "updateCUTDT",
                SQNO = dataReq.SQNO,
                COMPANY = dataReq.COMPANY,
                UCODE = dataReq.UCODE

            };
            ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req1);
        }

        private void updatePOSTMST_SNDST_Y(AuditPostReq dataReq)
        {
            AUDITPOSTMSTReq req = new AUDITPOSTMSTReq()
            {
                SQNO = dataReq.SQNO,
                COMPANY = dataReq.COMPANY,
                ASSETNO = dataReq.ASSETNO,
                UCODE = dataReq.UCODE,
                MODE = "updateSNDST_Y"

            };
            var updateSNDST = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTAdo.GetInstant().saveAUDITPOSTMST(req);
        }

        private void addPOSTTEMP_NotExists_PCODE_SNDST_Y(AuditPostReq dataReq)
        {
            AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
            {
                MODE = "addNotExists_PCODE_SNDST_Y",
                SQNO = dataReq.SQNO,
                COMPANY = dataReq.COMPANY,
                UCODE = dataReq.UCODE

            };
            ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req1);
        }

        private void addPOSTTEMP_NotExists_PCODE(AuditPostReq dataReq)
        {
            AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
            {
                MODE = "addNotExists_PCODE",
                SQNO = dataReq.SQNO,
                COMPANY = dataReq.COMPANY,
                UCODE = dataReq.UCODE

            };
            ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req1);
        }

        private void addPOSTTEMP_NotExists(AuditPostReq dataReq)
        {
            AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
            {
                MODE = "addNotExists",
                SQNO = dataReq.SQNO,
                COMPANY = dataReq.COMPANY,
                UCODE = dataReq.UCODE

            };
            ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req1);
        }

        private void updatePOSTTEMP_SNDST_Y(AuditPostReq dataReq)
        {
            AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
            {
                MODE = "updateSNDST_Y",
                SQNO = dataReq.SQNO,
                COMPANY = dataReq.COMPANY,
                UCODE = dataReq.UCODE

            };
            ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req1);
        }

        private void updatePOSTTEMP_PCODE_PostMST(AuditPostReq dataReq)
        {
            AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
            {
                MODE = "updatePCODE_PostMST",
                SQNO = dataReq.SQNO,
                COMPANY = dataReq.COMPANY,
                UCODE = dataReq.UCODE

            };
            ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req1);
        }

        private void updatePOSTDEP_PCODE_PostMST(AuditPostReq dataReq)
        {
            AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
            {
                MODE = "updatePCODE_PostMST",
                SQNO = dataReq.SQNO,
                COMPANY = dataReq.COMPANY,
                UCODE = dataReq.UCODE

            };
            ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().SP_AUDITPOSTMSTTODEP(req1);
        }

        private void updatePOSTTEMP_PCODE_PostMST_Dup(AuditPostReq dataReq)
        {
            AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
            {
                MODE = "updatePCODE_PostMST_Dup",
                SQNO = dataReq.SQNO,
                COMPANY = dataReq.COMPANY,
                UCODE = dataReq.UCODE

            };
            ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req1);
        }

        private void updatePOSTDEP_PCODE_PostMST_Dup(AuditPostReq dataReq)
        {
            AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
            {
                MODE = "updatePCODE_PostMST_Dup",
                SQNO = dataReq.SQNO,
                COMPANY = dataReq.COMPANY,
                UCODE = dataReq.UCODE

            };
            ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().SP_AUDITPOSTMSTTODEP(req1);
        }

        private void InSdataToTEMP(AuditPostReq dataReq)
        {
            AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
            {
                MODE = "add_PostMST_NoDup",
                SQNO = dataReq.SQNO,
                COMPANY = dataReq.COMPANY,
                UCODE = dataReq.UCODE

            };
            ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req1);

            var req2 = req1;
            req2.MODE = "add_PostMST_Dup";
            ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req2);
        }


        private AuditDuplicateRes getauditassetno(AuditPostReq dataReq, AuditDuplicateRes res)
        {
            try
            {
                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().getAuditAssetNo(dataReq);
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
        
            private AuditDuplicateRes updateaudittotemp(AuditPostReq dataReq, AuditDuplicateRes res)
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
                ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req1);

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
                var lstPostMSTToTEMP = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().getDataToSendDep(dataReq);
                res.AuditToTEMPLST = lstPostMSTToTEMP;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRN(dataReq);
                res.POSTTRNDuplicateLST = lstPostTRN;

                var lstNoAudit = ASSETKKF_ADO.Mssql.Audit.AUDITCUTDATEAdo.GetInstant().getNoAudit(dataReq);
                res.NoAuditLST = lstNoAudit;
            }
            return res;
        }

        private AuditDuplicateRes savebfsend(AuditPostReq dataReq, AuditDuplicateRes res)
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
                ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req1);

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
                var lstPostMSTToTEMP = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().getDataToSendDep(dataReq);
                res.AuditToTEMPLST = lstPostMSTToTEMP;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRN(dataReq);
                res.POSTTRNDuplicateLST = lstPostTRN;

                var lstNoAudit = ASSETKKF_ADO.Mssql.Audit.AUDITCUTDATEAdo.GetInstant().getNoAudit(dataReq);
                res.NoAuditLST = lstNoAudit;
            }
            return res;
        }

        private AuditDuplicateRes sendaudit2dep(AuditPostReq dataReq, AuditDuplicateRes res)
        {
            try
            {
                updateCUTDT(dataReq);

                updatePOSTMST_SNDST_Y(dataReq);
                addPOSTTEMP_NotExists_PCODE_SNDST_Y(dataReq);
                addPOSTTEMP_NotExists_PCODE(dataReq);
                addPOSTTEMP_NotExists(dataReq);
                updatePOSTTEMP_SNDST_Y(dataReq);
                updatePOSTTEMP_PCODE_PostMST(dataReq);
                updatePOSTDEP_PCODE_PostMST(dataReq);
                updatePOSTTEMP_PCODE_PostMST_Dup(dataReq);
                updatePOSTDEP_PCODE_PostMST_Dup(dataReq);

                var lstPostMSTToTEMP = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().getDataToSendDep(dataReq);
                if (lstPostMSTToTEMP == null || lstPostMSTToTEMP != null && lstPostMSTToTEMP.Count == 0)
                {
                    InSdataToTEMP(dataReq);
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

                var updateSNDST = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().saveAUDITPOSTTRN(req);

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
                ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().SP_AUDITPOSTMSTTODEP(req1);

                

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
                var lstPostMSTToTEMP = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().getDataToSendDep(dataReq);
                res.AuditToTEMPLST = lstPostMSTToTEMP;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRN(dataReq);
                res.POSTTRNDuplicateLST = lstPostTRN;

                var lstNoAudit = ASSETKKF_ADO.Mssql.Audit.AUDITCUTDATEAdo.GetInstant().getNoAudit(dataReq);
                res.NoAuditLST = lstNoAudit;
            }
            return res;
        }



    }
}
