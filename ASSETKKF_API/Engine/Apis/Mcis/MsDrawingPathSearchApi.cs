using ASSETKKF_MODEL.Request.Mcis;
using ASSETKKF_MODEL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Apis.Mcis
{
    public class MsDrawingPathSearchApi : Base< MsDrawingPathReq >
    {
        public MsDrawingPathSearchApi()
        {
            PermissionKey = "ADMIN";

        }

        protected override void ExecuteChild( MsDrawingPathReq  dataReq, ResponseAPI dataRes)
        {
          /*  var res = new List<ASSETKKF_MODEL.Response.Mcis.MsDrawingPathRes>();
            var roles = ASSETKKF_ADO.Mssql.Mcis.MsDrawingPathAdo.GetInstant().Search(dataReq);

            foreach (var x in roles)
            {
                var tmp = new ASSETKKF_MODEL.Response.Mcis.MsDrawingPathRes();

                tmp.DrawingCd = x.DrawingCd;
                tmp.Itemno = x.Itemno;
                tmp.PathName = x.PathName;
                tmp.PathLink = x.PathLink;
                tmp.EditUser = x.EditUser;
                tmp.EditDate = x.EditDate;

                res.Add(tmp);
            }
            dataRes.data = res;
            */

            var tmp = new ASSETKKF_MODEL.Response.Mcis.MsDrawingPathRes();
            var res = new List<ASSETKKF_MODEL.Response.Mcis.MsDrawingPathRes>();

            try
            {
                var roles = ASSETKKF_ADO.Mssql.Mcis.MsDrawingPathAdo.GetInstant().Search(dataReq);

                if (roles.Count == 0)
                {
                    tmp = new ASSETKKF_MODEL.Response.Mcis.MsDrawingPathRes();
                    tmp.DrawingCd = dataReq.DrawingCd;
                    tmp._result._status = "F";
                    tmp._result._code = "F0002";
                    tmp._result._message = "ไม่พบข้อมูล ใบสั่งผลิต.";

                    res.Add(tmp);
                }
                else
                {
                    foreach (var x in roles)
                    {
                        tmp = new ASSETKKF_MODEL.Response.Mcis.MsDrawingPathRes();
                        tmp.RowID = x.RowID;
                        tmp.DrawingCd = x.DrawingCd;
                        tmp.DrawingNmTh = x.DrawingNmTh;
                        tmp.DrawingCdDesc = x.DrawingCd +" : " + x.DrawingNmTh;
                        tmp.Itemno = x.Itemno;
                        tmp.PathName = x.PathName;
                        tmp.PathLink = x.PathLink;
                        tmp.EditUser = x.EditUser;
                        tmp.EditDate = x.EditDate;

                        tmp._result._status = "S";
                        tmp._result._code = "S0000";
                        tmp._result._message = "เรียบร้อย...";

                        res.Add(tmp);
                    }
                }
            }
            catch (Exception e)
            {
                tmp = new ASSETKKF_MODEL.Response.Mcis.MsDrawingPathRes();
                tmp.DrawingCd = dataReq.DrawingCd;
                tmp._result._status = "F";
                tmp._result._code = "F0001";
                tmp._result._message = "การเชื่อมต่อฐานข้อมูลมีปัญหา..."+e.Message;

                res.Add(tmp);
            }
            dataRes.data = res;

        }
    }
}
