using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_ADO.Mssql.Asset;
using ASSETKKF_ADO.Mssql.Audit;
using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Data.Mssql.Audit;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Audit;

namespace ASSETKKF_API.Engine.Asset.AUDITSEND
{
    public class AuditResultApi : Base<TaskAudit>
    {
        public AuditResultApi()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(TaskAudit dataReq, ResponseAPI dataRes)
        {
            var res = new AuditDuplicateRes();

            try
            {
                List<AuditResult> auditLst = new List<AuditResult>();

                var mode = String.IsNullOrEmpty(dataReq.MODE) ? dataReq.MODE : dataReq.MODE.ToLower();

                switch (mode)
                {
                    

                    default:
                        auditLst = AuditResultAdo.GetInstant().GetData(dataReq);
                        break;
                }

                res.AuditResultLst = auditLst;

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
