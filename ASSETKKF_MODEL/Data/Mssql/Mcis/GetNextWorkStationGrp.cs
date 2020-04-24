using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Data.Mssql.Mcis
{
    public class GetNextWorkStationGrp
    {        
        public string   WorkOrderID;
        public string   WorkOrderDesc;
        public string   DrawingCd;
        public string   WorkStationGrpCd;
        public string   WorkStationGrp_STD;
        public string   SeqNo_STD;
        public string   WorkStationGrpNm;
        public int?     sCnt;
        public int?     rCnt;
        public decimal? NoOfMins;
        public string   message;

        public string   _status;
        public string   _code;
        public string   _message;
    }
}
