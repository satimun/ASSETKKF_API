using ASSETKKF_API.Service;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Asset;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Asset.AUDITCUT
{
    public class AuditPostMSTAPI : Base<AUDITPOSTMSTReq>
    {
        private FilesUtil FilesUtilSvc ;
        public AuditPostMSTAPI(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
            FilesUtilSvc = new FilesUtil(Configuration);
        }

        protected override void ExecuteChild(AUDITPOSTMSTReq dataReq, ResponseAPI dataRes)
        {
            var res = new AuditPostRes();

            try
            {
                DBMode = dataReq.DBMode;
                res._result.ServerAddr = ConnectionString();
                res._result.DBMode = DBMode;

                var req = new ASSETKKF_MODEL.Request.Asset.AUDITPOSTMSTReq()
                {
                    SQNO = dataReq.SQNO,
                    DEPCODEOL = dataReq.DEPCODEOL,
                    COMPANY = dataReq.COMPANY,
                    LEADERCODE = dataReq.LEADERCODE,
                    LEADERNAME = dataReq.LEADERNAME,
                    AREACODE = dataReq.AREACODE,
                    AREANAME = dataReq.AREANAME,
                    UCODE = dataReq.UCODE,
                    ASSETNO = dataReq.ASSETNO,
                    FINDY = dataReq.FINDY,
                    PCODE = dataReq.PCODE,
                    PNAME = dataReq.PNAME,
                    MEMO1 = dataReq.MEMO1,
                    IMGPATH = dataReq.IMGPATH,
                    FileToUpload = dataReq.FileToUpload,
                    PFLAG = dataReq.PFLAG
                };

                //var updateAuditPost = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().updateAUDITPOSTMST(dataReq);
                var updateAuditPost = System.Threading.Tasks.Task.Factory.StartNew(() => ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().updateAUDITPOSTMST(dataReq, null, conString));
                updateAuditPost.Wait();



                if (!String.IsNullOrEmpty(req.IMGPATH))
                {
                    res.IMGPATH = FilesUtilSvc.uploadCamera(req.IMGPATH);
                    req.IMGPATH = res.IMGPATH;
                    dataReq.IMGPATH = res.IMGPATH;
                }

                if(req.FileToUpload != null)
                {
                    res.IMGPATH = FilesUtilSvc.uploadImgFile(req.FileToUpload);
                    req.IMGPATH = res.IMGPATH;
                }

                if (!String.IsNullOrEmpty(res.IMGPATH))
                {
                    //ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().UpdateAUDITPOSTMSTImage(dataReq);
                    var task2 = System.Threading.Tasks.Task.Factory.StartNew(() => ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().UpdateAUDITPOSTMSTImage(dataReq, null, conString));
                    task2.Wait();
                    res.IMGSRC = FilesUtilSvc.getImageURL(res.IMGPATH);
                }

                

                res._result._code = "201";
                res._result._message = "";
                res._result._status = "Created";

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
                var req1 = new ASSETKKF_MODEL.Request.Asset.AuditPostReq()
                {
                    SQNO = dataReq.SQNO,
                    DEPCODEOL = dataReq.DEPCODEOL,
                    COMPANY = dataReq.COMPANY,
                    LEADERCODE = dataReq.LEADERCODE,
                    AREACODE = dataReq.AREACODE,
                    UCODE = dataReq.UCODE
                };
                res.AUDITPOSTTRNLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTTRN(req1, null, conString);

                var lstAUDITPOSTMST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req1,null, null, conString);
                var lstWait = lstAUDITPOSTMST.Where(p => String.IsNullOrEmpty(p.PCODE)).ToList();
                var lstChecked = lstAUDITPOSTMST.Where(p => !String.IsNullOrEmpty(p.PCODE)).ToList();
                res.AUDITPOSTMSTWAITLST = lstWait;
                res.AUDITPOSTMSTCHECKEDLST = lstChecked;
                res.AUDITPOSTMSTNOPROBLEMLST = lstChecked.Where(x => x.PFLAG != "Y").ToList();
                res.AUDITPOSTMSTPROBLEMLST = lstChecked.Where(x => x.PFLAG == "Y").ToList();
                var lstAUDITCUTDATE = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITCUTDATE(req1, null, conString);
                res.AUDITCUTDATELST = lstAUDITCUTDATE;

                res.AUDITPOSTMST = lstAUDITPOSTMST.Where(x => x.INPID == dataReq.UCODE && x.ASSETNO == dataReq.ASSETNO && x.SQNO == dataReq.SQNO).FirstOrDefault();

                AuditSummaryReq reqSum = new AuditSummaryReq()
                {
                    Company = dataReq.COMPANY,
                    year = res.AUDITPOSTMST.YR.ToString(),
                    mn = res.AUDITPOSTMST.MN.ToString(),
                    sqno = dataReq.SQNO
                };
                var lstSum = ASSETKKF_ADO.Mssql.Asset.DashboardADO.GetInstant().getInspectionByDEPMST(reqSum, null, conString);
                res.DashboardInspectionLST = lstSum;


                res.AREACODE = dataReq.AREACODE;
                res.COMPANY = dataReq.COMPANY;
                res.DEPCODEOL = dataReq.DEPCODEOL;
                res.LEADERCODE = dataReq.LEADERCODE;
                res.SQNO = dataReq.SQNO;
            }

            dataRes.data = res;
        }

        
    }
}
