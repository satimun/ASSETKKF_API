using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Asset.AUDITCOMP
{
    public class CompEditAuditApi : Base<AuditPostReq>
    {
        public CompEditAuditApi()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(AuditPostReq dataReq, ResponseAPI dataRes)
        {
            var res = new AuditCompRes();
            try
            {
                var mode = !String.IsNullOrEmpty(dataReq.mode) ? dataReq.mode.Trim().ToLower() : dataReq.mode;
                switch (mode)
                {
                    case "getaudittoedit":
                        res = getaudittoedit(dataReq, res);
                        break;

                    case "depeditmst":
                        res = depeditmst(dataReq, res);
                        break;

                    case "depimgmst":
                        res = depimgmst(dataReq, res);
                        break;

                    case "depimgtrn":
                        res = depimgtrn(dataReq, res);
                        break;

                    case "depedittrn":
                        res = depedittrn(dataReq, res);
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

        private AuditCompRes getaudittoedit(AuditPostReq dataReq, AuditCompRes res)
        {
            try
            {
                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getDataToCompEdit(dataReq);
                res.AUDITPOSTMSTTODEPLST = lst;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNComp(dataReq);
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

        private AuditCompRes depeditmst(AuditPostReq dataReq, AuditCompRes res)
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
                        FINDY = dataReq.FINDY,
                        PCODE = dataReq.PCODE,
                        PNAME = dataReq.PNAME,
                        MODE = "updatePCODE"

                    };
                var updateDep = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().SP_AUDITPOSTMSTTODEP(req);

                Thread.Sleep(1000);

                AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
                {
                    SQNO = dataReq.SQNO,
                    COMPANY = dataReq.COMPANY,
                    ASSETNO = dataReq.ASSETNO,
                    INPID = dataReq.INPID,
                    UCODE = dataReq.UCODE,
                    FLAG = "",
                    MODE = "updateDEP_STY"

                };
                var updateSTCY = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().SP_AUDITPOSTMSTTODEPPHONE(req1);

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
            }


            return res;

        }

        private AuditCompRes depimgmst(AuditPostReq dataReq, AuditCompRes res)
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
                    MODE = "update_ComIMG"

                };
                var updateSTY = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().SP_AUDITPOSTMSTTODEPPHONE(req);

                Thread.Sleep(1000);

                AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
                {
                    SQNO = dataReq.SQNO,
                    COMPANY = dataReq.COMPANY,
                    ASSETNO = dataReq.ASSETNO,
                    INPID = dataReq.INPID,
                    UCODE = dataReq.UCODE,
                    FLAG = "",
                    MODE = "updateTRN_STY"

                };

                var updateSNDST = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().SP_AUDITPOSTTRNPHONE(req1);
                Thread.Sleep(1000);

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
                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getDataToCompEdit(req);
                res.AUDITPOSTMSTTODEPLST = lst;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNComp(req);
                res.POSTTRNDuplicateLST = lstPostTRN;
            }


            return res;
        }

        private AuditCompRes depimgtrn(AuditPostReq dataReq, AuditCompRes res)
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

                Thread.Sleep(1000);

                AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
                {
                    SQNO = dataReq.SQNO,
                    COMPANY = dataReq.COMPANY,
                    ASSETNO = dataReq.ASSETNO,
                    INPID = dataReq.INPID,
                    UCODE = dataReq.UCODE,
                    FLAG = "",
                    MODE = "updateTRN_STY"

                };

                var updateSNDST = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().SP_AUDITPOSTTRNPHONE(req1);
                Thread.Sleep(1000);


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
                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getDataToCompEdit(req);
                res.AUDITPOSTMSTTODEPLST = lst;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNComp(req);
                res.POSTTRNDuplicateLST = lstPostTRN;
            }


            return res;
        }

        private AuditCompRes depedittrn(AuditPostReq dataReq, AuditCompRes res)
        {
            try
            {
                AUDITPOSTTRNReq req = new AUDITPOSTTRNReq()
                {
                    SQNO = dataReq.SQNO,
                    COMPANY = dataReq.COMPANY,
                    ASSETNO = dataReq.ASSETNO,
                    INPID = dataReq.INPID,
                    UCODE = dataReq.UCODE,
                    FLAG = "",
                    MEMO1 = dataReq.MEMO1,
                    ASSETNAME = dataReq.ASSETNAME,
                    OFFICECODE = dataReq.OFFICECODE,
                    OFNAME = dataReq.OFNAME,
                    ASSETNONEW = dataReq.ASSETNONEW,
                    MODE = "EDITNEW"

                };


                var updateAuditPost = ASSETKKF_ADO.Mssql.Asset.AUDITPOSTTRNADO.GetInstant().addAUDITPOSTTRN(req);

                Thread.Sleep(1000);

                AUDITPOSTMSTReq req1 = new AUDITPOSTMSTReq()
                {
                    SQNO = dataReq.SQNO,
                    COMPANY = dataReq.COMPANY,
                    ASSETNO = dataReq.ASSETNO,
                    INPID = dataReq.INPID,
                    UCODE = dataReq.UCODE,
                    FLAG = "",
                    MODE = "updateTRN_STY"

                };

                var updateSNDST = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().SP_AUDITPOSTTRNPHONE(req1);
                Thread.Sleep(1000);


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
