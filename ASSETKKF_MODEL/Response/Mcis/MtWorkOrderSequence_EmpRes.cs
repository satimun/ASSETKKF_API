using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Response.Mcis
{
    public class MtWorkOrderSequence_EmpRes
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

        public string Grp_name;
        public string WorkOrderDesc;
        public string Emp_name;
        public string Groupname;
        public string Customer_name;

        public string StartTimeStr;
        public string EndTimeStr;
        public string User_dateStr;
        public string WantdateStr;

        public ResultDataResponse _result = new ResultDataResponse();
    }
}
