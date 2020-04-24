using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Response.Mcis
{
    public class GetNextWorkStationGrpRes
    {
        public string workOrderID;
        public string workOrderDesc;
        public string drawingCd;
        public string workStationGrpCd;
        public string workStationGrp_STD;
        public string seqNo_STD;
        public string workStationGrpNm;
        public int? scnt;
        public int? rcnt;
        public decimal?noOfMins;
        public string message;

        
        public ResultDataResponse _result = new ResultDataResponse();
    }
}
