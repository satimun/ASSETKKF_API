using System;
using System.Collections.Generic;
using System.Text;
 
namespace ASSETKKF_MODEL.Data.Mssql.Mcis
{
    public class MtMoveOrderSequence
    {
        public int? Sequence;
        public DateTime? MoveDate;
        public DateTime? MoveTime;
        public string EmployeeId;
        public string EmployeeName;

        public string WorkOrderId;
        public string WorkOrderDesc;

        public string DrawingCd;
        public string WorkStationGrpCd_FR;
        public string WorkStationGrpCdDesc_FR;

        public string WorkStationGrpCd_STD;
        public string WorkStationGrpCdDesc_STD;

        public string WorkStationGrpCd_TO;
        public string WorkStationGrpCdDesc_TO;

        public string SeqNo_NEXT;
        public DateTime? EndTime;
        public string CauseStatus;
        public string RemarkCause;
        public string AppvCauseId;
        public DateTime? AppvCauseDt;
        public string Process_Flag;
        public string User_Id;
        public DateTime? User_date;

        public int? Cnt;
        public decimal? NoOfMins;

        public string _status;
        public string _code;
        public string _message;


    }
    
}
