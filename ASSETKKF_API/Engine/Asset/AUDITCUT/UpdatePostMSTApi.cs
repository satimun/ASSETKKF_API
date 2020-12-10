using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Engine.Asset.AUDITCUT
{
    public class UpdatePostMSTApi : Base<ASSETASSETNOReq>
    {
        public UpdatePostMSTApi(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }

        protected override void ExecuteChild(ASSETASSETNOReq dataReq, ResponseAPI dataRes)
        {
            var res = new ASSETKKF_MODEL.Response.Asset.ASSETASSETNORes();
            try
            {
                DBMode = dataReq.DBMode;
                res._result.ServerAddr = ConnectionString();
                res._result.DBMode = DBMode;

                var reqProblem = new STProblemReq
                {
                    Company = dataReq.COMPANY
                };
                var lstProblem = ASSETKKF_ADO.Mssql.Asset.STProblemADO.GetInstant().Search(reqProblem, null, conString);
                var objProblem = lstProblem.FirstOrDefault();

                var reqPostMst = new AUDITPOSTMSTReq
                {
                    SQNO = dataReq.SQNO,
                    DEPCODEOL = dataReq.DEPCODEOL,
                    COMPANY = dataReq.COMPANY,
                    LEADERCODE = dataReq.LEADERCODE,
                    LEADERNAME = dataReq.LEADERNAME,
                    AREACODE = dataReq.AREACODE,
                    AREANAME = dataReq.AREANAME,
                    ASSETNO = dataReq.ASSETNO,
                    FINDY = objProblem.FINDY,
                    PCODE = objProblem.Pcode,
                    PNAME = objProblem.Pname,
                    UCODE = dataReq.UCODE,
                    isdept = dataReq.isdept


                };

                var reqPostChk = new AuditPostCheckReq
                {
                    SQNO = dataReq.SQNO,
                    DEPCODEOL = dataReq.DEPCODEOL,
                    COMPANY = dataReq.COMPANY,
                    LEADERCODE = dataReq.LEADERCODE,
                    LEADERNAME = dataReq.LEADERNAME,
                    AREACODE = dataReq.AREACODE,
                    AREANAME = dataReq.AREANAME,
                    ASSETNO = dataReq.ASSETNO,
                    UCODE = dataReq.UCODE,
                    isdept = dataReq.isdept


                };

                var req1 = new ASSETKKF_MODEL.Request.Asset.AuditPostReq()
                {
                    SQNO = dataReq.SQNO,
                    DEPCODEOL = dataReq.DEPCODEOL,
                    COMPANY = dataReq.COMPANY,
                    LEADERCODE = dataReq.LEADERCODE,
                    AREACODE = dataReq.AREACODE,
                    UCODE = dataReq.UCODE,
                    isdept = dataReq.isdept
                };

                reqPostMst.MODE = "EDIT";
                //ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().updateAUDITPOSTMST(reqPostMst);
                var editAuditPost = System.Threading.Tasks.Task.Factory.StartNew(() => ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().updateAUDITPOSTMST(reqPostMst, null, conString));
                editAuditPost.Wait();

                //res.AUDITPOSTMSTWAITLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req1, "");
                //res.AUDITPOSTMSTCHECKEDLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req1, "Y");
                //res.AUDITPOSTTRNLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTTRN(req1);
                //var lstAUDITAssetNo = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().checkAUDITAssetNo(reqPostChk);
                //res.AUDITPOSTMST = lstAUDITAssetNo.FirstOrDefault();

                var lstAUDITPOSTMST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req1,null, null, conString);
                var lstAUDITPOSTTRN = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTTRN(req1, null, conString);

                res.AUDITPOSTTRNLST = lstAUDITPOSTTRN;
               // var lstWait = lstAUDITPOSTMST.Where(p => String.IsNullOrEmpty(p.PCODE)).ToList();
                var lstChecked = lstAUDITPOSTMST.Where(p => !String.IsNullOrEmpty(p.PCODE)).ToList();
                var lstWait = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST_WAIT(req1, null, null, conString);
                res.AUDITPOSTMSTWAITLST = lstWait;
                res.AUDITPOSTMSTCHECKEDLST = lstChecked;
                res.AUDITPOSTMSTNOPROBLEMLST = lstChecked.Where(x => x.PFLAG != "Y").ToList();
                res.AUDITPOSTMSTPROBLEMLST = lstChecked.Where(x => x.PFLAG == "Y").ToList();

                res.AUDITPOSTMST = lstAUDITPOSTMST.Where(x => x.INPID == dataReq.UCODE).FirstOrDefault();

                AuditSummaryReq reqSum = new AuditSummaryReq()
                {
                    Company = dataReq.COMPANY,
                    year = res.AUDITPOSTMST.YR.ToString(),
                    mn = res.AUDITPOSTMST.MN.ToString(),
                    sqno = dataReq.SQNO,
                    isdept = dataReq.isdept
                };
                //var lstSum = ASSETKKF_ADO.Mssql.Asset.DashboardADO.GetInstant().getInspectionByDEPMST(reqSum, null, conString);
                //res.DashboardInspectionLST = lstSum;


                res._result._code = "200";
                res._result._message = "";
                res._result._status = "OK";

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
