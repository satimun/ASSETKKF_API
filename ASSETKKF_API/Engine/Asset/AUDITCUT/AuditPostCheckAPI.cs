﻿using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Asset;
using Core.Util;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Asset.AUDITCUT
{
    public class AuditPostCheckAPI : Base<AuditPostCheckReq>
    {
        public AuditPostCheckAPI(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }        

        protected override void ExecuteChild(AuditPostCheckReq dataReq, ResponseAPI dataRes)
        {
            var res = new AuditPostRes();

            DBMode = dataReq.DBMode;
            res._result.ServerAddr = ConnectionString();
            res._result.DBMode = DBMode;


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

            try
            {
                List<ASAUDITPOSTMST> lst = new List<ASAUDITPOSTMST>();
                //var lst = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().checkAUDITAssetNo(dataReq);
                var t1 = System.Threading.Tasks.Task.Factory.StartNew(() =>  lst = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().checkAUDITAssetNo(dataReq,transac,  conString));
                t1.Wait();


                if (lst == null || (lst != null && lst.Count == 0))
                {
                    res._result._code = "404";
                    res._result._message = "ไม่พบข้อมูล";
                    res._result._status = "Bad Request";

                    ASSETASSETNOReq req = new ASSETASSETNOReq()
                    {
                        ASSETNO = dataReq.ASSETNO
                    };
                    ASSETASSETNO obj = ASSETKKF_ADO.Mssql.Asset.AUDITPOSTTRNADO.GetInstant().checkASSETASSETNO(req,transac,  conString);

                    res.ASSETASSETNO = obj;

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
                        var lstProblem = ASSETKKF_ADO.Mssql.Asset.STProblemADO.GetInstant().Search(reqProblem,transac,conString);
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
                            FINDY = objProblem!=null? objProblem.FINDY:null,
                            PCODE = objProblem != null ? objProblem.Pcode : null,
                            PNAME = objProblem != null ? objProblem.Pname : null,
                            UCODE = dataReq.UCODE,
                            PFLAG = objProblem != null ? objProblem.PFLAG : null,
                            isdept = dataReq.isdept

                        };
                        var reqPostMstPhone = new AUDITPOSTMSTReq();
                        reqPostMstPhone = reqPostMst;

                        //เคยบันทึกแล้ว มาบันทึกซ้ำ
                         var objDuplicate = lst.Where(x => x.INPID == dataReq.UCODE && !String.IsNullOrEmpty(x.PCODE)).FirstOrDefault();
                        //บันทึกครั้งแรก เป็นคน Export
                        var objFirstEx = lst.Where(x => x.INPID == dataReq.UCODE && String.IsNullOrEmpty(x.PCODE)).FirstOrDefault();
                        //บันทึกครั้งแรก ไม่ได้เป็นผู้ Export
                        //var objFirst = lst.Where(x => x.INPID != dataReq.UCODE && String.IsNullOrEmpty(x.PCODE)).FirstOrDefault();
                        var objFirst = new ASAUDITPOSTMST();
                        //บันทึกครั้งแรก แต่มีคนมาบันทึกก่อนหน้าแล้ว
                        var objSecound = lst.Where(x => x.INPID != dataReq.UCODE && !String.IsNullOrEmpty(x.PCODE)).FirstOrDefault();

                        if (objDuplicate != null)
                        {
                            res._result._code = "202";
                            res._result._message = objDuplicate.INPID + "เคยบันทึกผลการตรวจสอบทรัพย์สิน " + objDuplicate.ASSETNO + " เป็น " + objDuplicate.PCODE + " : " + objDuplicate.PNAME;
                            res._result._message += " เมื่อ " + objDuplicate.INPDT;
                            res._result._status = "Accepted ";
                            res.AUDITPOSTMST = objDuplicate;
                        }
                        else if (objSecound != null)
                        {
                            if (!String.IsNullOrEmpty(dataReq.status))
                            {

                                reqPostMst.MODE = "";
                                //ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().updateAUDITPOSTMST(reqPostMst);
                                var task1 = System.Threading.Tasks.Task.Factory.StartNew(() => ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().updateAUDITPOSTMST(reqPostMst,transac,conString));
                                task1.Wait();


                                reqPostMstPhone.MODE = "Edit";
                                //ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().updateAUDITPOSTMSTPHONE(reqPostMstPhone);
                                var task2 = System.Threading.Tasks.Task.Factory.StartNew(() => ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().updateAUDITPOSTMSTPHONE(reqPostMstPhone,transac,conString));
                                task2.Wait();

                                res.AUDITPOSTMST = objSecound;


                                res._result._code = "201";
                                res._result._message = objSecound.INPID + "บันทึกผลการตรวจสอบทรัพย์สิน " + objSecound.ASSETNO + " เรียบร้อยแล้ว";
                                res._result._status = "Created";
                            }
                            else
                            {
                                res._result._code = "208";
                                res._result._message = objSecound.INPID + "บันทึกผลการตรวจสอบทรัพย์สิน " + objSecound.ASSETNO + " เป็น " + objSecound.PCODE + " : " + objSecound.PNAME;
                                res._result._message += " เมื่อ " + objSecound.INPDT;
                                res._result._status = "Already Saved ";
                                res.AUDITPOSTMST = objSecound;
                            }

                        }
                        else if (objFirstEx != null)
                        {
                            reqPostMst.MODE = "";
                            //ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().updateAUDITPOSTMST(reqPostMst);
                            var task1 = System.Threading.Tasks.Task.Factory.StartNew(() => ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().updateAUDITPOSTMST(reqPostMst,transac,conString));
                            task1.Wait();

                            reqPostMstPhone.MODE = "Edit";
                            //ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().updateAUDITPOSTMSTPHONE(reqPostMstPhone);
                            var task2 = System.Threading.Tasks.Task.Factory.StartNew(() => ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().updateAUDITPOSTMSTPHONE(reqPostMstPhone,transac,conString));
                            task2.Wait();

                            res.AUDITPOSTMST = objFirstEx;


                            res._result._code = "201";
                            res._result._message = objFirstEx.INPID + "บันทึกผลการตรวจสอบทรัพย์สิน " + objFirstEx.ASSETNO + " เรียบร้อยแล้ว";
                            res._result._status = "Created";
                        }
                        else if (objFirst != null)
                        {
                            reqPostMst.MODE = "ADD";
                            //ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().updateAUDITPOSTMST(reqPostMst);
                            var task1 = System.Threading.Tasks.Task.Factory.StartNew(() => ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().updateAUDITPOSTMST(reqPostMst,transac,conString));
                            task1.Wait();

                            res.AUDITPOSTMST = objFirst;

                            res._result._code = "201";
                            res._result._message = objFirst.INPNAME + "บันทึกผลการตรวจสอบทรัพย์สิน " + objFirst.ASSETNO + " เรียบร้อยแล้ว";
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
                var lstAUDITCUTDATE = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITCUTDATE(req1,transac,conString);
                res.AUDITCUTDATELST = lstAUDITCUTDATE;

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
                    isdept = dataReq.isdept

                };

                var lstAUDITPOSTMST = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST(req,null, null, conString);
                var lstAUDITPOSTTRN = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTTRN(req, null, conString);

                res.AUDITPOSTTRNLST = lstAUDITPOSTTRN;

                //var lstWait = lstAUDITPOSTMST.Where(p => String.IsNullOrEmpty(p.PCODE)).ToList();
                var lstChecked = lstAUDITPOSTMST.Where(p => !String.IsNullOrEmpty(p.PCODE)).ToList();
                var lstWait = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().getAUDITPOSTMST_WAIT(req, null, null, conString);
                res.AUDITPOSTMSTWAITLST = lstWait;
                res.AUDITPOSTMSTCHECKEDLST = lstChecked;
                res.AUDITPOSTMSTNOPROBLEMLST = lstChecked.Where(x => x.PFLAG != "Y").ToList();
                res.AUDITPOSTMSTPROBLEMLST = lstChecked.Where(x => x.PFLAG == "Y").ToList();

                //res.AUDITPOSTMST = lstAUDITPOSTMST.Where(x => x.INPID == dataReq.UCODE && x.ASSETNO == dataReq.ASSETNO && x.SQNO == dataReq.SQNO).FirstOrDefault();
                AuditSummaryReq reqSum = new AuditSummaryReq()
                {
                    Company = dataReq.COMPANY,
                    year = dataReq.YEAR,
                    mn = dataReq.MN,
                    sqno = dataReq.SQNO,
                    isdept = dataReq.isdept
                };
                //var lstSum = ASSETKKF_ADO.Mssql.Asset.DashboardADO.GetInstant().getInspectionByDEPMST(reqSum,transac,conString);
                //res.DashboardInspectionLST = lstSum;
            }

            dataRes.data = res;

        }
    }
}
