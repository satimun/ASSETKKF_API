using ASSETKKF_MODEL.Request.Mcis;
using ASSETKKF_MODEL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Apis.Mcis
{
    public class MtWorkOrderSequenceLGetDataApi : Base<MtWorkOrderSequenceLReq>
    {
        public MtWorkOrderSequenceLGetDataApi()
        {
            PermissionKey = "ADMIN";

        }

        protected override void ExecuteChild(MtWorkOrderSequenceLReq dataReq, ResponseAPI dataRes)
        {
            var res = new List<ASSETKKF_MODEL.Response.Mcis.MtWorkOrderSequenceLRes>();
            var tmp = new ASSETKKF_MODEL.Response.Mcis.MtWorkOrderSequenceLRes();
            
            try
            { 
                var roles = ASSETKKF_ADO.Mssql.Mcis.MtWorkOrderSequenceLAdo.GetInstant().GetData(dataReq);
                if (roles.Count == 0)
                {
                    tmp = new ASSETKKF_MODEL.Response.Mcis.MtWorkOrderSequenceLRes();
                    tmp.WorkOrderId = dataReq.WorkOrderId;
                    tmp._result._status = "F";
                    tmp._result._code = "F0002";
                    tmp._result._message = "ไม่พบข้อมูล ใบสั่งผลิต." ;

                    res.Add(tmp);
                }
                else
                {
                    foreach (var x in roles)
                    {
                        tmp = new ASSETKKF_MODEL.Response.Mcis.MtWorkOrderSequenceLRes();

                        tmp.WorkDate = x.WorkDate;
                        tmp.WorkStationGrpCd = x.WorkStationGrpCd;
                        tmp.EmployeeId = x.EmployeeId;
                        tmp.WorkOrderId = x.WorkOrderId;
                        tmp.EndTime = x.EndTime;
                        tmp.QtyAmt = x.QtyAmt;
                        tmp.DrawingCd = x.DrawingCd;
                        tmp.CusCod = x.CusCod;
                        tmp.Move_Flag = x.Move_Flag;
                        tmp.ReworkFlag = x.ReworkFlag;
                        tmp.WorkStationGrp_STD = x.WorkStationGrp_STD;
                        tmp.SeqNo_STD = x.SeqNo_STD;
                        tmp.WorkStationGrpCd_Next = x.WorkStationGrpCd_Next;
                        tmp.SeqNo_NEXT = x.SeqNo_NEXT;
                        tmp.CauseStatus = x.CauseStatus;
                        tmp.RemarkCause = x.RemarkCause;
                        tmp.AppvCauseId = x.AppvCauseId;
                        tmp.AppvCauseDt = x.AppvCauseDt;
                        tmp.User_Id = x.User_Id;
                        tmp.User_date = x.User_date;
                        tmp.ExtFlag = x.ExtFlag;
                        tmp.EndSequence = x.EndSequence;

                        res.Add(tmp);
                    }
                }
            }
            catch (Exception)
            {
                tmp = new ASSETKKF_MODEL.Response.Mcis.MtWorkOrderSequenceLRes();
                tmp.WorkOrderId = dataReq.WorkOrderId;
                tmp._result._status = "F";
                tmp._result._code = "F0001";
                tmp._result._message = "การเชื่อมต่อฐานข้อมูลมีปัญหา...";

                res.Add(tmp);
            }
            dataRes.data = res;
        }
    }
}
