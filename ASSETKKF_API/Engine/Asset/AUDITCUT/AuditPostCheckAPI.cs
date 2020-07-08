﻿using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Asset;
using Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Asset.AUDITCUT
{
    public class AuditPostCheckAPI : Base<AuditPostCheckReq>
    {
        public AuditPostCheckAPI()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(AuditPostCheckReq dataReq, ResponseAPI dataRes)
        {
            var res = new AuditPostRes();


            var req1 = new ASSETKKF_MODEL.Request.Asset.AuditPostReq()
            {
                SQNO = dataReq.SQNO,
                DEPCODEOL = dataReq.DEPCODEOL,
                COMPANY = dataReq.COMPANY,
                LEADERCODE = dataReq.LEADERCODE,
                AREACODE = dataReq.AREACODE,
                UCODE = dataReq.UCODE
            };

            try
            {
                var lst = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().checkAUDITAssetNo(dataReq);
                

                if (lst == null || (lst != null && lst.Count == 0))
                {
                    res._result._code = "404";
                    res._result._message = "ไม่พบข้อมูล";
                    res._result._status = "Bad Request";
                }
                else
                {
                    if(dataReq.MODE != null && dataReq.MODE.ToLower() == "check")
                    {
                        var obj = lst.Where(x => x.INPID == dataReq.UCODE).FirstOrDefault();
                        res.AUDITPOSTMST = obj;

                        res._result._code = "200";
                        res._result._message = "";
                        res._result._status = "OK";
                    }
                    else
                    {

                    

                    var reqProblem = new STProblemReq
                    {
                        Company = dataReq.COMPANY
                    };
                    var lstProblem = ASSETKKF_ADO.Mssql.Asset.STProblemADO.GetInstant().Search(reqProblem);
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
                        PFLAG = dataReq.PFLAG


                    };

                    var reqPostMstPhone = new AUDITPOSTMSTReq();
                    reqPostMstPhone = reqPostMst;

                    if (lst.Count == 1)
                    {
                        var objPostChk = lst.FirstOrDefault();
                        
                        // บันทึกผลการทดสอบเป็นคนแรก
                        if (String.IsNullOrEmpty(objPostChk.PCODE))
                        {
                            reqPostMst.MODE = "";
                            ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().updateAUDITPOSTMST(reqPostMst);

                            var lstPostMstPhone = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getASAUDITPOSTMSTPHONE(reqPostMst);
                            if(lstPostMstPhone == null || (lstPostMstPhone != null && lstPostMstPhone.Count == 0))
                            {
                                reqPostMstPhone.MODE = "Add";
                            }
                            else
                            {
                                reqPostMstPhone.MODE = "Edit";
                            }
                            ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().updateAUDITPOSTMSTPHONE(reqPostMstPhone);

                            res.AUDITPOSTMSTWAITLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req1, "");
                            res.AUDITPOSTMSTCHECKEDLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req1, "Y");
                            res.AUDITPOSTTRNLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTTRN(req1);

                            var lstAUDITAssetNo = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().checkAUDITAssetNo(dataReq);
                            res.AUDITPOSTMST = lstAUDITAssetNo.FirstOrDefault();

                            res._result._code = "201";
                            res._result._message = "";
                            res._result._status = "Created";
                        }
                        else
                        {
                            if (!objPostChk.INPID.Equals(dataReq.UCODE))
                            {
                                reqPostMst.MODE = "ADD";
                                ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().updateAUDITPOSTMST(reqPostMst);

                                res.AUDITPOSTMSTWAITLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req1, "");
                                res.AUDITPOSTMSTCHECKEDLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req1, "Y");
                                res.AUDITPOSTTRNLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTTRN(req1);

                                var lstAUDITAssetNo = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().checkAUDITAssetNo(dataReq);
                                res.AUDITPOSTMST = lstAUDITAssetNo.FirstOrDefault();

                                res._result._code = "201";
                                res._result._message = "";
                                res._result._status = "Created";

                            }
                            else
                            {
                                res._result._code = "202";
                                res._result._message = "";
                                res._result._status = "Accepted ";

                            }

                        }
                    }
                    else
                    {
                        var tmpPostMst = lst.Where(x => x.INPID.Equals(dataReq.UCODE)).ToList();
                        if (tmpPostMst == null)
                        {
                            reqPostMst.MODE = "ADD";
                            ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().updateAUDITPOSTMST(reqPostMst);

                            res.AUDITPOSTMSTWAITLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req1, "");
                            res.AUDITPOSTMSTCHECKEDLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req1, "Y");
                            res.AUDITPOSTTRNLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTTRN(req1);

                           var lstAUDITAssetNo = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().checkAUDITAssetNo(dataReq);
                           res.AUDITPOSTMST = lstAUDITAssetNo.FirstOrDefault();

                                res._result._code = "201";
                            res._result._message = "";
                            res._result._status = "Created";

                        }
                        else
                        {
                            res._result._code = "202";
                            res._result._message = "";
                            res._result._status = "Accepted ";

                        }
                    }

                        
                    }

                }
            }
            catch(Exception ex)
            {
                res.AUDITPOSTMSTWAITLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req1, "");
                res.AUDITPOSTMSTCHECKEDLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req1, "Y");
                res.AUDITPOSTTRNLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTTRN(req1);

                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }
            dataRes.data = res;

        }
    }
}
