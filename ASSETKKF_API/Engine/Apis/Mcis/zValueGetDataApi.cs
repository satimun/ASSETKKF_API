using ASSETKKF_MODEL.Request.Mcis;
using ASSETKKF_MODEL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Apis.Mcis
{
    public class zValueGetDataApi : Base<zValueReq>
    {
        public zValueGetDataApi()
        {
            PermissionKey = "ADMIN";

        }

        protected override void ExecuteChild(zValueReq dataReq, ResponseAPI dataRes)
        {
            var tmp = new ASSETKKF_MODEL.Response.Mcis.zValueRes();
            var res = new List<ASSETKKF_MODEL.Response.Mcis.zValueRes>();
           

            try
            {
                var roles = ASSETKKF_ADO.Mssql.Mcis.zValueAdo.GetInstant().GetData(dataReq);

                if (roles.Count == 0)
                {
                    tmp = new ASSETKKF_MODEL.Response.Mcis.zValueRes();
                    tmp.ValueName = dataReq.ValueName;
                    tmp._result._status = "F";
                    tmp._result._code = "F0002";
                    tmp._result._message = "ไม่พบข้อมูล ใบสั่งผลิต.";

                    res.Add(tmp);
                }
                else
                {

                    foreach (var x in roles)
                    {
                        tmp = new ASSETKKF_MODEL.Response.Mcis.zValueRes();

                        tmp.ValueName = x.ValueName;
                        tmp.ValueData = x.ValueData;
                        tmp.ValueDes = x.ValueDes;

                        tmp._result._status = "S";
                        tmp._result._code = "S0000";
                        tmp._result._message = "เรียบร้อย...";

                        res.Add(tmp);
                    }
                }
            }
            catch (Exception)
            {
                tmp = new ASSETKKF_MODEL.Response.Mcis.zValueRes();
                tmp.ValueName = dataReq.ValueName;
                tmp._result._status = "F";
                tmp._result._code = "F0001";
                tmp._result._message = "การเชื่อมต่อฐานข้อมูลมีปัญหา...";

                res.Add(tmp);
            }
            dataRes.data = res;
        }
    }
}
