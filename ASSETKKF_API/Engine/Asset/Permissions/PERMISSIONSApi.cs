using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Request.Permissions;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Permissions;

namespace ASSETKKF_API.Engine.Asset.Permissions
{
    public class PERMISSIONSApi : Base<PERMISSIONSReq>
    {
        public PERMISSIONSApi()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(PERMISSIONSReq dataReq, ResponseAPI dataRes)
        {
            var res = new PERMISSIONSRes();

            try
            {
                switch (dataReq.MODE.Trim().ToLower())
                {
                    case "insert":
                        res = insert(dataReq);
                        break;

                    case "update":
                        res = update(dataReq);
                        break;

                    case "delete":
                        res = delete(dataReq);
                        break;
                    case "deletebygroup":
                        res = deleteByGroup(dataReq);
                        break;

                    case "search":
                        res = search(dataReq); 
                        break;
                    case "groupuser":
                        res = getGroupUser(dataReq);
                        break;

                    case "groupuseractive":
                        res = getGroupUserActive(dataReq);
                        break;

                    case "getmenu":
                        res = getMenu(dataReq);
                        break;

                    case "getgrouppermission":
                        res = getGroupPermission(dataReq);
                        break;

                    case "validpermission":
                        res = validPermission(dataReq);
                        break;


                    default:
                        res = getPermissions(dataReq); 
                        break;
                }


            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }

            dataRes.data = res;

        }


        private PERMISSIONSRes insert(PERMISSIONSReq dataReq)
        {
            var res = new PERMISSIONSRes();
            try
            {
                var lst = ASSETKKF_ADO.Mssql.Asset.STPERMISSIONSAdo.GetInstant().Get(new STPERMISSIONS() { MENUCODE = dataReq.MENUCODE ,GUCODE = dataReq.GUCODE , COMPANY = dataReq.COMPANY });
                if (lst == null || (lst != null && lst.Count == 0))
                {
                    var req = new STPERMISSIONS()
                    {
                        MENUCODE = dataReq.MENUCODE,
                        GUCODE = dataReq.GUCODE,
                        COMPANY = dataReq.COMPANY,
                        P_ACCESS = dataReq.P_ACCESS,
                        P_MANAGE = dataReq.P_MANAGE,
                        P_DELETE = dataReq.P_DELETE,
                        P_APPROVE = dataReq.P_APPROVE,
                        P_EXPORT = dataReq.P_EXPORT,
                        INPID = dataReq.INPID,
                    };

                    var state = ASSETKKF_ADO.Mssql.Asset.STPERMISSIONSAdo.GetInstant().Insert(req);

                    res._result._code = "200";
                    res._result._message = "บันทึกข้อมูลเรียบร้อยแล้ว";
                    res._result._status = "OK";
                }
                else
                {
                    throw new Exception("กำหนดสิทธิ์การใช้งานซ้ำ");

                }
            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }
            finally
            {
                var newList = ASSETKKF_ADO.Mssql.Asset.STPERMISSIONSAdo.GetInstant().ListActive(new STPERMISSIONS() { COMPANY = dataReq.COMPANY });
                res.PERMISSIONSLST = newList;
            }
            return res;
        }


        private PERMISSIONSRes update(PERMISSIONSReq dataReq)
        {
            var res = new PERMISSIONSRes();
            try
            {
                var lst = ASSETKKF_ADO.Mssql.Asset.STPERMISSIONSAdo.GetInstant().Get(new STPERMISSIONS() { MENUCODE = dataReq.MENUCODE, GUCODE = dataReq.GUCODE, COMPANY = dataReq.COMPANY });
                if (lst != null && lst.Count > 0)
                {
                    var req = new STPERMISSIONS()
                    {
                        MENUCODE = dataReq.MENUCODE,
                        GUCODE = dataReq.GUCODE,
                        COMPANY = dataReq.COMPANY,
                        P_ACCESS = dataReq.P_ACCESS,
                        P_MANAGE = dataReq.P_MANAGE,
                        P_DELETE = dataReq.P_DELETE,
                        P_APPROVE = dataReq.P_APPROVE,
                        P_EXPORT = dataReq.P_EXPORT,
                        INPID = dataReq.INPID,
                    };

                    var state = ASSETKKF_ADO.Mssql.Asset.STPERMISSIONSAdo.GetInstant().Update(req);

                    res._result._code = "200";
                    res._result._message = "บันทึกข้อมูลเรียบร้อยแล้ว";
                    res._result._status = "OK";
                }
                else
                {
                    res._result._code = "404";
                    res._result._message = "ไม่พบข้อมูล";
                    res._result._status = "Not Found";

                }
            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }
            finally
            {
                var newList = ASSETKKF_ADO.Mssql.Asset.STPERMISSIONSAdo.GetInstant().ListActive(new STPERMISSIONS() { COMPANY = dataReq.COMPANY });
                res.PERMISSIONSLST = newList;
            }
            return res;
        }

