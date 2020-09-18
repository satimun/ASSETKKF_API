using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Audit;

namespace ASSETKKF_API.Engine.Asset.AUDITDEP
{
    public class DepEditAuditApi : Base<AuditPostReq>
    {
        public DepEditAuditApi()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(AuditPostReq dataReq, ResponseAPI dataRes)
        {
            var res = new AuditDepRes();
            try
            {
                var mode = !String.IsNullOrEmpty(dataReq.mode) ? dataReq.mode.Trim().ToLower() : dataReq.mode;
                switch (mode)
                {
                    case "getaudittoclear":
                        res = getaudittoclear(dataReq, res);
                        break;

                    case "getaudittoconfirm":
                        res = getaudittoconfirm(dataReq, res);
                        break;

                    case "getauditassetno":
                        res = getauditassetno(dataReq, res);
                        break;

                    case "confirminformedmst":
                        res = confirminformedmst(dataReq, res);
                        break;

                    case "confirminformedtrn":
                        res = confirminformedtrn(dataReq, res);
                        break;

                    case "depeditmst":
                        res = depeditmst(dataReq, res);
                        break;

                    case "depedittrn":
                        res = depedittrn(dataReq, res);
                        break;

                    case "depimgmst":
                        res = depimgmst(dataReq, res);
                        break;

                    case "depimgtrn":
                        res = depimgtrn(dataReq, res);
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

        private AuditDepRes getaudittoclear(AuditPostReq dataReq, AuditDepRes res)
        {
            try
            {
                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getDataToClear(dataReq);
                res.AUDITPOSTMSTTODEPLST = lst;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNDep(dataReq);
                res.POSTTRNDuplicateLST = lstPostTRN;
            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }


            return res;

        }

        private AuditDepRes getaudittoconfirm(AuditPostReq dataReq, AuditDepRes res)
        {
            try
            {
                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getDataToConfirm(dataReq);
                res.AUDITPOSTMSTTODEPLST = lst;
            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }


            return res;

        }

        private AuditDepRes getauditassetno(AuditPostReq dataReq, AuditDepRes res)
        {
            try
            {
                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getAuditAssetNo(dataReq);
                res.AUDITPOSTMSTTODEP = lst != null ? lst.FirstOrDefault() : null;

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

        private AuditDepRes confirminformedmst(AuditPostReq dataReq, AuditDepRes res)
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
                var updateSTCY = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().SP_AUDITPOSTMSTTODEPPHONE(req);

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

        private AuditDepRes confirminformedtrn(AuditPostReq dataReq, AuditDepRes res)
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

                var updateSNDST = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().SP_AUDITPOSTTRNPHONE(req);


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

        private AuditDepRes depeditmst(AuditPostReq dataReq, AuditDepRes res)
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
                var updateSTY = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().SP_AUDITPOSTMSTTODEPPHONE(req);

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
                AuditPostReq req = new AuditPostReq()
                {
                    SQNO = dataReq.SQNO,
                    COMPANY = dataReq.COMPANY,
                    filter = dataReq.filter
                };
                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getDataToClear(req);
                res.AUDITPOSTMSTTODEPLST = lst;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNDep(req);
                res.POSTTRNDuplicateLST = lstPostTRN;
            }


            return res;
        }

        private AuditDepRes depedittrn(AuditPostReq dataReq, AuditDepRes res)
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

                var updateSTY = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().SP_AUDITPOSTTRNPHONE(req);


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
                AuditPostReq req = new AuditPostReq()
                {
                    SQNO = dataReq.SQNO,
                    COMPANY = dataReq.COMPANY,
                    filter = dataReq.filter
                };
                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getDataToClear(req);
                res.AUDITPOSTMSTTODEPLST = lst;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNDep(req);
                res.POSTTRNDuplicateLST = lstPostTRN;
            }


            return res;
        }

        private AuditDepRes depimgmst(AuditPostReq dataReq, AuditDepRes res)
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
                var updateSTY = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().SP_AUDITPOSTMSTTODEPPHONE(req);

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
                AuditPostReq req = new AuditPostReq()
                {
                    SQNO = dataReq.SQNO,
                    COMPANY = dataReq.COMPANY,
                    filter = dataReq.filter
                };
                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getDataToClear(req);
                res.AUDITPOSTMSTTODEPLST = lst;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNDep(req);
                res.POSTTRNDuplicateLST = lstPostTRN;
            }


            return res;
        }

        private AuditDepRes depimgtrn(AuditPostReq dataReq, AuditDepRes res)
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

                var updateSTY = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().SP_AUDITPOSTTRNPHONE(req);


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
                AuditPostReq req = new AuditPostReq()
                {
                    SQNO = dataReq.SQNO,
                    COMPANY = dataReq.COMPANY,
                    filter = dataReq.filter
                };
                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getDataToClear(req);
                res.AUDITPOSTMSTTODEPLST = lst;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNDep(req);
                res.POSTTRNDuplicateLST = lstPostTRN;
            }


            return res;
        }

    }
}
