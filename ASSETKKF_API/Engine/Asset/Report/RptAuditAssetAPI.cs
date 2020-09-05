using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_MODEL.Request.Report;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Report;

namespace ASSETKKF_API.Engine.Asset.Report
{
    public class RptAuditAssetAPI : Base<RptAuditAssetReq>
    {
        public RptAuditAssetAPI()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(RptAuditAssetReq dataReq, ResponseAPI dataRes)
        {
            var res = new RptAuditAssetRes();
            try
            {
                List<ASSETKKF_MODEL.Response.Report.RptAuditAsset> obj = new List<RptAuditAsset>();

                var mode = String.IsNullOrEmpty(dataReq.MODE) ? dataReq.MODE : dataReq.MODE.ToLower();

                switch (mode)
                {
                    case "main":
                        obj = ASSETKKF_ADO.Mssql.Asset.RptAuditAssetADO.GetInstant().GetAuditAssetMainLists(dataReq);
                        break;

                    default:
                        obj = ASSETKKF_ADO.Mssql.Asset.RptAuditAssetADO.GetInstant().GetAuditAssetLists(dataReq);
                        break;
                }               


                var objTRN = ASSETKKF_ADO.Mssql.Asset.RptAuditAssetADO.GetInstant().GetAuditAssetTRNLists(dataReq);



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
