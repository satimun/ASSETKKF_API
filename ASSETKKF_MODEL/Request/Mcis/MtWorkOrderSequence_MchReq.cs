using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Request.Mcis
{
    public class MtWorkOrderSequence_MchReq
    { 
        public DateTime? WorkDate;
        public int? ItemNo;
        public string WorkStationGrpCd;
        public string EmployeeId;
        public string WorkOrderId;
        public DateTime? StartTime;
        public DateTime? EndTime;
        public decimal? QtyAmt;
        public decimal? StdTime;
        public decimal? ActTime;
        public decimal? DiffTime;
        public string Use_FreeTimeOT;
        public string User_Id;
        public DateTime? User_date;
        public string DrawingCd;
        public string CusCod;
        public string Post_flag;
        public string SupplierCd;
        public string Remark;
        public DateTime? Wantdate;
        public string ReworkFlag;
        public string Pause_Flag;
    }
}
