using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Request.Mcis
{
    public class MtWorkOrderSequenceCloseOrderReq
    {
        public string EmployeeId;
        public string WorkOrderID;
        public string WorkStationGrpCd;
        public string AddEmployeeId;
        public string User_Id;
        public string USERAPP;
        public int? CloseChoose;
        public string EndSequence;
        public DateTime? DateSelect;
        public string TimeSelect;
        public DateTime? DateSelectFull;

    }
}
