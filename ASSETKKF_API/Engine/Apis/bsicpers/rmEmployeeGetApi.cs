using ASSETKKF_MODEL.Request.bsicpers;
using ASSETKKF_MODEL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Apis.bsicpers
{
    public class rmEmployeeGetApi : Base<rmEmployeeReq>
    {
        public rmEmployeeGetApi()
        {
            PermissionKey = "ADMIN";

        }

        protected override void ExecuteChild(rmEmployeeReq dataReq, ResponseAPI dataRes)
        {
            var res = new List<ASSETKKF_MODEL.Response.bsicpers.rmEmployeeRes>();
            var tmp = new ASSETKKF_MODEL.Response.bsicpers.rmEmployeeRes();


            try
            {

                var roles = ASSETKKF_ADO.Mssql.bsicpers.rmEmployeeAdo.GetInstant().GetData(dataReq);
                if (roles.Count == 0)
                {
                    tmp = new ASSETKKF_MODEL.Response.bsicpers.rmEmployeeRes();
                    tmp.EmployeeID = dataReq.EmployeeID;
                    tmp._result._status = "F";
                    tmp._result._code = "F0002";
                    tmp._result._message = "ไม่พบข้อมูล รหัสพนักงานที่ระบุ" ;

                    res.Add(tmp);
                }
                else
                {
                    foreach (var x in roles)
                    {
                        tmp = new ASSETKKF_MODEL.Response.bsicpers.rmEmployeeRes();

                        tmp.EmployeeID = x.EmployeeID;
                        tmp.EmploType = x.EmploType;
                        tmp.Status = x.Status;
                        tmp.Shift = x.Shift;
                        tmp.Weekend = x.Weekend;
                        tmp.DepCode = x.DepCode;
                        tmp.Position = x.Position;
                        tmp.TitleName = x.TitleName;
                        tmp.FirstName = x.FirstName;
                        tmp.LastName = x.LastName;
                        tmp.StartDate = x.StartDate;
                        tmp.EmploDate = x.EmploDate;
                        tmp.EndDate = x.EndDate;
                        tmp.EditDate = x.EditDate;

                        

                        res.Add(tmp);
                    }
                }
            }
            catch (Exception)
            {
                tmp = new ASSETKKF_MODEL.Response.bsicpers.rmEmployeeRes();
                tmp.EmployeeID = dataReq.EmployeeID;
                tmp._result._status = "F";
                tmp._result._code = "F0001";
                tmp._result._message = "การเชื่อมต่อฐานข้อมูลมีปัญหา...";

                res.Add(tmp);
            }
            dataRes.data = res;
        }
    }
}
