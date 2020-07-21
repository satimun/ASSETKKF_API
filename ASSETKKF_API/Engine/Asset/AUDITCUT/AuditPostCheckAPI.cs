using ASSETKKF_MODEL.Request.Asset;
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
                        res._result._message = "พบข้อมูล";
                        res._result._status = "OK";
                    }else if (dataReq.MODE != null && dataReq.MODE.ToLower() == "detail")
                    {
                        res.AUDITPOSTMST = lst.FirstOrDefault();

                        res._result._code = "200";
                        res._result._message = "พบข้อมูล";
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

                        //เคยบันทึกแล้ว มาบันทึกซ้ำ
                        var objDuplicate = lst.Where(x => x.INPID == dataReq.UCODE && !String.IsNullOrEmpty(x.PCODE)).FirstOrDefault();
                        //บันทึกครั้งแรก เป็นคน Export
                        var objFirstEx = lst.Where(x => x.INPID == dataReq.UCODE && String.IsNullOrEmpty(x.PCODE)).FirstOrDefault();
                        //บันทึกครั้งแรก ไม่ได้เป็นผู้ Export
                        var objFirst = lst.Where(x => x.INPID != dataReq.UCODE && String.IsNullOrEmpty(x.PCODE)).FirstOrDefault();
                        //บันทึกครั้งแรก แต่มีคนมาบันทึกก่อนหน้าแล้ว
                        var objSecound = lst.Where(x => x.INPID != dataReq.UCODE && !String.IsNullOrEmpty(x.PCODE)).FirstOrDefault();

                        if (objDuplicate != null)
                        {
                            res._result._code = "202";
                            res._result._message = objDuplicate.INPID + "เคยบันทึกผลการตรวจสอบทรัพย์สิน " + objDuplicate.ASSETNO + " แล้ว";
                            res._result._status = "Accepted ";
                        }else if (objFirstEx != null)
                        {
                            reqPostMst.MODE = "";
                            ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().updateAUDITPOSTMST(reqPostMst);
                            var lstPostMstPhone = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getASAUDITPOSTMSTPHONE(reqPostMst);
                            if (lstPostMstPhone == null || (lstPostMstPhone != null && lstPostMstPhone.Count == 0))
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
                            res._result._message = objFirstEx.INPID + "บันทึกผลการตรวจสอบทรัพย์สิน " + objFirstEx.ASSETNO + " เรียบร้อยแล้ว";
                            res._result._status = "Created";
                        }
                        else if (objFirst != null)
                        {
                            reqPostMst.MODE = "ADD";
                            ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().updateAUDITPOSTMST(reqPostMst);

                            res.AUDITPOSTMSTWAITLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req1, "");
                            res.AUDITPOSTMSTCHECKEDLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req1, "Y");
                            res.AUDITPOSTTRNLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTTRN(req1);

                            var lstAUDITAssetNo = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().checkAUDITAssetNo(dataReq);
                            res.AUDITPOSTMST = lstAUDITAssetNo.FirstOrDefault();

                            res._result._code = "201";
                            res._result._message = objFirst.INPNAME + "บันทึกผลการตรวจสอบทรัพย์สิน " + objFirst.ASSETNO + " เรียบร้อยแล้ว";
                            res._result._status = "Created";
                        }
                        else if (objSecound != null)
                        {
                            reqPostMst.MODE = "ADD";
                            ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().updateAUDITPOSTMST(reqPostMst);

                            res.AUDITPOSTMSTWAITLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req1, "");
                            res.AUDITPOSTMSTCHECKEDLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req1, "Y");
                            res.AUDITPOSTTRNLST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTTRN(req1);

                            var lstAUDITAssetNo = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().checkAUDITAssetNo(dataReq);
                            res.AUDITPOSTMST = lstAUDITAssetNo.FirstOrDefault();

                            res._result._code = "201";
                            res._result._message = objSecound.INPID + "บันทึกผลการตรวจสอบทรัพย์สิน " + objSecound.ASSETNO + " เรียบร้อยแล้ว";
                            res._result._status = "Created";
                        }
                        else
                        {
                            var obj = lst.FirstOrDefault();
                            res._result._code = "404";
                            res._result._message = "ไม่พบข้อมูลตามเงื่อนไข :: ผู้ตรวจสอบ " + obj.INPID  + " ,ทรัพย์สิน " + obj.ASSETNO + " : " + obj.ASSETNAME + " " + obj.PCODE;
                            res._result._status = "Bad Request";
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
