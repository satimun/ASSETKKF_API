using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Report;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Engine.Asset.Report
{
    public class AuditSummaryApi : Base<AuditSummaryReq>
    {
        public AuditSummaryApi(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }

        protected override void ExecuteChild(AuditSummaryReq dataReq, ResponseAPI dataRes)
        {
            RptAuditSummaryRes res = new RptAuditSummaryRes();
            try
            {
                DBMode = dataReq.DBMode;
                res._result.ServerAddr = ConnectionString();
                res._result.DBMode = DBMode;

                var mode = String.IsNullOrEmpty(dataReq.mode) ? null : dataReq.mode.Trim().ToLower();

                switch (mode)
                {
                    case "depmst":
                        GetAuditSummaryByDepmst(dataReq, res, conString);
                        break;

                    case "depcodeol":
                        GetAuditSummaryByDepcodeol(dataReq, res, conString);
                        break;

                    default:
                        GetAuditSummary(dataReq, res, conString);
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

        private RptAuditSummaryRes GetAuditSummary(AuditSummaryReq dataReq, RptAuditSummaryRes res, string conStr = null)
        {
            try
            {                
                var objFIXEDASSET = ASSETKKF_ADO.Mssql.Report.AuditSummaryAdo.GetInstant().getFIXEDASSET(dataReq, null, conString);
                var lstASSETOWNERLst = ASSETKKF_ADO.Mssql.Report.AuditSummaryAdo.GetInstant().getASSETOWNER(dataReq, null, conString);
                var lstRANKTOP3 = ASSETKKF_ADO.Mssql.Report.AuditSummaryAdo.GetInstant().getRANKDEPMSTTOP3(dataReq, null, conString);
                var lstMONTHDEPMST = ASSETKKF_ADO.Mssql.Report.AuditSummaryAdo.GetInstant().getMONTHDEPMST(dataReq, null, conString);

                res.SummaryFIXEDASSET = objFIXEDASSET;
                res.SummaryASSETOWNERLst = lstASSETOWNERLst;
                res.SummaryRANKDEPMSTTOP3Lst = lstRANKTOP3;
                res.SummaryMONTHDEPMSTLst = lstMONTHDEPMST;

                if (objFIXEDASSET != null || lstASSETOWNERLst.Count > 0 || lstRANKTOP3.Count > 0 || lstMONTHDEPMST.Count > 0)
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

        private RptAuditSummaryRes GetAuditSummaryByDepmst(AuditSummaryReq dataReq, RptAuditSummaryRes res, string conStr = null)
        {
            try
            {
                var objFIXEDASSET = ASSETKKF_ADO.Mssql.Report.AuditSummaryAdo.GetInstant().getFIXEDASSET(dataReq, null, conString);
                var lstASSETOWNERLst = ASSETKKF_ADO.Mssql.Report.AuditSummaryAdo.GetInstant().getASSETOWNER(dataReq, null, conString);
                var lstRANKTOP3 = ASSETKKF_ADO.Mssql.Report.AuditSummaryAdo.GetInstant().getRANKDEPCODEOLTOP3(dataReq, null, conString);
                var lstMONTHDEPCODEOL = ASSETKKF_ADO.Mssql.Report.AuditSummaryAdo.GetInstant().getMONTHDEPCODEOL(dataReq, null, conString);

                res.SummaryFIXEDASSET = objFIXEDASSET;
                res.SummaryASSETOWNERLst = lstASSETOWNERLst;
                res.SummaryRANKDEPCODEOLTOP3Lst = lstRANKTOP3;
                res.SummaryMONTHDEPCODEOLLst= lstMONTHDEPCODEOL;

                if (objFIXEDASSET != null || lstASSETOWNERLst.Count > 0 || lstRANKTOP3.Count > 0 || lstMONTHDEPCODEOL.Count > 0 )
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


        private RptAuditSummaryRes GetAuditSummaryByDepcodeol(AuditSummaryReq dataReq, RptAuditSummaryRes res, string conStr = null)
        {
            try
            {
                var lstMONTHOFFICECODE = ASSETKKF_ADO.Mssql.Report.AuditSummaryAdo.GetInstant().getMONTHOFFICECODE(dataReq, null, conString);
                res.SummaryMONTHOFFICECODELst = lstMONTHOFFICECODE;

                if (lstMONTHOFFICECODE.Count > 0)
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

    }
}
