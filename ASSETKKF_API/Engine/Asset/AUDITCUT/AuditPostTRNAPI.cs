using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_API.Service;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Asset;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Engine.Asset.AUDITCUT
{
    public class AuditPostTRNAPI : Base<AUDITPOSTTRNReq>
    {
        private FilesUtil FilesUtilSvc;
        public AuditPostTRNAPI(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
            FilesUtilSvc = new FilesUtil(Configuration);
        }

        protected override void ExecuteChild(AUDITPOSTTRNReq dataReq, ResponseAPI dataRes)
        {
            var res = new AuditPostTRNRes();

            try
            {
                DBMode = dataReq.DBMode;
                res._result.ServerAddr = ConnectionString();
                res._result.DBMode = DBMode;

                if (dataReq.MODE.Trim().ToLower() == "add")
                {
                    var objTRN = ASSETKKF_ADO.Mssql.Asset.AUDITPOSTTRNADO.GetInstant().getAuditPostTRN(dataReq,null,conString);
                    if(objTRN != null && objTRN.Count > 0)
                    {
                        throw new Exception("คุณได้บันทึกตรวจสอบรหัสทรัพย์สินนี้แล้ว กรุณาตรวจสอบข้อมูล");
                    }
                }

                var updateAuditPost = ASSETKKF_ADO.Mssql.Asset.AUDITPOSTTRNADO.GetInstant().addAUDITPOSTTRN(dataReq,null,conString);

                if (!String.IsNullOrEmpty(dataReq.IMGPATH))
                {
                    res.IMGPATH = FilesUtilSvc.uploadCamera(dataReq.IMGPATH);
                    dataReq.IMGPATH = res.IMGPATH;
                }

                if (dataReq.FileToUpload != null)
                {
                    res.IMGPATH = FilesUtilSvc.uploadImgFile(dataReq.FileToUpload);
                    dataReq.IMGPATH = res.IMGPATH;
                }

                if (!String.IsNullOrEmpty(res.IMGPATH))
                {
                    ASSETKKF_ADO.Mssql.Asset.AUDITPOSTTRNADO.GetInstant().UpdateAUDITPOSTTRNIMG(dataReq,null,conString);
                    res.IMGSRC = FilesUtilSvc.getImageURL(res.IMGPATH);
                }

                if (dataReq.MODE.Trim().ToLower() == "editnew")
                {
                    AuditPostReq req1 = new AuditPostReq()
                    {
                        COMPANY = dataReq.COMPANY,
                        SQNO = dataReq.SQNO
                    };
                    var lstPostMSTToTEMP = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTTOTEMPAdo.GetInstant().getDataToSendDep(req1,null,null,conString);
                    res.AuditToTEMPLST = lstPostMSTToTEMP;

                    var lstPostTRN = ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant().getPOSTTRN(req1,null,null,conString);
                    res.POSTTRNDuplicateLST = lstPostTRN;

                    var lstNoAudit = ASSETKKF_ADO.Mssql.Audit.AUDITCUTDATEAdo.GetInstant().getNoAudit(req1,null,null,conString);
                    res.NoAuditLST = lstNoAudit;

                    res._result._code = "200";
                    res._result._message = "";
                    res._result._status = "OK";
                }
                else
                {
                    var req1 = new ASSETKKF_MODEL.Request.Asset.AuditPostReq()
                    {
                        SQNO = dataReq.SQNO,
                        DEPCODEOL = dataReq.DEPCODEOL,
                        COMPANY = dataReq.COMPANY,
                        LEADERCODE = dataReq.LEADERCODE,
                        AREACODE = dataReq.AREACODE,
                        UCODE = dataReq.UCODE
                    };

                    res.AUDITPOSTTRNLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTTRN(req1,null,conString);

                    var lstAUDITPOSTMST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req1,null,null,conString);
                    //var lstWait = lstAUDITPOSTMST.Where(p => String.IsNullOrEmpty(p.PCODE)).ToList();
                    var lstChecked = lstAUDITPOSTMST.Where(p => !String.IsNullOrEmpty(p.PCODE)).ToList();
                    var lstWait = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST_WAIT(req1, null, null, conString);
                    res.AUDITPOSTMSTWAITLST = lstWait;
                    res.AUDITPOSTMSTCHECKEDLST = lstChecked;
                    res.AUDITPOSTMSTNOPROBLEMLST = lstChecked.Where(x => x.PFLAG != "Y").ToList();
                    res.AUDITPOSTMSTPROBLEMLST = lstChecked.Where(x => x.PFLAG == "Y").ToList();
                    var lstAUDITCUTDATE = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITCUTDATE(req1,null,conString);
                    res.AUDITCUTDATELST = lstAUDITCUTDATE;

                    res.AREACODE = dataReq.AREACODE;
                    res.COMPANY = dataReq.COMPANY;
                    res.DEPCODEOL = dataReq.DEPCODEOL;
                    res.LEADERCODE = dataReq.LEADERCODE;
                    res.SQNO = dataReq.SQNO;

                    res._result._code = "201";
                    res._result._message = "";
                    res._result._status = "Created";
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

    }
}
