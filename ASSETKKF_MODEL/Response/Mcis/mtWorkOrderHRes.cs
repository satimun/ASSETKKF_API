using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Response.Mcis
{
    public class mtWorkOrderHRes
    {
        public string WorkOrderID;
        public string MchProjectID;
        public string DrawingCd;
        public string WorkOrderDesc;
        public decimal? QtyAmt;
        public string FMDeptCd;
        public DateTime? CreatedDate;
        public string WorkOrderStatus;
        public string FstRcvrWs;
        public string PrintFlg;
        public decimal? AmtUnit;
        public string DocListNo;
        public string MainWS;
        public string SendStock;
        public string USER_ID;
        public DateTime? USER_DATE;
        public DateTime? Date_Open;
        public DateTime? PDate_Cls;
        public DateTime? RDate_cls;
        public string CreateSPAuto;
        public decimal? DM_COST;
        public decimal? DM_FOH;
        public decimal? SEQUENCE_COST;
        public decimal? SEQUENCE_PROFIT;
        public decimal? TOTPRC;
        public decimal? TOTCOST;
        public string DM_COST_VERSION;
        public string DEL_FLAG;
        public DateTime? MATDAYWANT;
        public decimal? SEQUENCE_ACT;
        public decimal? TOTCOST_ACT;
        public decimal? SEQUENCE_DIF;
        public decimal? PROFIT_BEG;
        public string STATION_GRP;

        public ResultDataResponse _result = new ResultDataResponse();
    }
}
