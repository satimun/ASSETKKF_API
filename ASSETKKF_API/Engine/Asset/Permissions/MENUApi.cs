using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Request.Permissions;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Permissions;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Engine.Asset.Permissions
{
    public class MENUApi : Base<MENUREq>
    {
        public MENUApi(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }

        protected override void ExecuteChild(MENUREq dataReq, ResponseAPI dataRes)
        {
            DBMode = dataReq.DBMode;
            
            var res = new MENURes();
            res._result.ServerAddr = ConnectionString();
            res._result.DBMode = DBMode;

            try
            {
                switch (dataReq.MODE.Trim().ToLower())
                {
                    case "insert":
                        res = insert(dataReq,conString);
                        break;

                    case "update":
                        res = update(dataReq, conString);
                        break;

                    case "delete":
                        res = delete(dataReq, conString);
                        break;

                    case "search":
                        res = search(dataReq, conString);
                        break;

                    case "check":
                        res = check(dataReq, conString);
                        break;

                    case "active":
                        res = active(dataReq, 1, conString);
                        break;

                    case "inactive":
                        res = active(dataReq, 2, conString);
                        break;

                    default:
                        res = getMenu(dataReq, conString);
                        break;
                }
                               

            }
            catch (SqlException ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Execute exception Error";
            }
            catch (InvalidOperationException ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Connection Exception Error";
            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }

            dataRes.data = res;

        }

        private MENURes check(MENUREq dataReq, string conStr = null)
        {
            var res = new MENURes();
            try
            {
                var lst = ASSETKKF_ADO.Mssql.Asset.STMENUAdo.GetInstant().Search(new STMENU() { MENUCODE = dataReq.MENUCODE },null,conStr);
                if (lst == null || (lst != null && lst.Count == 0))
                {

                    res._result._code = "200";
                    res._result._message = "ข้อมูลพร้อมใช้งาน";
                    res._result._status = "OK";
                }
                else
                {
                    var obj = lst.FirstOrDefault();
                    if (obj.FLAG.Equals("2"))
                    {
                        throw new Exception("รหัสหน้าจอนี้มีอยู่แล้ว แต่ถูกปิดการใช้งาน");
                    }
                    else
                    {
                        throw new Exception("รหัสหน้าจอซ้ำ");
                    }

                }

            }
            catch(Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }
            return res;
        }

        private MENURes insert(MENUREq dataReq, string conStr = null)
        {
            var res = new MENURes();
            try
            {
                var lst = ASSETKKF_ADO.Mssql.Asset.STMENUAdo.GetInstant().Search(new STMENU() { MENUCODE = dataReq.MENUCODE},null,conStr);
                if (lst == null || (lst != null && lst.Count == 0))
                {
                    var req = new STMENU() { 
                        MENUCODE = dataReq.MENUCODE ,
                        MENUNAME = dataReq.MENUNAME,
                        INPID = dataReq.INPID,
                    };

                    var state = ASSETKKF_ADO.Mssql.Asset.STMENUAdo.GetInstant().Insert(req,null,conStr);
                    
                    res._result._code = "200";
                    res._result._message = "บันทึกข้อมูลเรียบร้อยแล้ว";
                    res._result._status = "OK";
                }
                else
                {
                    var obj = lst.FirstOrDefault();
                    if (obj.FLAG.Equals("2"))
                    {
                        throw new Exception("รหัสหน้าจอนี้มีอยู่แล้ว แต่ถูกปิดการใช้งาน");
                    }
                    else
                    {
                        throw new Exception("รหัสหน้าจอซ้ำ");
                    }
                    
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
                var newList = ASSETKKF_ADO.Mssql.Asset.STMENUAdo.GetInstant().Search(new STMENU() { MENUCODE = null },null,conStr);
                res.MENULST = newList;
            }
            return res;
        }

        private MENURes update(MENUREq dataReq, string conStr = null)
        {
            var res = new MENURes();
            try
            {
                var lst = ASSETKKF_ADO.Mssql.Asset.STMENUAdo.GetInstant().Search(new STMENU() { MENUCODE = dataReq.MENUCODE },null,conStr);
                if (lst != null && lst.Count > 0)
                {
                    var req = new STMENU()
                    {
                        MENUCODE = dataReq.MENUCODE,
                        MENUNAME = dataReq.MENUNAME,
                        FLAG = dataReq.FLAG,
                        INPID = dataReq.INPID,
                    };

                    var state = ASSETKKF_ADO.Mssql.Asset.STMENUAdo.GetInstant().Update(req,null,conStr);

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
                var newList = ASSETKKF_ADO.Mssql.Asset.STMENUAdo.GetInstant().Search(new STMENU() { MENUCODE = null },null,conStr);
                res.MENULST = newList;
            }
            return res;
        }

        private MENURes delete(MENUREq dataReq, string conStr = null)
        {
            var res = new MENURes();
            try
            {
                var lst = ASSETKKF_ADO.Mssql.Asset.STPERMISSIONSAdo.GetInstant().Get(new STPERMISSIONS() { MENUCODE = dataReq.MENUCODE },null,conStr);
                if (lst == null || (lst != null && lst.Count == 0))
                {
                    var req = new STMENU()
                    {
                        MENUCODE = dataReq.MENUCODE,
                        MENUNAME = dataReq.MENUNAME,
                        FLAG = dataReq.FLAG,
                        INPID = dataReq.INPID,
                    };

                    var state = ASSETKKF_ADO.Mssql.Asset.STMENUAdo.GetInstant().Delete(req,null,conStr);

                    res._result._code = "200";
                    res._result._message = "ลบข้อมูลเรียบร้อยแล้ว";
                    res._result._status = "OK";
                }
                else
                {
                    throw new Exception("ไม่สามารถลบข้อมูลได้ เนื่องจากหน้าจอนี้ถูกกำหนดสิทธิ์การใช้งานแล้ว");

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
                var newList = ASSETKKF_ADO.Mssql.Asset.STMENUAdo.GetInstant().Search(new STMENU() { MENUCODE = dataReq.MENUCODE },null,conStr);
                res.MENULST = newList;
            }
            return res;
        }

        private MENURes active(MENUREq dataReq,int flag, string conStr = null)
        {
            var res = new MENURes();
            try
            {
                var lst = ASSETKKF_ADO.Mssql.Asset.STMENUAdo.GetInstant().Search(new STMENU() { MENUCODE = dataReq.MENUCODE },null,conStr);
                if (lst != null && lst.Count > 0)
                {
                    var obj = lst.FirstOrDefault();
                    var req = new STMENU()
                    {
                        MENUCODE = obj.MENUCODE,
                        MENUNAME = obj.MENUNAME,
                        FLAG = flag.ToString(),
                        INPID = dataReq.INPID,
                    };

                    var state = ASSETKKF_ADO.Mssql.Asset.STMENUAdo.GetInstant().Update(req,null,conStr);

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
                var newList = ASSETKKF_ADO.Mssql.Asset.STMENUAdo.GetInstant().Search(new STMENU() { MENUCODE = dataReq.MENUCODE },null,conStr);
                res.MENULST = newList;
            }
            return res;
        }


        private MENURes search(MENUREq dataReq, string conStr = null)
        {
            var res = new MENURes();
            try
            {
                var req = new STMENU()
                {
                    MENUCODE = dataReq.MENUCODE,
                    MENUNAME = dataReq.MENUNAME,
                    FLAG = dataReq.FLAG,
                    INPID = dataReq.INPID,
                };

                var lst = ASSETKKF_ADO.Mssql.Asset.STMENUAdo.GetInstant().Search(new STMENU() { MENUCODE = dataReq.MENUCODE },null,conStr);
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
            catch(Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }
            return res;
        }

        private MENURes getMenu(MENUREq dataReq, string conStr = null)
        {
            var res = new MENURes();
            try
            {
                var req = new STMENU()
                {
                    MENUCODE = dataReq.MENUCODE,
                    MENUNAME = dataReq.MENUNAME,
                    FLAG = dataReq.FLAG,
                    INPID = dataReq.INPID,
                };

                var lst = ASSETKKF_ADO.Mssql.Asset.STMENUAdo.GetInstant().ListActive(new STMENU() { MENUCODE = dataReq.MENUCODE },null,conStr);
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

    }
}
