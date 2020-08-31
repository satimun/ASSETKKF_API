using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Home;

namespace ASSETKKF_API.Engine.Asset.Home
{
    public class AuditCancelApi : Base<AsFixedAsset>
    {
        public AuditCancelApi()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(AsFixedAsset dataReq, ResponseAPI dataRes)
        {
            var res = new TaskAuditRes();

            try
            {
                var state = ASSETKKF_ADO.Mssql.Asset.TaskAuditAdo.GetInstant().AuditCancel(dataReq);

                var taskReq = new TaskAudit()
                {
                    INPID = dataReq.INPID,
                    COMPANY = dataReq.COMPANY
                };
                var lst = ASSETKKF_ADO.Mssql.Asset.TaskAuditAdo.GetInstant().GetData(taskReq);

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

                res.TaskAuditLST = lst;

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
