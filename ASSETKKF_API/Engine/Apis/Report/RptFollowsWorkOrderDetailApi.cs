using Core.Util;
using ASSETKKF_MODEL.Request.Report;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace ASSETKKF_API.Engine.Apis.Report
{
    public class RptFollowsWorkOrderDetailApi : Base<RptWorkOrderReq>
    {
        public RptFollowsWorkOrderDetailApi()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true; 
        }

        protected override void ExecuteChild(RptWorkOrderReq dataReq, ResponseAPI dataRes)
        {
            var tmp = new ASSETKKF_MODEL.Response.Report.RptFollowsWorkOrderDetailRes();
            var res = new List<ASSETKKF_MODEL.Response.Report.RptFollowsWorkOrderDetailRes>();
           
            try
            {
                var roles = ASSETKKF_ADO.Mssql.Mcis.Report.RptFollowsWorkOrderAdo.GetInstant().GetDetail(dataReq.WorkOrderID);

                foreach (var x in roles)
                {
                    tmp = new ASSETKKF_MODEL.Response.Report.RptFollowsWorkOrderDetailRes();

                    tmp.WorkOrderID = x.WorkOrderID;
                    tmp.SeqNo = x.SeqNo;
                    tmp.WorkStationGrp = x.WorkStationGrp;
                    tmp.zSequence = x.zSequence;
                    tmp.WorkStationGrpNm = x.WorkStationGrpNm;
                    tmp.DlCost = x.DlCost;
                    tmp.FOHCost = x.FOHCost;
                    tmp.MDCost = x.MDCost;
                    tmp.TotalCost = x.TotalCost;
                    tmp.NoOfMinStd = x.NoOfMinStd;
                    tmp.NoOfMinsAct = x.NoOfMinsAct;
                    tmp.zDiffTime = x.NoOfMinStd- x.NoOfMinsAct;           // zDiffTime = (NoOfMinStd-NoOfMinsAct);
                    tmp.StartTime = x.StartTime;
                    tmp.EndTime = x.EndTime;
                    tmp.MoveTime = x.MoveTime;
                    tmp.MoveEmpID = x.MoveEmpID;
                    tmp.MoveEmpNm = x.MoveEmpNm;
                    tmp.zWorkStationGrp_Next = x.zWorkStationGrp_Next;
                    tmp.WorkStationGrp_Next = x.WorkStationGrp_Next;
                    tmp.ReworkFlag = x.ReworkFlag;

                    tmp.StartTimeStr = DateTimeUtil.ToStringViewDT(x.StartTime);
                    tmp.EndTimeStr = DateTimeUtil.ToStringViewDT(x.EndTime);
                    tmp.MoveTimeStr = DateTimeUtil.ToStringViewDT(x.MoveTime);


                    tmp.MAXSEQ = x.MAXSEQ;
                    tmp.CTMCH = x.CTMCH;
                    tmp.CTEMP = x.CTEMP;
                    tmp.CTMOVE = x.CTMOVE;
                    tmp.WorkOrderDesc = x.WorkOrderDesc;
                    tmp.WorkOrderStatus = x.WorkOrderStatus;

                    res.Add(tmp);
                }
            }
            catch (Exception)
            {

            }

            dataRes.data = res;

        }
    }



}
