using ASSETKKF_MODEL.Request.Mcis;
using ASSETKKF_MODEL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Apis.Mcis
{
    public class MtWorkOrderSequence_MchGetDataApi : Base<MtWorkOrderSequence_MchReq>
    {
        public MtWorkOrderSequence_MchGetDataApi()
        {
            PermissionKey = "ADMIN";

        }

        protected override void ExecuteChild(MtWorkOrderSequence_MchReq dataReq, ResponseAPI dataRes)
        {
            var tmp = new ASSETKKF_MODEL.Response.Mcis.MtWorkOrderSequence_MchRes();
            var res = new List<ASSETKKF_MODEL.Response.Mcis.MtWorkOrderSequence_MchRes>();
            
            try
            {
                var roles = ASSETKKF_ADO.Mssql.Mcis.MtWorkOrderSequence_MchAdo.GetInstant().GetData(dataReq);

                if (roles.Count == 0)
                {
                    tmp = new ASSETKKF_MODEL.Response.Mcis.MtWorkOrderSequence_MchRes();
                    tmp.WorkOrderId = dataReq.WorkOrderId;
                    tmp.EmployeeId = dataReq.EmployeeId;
                    tmp.WorkStationGrpCd = dataReq.WorkStationGrpCd;
                    tmp._result._status = "F";
                    tmp._result._code = "F0002";
                    tmp._result._message = "ไม่พบข้อมูล ใบสั่งผลิต." + dataReq.WorkOrderId;

                    res.Add(tmp);
                }
                else
                {
                    foreach (var x in roles)
                    {
                        tmp = new ASSETKKF_MODEL.Response.Mcis.MtWorkOrderSequence_MchRes();

                        tmp.WorkDate = x.WorkDate;
                        tmp.ItemNo = x.ItemNo;
                        tmp.WorkStationGrpCd = x.WorkStationGrpCd;
                        tmp.EmployeeId = x.EmployeeId;
                        tmp.WorkOrderId = x.WorkOrderId;
                        tmp.StartTime = x.StartTime;
                        tmp.EndTime = x.EndTime;
                        tmp.QtyAmt = x.QtyAmt;
                        tmp.StdTime = x.StdTime;
                        tmp.ActTime = x.ActTime;
                        tmp.DiffTime = x.DiffTime;
                        tmp.Use_FreeTimeOT = x.Use_FreeTimeOT;
                        tmp.User_Id = x.User_Id;
                        tmp.User_date = x.User_date;
                        tmp.DrawingCd = x.DrawingCd;
                        tmp.CusCod = x.CusCod;
                        tmp.Post_flag = x.Post_flag;
                        tmp.SupplierCd = x.SupplierCd;
                        tmp.Remark = x.Remark;
                        tmp.Wantdate = x.Wantdate;
                        tmp.ReworkFlag = x.ReworkFlag;
                        tmp.Pause_Flag = x.Pause_Flag;

                        tmp.Grp_name = x.Grp_name;
                        tmp.WorkOrderDesc = x.WorkOrderDesc;
                        tmp.Emp_name = x.Emp_name;
                        tmp.Groupname = x.Groupname;
                        tmp.Customer_name = x.Customer_name;

                        res.Add(tmp);
                    }
                }
            }
            catch (Exception ex)
            {
                tmp = new ASSETKKF_MODEL.Response.Mcis.MtWorkOrderSequence_MchRes();
                tmp.WorkOrderId = dataReq.WorkOrderId;
                tmp.EmployeeId = dataReq.EmployeeId;
                tmp.WorkStationGrpCd = dataReq.WorkStationGrpCd;
                tmp._result._status = "F";
                tmp._result._code = "F0001";
                tmp._result._message = "การเชื่อมต่อฐานข้อมูลมีปัญหา..." + ex.Message;
                res.Add(tmp);
            }
            dataRes.data = res;
        }
    }
}
