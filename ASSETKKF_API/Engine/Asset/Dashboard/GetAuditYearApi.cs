﻿using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Asset.Dashboard
{
    public class GetAuditYearApi : Base<AuditSummaryReq>
    {
        public GetAuditYearApi()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(AuditSummaryReq dataReq, ResponseAPI dataRes)
        {
            AuditYearRes res = new AuditYearRes();
            try
            {

                var obj = ASSETKKF_ADO.Mssql.Asset.AuditSummaryADO.GetInstant().GetAuditYear(dataReq);
                if (obj == null)
                {
                    res._result._code = "404";
                    res._result._message = "ไม่พบข้อมูล";
                    res._result._status = "Bad Request";
                }
                else
                {


                    res.AuditYearLST = obj;

                    res._result._code = "200";
                    res._result._message = "";
                    res._result._status = "OK";

                }


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
