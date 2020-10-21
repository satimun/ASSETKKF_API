using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_MODEL.Request.Report;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Report;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Engine.Asset.Report
{
    public class RptAuditAssetAPI : Base<RptAuditAssetReq>
    {
        public RptAuditAssetAPI(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }

        protected override void ExecuteChild(RptAuditAssetReq dataReq, ResponseAPI dataRes)
        {
            var res = new RptAuditAssetRes();
            try
            {
                DBMode = dataReq.DBMode;
                res._result.ServerAddr = ConnectionString();
                List<ASSETKKF_MODEL.Response.Report.RptAuditAsset> obj = new List<RptAuditAsset>();

                var mode = String.IsNullOrEmpty(dataReq.MODE) ? dataReq.MODE : dataReq.MODE.ToLower();

                switch (mode)
                {
                    case "main":
                        obj = ASSETKKF_ADO.Mssql.Asset.RptAuditAssetADO.GetInstant(conString).GetAuditAssetMainLists(dataReq);
                        break;

                    default:
                        obj = ASSETKKF_ADO.Mssql.Asset.RptAuditAssetADO.GetInstant(conString).GetAuditAssetLists(dataReq);
                        break;
                }               


                var objTRN = ASSETKKF_ADO.Mssql.Asset.RptAuditAssetADO.GetInstant(conString).GetAuditAssetTRNLists(dataReq);



                if (obj == null && objTRN == null)
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

                res.auditAssetLst = obj;
                res.auditAssetTRNLst = objTRN;
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
