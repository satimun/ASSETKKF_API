using Core.Util;
using ASSETKKF_ADO.Mssql.Mcis;
using ASSETKKF_MODEL.Data.Mssql.Mcis;
using ASSETKKF_MODEL.Enum;
using ASSETKKF_MODEL.Request.ERP;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.ERP;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Apis.ERP
{
    public class ItemGroupSyncApi : BaseSend<ItemGroupsReq>
    {
        public ItemGroupSyncApi()
        {
            PermissionKey = "ADMIN";
        }

        protected override void ExecuteChild(ItemGroupsReq dataReq, ResponseAPI dataRes)
        {
            ResponseErp _result = new ResponseErp();
            var amw_refId = "RITG" + DateTimeUtil.ToRefId(DateTime.Now) + "-" + "0001";

            var conn = ASSETKKF_ADO.Mssql.Mcis.Base.OpenConnection();//ตรงนี้สามารถกำหนด สำรอง/จริงได้
            conn.Open();

            List<ItemGroupRes> Item_Group = new List<ItemGroupRes>();


            if (dataReq != null)
            {
                amw_refId = dataReq.amw_refId;
                foreach (var afor in dataReq.item_group)
                {
                    ItemGroupRes _itemGroup  = new ItemGroupRes();
                    lnItemGroup _lnItemGroup = new lnItemGroup();
                    
                    if (NumberUtil.GetID(afor.seq_itemgroup) != null)
                    {
                        _itemGroup.seq_itemgroup = afor.seq_itemgroup;
                        _itemGroup.itemgroup     = afor.itemgroup;
                        _itemGroup.description   = afor.description;

                        _lnItemGroup.seq_itemgroup = afor.seq_itemgroup;
                        _lnItemGroup.itemgroup   = afor.itemgroup;
                        _lnItemGroup.description = afor.description;

                        var iSuccess = true;
                        SqlTransaction tran = conn.BeginTransaction();


                        try
                        {
                           
                            
                            lnItemGroupAdo.GetInstant().Save(_lnItemGroup, "", tran);

                            _itemGroup.code = "S0001";
                            _itemGroup.message = "SUCCESS";
                            _itemGroup.status = "S";
                        }
                        catch (Exception ex)
                        {
                            iSuccess = false;
                            _itemGroup.code = "F0002";
                            _itemGroup.message = "Failed : " + "บันทึกข้อมูลไม่สำเร็จ "+ex.Message;
                            _itemGroup.status = "F";

                        }
                        finally
                        {
                            
                        }

                        if (!iSuccess)
                        {
                            tran.Rollback();
                        }
                        else { tran.Commit(); }

                    }
                    else
                    {
                        _itemGroup.seq_itemgroup = afor.seq_itemgroup;
                        _itemGroup.itemgroup = afor.itemgroup;
                        _itemGroup.description = afor.description;

                        _itemGroup.code = "F0001";
                        _itemGroup.message = "Failed : " + "กรุณาระบุ Seq_Itemgroup ";
                        _itemGroup.status  = "F";
                        
                    }



                    Item_Group.Add(_itemGroup);
                }

                
                _result.amw_refId = amw_refId;
                _result.code = "S0001";
                _result.message = "SUCCESS";
                _result.status = "S";

                _result.data = Item_Group;

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
