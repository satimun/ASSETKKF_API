using System;
using System.Collections.Generic;
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
        private IConfiguration Configuration;
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

                var updateAuditPost = ASSETKKF_ADO.Mssql.Asset.AUDITPOSTTRNADO.GetInstant().addAUDITPOSTTRN(dataReq);

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
                    ASSETKKF_ADO.Mssql.Asset.AUDITPOSTTRNADO.GetInstant().UpdateAUDITPOSTTRNIMG(dataReq);
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
                res.AUDITPOSTMSTWAITLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req1, "");
                res.AUDITPOSTMSTCHECKEDLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req1, "Y");
                res.AUDITPOSTTRNLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTTRN(req1);

                res.AREACODE = dataReq.AREACODE;
                res.COMPANY = dataReq.COMPANY;
                res.DEPCODEOL = dataReq.DEPCODEOL;
                res.LEADERCODE = dataReq.LEADERCODE;
                res.SQNO = dataReq.SQNO;

                res._result._code = "201";
                res._result._message = "";
                res._result._status = "Created";

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
