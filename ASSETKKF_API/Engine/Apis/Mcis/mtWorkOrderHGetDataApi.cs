using ASSETKKF_MODEL.Request.Mcis;
using ASSETKKF_MODEL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Apis.Mcis
{
    public class mtWorkOrderHGetDataApi : Base< mtWorkOrderHReq >
    {
        public mtWorkOrderHGetDataApi()
        {
            PermissionKey = "ADMIN";

        }

        protected override void ExecuteChild( mtWorkOrderHReq  dataReq, ResponseAPI dataRes)
        {
            var tmp = new ASSETKKF_MODEL.Response.Mcis.mtWorkOrderHRes();
            var res = new List<ASSETKKF_MODEL.Response.Mcis.mtWorkOrderHRes>();
            try
            {
                var roles = ASSETKKF_ADO.Mssql.Mcis.mtWorkOrderHAdo.GetInstant().GetData(dataReq);

                if (roles.Count == 0)
                {
                    tmp = new ASSETKKF_MODEL.Response.Mcis.mtWorkOrderHRes();
                    tmp.WorkOrderID = dataReq.WorkOrderID;
                    tmp._result._status = "F";
                    tmp._result._code = "F0002";
                    tmp._result._message = "ไม่พบข้อมูล ใบสั่งผลิต." + dataReq.WorkOrderID;

                    res.Add(tmp);
                }
                else
                {
                    foreach (var x in roles)
                    {
                        tmp = new ASSETKKF_MODEL.Response.Mcis.mtWorkOrderHRes();

                        tmp.WorkOrderID = x.WorkOrderID;
                        tmp.MchProjectID = x.MchProjectID;
                        tmp.DrawingCd = x.DrawingCd;
                        tmp.WorkOrderDesc = x.WorkOrderDesc;
                        tmp.QtyAmt = x.QtyAmt;
                        tmp.FMDeptCd = x.FMDeptCd;
                        tmp.CreatedDate = x.CreatedDate;
                        tmp.WorkOrderStatus = x.WorkOrderStatus;
                        tmp.FstRcvrWs = x.FstRcvrWs;
                        tmp.PrintFlg = x.PrintFlg;
                        tmp.AmtUnit = x.AmtUnit;
                        tmp.DocListNo = x.DocListNo;
                        tmp.MainWS = x.MainWS;
                        tmp.SendStock = x.SendStock;
                        tmp.USER_ID = x.USER_ID;
                        tmp.USER_DATE = x.USER_DATE;
                        tmp.Date_Open = x.Date_Open;
                        tmp.PDate_Cls = x.PDate_Cls;
                        tmp.RDate_cls = x.RDate_cls;
                        tmp.CreateSPAuto = x.CreateSPAuto;
                        tmp.DM_COST = x.DM_COST;
                        tmp.DM_FOH = x.DM_FOH;
                        tmp.SEQUENCE_COST = x.SEQUENCE_COST;
                        tmp.SEQUENCE_PROFIT = x.SEQUENCE_PROFIT;
                        tmp.TOTPRC = x.TOTPRC;
                        tmp.TOTCOST = x.TOTCOST;
                        tmp.DM_COST_VERSION = x.DM_COST_VERSION;
                        tmp.DEL_FLAG = x.DEL_FLAG;
                        tmp.MATDAYWANT = x.MATDAYWANT;
                        tmp.SEQUENCE_ACT = x.SEQUENCE_ACT;
                        tmp.TOTCOST_ACT = x.TOTCOST_ACT;
                        tmp.SEQUENCE_DIF = x.SEQUENCE_DIF;
                        tmp.PROFIT_BEG = x.PROFIT_BEG;
                        tmp.STATION_GRP = x.STATION_GRP;
 

                        res.Add(tmp);
                    }
                }
            }
            catch (Exception)
            {
                tmp = new ASSETKKF_MODEL.Response.Mcis.mtWorkOrderHRes();
                tmp.WorkOrderID = dataReq.WorkOrderID;
                tmp._result._status = "F";
                tmp._result._code = "F0001";
                tmp._result._message = "การเชื่อมต่อฐานข้อมูลมีปัญหา...";

                res.Add(tmp);
            }
            dataRes.data = res;
        }
    }
}
