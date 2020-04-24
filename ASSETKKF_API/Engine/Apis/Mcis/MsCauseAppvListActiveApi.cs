using ASSETKKF_MODEL.Request.Mcis;
using ASSETKKF_MODEL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Apis.Mcis
{

    public class MsCauseAppvListActiveApi : Base<MsCauseAppvReq>
    {
        public MsCauseAppvListActiveApi()
        {
            PermissionKey = "ADMIN";

        }

        protected override void ExecuteChild(MsCauseAppvReq dataReq, ResponseAPI dataRes)
        {
            var tmp = new ASSETKKF_MODEL.Response.Mcis.MsCauseAppvRes();
            var res = new List<ASSETKKF_MODEL.Response.Mcis.MsCauseAppvRes>();
            
            try
            {
                var roles = ASSETKKF_ADO.Mssql.Mcis.MsCauseAppvAdo.GetInstant().ListActive();
                if (roles.Count == 0)
                {
                    tmp = new ASSETKKF_MODEL.Response.Mcis.MsCauseAppvRes();
                    tmp.CauseID = dataReq.CauseID;
                    tmp._result._status = "F";
                    tmp._result._code = "F0002";
                    tmp._result._message = "ไม่พบข้อมูล สาเหตุ การอนุมัติ"  ;

                    res.Add(tmp);
                }
                else
                {

                    foreach (var x in roles)
                    {
                        tmp = new ASSETKKF_MODEL.Response.Mcis.MsCauseAppvRes();

                        tmp.CauseID = x.CauseID;
                        tmp.CauseName = x.CauseName;

                        res.Add(tmp);
                    }
                }
               
            }
            catch (Exception)
            {
                tmp = new ASSETKKF_MODEL.Response.Mcis.MsCauseAppvRes();
                tmp.CauseID = dataReq.CauseID;
                tmp._result._status = "F";
                tmp._result._code = "F0001";
                tmp._result._message = "การเชื่อมต่อฐานข้อมูลมีปัญหา...";

                res.Add(tmp);
            }

            dataRes.data = res;
        }
    }
}
