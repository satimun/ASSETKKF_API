using Core.Util;
using ASSETKKF_MODEL.Common;
using ASSETKKF_MODEL.Enum;
using ASSETKKF_MODEL.Request.ERP;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.ERP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Apis.ERP
{
    public class BomReturnStatusErpApi : BaseSend<BomReturnStatusErpReq>
    {
        public BomReturnStatusErpApi()
        {
            PermissionKey = "ADMIN";
        }

        protected override void ExecuteChild(BomReturnStatusErpReq dataReq, ResponseAPI dataRes)
        {
            ResponseErp _result = new ResponseErp();
            var amw_refId = "RBOM" + DateTimeUtil.ToRefId(DateTime.Now) + "-" + "0001";
           

            if (dataReq != null)
            {
                try
                {
                    if ((dataReq.amw_refId != null) && (dataReq.amw_refId.Trim() != ""))
                    {

                        _result.amw_refId = amw_refId;
                        _result.code = "S0001";
                        _result.message = "SUCCESS";
                        _result.status = "S";
                        _result.data = dataReq;
                    }
                    else
                    {
                        _result.amw_refId = amw_refId;
                        _result.code = "F0001";
                        _result.message = "Failed : "+ "กรุณาระบุ amw_refId ";
                        _result.status = "F";
                        _result.data = dataReq;
                    }        
                   
                }
                catch (Exception ex)
                {
                    _result = ExceptionConvert(ex);

                    _result.amw_refId = amw_refId;
                    _result.data = dataReq;

                }

               
            }
            else
            {
                _result.amw_refId = amw_refId;
                _result.code    = "F0000";
                _result.message = "ไม่รองรับข้อมูล == null";
                _result.status  = "F";
                _result.data    = dataReq;
            }

            dataRes.data = _result;

        }

        private ResponseErp ExceptionConvert(Exception ex)
        {
            ResponseErp _result = new ResponseErp();
            string StackTraceMsg = string.Empty;
            StackTraceMsg = ex.StackTrace;
            //map error code, message
            ErrorCode error = EnumUtil.GetEnum<ErrorCode>(ex.Message);
            _result.code = error.ToString();
            if (_result.code == ErrorCode.U000.ToString())
            {
                _result.message = ex.Message;
            }
            else
            {
                _result.message = error.GetDescription();
            }

            _result.status = "F";            

            return _result;
        }

    }
}
