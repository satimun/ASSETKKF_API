using Core.Util;
using ASSETKKF_API.Constant;
using ASSETKKF_MODEL.Request.Oauth;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Oauth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response.Asset;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Engine.Asset.AUDITCUT
{
    public class AuditCutAPI :  Base<AuditCutReq>
    {
        public AuditCutAPI(IConfiguration configuration)
    {
        AllowAnonymous = true;
        RecaptchaRequire = true;
        Configuration = configuration;
    }

    protected override void ExecuteChild(AuditCutReq dataReq, ResponseAPI dataRes)
    {
        var res = new AuditCutListRes();
            try
            {
                DBMode = dataReq.DBMode;
                res._result.ServerAddr = ConnectionString();
                res._result.DBMode = DBMode;

                var obj = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant().GetAuditCutLists(dataReq,null, conString);
                if (obj == null)
                {
                    res._result._code = "404";
                    res._result._message = "ไม่พบข้อมูล";
                    res._result._status = "Not Found";

                }
                else
                {
                    res._result._code = "200";
                    res._result._message = "";
                    res._result._status = "OK";
                }

                res.auditCutNoLst = obj;
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