        private PERMISSIONSRes delete(PERMISSIONSReq dataReq)
        {
            var res = new PERMISSIONSRes();
            try
            {
                var lst = ASSETKKF_ADO.Mssql.Asset.STPERMISSIONSAdo.GetInstant().Get(new STPERMISSIONS() { MENUCODE = dataReq.MENUCODE, GUCODE = dataReq.GUCODE, COMPANY = dataReq.COMPANY });
                if (lst != null && lst.Count > 0)
                {
                    var req = new STPERMISSIONS()
                    {
                        MENUCODE = dataReq.MENUCODE,
                        GUCODE = dataReq.GUCODE,
                        COMPANY = dataReq.COMPANY,
                        P_ACCESS = dataReq.P_ACCESS,
                        P_MANAGE = dataReq.P_MANAGE,
                        P_DELETE = dataReq.P_DELETE,
                        P_APPROVE = dataReq.P_APPROVE,
                        P_EXPORT = dataReq.P_EXPORT,
                        INPID = dataReq.INPID,
                    };

                    var state = ASSETKKF_ADO.Mssql.Asset.STPERMISSIONSAdo.GetInstant().Delete(req);

                    res._result._code = "200";
                    res._result._message = "ลบข้อมูลเรียบร้อยแล้ว";
                    res._result._status = "OK";
                }
                else
                {
                    res._result._code = "404";
                    res._result._message = "ไม่พบข้อมูล";
                    res._result._status = "Not Found";

                }
            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }
            finally
            {
                var newList = ASSETKKF_ADO.Mssql.Asset.STPERMISSIONSAdo.GetInstant().ListActive(new STPERMISSIONS() { COMPANY = dataReq.COMPANY });
                res.PERMISSIONSLST = newList;
            }
            return res;
        }

        private PERMISSIONSRes deleteByGroup(PERMISSIONSReq dataReq)
        {
            var res = new PERMISSIONSRes();
            try
            {
                var lst = ASSETKKF_ADO.Mssql.Asset.STPERMISSIONSAdo.GetInstant().Get(new STPERMISSIONS() { MENUCODE = dataReq.MENUCODE, GUCODE = dataReq.GUCODE, COMPANY = dataReq.COMPANY });
                if (lst != null && lst.Count > 0)
                {
                    var req = new STPERMISSIONS()
                    {
                        MENUCODE = dataReq.MENUCODE,
                        GUCODE = dataReq.GUCODE,
                        COMPANY = dataReq.COMPANY,
                        P_ACCESS = dataReq.P_ACCESS,
                        P_MANAGE = dataReq.P_MANAGE,
                        P_DELETE = dataReq.P_DELETE,
                        P_APPROVE = dataReq.P_APPROVE,
                        P_EXPORT = dataReq.P_EXPORT,
                        INPID = dataReq.INPID,
                    };

                    var state = ASSETKKF_ADO.Mssql.Asset.STPERMISSIONSAdo.GetInstant().DeleteAllByGroup(req);

                    res._result._code = "200";
                    res._result._message = "ลบข้อมูลเรียบร้อยแล้ว";
                    res._result._status = "OK";
                }
                else
                {
                    res._result._code = "404";
                    res._result._message = "ไม่พบข้อมูล";
                    res._result._status = "Not Found";

                }
            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }
            finally
            {
                var newList = ASSETKKF_ADO.Mssql.Asset.STPERMISSIONSAdo.GetInstant().ListActive(new STPERMISSIONS() { COMPANY = dataReq.COMPANY });
                res.PERMISSIONSLST = newList;
            }
            return res;
        }

