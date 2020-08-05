using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Asset;

namespace ASSETKKF_API.Engine.Asset.Dashboard
{
    public class InspectionApi : Base<AuditSummaryReq>
    {
        public InspectionApi()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(AuditSummaryReq dataReq, ResponseAPI dataRes)
        {
            var res = new InspectionRes();
            try
            {
                List<DashboardInspection> lst = new List<DashboardInspection>();
                var mode = String.IsNullOrEmpty(dataReq.inspection) ? null : dataReq.inspection.Trim().ToLower();

                switch (mode)
                {
                    case "depcodeol":
                        lst = ASSETKKF_ADO.Mssql.Asset.DashboardADO.GetInstant().getInspectionByDEPCODEOL(dataReq);
                        break;

                    case "officecode":
                        lst = ASSETKKF_ADO.Mssql.Asset.DashboardADO.GetInstant().getInspectionByOFFICECODE(dataReq);
                        break;
                    case "typecode":
                        lst = ASSETKKF_ADO.Mssql.Asset.DashboardADO.GetInstant().getInspectionByTYPECODE(dataReq);
                        break;
                    case "gastcode":
                        lst = ASSETKKF_ADO.Mssql.Asset.DashboardADO.GetInstant().getInspectionByGASTCODE(dataReq);
                        break;

                    case "assetno":
                        lst = ASSETKKF_ADO.Mssql.Asset.DashboardADO.GetInstant().getInspectionByASSETNO(dataReq);
                        break;


                    default:
                        lst = ASSETKKF_ADO.Mssql.Asset.DashboardADO.GetInstant().getInspectionByDEPMST(dataReq);
                        break;
                }

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

                res.DashboardInspectionLST = lst;

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
