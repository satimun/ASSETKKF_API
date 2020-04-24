using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Response.Mcis
{
    public class MtWorkOrderSequenceLRes
    {
        public DateTime? WorkDate;
        public string WorkStationGrpCd;
        public string EmployeeId;
        public string WorkOrderId;
        public DateTime? EndTime;
        public decimal? QtyAmt;
        public string DrawingCd;
        public string CusCod;
        public string Move_Flag;
        public string ReworkFlag;
        public string WorkStationGrp_STD;
        public string SeqNo_STD;
        public string WorkStationGrpCd_Next;
        public string SeqNo_NEXT;
        public string CauseStatus;
        public string RemarkCause;
        public string AppvCauseId;
        public DateTime? AppvCauseDt;
        public string User_Id;
        public DateTime? User_date;
        public string ExtFlag;
        public string EndSequence;

        public ResultDataResponse _result = new ResultDataResponse();
    }
}
