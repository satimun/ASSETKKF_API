﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Asset;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Engine.Asset.Dashboard
{
    public class InspectionApi : Base<AuditSummaryReq>
    {
        public InspectionApi(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }

        protected override void ExecuteChild(AuditSummaryReq dataReq, ResponseAPI dataRes)
        {
            var res = new InspectionRes();
            try
            {
                DBMode = dataReq.DBMode;
                res._result.ServerAddr = ConnectionString();
                res._result.DBMode = DBMode;

                List<DashboardInspection> lst = new List<DashboardInspection>();
                var mode = String.IsNullOrEmpty(dataReq.inspection) ? null : dataReq.inspection.Trim().ToLower();

                switch (mode)
                {
                    case "depcodeol":
                        lst = ASSETKKF_ADO.Mssql.Asset.DashboardADO.GetInstant().getInspectionByDEPCODEOL(dataReq, null, conString);
                        break;

                    case "officecode":
                        lst = ASSETKKF_ADO.Mssql.Asset.DashboardADO.GetInstant().getInspectionByOFFICECODE(dataReq, null, conString);
                        break;
                    case "typecode":
                        lst = ASSETKKF_ADO.Mssql.Asset.DashboardADO.GetInstant().getInspectionByTYPECODE(dataReq, null, conString);
                        break;
                    case "gastcode":
                        lst = ASSETKKF_ADO.Mssql.Asset.DashboardADO.GetInstant().getInspectionByGASTCODE(dataReq, null, conString);
                        break;

                    case "assetno":
                        lst = ASSETKKF_ADO.Mssql.Asset.DashboardADO.GetInstant().getInspectionByASSETNO(dataReq, null, conString);
                        break;
                    case "auditofficecode":
                        lst = ASSETKKF_ADO.Mssql.Asset.DashboardADO.GetInstant().getAuditOFFICECODE(dataReq, null, conString);
                        break;

                    case "getpivotdept":
                        var obj = ASSETKKF_ADO.Mssql.Report.PivotDataAdo.GetInstant().getProblemByDep(dataReq,null,  conString);
                        break;


                    default:
                        lst = ASSETKKF_ADO.Mssql.Asset.DashboardADO.GetInstant().getInspectionByDEPMST(dataReq, null, conString);
                        break;
                }
                

                if (lst == null)
                {
                    res._result._code = "404";
                    res._result._message = "ไม่พบข้อมูล";
                    res._result._status = "Bad Request";
                }
                else
                {

                    res._result._code = "200";
                    res._result._message = "";
                    res._result._status = "OK";

                }

                res.DashboardInspectionLST = lst;

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
