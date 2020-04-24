using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Response.Report
{
    public class RptFollowsWorkOrderRes
    { 
        public string WorkOrderID;
        public  RptFollowsWorkOrderViewRes  RptFollowsWorkOrder = new  RptFollowsWorkOrderViewRes ();
    }

    public class RptFollowsWorkOrderViewRes
    {
        public RptFollowsWorkOrderHeadRes WorkOrderHead = new RptFollowsWorkOrderHeadRes();
        public List<RptFollowsWorkOrderDetailRes> WorkOrderDetail = new List<RptFollowsWorkOrderDetailRes>();
        public List<RptFollowsWorkOrderLastRes> WorkOrderLast = new List<RptFollowsWorkOrderLastRes>();
    }

    public class RptFollowsWorkOrderHeadRes
    {
        public string MchProjectID;
        public string WorkOrderID;
        public string WorkOrderDesc;
        public string DrawingCd;
        public string MchProjectNm;
        public decimal? QTYAmt;
        public string Cuscod;
        public string WorkOrderStatus;
        public string JobTypeCd;
        public string JobTypeNm;
        public string JobPriorityCd;
        public string STATION_GRP;
        public string JobPriorityNm;

        public ResultDataResponse _result = new ResultDataResponse();

    }

    public class RptFollowsWorkOrderDetailRes
    {
        public string WorkOrderID;
        public decimal SeqNo;
        public string WorkStationGrp;
        public decimal? zSequence;
        public string WorkStationGrpNm;
        public decimal? DlCost;
        public decimal? FOHCost;
        public decimal? MDCost;
        public decimal? TotalCost;
        public decimal? NoOfMinStd;
        public decimal? NoOfMinsAct;
        public decimal? zDiffTime;     // zDiffTime = (NoOfMinStd-NoOfMinsAct);
        public DateTime? StartTime;
        public DateTime? EndTime;
        public DateTime? MoveTime;
        public String MoveEmpID;
        public String MoveEmpNm;
        public String zWorkStationGrp_Next;
        public String WorkStationGrp_Next;
        public String ReworkFlag;

        public String StartTimeStr;
        public String EndTimeStr;
        public String MoveTimeStr;

        public int? MAXSEQ;
        public int? CTMCH;
        public int? CTEMP;
        public int? CTMOVE;
        public String WorkOrderDesc;
        public String WorkOrderStatus;

        public ResultDataResponse _result = new ResultDataResponse();

    }

    public class RptFollowsWorkOrderLastRes
    {
        public string WorkOrderID;
        public String DRAWINGCD;
        public String SEQNO;
        public String WORKSTATIONGRPCD;
        public decimal? DLCOST;
        public String WORKSTATIONGRPNM;
        public decimal? FOHCOST;
        public decimal? MDCOST;
        public decimal? SetupTime;
        public decimal? NOOFMINS;
        public decimal? NoOfMinStd;
        public decimal? U_COST;
        public decimal? TOTCOST;

        public ResultDataResponse _result = new ResultDataResponse();
    }
}
