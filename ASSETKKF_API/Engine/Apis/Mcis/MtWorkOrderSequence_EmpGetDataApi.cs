using ASSETKKF_MODEL.Request.Mcis;
using ASSETKKF_MODEL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Apis.Mcis
{
    public class MtWorkOrderSequence_EmpGetDataApi : Base<MtWorkOrderSequence_EmpReq>
    {
        public MtWorkOrderSequence_EmpGetDataApi()
        {
            PermissionKey = "ADMIN";

        }

        protected override void ExecuteChild(MtWorkOrderSequence_EmpReq dataReq, ResponseAPI dataRes)
        {
            var tmp = new ASSETKKF_MODEL.Response.Mcis.MtWorkOrderSequence_EmpRes();
            var res = new List<ASSETKKF_MODEL.Response.Mcis.MtWorkOrderSequence_EmpRes>();

            try
            {
                var roles = ASSETKKF_ADO.Mssql.Mcis.MtWorkOrderSequence_EmpAdo.GetInstant().GetData(dataReq);

                if (roles.Count == 0)
                {
                    tmp = new ASSETKKF_MODEL.Response.Mcis.MtWorkOrderSequence_EmpRes();
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
                        tmp = new ASSETKKF_MODEL.Response.Mcis.MtWorkOrderSequence_EmpRes();

                        tmp.WorkDate = x.WorkDate;
                        tmp.ItemNo = x.ItemNo;
                        tmp.EmployeeId = x.EmployeeId;
                        tmp.WorkStationGrpCd = x.WorkStationGrpCd;
                        tmp.WorkOrderId = x.WorkOrderId;
                        tmp.StartTime = x.StartTime;
                        tmp.QtyAmt = x.QtyAmt;
                        tmp.EndTime = x.EndTime;
                        tmp.StdTime = x.StdTime;
                        tmp.ActTime = x.ActTime;
                        tmp.DiffTime = x.DiffTime;
                        tmp.Use_FreeTimeOT = x.Use_FreeTimeOT;
                        tmp.User_Id = x.User_Id;
                        tmp.User_date = x.User_date;
                        tmp.DrawingCd = x.DrawingCd;
                        tmp.CusCod = x.CusCod;
                        tmp.Post_flag = x.Post_flag;
                        tmp.Pause_Flag = x.Pause_Flag;
                        tmp.ReworkFlag = x.ReworkFlag;

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
                tmp = new ASSETKKF_MODEL.Response.Mcis.MtWorkOrderSequence_EmpRes();
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
