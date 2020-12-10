using ASSETKKF_ADO.Mssql.Audit;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Audit;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Asset.AUDITCOMP
{
    public class CompEditAuditApi : Base<AuditPostReq>
    {
        public CompEditAuditApi(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }

        protected override void ExecuteChild(AuditPostReq dataReq, ResponseAPI dataRes)
        {
            var res = new AuditCompRes();
            try
            {
                DBMode = dataReq.DBMode;
                res._result.ServerAddr = ConnectionString();
                res._result.DBMode = DBMode;

                var mode = !String.IsNullOrEmpty(dataReq.mode) ? dataReq.mode.Trim().ToLower() : dataReq.mode;
                switch (mode)
                {
                    case "getaudittoedit":
                        res = getaudittoedit(dataReq, res,conString);
                        break;

                    case "depeditmst":
                        res = depeditmst(dataReq, res,conString);
                        break;

                    case "depimgmst":
                        res = depimgmst(dataReq, res,conString);
                        break;

                    case "depimgtrn":
                        res = depimgtrn(dataReq, res,conString);
                        break;

                    case "depedittrn":
                        res = depedittrn(dataReq, res,conString);
                        break;
                    case "checkacc":
                        res = checkacc(dataReq, res, conString);
                        break;
                    case "compliance_close":
                        res = compliance_close(dataReq, res, conString);
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

        private AuditCompRes getaudittoedit(AuditPostReq dataReq, AuditCompRes res, string conStr = null)
        {
            try
            {
                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getDataToCompEdit(dataReq,null,null,conStr);
                res.AUDITPOSTMSTTODEPLST = lst;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNComp(dataReq,null,null,conStr);
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

        private AuditCompRes depeditmst(AuditPostReq dataReq, AuditCompRes res, string conStr = null)
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
                        MODE = "updatePCODE_TODEP",
                        snnstdt = dataReq.snnstdt,
                        expstdt = dataReq.expstdt

                    };
                /*var updateDep = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant(conString).SP_AUDITPOSTMSTTODEP(req);*/
                ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().SP_AUDITPOSTMSTTOTEMP(req,null,null,conStr);

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
                var updateSTCY = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().SP_AUDITPOSTMSTTODEPPHONE(req1,null,null,conStr);

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

                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getDataToCompEdit(req,null,null,conStr);
                res.AUDITPOSTMSTTODEPLST = lst;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNComp(req,null,null,conStr);
                res.POSTTRNDuplicateLST = lstPostTRN;
            }


            return res;

        }

        private AuditCompRes depimgmst(AuditPostReq dataReq, AuditCompRes res, string conStr = null)
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
                var updateSTY = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().SP_AUDITPOSTMSTTODEPPHONE(req,null,null,conStr);

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

                var updateSNDST = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().SP_AUDITPOSTTRNPHONE(req1,null,null,conStr);
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
                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getDataToCompEdit(req,null,null,conStr);
                res.AUDITPOSTMSTTODEPLST = lst;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNComp(req,null,null,conStr);
                res.POSTTRNDuplicateLST = lstPostTRN;
            }


            return res;
        }

        private AuditCompRes depimgtrn(AuditPostReq dataReq, AuditCompRes res, string conStr = null)
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

                var updateSNDST = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().SP_AUDITPOSTTRNPHONE(req1,null,null,conStr);
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
                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getDataToCompEdit(req,null,null,conStr);
                res.AUDITPOSTMSTTODEPLST = lst;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNComp(req,null,null,conStr);
                res.POSTTRNDuplicateLST = lstPostTRN;
            }


            return res;
        }

        private AuditCompRes depedittrn(AuditPostReq dataReq, AuditCompRes res, string conStr = null)
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


                var updateAuditPost = ASSETKKF_ADO.Mssql.Asset.AUDITPOSTTRNADO.GetInstant().addAUDITPOSTTRN(req,null,conStr);

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

                var updateSNDST = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().SP_AUDITPOSTTRNPHONE(req1,null,null,conStr);
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
                var lst = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTODEPAdo.GetInstant().getDataToClear(req,null,null,conStr);
                res.AUDITPOSTMSTTODEPLST = lst;

                var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRNDep(req,null,null,conStr);
                res.POSTTRNDuplicateLST = lstPostTRN;
            }


            return res;
        }

        private  AuditCompRes checkacc(AuditPostReq dataReq, AuditCompRes res, string conStr = null)
        {
            try
            {
                var obj = AuditCompAdo.GetInstant().getAuditAcc(dataReq,  null, conString).FirstOrDefault();
                if(obj != null)
                {
                    res.send_acc = obj.QTY_TOTAL;
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
            return res;
        }

        private AuditCompRes compliance_close(AuditPostReq dataReq, AuditCompRes res, string conStr = null)
        {
            try
            {
                var updateCutMST = AuditManagerAdo.GetInstant().saveAUDITCUTDATEMST(dataReq, null, conStr);

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


    }
}
