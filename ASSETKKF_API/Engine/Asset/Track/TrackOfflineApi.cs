using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Request.Track;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Asset;
using ASSETKKF_MODEL.Response.Track;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Engine.Asset.Track
{
    public class TrackOfflineApi : Base<TrackOfflineReq>
    {
        public TrackOfflineApi(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }

        protected override void ExecuteChild(TrackOfflineReq dataReq, ResponseAPI dataRes)
        {
            var res = new TrackOfflineRes();

            try
            {
                DBMode = dataReq.DBMode;
                res._result.ServerAddr = ConnectionString();
                var mode = String.IsNullOrEmpty(dataReq.mode) ? dataReq.mode : dataReq.mode.ToLower();
                switch (mode)
                {
                    case "getsqno":
                        getSQNO(dataReq,res);
                        break;

                    case "getproblem":
                        GetProblem(dataReq, res);
                        break;

                    case "addpostmst":
                        addPostMST(dataReq,res);
                        break;

                    case "addposttrn":
                        addPostTRN(dataReq, res);
                        break;

                    case "updatepostmst":
                        auditPostMST(dataReq, res);
                        break;

                    case "updateposttrn":
                        auditPostTRN(dataReq, res);
                        break;

                    case "gettrackhd":
                        GetTrackOfflineHD(dataReq, res);
                        break;

                    case "setauditpostmst":
                        SetAuditPostMST(dataReq, res);
                        break;

                    default:
                        getTrackOffline(dataReq, res);
                        break;
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

        private TrackOfflineRes SetAuditPostMST(TrackOfflineReq dataReq, TrackOfflineRes res)
        {
            try
            {
                List<ASAUDITPOSTMST> lst = new List<ASAUDITPOSTMST>();
                lst = GetCutPostMST(dataReq);
                if (lst != null && lst.Count == 0)
                {
                    var req = new AuditPostReq
                    {
                        COMPANY = dataReq.company,
                        SQNO = dataReq.sqno,
                        INPID = dataReq.ucode
                    };
                    InsertAuditPostMST(req);
                }
                    
            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }

            return res;
        }

        private TrackOfflineRes GetTrackOfflineHD(TrackOfflineReq dataReq, TrackOfflineRes res)
        {
            try
            {
                List<TrackHDRes> lstTrackHD = new List<TrackHDRes>();
                lstTrackHD = GetTrackHD(dataReq);

                res.lstTrackHD = lstTrackHD;

                if(lstTrackHD != null && lstTrackHD.Count > 0)
                {
                    res._result._code = "200";
                    res._result._message = "";
                    res._result._status = "OK";
                }
                else
                {
                    res._result._code = "404";
                    res._result._message = "ไม่พบข้อมูล";
                    res._result._status = "Bad Request";
                }
            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }

            return res;
        }

        private TrackOfflineRes getTrackOffline(TrackOfflineReq dataReq, TrackOfflineRes res)
        {
            try
            {
                List<TrackPostMSTRes> lstTrackPostMST = new List<TrackPostMSTRes>();
                List<TrackPostTRNRes> lstTrackPostTRN = new List<TrackPostTRNRes>();

                lstTrackPostMST = GetTrackPostMST(dataReq);
                lstTrackPostTRN = GetTrackPostTRN(dataReq);

                res.lstTrackPostMST = lstTrackPostMST;
                res.lstTrackPostTRN = lstTrackPostTRN;

                if((lstTrackPostMST != null && lstTrackPostMST.Count > 0) || (lstTrackPostTRN != null && lstTrackPostTRN.Count > 0))
                {
                    res._result._code = "200";
                    res._result._message = "";
                    res._result._status = "OK";
                }
                else
                {
                    res._result._code = "404";
                    res._result._message = "ไม่พบข้อมูล";
                    res._result._status = "Bad Request";
                }
            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }

            return res;
        }

        private TrackOfflineRes getSQNO(TrackOfflineReq dataReq, TrackOfflineRes res)
        {
            try
            {
                var obj = GetAUDITCUTDATEMST(dataReq);
                if(obj != null)
                {
                    res.sqno = obj.SQNO;
                    res.yr = obj.YR.ToString();
                    res.mn = obj.MN.ToString();
                    res.yrmn = obj.YRMN.ToString();

                    res._result._code = "200";
                    res._result._message = "";
                    res._result._status = "OK";
                }
                else
                {
                    res._result._code = "404";
                    res._result._message = "ไม่พบข้อมูล";
                    res._result._status = "Bad Request";
                }
            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }

            return res;
        }

        private TrackOfflineRes GetProblem(TrackOfflineReq dataReq, TrackOfflineRes res)
        {
            try
            {
                var obj = GetProblemBase(dataReq);
                if (obj != null)
                {
                    res.problem = obj;

                    res._result._code = "200";
                    res._result._message = "";
                    res._result._status = "OK";
                }
                else
                {
                    res._result._code = "404";
                    res._result._message = "ไม่พบข้อมูล";
                    res._result._status = "Bad Request";
                }

            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }

            return res;
        }

        private TrackOfflineRes addPostMST(TrackOfflineReq dataReq, TrackOfflineRes res)
        {
            try
            {
                var obj = InsertTrackPostMST(dataReq);
                if (obj.Result > 0)
                {
                    res._result._code = "200";
                    res._result._message = "";
                    res._result._status = "OK";
                }
                else
                {
                    res._result._code = "400";
                    res._result._message = "พบความผิดพลาดระหว่างประมวลผลข้อมูล " + obj.Result.ToString();
                    res._result._status = "Bad Request";

                }

                
            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }

            return res;
        }

        private TrackOfflineRes addPostTRN(TrackOfflineReq dataReq, TrackOfflineRes res)
        {
            try
            {
                var obj = InsertTrackPostTRN(dataReq);

                if (obj.Result > 0)
                {
                    res._result._code = "200";
                    res._result._message = "";
                    res._result._status = "OK";
                }
                else
                {
                    res._result._code = "400";
                    res._result._message = "พบความผิดพลาดระหว่างประมวลผลข้อมูล " + obj.Result.ToString();
                    res._result._status = "Bad Request";

                }
            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }

            return res;
        }

        private  TrackOfflineRes auditPostMST(TrackOfflineReq dataReq, TrackOfflineRes res)
        {
            try
            {
                string msg, flag, transy;
                flag = "1";
                transy = "";
                msg = "";

                ASAUDITPOSTMST objMst = new ASAUDITPOSTMST();
                if (String.IsNullOrEmpty(dataReq.sqno))
                {
                    msg = "เลขที่เอกสารไม่ถูกต้อง";
                    res._result._code = "404";
                    res._result._message = msg;
                    res._result._status = "Bad Request";
                }
                else
                {
                     objMst = GetASAUDITPOSTMST(dataReq);
                    if (objMst != null)
                    {
                        if (!String.IsNullOrEmpty(objMst.PCODE))
                        {
                            msg = "ไม่สามารถบันทึกผลการตรวจสอบได้ เนื่องจากรหัสทรัพย์สินนี้เคยถูกบันทึกผลเป็น" + objMst.PCODE + " : " + objMst.PNAME;
                            res._result._code = "501";
                            res._result._message = msg;
                            res._result._status = "Not Implemented";
                        }
                        else
                        {
                            transy = "Y";
                            res._result._code = "200";
                            res._result._message = msg;
                            res._result._status = "OK";
                        }
                    }
                    else
                    {
                        msg = "ไม่พบข้อมูล";
                        res._result._code = "404";
                        res._result._message = msg;
                        res._result._status = "Bad Request";
                    }
                }

                dataReq.flag = flag;
                dataReq.remark = msg;

                UpdateTransferTrackPostMST(dataReq);

                if (!String.IsNullOrEmpty(transy))
                {                   

                    var reqPostMst = new AUDITPOSTMSTReq
                    {
                        SQNO = dataReq.sqno,
                        DEPCODEOL = objMst != null? objMst.DEPCODEOL:null,
                        COMPANY = dataReq.company,
                        LEADERCODE = objMst != null ? objMst.LEADERCODE:null,
                        LEADERNAME = objMst != null ? objMst.LEADERNAME:null,
                        AREACODE = objMst != null ? objMst.AREACODE:null,
                        AREANAME = null,
                        ASSETNO = dataReq.assetno,
                        FINDY = dataReq.findy,
                        PCODE = dataReq.pcode,
                        PNAME = dataReq.pname,
                        UCODE = dataReq.inpid,
                        PFLAG = dataReq.pflag,
                        MEMO1 = dataReq.memo1

                    };

                    if (!String.IsNullOrEmpty(dataReq.transy) && dataReq.transy.ToLower().Equals("y"))
                    {
                       var stSave =  UpdateAuditPostMST(reqPostMst);
                        if (stSave.Result != 0)
                        {
                            flag = "2";
                            msg = "บันทึกผลการตรวจสอบเรียบร้อยแล้ว";
                        }
                        else
                        {
                            msg = "พบข้อผิดพลาดจากการบันทึกผลการตรวจสอบ";
                        }
                    }
                    else
                    {
                        msg = "ไม่บันทึกผลการตรวจสอบ";
                    }

                    dataReq.remark = msg;
                    dataReq.flag = flag;
                    
                }
                dataReq.transy = !String.IsNullOrEmpty(dataReq.transy) ? dataReq.transy.ToUpper() : dataReq.transy;
                UpdateAuditTrackPostMST(dataReq);




            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }

            return res;
        }

        private TrackOfflineRes auditPostTRN(TrackOfflineReq dataReq, TrackOfflineRes res)
        {
            try
            {
                string msg, flag, transy;
                flag = "1";
                transy = "";
                msg = "";

                ASAUDITPOSTTRN objTRN = new ASAUDITPOSTTRN();

                if (String.IsNullOrEmpty(dataReq.sqno))
                {
                    msg = "เลขที่เอกสารไม่ถูกต้อง";
                    res._result._code = "404";
                    res._result._message = msg;
                    res._result._status = "Bad Request";
                }
                else
                {
                    objTRN = GetASAUDITPOSTTRN(dataReq);
                    if (objTRN == null)
                    {
                        transy = "Y";
                        res._result._code = "200";
                        res._result._message = msg;
                        res._result._status = "OK";
                    }
                    else
                    {
                        msg = "ไม่สามารถบันทึกผลการตรวจสอบได้ เนื่องจากรหัสทรัพย์สินนี้เคยถูกบันทึกผลแล้ว";
                        res._result._code = "501";
                        res._result._message = msg;
                        res._result._status = "Not Implemented";
                    }
                }

                dataReq.flag = flag;
                dataReq.remark = msg;

                UpdateTransferTrackPostTRN(dataReq);

                if (!String.IsNullOrEmpty(transy))
                {
                    int yr,mn,yrmn;
                    Int32.TryParse(dataReq.yr, out yr);
                    Int32.TryParse(dataReq.mn, out mn);
                    Int32.TryParse(dataReq.yrmn, out yrmn);

                    var reqPostTrn = new AUDITPOSTTRNReq()
                    {
                        SQNO = dataReq.sqno,
                        COMPANY = dataReq.company,
                        DEPCODEOL = dataReq.depcodeol,
                        YR = yr,
                        MN = mn,
                        YRMN = yrmn,
                        MEMO1 = dataReq.memo1,
                        ASSETNO = dataReq.assetno,
                        ASSETNAME = dataReq.assetname,
                        OFFICECODE = dataReq.officecode,
                        OFNAME = dataReq.ofname,
                        POSITNAME = dataReq.positname,
                        MODE = "ADD",
                        UCODE = dataReq.inpid
                    };
                    

                    if (!String.IsNullOrEmpty(dataReq.transy) && dataReq.transy.ToLower().Equals("y"))
                    {
                        var stSave = UpdateAuditPostTRN(reqPostTrn);
                        if (stSave.Result != 0)
                        {
                            flag = "2";
                            msg = "บันทึกผลการตรวจสอบเรียบร้อยแล้ว";
                        }
                        else
                        {
                            msg = "พบข้อผิดพลาดจากการบันทึกผลการตรวจสอบ";
                        }
                    }
                    else
                    {
                        msg = "ไม่บันทึกผลการตรวจสอบ";
                    }

                    dataReq.remark = msg;
                    dataReq.flag = flag;                    
                }
                dataReq.transy = !String.IsNullOrEmpty(dataReq.transy) ? dataReq.transy.ToUpper() : dataReq.transy;
                UpdateAuditTrackPostTRN(dataReq);
            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }

            return res;
        }



        public  ASAUDITCUTDATEMST GetAUDITCUTDATEMST(TrackOfflineReq dataReq)
        {            
            return  Task.Run(() =>  ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant(conString).getASAUDITCUTDATEMST(dataReq)).Result;
        }

        public ASAUDITPOSTMST GetASAUDITPOSTMST(TrackOfflineReq dataReq)
        {
            return  Task.Run(() => ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant(conString).getASAUDITPOSTMST(dataReq)).Result;
        }

        public ASAUDITPOSTTRN GetASAUDITPOSTTRN(TrackOfflineReq dataReq)
        {
            return  Task.Run(() => ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant(conString).getASAUDITPOSTTRN(dataReq)).Result;
        }

        public  List<TrackPostMSTRes> GetTrackPostMST(TrackOfflineReq dataReq)
        {
            return  Task.Run(() => ASSETKKF_ADO.Mssql.Track.TrackPostMSTAdo.GetInstant(conString).GetData(dataReq)).Result;
        }

        public List<TrackPostTRNRes> GetTrackPostTRN(TrackOfflineReq dataReq)
        {
            return  Task.Run(() => ASSETKKF_ADO.Mssql.Track.TrackPostTRNAdo.GetInstant(conString).GetData(dataReq)).Result;
        }

        public List<TrackHDRes> GetTrackHD(TrackOfflineReq dataReq)
        {
            return Task.Run(() => ASSETKKF_ADO.Mssql.Track.TrackHDAdo.GetInstant(conString).GetData(dataReq)).Result;
        }

        public List<ASAUDITPOSTMST> GetCutPostMST(TrackOfflineReq dataReq)
        {
            var reqMst = new AuditPostReq
            {
                COMPANY = dataReq.company,
                SQNO = dataReq.sqno,
                INPID = dataReq.ucode
            };
            return Task.Run(() => ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant(conString).getAUDITPOSTMST(reqMst)).Result;
        }

        public ProblemList GetProblemBase(TrackOfflineReq dataReq)
        {
            var reqProblem = new STProblemReq
            {
                Company = dataReq.company
            };
            var lstProblem = Task.Run(() => ASSETKKF_ADO.Mssql.Asset.STProblemADO.GetInstant(conString).Search(reqProblem)).Result;
            return lstProblem.FirstOrDefault(); 
        }

        public  Task<int> InsertTrackPostMST(TrackOfflineReq dataReq)
        {
            return  Task.Run(() => ASSETKKF_ADO.Mssql.Track.TrackPostMSTAdo.GetInstant(conString).Insert(dataReq));
        }

        public  Task<int> InsertTrackPostTRN(TrackOfflineReq dataReq)
        {
            return  Task.Run(() => ASSETKKF_ADO.Mssql.Track.TrackPostTRNAdo.GetInstant(conString).Insert(dataReq));
        }

        public  Task<int> UpdateTransferTrackPostMST(TrackOfflineReq dataReq)
        {
            return  Task.Run(() => ASSETKKF_ADO.Mssql.Track.TrackPostMSTAdo.GetInstant(conString).UpdateTransfer(dataReq));
        }

        public Task<int> UpdateAuditTrackPostMST(TrackOfflineReq dataReq)
        {
            return Task.Run(() => ASSETKKF_ADO.Mssql.Track.TrackPostMSTAdo.GetInstant(conString).UpdateAudit(dataReq));
        }

        public  Task<int> UpdateTransferTrackPostTRN(TrackOfflineReq dataReq)
        {
            return  Task.Run(() => ASSETKKF_ADO.Mssql.Track.TrackPostTRNAdo.GetInstant(conString).UpdateTransfer(dataReq));
        }


        public Task<int> UpdateAuditTrackPostTRN(TrackOfflineReq dataReq)
        {
            return Task.Run(() => ASSETKKF_ADO.Mssql.Track.TrackPostTRNAdo.GetInstant(conString).UpdateAudit(dataReq));
        }

        public Task<int> UpdateAuditPostMST(AUDITPOSTMSTReq dataReq)
        {
            return Task.Run(() => ASSETKKF_ADO.Mssql.Audit.AUDITPOSTMSTAdo.GetInstant(conString).saveAUDITPOSTMST(dataReq));
        }

        public Task<int> UpdateAuditPostTRN(AUDITPOSTTRNReq dataReq)
        {
            return Task.Run(() => ASSETKKF_ADO.Mssql.Audit.AUDITPOSTTRNAdo.GetInstant(conString).saveAUDITPOSTTRN(dataReq));
        }

        public Task<int> InsertAuditPostMST(AuditPostReq dataReq)
        {
            return Task.Run(() => ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant(conString).addAUDITPOSTMST(dataReq));

        }
    }
}
