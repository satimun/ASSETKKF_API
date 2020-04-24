 
using ASSETKKF_MODEL.Request.Mcis;
using ASSETKKF_MODEL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Apis.Mcis
{
    public class msUserSequenceGetDataApi : Base<msUserSequenceReq>
    {
        public msUserSequenceGetDataApi()
        {
            PermissionKey = "ADMIN";

        }

        protected override void ExecuteChild(msUserSequenceReq dataReq, ResponseAPI dataRes)
        {
            var res = new List<ASSETKKF_MODEL.Response.Mcis.msUserSequenceRes>();
            var tmp = new ASSETKKF_MODEL.Response.Mcis.msUserSequenceRes();
           
            try
            {
                var roles = ASSETKKF_ADO.Mssql.Mcis.msUserSequenceAdo.GetInstant().GetData(dataReq);
                if (roles.Count == 0)
                {
                    tmp = new ASSETKKF_MODEL.Response.Mcis.msUserSequenceRes();
                    tmp.USERCODE  = dataReq.USERCODE;
                    tmp._result._status = "F";
                    tmp._result._code = "F0002";
                    tmp._result._message = "ไม่มีสิทธิ์ การอนุมัติ";

                    res.Add(tmp);
                }
                else
                {

                    foreach (var x in roles)
                    {
                         tmp = new ASSETKKF_MODEL.Response.Mcis.msUserSequenceRes();

                        tmp.USERCODE = x.USERCODE;
                        tmp.STDATE = x.STDATE;
                        tmp.ENDATE = x.ENDATE;
                        tmp.USER_ID = x.USER_ID;
                        tmp.USER_DATE = x.USER_DATE;
                        tmp.EDIT_TYPE = x.EDIT_TYPE;

                        res.Add(tmp);
                    }
                }
            }
            catch (Exception)
            {
                tmp = new ASSETKKF_MODEL.Response.Mcis.msUserSequenceRes();
                tmp.USERCODE = dataReq.USERCODE;
                tmp._result._status = "F";
                tmp._result._code = "F0001";
                tmp._result._message = "การเชื่อมต่อฐานข้อมูลมีปัญหา...";

                res.Add(tmp);
            }
            dataRes.data = res;
        }
    }
}
