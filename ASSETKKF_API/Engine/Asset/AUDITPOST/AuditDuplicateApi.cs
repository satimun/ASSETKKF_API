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
                    case "confirmduplicatemst":
                        res = confirmDuplicateMST(dataReq, res);
                        break;
                    case "confirmduplicatetrn":
                        res = confirmDuplicateTRN(dataReq, res);
                        break;
                    case "savemst2tmp":
                        res = savemst2tmp(dataReq, res);
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

               /* var lstPostMST = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTAdo.GetInstant().getPOSTMSTDuplicate(dataReq);
                res.POSTMSTDuplicateLST = lstPostMST;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNDuplicate(dataReq);
                res.POSTTRNDuplicateLST = lstPostTRN;*/

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
                    MODE = "updateSNDST_Y_ASSETNO_INPID"
                };

                var updateSNDST = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().saveAUDITPOSTTRN(req);

                /*var lstPostMST = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTAdo.GetInstant().getPOSTMSTDuplicate(dataReq);
                res.POSTMSTDuplicateLST = lstPostMST;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNDuplicate(dataReq);
                res.POSTTRNDuplicateLST = lstPostTRN;*/

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



    }
}
