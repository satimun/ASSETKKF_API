using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Engine.Asset.AUDITCUT
{
    public class CentralOfficerApi : Base<AuditCutInfoReq>
    {
        public CentralOfficerApi(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }

        protected override void ExecuteChild(AuditCutInfoReq dataReq, ResponseAPI dataRes)
        {
            var res = new ASSETKKF_MODEL.Response.Asset.AuditCutInfoRes();
            try
            {
                DBMode = dataReq.DBMode;
                res._result.ServerAddr = ConnectionString();
                var objLeaderList = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant(conString).getCentralOfficerLst(dataReq);
                res.auditCutLeaderList = objLeaderList;

                ASSETOFFICECODEReq reqASSETOFFICECODE = new ASSETOFFICECODEReq()
                {
                    COMPANY = dataReq.Company
                };

                ASSETASSETNOReq reqASSETASSETNO = new ASSETASSETNOReq()
                {
                    COMPANY = dataReq.Company
                };


                //var lstASSETOFFICECODE = ASSETKKF_ADO.Mssql.Asset.AUDITPOSTTRNADO.GetInstant().getASSETOFFICECODELST(reqASSETOFFICECODE);
                var lstASSETASSETNO = ASSETKKF_ADO.Mssql.Asset.AUDITPOSTTRNADO.GetInstant(conString).getASSETASSETNOLST(reqASSETASSETNO);

                res.ASSETASSETNOLST = lstASSETASSETNO;

                res._result._code = "200";
                res._result._message = "";
                res._result._status = "OK";
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
