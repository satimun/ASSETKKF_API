﻿using ASSETKKF_API.Service;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Asset;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Asset.AUDITCUT
{
    public class AuditPostMSTAPI : Base<AUDITPOSTMSTReq>
    {
        private IConfiguration Configuration;
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

                var updateAuditPost = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().updateAUDITPOSTMST(dataReq);



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
                    ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().UpdateAUDITPOSTMSTImage(dataReq);
                    res.IMGSRC = FilesUtilSvc.getImageURL(res.IMGPATH);
                }

                var req1 = new ASSETKKF_MODEL.Request.Asset.AuditPostReq()
                {
                    SQNO = dataReq.SQNO,
                    DEPCODEOL = dataReq.DEPCODEOL,
                    COMPANY = dataReq.COMPANY,
                    LEADERCODE = dataReq.LEADERCODE,
                    AREACODE = dataReq.AREACODE,
                    UCODE = dataReq.UCODE
                };
                res.AUDITPOSTTRNLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTTRN(req1);

                var lstAUDITPOSTMST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req1);
                var lstWait = lstAUDITPOSTMST.Where(p => String.IsNullOrEmpty(p.PCODE)).ToList();
                var lstChecked = lstAUDITPOSTMST.Where(p => !String.IsNullOrEmpty(p.PCODE)).ToList();
                res.AUDITPOSTMSTWAITLST = lstWait;
                res.AUDITPOSTMSTCHECKEDLST = lstChecked;
                res.AUDITPOSTMSTNOPROBLEMLST = lstChecked.Where(x => x.PFLAG != "Y").ToList();
                res.AUDITPOSTMSTPROBLEMLST = lstChecked.Where(x => x.PFLAG == "Y").ToList();
                var lstAUDITCUTDATE = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITCUTDATE(req1);
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
            catch(Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }

            dataRes.data = res;
        }

        
    }
}
