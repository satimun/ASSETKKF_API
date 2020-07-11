﻿using ASSETKKF_ADO.Mssql.Asset;
using ASSETKKF_MODEL.Request.Report;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Asset.Report
{
    public class AuditCUTDTApi : Base<RptAuditAssetReq>
    {
        public AuditCUTDTApi()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(RptAuditAssetReq dataReq, ResponseAPI dataRes)
        {
            AuditCutDTRes res = new AuditCutDTRes();
            try
            {

                var obj = ASSETKKF_ADO.Mssql.Asset.RptAuditAssetADO.GetInstant().GetAuditCUTDT(dataReq);
                if (obj == null)
                {
                    res._result._code = "404";
                    res._result._message = "ไม่พบข้อมูล";
                    res._result._status = "Bad Request";
                }
                else
                {


                    res.AuditCutDTLST = obj;

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