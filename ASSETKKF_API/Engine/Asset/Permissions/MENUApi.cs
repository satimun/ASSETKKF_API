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

                    case "search":
                        res = search(dataReq);
                        break;

                    case "check":
                        res = check(dataReq);
                        break;

                    case "active":
                        res = active(dataReq, 1);
                        break;

                    case "inactive":
                        res = active(dataReq, 2);
                        break;

                    default:
                        res = getMenu(dataReq);
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

        private MENURes check(MENUREq dataReq)
        {
            var res = new MENURes();
            try
            {
                var lst = ASSETKKF_ADO.Mssql.Asset.STMENUAdo.GetInstant(conString).Search(new STMENU() { MENUCODE = dataReq.MENUCODE });
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

        private MENURes insert(MENUREq dataReq)
        {
            var res = new MENURes();
            try
            {
                var lst = ASSETKKF_ADO.Mssql.Asset.STMENUAdo.GetInstant(conString).Search(new STMENU() { MENUCODE = dataReq.MENUCODE});
                if (lst == null || (lst != null && lst.Count == 0))
                {
                    var req = new STMENU() { 
                        MENUCODE = dataReq.MENUCODE ,
                        MENUNAME = dataReq.MENUNAME,
                        INPID = dataReq.INPID,
                    };

                    var state = ASSETKKF_ADO.Mssql.Asset.STMENUAdo.GetInstant(conString).Insert(req);
                    
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
                var newList = ASSETKKF_ADO.Mssql.Asset.STMENUAdo.GetInstant(conString).Search(new STMENU() { MENUCODE = null });
                res.MENULST = newList;
            }
            return res;
        }

        private MENURes update(MENUREq dataReq)
        {
            var res = new MENURes();
            try
            {
                var lst = ASSETKKF_ADO.Mssql.Asset.STMENUAdo.GetInstant(conString).Search(new STMENU() { MENUCODE = dataReq.MENUCODE });
                if (lst != null && lst.Count > 0)
                {
                    var req = new STMENU()
                    {
                        MENUCODE = dataReq.MENUCODE,
                        MENUNAME = dataReq.MENUNAME,
                        FLAG = dataReq.FLAG,
                        INPID = dataReq.INPID,
                    };

                    var state = ASSETKKF_ADO.Mssql.Asset.STMENUAdo.GetInstant(conString).Update(req);

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
                var newList = ASSETKKF_ADO.Mssql.Asset.STMENUAdo.GetInstant(conString).Search(new STMENU() { MENUCODE = null });
                res.MENULST = newList;
            }
            return res;
        }

        private MENURes delete(MENUREq dataReq)
        {
            var res = new MENURes();
            try
            {
                var lst = ASSETKKF_ADO.Mssql.Asset.STPERMISSIONSAdo.GetInstant(conString).Get(new STPERMISSIONS() { MENUCODE = dataReq.MENUCODE });
                if (lst == null || (lst != null && lst.Count == 0))
                {
                    var req = new STMENU()
                    {
                        MENUCODE = dataReq.MENUCODE,
                        MENUNAME = dataReq.MENUNAME,
                        FLAG = dataReq.FLAG,
                        INPID = dataReq.INPID,
                    };

                    var state = ASSETKKF_ADO.Mssql.Asset.STMENUAdo.GetInstant(conString).Delete(req);

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
                var newList = ASSETKKF_ADO.Mssql.Asset.STMENUAdo.GetInstant(conString).Search(new STMENU() { MENUCODE = dataReq.MENUCODE });
                res.MENULST = newList;
            }
            return res;
        }

        private MENURes active(MENUREq dataReq,int flag)
        {
            var res = new MENURes();
            try
            {
                var lst = ASSETKKF_ADO.Mssql.Asset.STMENUAdo.GetInstant(conString).Search(new STMENU() { MENUCODE = dataReq.MENUCODE });
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

                    var state = ASSETKKF_ADO.Mssql.Asset.STMENUAdo.GetInstant(conString).Update(req);

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
                var newList = ASSETKKF_ADO.Mssql.Asset.STMENUAdo.GetInstant(conString).Search(new STMENU() { MENUCODE = dataReq.MENUCODE });
                res.MENULST = newList;
            }
            return res;
        }


        private MENURes search(MENUREq dataReq)
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

                var lst = ASSETKKF_ADO.Mssql.Asset.STMENUAdo.GetInstant(conString).Search(new STMENU() { MENUCODE = dataReq.MENUCODE });
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

        private MENURes getMenu(MENUREq dataReq)
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

                var lst = ASSETKKF_ADO.Mssql.Asset.STMENUAdo.GetInstant(conString).ListActive(new STMENU() { MENUCODE = dataReq.MENUCODE });
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
