using ASSETKKF_MODEL.Request.Mcis;
using ASSETKKF_MODEL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Apis.Mcis
{
    public class MtMoveOrderSequenceGetDataApi : Base<MtMoveOrderSequenceReq>
    {
        public MtMoveOrderSequenceGetDataApi()
        {
            PermissionKey = "ADMIN";

        }

        protected override void ExecuteChild(MtMoveOrderSequenceReq dataReq, ResponseAPI dataRes)
        {
            var res = new List<ASSETKKF_MODEL.Response.Mcis.MtMoveOrderSequenceRes>();
            var roles = ASSETKKF_ADO.Mssql.Mcis.MtMoveOrderSequenceAdo.GetInstant().GetData(dataReq);

            foreach (var x in roles)
            {
                var tmp = new ASSETKKF_MODEL.Response.Mcis.MtMoveOrderSequenceRes();

                tmp.Sequence = x.Sequence;
                tmp.MoveDate = x.MoveDate;
                tmp.MoveTime = x.MoveTime;
                tmp.EmployeeId = x.EmployeeId;
                tmp.WorkOrderId = x.WorkOrderId;
                tmp.DrawingCd = x.DrawingCd;
                tmp.WorkStationGrpCd_FR = x.WorkStationGrpCd_FR;
                tmp.WorkStationGrpCd_STD = x.WorkStationGrpCd_STD;
                tmp.WorkStationGrpCd_TO = x.WorkStationGrpCd_TO;
                tmp.SeqNo_NEXT = x.SeqNo_NEXT;
                tmp.EndTime = x.EndTime;
                tmp.CauseStatus = x.CauseStatus;
                tmp.RemarkCause = x.RemarkCause;
                tmp.AppvCauseId = x.AppvCauseId;
                tmp.AppvCauseDt = x.AppvCauseDt;
                tmp.Process_Flag = x.Process_Flag;
                tmp.User_Id = x.User_Id;
                tmp.User_date = x.User_date;

                res.Add(tmp);
            }
            dataRes.data = res;
        }
    }
}
