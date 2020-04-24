using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Request.Mcis
{
    public class MtWorkOrderSequence_EmpReq
    {
        public DateTime? WorkDate;
        public int? ItemNo;
        public string EmployeeId;
        public string WorkStationGrpCd;
        public string WorkOrderId;
        public DateTime? StartTime;
        public decimal? QtyAmt;
        public DateTime? EndTime;
        public decimal? StdTime;
        public decimal? ActTime;
        public decimal? DiffTime;
        public string Use_FreeTimeOT;
        public string User_Id;
        public DateTime? User_date;
        public string DrawingCd;
        public string CusCod;
        public string Post_flag;
        public string Pause_Flag;
        public string ReworkFlag;
    }
}