        private PERMISSIONSRes search(PERMISSIONSReq dataReq)
        {
            var res = new PERMISSIONSRes();
            try
            {
                var req = new STPERMISSIONS()
                {
                    MENUCODE = dataReq.MENUCODE,
                    GUCODE = dataReq.GUCODE,
                    COMPANY = dataReq.COMPANY,
                    P_ACCESS = dataReq.P_ACCESS,
                    P_MANAGE = dataReq.P_MANAGE,
                    P_DELETE = dataReq.P_DELETE,
                    P_APPROVE = dataReq.P_APPROVE,
                    P_EXPORT = dataReq.P_EXPORT,
                    INPID = dataReq.INPID,
                };

                var lst = ASSETKKF_ADO.Mssql.Asset.STPERMISSIONSAdo.GetInstant().Search(new STPERMISSIONS() { MENUCODE = dataReq.MENUCODE, GUCODE = dataReq.GUCODE, COMPANY = dataReq.COMPANY });
                res.PERMISSIONSLST = lst;

                if (lst != null && lst.Count > 0)
                {
                    res._result._code = "200";
                    res._result._message = "";
                    res._result._status = "OK";
                }
                else
                {
                    res._result._code = "404";
                    res._result._message = "ไม่พบข้อมูล";
                    res._result._status = "Not Found";
                }

            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }
            return res;
        }

        private PERMISSIONSRes getPermissions(PERMISSIONSReq dataReq)
        {
            var res = new PERMISSIONSRes();
            try
            {
                var req = new STPERMISSIONS()
                {
                    MENUCODE = dataReq.MENUCODE,
                    GUCODE = dataReq.GUCODE,
                    COMPANY = dataReq.COMPANY,
                    P_ACCESS = dataReq.P_ACCESS,
                    P_MANAGE = dataReq.P_MANAGE,
                    P_DELETE = dataReq.P_DELETE,
                    P_APPROVE = dataReq.P_APPROVE,
                    P_EXPORT = dataReq.P_EXPORT,
                    INPID = dataReq.INPID,
                };

                var lst = ASSETKKF_ADO.Mssql.Asset.STPERMISSIONSAdo.GetInstant().ListActive(new STPERMISSIONS() { MENUCODE = dataReq.MENUCODE, GUCODE = dataReq.GUCODE, COMPANY = dataReq.COMPANY });
                res.PERMISSIONSLST = lst;

                if (lst != null && lst.Count > 0)
                {
                    res._result._code = "200";
                    res._result._message = "";
                    res._result._status = "OK";
                }
                else
                {
                    res._result._code = "404";
                    res._result._message = "ไม่พบข้อมูล";
                    res._result._status = "Not Found";
                }

            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }
            return res;
        }

        private PERMISSIONSRes getGroupUserActive(PERMISSIONSReq dataReq)
        {
            var res = new PERMISSIONSRes();
            try
            {
                var req = new STPERMISSIONS()
                {
                    MENUCODE = dataReq.MENUCODE,
                    GUCODE = dataReq.GUCODE,
                    COMPANY = dataReq.COMPANY,
                    P_ACCESS = dataReq.P_ACCESS,
                    P_MANAGE = dataReq.P_MANAGE,
                    P_DELETE = dataReq.P_DELETE,
                    P_APPROVE = dataReq.P_APPROVE,
                    P_EXPORT = dataReq.P_EXPORT,
                    INPID = dataReq.INPID,
                };

                var lst = ASSETKKF_ADO.Mssql.Asset.STPERMISSIONSAdo.GetInstant().GroupUserActive(new STPERMISSIONS() { GUCODE = dataReq.GUCODE, COMPANY = dataReq.COMPANY });
                res.PERMISSIONSLST = lst;

                if (lst != null && lst.Count > 0)
                {
                    res._result._code = "200";
                    res._result._message = "";
                    res._result._status = "OK";
                }
                else
                {
                    res._result._code = "404";
                    res._result._message = "ไม่พบข้อมูล";
                    res._result._status = "Not Found";
                }

            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }
            return res;
        }


        private PERMISSIONSRes getGroupUser(PERMISSIONSReq dataReq)
        {
            var res = new PERMISSIONSRes();
            try
            {
                var req = new STPERMISSIONS()
                {
                    MENUCODE = dataReq.MENUCODE,
                    GUCODE = dataReq.GUCODE,
                    COMPANY = dataReq.COMPANY,
                    P_ACCESS = dataReq.P_ACCESS,
                    P_MANAGE = dataReq.P_MANAGE,
                    P_DELETE = dataReq.P_DELETE,
                    P_APPROVE = dataReq.P_APPROVE,
                    P_EXPORT = dataReq.P_EXPORT,
                    INPID = dataReq.INPID,
                };

                var lst = ASSETKKF_ADO.Mssql.Asset.STPERMISSIONSAdo.GetInstant().getGROUPUSER(new STPERMISSIONS() { COMPANY = dataReq.COMPANY });
                res.GROUPUSERLST = lst;

                if (lst != null && lst.Count > 0)
                {
                    res._result._code = "200";
                    res._result._message = "";
                    res._result._status = "OK";
                }
                else
                {
                    res._result._code = "404";
                    res._result._message = "ไม่พบข้อมูล";
                    res._result._status = "Not Found";
                }

            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }
            return res;
        }

