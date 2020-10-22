using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_ADO.Mssql.Audit;
using ASSETKKF_MODEL.Data.Mssql.Audit;
using ASSETKKF_MODEL.Request.Audit;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Audit;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Engine.Asset.AUDITACC
{
    public class AuditAccApi : Base<AuditResultReq>
    {
        public AuditAccApi(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }

        protected override void ExecuteChild(AuditResultReq dataReq, ResponseAPI dataRes)
        {
            var res = new AuditAccRes();
            try
            {
                DBMode = dataReq.DBMode;
                res._result.ServerAddr = ConnectionString();
                res._result.DBMode = DBMode;

                List<AuditAcc> auditLst = new List<AuditAcc>();
                var mode = String.IsNullOrEmpty(dataReq.MODE) ? dataReq.MODE : dataReq.MODE.ToLower();
                switch (mode)
                {
                    default:
                        auditLst = AuditAccAdo.GetInstant().GetData(dataReq,null,conString);
                        break;
                }

                res.AuditAccLst = auditLst;

                if (auditLst == null)
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
