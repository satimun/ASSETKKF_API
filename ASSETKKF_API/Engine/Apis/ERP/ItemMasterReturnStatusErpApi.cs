using Core.Util;
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
    public class ItemMasterReturnStatusErpApi : BaseSend<ItemMasterReturnStatusErpReq>
    {
        public ItemMasterReturnStatusErpApi()
        {
            PermissionKey = "ADMIN";
        }

        protected override void ExecuteChild(ItemMasterReturnStatusErpReq dataReq, ResponseAPI dataRes)
        {
            ResponseErp _result = new ResponseErp();
            var amw_refId = "RITM" + DateTimeUtil.ToRefId(DateTime.Now) + "-" + "0001";
            List<ItemMasterReturnStatusErpRes> _resDatas = new List<ItemMasterReturnStatusErpRes>();
            ItemMasterReturnStatusErpRes _data = new ItemMasterReturnStatusErpRes();


            if (dataReq != null)
            {
               
                    if ((dataReq.amw_refId != null) && (dataReq.amw_refId.Trim() != ""))
                    {
                        _result.amw_refId = amw_refId;
                        _result.code = "S0001";
                        _result.message = "SUCCESS";
                        _result.status = "S";
                        _result.data = dataReq;

                        
                        foreach (var afor in dataReq.item_master)
                        {
                            _data = new ItemMasterReturnStatusErpRes();
                            _data.seq_item = NumberUtil.Strtoint(afor.seq_item);
                            _data.item = afor.item;

                            if (afor.seq_item != null)
                            {                            
                                _data.code = "S0001";
                                _data.message = "SUCCESS";
                                _data.status = "S";

                            }
                            else
                            {
                                _data.code = "F0001";
                                _data.message = "Failed : " + "กรุณาระบุ amw_refId ";
                                _data.status = "F";
                            }

                        _resDatas.Add(_data);

                        }

                        _result.data = _resDatas;

                }
                    else
                    {
                        _result.amw_refId = amw_refId;
                        _result.code = "F0001";
                        _result.message = "Failed : " + "กรุณาระบุ amw_refId ";
                        _result.status = "F";
                        _result.data = dataReq;
                    }
                 

            }
            else
            {
                _result.amw_refId = amw_refId;
                _result.code = "F0000";
                _result.message = "ไม่รองรับข้อมูล == null";
                _result.status = "F";
                _result.data = dataReq;
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
 
