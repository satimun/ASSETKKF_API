using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Response.Mcis
{
    public class msWorkStationGrpRes
    {
        public string WorkStationGrpCd;
        public string WorkStationGrpNm;
        public decimal? DLCost;
        public decimal? FOHCost;
        public decimal? MDCost;
        public string WorkStationCD;
        public string LastUpdUsr;
        public DateTime? LastUpdDt;
        public decimal? UnionDLCost;
        public string WorkStationNM;
        public string MulEmp_flag;
        public string ExtGrp_Flag;

        public ResultDataResponse _result = new ResultDataResponse();
    }
}
