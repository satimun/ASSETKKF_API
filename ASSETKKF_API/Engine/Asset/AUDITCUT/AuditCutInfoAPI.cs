using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_ADO.Mssql.Asset;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Engine.Asset.AUDITCUT
{
    public class AuditCutInfoAPI : Base<AuditCutInfoReq>
    {
        public AuditCutInfoAPI(IConfiguration configuration)
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
                var objDEPTList = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant(conString).getDeptLst(dataReq);
                var objLeaderList = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant(conString).getLeaderCentralLst(dataReq);
                var objDepLikeList = ASSETKKF_ADO.Mssql.Asset.AuditCutADO.GetInstant(conString).getDepLikeList(dataReq);
                var objPOSITASSETLst = STPOSITASSETADO.GetInstant(conString).GetSTPOSITASSETLists(objDepLikeList, dataReq.Company);

                res.auditCutDEPTList = objDEPTList;
                res.auditCutLeaderList = objLeaderList;
                res.deplkList = objDepLikeList;
                res.STPOSITASSETLst = objPOSITASSETLst;

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
