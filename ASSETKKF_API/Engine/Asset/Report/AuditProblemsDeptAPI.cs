﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_MODEL.Request.Report;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Report;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Engine.Asset.Report
{
    public class AuditProblemsDeptAPI : Base<AuditProblemsReq>
    {
        public AuditProblemsDeptAPI(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }

        protected override void ExecuteChild(AuditProblemsReq dataReq, ResponseAPI dataRes)
        {
            var res = new AuditProblemsDeptRes();
            try
            {
                DBMode = dataReq.DBMode;
                res._result.ServerAddr = ConnectionString();
                var obj = ASSETKKF_ADO.Mssql.Asset.AuditProblemsADO.GetInstant(conString).GetDeptSummary(dataReq).ToList();

                if (obj == null)
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

                res.auditProblemLst = obj;

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
