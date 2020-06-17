using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_MODEL.Request.Report;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Report;

namespace ASSETKKF_API.Engine.Asset.Report
{
    public class AuditProblemsDeptAPI : Base<AuditProblemsReq>
    {
        public AuditProblemsDeptAPI()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(AuditProblemsReq dataReq, ResponseAPI dataRes)
        {
            var res = new AuditProblemsDeptRes();
            try
            {
                var obj = ASSETKKF_ADO.Mssql.Asset.AuditProblemsADO.GetInstant().GetDeptSummary(dataReq).ToList();

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
