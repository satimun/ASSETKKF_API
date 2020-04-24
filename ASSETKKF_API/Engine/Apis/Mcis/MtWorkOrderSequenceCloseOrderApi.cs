using ASSETKKF_MODEL.Request.Mcis;
using ASSETKKF_MODEL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Apis.Mcis
{
    public class MtWorkOrderSequenceCloseOrderApi : Base<MtWorkOrderSequenceCloseOrderReq>
    {
        public MtWorkOrderSequenceCloseOrderApi()
        {
            PermissionKey = "ADMIN"; 

        }

        protected override void ExecuteChild(MtWorkOrderSequenceCloseOrderReq dataReq, ResponseAPI dataRes)
        {
            var res = new List<ASSETKKF_MODEL.Response.Mcis.MtWorkOrderSequenceCloseOrderRes>();
            var tmp = new ASSETKKF_MODEL.Response.Mcis.MtWorkOrderSequenceCloseOrderRes();
            try
            {
                var roles = ASSETKKF_ADO.Mssql.Mcis.MtWorkOrderSequence_EmpAdo.GetInstant().CloseOrder(dataReq);
                if (roles.Count == 0)
                {
                    tmp = new ASSETKKF_MODEL.Response.Mcis.MtWorkOrderSequenceCloseOrderRes();
                    tmp.EmployeeId = dataReq.EmployeeId;
                    tmp.WorkOrderID = dataReq.WorkOrderID;
                    tmp.WorkStationGrpCd= dataReq.WorkStationGrpCd;
                    tmp._result._status = "F";
                    tmp._result._code = "F0002";
                    tmp._result._message = "ไม่พบข้อมูล ปิดงานใบสั่งผลิต" + dataReq.WorkOrderID;

                    res.Add(tmp);
                }
                else
                {

                    foreach (var x in roles)
                    {
                        tmp = new ASSETKKF_MODEL.Response.Mcis.MtWorkOrderSequenceCloseOrderRes();
                      
                        tmp.EmployeeId     = x.EmployeeId;
                        tmp.WorkStationGrpCd = x.WorkStationGrpCd;
                        tmp.WorkOrderID    = x.WorkOrderID;
                        tmp.AddEmployeeId  = x.AddEmployeeId;
                        tmp.User_Id        = x.User_Id;
                        tmp.USERAPP        = x.USERAPP;
                        tmp.CloseChoose    = x.CloseChoose;
                        tmp.EndSequence    = x.EndSequence;
                        tmp.DateSelect     = x.DateSelect;
                        tmp.TimeSelect     = x.TimeSelect;
                        tmp.DateSelectFull = x.DateSelectFull;

                        tmp._result._status  = x._status;
                        tmp._result._code    = x._code;
                        tmp._result._message = x._message;

                        res.Add(tmp);
                    }
                }
            }
            catch (Exception ex)
            {
                tmp = new ASSETKKF_MODEL.Response.Mcis.MtWorkOrderSequenceCloseOrderRes();
                tmp.EmployeeId = dataReq.EmployeeId;
                tmp.WorkOrderID = dataReq.WorkOrderID;
                tmp.WorkStationGrpCd = dataReq.WorkStationGrpCd;

                tmp._result._status = "F";
                tmp._result._code = "F0001";
                tmp._result._message = "การเชื่อมต่อฐานข้อมูลมีปัญหา..."+ ex.Message;

                res.Add(tmp);
            }
            dataRes.data = res;
        }
    }
}
