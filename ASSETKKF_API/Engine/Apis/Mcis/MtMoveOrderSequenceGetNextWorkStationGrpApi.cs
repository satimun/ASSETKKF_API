using ASSETKKF_MODEL.Request.Mcis;
using ASSETKKF_MODEL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Apis.Mcis
{
    public class MtMoveOrderSequenceGetNextWorkStationGrpApi : Base<MtMoveOrderSequenceReq>
    {
        public MtMoveOrderSequenceGetNextWorkStationGrpApi()
        {
            PermissionKey = "ADMIN";

        }

        protected override void ExecuteChild(MtMoveOrderSequenceReq dataReq, ResponseAPI dataRes)
        {
            var tmp = new ASSETKKF_MODEL.Response.Mcis.GetNextWorkStationGrpRes();
            
            var res = new List<ASSETKKF_MODEL.Response.Mcis.GetNextWorkStationGrpRes>();

            try 
            {
                var roles = ASSETKKF_ADO.Mssql.Mcis.MtMoveOrderSequenceAdo.GetInstant().GetNextWorkStationGrp(dataReq);
                if (roles.Count == 0)
                {
                    tmp = new ASSETKKF_MODEL.Response.Mcis.GetNextWorkStationGrpRes();
                    tmp.workOrderID = dataReq.WorkOrderId;
                    tmp._result._status = "F";
                    tmp._result._code = "F0002";
                    tmp._result._message = "ไม่พบข้อมูล ใบสั่งผลิต." + dataReq.WorkOrderId;

                    res.Add(tmp);
                }
                else
                {

                    foreach (var x in roles)
                    {
                        tmp = new ASSETKKF_MODEL.Response.Mcis.GetNextWorkStationGrpRes();

                        tmp.workOrderID = x.WorkOrderID;
                        tmp.workOrderDesc = x.WorkOrderDesc;
                        tmp.drawingCd = x.DrawingCd;
                        tmp.workStationGrpCd = x.WorkStationGrpCd;
                        tmp.workStationGrp_STD = x.WorkStationGrp_STD;
                        tmp.seqNo_STD = x.SeqNo_STD;
                        tmp.workStationGrpNm = x.WorkStationGrpNm;
                        tmp.scnt = x.sCnt;
                        tmp.noOfMins = x.NoOfMins;
                        tmp.message = x.message;

                        tmp._result._status = x._status;
                        tmp._result._code = x._code;
                        tmp._result._message = x._message;                  



                        res.Add(tmp);
                    }
                }
            }
            catch (Exception ex)
            {
                tmp = new ASSETKKF_MODEL.Response.Mcis.GetNextWorkStationGrpRes();
                tmp.workOrderID = dataReq.WorkOrderId;
                tmp._result._status = "F";
                tmp._result._code = "F0001";
                tmp._result._message = "การเชื่อมต่อฐานข้อมูลมีปัญหา..." + ex.Message;
                res.Add(tmp);
            }

            dataRes.data = res;

        }
    }
}
