using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Home;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Engine.Asset.Home
{
    public class TaskAuditApi : Base<TaskAudit>
    {
        public TaskAuditApi(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }

        protected override void ExecuteChild(TaskAudit dataReq, ResponseAPI dataRes)
        {
            var res = new TaskAuditRes();

            try
            {
                DBMode = dataReq.DBMode;
                res._result.ServerAddr = ConnectionString();
                res._result.DBMode = DBMode;

                List<ASSETKKF_MODEL.Data.Mssql.Asset.TaskAudit> auditLst = new List<TaskAudit>();

                var mode = String.IsNullOrEmpty(dataReq.MODE) ? dataReq.MODE : dataReq.MODE.ToLower();

                switch (mode)
                {
                    case "tracking":
                        auditLst = ASSETKKF_ADO.Mssql.Asset.TaskAuditAdo.GetInstant().GetTracking(dataReq,null,conString);
                        break;

                    default:
                        auditLst = ASSETKKF_ADO.Mssql.Asset.TaskAuditAdo.GetInstant().GetData(dataReq,null,conString);
                        break;
                }

                res.TaskAuditLST = auditLst;

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