        private PERMISSIONSRes getMenu(PERMISSIONSReq dataReq)
        {
            var res = new PERMISSIONSRes();
            try
            {
                var req = new STPERMISSIONS()
                {
                    MENUCODE = dataReq.MENUCODE,
                    GUCODE = dataReq.GUCODE,
                    COMPANY = dataReq.COMPANY,
                    P_ACCESS = dataReq.P_ACCESS,
                    P_MANAGE = dataReq.P_MANAGE,
                    P_DELETE = dataReq.P_DELETE,
                    P_APPROVE = dataReq.P_APPROVE,
                    P_EXPORT = dataReq.P_EXPORT,
                    INPID = dataReq.INPID,
                };

                var lst = ASSETKKF_ADO.Mssql.Asset.STPERMISSIONSAdo.GetInstant().ListMenu(new STPERMISSIONS() { MENUCODE = dataReq.MENUCODE, GUCODE = dataReq.GUCODE, COMPANY = dataReq.COMPANY });
                res.MENULST = lst;

                if (lst != null && lst.Count > 0)
                {
                    res._result._code = "200";
                    res._result._message = "";
                    res._result._status = "OK";
                }
                else
                {
                    res._result._code = "404";
                    res._result._message = "ไม่พบข้อมูล";
                    res._result._status = "Not Found";
                }

            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }
            return res;
        }

        private PERMISSIONSRes getGroupPermission(PERMISSIONSReq dataReq)
        {
            var res = new PERMISSIONSRes();
            try
            {
                var req = new STPERMISSIONS()
                {
                    MENUCODE = dataReq.MENUCODE,
                    GUCODE = dataReq.GUCODE,
                    COMPANY = dataReq.COMPANY,
                    P_ACCESS = dataReq.P_ACCESS,
                    P_MANAGE = dataReq.P_MANAGE,
                    P_DELETE = dataReq.P_DELETE,
                    P_APPROVE = dataReq.P_APPROVE,
                    P_EXPORT = dataReq.P_EXPORT,
                    INPID = dataReq.INPID,
                };

                var lst = ASSETKKF_ADO.Mssql.Asset.STPERMISSIONSAdo.GetInstant().GroupPermissions(req);
                res.PERMISSIONSLST = lst;

                if (lst != null && lst.Count > 0)
                {
                    res._result._code = "200";
                    res._result._message = "";
                    res._result._status = "OK";
                }
                else
                {
                    res._result._code = "404";
                    res._result._message = "ไม่พบข้อมูล";
                    res._result._status = "Not Found";
                }

            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }
            return res;
        }

        private PERMISSIONSRes validPermission(PERMISSIONSReq dataReq)
        {
            var res = new PERMISSIONSRes();
            try
            {
                var userReq = new ASSETKKF_MODEL.Data.Mssql.Asset.STUSERASSET(){
                    UCODE = dataReq.INPID,
                    COMPANY = dataReq.COMPANY,
                };
                var userLst = ASSETKKF_ADO.Mssql.Asset.STUSERASSETAdo.GetInstant().Search(userReq);
                var objUser = userLst != null ? userLst.FirstOrDefault() : null;
                var gucode = objUser != null ? objUser.GUCODE : null;
                var req = new STPERMISSIONS()
                {
                    MENUCODE = dataReq.MENUCODE,
                    GUCODE = gucode,
                    COMPANY = dataReq.COMPANY,
                    P_ACCESS = dataReq.P_ACCESS,
                    P_MANAGE = dataReq.P_MANAGE,
                    P_DELETE = dataReq.P_DELETE,
                    P_APPROVE = dataReq.P_APPROVE,
                    P_EXPORT = dataReq.P_EXPORT,
                    INPID = dataReq.INPID,
                };

                var lst = ASSETKKF_ADO.Mssql.Asset.STPERMISSIONSAdo.GetInstant().Valid(req);
                res.PERMISSIONSLST = lst;

                if (lst != null && lst.Count > 0)
                {
                    res.hasPermission = true;
                    res._result._code = "200";
                    res._result._message = "";
                    res._result._status = "OK";
                }
                else
                {
                    res.hasPermission = false;
                    res._result._code = "404";
                    res._result._message = "ไม่พบข้อมูล";
                    res._result._status = "Not Found";
                }

            }
            catch (Exception ex)
            {
                res.hasPermission = false;
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }
            return res;
        }


    }
}

