﻿using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Asset;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Asset.Dashboard
{
    public class AuditDeptSummaryApi : Base<AuditSummaryReq>
    {
        public AuditDeptSummaryApi(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }

        protected override void ExecuteChild(AuditSummaryReq dataReq, ResponseAPI dataRes)
        {
            AuditDeptSummaryRes res = new AuditDeptSummaryRes();
            try
            {
                DBMode = dataReq.DBMode;
                res._result.ServerAddr = ConnectionString();
                res._result.DBMode = DBMode;
                var obj = ASSETKKF_ADO.Mssql.Asset.AuditSummaryADO.GetInstant().GetDEPMSTSummary(dataReq, null, conString);
                if (obj == null)
                {
                    res._result._code = "404";
                    res._result._message = "ไม่พบข้อมูล";
                    res._result._status = "Bad Request";
                }
                else
                {
                   

                    res.AuditSummaryLST = obj;

                    res._result._code = "200";
                    res._result._message = "";
                    res._result._status = "OK";

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
    }
}
