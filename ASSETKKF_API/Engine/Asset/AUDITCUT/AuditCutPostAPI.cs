using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Asset;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Asset.AUDITCUT
{
    public class AuditCutPostAPI : Base<AuditPostReq>
    {
        public AuditCutPostAPI(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }

        protected override void ExecuteChild(AuditPostReq dataReq, ResponseAPI dataRes)
        {
            var res = new AuditPostRes();
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
                isdept = dataReq.isdept

            };

            List<ASAUDITPOSTMST> lstAUDITPOSTMST = new List<ASAUDITPOSTMST>();
            var stadd = 0;

            try
            {
                DBMode = dataReq.DBMode;
                res._result.ServerAddr = ConnectionString();
                res._result.DBMode = DBMode;


                var reqASSETOFFICECODE = new ASSETOFFICECODEReq()
                {
                    COMPANY = dataReq.COMPANY,
                    DEPCODEOL = dataReq.DEPCODEOL,
                };

                var reqASSETASSETNO = new ASSETASSETNOReq()
                {
                    COMPANY = dataReq.COMPANY,

                };

                //var objAUDITCUTDATEMST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITCUTDATEMST(req);
                var lstAUDITCUTDATE = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITCUTDATE(req, null, conString);
               // var validAUDITCUTDATEMST = objAUDITCUTDATEMST != null;
                var validAUDITCUTDATE = lstAUDITCUTDATE != null && lstAUDITCUTDATE.Count > 0;

                res.AUDITCUTDATELST = lstAUDITCUTDATE;

                if (validAUDITCUTDATE)
                {
                    lstAUDITPOSTMST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req, null,null, conString);
                    //var lstAUDITPOSTTRN = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTTRN(req);
                    /*var lstASSETOFFICECODE = ASSETKKF_ADO.Mssql.Asset.AUDITPOSTTRNADO.GetInstant().getASSETOFFICECODELST(reqASSETOFFICECODE);
                    var lstASSETASSETNO = ASSETKKF_ADO.Mssql.Asset.AUDITPOSTTRNADO.GetInstant().getASSETASSETNOLST(reqASSETASSETNO);

                    res.ASSETOFFICECODELST = lstASSETOFFICECODE;
                    res.ASSETASSETNOLST = lstASSETASSETNO;*/
                    //res.AUDITPOSTTRNLST = lstAUDITPOSTTRN;


                    if (lstAUDITPOSTMST == null || ((lstAUDITPOSTMST != null) && (lstAUDITPOSTMST.Count == 0)))
                    {
                        //var addAuditPost = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().addAUDITPOSTMST(req);
                        var addAuditPost = System.Threading.Tasks.Task.Factory.StartNew(() => ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().addAUDITPOSTMST(req, null, conString));
                        addAuditPost.Wait();
                        stadd = 1;
                        //lstAUDITPOSTMST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req);
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
                if(stadd > 0)
                {
                    lstAUDITPOSTMST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req, null,null,conString);
                }
                
                var lstAUDITPOSTTRN = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTTRN(req, null, conString);
                res.AUDITPOSTTRNLST = lstAUDITPOSTTRN;

                //var lstWait = lstAUDITPOSTMST.Where(p => String.IsNullOrEmpty(p.PCODE)).ToList();
                var lstChecked = lstAUDITPOSTMST.Where(p => !String.IsNullOrEmpty(p.PCODE)).ToList();

                var lstWait = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST_WAIT(req, null, null, conString);
                
                res.AUDITPOSTMSTWAITLST = lstWait;
                res.AUDITPOSTMSTCHECKEDLST = lstChecked;
                res.AUDITPOSTMSTNOPROBLEMLST = lstChecked.Where(x => x.PFLAG != "Y").ToList();
                res.AUDITPOSTMSTPROBLEMLST = lstChecked.Where(x => x.PFLAG == "Y").ToList();

                AuditSummaryReq reqSum = new AuditSummaryReq()
                {
                    Company = dataReq.COMPANY,
                    year = dataReq.YEAR,
                    mn = dataReq.MN,
                    sqno = dataReq.SQNO,
                    isdept = dataReq.isdept 
                };

                //var lstSum = ASSETKKF_ADO.Mssql.Asset.DashboardADO.GetInstant().getInspectionByDEPMST(reqSum, null, conString);
                //res.DashboardInspectionLST = lstSum;

            }
            

            dataRes.data = res;

        }
    }
}
