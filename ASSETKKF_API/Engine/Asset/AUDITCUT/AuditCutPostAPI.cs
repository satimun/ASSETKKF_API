using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Asset.AUDITCUT
{
    public class AuditCutPostAPI : Base<AuditPostReq>
    {
        public AuditCutPostAPI()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(AuditPostReq dataReq, ResponseAPI dataRes)
        {
            var res = new AuditPostRes();

            try
            {
                var req = new ASSETKKF_MODEL.Request.Asset.AuditPostReq()
                {
                    SQNO = dataReq.SQNO,
                    DEPCODEOL = dataReq.DEPCODEOL,
                    COMPANY = dataReq.COMPANY,
                    LEADERCODE = dataReq.LEADERCODE,
                    AREACODE = dataReq.AREACODE,
                    UCODE = dataReq.UCODE,
                    LEADERNAME = dataReq.LEADERNAME,
                    AREANAME = dataReq.AREANAME,
                    IMGPATH = dataReq.IMGPATH,
                    YEAR = dataReq.YEAR,
                    MN = dataReq.MN,
                    DEPMST = dataReq.DEPMST,
                    cutdt = dataReq.cutdt,
                    OFFICECODE = dataReq.OFFICECODE,
                    TYPECODE = dataReq.TYPECODE,
                    GASTCODE = dataReq.GASTCODE,
                    AREA = dataReq.AREA,

                };

                var reqASSETOFFICECODE = new ASSETOFFICECODEReq()
                {
                    COMPANY = dataReq.COMPANY,
                    DEPCODEOL = dataReq.DEPCODEOL
                };

                var reqASSETASSETNO = new ASSETASSETNOReq()
                {
                    COMPANY = dataReq.COMPANY,
                };

                var objAUDITCUTDATEMST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITCUTDATEMST(req);
                var lstAUDITCUTDATE = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITCUTDATE(req);
                var validAUDITCUTDATEMST = objAUDITCUTDATEMST != null;
                var validAUDITCUTDATE = lstAUDITCUTDATE != null && lstAUDITCUTDATE.Count > 0;

                if (validAUDITCUTDATEMST && validAUDITCUTDATE)
                {
                    var lstAUDITPOSTMST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req);
                    var lstAUDITPOSTTRN = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTTRN(req);
                    var lstASSETOFFICECODE = ASSETKKF_ADO.Mssql.Asset.AUDITPOSTTRNADO.GetInstant().getASSETOFFICECODELST(reqASSETOFFICECODE);
                    var lstASSETASSETNO = ASSETKKF_ADO.Mssql.Asset.AUDITPOSTTRNADO.GetInstant().getASSETASSETNOLST(reqASSETASSETNO);

                    res.ASSETOFFICECODELST = lstASSETOFFICECODE;
                    res.ASSETASSETNOLST = lstASSETASSETNO;
                    res.AUDITPOSTTRNLST = lstAUDITPOSTTRN;


                    if (lstAUDITPOSTMST == null || ((lstAUDITPOSTMST != null) && (lstAUDITPOSTMST.Count == 0)))
                    {
                        var addAuditPost = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().addAUDITPOSTMST(req);
                        lstAUDITPOSTMST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req);
                    }

                    if(lstAUDITPOSTMST == null || ((lstAUDITPOSTMST != null) && (lstAUDITPOSTMST.Count == 0)))
                    {
                        res._result._code = "204";
                        res._result._message = "ไม่พบข้อมูล";
                        res._result._status = "No Content";
                    }
                    else
                    {
                        res._result._code = "200";
                        res._result._message = "";
                        res._result._status = "OK";

                        //res.AUDITPOSTMSTWAITLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req, "");
                        //res.AUDITPOSTMSTCHECKEDLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req, "Y");

                        var lstWait = lstAUDITPOSTMST.Where(p => String.IsNullOrEmpty(p.PCODE)).ToList();
                        var lstChecked = lstAUDITPOSTMST.Where(p => !String.IsNullOrEmpty(p.PCODE)).ToList();
                        res.AUDITPOSTMSTWAITLST = lstWait;
                        res.AUDITPOSTMSTCHECKEDLST = lstChecked;
                    }

                    res.AREACODE = dataReq.AREACODE;                   
                    res.COMPANY = dataReq.COMPANY;
                    res.DEPCODEOL = dataReq.DEPCODEOL;
                    res.LEADERCODE = dataReq.LEADERCODE;
                    res.SQNO = dataReq.SQNO;
                   
                }
                else
                {
                    res._result._code = "404";
                    res._result._message = "ไม่พบข้อมูล";
                    res._result._status = "Not Found";
                }
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
