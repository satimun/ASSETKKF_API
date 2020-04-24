using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Request.Mcis
{
    public class MtMoveOrderSequenceReq
    {
        public int? Sequence;
        public DateTime? MoveDate;//
        public DateTime? MoveTime;//
        public string EmployeeId;//
        public string WorkOrderId;//
        public string DrawingCd;
        public string WorkStationGrpCd_FR;
        public string WorkStationGrpCd_STD;
        public string WorkStationGrpCd_TO;//
        public string SeqNo_NEXT;
        public DateTime? EndTime;
        public string CauseStatus;//
        public string RemarkCause;//
        public string AppvCauseId;//
        public DateTime? AppvCauseDt;
        public string Process_Flag;
        public string User_Id;//
        public DateTime? User_date;

        //EmployeeId,WorkOrderId,WorkStationGrpCd_TO,MoveDate,MoveTime,User_Id,AppvCauseId,RemarkCause,CauseStatus
    }
}
