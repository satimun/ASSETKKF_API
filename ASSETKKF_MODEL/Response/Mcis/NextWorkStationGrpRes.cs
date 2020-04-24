using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Response.Mcis
{
    public class NextWorkStationGrpRes
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
        public string message  ;

        public ResultDataResponse _result = new ResultDataResponse();   

    }
}
