using ASSETKKF_MODEL.Request.Mcis;
using ASSETKKF_MODEL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Apis.Mcis
{
    public class MtMoveOrderSequenceInsertMoveApi : Base< MtMoveOrderSequenceReq >
    {
        public MtMoveOrderSequenceInsertMoveApi()
        {
            PermissionKey = "ADMIN";

        }

        protected override void ExecuteChild( MtMoveOrderSequenceReq  dataReq, ResponseAPI dataRes)
        {
            var tmp = new ASSETKKF_MODEL.Response.Mcis.MtMoveOrderSequenceRes();
            var res = new List<ASSETKKF_MODEL.Response.Mcis.MtMoveOrderSequenceRes>();

            var restmp = new ASSETKKF_MODEL.Response.Mcis.MtMoveOrderSequenceRes();


            try
            {
                var roles = ASSETKKF_ADO.Mssql.Mcis.MtMoveOrderSequenceAdo.GetInstant().InsertMove(dataReq);
                if (roles.Count == 0)
                {
                    tmp = new ASSETKKF_MODEL.Response.Mcis.MtMoveOrderSequenceRes();
                    tmp.EmployeeId = dataReq.EmployeeId;
                    tmp.WorkStationGrpCd_TO = dataReq.WorkStationGrpCd_TO;
                    tmp.WorkOrderId = dataReq.WorkOrderId;
                    tmp.AppvCauseId = dataReq.AppvCauseId;
                    tmp._result._status = "F";
                    tmp._result._code = "F0002";
                    tmp._result._message = "บันทึกไม่สำเร็จ ." + dataReq.WorkOrderId;

                    res.Add(tmp);
                }
                {
                    foreach (var x in roles)
                    {
                        tmp = new ASSETKKF_MODEL.Response.Mcis.MtMoveOrderSequenceRes();

                        tmp.MoveDate = x.MoveDate;
                        tmp.MoveTime = x.MoveTime;
                        tmp.EmployeeId = x.EmployeeId;   
                        tmp.EmployeeName = x.EmployeeName;                         

                        tmp.WorkOrderId = x.WorkOrderId;
                        tmp.WorkOrderDesc = x.WorkOrderDesc;
                       
                        tmp.DrawingCd = x.DrawingCd;
                        tmp.WorkStationGrpCd_FR = x.WorkStationGrpCd_FR;
                        tmp.WorkStationGrpCdDesc_FR = x.WorkStationGrpCdDesc_FR;

                        tmp.WorkStationGrpCd_STD = x.WorkStationGrpCd_STD;
                        tmp.WorkStationGrpCdDesc_STD = x.WorkStationGrpCdDesc_STD;

                        tmp.WorkStationGrpCd_TO = x.WorkStationGrpCd_TO;
                        tmp.WorkStationGrpCdDesc_TO = x.WorkStationGrpCdDesc_TO;

                        tmp.SeqNo_NEXT = x.SeqNo_NEXT;
                        tmp.EndTime = x.EndTime;
                        tmp.CauseStatus = x.CauseStatus;
                        tmp.RemarkCause = x.RemarkCause;
                        tmp.AppvCauseId = x.AppvCauseId;
                        tmp.AppvCauseDt = x.AppvCauseDt;
                        tmp.Process_Flag = x.Process_Flag;
                        tmp.User_Id = x.User_Id;
                        tmp.User_date = x.User_date;

                        tmp._result._status = x._status;
                        tmp._result._code = x._code;
                        tmp._result._message = x._message;

                        res.Add(tmp);
                    }

                }

            }
            catch (Exception ex)
            {
                tmp = new ASSETKKF_MODEL.Response.Mcis.MtMoveOrderSequenceRes();
                tmp.EmployeeId = dataReq.EmployeeId;
                tmp.WorkStationGrpCd_TO = dataReq.WorkStationGrpCd_TO;
                tmp.WorkOrderId = dataReq.WorkOrderId;
                tmp.AppvCauseId = dataReq.AppvCauseId;
                tmp._result._status = "F";
                tmp._result._code = "F0001";
                tmp._result._message = "การเชื่อมต่อฐานข้อมูลมีปัญหา..."+ex.Message;

                res.Add(tmp);
            }
                
             
            dataRes.data = res;
        }
    }
}
