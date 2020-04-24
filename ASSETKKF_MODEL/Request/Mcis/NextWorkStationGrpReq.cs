using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Request.Mcis
{
    public class NextWorkStationGrpReq
    {
        public string WorkOrderID;
        public string WorkOrderDesc;
        public string DrawingCd;
        public string WorkStationGrpCd;
        public string WorkStationGrp_STD;
        public string SeqNo_STD;

        public string WorkStationGrpNm;

        public int? sCnt;
        public int? rCnt;
        public decimal? NoOfMins;

    }
}
