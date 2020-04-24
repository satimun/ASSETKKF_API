using ASSETKKF_MODEL.Request.Mcis;
using ASSETKKF_MODEL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Apis.Mcis
{
    public class ViewWorkOrderDGetDataApi : Base<ViewWorkOrderDReq>
    {
        public ViewWorkOrderDGetDataApi()
        {
            PermissionKey = "ADMIN";

        }

        protected override void ExecuteChild(ViewWorkOrderDReq dataReq, ResponseAPI dataRes)
        {
            var tmp = new ASSETKKF_MODEL.Response.Mcis.ViewWorkOrderDRes();
            var res = new List<ASSETKKF_MODEL.Response.Mcis.ViewWorkOrderDRes>();
            try
            {

                var roles = ASSETKKF_ADO.Mssql.Mcis.ViewWorkOrderDAdo.GetInstant().GetData(dataReq);
                if (roles.Count == 0)
                {
                    tmp = new ASSETKKF_MODEL.Response.Mcis.ViewWorkOrderDRes();
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
                        tmp = new ASSETKKF_MODEL.Response.Mcis.ViewWorkOrderDRes();

                        
                        tmp.WorkOrderID = x.WorkOrderID;
                        tmp.MatItemCd = x.MatItemCd;
                        tmp.MatType = x.MatType;
                        tmp.MatUnitCd = x.MatUnitCd;
                        tmp.MatSize1 = x.MatSize1;
                        tmp.MatSize2 = x.MatSize2;
                        tmp.MatQty = x.MatQty;
                        tmp.CrtReqDocFlg = x.CrtReqDocFlg;
                        tmp.AppvNo = x.AppvNo;
                        tmp.AppvSeqNo = x.AppvSeqNo;
                        tmp.SumQty = x.SumQty;
                        tmp.PUR_FLAG = x.PUR_FLAG;
                        tmp.NOTPUR_FLAG = x.NOTPUR_FLAG;
                        tmp.CALCOST_FLAG = x.CALCOST_FLAG;
                        tmp.NOTCALCOST_FLAG = x.NOTCALCOST_FLAG;
                        tmp.USER_ID = x.USER_ID;
                        tmp.USER_DATE = x.USER_DATE;

                        tmp.QtyAmt = x.QtyAmt;
                        tmp.ItemCode = x.ItemCode;
                        tmp.ItemName = x.ItemName;

                        res.Add(tmp);
                    }
                }
            }
            catch (Exception)
            {
                tmp = new ASSETKKF_MODEL.Response.Mcis.ViewWorkOrderDRes();
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
